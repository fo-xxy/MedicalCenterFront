using MedicalCenter.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MedicalCenter.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://192.168.1.13:5000/api/"; 

        public AuthService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<LoginResponse> LoginAsync(string email, string password)
        {
            try
            {
                var loginData = new { email = email, password = password };
                
                var json = JsonSerializer.Serialize(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{BaseUrl}Auth/Login", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<LoginResponse>(responseString,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }

                return new LoginResponse { Success = false, Message = "Credenciales incorrectas" };
            }
            catch (Exception ex)
            {
                return new LoginResponse { Success = false, Message = "Error de conexión: " + ex.Message };
            }
        }

        public async Task<bool> RegisterAsync(string email, string password, string role)
        {
            try
            {
                var registerData = new
                {
                    email = email,
                    password_digest = password,
                    role = role
                };

                var json = JsonSerializer.Serialize(registerData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{BaseUrl}Auth/Register", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en Registro: {ex.Message}");
                return false;
            }
        }
    }
}
