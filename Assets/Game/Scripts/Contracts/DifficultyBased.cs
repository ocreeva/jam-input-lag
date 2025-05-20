using System;
using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Game
{
    [Serializable]
    public class DifficultyBased<T>
        where T : ScriptableObject
    {
        [SerializeReference] private T _casual;
        [SerializeReference] private T _standard;
        [SerializeReference] private T _expert;

        public T Value => this[Omnibus.Game.Difficulty.Value];

        public T this[Difficulty difficulty]
        => difficulty switch
        {
            Difficulty.Casual => _casual,
            Difficulty.Standard => _standard,
            Difficulty.Expert => _expert,
            _ => throw new NotSupportedException($"Unexpected difficulty: {difficulty}")
        };
    }
}
