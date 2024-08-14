using RealEstate.Api.Entities;

namespace RealEstate.Api.DTO.PropertyDTO
{
    public class RawPropertyInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Adress { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }

        public string Description { get; set; }

        public int PropertyTypeId { get; set; }
        public int PropertyStatusId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public double Price { get; set; }

        public int CurrencyId { get; set; }

        public int CreatorId { get; set; }

        public DateTime CreatedDate { get; set; }


        public static RawPropertyInfoDTO FromProperty(Properties property)
        {
            return new RawPropertyInfoDTO()
            {
                Id = property.Id,
                Name = property.Name,
                Adress = property.Adress,
                Latitude = property.Latitude,
                Longitude = property.Longitude,
                Description = property.Description,
                PropertyTypeId = property.PropertyType.Id,
                PropertyStatusId = property.PropertyStatus.Id,
                StartDate = property.StartDate,
                EndDate = property.EndDate,
                Price = property.Price,
                CurrencyId = property.Currency.Id,
                CreatorId = property.Creator.Id,
                CreatedDate = property.CreatedDate,
            };
        }

    }
}
