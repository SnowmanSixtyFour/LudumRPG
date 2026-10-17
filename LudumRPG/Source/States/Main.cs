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
        public Battle battle;

        // Overlay
        private StaticSprite pauseOverlay;

        public Main()
        {
            // Set States
            title = new Title();
            overworld = new Overworld();

            // Set Current State
            currentState = title;

            // Set Overlay
            pauseOverlay = new StaticSprite(null, new Rectangle(0, 0, Global.resWidth, Global.resHeight), Color.Black * 0.5f);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // --- Pausing ---

            // Unpause automatically if not in a valid state to pause
            if (Global.paused)
            {
                if (currentState == title)
                {
                    Global.paused = false;
                }
            }

            // If Pausing is Possible
            if (canPause)
            {
                // Toggle Pause
                if (PressPause())
                {
                    // If in a valid state to pause
                    if (currentState != title)
                    {
                        // Pause / Unpause Game
                        Global.paused = !Global.paused;
                    }
                }
            }

            // Pause Game when Inactive
            if (Global.pauseWhenInactive && !Global.active) Global.paused = true;

            // --- State ---

            // Update Current State
            if (!canPause ||
                canPause && !Global.paused)
            {
                currentState.Update(gameTime, this);
            }
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
