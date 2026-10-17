// The Dialog Storage class.

/*
 * To properly store dialog, create a new Dialog.
 * Then, use the Add method on its Lines variable.
 * 
 * Example:
 * shopping.Lines.Add(new DialogLine("Shopkeeper", "Welcome to my\nlittle shop!"));
 * 
 * Additionally, you can use a Texture2D image after the name and line for a portrait.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudumRPG.Source.Data.Story;

namespace LudumRPG.Source.Data
{
    internal class DialogStorage
    {
        public static Dialog test;

        public DialogStorage()
        {
        }

        public static void LoadDialog()
        {
            test = new Dialog("Test");
            test.Lines.Add(new DialogLine("Test Guy", "Hello there!"));
            test.Lines.Add(new DialogLine("Test Guy", "This is a test\nsecond line!\nblah blah blah", Global.logo));
        }
    }
}
