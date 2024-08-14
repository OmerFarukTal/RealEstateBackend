using RealEstate.Api.Entities;

namespace RealEstate.Api.DTO.PropertyDTO
{
    public class AddPropertyDTO
    {
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

        public Properties ToProperty()
        {
            return new Properties()
            {
                Name = Name,
                Adress = Adress,
                Latitude = Latitude,
                Longitude = Longitude,
                Description = Description,
                PropertyTypeId = PropertyTypeId,
                PropertyStatusId = PropertyStatusId,
                StartDate = StartDate,
                EndDate = EndDate,
                Price = Price,
                CurrencyId = CurrencyId,
                CreatorId = CreatorId,
                CreatedDate = CreatedDate,

            };
        }
    }
}
