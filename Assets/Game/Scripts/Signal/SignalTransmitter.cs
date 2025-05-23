using Moyba.Contracts;
using Moyba.Game.Signal;
using UnityEngine;

namespace Moyba.Game
{
    [RequireComponent(typeof(SignalTransmitterIsTransmitting))]
    public class SignalTransmitter : AnEntity<GameManager, SignalTransmitter>, ISignalTransmitter
    {
        public SignalTransmitterIsTransmitting IsTransmitting { get; private set; }
        IReadOnlyBoolValue<ISignalTransmitter> ISignalTransmitter.IsTransmitting => this.IsTransmitting;

        public Vector3 Position => this.transform.position;

        void Awake()
        {
            this.IsTransmitting = this.GetComponent<SignalTransmitterIsTransmitting>();
        }

        private void OnDisable()
        {
            _manager.Deregister(this);
        }

        private void OnEnable()
        {
            _manager.Register(this);
        }
    }
}
