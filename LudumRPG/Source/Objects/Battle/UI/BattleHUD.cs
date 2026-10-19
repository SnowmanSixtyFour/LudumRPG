using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LudumRPG.Source.Graphics;
using LudumRPG.Source.Data;

namespace LudumRPG.Source.Objects.Battle.UI
{
    internal class BattleHUD
    {
        // Visuals
        private StaticSprite BG;
        private Text question;

        // Bare bones button code
        private Text
            attackButton,
            healButton;
        public bool attackSelected { get; private set; } = true;

        private Color fancyColor; // Selected Button Colour
        private bool fancyColorUp;
        private byte amountToAdd = 10;

        // Properties
        private bool
            visible = true,
            onMainHUD = true;

        public bool active { get; private set; } = true;

        public BattleHUD()
        {
            BG = new StaticSprite(Assets.mainBattleHUD, new Rectangle(12, 160, Global.resWidth - 24, 50), Color.White);
            question = new Text("", new Vector2(BG.X + 8, BG.Y + 4), Color.White);

            attackButton = new Text("Attack", new Vector2(BG.X + 40, BG.Y + 20), Color.White);
            healButton = new Text("Heal", new Vector2(attackButton.X + 120, attackButton.Y), Color.White);

            fancyColor = new Color(255, 255, 0);
            fancyColorUp = true;
        }

        public void Update(GameTime gameTime, States.Battle battle)
        {
            // Hide HUD
            if (battle.currentTurn == battle.bossTurn || battle.defensive) visible = false;
            else visible = true;

            // Update HUD
            if (onMainHUD)
            {
                // Question Flavour Text
                if (battle.currentTurn == battle.playerOneTurn)
                {
                    question.setText($"What will {DialogStorage.name1} do?");
                }
                if (battle.currentTurn == battle.playerTwoTurn)
                {
                    question.setText($"What will {DialogStorage.name2} do?");
                }

                // Selected Button
                if (fancyColorUp)
                {
                    if (fancyColor.B < 255)
                    {
                        if (fancyColor.B + amountToAdd < 255)
                        {
                            fancyColor.B += amountToAdd;
                        }
                        else fancyColor.B = 255;
                    }
                    else fancyColorUp = false;
                }
                else
                {
                    if (fancyColor.B > 0)
                    {
                        if (fancyColor.B - amountToAdd > 0)
                        {
                            fancyColor.B -= amountToAdd;
                        }
                        else fancyColor.B = 0;
                    }
                    else fancyColorUp = true;
                }
                if (attackSelected)
                {
                    attackButton.setColor(fancyColor);
                    healButton.setColor(Color.White);
                }
                else
                {
                    attackButton.setColor(Color.White);
                    healButton.setColor(fancyColor);
                }
            }

            // Update Public Properties
            if (visible) active = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
            {
                // Main HUD
                if (onMainHUD)
                {
                    BG.Draw(spriteBatch);
                    question.Draw(spriteBatch);

                    attackButton.Draw(spriteBatch);
                    healButton.Draw(spriteBatch);
                }
            }
        }

        // Button Handling

        /// <summary>
        /// Alternate between the two buttons provided.
        /// </summary>
        public void SwitchButton()
        {
            // Switch Button Selected
            attackSelected = !attackSelected;

            // Reset Colour
            fancyColor.B = 0;
            fancyColorUp = true;
        }
    }
}
