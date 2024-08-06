using RealEstate.Api.Entities;

namespace RealEstate.Api.DTO.TranslationDTO
{
    public class TranslationInfoDTO
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string TR { get; set; }
        public string EN { get; set; }

        public static TranslationInfoDTO FromTranslation(Translations translations)
        {
            return new TranslationInfoDTO()
            {
                Id = translations.Id,
                Key = translations.Key,
                TR = translations.TR,
                EN = translations.EN,
            };
        }
    }
}
