using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LudumRPG.Source.Objects.Overworld.Characters
{
    internal class NPC : Character
    {
        public NPC(Texture2D texture, Point location, Point size, Point sheetSize, Color color) : base(texture, location, size, sheetSize, color)
        {
        }
    }
}
