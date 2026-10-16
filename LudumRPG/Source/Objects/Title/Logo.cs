using LudumRPG.Source.Graphics;
using LudumRPG.Source.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumRPG.Source.Objects.Title
{
    internal class Logo
    {
        private StaticSprite logo;

        // Movement Variables
        private bool movingUp = false;
        private float
            yVelocity = 0,
            gravity = 0.2f, maxMovementSpeed = 1.5f,

            maxUpHeight, maxDownHeight;

        public Logo(Point location)
        {
            logo = new StaticSprite(Global.logo, new Rectangle(location, new Point(216, 80)), Color.White);

            maxUpHeight = location.Y - 2;
            maxDownHeight = location.Y + 2;
        }

        public void Update(GameTime gameTime)
        {
            // Logo Movement

            if (logo.Y < maxUpHeight) movingUp = false;
            if (logo.Y > maxDownHeight) movingUp = true;

            logo.Y += Convert.ToInt32(yVelocity);

            if (movingUp)
            {
                if (yVelocity > -maxMovementSpeed) yVelocity -= gravity;
                else yVelocity = -maxMovementSpeed;
            }
            if (!movingUp)
            {
                if (yVelocity < maxMovementSpeed) yVelocity += gravity;
                else yVelocity = maxMovementSpeed;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            logo.Draw(spriteBatch);
        }
    }
}
