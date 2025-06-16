using System;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;
using System.Reflection;

namespace VoiceIsolatorUploader
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // --- Tworzenie folderu temp i pliku config.json w folderze aplikacji ---
            string exePath = Assembly.GetExecutingAssembly().Location;
            string appFolder = Path.GetDirectoryName(exePath);
            string tempFolder = Path.Combine(appFolder, "temp");
            string configPath = Path.Combine(appFolder, "config.json");

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

            using (var login = new LoginForm(appFolder))
            {
                if (login.ShowDialog() != DialogResult.OK || !login.LoginSuccess)
                    return;
            }

            Application.Run(new MainForm(appFolder));
            TempManager.ClearTempFolder(appFolder);
        }
    }
}
