using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Game.Signal
{
    public class SignalTransmitterIsTransmitting : ABoolValueTrait<GameManager, SignalTransmitter, ISignalTransmitter>
    {
        [Header("Configuration")]
        [SerializeField] private Layer _avatarLayer;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer != _avatarLayer) return;

            this.Value = true;
        }
    }
}
