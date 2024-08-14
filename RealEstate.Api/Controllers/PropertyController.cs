using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.Api.Context;
using RealEstate.Api.DTO.PropertyDTO;

namespace RealEstate.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly RealEstateContext context;

        public PropertyController(RealEstateContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult AddProperty(AddPropertyDTO addPropertyDTO)
        {
            var response = context.Properties.Add(addPropertyDTO.ToProperty());
            context.SaveChanges();

            var property = context.Properties.Include(a => a.PropertyType)
                                             .Include(a => a.PropertyStatus)
                                             .Include(a => a.Creator)
                                             .Include(a => a.Currency)
                                             .FirstOrDefault(x => x.Id == response.Entity.Id  && !x.IsDeleted);

            return Ok(PropertyInfoDTO.FromProperty(property));
        }

        [HttpDelete]
        public IActionResult DeleteProperty(int id)
        {
            var property = context.Properties.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            property.IsDeleted = true;
            context.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public IActionResult GetProperty(int id)
        {   
            var property = context.Properties.Include(a => a.PropertyType)
                                             .Include(a => a.PropertyStatus)
                                             .Include(a => a.Creator)
                                             .Include(a => a.Currency)
                                             .FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (property == null) return NotFound();


            return Ok(PropertyInfoDTO.FromProperty(property));
        }

        [HttpGet]
        [Route("raw")]
        public IActionResult GetRawProperty(int id)
        {
            var property = context.Properties.Include(a => a.PropertyType)
                                             .Include(a => a.PropertyStatus)
                                             .Include(a => a.Creator)
                                             .Include(a => a.Currency)
                                             .FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (property == null) return NotFound();


            return Ok(RawPropertyInfoDTO.FromProperty(property));
        }

        [HttpPut]
        public IActionResult PutProperty(EditPropertyDTO editPropertyDTO)
        {
            var property = context.Properties.Include(a => a.PropertyType)
                                             .Include(a => a.PropertyStatus)
                                             .Include(a => a.Creator)
                                             .Include(a => a.Currency)
                                             .FirstOrDefault(x => x.Id == editPropertyDTO.Id && !x.IsDeleted);
            if (property == null) return NotFound();

            property.Name = editPropertyDTO.Name;
            property.Description = editPropertyDTO.Description;
            property.Adress = editPropertyDTO.Adress;
            property.Latitude = editPropertyDTO.Latitude;
            property.Longitude = editPropertyDTO.Longitude;
            property.PropertyStatusId = editPropertyDTO.PropertyStatusId;
            property.PropertyTypeId = editPropertyDTO.PropertyTypeId;
            property.StartDate = editPropertyDTO.StartDate;
            property.EndDate = editPropertyDTO.EndDate;
            property.Price = editPropertyDTO.Price;
            property.CurrencyId = editPropertyDTO.CurrencyId;
            property.CreatorId = editPropertyDTO.CreatorId;
            property.CreatedDate = editPropertyDTO.CreatedDate;
            context.SaveChanges();

            return Ok(PropertyInfoDTO.FromProperty(property));
        }




        [HttpGet]
        [Route("page")]
        public IActionResult GetPropertyPage(int page, int pageSize)
        {
            var properties = context.Properties.Include(a => a.PropertyType)
                                       .Include(a => a.PropertyStatus)
                                       .Include(a => a.Creator)
                                       .Include(a => a.Currency)
                                       .Where(x => !x.IsDeleted)
                                       .Skip((page - 1) * pageSize)
                                       .Take(pageSize)
                                       .ToList();

            if (!properties.Any()) return NotFound();

            var propertyDtos = properties.Select(PropertyInfoDTO.FromProperty).ToList();
            return Ok(propertyDtos);
        }

        [HttpGet]
        [Route("list")]
        public IActionResult GetAll()
        {
            var property = context.Properties.Include(a => a.PropertyType)
                                             .Include(a => a.PropertyStatus)
                                             .Include(a => a.Creator)
                                             .Include(a => a.Currency)
                                             .ToList();
            if (property == null) return NotFound();

            List<PropertyInfoDTO> listDTO = new List<PropertyInfoDTO>();
            property.ForEach(x =>
            {
                if (!x.IsDeleted) listDTO.Add(PropertyInfoDTO.FromProperty(x));
            });
            return Ok(listDTO);
        }


    }
}
