using RealEstate.Api.Entities;

namespace RealEstate.Api.DTO.UpdatePropertyDTO
{
    public class AddUpdatePropertyDTO
    {
        public int PropertyId { get; set; }
        public int UpdatorId { get; set; }

        public DateTime UpdateDate { get; set; }
        public string UpdateReason { get; set; }

        public UpdateProperty ToUpdateProperty()
        {
            return new UpdateProperty()
            {
                PropertyId = PropertyId,
                UpdatorId = UpdatorId,
                UpdateDate = UpdateDate,
                UpdateReason = UpdateReason
            };
        }
    }
}
