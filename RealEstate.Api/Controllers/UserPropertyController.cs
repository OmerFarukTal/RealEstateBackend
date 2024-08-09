using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.Api.Context;
using RealEstate.Api.DTO.PropertyDTO;

namespace RealEstate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPropertyController : ControllerBase
    {
        private readonly RealEstateContext context;

        public UserPropertyController(RealEstateContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetUserProperty(int userId)
        {
            var properties = context.Properties.Include(a => a.PropertyType)
                                       .Include(a => a.PropertyStatus)
                                       .Include(a => a.Creator)
                                       .Include(a => a.Currency)
                                       .Where(x => !x.IsDeleted && x.CreatorId == userId)
                                       .ToList();
            if (properties == null) return NotFound();

            List<PropertyInfoDTO> listDTO = new List<PropertyInfoDTO>();
            properties.ForEach(x => listDTO.Add(PropertyInfoDTO.FromProperty(x)));
            return Ok(listDTO);

        }

        [HttpGet]
        [Route("page")]
        public IActionResult GetUserPropertyPage(int userId, int page, int pageSize)
        {
            var properties = context.Properties.Include(a => a.PropertyType)
                                       .Include(a => a.PropertyStatus)
                                       .Include(a => a.Creator)
                                       .Include(a => a.Currency)
                                       .Where(x => !x.IsDeleted && x.CreatorId == userId)
                                       .Skip((page - 1) * pageSize)
                                       .Take(pageSize)
                                       .ToList();
            if (properties == null) return NotFound();

            List<PropertyInfoDTO> listDTO = new List<PropertyInfoDTO>();
            properties.ForEach(x => listDTO.Add(PropertyInfoDTO.FromProperty(x)));
            return Ok(listDTO);

        }


    }
}
