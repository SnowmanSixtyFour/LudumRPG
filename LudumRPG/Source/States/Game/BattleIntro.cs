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

        public BattleIntro()
        {
            text = new BattleIntroText();

            text.setDialog(DialogStorage.battleIntro);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // End Scene
            if (PressConfirm())
            {
                text.Proceed();
            }
            if (text.endOfDialog)
            {
                SwitchState(main.battle);
            }

            // Update Objects
            text.Update(gameTime);
        }

        public override void OnReset()
        {
            text.Reset();
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.Black);

            text.Draw(spriteBatch);
        }
    }
}
