using RealEstate.Api.Entities;
using System.Text;
using System.Security.Cryptography;

namespace RealEstate.Api.DTO.UserDTO
{
    public class AddUserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }
        
        public Users ToUser()
        {
            SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(Password));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            string hashedPassword = builder.ToString();
            return new Users()
            {
                UserName = this.UserName,
                Password = hashedPassword,
                RoleId = this.RoleId,
                Email = this.Email
            };
        }
    }
}
