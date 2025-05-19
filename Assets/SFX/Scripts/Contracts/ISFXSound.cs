namespace Moyba.SFX
{
    public interface ISFXSound
    {
        SoundClip SoundClip { get; }

        void Play();
    }
}
