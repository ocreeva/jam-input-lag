using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Game
{
    public interface ISignalTransmitter
    {
        IReadOnlyBoolValue<ISignalTransmitter> IsTransmitting { get; }
        Vector3 Position { get; }
    }
}
