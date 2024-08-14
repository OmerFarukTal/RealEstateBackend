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
        public IActionResult UploadImageFile([FromBody] AddImageDTO addImageDTO)
        {
            if (string.IsNullOrEmpty(addImageDTO.Source))
                return BadRequest("The image field is required.");

            // Decode the Base64 string
            var base64Parts = addImageDTO.Source.Split(',');
            var imageData = Convert.FromBase64String(base64Parts.Length > 1 ? base64Parts[1] : base64Parts[0]);

            var directoryPath = Path.Combine("wwwroot", "images");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + ".jpg"; // Or use appropriate extension
            var filePath = Path.Combine(directoryPath, uniqueFileName);

            System.IO.File.WriteAllBytes(filePath, imageData);

            var imageUrl = $"/images/{uniqueFileName}";

            Images imageEntity = new Images()
            {
                Name = addImageDTO.Name,
                Source = imageUrl,
                PropertyId = addImageDTO.PropertyId,
            };

            var response = context.Images.Add(imageEntity);
            context.SaveChanges();

            var returnedImage = context.Images.Include(a => a.Property).FirstOrDefault(x => x.Id == response.Entity.Id);

            return Ok(ImageInfoDTO.FromImage(returnedImage));
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
