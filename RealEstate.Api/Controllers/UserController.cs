using Microsoft.AspNetCore.Mvc;
using RealEstate.Api.Context;
using RealEstate.Api.DTO.UserDTO;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;


namespace RealEstate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly RealEstateContext context;

        public UserController(RealEstateContext context)
        {
            this.context = context;
        }

        [HttpPost]
        [Route("sign")]
        public IActionResult SignUp(AddUserDTO addUserDTO)
        {   
            var user = context.Users.FirstOrDefault(x => x.UserName.Equals(addUserDTO.UserName) && !x.IsDeleted);
            if (user != null) return BadRequest();

            var response = context.Users.Add(addUserDTO.ToUser());
            context.SaveChanges();

            var addedUser = context.Users.Include(a => a.Role).FirstOrDefault(x => response.Entity.Id == x.Id && !x.IsDeleted);


            return Ok(UserInfoDTO.FromUser(addedUser));

        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginUserDTO loginUserDTO)
        {
            var user = context.Users.Include(a => a.Role).FirstOrDefault(x => x.UserName.Equals(loginUserDTO.UserName) && !x.IsDeleted);
            if (user == null) return NotFound();

            SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(loginUserDTO.Password));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            string hashedPassword = builder.ToString();

            if (!user.Password.Equals(hashedPassword)) return BadRequest();
            return Ok(UserInfoDTO.FromUser(user));
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var user = context.Users.Include(a => a.Role).FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (user == null) return NotFound(new {message = "This user does not exist." });

            return Ok(UserInfoDTO.FromUser(user));
        }

        [HttpGet]
        [Route("role")]
        public IActionResult Role(int id)
        {
            var user = context.Users.Include(a => a.Role).FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (user == null) return NotFound(new { message = "This user does not exist." });

            return Ok(new { message = user.Role.Name.ToString() });
        }

        [HttpGet]
        [Route("idFromUsername")]
        public IActionResult IdFromUserName(string userName)
        {
            var user = context.Users.FirstOrDefault(x => x.UserName.Equals(userName) && !x.IsDeleted);
            if (user == null) return NotFound(new { message = "This user does not exist." });

            return Ok(new { message = user.Id.ToString() });
        }

        [HttpGet]
        [Route("isAuthenticatedRoleName")]
        public IActionResult IsAuthenticatedRoleName(int id, string requiredRole)
        {
            var user = context.Users.Include(a => a.Role).FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (user == null) return NotFound();

            if (requiredRole == null) return BadRequest();

            if (user.Role.Name.Equals(requiredRole)) return Ok();
            return BadRequest();

        }

        [HttpGet]
        [Route("isAuthenticatedRoleId")]
        public IActionResult IsAuthenticatedRoleID(int id, int requiredRole)
        {
            var user = context.Users.Include(a => a.Role).FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (user == null) return NotFound();

            if (requiredRole == null || requiredRole <= 0) return BadRequest();

            if (user.Role.Id == requiredRole) return Ok();
            return BadRequest();

        }

    }
}
