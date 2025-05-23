using System;
using System.Collections.Generic;
using System.Linq;
using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Game
{
    public class GameSignalLatency : AValueTrait<GameManager, float>
    {
        private static readonly _StubGameSignalLatency _Stub = new _StubGameSignalLatency();

        private readonly HashSet<Vector3> _activeTransmitters = new HashSet<Vector3> { Vector3.zero};

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

        private void HandleTransmitterActivated(ISignalTransmitter transmitter)
        => _activeTransmitters.Add(transmitter.Position);

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.SignalLatency, this), "is stubbing a different instance.");
            _manager.SignalLatency = _Stub;
            _Stub.TransferControlFrom(this);
        }

        private void OnDisable()
        {
            Omnibus.Game.Difficulty.OnChanged -= this.HandleDifficultyChanged;
            _manager.OnTransmitterActivated -= this.HandleTransmitterActivated;
        }

        private void OnEnable()
        {
            Omnibus.Game.Difficulty.OnChanged += this.HandleDifficultyChanged;
            _manager.OnTransmitterActivated += this.HandleTransmitterActivated;

            // cache the settings to avoid a lookup every frame
            _settings = _manager.Configuration.Value.Signal;
        }

        private void Update()
        {
            var avatarPosition = Omnibus.Avatar.Kinematics.Position;
            var minDistanceToTransmitter = _activeTransmitters.Min(position => (position - avatarPosition).magnitude);
            this.Value = _settings.GetLatencyAt(minDistanceToTransmitter);
        }

        private class _StubGameSignalLatency : AValueTraitStub<GameSignalLatency> { }
    }
}
