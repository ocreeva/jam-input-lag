using Moyba.Contracts;

namespace Moyba.SFX
{
    public interface ISFXManager
    {
        IBoolValue MusicIsMuted { get; }
        IBoolValue SoundIsMuted { get; }
        ISFXVolume Volume { get; }

        void Play(SoundClip soundClip);
    }
}
