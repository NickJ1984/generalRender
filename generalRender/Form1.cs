using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace generalRender
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            frame box = new frame(100, 100);
            box.graphic.DrawRectangle(Pens.Aqua, 0, 0, 99, 99);
            e.Graphics.DrawImage(box.bmap, new Point(100, 100));
        }
    }
}
