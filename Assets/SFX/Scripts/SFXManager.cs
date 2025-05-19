using System.Collections;
using System.Collections.Generic;
using Moyba.Contracts;
using UnityEngine;

namespace Moyba.SFX
{
    [CreateAssetMenu(fileName = "Omnibus.SFX.asset", menuName = "Omnibus/SFX")]
    public class SFXManager : AManager, ISFXManager
    {
        private readonly IDictionary<SoundClip, ISFXSound> _sounds = new Dictionary<SoundClip, ISFXSound>();

        public IBoolValue MusicIsMuted { get; internal set; } = SFX.MusicIsMuted.Stub;
        public IBoolValue SoundIsMuted { get; internal set; } = SFX.SoundIsMuted.Stub;
        public IValue<float> Volume { get; internal set; } = SFXVolume.Stub;

        public void Play(SoundClip soundClip)
        {
            if (!_sounds.TryGetValue(soundClip, out var sound)) return;

            sound.Play();
        }

        internal void Deregister(ISFXSound sound)
        => _sounds.Remove(sound.SoundClip);

        internal void Register(ISFXSound sound)
        => _sounds.Add(sound.SoundClip, sound);
    }
}
