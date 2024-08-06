using RealEstate.Api.DTO.PropertyStatusDTO;
using RealEstate.Api.Entities;

namespace RealEstate.Api.DTO.PropertyTypesDTO
{
    public class PropertyTypeInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static PropertyTypeInfoDTO FromPropertyType(PropertyTypes propertyTypes)
        {
            return new PropertyTypeInfoDTO()
            {
                Id = propertyTypes.Id,
                Name = propertyTypes.Name,
            };
        }
    }
}
