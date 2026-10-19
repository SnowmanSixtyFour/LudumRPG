using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentalTechBookingLauncher.Source.GUI
{
    internal class MainPage
    {
        // Form Properties
        private Form form;

        private String
            optionsFile = $@"C:\Users\{Environment.UserName}\Documents\My Games\Rental-Tech Booking\LudumDare60\options.json",
            gameFile = $@"{AppDomain.CurrentDomain.BaseDirectory}\RentalTechBooking.exe";

        private int
            buttonWidth = 120, buttonHeight = 25,
            buttonX = 110;

        // GUI
        private Button
            startButton,
            fullscreenButton,
            crtFilterButton;
        private ComboBox
            aspectRatioList;
        private Label
            aspectRatioLabel;

        private int
            defaultWidth1 = 768,
            defaultWidth2 = 840,
            defaultWidth3 = 1080,
            defaultWidth4 = 1280,

            defaultHeight1 = 672,
            defaultHeight2 = 735,
            defaultHeight3 = 945,
            defaultHeight4 = 1120;

        // Options
        private int
            gameWidth = 768,
            gameHeight = 672;
        private bool
            fullscreen = false,
            crtFilter = false;

        public MainPage(Form form)
        {
            // Set Form
            this.form = form;

            // Set Buttons
            startButton = new Button
            {
                Left = buttonX,
                Top = 30,
                Width = buttonWidth,
                Height = buttonHeight,
                Text = "Start Game"
            };
            startButton.Click += StartGame;

            fullscreenButton = new Button
            {
                Left = buttonX,
                Top = 130,
                Width = buttonWidth,
                Height = buttonHeight,
                Text = "Fullscreen"
            };
            fullscreenButton.Click += FullscreenChanged;

            crtFilterButton = new Button
            {
                Left = buttonX,
                Top = 160,
                Width = buttonWidth,
                Height = buttonHeight,
                Text = "CRT Filter"
            };
            crtFilterButton.Click += CRTFilterChanged;

            aspectRatioLabel = new Label
            {
                Left = buttonX,
                Top = 202,
                Height = 15,
                Text = "Aspect Ratio"
            };

            aspectRatioList = new ComboBox
            {
                Left = buttonX,
                Top = 220,
                Width = buttonWidth,
                Height = buttonHeight
            };
            aspectRatioList.DropDownStyle = ComboBoxStyle.DropDownList; // Prevent Editing
            
            aspectRatioList.Items.Add($"{defaultWidth1}x{defaultHeight1}");
            aspectRatioList.Items.Add($"{defaultWidth2}x{defaultHeight2}");
            aspectRatioList.Items.Add($"{defaultWidth3}x{defaultHeight3}");
            aspectRatioList.Items.Add($"{defaultWidth4}x{defaultHeight4}");

            // Load Options into Launcher
            Load();
            UpdateButtonText();

            // Add Objects to Controls
            form.Controls.Add(startButton);
            form.Controls.Add(fullscreenButton);
            form.Controls.Add(crtFilterButton);

            form.Controls.Add(aspectRatioLabel);
            form.Controls.Add(aspectRatioList);
        }

        private void UpdateButtonText()
        {
            // Update Text of Option Buttons

            fullscreenButton.Text = ("Fullscreen: " + fullscreen);
            crtFilterButton.Text = ("CRT Filter: " + crtFilter);
        }

        private void StartGame(object sender, EventArgs e)
        {
            // Save Launcher Options
            Save();

            // Start Game
            if (File.Exists(gameFile))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = gameFile,
                    WorkingDirectory = Path.GetDirectoryName(gameFile),
                    UseShellExecute = true
                });

                // Close
                Application.Exit();
            }
            else
            {
                MessageBox.Show("\"RentalTechBooking.exe\" could not be found!", "Error!");
            }
        }

        private void FullscreenChanged(object sender, EventArgs e)
        {
            fullscreen = !fullscreen;

            Save();
        }

        private void CRTFilterChanged(object sender, EventArgs e)
        {
            crtFilter = !crtFilter;

            Save();
        }

        // Button Clicks

        // File Saving and Loading

        public void Save()
        {
            // Update Text of Buttons
            UpdateButtonText();

            // Update Aspect Ratio
            if (aspectRatioList.SelectedItem == aspectRatioList.Items[0])
            {
                this.gameWidth = defaultWidth1;
                this.gameHeight = defaultHeight1;
            }
            if (aspectRatioList.SelectedItem == aspectRatioList.Items[1])
            {
                this.gameWidth = defaultWidth2;
                this.gameHeight = defaultHeight2;
            }
            if (aspectRatioList.SelectedItem == aspectRatioList.Items[2])
            {
                this.gameWidth = defaultWidth3;
                this.gameHeight = defaultHeight3;
            }
            if (aspectRatioList.SelectedItem == aspectRatioList.Items[3])
            {
                this.gameWidth = defaultWidth4;
                this.gameHeight = defaultHeight4;
            }

            try
            {
                // Save Data in Launcher
                var data = new
                {
                    Width = this.gameWidth,
                    Height = this.gameHeight,
                    Fullscreen = this.fullscreen,
                    CRTFilter = this.crtFilter
                };

                // Convert to JSON
                String json = JsonSerializer.Serialize(
                    data,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

                // Save to File
                File.WriteAllText(optionsFile, json);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error!\n" + e);
            }
        }

        public void Load()
        {
            // Load File
            if (File.Exists(optionsFile))
            {
                // Get JSON
                string json = File.ReadAllText(optionsFile);

                // Load Variables into Launcher
                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    JsonElement root = doc.RootElement;

                    gameWidth = root.GetProperty("Width").GetInt32();
                    gameHeight = root.GetProperty("Height").GetInt32();
                    fullscreen = root.GetProperty("Fullscreen").GetBoolean();
                    crtFilter = root.GetProperty("CRTFilter").GetBoolean();
                }

                // Set Aspect Ratio List Item
                if (gameWidth == defaultWidth1 && gameHeight == defaultHeight1) aspectRatioList.SelectedItem = aspectRatioList.Items[0];
                if (gameWidth == defaultWidth2 && gameHeight == defaultHeight2) aspectRatioList.SelectedItem = aspectRatioList.Items[1];
                if (gameWidth == defaultWidth3 && gameHeight == defaultHeight3) aspectRatioList.SelectedItem = aspectRatioList.Items[2];
                if (gameWidth == defaultWidth4 && gameHeight == defaultHeight4) aspectRatioList.SelectedItem = aspectRatioList.Items[3];
            }

            // Create new File
            else
            {
                Save();
            }
        }
    }
}
