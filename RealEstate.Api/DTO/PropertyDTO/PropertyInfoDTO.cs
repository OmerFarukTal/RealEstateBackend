using RealEstate.Api.Entities;

namespace RealEstate.Api.DTO.PropertyDTO
{
    public class PropertyInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string PropertyTypeName { get; set; }
        public string PropertyStatusName { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public double Price { get; set; }

        public string CurrencyName { get; set; }

        public string CreatorName { get; set; }

        public DateTime CreatedDate { get; set; }


        public static PropertyInfoDTO FromProperty(Properties property) 
        {
            return new PropertyInfoDTO()
            {
                Id = property.Id,
                Name = property.Name,
                Description = property.Description,
                PropertyTypeName = property.PropertyType.Name,
                PropertyStatusName = property.PropertyStatus.Name,
                StartDate = property.StartDate,
                EndDate = property.EndDate,
                Price = property.Price,
                CurrencyName = property.Currency.Name,
                CreatorName = property.Creator.UserName,
                CreatedDate = property.CreatedDate,
            };
        }

    }
}
