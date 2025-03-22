using System;
using System.Collections.Generic;

namespace NirmayaMobileApp.DTO
{
    public class BreastCancerScreeningDTO
    {
        public int Id { get; set; }
        public string PersonName { get; set; }
        public string FatherHusbandName { get; set; }
        public string TelephoneNo { get; set; }
        public string Address { get; set; }
        public string Age { get; set; }
        public DateTime DateOfScreening { get; set; }
        public string ExaminationBy { get; set; }
        public DateTime? DateOfLastMenstrualPeriod { get; set; }
        public string AgeAtFirstMenstruation { get; set; }
        public string NumberOfPregnancies { get; set; }
        public string NumberOfBirths { get; set; }
        public string AgeAtFirstChildbirth { get; set; }
        public string BreastfeedingHistory { get; set; }
        public string MenopausalStatus { get; set; }
        public string HistoryOfBreastLumps { get; set; }
        public string PreviousBreastBiopsies { get; set; }
        public string UseOfHormonalContraceptives { get; set; }
        public string HormoneReplacementTherapy { get; set; }
        public bool IsAgeRiskFactor { get; set; }
        public bool IsFamilyHistoryRiskFactor { get; set; }
        public bool IsGeneticMutationsRiskFactor { get; set; }
        public bool IsPreviousRadiationTherapyRiskFactor { get; set; }
        public bool IsObesityRiskFactor { get; set; }
        public bool IsOtherRiskFactor { get; set; }
        public string AlcoholConsumption { get; set; }
        public string Smoking { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
} 