using Moyba.Contracts;

namespace Moyba.SFX
{
    public interface ISFXVolume : IValue<float>
    {
        void Fade();
    }
}
