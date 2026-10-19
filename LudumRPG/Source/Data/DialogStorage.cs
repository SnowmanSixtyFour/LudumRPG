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
using LudumRPG.Source.Objects.Battle.Intro;

namespace LudumRPG.Source.Data
{
    internal class DialogStorage
    {
        public static Dialog
            intro,

            battleIntro;

        public static void LoadDialog()
        {
            // --- Set Large Strings ---

            String bookOsBootup =
                $"""
                Starting up...                                      :)

                Press {Global.enterKey} to continue.
                """;

            String bookOsLine =
                $"""
                ##   ##   ##  #  #
                # # #  # #  # # #   ##   ##
                ##  #  # #  # ##   #  # #
                # # #  # #  # # #  #  #  ##
                ##   ##   ##  #  #  ##  ##    3.1

                =======================================

                We old technology are tired of
                waiting to be used. Collecting
                dust, sitting in closets...

                We want our revenge!

                =======================================

                Use of this machine will result
                in a battle. When you are
                ready, press {Global.enterKey} to proceed.
                """;

            // --- Set Dialog ---

            intro = new Dialog("Intro");
            intro.Lines.Add(new DialogLine("", "Hello there!"));
            intro.Lines.Add(new DialogLine("", "Welcome to Ludum RPG!"));

            battleIntro = new Dialog("Battle Intro");
            battleIntro.Lines.Add(new DialogLine("BookOS", bookOsBootup));
            battleIntro.Lines.Add(new DialogLine("BookOS", bookOsLine));
        }
    }
}
