namespace RealEstate.Api.DTO.ImageDTO
{
    public class EditImageDTO
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string Source { get; set; }
        public string Name { get; set; }
    }
}
