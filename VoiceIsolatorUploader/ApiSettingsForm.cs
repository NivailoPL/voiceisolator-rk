using System;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;

namespace VoiceIsolatorUploader
{
    public partial class ApiSettingsForm : Form
    {
        private readonly string appFolder;

        public ApiSettingsForm(string appFolder)
        {
            InitializeComponent();
            this.appFolder = appFolder;
            // Wczytaj klucz API z config.json w AppData
            string configPath = System.IO.Path.Combine(appFolder, "config.json");
            if (System.IO.File.Exists(configPath))
            {
                try
                {
                    var cfg = System.Text.Json.JsonDocument.Parse(System.IO.File.ReadAllText(configPath)).RootElement;
                    if (cfg.TryGetProperty("api_key", out var apiKey))
                    {
                        apiKeyTextBox.Text = apiKey.GetString();
                    }
                }
                catch { }
            }
        }

        private void setApiButton_Click(object sender, EventArgs e)
        {
            string apiKey = apiKeyTextBox.Text.Trim();
            if (string.IsNullOrEmpty(apiKey))
            {
                errorLabel.Text = "Podaj klucz API!";
                errorLabel.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Zapisz do config.json w AppData
            string configPath = System.IO.Path.Combine(appFolder, "config.json");
            var newConfig = new System.Collections.Generic.Dictionary<string, string>
            {
                ["api_key"] = apiKey
            };
            
            var json = System.Text.Json.JsonSerializer.Serialize(newConfig);
            System.IO.File.WriteAllText(configPath, json);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
