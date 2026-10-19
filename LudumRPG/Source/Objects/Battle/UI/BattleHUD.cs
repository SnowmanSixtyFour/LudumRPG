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

        // Properties
        private bool
            visible = true,
            onMainHUD = true;

        public BattleHUD()
        {
            BG = new StaticSprite(null, new Rectangle(12, 160, Global.resWidth - 24, 50), Color.Black);
            question = new Text("", new Vector2(BG.X + 8, BG.Y + 4), Color.White);
        }

        public void Update(GameTime gameTime, States.Battle battle)
        {
            // Hide HUD
            if (battle.currentTurn == battle.bossTurn) visible = false;
            else
            {
                if (!battle.defensive) visible = true;
            }

            // Update HUD
            if (onMainHUD)
            {
                if (battle.currentTurn == battle.playerOneTurn)
                {
                    question.setText($"What will {DialogStorage.name1} do?");
                }
                if (battle.currentTurn == battle.playerTwoTurn)
                {
                    question.setText($"What will {DialogStorage.name2} do?");
                }
            }
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
                }
            }
        }
    }
}
