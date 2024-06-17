using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace RectangleTriangleCircle
{
    public partial class Form1 : Form
    {

        private Random rdm;
        private Thread rectangleThread;
        private Thread triangleThread;
        private Thread circleThread;
        private bool isRunning; // Флаг за продължаване на генерирането

        public Form1()
        {
            InitializeComponent();
            rdm = new Random();
            isRunning = false;
        }



        private void btnCircle_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                isRunning = true;
                circleThread = new Thread(GenerateCircles);
                circleThread.Start();
            }
        }

        private void GenerateRectangles()
        {
            try
            {
                while (isRunning)
                {
                    int width = rdm.Next(10, 100); 
                    int height = rdm.Next(10, 100); 
                    int x = rdm.Next(0, this.ClientSize.Width - width);
                    int y = rdm.Next(0, this.ClientSize.Height - height);

                    using (Graphics g = this.CreateGraphics())
                    {
                        g.DrawRectangle(new Pen(Brushes.Red, 4), new Rectangle(x, y, width, height));
                    }

                    Thread.Sleep(100);
                }
            }
            catch (ThreadAbortException)
            {
                Thread.ResetAbort();
            }
        }


        private void GenerateTriangles()
        {
            try
            {
                while (isRunning)
                {
                    int size = rdm.Next(10, Math.Min(this.ClientSize.Width, this.ClientSize.Height) / 2);
                    int x = rdm.Next(0, this.ClientSize.Width - size);
                    int y = rdm.Next(0, this.ClientSize.Height - size);

                    Point[] points = {
                        new Point(x, y + size),
                        new Point(x + size, y + size),
                        new Point(x + size / 2, y)
                    };

                    using (Graphics g = this.CreateGraphics())
                    {
                        g.DrawPolygon(new Pen(Brushes.Green, 4), points);
                    }

                    Thread.Sleep(100);
                }
            }
            catch (ThreadAbortException)
            {
                Thread.ResetAbort();
            }
        }

        private void GenerateCircles()
        {
            try
            {
                while (isRunning)
                {
                    int diameter = rdm.Next(10, Math.Min(this.ClientSize.Width, this.ClientSize.Height) / 2);
                    int x = rdm.Next(0, this.ClientSize.Width - diameter);
                    int y = rdm.Next(0, this.ClientSize.Height - diameter);

                    using (Graphics g = this.CreateGraphics())
                    {
                        g.DrawEllipse(new Pen(Brushes.Blue, 4), new Rectangle(x, y, diameter, diameter));
                    }

                    Thread.Sleep(100);
                }
            }
            catch (ThreadAbortException)
            {
                Thread.ResetAbort();
            }
        }


        private void btnRed_Click_1(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                isRunning = true;
                rectangleThread = new Thread(GenerateRectangles);
                rectangleThread.Start();
            }
        }

        private void btnTriangle_Click_1(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                isRunning = true;
                triangleThread = new Thread(GenerateTriangles);
                triangleThread.Start();
            }
        }

        private void btnStop_Click_1(object sender, EventArgs e)
        {
            isRunning = false;

            if (rectangleThread != null && rectangleThread.IsAlive)
            {
                rectangleThread.Abort();
            }

            if (triangleThread != null && triangleThread.IsAlive)
            {
                triangleThread.Abort();
            }

            if (circleThread != null && circleThread.IsAlive)
            {
                circleThread.Abort();
            }

            using (Graphics g = this.CreateGraphics())
            {
                g.Clear(BackColor);
            }
        }
    }
}
