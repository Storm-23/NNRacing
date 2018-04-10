using System;

namespace System
{
    public struct Segment
    {
        public Vector2 From;
        public Vector2 To;

        public Segment(Vector2 from, Vector2 to)
        {
            this.From = from;
            this.To = to;
        }
    }
}