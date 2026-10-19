using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LudumRPG.Source.Graphics;
using LudumRPG.Source.Objects;
using LudumRPG.Source.Objects.Battle.Characters.PartyMembers;
using LudumRPG.Source.Objects.Battle.Characters.Bosses;

namespace LudumRPG.Source.States
{
    internal class Battle : State
    {
        // Characters
        private PlayerOne playerOne;
        private PlayerTwo playerTwo;

        private Computer computer;

        // Positions
        private Point
            playerOnePos = new Point(80, 120),
            playerTwoPos = new Point(50, 140),

            bossPos = new Point(Global.resWidth - 60, 130);

        public Battle()
        {
            // Set Characters
            playerOne = new PlayerOne(playerOnePos);
            playerTwo = new PlayerTwo(playerTwoPos);

            computer = new Computer(bossPos);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // Update Characters
            playerOne.Update(gameTime);
            playerTwo.Update(gameTime);

            computer.Update(gameTime);
        }

        public override void OnReset()
        {
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);

            // Draw Characters

            playerOne.Draw(spriteBatch);
            playerTwo.Draw(spriteBatch);

            computer.Draw(spriteBatch);
        }
    }
}
