using RealEstate.Api.Entities;

namespace RealEstate.Api.DTO.UpdatePropertyDTO
{
    public class UpdatePropertyInfoDTO
    {
        public int Id { get; set; }

        public int PropertyId { get; set; }
        public int UpdatorId { get; set; }

        public DateTime UpdateDate { get; set; }
        public string UpdateReason { get; set; }

        public static UpdatePropertyInfoDTO FromUpdateProperty(UpdateProperty updateProperty) 
        {
            return new UpdatePropertyInfoDTO
            {
                Id = updateProperty.Id,
                PropertyId = updateProperty.PropertyId,
                UpdatorId = updateProperty.UpdatorId,
                UpdateDate = updateProperty.UpdateDate,
                UpdateReason = updateProperty.UpdateReason,
            };
        }
    }
}
