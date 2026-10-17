using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumRPG.Source.Data.Story
{
    internal class Dialog
    {
        public String Name { get; set; }
        public List<DialogLine> Lines { get; set; }

        public Dialog(String name)
        {
            this.Name = name;
            this.Lines = new List<DialogLine>();
        }
    }
}
