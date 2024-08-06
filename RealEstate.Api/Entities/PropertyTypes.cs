namespace RealEstate.Api.Entities
{
    public class PropertyTypes : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Properties> Properties { get; set; }

    }
}
