using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Terrain
{
    public partial class FloorTile : AnEntity<TerrainManager, FloorTile>, IFloorTile
    {
        [Header("Configuration")]
        [SerializeField] private bool _isDangerous;
        [SerializeField] private bool _isTarget;

        public Coordinate Coordinate { get; private set; }
        public float Height { get; private set; }
        public bool IsDangerous => _isDangerous;
        public bool IsTarget => _isTarget;

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
