using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LudumRPG.Source.Graphics;
using LudumRPG.Source.Objects;
using LudumRPG.Source.Objects.Title;

namespace LudumRPG.Source.States
{
    internal class Title : State
    {
        private Logo logo;

        private StaticSprite BG;
        private float bgScrollSpeed = 30.0f;

        public Title()
        {
            BG = new StaticSprite(Global.titleBG, new Rectangle(new Point(0, 0), new Point(Global.windowWidth, Global.windowHeight)), Color.White, true);
            logo = new Logo(new Point(20, 50));
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // Update BG
            float bgOffset = bgScrollSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            BG.xOffset += bgOffset;
            BG.yOffset += bgOffset;

            // Update Sprites
            logo.Update(gameTime);

            // Go to Level
            if (PressConfirm())
            {
                SwitchState(main.level);
            }
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            BG.Draw(spriteBatch);

            logo.Draw(spriteBatch);
        }
    }
}
