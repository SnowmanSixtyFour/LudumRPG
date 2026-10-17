using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LudumRPG.Source.Graphics;
using LudumRPG.Source.Objects;
using LudumRPG.Source.Objects.Overworld.UI;
using LudumRPG.Source.Data.Storage;
using LudumRPG.Source.Data;
using System.Diagnostics;

namespace LudumRPG.Source.States
{
    internal class Overworld : State
    {
        DialogHUD dialogHUD;

        public Overworld()
        {
            /*
            // Test Character
            giovanni = new Character(Global.gio, new Point(10, 200), new Point(48, 29), new Point(16, 29), Color.White);
            giovanni.SetSize(2);

            // Test Create Animations
            giovanni.CreateAnimation("idle", 0, 0);
            giovanni.CreateAnimation("walk", 1, 2);

            debug = new Text(Global.arial, "", new Vector2(10, 10), Color.White, 1.0f);
            */

            dialogHUD = new DialogHUD();

            dialogHUD.setDialog(DialogStorage.test);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            dialogHUD.Update(gameTime);

            // Advance through Dialog
            if (PressConfirm())
            {
                dialogHUD.Proceed();
            }

            /*
            // Text
            debug.setText("X: " + giovanni.X
                + "\nY: " + giovanni.Y
                + "\nWidth: " + giovanni.Width
                + "\nHeight: " + giovanni.Height);

            // Char Movement
            if (KeyDown(Keys.Left))
            {
                giovanni.X -= 2;
            }
            if (KeyDown(Keys.Right))
            {
                giovanni.X += 2;
            }
            if (KeyDown(Keys.Up))
            {
                giovanni.Y -= 2;
            }
            if (KeyDown(Keys.Down))
            {
                giovanni.Y += 2;
            }

            // Char Animations
            if (!KeyDown(Keys.Left) // Idle
                && !KeyDown(Keys.Right)
                && !KeyDown(Keys.Up)
                && !KeyDown(Keys.Down))
            {
                giovanni.PlayAnimation("idle");
            }
            else // Walking
            {
                giovanni.PlayAnimation("walk");
            }

            // Toggle Filter
            if (KeyPress(Keys.Enter))
            {
                Global.shadersEnabled = !Global.shadersEnabled;
            }
            */
        }

        public override void OnReset()
        {
            dialogHUD.Hide();
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);

            dialogHUD.Draw(spriteBatch);
        }
    }
}
