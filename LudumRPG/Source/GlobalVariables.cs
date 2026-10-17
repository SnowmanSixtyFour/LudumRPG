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

namespace LudumRPG.Source
{
    internal class Global
    {
        // Window

        public static string windowName = "Ludum RPG";

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

        // Graphics

        public static bool shadersEnabled = false;

        // Sprites

        public static Texture2D
            // Util
            noImg,

            // Title
            titleBG,
            logo;

        public static SpriteFont
            retro;

        public static Effect
            crt;

        // Set game options
        public static void SetOptions()
        {
            // Window Size
            Global.windowWidth = MainGame.options.Width;
            Global.windowHeight = MainGame.options.Height;

            // Fullscreen
            Global.fullscreen = MainGame.options.Fullscreen;
        }

        // Load game assets
        public static void LoadContent(ContentManager content)
        {
            // Images

            Global.noImg = content.Load<Texture2D>("Assets/Images/pixel");

            Global.titleBG = content.Load<Texture2D>("Assets/Images/Title/titlePattern");
            Global.logo = content.Load<Texture2D>("Assets/Images/Title/logo");

            // Fonts

            Global.retro = content.Load<SpriteFont>("Assets/Fonts/retro");

            // Shaders

            Global.crt = content.Load<Effect>("Assets/Shaders/CRTFilter");

            // Sounds

            SFX.dialog = content.Load<SoundEffect>("Assets/Audio/SFX/Dialog/dialog");
            SFX.dialogContinue = content.Load<SoundEffect>("Assets/Audio/SFX/Dialog/dialogContinue");
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
            // Dialog
            dialog, dialogContinue;
    }
}
