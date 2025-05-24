using System.Collections.Generic;

namespace Moyba.Terrain
{
    public interface ITerrainManager
    {
        IFloorTile GetFloorTile(Coordinate coordinate);
        bool TryGetFloorTile(Coordinate coordinate, out IFloorTile floorTile);
    }
}
