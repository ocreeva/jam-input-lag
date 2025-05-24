using System.Collections;
using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Game
{
    public class GameLevelStart : ATrait<GameManager>, IGameLevelStart
    {
        private static readonly _StubGameLevelStart _Stub = new _StubGameLevelStart();

        internal static IGameLevelStart Stub => _Stub;

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.LevelStart, _Stub), "is replacing a non-stub instance.");
            _manager.LevelStart = this;
            _Stub.TransferControlTo(this);
        }

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.LevelStart, this), "is stubbing a different instance.");
            _manager.LevelStart = _Stub;
            _Stub.TransferControlFrom(this);
        }

        private IEnumerator Start()
        {
            Omnibus.UI.Show(Moyba.UI.Overlay.ScreenFade, immediately: true);

            yield return null;

            Omnibus.UI.Hide(Moyba.UI.Overlay.ScreenFade);

            yield return new WaitForSeconds(0.8f);

            Omnibus.Camera.Appendix.TransitionToGameCamera();

            yield return new WaitForSeconds(0.6f);

            Omnibus.Avatar.Kinematics.StandUp();

            yield return new WaitForSeconds(0.6f);

            Omnibus.Input.Avatar.enabled = true;
            Omnibus.Game.Timer.enabled = true;
        }

        private class _StubGameLevelStart : ATraitStub<GameLevelStart>, IGameLevelStart { }
    }
}
