using RealEstate.Api.Entities;

namespace RealEstate.Api.DTO.RoleDTO
{
    public class RoleInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static RoleInfoDTO FromRole(Roles roles)
        {
            return new RoleInfoDTO
            {   
                Id = roles.Id,
                Name = roles.Name,
            };
        }
    }
}
