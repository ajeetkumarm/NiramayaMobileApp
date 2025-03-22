namespace Nirmaya.DTO
{
    public class RegistrationDefaultDataDTO
    {
        public IEnumerable<DropdownDTO> YesNoOptions { get; set; } = [];
        public IEnumerable<DropdownDTO> Genders { get; set; } = [];
        public IEnumerable<DropdownDTO> Blocks { get; set; } = [];
        public IEnumerable<DropdownDTO> Districts { get; set; } = [];
        public IEnumerable<DropdownDTO> Villages { get; set; } = [];
        public IEnumerable<DropdownDTO> ScreeningStatus { get; set; } = [];
        public IEnumerable<DropdownDTO> TBScreeining { get; set; } = [];
        public IEnumerable<DropdownDTO> Marital { get; set; } = [];
        public IEnumerable<DropdownDTO> KeyVulPopulation { get; set; } = [];
        public IEnumerable<DropdownDTO> Occupation { get; set; } = [];
    }
}
