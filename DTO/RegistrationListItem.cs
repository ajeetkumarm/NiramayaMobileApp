using System;
using Newtonsoft.Json;

namespace NCD.DTO
{
    public class RegistrationListItem
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string EnrollmentId { get; set; }
        public string ProjectCode { get; set; }
        
        [JsonProperty("projectName")]
        public string Project { get; set; }
        
        [JsonProperty("mName")]
        public string Month { get; set; }
        
        public int StateId { get; set; }
        
        [JsonProperty("stateName")]
        public string State { get; set; }
        
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int BlockId { get; set; }
        public string BlockName { get; set; }
        public int VillageId { get; set; }
        
        [JsonProperty("villageName")]
        public string Village { get; set; }
        
        public string ScreeningStatus { get; set; }
        public DateTime DateOfScreening { get; set; }
        
        [JsonProperty("nameOfThePerson")]
        public string Name { get; set; }
        
        [JsonProperty("fatherHusbandName")]
        public string FatherName { get; set; }
        
        public int Age { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string AlternateMobileNumber { get; set; }
        
        [JsonProperty("pAddress")]
        public string Address { get; set; }
        
        [JsonProperty("keyVulPopulation")]
        public string KeyPopulation { get; set; }
        
        public string TbScreening { get; set; }
        public DateTime DateOfTBScreening { get; set; }
        public string TbEnrollmentId { get; set; }
        public string MaritalStatus { get; set; }
        public int Occupation { get; set; }
    }

    public class RegistrationFilterDTO
    {
        public string CreatedBy { get; set; } = "0";
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string QueryType { get; set; } = "A";
    }
} 