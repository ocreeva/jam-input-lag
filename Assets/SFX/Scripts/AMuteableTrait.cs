using System;
using Moyba.Contracts;
using UnityEngine;
using UnityEngine.Audio;

namespace Moyba.SFX
{
    public abstract class AMuteableTrait : ATrait<SFXManager>, IMuteable
    {
        private readonly string _PlayerPrefsKey;

        [Header("Components")]
        [SerializeField] private AudioMixerSnapshot _audioSnapshotMuted;
        [SerializeField] private AudioMixerSnapshot _audioSnapshotUnmuted;

        [NonSerialized] private bool _isMuted = true;

        protected AMuteableTrait()
        {
            _PlayerPrefsKey = this.GetType().Name + "_IsMuted";
        }

        public event SimpleEventHandler OnMuted;
        public event SimpleEventHandler OnUnmuted;

        public bool IsMuted
        {
            get => _isMuted;
            set => _Set(value, ref _isMuted, onFalse: this.OnUnmuted, onTrue: this.OnMuted);
        }

        private void HandleMuted_Persistence()
        => PlayerPrefs.SetInt(_PlayerPrefsKey, 1);
        private void HandleUnmuted_Persistence()
        => PlayerPrefs.DeleteKey(_PlayerPrefsKey);

        private void HandleMuted()
        => _audioSnapshotMuted.TransitionTo(0);
        private void HandleUnmuted()
        => _audioSnapshotUnmuted.TransitionTo(0);

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

            this.IsMuted = PlayerPrefs.GetInt(_PlayerPrefsKey, default) != default;

            this.OnMuted += this.HandleMuted_Persistence;
            this.OnUnmuted += this.HandleUnmuted_Persistence;
        }

        private void Start()
        {
            // first OnEnable transition may be lost if audio mixer initializes later than this component 
            (this.IsMuted ? _audioSnapshotMuted : _audioSnapshotUnmuted).TransitionTo(0);
        }

        protected class _StubMuteableTrait : ATraitStub<AMuteableTrait>, IMuteable
        {
            public event SimpleEventHandler OnMuted;
            public event SimpleEventHandler OnUnmuted;

            public bool IsMuted
            {
                get => true;
                set => _SetFail(nameof(IsMuted));
            }

            protected override void TransferEvents(AMuteableTrait trait)
            {
                (this.OnMuted, trait.OnMuted) = (trait.OnMuted, this.OnMuted);
                (this.OnUnmuted, trait.OnUnmuted) = (trait.OnUnmuted, this.OnUnmuted);
            }
        }
    }
}
