using MedicalCenter.Models;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MedicalCenter.Services.Auth
{
    internal class AuthApiService : ApiService
    {
        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                var loginData = new { email = loginRequest.Email, password = loginRequest.Password };
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

        public async Task<bool> RegisterAsync(RegisterRequest registerRequest)
        {
            try
            {
                var registerData = new
                {
                    email = registerRequest.Email,
                    password_digest = registerRequest.Password,
                    role = registerRequest.Role
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
