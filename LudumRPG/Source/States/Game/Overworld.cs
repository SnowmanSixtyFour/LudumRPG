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
using LudumRPG.Source.Objects.Overworld.NPCs.Bookstore;

namespace LudumRPG.Source.States
{
    internal class Overworld : State
    {
        // Main Characters
        Player player;
        Companion companion;

        // NPCs
        Computer computer;

        // HUD
        DialogHUD dialogHUD;

        public Overworld()
        {
            player = new Player(124, 110);
            companion = new Companion(player);

            computer = new Computer(180, 30);

            dialogHUD = new DialogHUD();
            dialogHUD.setDialog(DialogStorage.intro);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // --- Dialog Events ---

            // Prevent movement during Dialog
            if (dialogHUD.isVisible())
            {
                player.canMove = false;
            }
            else
            {
                player.canMove = true;
            }

            // Advance through Dialog
            if (PressConfirm())
            {
                dialogHUD.Proceed();
            }

            // --- NPC Interactions ---
            // This part was rushed for the game jam -Snowman64

            if (player.Bounds.Intersects(computer.Bounds) && PressConfirm())
            {
                SwitchState(main.battleIntro);
            }

            // --- Update Objects ---

            player.Update(gameTime);
            companion.Update(gameTime, player);
            computer.Update(gameTime);
            dialogHUD.Update(gameTime);
        }

        public override void OnReset()
        {
            dialogHUD.Hide();
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);

            // NPCs
            computer.Draw(spriteBatch);

            // Main Characters
            if (companion.behindPlayer) companion.Draw(spriteBatch);
            player.Draw(spriteBatch);
            if (!companion.behindPlayer) companion.Draw(spriteBatch);

            // HUD
            dialogHUD.Draw(spriteBatch);
        }
    }
}
