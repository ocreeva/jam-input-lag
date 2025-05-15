using System;
using Moyba.Contracts;
using Moyba.SFX;
using UnityEngine;
using UnityEngine.UI;

namespace Moyba.UI.Dialogs
{
    [RequireComponent(typeof(Toggle))]
    public abstract class PauseDialog_AMuteToggle : MonoBehaviour
    {
        [NonSerialized] private Toggle _toggle;

        protected abstract IBoolValue ChannelIsMuted { get; }

        private void Awake()
        {
            _toggle = this.GetComponent<Toggle>();
        }

        private void HandleIsMutedValueChanged(bool value)
        => _toggle.isOn = !value;

        private void OnDisable()
        {
            this.ChannelIsMuted.OnChanged -= this.HandleIsMutedValueChanged;
        }

        private void OnEnable()
        {
            this.ChannelIsMuted.OnChanged += this.HandleIsMutedValueChanged;

            _toggle.isOn = !this.ChannelIsMuted.Value;
        }
    }
}
