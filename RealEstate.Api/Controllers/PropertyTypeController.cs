using Microsoft.AspNetCore.Mvc;
using RealEstate.Api.Context;
using RealEstate.Api.DTO.PropertyTypesDTO;

namespace RealEstate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyTypeController : ControllerBase
    {
        private readonly RealEstateContext context;

        public PropertyTypeController(RealEstateContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult AddPropertyType([FromBody] AddPropertyTypeDTO addPropertyStatusDTO)
        {
            var response = context.PropertyTypes.Add(addPropertyStatusDTO.ToPropertyType());
            context.SaveChanges();

            return Ok(PropertyTypeInfoDTO.FromPropertyType(response.Entity));
        }


        [HttpPut]
        public IActionResult ModifyPropertyType([FromBody] EditPropertyTypeDTO editPropertyTypeDTO)
        {
            string name = editPropertyTypeDTO.Name;
            int id = editPropertyTypeDTO.Id;

            var propertyType = context.PropertyTypes.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (propertyType == null) return NotFound(new { message = "This property type does not exist" });

            propertyType.Name = name;
            context.SaveChanges();

            return Ok(PropertyTypeInfoDTO.FromPropertyType(propertyType));
        }


        [HttpGet]
        public IActionResult GetPropertyType(int id)
        {
            var propertyType = context.PropertyTypes.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (propertyType == null) return NotFound(new {message = "This property type does not exist"});

            return Ok(PropertyTypeInfoDTO.FromPropertyType(propertyType));
        }

        [HttpDelete]
        public IActionResult DeletePropertyType(int id)
        {
            var propertyType = context.PropertyTypes.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (propertyType == null) return NotFound(new { message = "This property type does not exist" });

            propertyType.IsDeleted = true;
            context.SaveChanges();

            return Ok(new {message = "Property Type " + propertyType.Id + " with name " + propertyType.Name  + " is deleted"});
        }

        [HttpGet]
        [Route("list")]
        public IActionResult GetAll()
        {

            var propertyType = context.PropertyTypes.ToList();
            if (propertyType == null) return NotFound();

            List<PropertyTypeInfoDTO> listDTO = new List<PropertyTypeInfoDTO>();
            propertyType.ForEach(x =>
            {
                if (!x.IsDeleted) listDTO.Add(PropertyTypeInfoDTO.FromPropertyType(x));
            });

            return Ok(listDTO);

        }

    }
}
