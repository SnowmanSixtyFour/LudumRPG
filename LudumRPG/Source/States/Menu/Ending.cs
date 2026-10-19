using LudumRPG.Source.Data;
using LudumRPG.Source.Graphics;
using LudumRPG.Source.Objects;
using LudumRPG.Source.Objects.Battle.Intro;
using LudumRPG.Source.Objects.Overworld.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace LudumRPG.Source.States
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
