using System;
using System.Windows.Forms;

namespace VoiceIsolatorUploader
{
    public partial class LoginForm : Form
    {
        public string Username => usernameTextBox.Text;
        public string Password => passwordTextBox.Text;
        public bool LoginSuccess { get; private set; } = false;

        public LoginForm()
        {
            InitializeComponent();
    try {
        this.Icon = new System.Drawing.Icon("Izolator Głosu RK.ico");
    } catch {}
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (usernameTextBox.Text == "newsroom" && passwordTextBox.Text == "newsroom123")
            {
                LoginSuccess = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                errorLabel.Text = "Błędny login lub hasło!";
                errorLabel.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
