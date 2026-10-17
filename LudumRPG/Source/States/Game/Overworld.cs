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
using LudumRPG.Source.Objects.Overworld;

namespace LudumRPG.Source.States
{
    internal class Overworld : State
    {
        Player player;
        Companion companion;

        DialogHUD dialogHUD;

        public Overworld()
        {
            player = new Player(new Point(0, 0));
            companion = new Companion();

            dialogHUD = new DialogHUD();
            dialogHUD.setDialog(DialogStorage.intro);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // Advance through Dialog
            if (PressConfirm())
            {
                dialogHUD.Proceed();
            }

            // Update Objects
            player.Update(gameTime);
            companion.Update(gameTime, player);
            dialogHUD.Update(gameTime);
        }

        public override void OnReset()
        {
            dialogHUD.Hide();
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);

            // Draw Objects
            player.Draw(spriteBatch);

            // Draw HUD
            dialogHUD.Draw(spriteBatch);
        }
    }
}
