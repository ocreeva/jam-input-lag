using System;
using Moyba.Contracts;
using Unity.Cinemachine;
using UnityEngine;

namespace Moyba.Camera
{
    [RequireComponent(typeof(CinemachineCamera))]
    public class OutOfBoundsCamera : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private Vector3 _offset;

        [NonSerialized] private CinemachineCamera _camera;

        private void Awake()
        {
            _camera = this.GetComponent<CinemachineCamera>();
        }

        private void HandleAvatarGrounded()
        {
            _camera.Priority = 0;
        }

        private void HandleAvatarOutOfBounds()
        {
            var targetCoordinate = Omnibus.Avatar.Coordinate.LastGroundedValue;
            var targetPosition = targetCoordinate.ToWorldPosition();

            var floorTile = Omnibus.Terrain.GetFloorTile(targetCoordinate);
            targetPosition.y = floorTile.Height;

            this.transform.position = targetPosition + _offset;

            _camera.Priority = 2;
        }

        private void OnDisable()
        {
            Omnibus.Avatar.IsGrounded.OnTrue -= this.HandleAvatarGrounded;
            Omnibus.Avatar.IsOutOfBounds.OnTrue -= this.HandleAvatarOutOfBounds;
        }

        private void OnEnable()
        {
            Omnibus.Avatar.IsGrounded.OnTrue += this.HandleAvatarGrounded;
            Omnibus.Avatar.IsOutOfBounds.OnTrue += this.HandleAvatarOutOfBounds;
        }
    }
}
