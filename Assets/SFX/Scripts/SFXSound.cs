using System;
using Moyba.Contracts;
using UnityEngine;

namespace Moyba.SFX
{
    [RequireComponent(typeof(AudioSource))]
    public class SFXSound : AnEntity<SFXManager>, ISFXSound
    {
        [Header("Configuration")]
        [SerializeField] private SoundClip _soundClip;
        [SerializeField, Range(0f, 10f)] private float _startTimeOffset;

        [NonSerialized] private AudioSource _audioSource;

        public SoundClip SoundClip => _soundClip;

        public void Play()
        {
            _audioSource.time = _startTimeOffset;
            _audioSource.Play();
        }

        private void Awake()
        {
            _audioSource = this.GetComponent<AudioSource>();
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
