using Moyba.Contracts;

namespace Moyba.SFX
{
    public interface ISFXManager
    {
        IBoolValue MusicIsMuted { get; }
        IBoolValue SoundIsMuted { get; }
        IValue<float> Volume { get; }
    }
}
