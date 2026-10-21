using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RentalTechBooking.Source.Graphics;

namespace RentalTechBooking.Source.Objects.Overworld
{
    internal class Companion : Character
    {
        protected int walkSpeed = 1;
        public readonly int followDistance = 22;

        public bool moving { get; private set; } = false;
        public bool behindPlayer { get; private set; } = true;

        public Companion(Player player) : base(Assets.beenie, new Point(player.X, player.Y - 22), new Point(396, 61), new Point(33, 61), Color.White)
        {
            CreateAnimation("idle", 0, 0);

            CreateAnimation("walkDown", 0, 3);
            CreateAnimation("walkLeft", 4, 5);
            CreateAnimation("walkRight", 6, 7);
            CreateAnimation("walkUp", 8, 11);
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
                    PlayAnimation("walkLeft");
                }

                if (X < (player.X - followDistance))
                {
                    X += walkSpeed;
                    PlayAnimation("walkRight");
                }

                if (Y > (player.Y + followDistance))
                {
                    Y -= walkSpeed;
                    PlayAnimation("walkUp");
                }

                if (Y < (player.Y - followDistance))
                {
                    Y += walkSpeed;
                    PlayAnimation("walkDown");
                }
            }

            else
            {
                PlayAnimation("idle");
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

            // Check if behind player
            if (Y <= player.Y) this.behindPlayer = true;
            else this.behindPlayer = false;

            // TODO: Play Animations
        }
    }
}
