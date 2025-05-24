using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Game
{
    public class GameLevelComplete : ATrait<GameManager>, IGameLevelComplete
    {
        private static readonly _StubGameLevelComplete _Stub = new _StubGameLevelComplete();

        internal static IGameLevelComplete Stub => _Stub;

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.LevelComplete, _Stub), "is replacing a non-stub instance.");
            _manager.LevelComplete = this;
            _Stub.TransferControlTo(this);
        }

        private void HandleAvatarOnTarget()
        {
            // halt the game timer
            Omnibus.Game.Timer.Halt();

            // disable avatar input
            Omnibus.Input.Avatar.enabled = false;

            // transition the avatar to a victory pose
            Omnibus.Avatar.Kinematics.PoseForVictory();

            // close up for the victory pose
            Omnibus.Camera.Appendix.TransitionToAvatarCamera();
        }

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.LevelComplete, this), "is stubbing a different instance.");
            _manager.LevelComplete = _Stub;
            _Stub.TransferControlFrom(this);
        }

        private void OnDisable()
        {
            Omnibus.Avatar.IsOnTarget.OnTrue -= this.HandleAvatarOnTarget;
        }

        private void OnEnable()
        {
            Omnibus.Avatar.IsOnTarget.OnTrue += this.HandleAvatarOnTarget;
        }

        private class _StubGameLevelComplete : ATraitStub<GameLevelComplete>, IGameLevelComplete { }
    }
}
