using Microsoft.AspNetCore.Mvc;
using RealEstate.Api.Context;
using RealEstate.Api.DTO.AdminCredentialDTO;
using System.Text;
using System.Security.Cryptography;


namespace RealEstate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminCredentialController : ControllerBase
    {
        private readonly RealEstateContext context;

        public AdminCredentialController(RealEstateContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult addCredential([FromBody] AddAdminCredentialDTO addAdminCredentialDTO)
        {
            var response = context.AdminCredential.Add(addAdminCredentialDTO.ToAdminCredential());
            context.SaveChanges();

            return Ok(AdminCredentialInfoDTO.FromAdminCredential(response.Entity));
        }

        [HttpDelete]
        public IActionResult deleteCredential(int id)
        {
            var adminCredential = context.AdminCredential.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (adminCredential == null) return NotFound();

            adminCredential.IsDeleted = true;
            context.SaveChanges();

            return Ok();
        }


        [HttpGet]
        public IActionResult checkCredential(string credentialToCheck)
        {
            SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(credentialToCheck));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            string hashedCredential = builder.ToString();


            var adminCredential = context.AdminCredential.FirstOrDefault(x => x.Credential.Equals(hashedCredential) && !x.IsDeleted);            
            if (adminCredential == null)  return NotFound();

            return Ok();
        }



    }
}
