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

        public event ValueEventHandler<ISignalTransmitter> OnTransmitterActivated;

        public IValue<Difficulty> Difficulty { get; } = new AValue<Difficulty>();
        public IGameLevelComplete LevelComplete { get; internal set; } = GameLevelComplete.Stub;
        public IGameSignal Signal { get; internal set; } = GameSignal.Stub;
        internal IValue<float> SignalLatency { get; set; } = GameSignalLatency.Stub;
        public IGameTimer Timer { get; internal set; } = GameTimer.Stub;

        internal DifficultyBased<GameConfiguration> Configuration => _configuration;

        internal void Deregister(ISignalTransmitter transmitter) => transmitter.IsTransmitting.OnTrue -= this.HandleTransmitterIsTransmitting;
        internal void Register(ISignalTransmitter transmitter) => transmitter.IsTransmitting.OnTrue += this.HandleTransmitterIsTransmitting;

        private void HandleDifficultyChanged(Difficulty difficulty)
        {
            PlayerPrefs.SetString(_DifficultySetting, difficulty.ToString());
        }

        private void HandleTransmitterIsTransmitting(ISignalTransmitter transmitter) => this.OnTransmitterActivated?.Invoke(transmitter);

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
