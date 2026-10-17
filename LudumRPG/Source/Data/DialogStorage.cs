// The Dialog Storage class.

/*
 * To properly store dialog, create a new Dialog.
 * Then, use the Add method on its Lines variable.
 * 
 * Example:
 * shopping.Lines.Add(new DialogLine("Shopkeeper", "Welcome to my little shop!"));
 * 
 * Additionally, you can use a Texture2D image after the name and line for a portrait.
 * Because of the small dialog box size though, you might wanna use \n for multiple lines.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudumRPG.Source.Data.Storage;

namespace LudumRPG.Source.Data
{
    internal class DialogStorage
    {
        public static Dialog
            intro;

        public static void LoadDialog()
        {
            intro = new Dialog("Intro");
            intro.Lines.Add(new DialogLine("", "Hello there!"));
            intro.Lines.Add(new DialogLine("", "Welcome to Ludum RPG!"));
        }
    }
}
