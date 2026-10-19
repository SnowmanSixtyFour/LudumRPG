// The main state of the game.

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RentalTechBooking.Source.Graphics;
using RentalTechBooking.Source.Objects;
using RentalTechBooking.Source.States.Menu;

namespace RentalTechBooking.Source.States
{
    internal class Main : State
    {
        public State currentState;

        // States
        public Title title;
        public Overworld overworld;
        public BattleIntro battleIntro;
        public Battle battle;
        public Ending ending;

        // Overlay
        private StaticSprite pauseOverlay;
        private Text pauseText;

        public Main()
        {
            // Set States
            title = new Title();
            overworld = new Overworld();
            battleIntro = new BattleIntro();
            battle = new Battle();
            ending = new Ending();

            // Set Current State
            currentState = title;

            currentState.OnReset(); // Prepare state on startup

            // Set Overlay
            pauseOverlay = new StaticSprite(null, new Rectangle(0, 0, Global.resWidth, Global.resHeight), Color.Black * 0.5f);
            pauseText = new Text("Paused", new Vector2(106, 100), Color.White);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // --- Debug Tools ---

            // Toggle Debug Mode
            if (KeyPress(Keys.D0)) Global.debug = !Global.debug;

            // --- Pausing ---

            // If Pausing is Possible
            if (canPause)
            {
                // Pause Game when Inactive
                if (Global.pauseWhenInactive && !Global.active)
                {
                    // If in a valid state to pause
                    if (currentState != title && currentState != battleIntro)
                    {
                        Global.paused = true;
                    }
                }

                // Toggle Pause
                if (PressPause())
                {
                    if (currentState != title && currentState != battleIntro)
                    {
                        // Pause / Unpause Game
                        Global.paused = !Global.paused;
                    }
                }
            }

            // --- State ---

            // Update Current State
            if (!canPause ||
                canPause && !Global.paused)
            {
                currentState.Update(gameTime, this);
            }

            // --- Reset ---
            // Scrapped for now. Resetting the battle state is a little too daunting -Snowman64

            /*
            if (KeyPress(Keys.R))
            {
                SwitchState(title);
            }
            */
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            // Draw Current State
            currentState.OnDraw(spriteBatch);

            // Pause Overlay
            if (canPause)
            {
                if (Global.paused)
                {
                    pauseOverlay.Draw(spriteBatch);
                    pauseText.Draw(spriteBatch);
                }
            }
        }
    }
}
