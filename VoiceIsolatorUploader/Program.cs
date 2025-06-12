using System;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;

namespace VoiceIsolatorUploader
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // --- Tworzenie folderu temp i pliku config.json w %appdata% ---
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appFolder = Path.Combine(appDataPath, "VoiceIsolatorUploader");
            string tempFolder = Path.Combine(appFolder, "temp");
            string configPath = Path.Combine(appFolder, "config.json");

            if (!Directory.Exists(appFolder))
                Directory.CreateDirectory(appFolder);
            if (!Directory.Exists(tempFolder))
                Directory.CreateDirectory(tempFolder);
            if (!File.Exists(configPath))
                File.WriteAllText(configPath, "{}\n");

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                try
                {
                    string logPath = Path.Combine(appFolder, "fatal_error.log");
                    File.AppendAllText(logPath, $"[{DateTime.Now}] Unhandled Exception: {e.ExceptionObject}\n");
                }
                catch { }
            };
            Application.ThreadException += (sender, e) =>
            {
                try
                {
                    string logPath = Path.Combine(appFolder, "fatal_error.log");
                    File.AppendAllText(logPath, $"[{DateTime.Now}] UI Exception: {e.Exception}\n");
                }
                catch { }
            };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            TempManager.EnsureTempFolder(appFolder);

            using (var login = new LoginForm())
            {
                if (login.ShowDialog() != DialogResult.OK || !login.LoginSuccess)
                    return;
            }

            Application.Run(new MainForm(appFolder));
            TempManager.ClearTempFolder(appFolder);
        }
    }
}
