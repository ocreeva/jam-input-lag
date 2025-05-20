using System;
using Moyba.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace Moyba.Game.UI
{
    [RequireComponent(typeof(Slider))]
    public class GameUI_LatencySlider : MonoBehaviour
    {
        [NonSerialized] private Slider _slider;

        private void Awake()
        {
            _slider = this.GetComponent<Slider>();
        }

        private void HandleLatencyChanged(float latency)
        => _slider.value = latency;

        private void OnDisable()
        {
            Omnibus.Game.Signal.Latency.OnChanged -= this.HandleLatencyChanged;
        }

        private void OnEnable()
        {
            Omnibus.Game.Signal.Latency.OnChanged += this.HandleLatencyChanged;

            _slider.value = Omnibus.Game.Signal.Latency.Value;
        }
    }
}
