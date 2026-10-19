using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RentalTechBooking.Source.Objects.Battle.Characters.PartyMembers
{
    internal class PlayerTwo : PartyMember
    {
        public PlayerTwo(Point startingLocation) : base(Keys.X, Buttons.B, Global.noImg, startingLocation, new Point(16, 16), new Point(16, 16), Color.White)
        {
            // Set Values
            SetMaxHealth(21, true);
        }
    }
}
