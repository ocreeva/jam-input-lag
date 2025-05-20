using System;
using UnityEngine;

namespace Moyba.Game
{
    public partial class GameConfiguration
    {
        [SerializeField] private TimerConfiguration _timer;

        public TimerConfiguration Timer => _timer;

        [Serializable]
        public class TimerConfiguration
        {
            [SerializeField, Range(0f, 3600f)] private float _initialDuration = 30f;

            public float InitialDuration => _initialDuration;
        }
    }
}
