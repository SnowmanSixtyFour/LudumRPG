// The main state of the game.

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LudumRPG.Source.Graphics;
using LudumRPG.Source.Objects;
using LudumRPG.Source.States.Menu;

namespace LudumRPG.Source.States
{
    internal class Main : State
    {
        public State currentState;

        // States
        public Title title;
        public Overworld overworld;
        public BattleIntro battleIntro;
        public Battle battle;

        // Overlay
        private StaticSprite pauseOverlay;

        public Main()
        {
            // Set States
            title = new Title();
            overworld = new Overworld();
            battleIntro = new BattleIntro();
            battle = new Battle();

            // Set Current State
            currentState = battle;

            // Set Overlay
            pauseOverlay = new StaticSprite(null, new Rectangle(0, 0, Global.resWidth, Global.resHeight), Color.Black * 0.5f);
        }

        public override void OnUpdate(GameTime gameTime)
        {
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
                }
            }
        }
    }
}
