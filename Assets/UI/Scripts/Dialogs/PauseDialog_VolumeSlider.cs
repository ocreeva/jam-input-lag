using System;
using Moyba.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace Moyba.UI.Dialogs
{
    [RequireComponent(typeof(Slider))]
    public class PauseDialog_VolumeSlider : MonoBehaviour
    {
        [NonSerialized] private Slider _slider;

        private void Awake()
        {
            _slider = this.GetComponent<Slider>();
        }

        private void HandleVolumeChanged(float value)
        => _slider.value = value;

        void OnDisable()
        {
            Omnibus.SFX.Volume.OnChanged -= this.HandleVolumeChanged;
        }

        void OnEnable()
        {
            Omnibus.SFX.Volume.OnChanged += this.HandleVolumeChanged;

            _slider.value = Omnibus.SFX.Volume.Value;
        }
    }
}
