﻿using Azure;
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
                                             .Include(a => a.Images)
                                             .FirstOrDefault(x => x.Id == response.Entity.Id  && !x.IsDeleted);

            return Ok(PropertyInfoDTO.FromProperty(property));
        }

        [HttpGet]
        public IActionResult GetProperty(int id)
        {   
            var property = context.Properties.Include(a => a.PropertyType)
                                             .Include(a => a.PropertyStatus)
                                             .Include(a => a.Creator)
                                             .Include(a => a.Currency)
                                             .Include(a => a.Images)
                                             .FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (property == null) return NotFound();


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
                                       .Include(a => a.Images)
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
                                             .Include(a => a.Images)
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
