using System;

namespace Moyba
{
    [Serializable]
    public struct CoordinateOffset : IEquatable<CoordinateOffset>
    {
        public int x, y, z;

        public CoordinateOffset(int x, int y, int z = 0) => (this.x, this.y, this.z) = (x, y, z);

        public CoordinateOffset right => new CoordinateOffset(this.y, -this.x, this.z);

        public static bool operator ==(CoordinateOffset o1, CoordinateOffset o2)
        => (o1.x == o2.x) && (o1.y == o2.y) && (o1.z == o2.z);

        public static bool operator !=(CoordinateOffset o1, CoordinateOffset o2)
        => (o1.x != o2.x) || (o1.y != o2.y) || (o1.z != o2.z);

        public static CoordinateOffset operator +(CoordinateOffset o1, CoordinateOffset o2)
        => new CoordinateOffset(o1.x + o2.x, o1.y + o2.y, o1.z + o2.z);

        public static CoordinateOffset operator -(CoordinateOffset o1, CoordinateOffset o2)
        => new CoordinateOffset(o1.x - o2.x, o1.y - o2.y, o1.z - o2.z);

        public static CoordinateOffset operator *(CoordinateOffset o, int m)
        => new CoordinateOffset(o.x * m, o.y * m, o.z * m);

        public bool Equals(CoordinateOffset other) => this == other;
        public override bool Equals(object obj)
        => obj is CoordinateOffset other && this.Equals(other);

        public override int GetHashCode()
        => HashCode.Combine(this.x, this.y, this.z);

        public override string ToString()
        => $"{this.x:+##0;-##0}, {this.y:+##0,-##0}, {this.z:+##0,-##0}";
    }
}
