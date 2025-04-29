using System;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;

namespace VoiceIsolatorUploader
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            // Inicjalizacja statusów i logów
            statusLabel.Text = "Krok 1/5";
            UpdateApiStatus();
            dragDropOutput.Enabled = false;

            // Najprostsza opcja: ustaw logBox zawsze 5px pod restartButton
            logBox.Location = new System.Drawing.Point(logBox.Location.X, restartButton.Bottom + 5);

            // PictureBox z logo (załaduj PNG z zasobów)
            try {
                logoPictureBox.Image = Properties.Resources.LogoVI;
            } catch {}

            // Przycisk restart domyślnie nieaktywny
            restartButton.Enabled = false;
        }

        private void UpdateApiStatus()
        {
            string apiKey = ApiClient.GetApiKeyFromConfig();
            if (!string.IsNullOrEmpty(apiKey))
            {
                apiStatusLabel.Text = "API: OK!";
                apiStatusLabel.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                apiStatusLabel.Text = "API: BRAK!";
                apiStatusLabel.ForeColor = System.Drawing.Color.Red;
            }
        }

        private async void dragDropInput_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 1)
            {
                string filePath = files[0];
                logBox.AppendText($"Załadowano plik: {filePath}\n");
                statusLabel.Text = "Krok 1/5: Sprawdzanie pliku";
                dragDropInput.Enabled = false;

                // Krok 1: Sprawdzenie pliku
                if (!File.Exists(filePath) || !(filePath.EndsWith(".wav") || filePath.EndsWith(".mp3")))
                {
                    logBox.SelectionColor = System.Drawing.Color.Red;
                    logBox.AppendText("Błędny format pliku!\n");
                    dragDropInput.Enabled = true;
                    restartButton.Enabled = true;
                    return;
                }

                statusLabel.Text = "Krok 2/5: Nawiązywanie połączenia z API";
                logBox.AppendText("Nawiązywanie połączenia z API...\n");
                string apiKey = ApiClient.GetApiKeyFromConfig();
                if (string.IsNullOrEmpty(apiKey))
                {
                    logBox.SelectionColor = System.Drawing.Color.Red;
                    logBox.AppendText("Brak klucza API!\n");
                    UpdateApiStatus();
                    dragDropInput.Enabled = true;
                    restartButton.Enabled = true;
                    return;
                }

                statusLabel.Text = "Krok 3/5: Wysyłanie pliku";
                logBox.AppendText("Wysyłanie pliku do API...\n");

                var api = new ApiClient(apiKey);
                var tempFolder = TempManager.TempFolder;
                var (success, msg, outputPath) = await api.IsolateVoiceAsync(filePath, tempFolder);

                if (!success)
                {
                    statusLabel.Text = "Błąd!";
                    logBox.SelectionColor = System.Drawing.Color.Red;
                    logBox.AppendText($"{msg}\n");
                    dragDropInput.Enabled = true;
                    restartButton.Enabled = true;
                    return;
                }

                statusLabel.Text = "Krok 4/5: Odbiór pliku";
                logBox.AppendText("Plik przetworzony, odbieranie...\n");

                if (!File.Exists(outputPath))
                {
                    statusLabel.Text = "Błąd!";
                    logBox.SelectionColor = System.Drawing.Color.Red;
                    logBox.AppendText("Nie udało się zapisać pliku wynikowego!\n");
                    dragDropInput.Enabled = true;
                    restartButton.Enabled = true;
                    return;
                }

                statusLabel.Text = "Krok 5/5: Gotowe!";
                logBox.SelectionColor = System.Drawing.Color.Black;
                logBox.AppendText($"Gotowe! Plik zapisano: {outputPath}\n");
                dragDropOutput.Tag = outputPath;
                dragDropOutput.Enabled = true;

                // --- Dodajemy ikonkę i label do dragDropOutput ---
                dragDropOutput.Controls.Clear();
                try
                {
                    var icon = System.Drawing.Icon.ExtractAssociatedIcon(outputPath)?.ToBitmap();
                    var pic = new PictureBox();
                    pic.Image = icon;
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    pic.Width = 48;
                    pic.Height = 48;
                    pic.Cursor = Cursors.Hand;
                    pic.Location = new System.Drawing.Point(20, 30);
                    pic.MouseDown += (s, e2) => {
                        if (e2.Button == MouseButtons.Left)
                        {
                            var data = new DataObject(DataFormats.FileDrop, new string[] { outputPath });
                            pic.DoDragDrop(data, DragDropEffects.Copy);
                        }
                    };
                    var lbl = new Label();
                    lbl.Text = Path.GetFileName(outputPath);
                    lbl.Location = new System.Drawing.Point(80, 40);
                    lbl.AutoSize = true;
                    dragDropOutput.Controls.Add(pic);
                    dragDropOutput.Controls.Add(lbl);
                }
                catch { /* Jeśli nie uda się pobrać ikony, po prostu nie pokazuj */ }

                restartButton.Enabled = true;
            }
        }

        private void dragDropOutput_DragEnter(object sender, DragEventArgs e)
        {
            if (dragDropOutput.Enabled && dragDropOutput.Tag is string outputPath && File.Exists(outputPath))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void dragDropOutput_DragDrop(object sender, DragEventArgs e)
        {
            if (dragDropOutput.Tag is string outputPath && File.Exists(outputPath))
            {
                // Pobierz ścieżkę docelową z miejsca upuszczenia
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] destPaths = (string[])e.Data.GetData(DataFormats.FileDrop);
                    if (destPaths.Length > 0 && Directory.Exists(destPaths[0]))
                    {
                        string destFile = Path.Combine(destPaths[0], Path.GetFileName(outputPath));
                        File.Copy(outputPath, destFile, true);
                        logBox.AppendText($"Plik skopiowany do: {destFile}\n");
                    }
                }
            }
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            logBox.Clear();
            statusLabel.Text = "Krok 1/5";
            dragDropInput.Enabled = true;
            dragDropOutput.Enabled = false;
            dragDropOutput.Tag = null;
            TempManager.ClearTempFolder();
            restartButton.Enabled = false;
        }

        private void setApiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var apiSettings = new ApiSettingsForm();
            if (apiSettings.ShowDialog() == DialogResult.OK)
            {
                UpdateApiStatus();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dragDropInput_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length == 1 && (files[0].EndsWith(".wav") || files[0].EndsWith(".mp3")))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }
    }
}
