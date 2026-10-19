using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LudumRPG.Source.Objects.Battle.Characters.PartyMembers
{
    internal class PlayerOne : PartyMember
    {
        public PlayerOne(Point startingLocation) : base(Keys.Z, Buttons.A, Global.noImg, startingLocation, new Point(16, 16), new Point(16, 16), Color.White)
        {
        }
    }
}
