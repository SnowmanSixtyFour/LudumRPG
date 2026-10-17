using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LudumRPG.Source.Graphics;

namespace LudumRPG.Source.Objects.Overworld
{
    internal class Companion : Character
    {
        protected int walkSpeed = 1;
        protected int followDistance = 22;

        public bool moving { get; private set; } = false;

        public Companion(Player player) : base(null, new Point(player.X, player.Y - 22), new Point(16, 16), new Point(16, 16), Color.White)
        {
            CreateAnimation("idle", 0, 0);
            CreateAnimation("walk", 1, 2);
        }

        public void Update(GameTime gameTime, Player player)
        {
            // --- Follow Player ---

            // If player is Moving
            if (player.movingHorizontal || player.movingVertical)
            {
                if (X > (player.X + followDistance))
                {
                    X -= walkSpeed;
                }

                if (X < (player.X - followDistance))
                {
                    X += walkSpeed;
                }

                if (Y > (player.Y + followDistance))
                {
                    Y -= walkSpeed;
                }

                if (Y < (player.Y - followDistance))
                {
                    Y += walkSpeed;
                }
            }

            // Player is moving Up / Down
            if (player.movingVertical && !player.movingHorizontal)
            {
                if (X > player.X)
                {
                    X -= walkSpeed;
                }
                if (X < player.X)
                {
                    X += walkSpeed;
                }
            }

            // Player is moving Left / Right
            if (player.movingHorizontal && !player.movingVertical)
            {
                if (Y > player.Y)
                {
                    Y -= walkSpeed;
                }

                if (Y < player.Y)
                {
                    Y += walkSpeed;
                }
            }
        }
    }
}
