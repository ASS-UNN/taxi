using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TaxiDesktopClient
{
    public partial class MapForm : Form
    {
        Color Mark = Color.FromArgb(255, 0, 0);
        Bitmap Init = null;
        Bitmap Im = null;
        DesktopForm Main = null;
        float Prop;
        public int MapMode = 0;
        public MapForm()
        {
            InitializeComponent();
            Init = new Bitmap(pictureBoxMap.Image);
            Im = new Bitmap(Init);
            Prop = (float)pictureBoxMap.Size.Width / Im.Width;
            
        }

        private void pictureBoxMap_Click(object sender, EventArgs e)
        {
            MouseEventArgs M = (MouseEventArgs)e;
            int X = (int)(M.X / Prop);
            int Y = (int)(M.Y / Prop);
            if (MapMode == 0)
            {
                Main.textBoxFromX.Text = X.ToString();
                Main.textBoxFromY.Text = Y.ToString();
                Close();
            }
            if (MapMode == 1)
            {
                Main.textBoxToX.Text = X.ToString();
                Main.textBoxToY.Text = Y.ToString();
                Close();
            }
        }

        private void MapForm_Load(object sender, EventArgs e)
        {
            Main = this.Owner as DesktopForm;
            
        }

        public void DrawPos(Coord Pos)
        {
            Im = (Bitmap)Init.Clone();
            int x = (int)Pos.x;
            int y = (int)Pos.y;
            int Size=20;
            for (int i=(int)Math.Max(0,x-Prop*Size);i<Math.Min(Im.Width, x+Prop*Size); i++)
            {
                for (int j = (int)Math.Max(0, y - Prop * Size); j < Math.Min(Im.Height, y + Prop * Size); j++)
                {
                    Im.SetPixel(i, j, Mark);
                }
            }          
            pictureBoxMap.Image = Im;
            pictureBoxMap.Refresh();
        }
        public void SetInit()
        {
            pictureBoxMap.Image = Init;
            pictureBoxMap.Refresh();
        }

    }
}
