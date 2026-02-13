using MedicalCenter.Models;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization; 

namespace MedicalCenter.Services.Claims
{
    public class ClaimsService : ApiService
    {
        public async Task<List<Claim>> GetClaimsAsync()
        {
            try
            {
                await AddAuthorizationHeader();

                var response = await _httpClient.GetAsync($"{BaseUrl}Claims/withNamePatient");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var result = JsonSerializer.Deserialize<ApiResponse<List<Claim>>>(content,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return result?.Data ?? new List<Claim>();
                }
                return new List<Claim>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                return new List<Claim>();
            }
        }

        public async Task<bool> ImportClaimsAsync(string filePath)
        {
            try
            {
                await AddAuthorizationHeader();

                var fileStream = File.OpenRead(filePath);
                var fileContent = new StreamContent(fileStream);

                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                using var content = new MultipartFormDataContent();
                content.Add(fileContent, "file", Path.GetFileName(filePath));

                var response = await _httpClient.PostAsync($"{BaseUrl}ClaimImports/upload", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al importar: {ex.Message}");
                return false;
            }
        }

        public async Task<string?> ExportClaimsAsync()
        {
            try
            {
                await AddAuthorizationHeader();
                var response = await _httpClient.GetAsync($"{BaseUrl}ClaimExport/export");

                if (response.IsSuccessStatusCode)
                {
                    byte[] rawBytes = await response.Content.ReadAsByteArrayAsync();

                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

             
                    string content = Encoding.GetEncoding("ISO-8859-1").GetString(rawBytes);

                   
                    if (content.StartsWith("sep="))
                    {
                        int firstNewLine = content.IndexOf('\n');
                        if (firstNewLine >= 0) content = content.Substring(firstNewLine + 1);
                    }

                    content = content.Replace(";", ",");

                
                    string fileName = $"Reporte_{DateTime.Now:yyyyMMdd_HHmm}.csv";
                    string localPath = Path.Combine(FileSystem.CacheDirectory, fileName);

                    var utf8WithBom = new UTF8Encoding(true);
                    await File.WriteAllTextAsync(localPath, content, utf8WithBom);

                    return localPath;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
            return null;
        }
    }
}
