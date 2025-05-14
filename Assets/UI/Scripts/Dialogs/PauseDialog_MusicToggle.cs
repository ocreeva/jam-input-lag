using System;
using Moyba.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace Moyba.UI.Dialogs
{
    [RequireComponent(typeof(Toggle))]
    public class PauseDialog_MusicToggle : MonoBehaviour
    {
        [NonSerialized] private Toggle _toggle;

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
            Omnibus.SFX.Music.OnMuted -= this.HandleMusicIsMuted;
            Omnibus.SFX.Music.OnUnmuted -= this.HandleMusicIsUnmuted;
        }

        private void OnEnable()
        {
            Omnibus.SFX.Music.OnMuted += this.HandleMusicIsMuted;
            Omnibus.SFX.Music.OnUnmuted += this.HandleMusicIsUnmuted;

            _toggle.isOn = !Omnibus.SFX.Music.IsMuted;
        }
    }
}
