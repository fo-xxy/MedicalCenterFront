using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalCenter.Models
{
    public class ClaimImportRequest
    {
        public string FileName { get; set; }
        public string FileBase64 { get; set; }
        public DateTime ImportDate { get; set; } = DateTime.Now;
    }
}
