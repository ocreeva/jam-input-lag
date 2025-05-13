using System;
using Moyba.Contracts;
using UnityEngine;

namespace Moyba.SFX
{
    [RequireComponent(typeof(AudioSource))]
    public class SFXMusic : ATrait<SFXManager>, ISFXMusic
    {
        private static readonly _StubSFXMusic _Stub = new _StubSFXMusic();

        internal static ISFXMusic Stub => _Stub;

        [NonSerialized] private AudioSource _audioSource;

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.Music, _Stub), "is replacing a non-stub instance.");
            _manager.Music = this;
            _Stub.TransferControlTo(this);

            _audioSource = this.GetComponent<AudioSource>();
        }

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.Music, this), "is stubbing a different instance.");
            _manager.Music = _Stub;
            _Stub.TransferControlFrom(this);
        }

        private void OnDisable()
        {
            _audioSource.Stop();
        }

        private void OnEnable()
        {
            _audioSource.Play();
        }

        private class _StubSFXMusic : ATraitStub<SFXMusic>, ISFXMusic { }
    }
}
