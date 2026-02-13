using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalCenter.Models
{
    internal class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
