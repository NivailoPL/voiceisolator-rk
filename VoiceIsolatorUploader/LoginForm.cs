using System;
using System.Windows.Forms;
using System.IO;

namespace VoiceIsolatorUploader
{
    public partial class LoginForm : Form
    {
        public bool LoginSuccess { get; private set; } = false;
        private readonly string appFolder;

        public LoginForm(string appFolder)
        {
            InitializeComponent();
            this.appFolder = appFolder;
            try {
                // Ikona ładowana przez MainForm z folderu Properties
            } catch {}

            this.Load += LoginForm_Load;
        }

        private async void LoginForm_Load(object sender, EventArgs e)
        {
            // Sprawdź zmienną środowiskową przy załadowaniu formularza
            string isolatorEnv = Environment.GetEnvironmentVariable("IZOLATOR");
            if (isolatorEnv == "izolator")
            {
                LoginSuccess = true;

                // Synchronizacja klucza API z plikiem sieciowym
                string configPath = Path.Combine(appFolder, "config.json");
                var (success, message) = await ApiKeyManager.SyncApiKeyWithNetworkAsync(configPath);
                
                if (!success)
                {
                    // Logujemy błąd, ale nie blokujemy logowania
                    string logPath = Path.Combine(appFolder, "api_sync.log");
                    try
                    {
                        File.AppendAllText(logPath, $"[{DateTime.Now}] {message}\n");
                    }
                    catch { }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                messageLabel.Text = "Wygląda na to, że nie masz uprawnień do korzystania z Izolatora Głosu RK! Jeśli uważasz, że jest to błąd, skontaktuj się z WW.";
                messageLabel.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
