using Moyba.Contracts;

namespace Moyba.SFX
{
    public interface ISFXManager
    {
        IMuteable Music { get; }
        IMuteable Sound { get; }
        IValueTrait<float> Volume { get; }
    }
}
