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

namespace LudumRPGLauncher.Source.GUI
{
    internal class MainPage
    {
        // Form Properties
        private Form form;

        private String
            optionsFile = $@"C:\Users\{Environment.UserName}\Documents\My Games\LudumRPG\LudumDare60\options.json",
            gameFile = $@"{AppDomain.CurrentDomain.BaseDirectory}\LudumRPG.exe";

        private int
            buttonWidth = 120, buttonHeight = 25,
            buttonX = 10;

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
            Load();

            // Set Buttons
            AddButton("Start Game", new Point(buttonX, 10), StartGame);
        }

        // Button Clicks

        private void StartGame()
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
                MessageBox.Show("\"LudumRPG.exe\" could not be found!", "Error!");
            }
        }

        // Objects

        public void AddButton(String text, Point position, Action action)
        {
            Button btn = new Button
            {
                Text = text,
                Left = position.X,
                Top = position.Y,
                Width = buttonWidth,
                Height = buttonHeight
            };

            btn.Click += (s, e) => action();
            btn.TabStop = false;

            form.Controls.Add(btn);
        }

        public void SetButtonText(Button btn, String newText)
        {
        }

        // File

        public void Save()
        {
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
            }

            // Create new File
            else
            {
                Save();
            }
        }
    }
}
