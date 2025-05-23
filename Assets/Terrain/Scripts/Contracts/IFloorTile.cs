namespace Moyba.Terrain
{
    public interface IFloorTile
    {
        Coordinate Coordinate { get; }
        float Height { get; }
        bool IsDangerous { get; }
    }
}
