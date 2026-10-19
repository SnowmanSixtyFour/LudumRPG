using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RentalTechBooking.Source.Graphics;
using RentalTechBooking.Source.Objects.Battle.Characters.Bosses;
using RentalTechBooking.Source.Objects.Battle.Characters;

namespace RentalTechBooking.Source.Objects.Battle.UI.Bosses
{
    internal class BossHealthBar
    {
        private int newWidth;

        private StaticSprite outline, bar, innerBar;
        private int
            actualBarWidth = 200,
            yPadding = 10,
            outlineSize = 2;

        private Color
            barColor = new Color(255, 48, 48),
            innerBarColor = new Color(96, 12, 12);

        public BossHealthBar()
        {
            // Set Sprites
            outline = new StaticSprite(null, new Rectangle(26, yPadding, 204, 11), Color.Black);
            bar = new StaticSprite(null, new Rectangle(28, yPadding + outlineSize, actualBarWidth, 7), barColor);
            innerBar = new StaticSprite(null, bar.GetDestRect(), innerBarColor);
        }

        public void Update(GameTime gameTime, Boss boss)
        {
            // Calculate Boss Bar Size
            newWidth = (actualBarWidth * boss.HP) / boss.maxHP;
            bar.SetWidth(newWidth);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            outline.Draw(spriteBatch);
            innerBar.Draw(spriteBatch);
            bar.Draw(spriteBatch);
        }
    }
}
