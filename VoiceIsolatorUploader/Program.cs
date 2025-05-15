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

            // Sprawdź ważność logowania w config.json
            TempManager.EnsureTempFolder(appFolder); // przekazujemy appFolder
            bool loginRequired = true;
            if (File.Exists(configPath))
            {
                try
                {
                    var cfg = System.Text.Json.JsonDocument.Parse(File.ReadAllText(configPath)).RootElement;
                    if (cfg.TryGetProperty("login_saved_until", out var loginUntil))
                    {
                        if (DateTime.TryParse(loginUntil.GetString(), out var dt))
                        {
                            if (dt > DateTime.Now)
                                loginRequired = false;
                        }
                    }
                }
                catch { }
            }

            if (loginRequired)
            {
                using (var login = new LoginForm())
                {
                    if (login.ShowDialog() != DialogResult.OK || !login.LoginSuccess)
                        return;
                    
                    // Zachowaj istniejący klucz API, jeśli istnieje
                    string existingApiKey = "";
                    if (File.Exists(configPath))
                    {
                        try
                        {
                            var cfg = System.Text.Json.JsonDocument.Parse(File.ReadAllText(configPath)).RootElement;
                            if (cfg.TryGetProperty("api_key", out var apiKeyElem))
                                existingApiKey = apiKeyElem.GetString();
                        }
                        catch { }
                    }
                    
                    // Zapisz datę ważności logowania na 7 dni zachowując istniejący klucz API
                    var newConfig = new { api_key = existingApiKey, login_saved_until = DateTime.Now.AddDays(7).ToString("o") };
                    var json = System.Text.Json.JsonSerializer.Serialize(newConfig);
                    File.WriteAllText(configPath, json);
                }
            }

            Application.Run(new MainForm(appFolder));
            TempManager.ClearTempFolder(appFolder);
        }
    }
}
