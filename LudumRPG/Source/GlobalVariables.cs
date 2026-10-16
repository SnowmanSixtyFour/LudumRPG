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
            defaultHeight = 480;

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

        // Cursor Size
        public static int cursorPixels = 30; // Int (to be used for Width and Height)
        public static Point cursorSize = new Point(cursorPixels, cursorPixels); // Point

        // Graphics

        public static bool shadersEnabled = false;

        // Sprites

        public static Texture2D
            noImg,
            cursor, cursorHighlight,
            gio;
        public static SpriteFont arial;
        public static Effect crt;

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

            Global.cursor = content.Load<Texture2D>("Assets/Images/Cursor");

            Global.gio = content.Load<Texture2D>("Assets/Images/Giovanni");

            // Fonts

            Global.arial = content.Load<SpriteFont>("Assets/Fonts/Arial");

            // Shaders

            Global.crt = content.Load<Effect>("Assets/Shaders/CRTFilter");
        }
    }
}
