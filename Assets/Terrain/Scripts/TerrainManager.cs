using System.Collections.Generic;
using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Terrain
{
    [CreateAssetMenu(fileName = "Omnibus.Terrain.asset", menuName = "Omnibus/Terrain")]
    public class TerrainManager : AManager, ITerrainManager
    {
        private readonly IDictionary<Coordinate, IFloorTile> _floorTiles = new Dictionary<Coordinate, IFloorTile>();

        public IFloorTile GetFloorTile(Coordinate coordinate)
        => _floorTiles[coordinate];

        public bool TryGetFloorTile(Coordinate coordinate, out IFloorTile floorTile)
        => _floorTiles.TryGetValue(coordinate, out floorTile);

        internal void Deregister(IFloorTile floorTile)
        => _floorTiles.Remove(floorTile.Coordinate);

        internal void Register(IFloorTile floorTile)
        => _floorTiles.Add(floorTile.Coordinate, floorTile);
    }
}
