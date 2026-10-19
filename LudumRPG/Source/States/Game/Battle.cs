using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LudumRPG.Source.Graphics;
using LudumRPG.Source.Objects;
using LudumRPG.Source.Objects.Battle.Characters.PartyMembers;
using LudumRPG.Source.Objects.Battle.Characters.Bosses;
using LudumRPG.Source.Objects.Battle.UI;

namespace LudumRPG.Source.States
{
    internal class Battle : State
    {
        // Debug
        private Text debugText;
        
        // Characters
        private PlayerOne playerOne;
        private PlayerTwo playerTwo;

        private Computer computer;

        // Positions
        private Point
            playerOnePos = new Point(70, 110),
            playerTwoPos = new Point(40, 130),

            bossPos = new Point(Global.resWidth - 60, 120);

        // GUI
        private BattleHUD battleHUD;

        // Properties
        public readonly int
            playerOneTurn = 0,
            playerTwoTurn = 1,
            bossTurn = 2;

        public int currentTurn { get; private set; } = 0;
        public bool defensive { get; private set; } = false;

        public Battle()
        {
            // Debug
            debugText = new Text(Global.arial, "", new Vector2(2, 2), Color.White, 0.60f);

            // Set Characters
            playerOne = new PlayerOne(playerOnePos);
            playerTwo = new PlayerTwo(playerTwoPos);

            computer = new Computer(bossPos);

            // Set GUI
            battleHUD = new BattleHUD();
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // Debug Mode
            if (Global.debug)
            {
                // Text
                debugText.setText(
                    $"""
                    Current Turn: {currentTurn}
                    Defensive: {defensive}
                    """
                    );

                // Keybinds
                if (KeyPress(Keys.D1))
                {
                    // Change Defense/Offense
                    playerOne.defense = !playerOne.defense;
                    playerTwo.defense = !playerTwo.defense;
                }
                if (KeyPress(Keys.D2))
                {
                    // Change Turn
                    if (currentTurn >= bossTurn) currentTurn = 0;
                    else currentTurn++;
                }
            }

            // Update Properties
            if (!playerOne.defense && !playerTwo.defense) defensive = false;
            else defensive = true;

            // Update Characters
            playerOne.Update(gameTime);
            playerTwo.Update(gameTime);

            computer.Update(gameTime);

            // Update GUI
            battleHUD.Update(gameTime, this);
        }

        public override void OnReset()
        {
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);

            // Draw Debug Mode
            if (Global.debug)
            {
                debugText.Draw(spriteBatch);
            }

            // Draw Characters

            playerOne.Draw(spriteBatch);
            playerTwo.Draw(spriteBatch);

            computer.Draw(spriteBatch);

            // Draw GUI

            battleHUD.Draw(spriteBatch);
        }
    }
}
