using System;
using Moyba.Contracts;

namespace Moyba.Game
{
    public interface IGameSignal
    {
        IReadOnlyValue<float> Latency { get; }

        void Send(Action action);
    }
}
