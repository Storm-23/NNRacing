using System;

namespace System
{
    public struct Vector2
    {
        public float X;
        public float Y;

        public Vector2(float x = 0, float y = 0)
        {
            X = x;
            Y = y;
        }

        public void Init(float x, float y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return string.Format("{0:0.00};{1:0.00}", X, Y);
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2() { X = v1.X + v2.X, Y = v1.Y + v2.Y };
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2() { X = v1.X - v2.X, Y = v1.Y - v2.Y };
        }

        public static Vector2 operator *(Vector2 v1, float k)
        {
            return new Vector2() { X = v1.X * k, Y = v1.Y * k };
        }

        public static Vector2 operator *(float k, Vector2 v1)
        {
            return new Vector2() { X = v1.X * k, Y = v1.Y * k };
        }

        public static Vector2 FromAngle(float r, float angle)
        {
            return new Vector2(r * (float)Math.Cos(angle), r * (float)Math.Sin(angle));
        }


        public static implicit operator System.Drawing.PointF(Vector2 v)
        {
            return new System.Drawing.PointF(v.X, v.Y);
        }

        public static implicit operator Vector2(System.Drawing.PointF p)
        {
            return new Vector2(p.X, p.Y);
        }

        public static implicit operator Vector2(System.Drawing.Point p)
        {
            return new Vector2(p.X, p.Y);
        }
    }
}
