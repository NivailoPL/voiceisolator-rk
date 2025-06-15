namespace VoiceIsolatorUploader
{
    partial class ApiSettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.apiKeyTextBox = new System.Windows.Forms.TextBox();
            this.setApiButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.errorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // apiKeyTextBox
            // 
            this.apiKeyTextBox.Location = new System.Drawing.Point(30, 30);
            this.apiKeyTextBox.Name = "apiKeyTextBox";
            // this.apiKeyTextBox.PlaceholderText = "Klucz API";
            this.apiKeyTextBox.Size = new System.Drawing.Size(250, 23);
            this.apiKeyTextBox.TabIndex = 0;
            // 
            // setApiButton
            // 
            this.setApiButton.Location = new System.Drawing.Point(30, 70);
            this.setApiButton.Name = "setApiButton";
            this.setApiButton.Size = new System.Drawing.Size(120, 30);
            this.setApiButton.TabIndex = 2;
            this.setApiButton.Text = "Ustaw API";
            this.setApiButton.UseVisualStyleBackColor = true;
            this.setApiButton.Click += new System.EventHandler(this.setApiButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(160, 70);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(120, 30);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Anuluj";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Location = new System.Drawing.Point(30, 110);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(0, 15);
            this.errorLabel.TabIndex = 4;
            // 
            // ApiSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 150);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.setApiButton);
            this.Controls.Add(this.apiKeyTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ApiSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ustawienia API";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private System.Windows.Forms.TextBox apiKeyTextBox;
        private System.Windows.Forms.Button setApiButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label errorLabel;
    }
}
