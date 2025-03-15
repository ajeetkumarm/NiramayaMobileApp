namespace NCD.DTO
{
    public class Student
    {
        public int Id { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public bool DoYouHaveAadhaarCard { get; set; }
        public string AadhaarCardNumber { get; set; } = string.Empty;
        public int InstitutionId { get; set; }
        public string Section { get; set; } = string.Empty;
        public int GradeId { get; set; } 
        public string Registrationumber { get; set; } = string.Empty;
        public int ChildStatudBeforeNCDSTC { get; set; }
        public int HowLongPlaningToStayThisArea { get; set; }
        public int Class { get; set; }
        public int Reasons { get; set; }
        public string? DropoutClass { get; set; }
        public int? DropoutYear { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int CreatedBy { get; set; }
    }
}
