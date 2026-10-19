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
        public static readonly String
            name1 = "Girl 1",
            name2 = "Girl 2",
            computerName = "BookOS",
            unknownName = "???";

        public static Dialog
            // Overworld
            intro, introEnd,

            // Battle Intro
            battleIntro, tutorial;

        public static void LoadDialog()
        {
            // --- Set Strings ---

            // Large Strings

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
            intro.Lines.Add(new DialogLine(unknownName, $"Hey, {name1}?", Assets.shortPortraitNormal));
            intro.Lines.Add(new DialogLine(name1, $"What's up, {name2}?", Assets.tallPortraitNormal));
            intro.Lines.Add(new DialogLine(name2, "I've been hearing a\nweird noise at the\nback of the store\nall day...", Assets.shortPortraitConfused));
            intro.Lines.Add(new DialogLine(name2, "What do you think\nit could be...?", Assets.shortPortraitConfused));
            intro.Lines.Add(new DialogLine(name1, "Huh? Weird noise?", Assets.tallPortraitConfused));
            intro.Lines.Add(new DialogLine(name1, "Let's check it out!", Assets.tallPortraitExcited));

            introEnd = new Dialog("Found Computer");
            introEnd.Lines.Add(new DialogLine(name1, $"{name2}, was this\nthe thing that was\nscaring you...?", Assets.tallPortraitConfused));
            introEnd.Lines.Add(new DialogLine(name2, "I guess so.\nI forgot we even had\nthis bucket of bolts.", Assets.shortPortraitNormal));
            introEnd.Lines.Add(new DialogLine(name1, "Hang on...\nIt's already been\nturned on?", Assets.tallPortraitNormal));

            battleIntro = new Dialog("Battle Intro");
            battleIntro.Lines.Add(new DialogLine(computerName, bookOsBootup));
            battleIntro.Lines.Add(new DialogLine(computerName, bookOsLine));

            tutorial = new Dialog("Battle Tutorial");
            tutorial.Lines.Add(new DialogLine("", "Wait, wait!!!"));
            tutorial.Lines.Add(new DialogLine("", "This won't be an easy battle...\n\nYou'll have to differentiate\neach button for both girls!"));
            tutorial.Lines.Add(new DialogLine("", $"Use {Global.enterKey} to control {name1},\n\nand {Global.backKey} to control {name2}!"));
            tutorial.Lines.Add(new DialogLine("", $"Simple, right? {Global.enterKey}, then {Global.backKey}!\n\nGood luck!"));
        }
    }
}
