using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.Api.Context;
using RealEstate.Api.DTO.ImageDTO;
using RealEstate.Api.Entities;

namespace RealEstate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly RealEstateContext context;

        public ImageController(RealEstateContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult AddCurrecy([FromBody] AddImageDTO addImageDTO)
        {
            var response = context.Images.Add(addImageDTO.ToImage());
            context.SaveChanges();

            var image = context.Images.Include(a => a.Property).FirstOrDefault(x => x.Id == response.Entity.Id && !x.IsDeleted);

            return Ok(ImageInfoDTO.FromImage(image));
        }

        [HttpPost]
        [Route("upload")]
        public IActionResult AddCurrecy(int propertyId, IFormFile image)
        {
            var directoryPath = Path.Combine("wwwroot", "images");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var filePath = Path.Combine(directoryPath, image.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            var imageUrl = $"/images/{image.FileName}";

            Images imageEntity = new Images()
            {
                Name = "string",
                Source = imageUrl,
                PropertyId = propertyId,
            };

            context.Images.Add(imageEntity);
            context.SaveChanges();

            return Ok(ImageInfoDTO.FromImage(imageEntity));

        }


        [HttpPut]
        public IActionResult ModifyCurrency([FromBody] EditImageDTO editImageDTO)
        {
            int id = editImageDTO.Id;
            string name = editImageDTO.Name;
            string source = editImageDTO.Source;
            int propertyId = editImageDTO.PropertyId;

            var image = context.Images.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (image == null) return NotFound(new { message = "This image does not exist" });

            image.Source = source;
            image.Name = name;
            image.PropertyId = propertyId;
            context.SaveChanges();


            return Ok(ImageInfoDTO.FromImage(image));
        }

        [HttpGet]
        public IActionResult GetTranslation(int id)
        {
            var image = context.Images.Include(a => a.Property).FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (image == null) return NotFound(new { message = "This image does not exist" });

            return Ok(ImageInfoDTO.FromImage(image));
        }

        [HttpDelete]
        public IActionResult DeleteTranslation(int id)
        {
            var image = context.Images.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (image == null) return NotFound(new { message = "This image does not exist" });

            image.IsDeleted = true;
            context.SaveChanges();

            return Ok(new { message = "Image " + image.Id + " with name " + image.Name + " is deleted" });
        }

        [HttpGet]
        [Route("list")]
        public IActionResult GetAll()
        {
            var image = context.Images.Include(a => a.Property).ToList();
            if (image == null) return NotFound();

            List<ImageInfoDTO> listDTO = new List<ImageInfoDTO>();
            image.ForEach(x =>
            {
                if (!x.IsDeleted) listDTO.Add(ImageInfoDTO.FromImage(x));
            });
            return Ok(listDTO);

        }
    }
}
