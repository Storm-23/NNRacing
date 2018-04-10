using System;
using System.Collections.Generic;

namespace NNRacingCore
{
    /// <summary>
    /// Track 
    /// </summary>
    [Serializable]
    public class Track
    {
        public List<ControlPoint> ControlPoints = new List<ControlPoint>();
        public List<PathPoint> Points = new List<PathPoint>();
        public float Length;
        public float Adhesion = 1f;

        public float DistanceBetweenPoints(PathPoint p1, PathPoint p2)
        {
            var d = Math.Abs(p2.DistanceFromStart - p1.DistanceFromStart);
            return Math.Min(d, Math.Abs(d - Length));
        }

        public void Prepare()
        {
            //calc prev/next
            for (int i = 0; i < Points.Count; i++)
            {
                Points[i].Next = Points[(i + 1) % Points.Count];
                Points[i].Prev = Points[(i - 1 + Points.Count) % Points.Count];
            }

            var cpIndex = 0;
            var prevI = 0;
            for (int i = 0; i < Points.Count; i++)
            {
                if (cpIndex < ControlPoints.Count - 1)
                if (ControlPoints[cpIndex + 1].Point.DistanceSquareTo(Points[i].Point) < 0.001f)
                {
                    cpIndex++;
                }

                Points[i].ControlPoint = ControlPoints[cpIndex];
            }

            //calc
            Length = 0f;
            for (int i = 0; i < Points.Count; i++)
            {
                var p = Points[i];
                p.Dir = p.Next.Point.Sub(p.Point);
                p.Length = p.Dir.Length();
                p.Normal = p.Dir.Normalized().Rotate90();
                p.DistanceFromStart = Length;
                Length += p.Length;
            }

            //calc width
            for (int i = 0; i < Points.Count; i++)
            {
                CalcWidth(Points[i]);
            }

            //calc walls
            for (int i = 0; i < Points.Count; i++)
            {
                var p = Points[i];
                var next = p.Next;

                //right
                var p1 = p.Point.Add(p.Normal.Mul(p.Width / 2));
                var p2 = next.Point.Add(next.Normal.Mul(next.Width / 2));
                p.RightWall = new Segment(p1, p2);

                //left
                p1 = p.Point.Add(p.Normal.Mul(-p.Width / 2));
                p2 = next.Point.Add(next.Normal.Mul(-next.Width / 2));
                p.LeftWall = new Segment(p1, p2);
            }
        }

        private void CalcWidth(PathPoint p)
        {
            if (ControlPoints.Count < 3)
            {
                p.Width = p.ControlPoint.Width;
            }
            else
            {
                var myCP = p.ControlPoint;
                var next = p;
                while (next.ControlPoint == myCP)
                    next = next.Next;
                var prev = p;
                while (prev.ControlPoint == myCP)
                    prev = prev.Prev;
                var first = prev.Next;
                var firstDist = first.DistanceFromStart;
                if (firstDist > p.DistanceFromStart)
                    firstDist -= Length;
                var nextDist = next.DistanceFromStart;
                if (nextDist < p.DistanceFromStart)
                    nextDist += Length;
                var k = (p.DistanceFromStart - firstDist) / (nextDist - firstDist);
                p.Width = p.ControlPoint.Width * (1 - k) + next.ControlPoint.Width * k;
            }

            p.r2 = (p.Width / 2) * (p.Width / 2);
        }

        public PathPoint FindPathPointForPoint(Vector2 p)
        {
            var bestDist = float.MaxValue;
            PathPoint res = null;
            foreach (var pp in Points)
            {
                if (pp.ContainsPoint(p))
                {
                    var d = pp.Point.DistanceTo(p);
                    if (d < bestDist)
                    {
                        bestDist = d;
                        res = pp;
                    }
                }
            }

            return res;
        }

        public Vector2 PathPosToPoint(float trackPos, out PathPoint pp)
        {
            while (trackPos < 0)
                trackPos += Length;

            foreach (var p in Points)
            {
                if (trackPos <= p.DistanceFromStart + p.Length)
                {
                    pp = p;
                    var d = (trackPos - p.DistanceFromStart) / p.Length;
                    return p.Point + p.Dir * d;
                }
            }

            pp = Points[0];
            return pp.Point;
        }
    }

    [Serializable]
    public class ControlPoint
    {
        public Vector2 Point;
        public float Width;
        public bool Temp = true;

        public ControlPoint()
        {
        }

        public ControlPoint(Vector2 point, float width)
        {
            this.Point = point;
            this.Width = width;
        }
    }
}