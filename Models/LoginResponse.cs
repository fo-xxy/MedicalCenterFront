using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MedicalCenter.Models
{
    public class LoginResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("data")]
        public object Data { get; set; }
        public UserData User { get; set; }
    }

    public class UserData
    {
        public string FullName { get; set; }
        public string Role { get; set; }
    }
}
