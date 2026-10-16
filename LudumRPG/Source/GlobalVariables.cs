using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LudumRPG.Source
{
    internal class Global
    {
        // Window

        public static string windowName = "Ludum RPG";

        public static readonly int
            defaultWidth = 844,
            defaultHeight = 480,

            resWidth = 256,
            resHeight = 224;

        public static int
            windowWidth = defaultWidth,
            windowHeight = defaultHeight;

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
        }
    }
}
