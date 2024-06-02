using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SM02P1
{
    public partial class UserControl1: UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.MouseClick += mouseClick;

            points = new List<PointF>();
            pointsInRange = new List<PointF>();
        }
        private List<PointF> points;
        private List<PointF> pointsInRange;

        private PointF pointQ;
        private const int d = 200;

        private void mouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Console.WriteLine($"Mouse clicked");
                points.Add(new PointF(e.X, e.Y));

                this.Invalidate();
            }
            else if (e.Button == MouseButtons.Right)
            {
                Console.WriteLine("Mouse Right");
                pointQ = new PointF(e.X, e.Y);
                pointsInRange = PointsWithinDistance(points, pointQ, d);
                this.Invalidate();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush brush = new SolidBrush(Color.Black);
            Brush rangePoints = new SolidBrush(Color.Green);
            Brush fixedPoint = new SolidBrush(Color.Red);

            foreach (PointF p in points)
            {
                g.FillEllipse(brush, p.X - 3, p.Y - 3, 6, 6);
            }

            foreach (PointF p in pointsInRange)
            {
                g.FillEllipse(rangePoints, p.X - 3, p.Y - 3, 6, 6);
            }

            g.FillEllipse(fixedPoint, pointQ.X - 4, pointQ.Y - 4, 6, 6);
        }
        ////////////////////////////
        private List<PointF> PointsWithinDistance(List<PointF> points, PointF q, double d)
        {
            List<PointF> result = new List<PointF>();
            foreach (var p in points)
            {
                double distance = Math.Sqrt(Math.Pow(p.X - q.X, 2) + Math.Pow(p.Y - q.Y, 2));
                if (distance <= d)
                {
                    result.Add(p);
                }
            }
            return result;
        }
        //////////////////////////
    }
}
