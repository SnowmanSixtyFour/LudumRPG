using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LudumRPG.Source.Data
{
    public class DataFile
    {
        // Properties
        public readonly String
            computerName = Environment.UserName,
            gameName = @"LudumRPG\LudumDare60"; // Keep game files in Ludum Dare 60 folder

        // File
        [JsonIgnore] public String fileName { get; set; }
        [JsonIgnore] public String subdirectory = "";

        public virtual void Save()
        {
            // Make sure file has a name
            if (this.fileName.Trim() != "")
            {
                // Organize Json
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                // Serialize Json
                String json = JsonSerializer.Serialize(this, GetType(), options);

                // If File already Exists
                if (Exists())
                {
                    if (SubdirectoryExists())
                    {
                        try
                        {
                            File.Delete($@"C:\Users\{computerName}\Documents\My Games\{gameName}\{subdirectory}\{fileName}.json");
                        }
                        catch
                        {
                            Directory.CreateDirectory($@"C:\Users\{computerName}\Documents\My Games\{gameName}\{subdirectory}\");
                            Save();
                        }
                    }
                    else
                    {
                        try
                        {
                            File.Delete($@"C:\Users\{computerName}\Documents\My Games\{gameName}\{fileName}.json");
                        }
                        catch
                        {
                            Directory.CreateDirectory($@"C:\Users\{computerName}\Documents\My Games\{gameName}\");
                            Save();
                        }
                    }
                }

                // Write File
                if (SubdirectoryExists())
                {
                    try
                    {
                        File.WriteAllText($@"C:\Users\{computerName}\Documents\My Games\{gameName}\{subdirectory}\{fileName}.json", json);
                    }
                    catch
                    {
                        Directory.CreateDirectory($@"C:\Users\{computerName}\Documents\My Games\{gameName}\{subdirectory}\");

                        Save();
                    }
                }
                else
                {
                    try
                    {
                        File.WriteAllText($@"C:\Users\{computerName}\Documents\My Games\{gameName}\{fileName}.json", json);
                    }
                    catch
                    {
                        Directory.CreateDirectory($@"C:\Users\{computerName}\Documents\My Games\{gameName}\");

                        Save();
                    }
                }
            }
        }

        public bool SubdirectoryExists()
        {
            return this.subdirectory.Trim() != "";
        }

        public bool Exists()
        {
            return File.Exists($@"C:\Users\{computerName}\Documents\My Games\{gameName}\{fileName}.json");
        }
    }
}
