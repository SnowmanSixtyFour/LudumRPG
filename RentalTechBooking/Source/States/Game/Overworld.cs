using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RentalTechBooking.Source.Graphics;
using RentalTechBooking.Source.Objects;
using RentalTechBooking.Source.Objects.Overworld.UI;
using RentalTechBooking.Source.Data.Storage;
using RentalTechBooking.Source.Data;
using System.Diagnostics;
using RentalTechBooking.Source.Objects.Overworld;
using RentalTechBooking.Source.Objects.Overworld.NPCs.Bookstore;

namespace RentalTechBooking.Source.States
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
                if (dialogHUD.dialog != DialogStorage.introEnd)
                {
                    dialogHUD.setDialog(DialogStorage.introEnd);
                }
            }

            if (dialogHUD.dialog == DialogStorage.introEnd && dialogHUD.endOfDialog)
            {
                SwitchState(main.battleIntro);

                // Play Computer Startup Sound
                SFX.computerStartup.Play();
            }

            // --- Update Objects ---

            player.Update(gameTime);
            companion.Update(gameTime, player);
            computer.Update(gameTime);
            dialogHUD.Update(gameTime);
        }

        public override void OnReset()
        {
            // Start Music
            ChangeSong(Music.bookstore);

            // Reset Dialog
            dialogHUD.setDialog(DialogStorage.intro);
            dialogHUD.currentLine = 0;
            dialogHUD.Show();

            // Reset Characters
            player.X = 124;
            player.Y = 110;

            companion.X = player.X;
            companion.Y = player.Y - companion.followDistance;
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
