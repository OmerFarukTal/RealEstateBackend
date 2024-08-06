using RealEstate.Api.Entities;

namespace RealEstate.Api.DTO.PropertyStatusDTO
{
    public class PropertyStatusInfoDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static PropertyStatusInfoDTO FromPropertyStatus(PropertyStatuses propertyStatus)
        {
            return new PropertyStatusInfoDTO()
            {
                Id = propertyStatus.Id,
                Name = propertyStatus.Name,
            };
        }
    }
}
