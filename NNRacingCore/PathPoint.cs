using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NNRacingCore
{
    /// <summary>
    /// Point of Track
    /// </summary>
    [Serializable]
    public class PathPoint
    {
        public Vector2 Point; //position in 2D space

        public float Width; //width of road (m)

        [XmlIgnore]
        public PathPoint Prev;//previous Point
        [XmlIgnore]
        public PathPoint Next;//next Point
        [XmlIgnore]
        public float DistanceFromStart;//distance from start to this point along track (m)
        [XmlIgnore]
        public Segment LeftWall; //segment of left wall
        [XmlIgnore]
        public Segment RightWall;//segment of right wall
        [XmlIgnore]
        public Vector2 Normal;//normal to direction of road
        [XmlIgnore]
        public Vector2 Dir; //direction of road
        [XmlIgnore]
        public float Length; //length of this part of track (m)
        [XmlIgnore]
        public ControlPoint ControlPoint;
        [XmlIgnore]
        public float r2;

        public bool ContainsPoint(Vector2 p)
        {
            return Normal.Side(p.Sub(Point)) <= 0;
        }
    }
}