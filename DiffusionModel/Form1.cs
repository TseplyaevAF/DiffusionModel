using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiffusionModel
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Graphics g;
        Pen pen_black, pen_blue;
        const int CountMolecules = 500;
        const int CenterPictureX = 207;
        const int HeightPicture = 332;
        Random rnd = new Random();
        
        public Form1()
        {
            InitializeComponent();
            button_Stop.Visible = false;
            pen_black = new Pen(Color.Black);
            pen_blue = new Pen(Color.Blue);
            bmp = new Bitmap(picture.Width, picture.Height);
            g = Graphics.FromImage(bmp);
            pen_black.Width = 5;
            pen_blue.Width = 5;
            DrawLine();
            Dots(pen_black, 'r');
            Dots(pen_blue, 'l');
            InitializeTimers();
        }

        private void DrawLine()
        {
            g.DrawLine(pen_black, CenterPictureX, 0, CenterPictureX, HeightPicture);
            picture.Image = bmp;
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                timer1.Enabled = true;
                timer1.Start();
            }
            button_Stop.Visible = true;
        }

        private void InitializeTimers()
        {
            // Таймер для быстрой смены кадров
            timer1.Interval = 10;
            timer1.Tick += new EventHandler(timer1_Tick);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            g.Clear(Color.FromArgb(122, 171, 211));
            RandomDots(pen_black);
            RandomDots(pen_blue);
        }

        // Рисование точек слева и справа
        private void Dots(Pen pen, char side)
        {
            int DotX, DotY;
            for (int i = 0; i < CountMolecules / 2; i++)
            {
                if (side == 'l')
                {
                    DotX = rnd.Next(0, CenterPictureX - 6);
                    DotY = rnd.Next(5, HeightPicture - 6);
                    g.DrawEllipse(pen, DotX, DotY, 5, 5);
                    picture.Image = bmp;
                }
                if (side == 'r')
                {
                    DotX = rnd.Next(CenterPictureX + 6, (CenterPictureX * 2) - 6);
                    DotY = rnd.Next(5, HeightPicture - 6);
                    g.DrawEllipse(pen, DotX, DotY, 5, 5);
                    picture.Image = bmp;
                }
            }
        }

        private void RandomDots(Pen pen)
        {
            int DotX, DotY;
            for (int i = 0; i < CountMolecules/2; i++)
            {
                int tmp;
                bool flag = false;
                do
                {
                    DotX = rnd.Next(0, (CenterPictureX * 2) - 6);
                    DotY = rnd.Next(5, HeightPicture - 6);
                    tmp = CenterPictureX - DotX;
                    if ((tmp == CenterPictureX) || (tmp < CenterPictureX))
                        flag = true;
                } while (!flag);
                g.DrawEllipse(pen, DotX, DotY, 5, 5);
                picture.Image = bmp;
            }
        }

        private void button_Stop_Click(object sender, EventArgs e)
        {
            button_Stop.Visible = false;
            timer1.Enabled = false;
            timer1.Stop();
            g.Clear(Color.FromArgb(122, 171, 211));
            DrawLine();
            Dots(pen_black, 'r');
            Dots(pen_blue, 'l');
        }
    }
}
