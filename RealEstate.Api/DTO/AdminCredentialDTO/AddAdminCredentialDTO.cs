using RealEstate.Api.Entities;
using System.Text;
using System.Security.Cryptography;

namespace RealEstate.Api.DTO.AdminCredentialDTO
{
    public class AddAdminCredentialDTO
    {
        public string Credential {  get; set; }

        public AdminCredential ToAdminCredential()
        {
            SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(Credential));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            string hashedCredential = builder.ToString();

            return new AdminCredential()
            {
                Credential = hashedCredential,
            };
        }
    }
}
