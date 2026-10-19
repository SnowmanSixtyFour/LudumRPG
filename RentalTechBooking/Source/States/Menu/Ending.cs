using RentalTechBooking.Source.Data;
using RentalTechBooking.Source.Graphics;
using RentalTechBooking.Source.Objects;
using RentalTechBooking.Source.Objects.Battle.Intro;
using RentalTechBooking.Source.Objects.Overworld.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace RentalTechBooking.Source.States
{
    internal class Ending : State
    {
        private CreditsText text;

        public Ending()
        {
            // Set Battle Intro Text
            text = new CreditsText();
            text.setDialog(DialogStorage.credits);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // Progress Dialog
            if (PressConfirm())
            {
                text.Proceed();
            }

            // Update Objects
            text.Update(gameTime);
        }

        public override void OnReset()
        {
            // Start Music
            ChangeSong(Music.bookstore);

            // Reset Text
            text.setDialog(DialogStorage.credits);
            text.Reset();
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.Black);

            text.Draw(spriteBatch);
        }
    }
}
