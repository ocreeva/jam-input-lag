using Moyba.Contracts;

namespace Moyba.Game
{
    public interface IGameTimer : IReadOnlyValue<float>
    {
        void Halt();
    }
}
