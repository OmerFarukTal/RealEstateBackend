using RealEstate.Api.Entities;

namespace RealEstate.Api.DTO.PropertyTypesDTO
{
    public class AddPropertyTypeDTO
    {
        public string Name { get; set; }

        public PropertyTypes ToPropertyType()
        {
            return new PropertyTypes()
            {
                Name = this.Name,
            };
        }
    }
}
