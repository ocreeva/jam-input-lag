using Moyba.Contracts;

namespace Moyba.Game
{
    public interface IGameManager
    {
        event ValueEventHandler<ISignalTransmitter> OnTransmitterActivated;

        IValue<Difficulty> Difficulty { get; }
        IGameSignal Signal { get; }
        IValue<float> Timer { get; }
    }
}
