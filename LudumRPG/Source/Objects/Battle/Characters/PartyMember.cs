using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LudumRPG.Source.Objects.Battle.Characters
{
    internal class PartyMember : Character
    {
        // Properties
        private Point startLocation;
        private Keys jumpKey;
        private Buttons jumpButton;

        // Jumping
        private int
            yVelocity = 0,

            jumpIncrement = 11,
            maxJumpHeight = 26,
            gravity = 1;
        private bool
            jumping = false, aboveGround = false;

        public bool defense = false;
        public bool isJumping { get { return jumping; } private set { jumping = value; } }

        public PartyMember(Keys jumpKey, Buttons jumpButton, Texture2D texture, Point location, Point size, Point sheetSize, Color color) : base(texture, location, size, sheetSize, color)
        {
            // Set Properties
            this.startLocation = location;
            this.jumpKey = jumpKey;
            this.jumpButton = jumpButton;
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // While Jumping
            if (jumping)
            {
                if (yVelocity < maxJumpHeight) yVelocity += gravity;
                else yVelocity = maxJumpHeight;
            }

            // Update Y Position
            Y += yVelocity;

            // End Jump
            if (yVelocity > 0 && Y >= startLocation.Y)
            {
                yVelocity = 0;

                jumping = false;

                Y = startLocation.Y;
            }

            // Defense Mode
            if (defense)
            {
                // --- Jumping ---

                // Initiate Jump
                if (!jumping)
                {
                    if (KeyDown(jumpKey) || ButtonDown(jumpButton))
                    {
                        yVelocity = -jumpIncrement;

                        jumping = true;

                        // TODO: Play Jump SFX
                    }
                }
            }

            // Offense Mode
            else
            {
            }
        }
    }
}
