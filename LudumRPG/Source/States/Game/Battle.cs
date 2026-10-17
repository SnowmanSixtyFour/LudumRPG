using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LudumRPG.Source.Graphics;
using LudumRPG.Source.Objects;

namespace LudumRPG.Source.States
{
    internal class Battle : State
    {
        public Battle()
        {
        }

        public override void OnUpdate(GameTime gameTime)
        {
        }

        public override void OnReset()
        {
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);
        }
    }
}
