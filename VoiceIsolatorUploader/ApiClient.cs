using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

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

                    // Wysyłamy oryginalny plik, pole 'audio', Content-Type na podstawie rozszerzenia
                    var fileBytes = File.ReadAllBytes(inputFilePath);
                    var fileContent = new ByteArrayContent(fileBytes);
                    string ext = Path.GetExtension(inputFilePath).ToLowerInvariant();
                    if (ext == ".mp3")
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue("audio/mpeg");
                    else if (ext == ".wav")
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue("audio/wav");
                    else
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    form.Add(fileContent, "audio", Path.GetFileName(inputFilePath));

                    var response = await client.PostAsync(endpoint, form);
                    if (!response.IsSuccessStatusCode)
                        return (false, $"Błąd API: {response.StatusCode}", null);

                    // Zapisz plik wynikowy mp3 do temp
                    string baseName = Path.GetFileNameWithoutExtension(inputFilePath);
                    string outMp3 = Path.Combine(tempFolder, $"{baseName}-wyizolowany.mp3");
                    using (var fs = new FileStream(outMp3, FileMode.Create, FileAccess.Write))
                    {
                        await response.Content.CopyToAsync(fs);
                    }

                    // KONWERSJA MP3 -> WAV (Linear PCM, 16bit, 44100Hz, Mono)
                    string outWav = Path.Combine(tempFolder, $"{baseName}-wyizolowany.wav");
                    using (var reader = new Mp3FileReader(outMp3))
                    {
                        var outFormat = new WaveFormat(44100, 1); // 44100Hz, Mono, 16bit
                        using (var resampler = new MediaFoundationResampler(reader, outFormat))
                        {
                            resampler.ResamplerQuality = 60;
                            WaveFileWriter.CreateWaveFile(outWav, resampler);
                        }
                    }

                    return (true, "Plik przetworzony i skonwertowany do WAV.", outWav);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Wyjątek: {ex.Message}", null);
            }
        }

        public static string GetApiKeyFromConfig(string appFolder)
        {
            string configPath = Path.Combine(appFolder, "config.json");
            if (!File.Exists(configPath)) return null;
            try
            {
                var cfg = JsonDocument.Parse(File.ReadAllText(configPath)).RootElement;
                if (cfg.TryGetProperty("api_key", out var apiKeyElem))
                {
                    string hashedKey = apiKeyElem.GetString();
                    return ConfigManager.DecodeApiKey(hashedKey);
                }
            }
            catch { }
            return null;
        }
    }
}
