using Microsoft.AspNetCore.Identity;

namespace RealEstate.Api.Entities
{
    public class Users :  BaseEntity 
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public int RoleId { get; set; }
        public Roles Role {  get; set; }

        public string Email { get; set; }

        public ICollection<Properties> Properties { get; set; }

        public ICollection<UpdateProperty> UpdateProperties { get; set; }
    }
}
