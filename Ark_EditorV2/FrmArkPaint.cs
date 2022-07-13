using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ark_EditorV2
{
    public partial class FrmArkPaint : Form
    {
        //Variables
        Bitmap bm;
        Graphics g;
        bool paint = false;
        Point px, py;
        Pen p = new Pen(Color.Black, 1);
        Pen eraser = new Pen(Color.White, 10);
        int index;
        int x, y, sX, sY, cX, cY;

        ColorDialog colorD = new ColorDialog();
        Color newColor;

        public FrmArkPaint()
        {
            InitializeComponent();
            this.Width = 950;
            this.Height = 700;
            bm = new Bitmap(picbDraw.Width,picbDraw.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            picbDraw.Image = bm;
        }

    //Events
        private void FrmArkPaint_Load(object sender, EventArgs e)
        {

        }

        private void picbDraw_MouseDown(object sender, MouseEventArgs e)
        {
            paint = true;
            py = e.Location;

            cX = e.X;
            cY = e.Y;
        }

        private void picbDraw_MouseMove(object sender, MouseEventArgs e)
        {

            if (paint)
            {

                if (index==1)
                {
                    px = e.Location;
                    g.DrawLine(p,px,py);
                    py = px;
                }

                if (index == 2)
                {
                    px = e.Location;
                    g.DrawLine(eraser, px, py);
                    py = px;
                }

            }
            picbDraw.Refresh();

            x = e.X;
            y = e.Y;
            sX = e.X - cX;
            sY = e.Y - cY;

        }

        private void picbDraw_MouseUp(object sender, MouseEventArgs e)
        {

            paint = false;

            sX = x - cX;
            sY = y - cY;

            if (index == 3)
            {
                g.DrawEllipse(p,cX,cY,sX,sY);
            }

            if (index==4)
            {
                g.DrawRectangle(p, cX, cY, sX, sY);
            }

            if (index == 5)
            {
                g.DrawLine(p, cX, cY,x, y);
            }
        }

        private void picbDraw_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;

            if (paint)
            {

                if (index == 3)
                {
                    g.DrawEllipse(p, cX, cY, sX, sY);
                }

                if (index == 4)
                {
                    g.DrawRectangle(p, cX, cY, sX, sY);
                }

                if (index == 5)
                {
                    g.DrawLine(p, cX, cY, x, y);
                }

            }

        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            colorD.ShowDialog();
            newColor = colorD.Color;
            btnPicColor.BackColor = colorD.Color;
            p.Color = colorD.Color;
        }

        
        //Event Mouse click to select color on Picture Box and change color burron
        private void pbColorPicker_MouseClick(object sender, MouseEventArgs e)
        {
            Point point = setPoint(pbColorPicker, e.Location);
            btnPicColor.BackColor = ((Bitmap)pbColorPicker.Image).GetPixel(point.X, point.Y);
            newColor = btnPicColor.BackColor;
            p.Color = btnPicColor.BackColor;

        }

        private void picbDraw_MouseClick(object sender, MouseEventArgs e)
        {
            if (index==7)
            {
                Point point = setPoint(picbDraw, e.Location);
                Bucket(bm, point.X, point.Y, newColor);
            }
        }


        //Butons

        private void btnBucket_Click(object sender, EventArgs e)
        {
            index = 7;
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            index = 5;
        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            index = 4;
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            index = 3;
        }

        private void btnEraser_Click(object sender, EventArgs e)
        {
            index = 2;
        }

        private void btnPencil_Click(object sender, EventArgs e)
        {
            index = 1;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            picbDraw.Image = bm;
            index = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var Sv = new SaveFileDialog();
            Sv.Filter = "Image(*.jpg)|*.jpg|(*.*|*.*";

            if (Sv.ShowDialog()==DialogResult.OK)
            {
                Bitmap btm = bm.Clone(new Rectangle(0, 0, picbDraw.Width, picbDraw.Height), bm.PixelFormat);
                btm.Save(Sv.FileName, ImageFormat.Jpeg);
                MessageBox.Show("Image saved  ;)");
            }


        }
         
        //Methods
        static Point setPoint(PictureBox pb, Point pt)
        {
            float pX = 1f * pb.Image.Width / pb.Width;
            float pY = 1f * pb.Image.Height / pb.Height;

            return new Point((int)(pt.X*pX), (int)(pt.Y*pX));
        }

        //Method  to validate old color and select new color
        private void validate(Bitmap bm, Stack<Point> sp, int x, int y, Color oldColor, Color newColor)
        {
            Color cx = bm.GetPixel(x, y);

            if (cx == oldColor)
            {
                sp.Push(new Point(x, y));
                bm.SetPixel(x, y, newColor);
            }
        }

        public void Bucket(Bitmap bm, int x, int y, Color newCol)
        {
            Color oldColor = bm.GetPixel(x, y);
            Stack<Point> pixel = new Stack<Point>();
            pixel.Push(new Point(x, y));
            bm.SetPixel(x, y, newCol);

            if (oldColor == newCol) return;

            while (pixel.Count>0)
            {
                Point pt = (Point)pixel.Pop();
                if (pt.X>=0 && pt.Y>0 && pt.X<bm.Width-1 && pt.Y<bm.Height-1)
                {
                    validate(bm,pixel, pt.X-1, pt.Y, oldColor,newCol);
                    validate(bm, pixel, pt.X, pt.Y - 1, oldColor, newCol);
                    validate(bm, pixel, pt.X + 1, pt.Y, oldColor, newCol);
                    validate(bm, pixel, pt.X, pt.Y + 1, oldColor, newCol);

                }
            }
        }
    }
}
