﻿namespace RealEstate.Api.Entities
{
    public class Properties : BaseEntity
    {
        public string Name { get; set; }

        public string Adress { get; set; }
        
        public int Latitude { get; set; }
        public int Longitude { get; set; }
 
        public string Description { get; set; }
        public int PropertyTypeId { get; set; }
        public PropertyTypes PropertyType { get; set; }

        public int PropertyStatusId { get; set; }
        public PropertyStatuses PropertyStatus { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public double Price { get; set; }

        public int CurrencyId { get; set; }
        public Currencies Currency { get; set; }

        public ICollection<Images> Images {  get; set; }

        public int CreatorId { get; set; }
        public Users Creator { get; set; }

        public ICollection<UpdateProperty> UpdateProperties { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
