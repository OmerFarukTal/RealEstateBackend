using RealEstate.Api.Entities;

namespace RealEstate.Api.DTO.PropertyStatusDTO
{
    public class AddPropertyStatusDTO
    {
        public string Name { get; set; }
        public PropertyStatuses ToPropertyStatus()
        {
            return new PropertyStatuses()
            {
                Name = this.Name
            }; 

        }
    }
}
