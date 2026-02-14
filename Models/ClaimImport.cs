using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MedicalCenter.Models
{
    public class ClaimImport
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("file_name")]
        public string FileName { get; set; }

        [JsonPropertyName("total_records")]
        public int? TotalRecords { get; set; }

        [JsonPropertyName("processed_records")]
        public int ProcessedRecords { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

      

        public string ProcessedCount => $"{ProcessedRecords}/{(TotalRecords ?? 0)}";

    }
}
