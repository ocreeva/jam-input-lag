using System;
using UnityEngine;

namespace Moyba
{
    [Serializable]
    public struct Coordinate : IEquatable<Coordinate>
    {
        private const float _ScaleH = 2.0f;
        private const float _ScaleV = 1.0f;

        public int x, y, z;

        public Coordinate(int x, int y, int z) => (this.x, this.y, this.z) = (x, y, z);
        public Coordinate(Vector3 worldPosition) : this(
            Mathf.RoundToInt(worldPosition.x / _ScaleH),
            Mathf.RoundToInt(worldPosition.z / _ScaleH),
            Mathf.RoundToInt(worldPosition.y / _ScaleV)
        )
        { }

        public static bool operator ==(Coordinate c1, Coordinate c2)
        => (c1.x == c2.x) && (c1.y == c2.y) && (c1.z == c2.z);

        public static bool operator !=(Coordinate c1, Coordinate c2)
        => (c1.x != c2.x) || (c1.y != c2.y) || (c1.z != c2.z);

        public static Coordinate operator +(Coordinate c, CoordinateOffset o)
        => new Coordinate(c.x + o.x, c.y + o.y, c.z + o.z);

        public static Coordinate operator -(Coordinate c, CoordinateOffset o)
        => new Coordinate(c.x - o.x, c.y - o.y, c.z - o.z);

        public bool Equals(Coordinate other) => this == other;
        public override bool Equals(object obj)
        => obj is Coordinate other && this.Equals(other);

        public override int GetHashCode()
        => HashCode.Combine(this.x, this.y, this.z);

        public Vector3 ToWorldPosition()
        => new Vector3(this.x * _ScaleH, this.z * _ScaleV, this.y * _ScaleH);
    }
}
