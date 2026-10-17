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
        private Form form;

        private String
            optionsLocation = $@"C:\Users\{Environment.UserName}\Documents\My Games\LudumRPG\LudumDare60\options.json",
            gameLocation = $@"{AppDomain.CurrentDomain.BaseDirectory}\LudumRPG.exe";

        private int
            buttonWidth = 120, buttonHeight = 25;

        public MainPage(Form form)
        {
            this.form = form;

            // Set Buttons
            AddButton("Start Game", new Point(10, 10), StartGame);
        }

        // Button Clicks

        private void StartGame()
        {
            if (File.Exists(gameLocation))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = gameLocation,
                    WorkingDirectory = Path.GetDirectoryName(gameLocation),
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
    }
}
