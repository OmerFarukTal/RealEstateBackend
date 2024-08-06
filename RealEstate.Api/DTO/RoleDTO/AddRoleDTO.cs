using RealEstate.Api.Entities;

namespace RealEstate.Api.DTO.RoleDTO
{
    public class AddRoleDTO
    {
        public string Name {get;set;}

        public Roles ToRole()
        {
            return new Roles()
            {
                Name = this.Name
            };
        }
    }
}
