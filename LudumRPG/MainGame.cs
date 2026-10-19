/* ----------------------------------------------
 * LudumRPG
 * Created by Snowman64
 *
 * DEVELOPED WITH THE Z ENGINE
 * https://github.com/SnowmanSixtyFour/ZEngine
 *
 * Made for Ludum Dare 60
 * https://ldjam.com/events/ludum-dare/60
 * ----------------------------------------------
 * All assets and programs in this list are free to use under the public domain.
 *
 * CRT Scanline Shaders - Timothy Lottes
 * https://github.com/libretro/glsl-shaders/blob/master/crt/shaders/crt-lottes.glsl
 *
 * jsfxr SFX Generator - Eric Fredricksen
 * https://sfxr.me/
 *
 * Computer Startup Sound - kyles
 * https://freesound.org/people/kyles/sounds/453818/
 * ----------------------------------------------
 */

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LudumRPG.Source;
using LudumRPG.Source.Data.Files;
using LudumRPG.Source.States;
using LudumRPG.Source.Data;
using LudumRPG.Source.Graphics;

namespace LudumRPG
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public static GraphicsDeviceManager publicGraphics;
        public static GraphicsDevice publicGraphicsDevice;
        public static GameTime gameTime;

        private Main game;
        public static OptionsFile options = new OptionsFile();

        private StaticSprite crtVignette;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Initialize variables
            publicGraphics = this.graphics;
            publicGraphicsDevice = this.GraphicsDevice;

            // Set window
            this.Window.Title = Global.windowName;
            this.Window.AllowUserResizing = true;
            this.Window.ClientSizeChanged += WindowSizeChanged;

            // Set Options
            if (!options.Exists()) // If options do not exist
            {
                Debug.Print("Creating options...");

                options.Save();

                Debug.Print("Options created.");
            }

            Debug.Print("Loading options...");

            // Load Options File
            options.Load();

            // Set Properties from Options
            Global.LoadOptions();

            Debug.Print("Options loaded.");

            base.Initialize();
        }

        // When window is resized
        private void WindowSizeChanged(object sender, EventArgs e)
        {
            // Update game camera
            game.cam.SetDestRect();
        }

        private void ChangeWindowSize(int windowWidth, int windowHeight)
        {
            // Window graphics
            graphics.PreferredBackBufferWidth = windowWidth;
            graphics.PreferredBackBufferHeight = windowHeight;

            graphics.ApplyChanges();

            // Game camera
            game.cam.SetDestRect();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load Content
            Global.LoadContent(Content);

            // Load Dialog
            DialogStorage.LoadDialog();

            // Set game state
            game = new Main();

            // Set CRT Shader
            Global.crt.Parameters["brightboost"].SetValue(0.92f);

            var texSize = new Vector2(Global.windowWidth, Global.windowHeight);
            Global.crt.Parameters["textureSize"]?.SetValue(texSize);
            Global.crt.Parameters["videoSize"]?.SetValue(texSize);
            var outSize = new Vector2(MainGame.publicGraphics.PreferredBackBufferWidth, MainGame.publicGraphics.PreferredBackBufferHeight);
            Global.crt.Parameters["outputSize"]?.SetValue(outSize);

            // Set Cam
            ChangeWindowSize(Global.windowWidth, Global.windowHeight);

            // Set Overlay
            crtVignette = new StaticSprite(Global.crtVignette, new Rectangle(0, 0, Global.resWidth, Global.resHeight), Color.White);

            // Make sure to check if game should be in fullscreen on start
            UpdateFullscreen();
        }

        /// <summary>
        /// Keeps the size of the window update, depending on fullscreen or windowed mode.
        /// </summary>
        public void UpdateFullscreen()
        {
            // Store Windowed Size
            if (!graphics.IsFullScreen)
            {
                Global.windowWidth = this.GraphicsDevice.Viewport.Width;
                Global.windowHeight = this.GraphicsDevice.Viewport.Height;
            }

            // Set Bool to False (so method only runs once)
            Global.fullscreenChanged = false;

            // Change Screen Size depending on Fullscreen Status
            if (Global.fullscreen) // Fullscreen Mode
            {
                // Set Window to Monitor Size
                graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

                // Apply Changes before Fullscreen Status is Set
                graphics.ApplyChanges();
            }
            if (!Global.fullscreen) // Windowed Mode
            {
                // Set Window to Previous Windowed Size
                graphics.PreferredBackBufferWidth = Global.windowWidth;
                graphics.PreferredBackBufferHeight = Global.windowHeight;
            }

            // Update Fullscreen Mode for Window
            this.graphics.IsFullScreen = Global.fullscreen;

            // Apply Changes to Window
            graphics.ApplyChanges();
        }

        // Update Global Properties
        private void UpdateGlobal()
        {
            // Global Variables
            Global.active = this.IsActive;
        }

        protected override void Update(GameTime gameTime)
        {
            // Quit Game
            if (Global.quit) Exit();

            // Update game variables
            UpdateGlobal();

            // Update public game time
            MainGame.gameTime = gameTime;

            // Update game
            game.Update(gameTime, game);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // Draw Game whether renderInactive is true, if false then only while user is tabbed in
            if (this.IsActive
                || Global.renderInactive && !this.IsActive)
            {
                game.cam.Activate();

                // SpriteBatch
                spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                DepthStencilState.None,
                RasterizerState.CullNone
                );

                // Draw Game
                game.Draw(spriteBatch);

                // CRT Filter Overlay
                if (Global.crtFilter)
                {
                    crtVignette.Draw(spriteBatch);
                }

                // End SpriteBatch
                spriteBatch.End();

                // Draw Game Camera
                game.cam.Draw(spriteBatch);
            }

            base.Draw(gameTime);
        }
    }
}
