using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Avatar
{
    public class AvatarCoordinate : AValueTrait<AvatarManager, Coordinate>, IAvatarCoordinate
    {
        private static readonly _StubAvatarCoordinate _Stub = new _StubAvatarCoordinate();

        internal static IAvatarCoordinate Stub => _Stub;

        public Coordinate LastGroundedValue { get; private set; }

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.Coordinate, _Stub), "is replacing a non-stub instance.");
            _manager.Coordinate = this;
            _Stub.TransferControlTo(this);
        }

        private void HandleCoordinateChanged(Coordinate coordinate)
        => this.UpdateLastGroundedValue(_manager.IsGrounded.Value, coordinate);

        private void HandleGroundedChanged(bool isGrounded)
        => this.UpdateLastGroundedValue(isGrounded, this.Value);

        private void UpdateLastGroundedValue(bool isGrounded, Coordinate coordinate)
        {
            if (!isGrounded) return;

            // make sure the Coordinate has a corresponding floor tile (i.e. not straddling an edge)
            if (!Omnibus.Terrain.TryGetFloorTile(coordinate, out var floorTile)) return;

            // don't retain dangerous tiles (no spawning above a tile with a hole)
            if (floorTile.IsDangerous) return;

            this.LastGroundedValue = coordinate;
        }

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.Coordinate, this), "is stubbing a different instance.");
            _manager.Coordinate = _Stub;
            _Stub.TransferControlFrom(this);
        }

        private void OnDisable()
        {
            this.OnChanged -= this.HandleCoordinateChanged;
            _manager.IsGrounded.OnChanged -= this.HandleGroundedChanged;
        }

        private void OnEnable()
        {
            this.OnChanged += this.HandleCoordinateChanged;
            _manager.IsGrounded.OnChanged += this.HandleGroundedChanged;
        }

        private void Update()
        => this.Value = new Coordinate(this.transform.position);

        private class _StubAvatarCoordinate : AValueTraitStub<AvatarCoordinate>, IAvatarCoordinate
        {
            public Coordinate LastGroundedValue => Coordinate.Zero;
        }
    }
}
