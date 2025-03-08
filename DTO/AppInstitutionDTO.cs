namespace Kadam.DTO
{
    public class AppInstitutionDTO
    {
        public AppInstitutionDTO()
        {
            GradeSections = [];
        }
        public int Id { get; set; }
        public string InstitutionName { get; set; } = string.Empty;
        public IEnumerable<AppGradeSectionDTO> GradeSections { get; set; }
    }

    public class AppGradeSectionDTO
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }
        public string GradeName { get; set; } = string.Empty;
        public string Sections { get; set; } = string.Empty;
    }
}
