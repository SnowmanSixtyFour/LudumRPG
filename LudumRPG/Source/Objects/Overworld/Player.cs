using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudumRPG.Source.Objects.Overworld.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LudumRPG.Source.Objects.Overworld
{
    internal class Player : Character
    {
        protected int walkSpeed = 1;

        public bool movingHorizontal { get; private set; } = false;
        public bool movingVertical { get; private set; } = false;

        public bool canMove = true;

        public Player(int X, int Y) : base(null, new Point(X, Y), new Point(16, 16), new Point(16, 16), Color.White)
        {
            // Create Animations
            CreateAnimation("idle", 0, 0);
            CreateAnimation("walk", 1, 2);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (canMove)
            {
                // Movement

                if (HoldLeft())
                {
                    X -= walkSpeed;
                }
                if (HoldRight())
                {
                    X += walkSpeed;
                }
                if (HoldUp())
                {
                    Y -= walkSpeed;
                }
                if (HoldDown())
                {
                    Y += walkSpeed;
                }

                if (HoldLeft() || HoldRight())
                {
                    movingHorizontal = true;
                }
                else
                {
                    movingHorizontal = false;
                }
                if (HoldUp() || HoldDown())
                {
                    movingVertical = true;
                }
                else
                {
                    movingVertical = false;
                }

                // Animations

                // Idle
                if (!HoldLeft()
                    && !HoldRight()
                    && !HoldUp()
                    && !HoldDown())
                {
                    PlayAnimation("idle");
                }
                // Walking
                else
                {
                    PlayAnimation("walk");
                }
            }
        }
    }
}
