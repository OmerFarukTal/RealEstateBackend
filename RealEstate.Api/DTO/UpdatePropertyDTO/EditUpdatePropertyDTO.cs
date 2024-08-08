namespace RealEstate.Api.DTO.UpdatePropertyDTO
{
    public class EditUpdatePropertyDTO
    {
        public int Id { get; set; }

        public int PropertyId { get; set; }
        public int UpdatorId { get; set; }

        public string UpdateReason { get; set; }
    }
}
