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
