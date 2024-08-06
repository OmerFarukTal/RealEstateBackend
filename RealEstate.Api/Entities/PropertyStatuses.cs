namespace RealEstate.Api.Entities
{
    public class PropertyStatuses : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Properties> Properties { get; set; }
    }
}
