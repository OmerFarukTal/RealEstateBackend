using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.Api.Context;
using RealEstate.Api.DTO.ImageDTO;

namespace RealEstate.Api.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyImageController : ControllerBase
    {
        private readonly RealEstateContext context;

        public PropertyImageController(RealEstateContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetImagesOfProperty(int id)
        {

            var property = context.Properties.Include(a => a.Images).FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (property == null) return NotFound();

            List<ImageInfoDTO> listDTO = new List<ImageInfoDTO>();
            foreach (var item in property.Images)
            {
                if (!item.IsDeleted) listDTO.Add(ImageInfoDTO.FromImage(item));
            }
            return Ok(listDTO);
        }

        
    }
}
