using Moyba.Contracts;
using UnityEngine;
using UnityEngine.Audio;

namespace Moyba.SFX
{
    public class AChannelIsMuted : ABoolValueTrait<SFXManager>
    {
        private readonly string _PlayerPrefsKey;

        [Header("Components")]
        [SerializeField] private AudioMixerSnapshot _audioSnapshotMuted;
        [SerializeField] private AudioMixerSnapshot _audioSnapshotUnmuted;

        protected AChannelIsMuted()
        {
            _PlayerPrefsKey = this.GetType().FullName;
        }

        private void HandleMuted_Persistence()
        => PlayerPrefs.SetInt(_PlayerPrefsKey, 1);
        private void HandleMuted()
        => _audioSnapshotMuted.TransitionTo(0);

        private void HandleUnmuted_Persistence()
        => PlayerPrefs.DeleteKey(_PlayerPrefsKey);
        private void HandleUnmuted()
        => _audioSnapshotUnmuted.TransitionTo(0);

        private void OnDisable()
        {
            this.OnTrue -= this.HandleMuted_Persistence;
            this.OnFalse -= this.HandleUnmuted_Persistence;

            this.Value = default;

            this.OnTrue -= this.HandleMuted;
            this.OnFalse -= this.HandleUnmuted;
        }

        private void OnEnable()
        {
            this.OnTrue += this.HandleMuted;
            this.OnFalse += this.HandleUnmuted;

            this.Value = PlayerPrefs.GetInt(_PlayerPrefsKey, default) != default;

            this.OnTrue += this.HandleMuted_Persistence;
            this.OnFalse += this.HandleUnmuted_Persistence;
        }

        private void Start()
        {
            // first OnEnable transition may be lost if audio mixer initializes later than this component 
            (this.Value ? _audioSnapshotMuted : _audioSnapshotUnmuted).TransitionTo(0);
        }

        protected class _StubChannelIsMuted : ABoolValueTraitStub<AChannelIsMuted> { }
    }
}
