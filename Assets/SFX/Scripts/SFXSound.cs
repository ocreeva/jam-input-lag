using UnityEngine;

namespace Moyba.SFX
{
    public class SFXSound : AMuteableTrait
    {
        private static readonly _StubMuteableTrait _Stub = new _StubMuteableTrait();

        internal static IMuteable Stub => _Stub;

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.Sound, _Stub), "is replacing a non-stub instance.");
            _manager.Sound = this;
            _Stub.TransferControlTo(this);
        }

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.Sound, this), "is stubbing a different instance.");
            _manager.Sound = _Stub;
            _Stub.TransferControlFrom(this);
        }
    }
}
