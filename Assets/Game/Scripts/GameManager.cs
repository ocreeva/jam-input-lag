using System;
using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Game
{
    [CreateAssetMenu(fileName = "Omnibus.Game.asset", menuName = "Omnibus/Game")]
    public class GameManager : AManager, IGameManager
    {
        private static readonly string _DifficultySetting = $"{typeof(GameManager).FullName}.{nameof(Difficulty)}";

        [Header("Settings")]
        [SerializeField] private DifficultyBased<GameConfiguration> _configuration;

        public IValue<Difficulty> Difficulty { get; } = new AValue<Difficulty>();
        public IGameSignal Signal { get; internal set; } = GameSignal.Stub;
        internal IValue<float> SignalLatency { get; set; } = GameSignalLatency.Stub;
        public IValue<float> Timer { get; internal set; } = GameTimer.Stub;

        internal DifficultyBased<GameConfiguration> Configuration => _configuration;

        private void HandleDifficultyChanged(Difficulty difficulty)
        {
            PlayerPrefs.SetString(_DifficultySetting, difficulty.ToString());
        }

        private void OnDisable()
        {
            this.Difficulty.OnChanged -= this.HandleDifficultyChanged;
        }

        private void OnEnable()
        {
            this.Difficulty.Value = (Difficulty)Enum.Parse(typeof(Difficulty), PlayerPrefs.GetString(_DifficultySetting, nameof(Game.Difficulty.Standard)));

            this.Difficulty.OnChanged += this.HandleDifficultyChanged;
        }
    }
}
