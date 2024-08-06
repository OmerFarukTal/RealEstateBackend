using RealEstate.Api.Entities;

namespace RealEstate.Api.DTO.CurrencyDTO
{
    public class AddCurrencyDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public Currencies ToCurrency()
        {
            return new Currencies()
            {
                Code = this.Code,
                Name = this.Name,
            };
        }
    }
}
