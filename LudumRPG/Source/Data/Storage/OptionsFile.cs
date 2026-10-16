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

namespace LudumRPG.Source.Data.Storage
{
    public class OptionsFile : DataFile
    {
        // TODO: Options { get; set; }

        public OptionsFile()
        {
            fileName = "options";
        }

        public override void Save()
        {
            // TODO: Set Default Variables

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

                // TODO: Load Saved Variables
            }

            // If file has errors or does not exist
            catch
            {
                Save();
            }
        }
    }
}
