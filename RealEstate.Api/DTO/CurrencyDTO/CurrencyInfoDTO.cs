using RealEstate.Api.Entities;

namespace RealEstate.Api.DTO.CurrencyDTO
{
    public class CurrencyInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public static CurrencyInfoDTO FromCurrency(Currencies currencies) 
        {
            return new CurrencyInfoDTO()
            {
                Id = currencies.Id,
                Name = currencies.Name,
                Code = currencies.Code,
            };
        
        }
    }
}
