using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LudumRPG.Source.Objects.Overworld
{
    internal class Player : Character
    {
        protected int walkSpeed = 1;

        public bool moving { get; private set; } = false;

        public Player(Point location) : base(null, location, new Point(16, 16), new Point(16, 16), Color.White)
        {
            // Create Animations
            CreateAnimation("idle", 0, 0);
            CreateAnimation("walk", 1, 2);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // Char Movement
            if (KeyDown(Keys.Left))
            {
                X -= walkSpeed;
            }
            if (KeyDown(Keys.Right))
            {
                X += walkSpeed;
            }
            if (KeyDown(Keys.Up))
            {
                Y -= walkSpeed;
            }
            if (KeyDown(Keys.Down))
            {
                Y += walkSpeed;
            }

            // Char Animations
            if (!KeyDown(Keys.Left) // Idle
                && !KeyDown(Keys.Right)
                && !KeyDown(Keys.Up)
                && !KeyDown(Keys.Down))
            {
                PlayAnimation("idle");
            }
            else // Walking
            {
                PlayAnimation("walk");
            }
        }
    }
}
