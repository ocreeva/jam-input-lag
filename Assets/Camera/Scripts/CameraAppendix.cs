using Moyba.Contracts;
using Unity.Cinemachine;
using UnityEngine;

namespace Moyba.Camera
{
    public class CameraAppendix : ATrait<CameraManager>, ICameraAppendix
    {
        private static readonly _StubCameraAppendix _Stub = new _StubCameraAppendix();

        [Header("Components")]
        [SerializeField] private CinemachineCamera _avatarCamera;
        [SerializeField] private CinemachineCamera _gameCamera;

        internal static ICameraAppendix Stub => _Stub;

        public void TransitionToAvatarCamera()
        {
            _avatarCamera.Priority = 2;
        }

        public void TransitionToGameCamera()
        {
            _avatarCamera.Priority = 0;
        }

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.Appendix, _Stub), "is replacing a non-stub instance.");
            _manager.Appendix = this;
            _Stub.TransferControlTo(this);
        }

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.Appendix, this), "is stubbing a different instance.");
            _manager.Appendix = _Stub;
            _Stub.TransferControlFrom(this);
        }

        private class _StubCameraAppendix : ATraitStub<CameraAppendix>, ICameraAppendix
        {
            public void TransitionToAvatarCamera() { }
            public void TransitionToGameCamera() { }
        }
    }
}
