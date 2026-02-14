using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MedicalCenter.Models
{
    public class Claim
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("claim_number")] 
        public string ClaimNumber { get; set; }

        [JsonPropertyName("patient_id")] 
        public int PatientId { get; set; }

        [JsonPropertyName("patient_name")]
        public string PatientName { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("service_date")]
        public DateTime ServiceDate { get; set; }

        [JsonPropertyName("idImport")]
        public int? IdImport { get; set; }
    }
}
