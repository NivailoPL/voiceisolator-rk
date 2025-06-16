using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace VoiceIsolatorUploader
{
    public static class ApiKeyManager
    {
        private const string NetworkApiKeyPath = @"R:\Ramowka\Realizatorzy\Wasiak\ProgramyRK\API-izolatorglosu.txt";
        private const int NetworkTimeout = 3000; // 3 seconds timeout

        public static async Task<(bool Success, string Message)> SyncApiKeyWithNetworkAsync(string configPath)
        {
            try
            {
                // Check if network path exists and is accessible
                if (!await Task.Run(() => 
                {
                    try
                    {
                        var task = Task.Run(() => File.Exists(NetworkApiKeyPath));
                        return task.Wait(NetworkTimeout) && task.Result;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }))
                {
                    return (false, "Nie można połączyć się z lokalizacją sieciową lub plik nie istnieje.");
                }

                // Read network API key with timeout
                string networkApiKey = await Task.Run(() => 
                {
                    try
                    {
                        var readTask = Task.Run(() => File.ReadAllText(NetworkApiKeyPath));
                        if (readTask.Wait(NetworkTimeout))
                        {
                            return readTask.Result;
                        }
                        throw new TimeoutException("Przekroczono limit czasu odczytu pliku sieciowego.");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Błąd odczytu pliku sieciowego: {ex.Message}");
                    }
                });

                // Validate network API key
                if (string.IsNullOrWhiteSpace(networkApiKey))
                {
                    return (false, "Plik API w lokalizacji sieciowej jest pusty.");
                }

                networkApiKey = networkApiKey.Trim();

                // Read local config
                string localApiKey = null;
                try
                {
                    if (File.Exists(configPath))
                    {
                        var config = JsonDocument.Parse(File.ReadAllText(configPath)).RootElement;
                        if (config.TryGetProperty("api_key", out var apiKeyElem))
                        {
                            localApiKey = apiKeyElem.GetString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Błąd odczytu lokalnego pliku konfiguracyjnego: {ex.Message}");
                }

                // Compare and update if necessary
                if (string.IsNullOrEmpty(localApiKey) || localApiKey != networkApiKey)
                {
                    try
                    {
                        var newConfig = new { api_key = networkApiKey };
                        string json = JsonSerializer.Serialize(newConfig, new JsonSerializerOptions { WriteIndented = true });
                        File.WriteAllText(configPath, json);
                        return (true, "Klucz API został pomyślnie zaktualizowany z pliku sieciowego.");
                    }
                    catch (Exception ex)
                    {
                        return (false, $"Błąd zapisu do pliku konfiguracyjnego: {ex.Message}");
                    }
                }

                return (true, "Klucz API jest aktualny.");
            }
            catch (Exception ex)
            {
                return (false, $"Wystąpił błąd podczas synchronizacji klucza API: {ex.Message}");
            }
        }
    }
} 