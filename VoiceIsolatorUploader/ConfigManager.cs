using System;
using System.Text;
using System.Collections.Generic;

namespace VoiceIsolatorUploader
{
    public static class ConfigManager
    {
        // Słownik mapujący zahashowane API na oryginalne wartości
        private static readonly Dictionary<string, string> ApiKeyMapping = new Dictionary<string, string>
        {
            // Przykładowe mapowanie (zahashowane_api -> oryginalne_api)
            { "8bb0cf6eb9b17d0f7d22b456f121257dc1254e1f01665370476383ea776df414", "TEST-API-KEY" }
        };

        public static string EncodeApiKey(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey)) return "";
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKey));
        }
        public static string DecodeApiKey(string hashedKey)
        {
            if (string.IsNullOrEmpty(hashedKey)) return "";
            return ApiKeyMapping.TryGetValue(hashedKey, out string originalKey) ? originalKey : "";
        }
    }
}
