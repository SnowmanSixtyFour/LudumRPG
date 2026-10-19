using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LudumRPG.Source.Graphics;
using LudumRPG.Source.Objects;
using LudumRPG.Source.Objects.Battle.Characters.PartyMembers;
using LudumRPG.Source.Objects.Battle.Characters.Bosses;
using LudumRPG.Source.Objects.Battle.UI;
using LudumRPG.Source.Objects.Battle.UI.Bosses;

namespace LudumRPG.Source.States
{
    internal class Battle : State
    {
        // Debug
        private Random random;
        private Text debugText;
        
        // Characters
        public PlayerOne playerOne;
        public PlayerTwo playerTwo;

        private Computer computer;

        // Positions
        private Point
            playerOnePos = new Point(70, 110),
            playerTwoPos = new Point(40, 130),

            bossPos = new Point(Global.resWidth - 60, 120);

        // GUI
        private BattleHUD battleHUD;
        private BossHealthBar bossBar;

        // Properties
        public readonly int
            playerOneTurn = 0,
            playerTwoTurn = 1,
            bossTurn = 2;

        // Player Stats
        private int
            minPlayerOneDamage = 7,
            minPlayerTwoDamage = 6,

            maxPlayerOneDamage = 14,
            maxPlayerTwoDamage = 16;

        public int currentTurn { get; private set; } = 0;
        public bool defensive { get; private set; } = false;

        public Battle()
        {
            // Debug
            random = new Random();
            debugText = new Text(Global.arial, "", new Vector2(2, 2), Color.White, 0.60f);

            // Set Characters
            playerOne = new PlayerOne(playerOnePos);
            playerTwo = new PlayerTwo(playerTwoPos);

            computer = new Computer(bossPos);

            // Set GUI
            battleHUD = new BattleHUD();
            bossBar = new BossHealthBar();
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

            // Battle Options
            if (battleHUD.active)
            {
                // Switch Button
                if (PressLeft() || PressRight())
                {
                    battleHUD.SwitchButton();
                }

                // Player One Options
                if (currentTurn == 0 && PressConfirm())
                {
                    if (battleHUD.attackSelected)
                    {
                        // Randomize Damage
                        int playerOneDamage = random.Next(minPlayerOneDamage, maxPlayerOneDamage + 1);

                        // Hurt Boss
                        Global.PlayHurtSound();
                        computer.HP -= playerOneDamage;

                        // Continue
                        currentTurn++;
                    }
                    else
                    {
                        // Prevent player from overhealing and wasting a whole turn
                        if (playerOne.HP >= playerOne.maxHP)
                        {
                            SFX.no.Play();
                        }
                        else
                        {
                            // Heal player
                            playerOne.HP += random.Next(2, 6);

                            currentTurn++;
                        }
                    }
                }

                // Player Two Options
                if (currentTurn == 1 && PressCancel())
                {
                    if (battleHUD.attackSelected)
                    {
                        int playerTwoDamage = random.Next(minPlayerTwoDamage, maxPlayerTwoDamage + 1);

                        Global.PlayHurtSound();
                        computer.HP -= playerTwoDamage;

                        currentTurn++;
                    }
                    else
                    {
                        if (playerTwo.HP >= playerTwo.maxHP)
                        {
                            SFX.no.Play();
                        }
                        else
                        {
                            playerTwo.HP += random.Next(1, 7);

                            currentTurn++;
                        }
                    }
                }

                // Wrong input
                if (currentTurn == 0 && PressCancel()) SFX.no.Play();
                if (currentTurn == 1 && PressConfirm()) SFX.no.Play();
            }

            // Boss Turn
            if (currentTurn > 1)
            {
                // If possible, set players to Defensive Mode
                if (playerOne.HP > 0) playerOne.defense = true;
                if (playerTwo.HP > 0) playerTwo.defense = true;
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
            bossBar.Update(gameTime, computer);
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

            // Draw GUI
            battleHUD.Draw(spriteBatch);
            bossBar.Draw(spriteBatch);

            // Draw Debug Mode
            if (Global.debug)
            {
                debugText.Draw(spriteBatch);
            }
        }
    }
}
