using System;

namespace Moyba
{
    [Serializable]
    public struct CoordinateOffset : IEquatable<CoordinateOffset>
    {
        public int x, y;

        public CoordinateOffset(int x, int y) => (this.x, this.y) = (x, y);

        public static CoordinateOffset Forward => new CoordinateOffset(0, 1);
        public static CoordinateOffset Backward => new CoordinateOffset(0, -1);
        public static CoordinateOffset Right => new CoordinateOffset(1, 0);
        public static CoordinateOffset Left => new CoordinateOffset(-1, 0);

        public CoordinateOffset right => new CoordinateOffset(this.y, -this.x);

        public static bool operator ==(CoordinateOffset o1, CoordinateOffset o2)
        => (o1.x == o2.x) && (o1.y == o2.y);

        public static bool operator !=(CoordinateOffset o1, CoordinateOffset o2)
        => (o1.x != o2.x) || (o1.y != o2.y);

        public static CoordinateOffset operator +(CoordinateOffset o1, CoordinateOffset o2)
        => new CoordinateOffset(o1.x + o2.x, o1.y + o2.y);

        public static CoordinateOffset operator -(CoordinateOffset o1, CoordinateOffset o2)
        => new CoordinateOffset(o1.x - o2.x, o1.y - o2.y);

        public static CoordinateOffset operator *(CoordinateOffset o, int m)
        => new CoordinateOffset(o.x * m, o.y * m);

        public bool Equals(CoordinateOffset other) => this == other;
        public override bool Equals(object obj)
        => obj is CoordinateOffset other && this.Equals(other);

        public override int GetHashCode()
        => HashCode.Combine(this.x, this.y);

        public override string ToString()
        => $"{this.x:+##0;-##0}, {this.y:+##0;-##0}";
    }
}
