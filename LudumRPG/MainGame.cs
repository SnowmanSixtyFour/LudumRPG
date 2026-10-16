// LudumRPG
// Created by Snowman64

// DEVELOPED WITH THE Z ENGINE
// https://github.com/SnowmanSixtyFour/ZEngine

// Made for Ludum Dare 60
// https://ldjam.com/events/ludum-dare/60

// Special Thanks
// CRT Scanline Shaders (Public Domain) - Timothy Lottes

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LudumRPG.Source;
using LudumRPG.Source.Data.Storage;
using LudumRPG.Source.States;

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
            Global.SetOptions();

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
        }

        // Update Global Variables
        private void UpdateGlobal()
        {
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
            game.Update(gameTime);

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

                // End SpriteBatch
                spriteBatch.End();

                // Draw Game Camera
                game.cam.Draw(spriteBatch);
            }

            base.Draw(gameTime);
        }
    }
}
