using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LudumRPG.Source.Graphics;
using LudumRPG.Source.Objects;
using LudumRPG.Source.Objects.Title;

namespace LudumRPG.Source.States.Menu
{
    internal class Title : State
    {
        private StaticSprite BG;
        private float bgScrollSpeed = 30.0f;

        private Logo logo;

        private Text pressStart;

        float startTimer = 0f;
        private bool startVisible = true;

        public Title()
        {
            BG = new StaticSprite(Global.titleBG, new Rectangle(new Point(0, 0), new Point(Global.windowWidth, Global.windowHeight)), Color.White, true);
            
            logo = new Logo(new Point(20, 50));

            pressStart = new Text(Global.retro, "Press Start!", new Vector2(86, 160), Color.White);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // Update BG
            float bgOffset = bgScrollSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            BG.xOffset += bgOffset;
            BG.yOffset += bgOffset;

            // Update Sprites
            logo.Update(gameTime);

            // Go to Overworld
            if (PressConfirm())
            {
                SwitchState(main.overworld);
            }

            // Press Start Flicker
            startTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (startTimer >= 500f)
            {
                startVisible = !startVisible;
                
                startTimer = 0f;
            }
        }

        public override void OnReset()
        {
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            BG.Draw(spriteBatch);

            logo.Draw(spriteBatch);

            if (startVisible) pressStart.Draw(spriteBatch);
        }
    }
}
