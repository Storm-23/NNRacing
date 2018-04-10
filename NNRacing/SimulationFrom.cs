using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using NNRacing.Properties;
using NNRacingCore;

namespace NNRacing
{
    public partial class SimulationFrom : Form
    {
        private bool IsSimulated;
        private byte[] lastLeaderWeights;
        private Car leader;
        private long prevTimestamp;
        private GraphicsPath TrackPath;

        public SimulationFrom()
        {
            InitializeComponent();

            if (File.Exists(Settings.Default.LastTrack))
                try
                {
                    OpenTrack(Settings.Default.LastTrack);
                }
                catch
                {
                }

            SetStyle( ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);

            //start simulation thread
            new Thread(o => Simulate()) {Priority = ThreadPriority.AboveNormal, IsBackground = true}.Start();
        }

        private Track Track
        {
            get { return Trainer.Instance.Track; }
        }

        private void Simulate()
        {
            var sw = Stopwatch.StartNew();

            while (true)
            {
                if (!IsSimulated)
                {
                    Thread.Sleep(1);
                    continue;
                }

                if (!Trainer.Instance.IsAlive)
                {
                    if (leader != null)
                    {
                        //show leader info
                        lbBestTime.Text = string.Format("Leader Path: {0:0.} m", leader.TotalPassedLength);
                        //save leader NN weights
                        using (var str = new MemoryStream())
                        {
                            var nn = Trainer.Instance.GeneticNN.GetNN(leader.Index);
                            nn.SaveWeights(str);
                            lastLeaderWeights = str.ToArray();
                        }
                    }
                    Trainer.Instance.BuildNextGeneration();
                }

                var dt = 0.02f;

                //update cars
                Trainer.Instance.Update(dt);

                //find leader
                var maxTotalPath = 0f;
                foreach (var car in Trainer.Instance.Cars)
                    if (car.TotalPassedLength > maxTotalPath)
                    {
                        maxTotalPath = car.TotalPassedLength;
                        leader = car;
                    }

                if (!cbFast.Checked)
                {
                    //wait interval of time
                    var timeScale = 3;
                    while (sw.ElapsedMilliseconds - prevTimestamp < 1000 * dt / timeScale)
                        Thread.Sleep(0);
                    prevTimestamp = sw.ElapsedMilliseconds;
                }

                //refresh form
                Invalidate(false);
            }
        }

        private void btQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btTrackEditor_Click(object sender, EventArgs e)
        {
            new TrackEditor().ShowDialog(this);
        }

        private void btOpenTrack_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog {Filter = "Track|*.xml"};
            if (dlg.ShowDialog(this) == DialogResult.OK)
                OpenTrack(dlg.FileName);
        }

        private void OpenTrack(string fileName)
        {
            using (var fs = File.OpenRead(fileName))
            {
                Trainer.Instance.Track = (Track) new XmlSerializer(typeof(Track)).Deserialize(fs);
                Trainer.Instance.Track.Prepare();
                BuildPath();
                Settings.Default.LastTrack = fileName;
                Settings.Default.Save();
            }
            Invalidate(false);
        }

        private void BuildPath()
        {
            var points = Track.Points;
            var maxX = points.Max(p => p.Point.X + p.Width);
            var maxY = points.Max(p => p.Point.Y + p.Width);
            var bmp = new Bitmap((int) maxX + 10, (int) maxY + 10);

            using (var gr = Graphics.FromImage(bmp))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;

                var pp = new List<PointF>();

                using (var pen = new Pen(Color.Black, 2))
                {
                    for (var i = 0; i < points.Count; i++)
                    {
                        var p = points[i];
                        var ppp = new PointF[] {p.LeftWall.From, p.LeftWall.To, p.RightWall.To, p.RightWall.From};
                        gr.FillPolygon(Brushes.Silver, ppp);
                        gr.DrawPolygon(Pens.Silver, ppp);
                        gr.DrawLine(pen, p.LeftWall.From, p.LeftWall.To);
                        gr.DrawLine(pen, p.RightWall.From, p.RightWall.To);
                        pp.Add(p.Point);
                    }
                }

                pp.Add(points[0].Point);

                using (var pen = new Pen(Color.White) {DashCap = DashCap.Round, DashPattern = new[] {4.0F, 3.0F}})
                {
                    gr.DrawLines(pen, pp.ToArray());
                }
            }

            if (BackgroundImage != null)
                BackgroundImage.Dispose();
            BackgroundImage = bmp;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Track == null)
                return;
            var gr = e.Graphics;

            gr.SmoothingMode = SmoothingMode.HighSpeed;

            for (var i = Trainer.Instance.Cars.Count - 1; i >= 0; i--)
            {
                var car = Trainer.Instance.Cars[i];

                var brush = car.IsAlive ? Brushes.Red : Brushes.Gray;

                if (car == leader)
                    brush = Brushes.Yellow;

                var d = car.LookAt.Mul(1.5f * car.Length / 2);
                var p1 = car.Pos.Add(d);
                var p2 = car.Pos.Sub(d);

                using (var pen = new Pen(brush, 2))
                {
                    gr.DrawLine(pen, p1, p2);
                }
            }

            if (leader != null)
            {
                lbStat1.Text = string.Format("Throttle: {0:0.00}", leader.Throttle);
                lbStat2.Text = string.Format("Steering: {0}", ToString(leader.SteeringAngle, "0.00"));
                lbStat4.Text = string.Format("Breaks", leader.Breaks);
                lbStat3.Text = string.Format("Speed: {0:000} km/h", leader.Velocity.Length() * 60 * 60 / 1000);
                lbStat4.BackColor = leader.Breaks ? Color.Orange : SystemColors.Control;
                ssMain.Refresh();
            }
        }

        private string ToString(float v, string format)
        {
            var res = v.ToString(format);
            if (!res.StartsWith("-"))
                res = "+" + res;
            return res;
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            if (IsSimulated)
            {
                IsSimulated = false;
                btStart.BackColor = SystemColors.Control;
                return;
            }

            if (Track == null)
            {
                MessageBox.Show("Load Track before!");
                return;
            }

            btStart.BackColor = Color.Gold;

            //
            Trainer.Instance.Population = (int) nudPopulation.Value;
            Trainer.Instance.BuildFirstPopulation();
            //
            IsSimulated = true;
        }

        private void btSaveLeaderNN_Click(object sender, EventArgs e)
        {
            if (lastLeaderWeights != null)
            {
                var weights = lastLeaderWeights;
                var sfd = new SaveFileDialog {Filter = "NN|*.txt"};
                if (sfd.ShowDialog(this) == DialogResult.OK)
                    File.WriteAllBytes(sfd.FileName, weights);
            }
        }

        private void btLoadNNandStart_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog {Filter = "NN|*.txt"};
            if (ofd.ShowDialog(this) == DialogResult.OK)
                Trainer.Instance.DefaultNNWeights = File.ReadAllBytes(ofd.FileName);
        }
    }
}