using System;
using System.Collections.Generic;
using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Game
{
    public class GameSignal : ATrait<GameManager>, IGameSignal
    {
        private static readonly _StubGameSignal _Stub = new _StubGameSignal();

        internal static IGameSignal Stub => _Stub;

        private readonly Queue<(float time, Action action)> _queue = new Queue<(float, Action)>();

        public IReadOnlyValue<float> Latency => _manager.SignalLatency;

        public void Send(Action action)
        => _queue.Enqueue((Time.time, action));

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.Signal, _Stub), "is replacing a non-stub instance.");
            _manager.Signal = this;
            _Stub.TransferControlTo(this);
        }

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.Signal, this), "is stubbing a different instance.");
            _manager.Signal = _Stub;
            _Stub.TransferControlFrom(this);
        }

        private void Update()
        {
            if (_queue.Count == 0) return;

            var targetTime = Time.time - _manager.SignalLatency.Value;
            while (_queue.Peek().time <= targetTime)
            {
                _queue.Dequeue().action();
                if (_queue.Count == 0) return;
            }
        }

        private class _StubGameSignal : ATraitStub<GameSignal>, IGameSignal
        {
            public IReadOnlyValue<float> Latency => ((GameManager)Omnibus.Game).SignalLatency;

            public void Send(Action action)
            => action();
        }
    }
}
