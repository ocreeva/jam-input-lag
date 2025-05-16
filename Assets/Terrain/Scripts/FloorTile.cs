using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Terrain
{
    public partial class FloorTile : AnEntity<TerrainManager>, IFloorTile
    {
        public Coordinate Coordinate { get; private set; }

        private void Awake()
        {
            this.Coordinate = new Coordinate(this.transform.position);
        }

        private void OnDisable()
        {
            _manager.Deregister(this);
        }

        private void OnEnable()
        {
            _manager.Register(this);
        }
    }
}
