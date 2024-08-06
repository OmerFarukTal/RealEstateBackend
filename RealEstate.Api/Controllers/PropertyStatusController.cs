using Microsoft.AspNetCore.Mvc;
using RealEstate.Api.Context;
using RealEstate.Api.DTO.PropertyStatusDTO;

namespace RealEstate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyStatusController : ControllerBase
    {
        private readonly RealEstateContext context;

        public PropertyStatusController(RealEstateContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult AddPropertyStatus([FromBody] AddPropertyStatusDTO addPropertyStatusDTO)
        {
            var response = context.PropertyStatuses.Add(addPropertyStatusDTO.ToPropertyStatus());
            context.SaveChanges();

            return Ok(PropertyStatusInfoDTO.FromPropertyStatus(response.Entity));
        }


        [HttpPut]
        public IActionResult ModifyPropertyStatus(EditPropertyStatusDTO editPropertyStatusDTO)
        {
            string name = editPropertyStatusDTO.Name;
            int id = editPropertyStatusDTO.Id;

            var propertyStatus = context.PropertyStatuses.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (propertyStatus == null) return NotFound(new { message = "This property status does not exist" });

            propertyStatus.Name = name;
            context.SaveChanges();

            return Ok(PropertyStatusInfoDTO.FromPropertyStatus(propertyStatus));
        }


        [HttpGet]
        public IActionResult GetPropertyStatus(int id)
        {
            var propertyStatus = context.PropertyStatuses.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (propertyStatus == null) return NotFound(new {message = "This property status does not exist"});

            return Ok(PropertyStatusInfoDTO.FromPropertyStatus(propertyStatus));
        }

        [HttpDelete]
        public IActionResult DeletePropertyStatus(int id)
        {
            var propertyStatus = context.PropertyStatuses.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (propertyStatus == null) return NotFound(new { message = "This property status does not exist" });

            propertyStatus.IsDeleted = true;
            context.SaveChanges();

            return Ok(new {message = "Prperty Status " + propertyStatus.Id + " with name " + propertyStatus.Name  + " is deleted"});
        }

        [HttpGet]
        [Route("list")]
        public IActionResult GetAll()
        {

            var books = context.PropertyStatuses.ToList();
            if (books == null) return NotFound();

            List<PropertyStatusInfoDTO> listDTO = new List<PropertyStatusInfoDTO>();
            books.ForEach(x =>
            {
                if (!x.IsDeleted) listDTO.Add(PropertyStatusInfoDTO.FromPropertyStatus(x));
            });

            return Ok(listDTO);

        }

    }
}
