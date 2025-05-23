using Moyba.Contracts;

namespace Moyba.Terrain
{
    public partial class FloorTile : AnEntity<TerrainManager, FloorTile>, IFloorTile
    {
        public Coordinate Coordinate { get; private set; }
        public float Height { get; private set; }

        private void Awake()
        {
            this.Coordinate = new Coordinate(this.transform.position);
            this.Height = this.transform.position.y;
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
