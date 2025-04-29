using System;
using System.IO;

namespace VoiceIsolatorUploader
{
    public static class TempManager
    {
        public static string TempFolder => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp");

        public static void EnsureTempFolder()
        {
            if (!Directory.Exists(TempFolder))
                Directory.CreateDirectory(TempFolder);
        }

        public static void ClearTempFolder()
        {
            if (Directory.Exists(TempFolder))
            {
                foreach (var file in Directory.GetFiles(TempFolder))
                {
                    try { File.Delete(file); } catch { }
                }
            }
        }
    }
}
