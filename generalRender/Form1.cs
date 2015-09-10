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
            /*frame box = new frame(100, 100);
            box.graphic.DrawRectangle(Pens.Aqua, 0, 0, 99, 99);
            e.Graphics.DrawImage(box.bmap, new Point(100, 100));*/
            //mazeElement mElem = new mazeElement(50, 50, Color.Green, e_direction.up | e_direction.down | e_direction.left | e_direction.right);
            //mElem.delDirection(e_direction.right | e_direction.up);
            //e.Graphics.DrawImage(mElem.picture, new Point(100, 200));
            //mElem.setColor(Color.Red);
            //e.Graphics.DrawImage(mElem.picture, new Point(150, 200));
            mazeGraphicArray mga = new mazeGraphicArray(5, 5, 40, 40, Color.Red, e_direction.up | e_direction.down | e_direction.left | e_direction.right);
            mga.draw();
            e.Graphics.DrawImage(mga.picture, new Point(10, 10));
        }
    }
}
