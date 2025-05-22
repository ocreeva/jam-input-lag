using System;
using UnityEngine;

namespace Moyba
{
    [Serializable]
    public struct Coordinate : IEquatable<Coordinate>
    {
        private const float _Scale = 2.0f;

        public int x, y;

        public Coordinate(int x, int y) => (this.x, this.y) = (x, y);
        public Coordinate(Vector3 worldPosition) : this(
            Mathf.RoundToInt(worldPosition.x / _Scale),
            Mathf.RoundToInt(worldPosition.z / _Scale)) { }

        public static Coordinate Zero => new Coordinate(0, 0);

        public static bool operator ==(Coordinate c1, Coordinate c2)
        => (c1.x == c2.x) && (c1.y == c2.y);

        public static bool operator !=(Coordinate c1, Coordinate c2)
        => (c1.x != c2.x) || (c1.y != c2.y);

        public static Coordinate operator +(Coordinate c, CoordinateOffset o)
        => new Coordinate(c.x + o.x, c.y + o.y);

        public static Coordinate operator -(Coordinate c, CoordinateOffset o)
        => new Coordinate(c.x - o.x, c.y - o.y);

        public bool Equals(Coordinate other) => this == other;
        public override bool Equals(object obj)
        => obj is Coordinate other && this.Equals(other);

        public override int GetHashCode()
        => HashCode.Combine(this.x, this.y);

        public override string ToString()
        => $"{this.x:##0;-##0}, {this.y:##0;-##0}";

        public Vector3 ToWorldPosition()
        => new Vector3(this.x * _Scale, 0f, this.y * _Scale);
    }
}
