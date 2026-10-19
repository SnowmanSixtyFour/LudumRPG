using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace LudumRPG.Source.Objects.Battle.Characters.Bosses
{
    internal class Computer : Boss
    {
        public Computer(Point startingLocation) : base(Global.noImg, startingLocation, new Point(16, 16), new Point(16, 16), Color.White)
        {
        }
    }
}
