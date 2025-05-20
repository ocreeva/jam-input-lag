using System;
using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Game
{
    public class GameTimer : AValueTrait<GameManager, float>
    {
        private static readonly _StubGameTimer _Stub = new _StubGameTimer();

        [NonSerialized] private GameConfiguration.TimerConfiguration _configuration;

        internal static IValue<float> Stub => _Stub;

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.Timer, _Stub), "is replacing a non-stub instance.");
            _manager.Timer = this;
            _Stub.TransferControlTo(this);
        }

        private void HandleDifficultyChanged(Difficulty _)
        => _configuration = _manager.Configuration.Value.Timer;

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.Timer, this), "is stubbing a different instance.");
            _manager.Timer = _Stub;
            _Stub.TransferControlFrom(this);
        }

        private void OnDisable()
        {
            Omnibus.Game.Difficulty.OnChanged -= this.HandleDifficultyChanged;
        }

        private void OnEnable()
        {
            Omnibus.Game.Difficulty.OnChanged += this.HandleDifficultyChanged;
            this.HandleDifficultyChanged(Omnibus.Game.Difficulty.Value);

            this.Value = _configuration.InitialDuration;
        }

        private void Update()
        {
            this.Value -= Time.deltaTime;
        }

        private class _StubGameTimer : AValueTraitStub<GameTimer> { }
    }
}
