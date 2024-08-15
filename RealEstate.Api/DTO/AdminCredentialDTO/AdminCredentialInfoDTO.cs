using RealEstate.Api.Entities;

namespace RealEstate.Api.DTO.AdminCredentialDTO
{
    public class AdminCredentialInfoDTO
    {
        public int Id { get; set; }
        public string Credential { get; set; }


        public static AdminCredentialInfoDTO FromAdminCredential(AdminCredential adminCredential)
        {
            return new AdminCredentialInfoDTO()
            {
                Id = adminCredential.Id,
                Credential = adminCredential.Credential,
            };
        }
    }
}
