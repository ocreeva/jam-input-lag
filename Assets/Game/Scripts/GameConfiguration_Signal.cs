using System;
using UnityEngine;

namespace Moyba.Game
{
    public partial class GameConfiguration
    {
        [SerializeField] private SignalConfiguration _signal;

        public SignalConfiguration Signal => _signal;

        private void OnValidate_Signal()
        {
            _signal.OnValidate();
        }

        [Serializable]
        public class SignalConfiguration
        {
            [SerializeField] private LatencyTransition[] _latency = Array.Empty<LatencyTransition>();

            public float GetLatencyAt(float distance)
            {
                var latency = 0f;

                foreach (var transition in _latency)
                {
                    if (distance <= transition.start) return latency;

                    var difference = distance - transition.start;
                    if (difference < transition.length) return latency + difference / transition.length;

                    latency++;
                }

                return latency;
            }

#if UNITY_EDITOR
            internal void OnValidate()
            {
                for (var index = 0; index < _latency.Length; index++)
                {
                    var transition = _latency[index];
                    if (transition.start < 0) _latency[index].start = 0;
                    if (transition.length < 0) _latency[index].length = 0;

                    if (index == 0) continue;

                    var previous = _latency[index - 1];
                    var previousEnd = previous.start + previous.length;
                    if (transition.start < previousEnd) _latency[index].start = previousEnd;
                }
            }
#endif

            [Serializable]
            public struct LatencyTransition
            {
                [Range(0, 100)] public int start;
                [Range(0, 10)] public int length;
            }
        }
    }
}
