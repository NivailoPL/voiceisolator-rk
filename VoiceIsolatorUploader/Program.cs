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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Sprawdź ważność logowania w config.json
            TempManager.EnsureTempFolder();
            bool loginRequired = true;
            string configPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
            if (System.IO.File.Exists(configPath))
            {
                try
                {
                    var cfg = System.Text.Json.JsonDocument.Parse(System.IO.File.ReadAllText(configPath)).RootElement;
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
                    // Zapisz datę ważności logowania na 7 dni
                    var newConfig = new { api_key = "", login_saved_until = DateTime.Now.AddDays(7).ToString("o") };
                    var json = System.Text.Json.JsonSerializer.Serialize(newConfig);
                    System.IO.File.WriteAllText(configPath, json);
                }
            }

            Application.Run(new MainForm());
            TempManager.ClearTempFolder();
        }
    }
}
