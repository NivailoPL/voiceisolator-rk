using System;
using System.IO;

namespace VoiceIsolatorUploader
{
    public static class TempManager
    {
        public static void EnsureTempFolder(string appFolder)
        {
            string tempFolder = Path.Combine(appFolder, "temp");
            if (!Directory.Exists(tempFolder))
                Directory.CreateDirectory(tempFolder);
        }

        public static void ClearTempFolder(string appFolder)
        {
            string tempFolder = Path.Combine(appFolder, "temp");
            if (Directory.Exists(tempFolder))
            {
                foreach (var file in Directory.GetFiles(tempFolder))
                {
                    try { File.Delete(file); } catch { }
                }
            }
        }
    }
}
