using Microsoft.AspNetCore.Mvc;
using RealEstate.Api.Context;
using RealEstate.Api.DTO.TranslationDTO;
using System.Reflection.Metadata.Ecma335;

namespace RealEstate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslationController : ControllerBase
    {
        private readonly RealEstateContext context;

        public TranslationController(RealEstateContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult AddTranslation([FromBody] AddTranslationDTO addTranslationDTO)
        {
            var response = context.Translations.Add(addTranslationDTO.ToTranslation());
            context.SaveChanges();

            return Ok(TranslationInfoDTO.FromTranslation(response.Entity));
        }


        [HttpPut]
        public IActionResult ModifyTranslation([FromBody] EditTranslationDTO editTranslationDTO)
        {
            int id = editTranslationDTO.Id;
            string Key = editTranslationDTO.Key;
            string TR = editTranslationDTO.TR;
            string EN = editTranslationDTO.EN;

            var translation = context.Translations.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (translation == null) return NotFound(new { message = "This translation does not exist" });

            translation.Key = Key;
            translation.TR = TR;
            translation.EN = EN;
            context.SaveChanges();


            return Ok(TranslationInfoDTO.FromTranslation(translation));
        }

        [HttpGet]
        public IActionResult GetTranslation(int id)
        {
            var translation = context.Translations.FirstOrDefault(x =>x.Id == id && !x.IsDeleted);
            if (translation == null) return NotFound(new { message = "This translation does not exist" });

            return Ok(TranslationInfoDTO.FromTranslation(translation));
        }

        [HttpDelete]
        public IActionResult DeleteTranslation(int id)
        {
            var translation = context.Translations.FirstOrDefault(x =>x.Id == id && !x.IsDeleted);
            if (translation == null) return NotFound(new { message = "This translation does not exist" });

            translation.IsDeleted = true;
            context.SaveChanges();

            return Ok(new { message = "Translation " + translation.Id + " with key " + translation.Key + " is deleted" });
        }

        [HttpGet]
        [Route("list")]
        public IActionResult GetAll()
        {
            var translation = context.Translations.ToList();
            if (translation == null) return NotFound();

            List<TranslationInfoDTO> listDTO = new List<TranslationInfoDTO>();
            translation.ForEach(x =>
            {
                if (!x.IsDeleted) listDTO.Add(TranslationInfoDTO.FromTranslation(x));
            });
            return Ok(listDTO);

        }

    }
}
