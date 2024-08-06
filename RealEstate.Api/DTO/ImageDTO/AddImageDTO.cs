using RealEstate.Api.Entities;

namespace RealEstate.Api.DTO.ImageDTO
{
    public class AddImageDTO
    {
        public string Name { get; set; }
        public string Source { get; set; }
        public int PropertyId { get; set; }

        public Images ToImage()
        {
            return new Images()
            {
                Name = this.Name,
                Source = this.Source,
                PropertyId = this.PropertyId
            };
        }
    }
}
