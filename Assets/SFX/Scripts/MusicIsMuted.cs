using Moyba.Contracts;
using UnityEngine;

namespace Moyba.SFX
{
    public class MusicIsMuted : AChannelIsMuted
    {
        private static readonly _StubChannelIsMuted _Stub = new _StubChannelIsMuted();

        internal static IBoolValue Stub => _Stub;

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.MusicIsMuted, _Stub), "is replacing a non-stub instance.");
            _manager.MusicIsMuted = this;
            _Stub.TransferControlTo(this);
        }

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.MusicIsMuted, this), "is stubbing a different instance.");
            _manager.MusicIsMuted = _Stub;
            _Stub.TransferControlFrom(this);
        }
    }
}
