using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumRPG.Source.Objects.Battle.Characters
{
    internal class Boss : Character
    {
        // Boss Properties
        public int HP { get; set; }
        public int maxHP { get; private set; } = 100;

        public Boss(Texture2D texture, Point location, Point size, Point sheetSize, Color color) : base(texture, location, size, sheetSize, color)
        {
            // Set HP
            this.HP = maxHP;
        }
    }
}
