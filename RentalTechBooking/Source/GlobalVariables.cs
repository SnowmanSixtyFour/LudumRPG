using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RentalTechBooking.Source
{
    internal class Global
    {
        // Debug Mode

        public static bool debug = false;

        // Window

        public static readonly String windowName = "Rental-Tech Booking (Ludum Dare 60)";

        public static readonly int
            // Draw Resolution
            resWidth = 256,
            resHeight = 224,

            // Window Size
            displayWidth = 768,
            displayHeight = 672;

        public static int
            windowWidth = displayWidth,
            windowHeight = displayHeight;

        public static bool active = true;

        // Settings

        // Quit Game
        public static bool quit = false;

        // When Inactive
        public static bool
            renderInactive = true,
            pauseWhenInactive = true;

        // Fullscreen
        public static bool
            fullscreen = false,

            fullscreenChanged = false;

        // Game

        public static string gameVersion = "0.1.0";

        public static bool
            paused = false;

        public static float dialogSpeed = 0.02f;

        // Keys
        public static readonly String
            keyboardEnterKey = "[Z]",
            keyboardBackKey = "[X]",

            controllerEnterKey = "[A]",
            controllerBackKey = "[B]";

        public static String
            enterKey = keyboardEnterKey,
            backKey = controllerBackKey;

        // Graphics

        public static bool crtFilter = false;

        // Sprites

        public static Texture2D
            // Util
            noImg,

            // Title
            titleBG,
            logo,

            // Overlay,
            crtVignette;

        public static SpriteFont
            retro,
            arial;

        public static Effect
            crt;

        // Set game options
        public static void LoadOptions()
        {
            // Window Size
            Global.windowWidth = MainGame.options.Width;
            Global.windowHeight = MainGame.options.Height;

            // Fullscreen
            Global.fullscreen = MainGame.options.Fullscreen;

            // Filters
            Global.crtFilter = MainGame.options.CRTFilter;
        }

        // Load game assets
        public static void LoadContent(ContentManager content)
        {
            // Text

            // Update Keys
            if (GamePad.GetState(PlayerIndex.One).IsConnected)
            {
                enterKey = controllerEnterKey;
                backKey = controllerBackKey;
            }
            else
            {
                enterKey = keyboardEnterKey;
                backKey = keyboardBackKey;
            }

            // Images

            Global.noImg = content.Load<Texture2D>("Assets/Images/pixel");

            Global.titleBG = content.Load<Texture2D>("Assets/Images/Title/titlePattern");
            Global.logo = content.Load<Texture2D>("Assets/Images/Title/logo");

            Global.crtVignette = content.Load<Texture2D>("Assets/Images/Overlay/crtVignette");

            Assets.computer = content.Load<Texture2D>("Assets/Images/Overworld/ComputerPlaceholder"); // Change to remove Placeholder

            Assets.dialogHUD = content.Load<Texture2D>("Assets/Images/GUI/HUDs/dialogHUD");
            Assets.mainBattleHUD = content.Load<Texture2D>("Assets/Images/GUI/HUDs/mainBattleHUD");

            Assets.portraitBG = content.Load<Texture2D>("Assets/Images/GUI/portraitBG");

            Assets.tallPortraitNormal = content.Load<Texture2D>("Assets/Images/GUI/Portrait/tallPortraitNormal");
            Assets.tallPortraitConfused = content.Load<Texture2D>("Assets/Images/GUI/Portrait/tallPortraitConfused");
            Assets.tallPortraitExcited = content.Load<Texture2D>("Assets/Images/GUI/Portrait/tallPortraitExcited");

            Assets.shortPortraitNormal = content.Load<Texture2D>("Assets/Images/GUI/Portrait/shortPortraitNormal");
            Assets.shortPortraitConfused = content.Load<Texture2D>("Assets/Images/GUI/Portrait/shortPortraitConfused");

            // Fonts

            Global.retro = content.Load<SpriteFont>("Assets/Fonts/retro");
            Global.arial = content.Load<SpriteFont>("Assets/Fonts/arial");

            // Shaders

            Global.crt = content.Load<Effect>("Assets/Shaders/CRTFilter");

            // Music

            Music.title = content.Load<Song>("Assets/Audio/Music/title");
            Music.bookstore = content.Load<Song>("Assets/Audio/Music/bookstore");
            Music.computer = content.Load<Song>("Assets/Audio/Music/computer");
            Music.boss = content.Load<Song>("Assets/Audio/Music/boss");

            // SFX

            SFX.select = content.Load<SoundEffect>("Assets/Audio/SFX/GUI/select");
            SFX.confirm = content.Load<SoundEffect>("Assets/Audio/SFX/GUI/confirm");
            SFX.no = content.Load<SoundEffect>("Assets/Audio/SFX/Battle/no");
            SFX.wait = content.Load<SoundEffect>("Assets/Audio/SFX/Battle/wait");

            SFX.dialog = content.Load<SoundEffect>("Assets/Audio/SFX/Dialog/dialog");
            SFX.dialogContinue = content.Load<SoundEffect>("Assets/Audio/SFX/Dialog/dialogContinue");

            SFX.computerStartup = content.Load<SoundEffect>("Assets/Audio/SFX/Cutscene/computerStartup");

            SFX.jump = content.Load<SoundEffect>("Assets/Audio/SFX/Battle/jump");
            SFX.hurt1 = content.Load<SoundEffect>("Assets/Audio/SFX/Battle/hurt1");
            SFX.hurt2 = content.Load<SoundEffect>("Assets/Audio/SFX/Battle/hurt2");
        }

        /// <summary>
        /// Plays a random hurt sound.
        /// </summary>
        public static void PlayHurtSound()
        {
            Random random = new Random();
            int randomNum = random.Next(0, 2);

            if (randomNum == 0)
            {
                SFX.hurt1.Play();
            }
            else
            {
                SFX.hurt2.Play();
            }
        }
    }

    /*
     * It probably isn't good practice,
     * but I separate certain assets like sound effects
     * in their own internal class to make naming easier
     */

    internal class SFX
    {
        public static SoundEffect
            // GUI
            select, confirm, no, wait,

            // Dialog
            dialog, dialogContinue,

            // Cutscenes
            computerStartup,

            // Battle
            jump,
            hurt1, hurt2;
    }

    internal class Music
    {
        public static Song
            // GUIs
            title,
            
            // Overworld
            bookstore,
            
            // Cutscenes
            computer,

            // Battles
            boss;
    }

    internal class Assets
    {
        public static Texture2D
            // GUIs
            dialogHUD,
            mainBattleHUD,

            portraitBG,

            // Portraits
            tallPortraitNormal,
            tallPortraitConfused,
            tallPortraitExcited,

            shortPortraitNormal,
            shortPortraitConfused,

            // NPCs
            computer;
    }
}
