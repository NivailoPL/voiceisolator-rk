using System;
using System.Text;

namespace VoiceIsolatorUploader
{
    public static class ConfigManager
    {
        public static string EncodeApiKey(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey)) return "";
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKey));
        }
        
        public static string DecodeApiKey(string encodedKey)
        {
            if (string.IsNullOrEmpty(encodedKey)) return "";
            try
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(encodedKey));
            }
            catch
            {
                return "";
            }
        }
    }
}
