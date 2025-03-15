namespace NCD.DTO
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string FatherHusbandName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string TelephoneNo { get; set; }
        public string Address { get; set; }
        public string KeyPopulationType { get; set; }
        public string Occupation { get; set; }

        public bool IsDeleted { get; set; } = false;
        public int CreatedBy { get; set; }
    }
}
