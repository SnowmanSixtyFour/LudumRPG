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
            test.Lines.Add(new DialogLine("Test Guy", "This is a test\nsecond line!\nblah blah blah"));
        }
    }
}
