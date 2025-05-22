using System.Collections;
using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Avatar
{
    public class AvatarIsOutOfBounds : ABoolValueTrait<AvatarManager>
    {
        private static readonly _StubAvatarIsOutOfBounds _Stub = new _StubAvatarIsOutOfBounds();

        [Header("Configuration")]
        [SerializeField, Range(0f, 5f)] private float _timeToTeleport = 0f;
        [SerializeField, Range(0f, 100f)] private float _teleportHeight = 1f;

        internal static IBoolValue Stub => _Stub;

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.IsOutOfBounds, _Stub), "is replacing a non-stub instance.");
            _manager.IsOutOfBounds = this;
            _Stub.TransferControlTo(this);
        }

        private void HandleAvatarOutOfBounds(bool isOutOfBounds)
        {
            if (!isOutOfBounds) return;

            this.StartCoroutine(Coroutine_OutOfBounds());
        }

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.IsOutOfBounds, this), "is stubbing a different instance.");
            _manager.IsOutOfBounds = _Stub;
            _Stub.TransferControlFrom(this);
        }

        private void OnDisable()
        {
            this.OnChanged -= this.HandleAvatarOutOfBounds;
        }

        private void OnEnable()
        {
            this.OnChanged += this.HandleAvatarOutOfBounds;
        }

        private IEnumerator Coroutine_OutOfBounds()
        {
            yield return new WaitForSeconds(_timeToTeleport);

            var targetCoordinate = _manager.Coordinate.LastGroundedValue;
            var targetPosition = targetCoordinate.ToWorldPosition();

            var floorTile = Omnibus.Terrain.GetFloorTile(_manager.Coordinate.LastGroundedValue);
            targetPosition.y = floorTile.Height + _teleportHeight;

            _manager.Kinematics.TeleportTo(targetPosition, shouldResetVelocity: true);
        }

        private class _StubAvatarIsOutOfBounds : ABoolValueTraitStub<AvatarIsOutOfBounds> { }
    }
}
