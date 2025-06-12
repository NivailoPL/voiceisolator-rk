using System;
using System.Windows.Forms;

namespace VoiceIsolatorUploader
{
    public partial class LoginForm : Form
    {
        public bool LoginSuccess { get; private set; } = false;

        public LoginForm()
        {
            InitializeComponent();
            try {
                // Ikona ładowana przez MainForm z folderu Properties
            } catch {}

            this.Load += LoginForm_Load;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Sprawdź zmienną środowiskową przy załadowaniu formularza
            string isolatorEnv = Environment.GetEnvironmentVariable("IZOLATOR");
            if (isolatorEnv == "izolator")
            {
                LoginSuccess = true;
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
