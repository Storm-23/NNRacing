using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NNRacingCore;

namespace CarPhysicsTester
{
    public partial class TrackPanel : UserControl
    {
        public CarBase Car;

        public TrackPanel()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
        }

        private Queue<TrackItem> track = new Queue<TrackItem>();

        private const float SCALE = 10;

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Car == null) return;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            e.Graphics.TranslateTransform(2 * Width / 3 , Height - 50);
            e.Graphics.ScaleTransform(1, -1);

            foreach (var item in track)
            {
                DrawPoint(e.Graphics, item.P1, Color.Gray);
                DrawPoint(e.Graphics, item.P2, Color.Gray);
            }

            while (track.Count > 400)
                track.Dequeue();

            var state = e.Graphics.Save();
            e.Graphics.TranslateTransform(Car.Pos.X*SCALE, Car.Pos.Y*SCALE);
            e.Graphics.RotateTransform(Car.LookAt.Angle() * 180 / (float)Math.PI);
            var img = Properties.Resources.Ery_Lamborghini_Murcielago_30;
            e.Graphics.DrawImage(img, -img.Width / 2, -img.Height / 2);
            e.Graphics.FillEllipse(Car.IsSliding ? Brushes.Red : Brushes.Blue, Car.Length * SCALE / 2, -3, 6, 6);
            e.Graphics.FillEllipse(Car.IsSliding ? Brushes.Red : Brushes.Blue, -Car.Length * SCALE / 2, -3, 6, 6);

            e.Graphics.Restore(state);

            var n = Car.LookAt.Rotate(PointFHelper.PIby2);
            var l = Car.LookAt*(Car.Length/2);
            track.Enqueue(new TrackItem { P1 = Car.Pos + l + n, P2 = Car.Pos + l - n});
            track.Enqueue(new TrackItem { P1 = Car.Pos - l + n, P2 = Car.Pos - l - n });

            base.OnPaint(e);
        }

        private SolidBrush brush = new SolidBrush(Color.White);

        private void DrawPoint(Graphics gr, Vector2 p, Color color, float size = 1.5f)
        {
            brush.Color = color;
            gr.FillEllipse(brush, p.X * SCALE-size / 2f, p.Y * SCALE-size / 2f, size, size);
        }
    }

    class TrackItem
    {
        public Vector2 P1;
        public Vector2 P2;
    }
}
