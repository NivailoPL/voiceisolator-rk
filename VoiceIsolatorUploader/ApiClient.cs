using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;

namespace VoiceIsolatorUploader
{
    public class ApiClient
    {
        private readonly string apiKey;
        private static readonly string endpoint = "https://api.elevenlabs.io/v1/audio-isolation";

        public ApiClient(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public async Task<(bool Success, string Message, string OutputPath)> IsolateVoiceAsync(string inputFilePath, string tempFolder)
        {
            if (!File.Exists(inputFilePath))
                return (false, "Plik wejściowy nie istnieje.", null);

            try
            {
                using (var client = new HttpClient())
                using (var form = new MultipartFormDataContent())
                {
                    client.DefaultRequestHeaders.Add("xi-api-key", apiKey);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var fileContent = new ByteArrayContent(File.ReadAllBytes(inputFilePath));
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue("audio/wav"); // lub "audio/mpeg" dla mp3
                    form.Add(fileContent, "audio", Path.GetFileName(inputFilePath));

                    var response = await client.PostAsync(endpoint, form);
                    if (!response.IsSuccessStatusCode)
                    {
                        string err = await response.Content.ReadAsStringAsync();
                        return (false, $"Błąd API: {response.StatusCode} {err}", null);
                    }

                    // Zapisz plik wynikowy do temp
                    var contentDisposition = response.Content.Headers.ContentDisposition;
                    // Nowa nazwa pliku: oryginalna_nazwa-wyizolowany.mp3
                    string baseName = Path.GetFileNameWithoutExtension(inputFilePath);
                    string outFile = Path.Combine(tempFolder, $"{baseName}-wyizolowany.mp3");
                    using (var fs = new FileStream(outFile, FileMode.Create, FileAccess.Write))
                    {
                        await response.Content.CopyToAsync(fs);
                    }
                    return (true, "Plik przetworzony pomyślnie.", outFile);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Wyjątek: {ex.Message}", null);
            }
        }

        public static string GetApiKeyFromConfig()
        {
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
            if (!File.Exists(configPath)) return null;
            try
            {
                var cfg = JsonDocument.Parse(File.ReadAllText(configPath)).RootElement;
                if (cfg.TryGetProperty("api_key", out var apiKeyElem))
                    return ConfigManager.DecodeApiKey(apiKeyElem.GetString());
            }
            catch { }
            return null;
        }
    }
}
