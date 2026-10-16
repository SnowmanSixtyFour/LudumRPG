// The template class for a state in the game.
// A state is a different scene.

/*
 * Templates:
 * 
 * To Update:
 * public override void Update(GameTime gameTime)
 * 
 * To Draw:
 * public override void OnDraw(SpriteBatch spriteBatch)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Gaming.Input;
using Windows.Gaming.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LudumRPG.Source.Data.Storage;
using LudumRPG.Source.Objects;

namespace LudumRPG.Source.States
{
    internal class State
    {
        public GraphicsDeviceManager graphics = MainGame.publicGraphics;
        public GraphicsDevice graphicsDevice = MainGame.publicGraphicsDevice;

        // State variables
        public KeyboardState keyboard, previousKeyboard;

        public MouseState mouse, previousMouse;
        public Vector2 mouseDelta;
        protected float MAXDELTA = Global.mouseDeltaMax;

        public GamePadState gamepad, previousGamepad;

        public int screenWidth, screenHeight;
        public Camera cam;

        // Pausing
        public bool canPause = true;

        // Files
        private OptionsFile options = MainGame.options;

        public State()
        {
            // Set Camera
            cam = new Camera(this.graphicsDevice, Global.windowWidth, Global.windowHeight);
        }

        public void Update(GameTime gameTime)
        {
            // Update state variables
            screenWidth = graphicsDevice.PresentationParameters.Bounds.Width;
            screenHeight = graphicsDevice.PresentationParameters.Bounds.Height;

            // Set Controls
            keyboard = Keyboard.GetState();
            gamepad = GamePad.GetState(PlayerIndex.One);

            // Override Update
            OnUpdate(gameTime);

            // Update Controls

            // Mouse Delta
            mouseDelta = new Vector2(Math.Min(MAXDELTA, (previousMouse.X - mouse.X)), Math.Min(MAXDELTA, (previousMouse.Y - mouse.Y)));

            previousKeyboard = keyboard;
            previousGamepad = gamepad;
        }

        // Data Files

        public void SaveFile(Data.DataFile file)
        {
        }

        public void LoadFile(Data.DataFile file)
        {
        }

        public void SaveToOptions()
        {
            // TODO: Update Properties in Options File

            // Save to File
            options.Save();
        }

        public virtual void OnUpdate(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            OnDraw(spriteBatch);
        }

        /// <summary>
        /// The method to override when drawing in the state.
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch used in Draw.</param>
        public virtual void OnDraw(SpriteBatch spriteBatch)
        {
        }

        /// <summary>
        /// Quits the game. Use with caution.
        /// </summary>
        public void ExitGame()
        {
            Global.quit = true;
        }

        // Controls

        public bool KeyPress(Keys key)
        {
            if (keyboard.IsKeyUp(key) && previousKeyboard.IsKeyDown(key))
            {
                return true;
            }
            else return false;
        }

        public bool KeyDown(Keys key)
        {
            if (keyboard.IsKeyDown(key))
            {
                return true;
            }
            else return false;
        }

        public bool ButtonPress(Buttons button)
        {
            if (gamepad.IsConnected)
            {
                if (gamepad.IsButtonUp(button) && previousGamepad.IsButtonDown(button))
                {
                    return true;
                }
                else return false;

            }
            else return false;
        }

        public bool ButtonDown(Buttons button)
        {
            if (gamepad.IsConnected)
            {
                if (gamepad.IsButtonDown(button))
                {
                    return true;
                }
                else return false;

            }
            else return false;
        }

        // UI Controls

        public bool PressConfirm()
        {
            if (KeyPress(Keys.Z) || KeyPress(Keys.Enter) || ButtonPress(Buttons.A) || ButtonPress(Buttons.Start)) return true;
            else return false;
        }

        public bool PressCancel()
        {
            if (KeyPress(Keys.X) || KeyPress(Keys.Escape) || ButtonPress(Buttons.B)) return true;
            else return false;
        }

        public bool ControllerConnected()
        {
            if (gamepad.IsConnected) return true;
            else return false;
        }

        public bool ControllerConnectivityChanged()
        {
            if (previousGamepad.IsConnected && !gamepad.IsConnected || gamepad.IsConnected && !previousGamepad.IsConnected) return true;
            else return false;
        }

        public bool PressUp()
        {
            if (KeyPress(Keys.Up) || ButtonPress(Buttons.LeftThumbstickUp) || ButtonPress(Buttons.DPadUp)) return true;
            else return false;
        }

        public bool PressDown()
        {
            if (KeyPress(Keys.Down) || ButtonPress(Buttons.LeftThumbstickDown) || ButtonPress(Buttons.DPadDown)) return true;
            else return false;
        }

        public bool PressLeft()
        {
            if (KeyPress(Keys.Left) || ButtonPress(Buttons.LeftThumbstickLeft) || ButtonPress(Buttons.DPadLeft)) return true;
            else return false;
        }

        public bool PressRight()
        {
            if (KeyPress(Keys.Right) || ButtonPress(Buttons.LeftThumbstickRight) || ButtonPress(Buttons.DPadRight)) return true;
            else return false;
        }

        public bool HoldUp()
        {
            if (KeyDown(Keys.Up) || ButtonDown(Buttons.LeftThumbstickUp) || ButtonDown(Buttons.DPadUp)) return true;
            else return false;
        }

        public bool HoldDown()
        {
            if (KeyDown(Keys.Down) || ButtonDown(Buttons.LeftThumbstickDown) || ButtonDown(Buttons.DPadDown)) return true;
            else return false;
        }

        public bool HoldLeft()
        {
            if (KeyDown(Keys.Left) || ButtonDown(Buttons.LeftThumbstickLeft) || ButtonDown(Buttons.DPadLeft)) return true;
            else return false;
        }

        public bool HoldRight()
        {
            if (KeyDown(Keys.Right) || ButtonDown(Buttons.LeftThumbstickRight) || ButtonDown(Buttons.DPadRight)) return true;
            else return false;
        }
    }
}
