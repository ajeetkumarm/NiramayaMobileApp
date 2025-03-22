namespace NCD.DTO
{
    public class Registration
    {
        public string QueryType { get; set; }
        public int Id { get; set; }
        public string EnrollmentId { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public int BlockId { get; set; }
        public int VillageId { get; set; }
        public string ScreeningStatus { get; set; }
        public DateTime Date_of_Screening { get; set; }
        public string TBScreening { get; set; }
        public DateTime Date_of_TB_Screening { get; set; }
        public string TBEnrollmentid { get; set; }
        public DateTime ScreeningDate { get; set; }
        public string NameofthePerson { get; set; }
        public string FatherName { get; set; }
        public string TelephoneNo { get; set; }
        public string AlternateNo1 { get; set; }
        public string Address { get; set; }
        public string KeyVulPopulation { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Occupation { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
