using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Avatar
{
    public class AvatarIsOnTarget : ABoolValueTrait<AvatarManager>
    {
        private static readonly _StubAvatarIsOnTargetTile _Stub = new _StubAvatarIsOnTargetTile();

        internal static IBoolValue Stub => _Stub;

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.IsOnTarget, _Stub), "is replacing a non-stub instance.");
            _manager.IsOnTarget = this;
            _Stub.TransferControlTo(this);
        }

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.IsOnTarget, this), "is stubbing a different instance.");
            _manager.IsOnTarget = _Stub;
            _Stub.TransferControlFrom(this);
        }

        private void HandleAvatarCoordinateChanged(Coordinate coordinate) => this.HandleValueUpdate(coordinate, _manager.IsGrounded.Value);
        private void HandleAvatarGrounded() => this.HandleValueUpdate(_manager.Coordinate.Value, true);

        private void HandleValueUpdate(Coordinate avatarCoordinate, bool avatarIsGrounded)
        {
            if (!avatarIsGrounded) return;

            var floorTile = Omnibus.Terrain.GetFloorTile(avatarCoordinate);
            this.Value = floorTile.IsTarget;
        }

        private void OnDisable()
        {
            _manager.Coordinate.OnChanged -= this.HandleAvatarCoordinateChanged;
            _manager.IsGrounded.OnTrue -= this.HandleAvatarGrounded;
        }

        private void OnEnable()
        {
            _manager.Coordinate.OnChanged += this.HandleAvatarCoordinateChanged;
            _manager.IsGrounded.OnTrue += this.HandleAvatarGrounded;
        }

        private class _StubAvatarIsOnTargetTile : ABoolValueTraitStub<AvatarIsOnTarget> { }
    }
}
