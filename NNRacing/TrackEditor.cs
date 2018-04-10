using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using NNRacingCore;

namespace NNRacing
{
    public partial class TrackEditor : Form
    {
        private ControlPoint draggedPoint;
        private ControlPoint selectedPoint;
        private Track Track = new Track();

        public TrackEditor()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);

            DrawModeOn();
            UpdateInterface();
        }

        private float PathWidth
        {
            get { return (float) nudTrackWidth.Value; }
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog {Filter = "Track|*.xml"};
            if (dlg.ShowDialog() == DialogResult.OK)
                OpenTrack(dlg.FileName);
        }

        private void OpenTrack(string fileName)
        {
            using (var fs = File.OpenRead(fileName))
            {
                Track = (Track) new XmlSerializer(typeof(Track)).Deserialize(fs);
            }
            DrawModeOff();
            nudAdhesion.Value = (decimal) Track.Adhesion;
            Invalidate(true);
        }

        private void SaveTrack(string fileName)
        {
            RemoveTempPoints();

            using (var fs = File.Create(fileName))
                new XmlSerializer(typeof(Track)).Serialize(fs, Track);

            //build image map
            var bmp = BuildImageMap();
            fileName = Path.ChangeExtension(fileName, ".png");
            bmp.Save(fileName);
        }

        private Bitmap BuildImageMap()
        {
            var points = Track.Points;
            var maxX = points.Max(p => p.Point.X + p.Width);
            var maxY = points.Max(p => p.Point.Y + p.Width);

            var scale = 4;
            var padding = 20 * scale;

            var bmp = new Bitmap((int) maxX * scale + padding, (int) maxY * scale + padding);

            using (var gr = Graphics.FromImage(bmp))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.ScaleTransform(scale, scale);

                var center = new List<PointF>();
                var leftWall = new List<PointF>();
                var rightWall = new List<PointF>();

                //draw road surface
                for (var i = 0; i < points.Count; i++)
                {
                    var p = points[i];
                    var ppp = new PointF[] {p.LeftWall.From, p.LeftWall.To, p.RightWall.To, p.RightWall.From};
                    gr.FillPolygon(Brushes.Red, ppp);
                    gr.DrawPolygon(Pens.Red, ppp);
                    leftWall.Add(p.LeftWall.From);
                    rightWall.Add(p.RightWall.From);
                    center.Add(p.Point);
                }

                center.Add(points[0].Point);
                leftWall.Add(points[0].LeftWall.From);
                rightWall.Add(points[0].RightWall.From);

                //draw center line
                using (var pen = new Pen(Color.Lime, 0.5f) {DashCap = DashCap.Round, DashPattern = new[] {14.0F, 6.0F}})
                {
                    gr.DrawLines(pen, center.ToArray());
                }

                //draw walls
                using (var pen = new Pen(Color.Blue, 0.8f))
                {
                    gr.DrawLines(pen, leftWall.ToArray());
                    gr.DrawLines(pen, rightWall.ToArray());
                }
            }

            return bmp;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            var dlg = new SaveFileDialog {Filter = "Track|*.xml"};
            if (dlg.ShowDialog() == DialogResult.OK)
                SaveTrack(dlg.FileName);
        }

        private void btNew_Click(object sender, EventArgs e)
        {
            Track = new Track();
            DrawModeOn();
            Invalidate(true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            if (btDraw.Checked)
            {
                RemoveTempPoints();
                var mouse = new ControlPoint(PointToClient(MousePosition), PathWidth) {Temp = true};
                Track.ControlPoints.Add(mouse);
            }

            TrackHelper.BuildPathPoints(Track);
            Track.Prepare();

            lbTrackLength.Text = string.Format("Track Length: {0:0.0}m", Track.Length);

            DrawTrack(e.Graphics);

            //
            if (selectedPoint != null)
                e.Graphics.FillRectangle(Brushes.LimeGreen, selectedPoint.Point.X - 3, selectedPoint.Point.Y - 3, 7, 7);
        }

        private void DrawTrack(Graphics gr)
        {
            if (Track.Points.Count > 1)
            {
                var needDrawLastWalls = false;

                if (Track.ControlPoints.Count > 3)
                {
                    var first = Track.ControlPoints[0];
                    var last = Track.ControlPoints[Track.ControlPoints.Count - 1];
                    if (first.Point.DistanceTo(last.Point) < 100)
                        needDrawLastWalls = true;
                }

                var points = Track.Points;
                for (var i = 0; i < points.Count; i++)
                {
                    if (i >= points.Count - 2 && !needDrawLastWalls)
                        continue;
                    var p = points[i];
                    gr.FillPolygon(Brushes.Blue,
                        new PointF[] {p.LeftWall.From, p.LeftWall.To, p.RightWall.To, p.RightWall.From});
                }

                for (var i = 0; i < points.Count; i++)
                {
                    var p = points[i];
                    gr.FillEllipse(Brushes.Tomato, p.Point.X - 1, p.Point.Y - 1, 3, 3);

                    if (i >= points.Count - 2 && !needDrawLastWalls)
                        continue;

                    gr.DrawLine(Pens.Red, p.LeftWall.From, p.RightWall.From);
                    gr.DrawLine(Pens.Navy, p.LeftWall.From, p.LeftWall.To);
                    gr.DrawLine(Pens.Navy, p.RightWall.From, p.RightWall.To);
                }

                for (var i = 0; i < Track.ControlPoints.Count; i++)
                {
                    var p = Track.ControlPoints[i].Point;
                    gr.FillEllipse(Brushes.Lime, p.X - 1, p.Y - 1, 3, 3);
                }
            }
        }

        private void RemoveTempPoints()
        {
            for (var i = Track.ControlPoints.Count - 1; i >= 0; i--)
                if (Track.ControlPoints[i].Temp)
                    Track.ControlPoints.RemoveAt(i);
        }

        private void TrackEditor_MouseMove(object sender, MouseEventArgs e)
        {
            //find point
            var mouse = e.Location;
            var best = Track.ControlPoints.OrderBy(cp => cp.Point.DistanceSquareTo(mouse)).FirstOrDefault();
            if (best != null && best.Point.DistanceSquareTo(mouse) > 100)
                best = null;

            if (btEditTrack.Checked)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (draggedPoint == null)
                        draggedPoint = best;

                    if (draggedPoint != null)
                    {
                        draggedPoint.Point = e.Location;
                        selectedPoint = draggedPoint;
                        UpdateInterface();
                    }
                }
                else
                {
                    draggedPoint = null;
                }

                if (best != null)
                    Cursor = Cursors.Hand;
                else
                    Cursor = Cursors.Default;

                Invalidate(true);
                return;
            }
            selectedPoint = null;

            Cursor = Cursors.Default;
            Invalidate(true);
        }

        private void TrackEditor_MouseDown(object sender, MouseEventArgs e)
        {
            //find point
            var mouse = e.Location;
            var best = Track.ControlPoints.OrderBy(cp => cp.Point.DistanceSquareTo(mouse)).FirstOrDefault();
            if (best != null && best.Point.DistanceSquareTo(mouse) > 100)
                best = null;

            if (btEditTrack.Checked)
                if (e.Button == MouseButtons.Left)
                {
                    SelectPoint(best);
                    UpdateInterface();
                    return;
                }

            if (!btDraw.Checked)
                return;

            if (e.Button == MouseButtons.Left && Track.ControlPoints.Count > 0)
            {
                Track.ControlPoints[Track.ControlPoints.Count - 1].Temp = false;
                UpdateInterface();
                Invalidate(true);
            }
        }

        private void SelectPoint(ControlPoint best)
        {
            selectedPoint = best;
            if (best != null)
                nudTrackWidth.Value = (decimal) best.Width;
        }

        private void EditModeOff()
        {
            btEditTrack.Checked = false;
            draggedPoint = null;
            selectedPoint = null;
            UpdateInterface();
        }

        private void DrawModeOff()
        {
            btDraw.Checked = false;
            RemoveTempPoints();
            UpdateInterface();
        }

        private void DrawModeOn()
        {
            btDraw.Checked = true;
            selectedPoint = draggedPoint = null;
            UpdateInterface();
        }

        private void UpdateInterface()
        {
            btDraw.BackColor = btDraw.Checked ? Color.Orange : SystemColors.Control;
            btEditTrack.BackColor = btEditTrack.Checked ? Color.Orange : SystemColors.Control;
            btDeletePoint.Enabled = (btDraw.Checked || btEditTrack.Checked) && Track.ControlPoints.Count > 1;
            Invalidate();
        }

        private void btDraw_Click(object sender, EventArgs e)
        {
            if (btEditTrack.Checked)
                EditModeOff();

            if (btDraw.Checked)
                DrawModeOff();
            else
                DrawModeOn();
        }

        private void nudTrackWidth_ValueChanged(object sender, EventArgs e)
        {
            if (btEditTrack.Checked && selectedPoint != null)
            {
                selectedPoint.Width = (float) nudTrackWidth.Value;
                Invalidate();
            }
        }

        private void btDeletePoint_Click(object sender, EventArgs e)
        {
            if (btDraw.Checked)
            {
                if (Track.ControlPoints.Count > 0 && Track.ControlPoints[Track.ControlPoints.Count - 1].Temp)
                    Track.ControlPoints.RemoveAt(Track.ControlPoints.Count - 1);
                if (Track.ControlPoints.Count > 0)
                    Track.ControlPoints.RemoveAt(Track.ControlPoints.Count - 1);
            }
            if (btEditTrack.Checked)
                if (selectedPoint != null)
                {
                    Track.ControlPoints.Remove(selectedPoint);
                    selectedPoint = draggedPoint = null;
                }
            UpdateInterface();
        }

        private void btEditTrack_Click(object sender, EventArgs e)
        {
            if (btDraw.Checked)
                DrawModeOff();
            btEditTrack.Checked = !btEditTrack.Checked;
            UpdateInterface();
        }

        private void btInsertPoint_Click(object sender, EventArgs e)
        {
            if (selectedPoint != null)
            {
                var index = Track.ControlPoints.IndexOf(selectedPoint);
                var cp = new ControlPoint(selectedPoint.Point.Add(new PointF(10, 0)), selectedPoint.Width)
                {
                    Temp = false
                };
                Track.ControlPoints.Insert(index + 1, cp);
                selectedPoint = cp;
                UpdateInterface();
            }
        }

        private void nudTension_ValueChanged(object sender, EventArgs e)
        {
            TrackHelper.Tension = (float) nudTension.Value;
            UpdateInterface();
        }

        private void paddingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var cp in Track.ControlPoints)
                cp.Point = cp.Point + new Vector2(23, -15);
        }

        private void nudAdhesion_ValueChanged(object sender, EventArgs e)
        {
            Track.Adhesion = (float) nudAdhesion.Value;
        }
    }

    internal static class TrackHelper
    {
        public static float Tension = 0.3f;
        public static float Flatness = 1f;

        public static GraphicsPath BuildPathPoints(Track track)
        {
            var path = new GraphicsPath();

            if (track.ControlPoints.Count < 2)
                return path;

            var points = track.ControlPoints.Select(cp => cp.Point.ToPointF()).ToArray();

            path.AddCurve(points, Tension);
            path.Flatten(new Matrix(), Flatness);
            points = path.PathPoints;

            track.Points.Clear();
            for (var i = 0; i < points.Length; i++)
            {
                var p = points[i];
                track.Points.Add(new PathPoint {Point = p, Width = 1});
            }

            return path;
        }
    }
}