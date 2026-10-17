using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace LudumRPG.Source.Data.Story
{
    internal class DialogLine
    {
        public String Name { get; set; }
        public String Text { get; set; }
        public Texture2D Portrait { get; set; }

        public int Length { get { return Text.Length; } }

        public DialogLine(String name, String text, Texture2D portrait = null)
        {
            this.Name = name;
            this.Text = text;
            this.Portrait = portrait;
        }
    }
}
