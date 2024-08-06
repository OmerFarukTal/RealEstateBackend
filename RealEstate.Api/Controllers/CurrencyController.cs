using Microsoft.AspNetCore.Mvc;
using RealEstate.Api.Context;
using RealEstate.Api.DTO.CurrencyDTO;
using System.Reflection.Metadata.Ecma335;

namespace RealEstate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly RealEstateContext context;

        public CurrencyController(RealEstateContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult AddCurrecy([FromBody] AddCurrencyDTO addCurrencyDTO)
        {
            var response = context.Currencies.Add(addCurrencyDTO.ToCurrency());
            context.SaveChanges();

            return Ok(CurrencyInfoDTO.FromCurrency(response.Entity));
        }


        [HttpPut]
        public IActionResult ModifyCurrency([FromBody] EditCurrencyDTO editCurrencyDTO)
        {
            int id = editCurrencyDTO.Id;
            string name = editCurrencyDTO.Name;
            string code = editCurrencyDTO.Code;

            var currencies = context.Currencies.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (currencies == null) return NotFound(new { message = "This currency does not exist" });

            currencies.Code = code;
            currencies.Name = name;
            context.SaveChanges();


            return Ok(CurrencyInfoDTO.FromCurrency(currencies));
        }

        [HttpGet]
        public IActionResult GetTranslation(int id)
        {
            var currency = context.Currencies.FirstOrDefault(x =>x.Id == id && !x.IsDeleted);
            if (currency == null) return NotFound(new { message = "This currency does not exist" });

            return Ok(CurrencyInfoDTO.FromCurrency(currency));
        }

        [HttpDelete]
        public IActionResult DeleteTranslation(int id)
        {
            var currency = context.Currencies.FirstOrDefault(x =>x.Id == id && !x.IsDeleted);
            if (currency == null) return NotFound(new { message = "This currency does not exist" });

            currency.IsDeleted = true;
            context.SaveChanges();

            return Ok(new { message = "Currency " + currency.Id + " with name " + currency.Name + " is deleted" });
        }

        [HttpGet]
        [Route("list")]
        public IActionResult GetAll()
        {
            var currency = context.Currencies.ToList();
            if (currency == null) return NotFound();

            List<CurrencyInfoDTO> listDTO = new List<CurrencyInfoDTO>();
            currency.ForEach(x =>
            {
                if (!x.IsDeleted) listDTO.Add(CurrencyInfoDTO.FromCurrency(x));
            });
            return Ok(listDTO);

        }

    }
}
