using System;
using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Game
{
    public class GameTimer : AValueTrait<GameManager, float>, IGameTimer
    {
        private static readonly _StubGameTimer _Stub = new _StubGameTimer();

        [NonSerialized] private bool _isTiming = true;

        internal static IGameTimer Stub => _Stub;

        public void Halt()
        => _isTiming = false;

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.Timer, _Stub), "is replacing a non-stub instance.");
            _manager.Timer = this;
            _Stub.TransferControlTo(this);
        }

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.Timer, this), "is stubbing a different instance.");
            _manager.Timer = _Stub;
            _Stub.TransferControlFrom(this);
        }

        private void Update()
        {
            if (!_isTiming) return;

            this.Value += Time.deltaTime;
        }

        private class _StubGameTimer : AValueTraitStub<GameTimer>, IGameTimer
        {
            public void Halt() { }
        }
    }
}
