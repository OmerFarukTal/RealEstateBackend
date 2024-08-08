using Microsoft.AspNetCore.Mvc;
using RealEstate.Api.Context;
using RealEstate.Api.DTO.UpdatePropertyDTO;

namespace RealEstate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdatePropertyController : ControllerBase
    {

        private readonly RealEstateContext context;

        public UpdatePropertyController(RealEstateContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult AddUpdateProperty([FromBody] AddUpdatePropertyDTO addUpdatePropertyDTO)
        {
            var response = context.UpdateProperties.Add(addUpdatePropertyDTO.ToUpdateProperty());
            context.SaveChanges();

            return Ok(UpdatePropertyInfoDTO.FromUpdateProperty(response.Entity));
        }

        [HttpPut]
        public IActionResult ModifyUpdateProperty([FromBody] EditUpdatePropertyDTO editUpdatePropertyDTO)
        {
            int propertyId = editUpdatePropertyDTO.PropertyId;
            int updatorId = editUpdatePropertyDTO.UpdatorId;
            string updateReason = editUpdatePropertyDTO.UpdateReason;

            var updateProperty = context.UpdateProperties.FirstOrDefault(x => x.Id == editUpdatePropertyDTO.Id && !x.IsDeleted);
            if (updateProperty == null) return NotFound();

            updateProperty.UpdateReason = updateReason;
            updateProperty.UpdatorId = updatorId;
            updateProperty.PropertyId = propertyId;
            
            context.SaveChanges();

            return Ok(UpdatePropertyInfoDTO.FromUpdateProperty(updateProperty));

        }

        [HttpDelete]
        public IActionResult DeleteUpdateProperty(int id)
        {
            var updateProperty = context.UpdateProperties.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (updateProperty == null) return NotFound();

            updateProperty.IsDeleted = true;
            context.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public IActionResult GetUpdateProperty(int id)
        {
            var updateProperty = context.UpdateProperties.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (updateProperty == null) return NotFound();

            return Ok(UpdatePropertyInfoDTO.FromUpdateProperty(updateProperty));
        }

        [HttpGet]
        [Route("list")]
        public IActionResult GetAll()
        {
            var updateProperty = context.UpdateProperties.ToList();
            if (updateProperty == null) return NotFound();

            List<UpdatePropertyInfoDTO> listDTO = new List<UpdatePropertyInfoDTO>();
            updateProperty.ForEach(x =>
            {
                if (!x.IsDeleted) listDTO.Add(UpdatePropertyInfoDTO.FromUpdateProperty(x));
            });

            return Ok(listDTO);
        }


    }
}
