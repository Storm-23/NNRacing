using System;
using System.Collections.Generic;

namespace NNRacingCore
{
    /// <summary>
    /// Car intended to moving on Track
    /// </summary>
    public class Car : CarBase
    {
        public float Penalty;
        private float maxDistFromStart = 0;
        public int Index;

        public virtual PathPoint CapturedPoint
        {
            get { return capturedPoint; }
            set
            {
                capturedPoint = value;
            }
        }

        float TotalCapturedLength;
        public float TotalPassedLength
        {
            get { return TotalCapturedLength + PosOnSegment; }
        }

        public bool IsAlive = true;
        public bool IsOutOfTrack = false;
        public bool IsOnGround = true;

        public Car(int index)
        {
            Index = index;
        }

        private float steeringdump = 0;

        internal void Update(Track track, float throttle, float steering, bool breaks, float dt)
        {
            if (!IsAlive)
                return;

            if (IsOutOfTrack)
            {
                Pos = capturedPoint.Point;
                Velocity *= 0.1f;
                LookAt = capturedPoint.Dir.Normalized();
                IsOutOfTrack = false;
            }

            FindNextCapturedPoint(track);

            base.Update(throttle, steering, breaks, dt);
        }


        public void Reset(Track track, bool placeByIndex)
        {
            if (placeByIndex)
            {
                PathPoint pp;
                var p = track.PathPosToPoint(-Index * 5, out pp);
                LookAt = pp.Dir.Normalized();
                Pos = p + LookAt.Rotate90() * 2 * (Index % 2 == 0 ? 1 : -1);
                CapturedPoint = pp;
            }
            else
            {
                Pos = track.Points[0].Point;
                LookAt = track.Points[0].Dir.Normalized();
                CapturedPoint = track.Points[0];
            }

            TotalCapturedLength = 0;
            Adhesion = track.Adhesion;

            IsAlive = true;
            IsOutOfTrack = false;
            IsOnGround = true;
            Penalty = 0;
        }

        private float PosOnSegment = 0f;
        private PathPoint capturedPoint;

        void FindNextCapturedPoint(Track track)
        {
            //check side
            if (CapturedPoint.Next.ContainsPoint(Pos))
            {
                if (CapturedPoint.Next.Point.DistanceSquareTo(Pos) <= CapturedPoint.Next.r2)
                {
                    var dist = track.DistanceBetweenPoints(CapturedPoint, CapturedPoint.Next);
                    //we captured next checkpoint
                    CapturedPoint = CapturedPoint.Next;
                    TotalCapturedLength += dist;
                    PosOnSegment = 0;
                }
            }

            //check out of track
            var d = CapturedPoint.Dir * (1 / CapturedPoint.Length);
            var p = Pos - CapturedPoint.Point;
            var proj = p.Projection(d);
            var onSegment = proj.DotScalar(d) >= 0;
            if (onSegment)
            {
                var l = proj.Length();
                PosOnSegment = l;
                if (l > CapturedPoint.Length)
                    onSegment = false;
                l = proj.DistanceSquareTo(p);
                if (l > CapturedPoint.r2)
                    onSegment = false;
            }

            if (!onSegment)
                if (CapturedPoint.Point.DistanceSquareTo(Pos) <= CapturedPoint.r2 ||
                    CapturedPoint.Next.Point.DistanceSquareTo(Pos) <= CapturedPoint.Next.r2)
                    onSegment = true;

            IsOutOfTrack = !onSegment;
        }
    }
}