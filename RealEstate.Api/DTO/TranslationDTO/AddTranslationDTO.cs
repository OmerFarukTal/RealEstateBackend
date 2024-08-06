using RealEstate.Api.Entities;

namespace RealEstate.Api.DTO.TranslationDTO
{
    public class AddTranslationDTO
    {

        public string Key { get; set; }
        public string TR { get; set; }
        public string EN { get; set; }

        public Translations ToTranslation()
        {
            return new Translations
            {
                Key = this.Key,
                TR = this.TR,
                EN = this.EN,
            };
        }

    }
}
