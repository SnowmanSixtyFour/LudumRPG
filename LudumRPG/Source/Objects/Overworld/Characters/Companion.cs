using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LudumRPG.Source.Graphics;

namespace LudumRPG.Source.Objects.Overworld
{
    internal class Companion : Character
    {
        protected int walkSpeed = 1;

        public bool moving { get; private set; } = false;

        public Companion() : base(null, new Point(0, 0), new Point(16, 16), new Point(16, 16), Color.White)
        {
            CreateAnimation("idle", 0, 0);
            CreateAnimation("walk", 1, 2);
        }

        public void Update(GameTime gameTime, Player player)
        {
            // Follow Player
            if (player.moving)
            {
            }
        }
    }
}
