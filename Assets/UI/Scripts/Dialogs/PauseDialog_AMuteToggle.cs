using System;
using Moyba.SFX;
using UnityEngine;
using UnityEngine.UI;

namespace Moyba.UI.Dialogs
{
    [RequireComponent(typeof(Toggle))]
    public abstract class PauseDialog_AMuteToggle : MonoBehaviour
    {
        [NonSerialized] private Toggle _toggle;

        protected abstract IMuteable Muteable { get; }

        private void Awake()
        {
            _toggle = this.GetComponent<Toggle>();
        }

        private void HandleMusicIsMuted()
        => _toggle.isOn = false;
        private void HandleMusicIsUnmuted()
        => _toggle.isOn = true;

        private void OnDisable()
        {
            this.Muteable.OnMuted -= this.HandleMusicIsMuted;
            this.Muteable.OnUnmuted -= this.HandleMusicIsUnmuted;
        }

        private void OnEnable()
        {
            this.Muteable.OnMuted += this.HandleMusicIsMuted;
            this.Muteable.OnUnmuted += this.HandleMusicIsUnmuted;

            _toggle.isOn = !this.Muteable.IsMuted;
        }
    }
}
