using RealEstate.Api.Entities;

namespace RealEstate.Api.DTO.ImageDTO
{
    public class ImageInfoDTO
    {
        public int Id { get; set; }
        public string PropertyName { get; set; }
        public string Source { get; set; }
        public string Name { get; set; }

        public static ImageInfoDTO FromImage(Images images)
        {
            return new ImageInfoDTO
            {
                Id = images.Id,
                Name = images.Name,
                Source = images.Source,
                PropertyName = images.Property.Name,
            };
        }
    }
}
