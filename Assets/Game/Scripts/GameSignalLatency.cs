using System;
using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Game
{
    public class GameSignalLatency : AValueTrait<GameManager, float>
    {
        private static readonly _StubGameSignalLatency _Stub = new _StubGameSignalLatency();

        [NonSerialized] private GameConfiguration.SignalConfiguration _settings;

        internal static IValue<float> Stub => _Stub;

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.SignalLatency, _Stub), "is replacing a non-stub instance.");
            _manager.SignalLatency = this;
            _Stub.TransferControlTo(this);
        }

        private void HandleDifficultyChanged(Difficulty _)
        => _settings = _manager.Configuration.Value.Signal;

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.SignalLatency, this), "is stubbing a different instance.");
            _manager.SignalLatency = _Stub;
            _Stub.TransferControlFrom(this);
        }

        private void OnDisable()
        {
            Omnibus.Game.Difficulty.OnChanged -= this.HandleDifficultyChanged;
        }

        private void OnEnable()
        {
            Omnibus.Game.Difficulty.OnChanged += this.HandleDifficultyChanged;

            // cache the settings to avoid a lookup every frame
            _settings = _manager.Configuration.Value.Signal;
        }

        private void Update()
        {
            // for the moment, for simplicity, we'll assume there's one wireless transmitter at the origin point of the
            // scene; a magnitude calculation isn't ideal, but this game isn't intensive enough for it to matter
            this.Value = _settings.GetLatencyAt(Omnibus.Avatar.Kinematics.Position.magnitude);
        }

        private class _StubGameSignalLatency : AValueTraitStub<GameSignalLatency> { }
    }
}
