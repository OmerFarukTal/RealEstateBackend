using Microsoft.AspNetCore.Mvc;
using RealEstate.Api.Context;
using RealEstate.Api.DTO.RoleDTO;

namespace RealEstate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RealEstateContext context;

        public RoleController(RealEstateContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult AddRole([FromBody] AddRoleDTO addRoleDTO)
        {
            var response = context.Roles.Add(addRoleDTO.ToRole());
            context.SaveChanges();

            return Ok(RoleInfoDTO.FromRole(response.Entity));
        }


        [HttpPut]
        public IActionResult ModifyType(EditRoleDTO editRoleDTO)
        {
            string name = editRoleDTO.Name;
            int id = editRoleDTO.Id;

            var role = context.Roles.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (role == null) return NotFound(new { message = "This role does not exist" });

            role.Name = name;
            context.SaveChanges();

            return Ok(RoleInfoDTO.FromRole(role));
        }


        [HttpGet]
        public IActionResult GetType(int id)
        {
            var role = context.Roles.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (role == null) return NotFound(new { message = "This role does not exist" });

            return Ok(RoleInfoDTO.FromRole(role));
        }

        [HttpDelete]
        public IActionResult DeleteRole(int id)
        {
            var role = context.Roles.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (role == null) return NotFound(new { message = "This role does not exist" });

            role.IsDeleted = true;
            context.SaveChanges();

            return Ok(new { message = "Role " + role.Id + " with name " + role.Name + " is deleted" });
        }

        [HttpGet]
        [Route("list")]
        public IActionResult GetAll()
        {

            var role = context.Roles.ToList();
            if (role == null) return NotFound();

            List<RoleInfoDTO> listDTO = new List<RoleInfoDTO>();
            role.ForEach(x =>
            {
                if (!x.IsDeleted) listDTO.Add(RoleInfoDTO.FromRole(x));
            });

            return Ok(listDTO);

        }
    }
}
