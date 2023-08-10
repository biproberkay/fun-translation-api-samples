using System.ComponentModel.DataAnnotations;

namespace GateRiddleSolver.Models
{
    public class TranslationRecord
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Text to translate")]
        [MaxLength(length:100, ErrorMessage ="too long text")]
        public string TextToTranslate { get; set; }
        public string? TranslationResult { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
