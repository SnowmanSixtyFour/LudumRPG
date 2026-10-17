using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using LudumRPG.Source;
using LudumRPG.Source.Data;

namespace LudumRPG.Source.Data.Files
{
    public class OptionsFile : DataFile
    {
        // Options
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Fullscreen { get; set; }

        public OptionsFile()
        {
            fileName = "options";
        }

        public override void Save()
        {
            // Set Default Variables
            if (this.Width <= 0) this.Width = Global.displayWidth;
            if (this.Height <= 0) this.Height = Global.displayHeight;

            // Save File
            base.Save();
        }

        public void Load()
        {
            // Read File
            try
            {
                String json = File.ReadAllText($@"C:\Users\{computerName}\Documents\My Games\{gameName}\{fileName}.json");
                OptionsFile file = JsonSerializer.Deserialize<OptionsFile>(json);

                // Load Options
                this.Width = file.Width;
                this.Height = file.Height;
                this.Fullscreen = file.Fullscreen;

                // If options file has invalid values
                if (file.Width <= 0 || file.Height <= 0)
                {
                    Save();
                }
            }

            // If file has errors or does not exist
            catch
            {
                Save();
            }
        }
    }
}
