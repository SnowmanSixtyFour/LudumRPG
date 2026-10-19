using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RentalTechBooking.Source.Graphics;
using RentalTechBooking.Source.Objects;
using RentalTechBooking.Source.Objects.Title;

namespace RentalTechBooking.Source.States.Menu
{
    internal class Title : State
    {
        private StaticSprite BG;
        private float bgScrollSpeed = 30.0f;

        private Logo logo;

        private Text
            pressStart,
            credits;

        private String
            pressStartText = $"Press {Global.enterKey} to Start!",
            creditsText = "Code by Snowman64, Artwork by ShawSure, Entry for Ludum Dare 60";

        float startTimer = 0f;
        private bool startVisible = true;

        public Title()
        {
            BG = new StaticSprite(Global.titleBG, new Rectangle(new Point(0, 0), new Point(Global.windowWidth, Global.windowHeight)), Color.White, true);
            
            logo = new Logo(new Point(20, 50));

            pressStart = new Text(Global.retro, pressStartText, new Vector2(58, 160), Color.White);
            credits = new Text(Global.retro, creditsText, new Vector2(Global.resWidth, 208), Color.White);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // Update BG
            float bgOffset = bgScrollSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            BG.xOffset += bgOffset;
            BG.yOffset += bgOffset;

            // Go to Overworld
            if (PressConfirm())
            {
                SFX.confirm.Play();

                SwitchState(main.overworld);
            }

            // Press Start Flicker
            startTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (startTimer >= 500f)
            {
                startVisible = !startVisible;
                
                startTimer = 0f;
            }

            // Move Credits
            credits.X--;
            if (credits.X < -500) credits.X = Global.resWidth; // Reset position

            // Update Sprites
            logo.Update(gameTime);
        }

        public override void OnReset()
        {
            // Start Music
            ChangeSong(Music.title);

            // Reset Text
            credits.X = Global.resWidth;
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            BG.Draw(spriteBatch);

            logo.Draw(spriteBatch);

            if (startVisible) pressStart.Draw(spriteBatch);
            credits.Draw(spriteBatch);
        }
    }
}
