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

        [HttpGet]
        [Route("pageSelective")]
        public IActionResult GetUserPropertyPageSelective(int userId, int page, int pageSize, int propertyTypeId, int propertyStatusId, int currencyId, double lowerBoundMoney, double upperBoundMoney, DateTime startDate, DateTime endDate)
        {
            var properties = context.Properties.Include(a => a.PropertyType)
                                       .Include(a => a.PropertyStatus)
                                       .Include(a => a.Creator)
                                       .Include(a => a.Currency)
                                       .Where(x => !x.IsDeleted && x.CreatorId == userId)
                                       .AsQueryable();

            if (propertyTypeId != null && propertyTypeId > 0) properties = properties.Where(p => p.PropertyTypeId == propertyTypeId);

            if (propertyStatusId != null && propertyStatusId > 0) properties = properties.Where(p => p.PropertyStatusId == propertyStatusId);

            if (currencyId != null && currencyId > 0) properties = properties.Where(p => p.CurrencyId == currencyId);

            if (lowerBoundMoney != null && lowerBoundMoney > 0) properties = properties.Where(p => p.Price >= lowerBoundMoney);

            if (upperBoundMoney != null && upperBoundMoney > 0) properties = properties.Where(p => p.Price <= upperBoundMoney);

            if (startDate != DateTime.MinValue) properties = properties.Where(p => p.StartDate >= startDate);

            if (endDate != DateTime.MinValue) properties = properties.Where(p => p.EndDate <= endDate);


            if (!properties.Any()) return Ok(new { list = properties.ToList(), totalLength = 2 });

            var propertiesList = properties.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var propertyDtos = propertiesList.Select(PropertyInfoDTO.FromProperty).ToList();
            return Ok(new { list = propertyDtos, totalLength = propertyDtos.Count });
        }
    }
}
