using Moyba.Contracts;

namespace Moyba.Game
{
    public interface IGameManager
    {
        event ValueEventHandler<ISignalTransmitter> OnTransmitterActivated;

        IValue<Difficulty> Difficulty { get; }
        IGameLevelComplete LevelComplete { get; }
        IGameLevelStart LevelStart { get; }
        IGameSignal Signal { get; }
        IGameTimer Timer { get; }

        void TransitionToNextScene();
    }
}
