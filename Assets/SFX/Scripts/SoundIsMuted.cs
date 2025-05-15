using Moyba.Contracts;
using UnityEngine;

namespace Moyba.SFX
{
    public class SoundIsMuted : AChannelIsMuted
    {
        private static readonly _StubChannelIsMuted _Stub = new _StubChannelIsMuted();

        internal static IBoolValue Stub => _Stub;

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.SoundIsMuted, _Stub), "is replacing a non-stub instance.");
            _manager.SoundIsMuted = this;
            _Stub.TransferControlTo(this);
        }

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.SoundIsMuted, this), "is stubbing a different instance.");
            _manager.SoundIsMuted = _Stub;
            _Stub.TransferControlFrom(this);
        }
    }
}
