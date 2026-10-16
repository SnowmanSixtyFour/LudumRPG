// The main state of the game.

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LudumRPG.Source.Graphics;
using LudumRPG.Source.Objects;

namespace LudumRPG.Source.States
{
    internal class Main : State
    {
        public State currentState;

        // States
        public Title title;
        public Level level;

        public Main()
        {
            // Set States
            title = new Title();
            level = new Level();

            // Set Current State
            currentState = title;
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // Pausing
            if (canPause) // If Pausing is Possible
            {
                // Toggle Pause
                if (KeyPress(Keys.Escape))
                {
                    // If in a valid state to pause
                    if (currentState != title)
                    {
                        // Pause / Unpause Game
                        Global.paused = !Global.paused;
                    }
                }
            }

            // Unpause automatically if not in a valid state to pause
            if (Global.paused)
            {
                if (currentState == title)
                {
                    Global.paused = false;
                }
            }

            // Pause Game when Inactive
            if (Global.pauseWhenInactive && !Global.active) Global.paused = true;

            // Update Current State
            currentState.Update(gameTime, this);
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            // Draw Current State
            currentState.OnDraw(spriteBatch);
        }
    }
}
