using RealEstate.Api.Entities;

namespace RealEstate.Api.DTO.UserDTO
{
    public class UserInfoDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }

        public static UserInfoDTO FromUser(Users user )
        {

            return new UserInfoDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                RoleName = user.Role.Name,
                Email = user.Email,
            };
        }
    }
}
