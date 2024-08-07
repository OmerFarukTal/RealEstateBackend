namespace RealEstate.Api.Entities
{
    public class UpdateProperty : BaseEntity
    {
        public int PropertyId { get; set; }
        public int UpdatorId { get; set; }

        public DateTime UpdateDate { get; set; }

        public string UpdateReason { get; set; }
    }
}
