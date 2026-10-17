using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumRPG.Source.Data.Story
{
    internal class DialogLine
    {
        public String Name { get; set; }
        public String Text { get; set; }

        public int Length { get { return Text.Length; } }

        public DialogLine(String name, String text)
        {
            this.Name = name;
            this.Text = text;
        }
    }
}
