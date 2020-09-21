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
        Pen pen;
        const int CountMolecules = 100;
        const int CenterPictureX = 207;
        const int HeightPicture = 332;
        Random rnd = new Random();
        
        public Form1()
        {
            InitializeComponent();
            button_Stop.Visible = false;
            pen = new Pen(Color.Black);
            bmp = new Bitmap(picture.Width, picture.Height);
            g = Graphics.FromImage(bmp);
            pen.Width = 5;
            DrawLine();
            RandomDots();
            InitializeTimers();
        }

        private void DrawLine()
        {
            g.DrawLine(pen, CenterPictureX, 0, CenterPictureX, HeightPicture);
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
            timer1.Interval = 100;
            timer1.Tick += new EventHandler(timer1_Tick);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            g.Clear(Color.FromArgb(122, 171, 211));
            RandomDots();
        }

        private void RandomDots()
        {
            int DotX, DotY;
            for (int i = 0; i < CountMolecules; i++)
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
            DrawLine();
        }
    }
}
