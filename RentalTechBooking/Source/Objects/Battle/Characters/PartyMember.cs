using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RentalTechBooking.Source.Objects.Battle.Characters
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

        // Character Values
        public int HP { get; set; }
        public int maxHP { get; private set; } = 20;
        protected float jumpPitch = 0.0f;

        public PartyMember(Keys jumpKey, Buttons jumpButton, Texture2D texture, Point location, Point size, Point sheetSize, Color color) : base(texture, location, size, sheetSize, color)
        {
            // Set Properties
            this.startLocation = location;
            this.jumpKey = jumpKey;
            this.jumpButton = jumpButton;

            // Set Values
            this.HP = maxHP;
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
                // Allow the party member to jump
                Jump();
            }

            // Offense Mode
            else
            {
            }
        }

        /// <summary>
        /// Makes the party member jump in the air.
        /// </summary>
        public void Jump()
        {
            if (!jumping)
            {
                if (KeyDown(jumpKey) || ButtonDown(jumpButton))
                {
                    yVelocity = -jumpIncrement;

                    jumping = true;

                    SFX.jump.Play(1.0f, jumpPitch, 0.0f);
                }
            }
        }

        public void SetMaxHealth(int amount, bool restoreHP = false)
        {
            this.maxHP = amount;

            if (restoreHP) this.HP = maxHP;
        }
    }
}
