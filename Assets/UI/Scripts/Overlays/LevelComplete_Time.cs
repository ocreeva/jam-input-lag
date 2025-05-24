using System;
using Moyba.Contracts;
using TMPro;
using UnityEngine;

namespace Moyba.UI.Overlays
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LevelComplete_Time : MonoBehaviour
    {
        [NonSerialized] private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = this.GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            var time = Mathf.RoundToInt(Omnibus.Game.Timer.Value);
            var seconds = time % 60;
            var minutes = (time - seconds) / 60;

            _text.text = minutes > 0 ? $"{minutes}m {seconds}s" : $"{seconds}s";
        }
    }
}
