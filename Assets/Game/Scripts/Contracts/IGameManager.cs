using Moyba.Contracts;

namespace Moyba.Game
{
    public interface IGameManager
    {
        IValue<Difficulty> Difficulty { get; }
        IGameSignal Signal { get; }
        IValue<float> Timer { get; }
    }
}
