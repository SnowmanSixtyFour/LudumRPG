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
    internal class BattleIntro : State
    {
        private BattleIntroText text;
        private bool textFinished = false;

        // Reimplementing the previous Dialog HUD for the tutorial tips
        private DialogHUD dialogHUD;

        public BattleIntro()
        {
            // Set Battle Intro Text
            text = new BattleIntroText();
            text.setDialog(DialogStorage.battleIntro);

            dialogHUD = new DialogHUD();
            dialogHUD.setDialog(DialogStorage.tutorial);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // Progress Text
            if (PressConfirm())
            {
                text.Proceed();
            }

            // Start Tutorial Dialog
            if (text.endOfDialog)
            {
                if (!textFinished)
                {
                    SFX.wait.Play();

                    textFinished = true;
                }

                dialogHUD.Show();
            }

            if (PressConfirm() && textFinished)
            {
                dialogHUD.Proceed();
            }

            // End Scene
            if (dialogHUD.endOfDialog)
            {
                SFX.confirm.Play();

                SwitchState(main.battle);
            }

            // Update Objects
            text.Update(gameTime);
            dialogHUD.Update(gameTime);
        }

        public override void OnReset()
        {
            // Start Music
            ChangeSong(Music.computer);

            // Reset Text
            text.setDialog(DialogStorage.battleIntro);
            text.Reset();

            dialogHUD.setDialog(DialogStorage.tutorial);
            dialogHUD.ResetTypewriter();
            dialogHUD.Hide();
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.Black);

            text.Draw(spriteBatch);
            if (textFinished) dialogHUD.Draw(spriteBatch);
        }
    }
}
