using System;
using System.Collections.Generic;

namespace NNRacingCore
{
    /// <summary>
    /// Provides methods to calculate intersections of sensor rays with segments of wall
    /// </summary>
    public static class Sensors
    {
        public static Vector2[] Rays = new Vector2[5];

        static Sensors()
        {
            var v = new Vector2(100, 0);
            Rays[0] = v.Rotate(-70 * PointFHelper.ToRadians);
            Rays[1] = v.Rotate(-30 * PointFHelper.ToRadians);
            Rays[2] = v.Rotate(+0 * PointFHelper.ToRadians) * 1.2f;
            Rays[3] = v.Rotate(+30 * PointFHelper.ToRadians);
            Rays[4] = v.Rotate(+70 * PointFHelper.ToRadians);
        }

        private static List<Segment> segments = new List<Segment>();

        public static void GetDistances(Car car, Track track, float[] sensors, int startIndex)
        {
            //get segments of walls
            {
                segments.Clear();
                var p = car.CapturedPoint;
                segments.Add(p.LeftWall);
                segments.Add(p.RightWall);
                while (track.DistanceBetweenPoints(car.CapturedPoint, p) <= 130)
                {
                    p = p.Next;
                    segments.Add(p.LeftWall);
                    segments.Add(p.RightWall);
                }
            }
            //enumarate sensors of car
            var angle = car.LookAt.Angle();
            for (int i = 0; i < Rays.Length; i++)
            {
                var p = Rays[i];
                p = p.Rotate(angle);//rotate sensor by car orientation
                var ray = new Segment(car.Pos, car.Pos + p);
                sensors[startIndex + i] = GetDistance(ray, segments);//calc distance to nearest wall segment
            }
        }

        public static float GetDistance(Segment seg, List<Segment> segments)
        {
            var minDist = seg.From.DistanceTo(seg.To);

            for (int i = 0; i < segments.Count; i++)
            {
                var s = segments[i];
                var p = SegmentIntersection.FindIntersection(seg.From, seg.To, s.From, s.To);
                if (p != null)
                {
                    var dist = p.Value.DistanceTo(seg.From);
                    if (dist < minDist)
                        minDist = dist;
                    return minDist;
                }
            }

            return minDist;
        }
    }
}