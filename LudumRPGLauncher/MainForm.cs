// The launcher made quick for the Ludum Dare 60 release of Ludum RPG. Mainly to mess with options.

// The code is pretty rough, I'll refine it later on. -Snowman64

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LudumRPGLauncher.Source;
using LudumRPGLauncher.Source.GUI;

namespace LudumRPGLauncher
{
    public partial class MainForm : Form
    {
        private MainPage main;

        public MainForm()
        {
            InitializeComponent();

            // Initialize Objects

            main = new MainPage(this);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Set Launcher Properties

            this.Text = ($"{Global.windowName} ({Global.launcherVersion})");

            // Prevent Resizing
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Set Size
            this.Width = Global.windowWidth;
            this.Height = Global.windowHeight;

            // Black and White Theme
            this.BackColor = Color.Black;
            this.ForeColor = Color.White;

            this.CenterToScreen();
        }
    }
}
