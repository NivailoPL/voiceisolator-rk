namespace VoiceIsolatorUploader
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.apiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setApiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.apiStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.dragDropInput = new System.Windows.Forms.GroupBox();
            this.dragDropOutput = new System.Windows.Forms.GroupBox();
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.restartButton = new System.Windows.Forms.Button();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.apiToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(512, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Text = "Plik";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Text = "Zakończ";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // apiToolStripMenuItem
            // 
            this.apiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setApiToolStripMenuItem});
            this.apiToolStripMenuItem.Name = "apiToolStripMenuItem";
            this.apiToolStripMenuItem.Text = "API";
            // 
            // setApiToolStripMenuItem
            // 
            this.setApiToolStripMenuItem.Name = "setApiToolStripMenuItem";
            this.setApiToolStripMenuItem.Text = "Ustaw API";
            this.setApiToolStripMenuItem.Click += new System.EventHandler(this.setApiToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.apiStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 360);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(512, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(80, 17);
            this.statusLabel.Text = "Krok 1/5";
            // 
            // apiStatusLabel
            // 
            this.apiStatusLabel.Name = "apiStatusLabel";
            this.apiStatusLabel.Size = new System.Drawing.Size(80, 17);
            this.apiStatusLabel.Text = "API: BRAK!";
            this.apiStatusLabel.ForeColor = System.Drawing.Color.Red;
            // 
            // dragDropInput
            // 
            this.dragDropInput.AllowDrop = true;
            this.dragDropInput.Location = new System.Drawing.Point(30, 50);
            this.dragDropInput.Name = "dragDropInput";
            this.dragDropInput.Size = new System.Drawing.Size(230, 110);
            this.dragDropInput.TabIndex = 2;
            this.dragDropInput.TabStop = false;
            this.dragDropInput.Text = "Przeciągnij plik (.wav, .mp3)";
            this.dragDropInput.DragEnter += new System.Windows.Forms.DragEventHandler(this.dragDropInput_DragEnter);
            this.dragDropInput.DragDrop += new System.Windows.Forms.DragEventHandler(this.dragDropInput_DragDrop);
            // 
            // dragDropOutput
            // 
            this.dragDropOutput.AllowDrop = true;
            this.dragDropOutput.Location = new System.Drawing.Point(270, 50);
            this.dragDropOutput.Name = "dragDropOutput";
            this.dragDropOutput.Size = new System.Drawing.Size(230, 110);
            this.dragDropOutput.TabIndex = 3;
            this.dragDropOutput.TabStop = false;
            this.dragDropOutput.Text = "Przeciągnij gotowy plik";
            this.dragDropOutput.Enabled = false;
            this.dragDropOutput.DragEnter += new System.Windows.Forms.DragEventHandler(this.dragDropOutput_DragEnter);
            this.dragDropOutput.DragDrop += new System.Windows.Forms.DragEventHandler(this.dragDropOutput_DragDrop);
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(30, 195);
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.Size = new System.Drawing.Size(470, 160);
            this.logBox.TabIndex = 4;
            this.logBox.Text = "";
            // 
            // restartButton
            // 
            this.restartButton.Location = new System.Drawing.Point(0, 150);
            this.restartButton.Name = "restartButton";
            this.restartButton.Size = new System.Drawing.Size(512, 40);
            this.restartButton.TabIndex = 5;
            this.restartButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.restartButton.Text = "ROZPOCZNIJ PONOWNIE";
            this.restartButton.UseVisualStyleBackColor = true;
            this.restartButton.Click += new System.EventHandler(this.restartButton_Click);
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Location = new System.Drawing.Point(5, 27);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(30, 30);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPictureBox.TabIndex = 6;
            this.logoPictureBox.TabStop = false;
            this.logoPictureBox.Image = global::VoiceIsolatorUploader.Properties.Resources.LogoVI;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 384);
            this.Controls.Add(this.restartButton);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.dragDropOutput);
            this.Controls.Add(this.dragDropInput);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.logoPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Voice Isolator Uploader";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem apiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setApiToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripStatusLabel apiStatusLabel;
        private System.Windows.Forms.GroupBox dragDropInput;
        private System.Windows.Forms.GroupBox dragDropOutput;
        private System.Windows.Forms.RichTextBox logBox;
        private System.Windows.Forms.Button restartButton;
        private System.Windows.Forms.PictureBox logoPictureBox;
    }
}
