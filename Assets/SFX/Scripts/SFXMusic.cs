using System;
using Moyba.Contracts;
using UnityEngine;
using UnityEngine.Audio;

namespace Moyba.SFX
{
    public class SFXMusic : ATrait<SFXManager>, ISFXMusic
    {
        private const string _IsMuted = "SFX_Music_IsMuted";

        private static readonly _StubSFXMusic _Stub = new _StubSFXMusic();

        [Header("Components")]
        [SerializeField] private AudioMixerSnapshot _audioSnapshotMuted;
        [SerializeField] private AudioMixerSnapshot _audioSnapshotUnmuted;

        [NonSerialized] private bool _isMuted = true;

        public event SimpleEventHandler OnMuted;
        public event SimpleEventHandler OnUnmuted;

        internal static ISFXMusic Stub => _Stub;

        public bool IsMuted
        {
            get => _isMuted;
            set => _Set(value, ref _isMuted, onFalse: this.OnUnmuted, onTrue: this.OnMuted);
        }

        private void HandleMuted_Persistence()
        => PlayerPrefs.SetInt(_IsMuted, 1);
        private void HandleUnmuted_Persistence()
        => PlayerPrefs.DeleteKey(_IsMuted);

        private void HandleMuted()
        => _audioSnapshotMuted.TransitionTo(0);
        private void HandleUnmuted()
        => _audioSnapshotUnmuted.TransitionTo(0);

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.Music, _Stub), "is replacing a non-stub instance.");
            _manager.Music = this;
            _Stub.TransferControlTo(this);
        }

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.Music, this), "is stubbing a different instance.");
            _manager.Music = _Stub;
            _Stub.TransferControlFrom(this);
        }

        private void OnDisable()
        {
            this.OnMuted -= this.HandleMuted_Persistence;
            this.OnUnmuted -= this.HandleUnmuted_Persistence;

            this.IsMuted = true;

            this.OnMuted -= this.HandleMuted;
            this.OnUnmuted -= this.HandleUnmuted;
        }

        private void OnEnable()
        {
            this.OnMuted += this.HandleMuted;
            this.OnUnmuted += this.HandleUnmuted;

            this.IsMuted = PlayerPrefs.GetInt(_IsMuted, default) != default;

            this.OnMuted += this.HandleMuted_Persistence;
            this.OnUnmuted += this.HandleUnmuted_Persistence;
        }

        private void Start()
        {
            // first OnEnable transition may be lost if audio mixer initializes later than this component 
            (this.IsMuted ? _audioSnapshotMuted : _audioSnapshotUnmuted).TransitionTo(0);
        }

        private class _StubSFXMusic : ATraitStub<SFXMusic>, ISFXMusic
        {
            public event SimpleEventHandler OnMuted;
            public event SimpleEventHandler OnUnmuted;

            public bool IsMuted
            {
                get => true;
                set => _SetFail(nameof(IsMuted));
            }

            protected override void TransferEvents(SFXMusic trait)
            {
                (this.OnMuted, trait.OnMuted) = (trait.OnMuted, this.OnMuted);
                (this.OnUnmuted, trait.OnUnmuted) = (trait.OnUnmuted, this.OnUnmuted);
            }
        }
    }
}
