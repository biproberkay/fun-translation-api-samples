namespace GateRiddleSolver.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TranslationRecord> TranslationRecords { get; set; }
    }
    public class LanguageCreateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}