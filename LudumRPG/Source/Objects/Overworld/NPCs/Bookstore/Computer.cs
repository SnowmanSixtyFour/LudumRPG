using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using LudumRPG.Source.Objects.Overworld.Characters;

namespace LudumRPG.Source.Objects.Overworld.NPCs.Bookstore
{
    internal class Computer : NPC
    {
        public Computer(int X, int Y) : base(Assets.computer, new Point(X, Y), new Point(64, 16), new Point(16, 16), Color.White) 
        {
            CreateAnimation("idle", 0, 3);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            PlayAnimation("idle");
        }
    }
}
