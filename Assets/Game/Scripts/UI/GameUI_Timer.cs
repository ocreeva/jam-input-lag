using System;
using Moyba.Contracts;
using TMPro;
using UnityEngine;

namespace Moyba.Game.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class GameUI_Timer : MonoBehaviour
    {
        private const float _MaxTimer = 999.99f;

        [SerializeField] private bool _isSubseconds;

        [NonSerialized] private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = this.GetComponent<TextMeshProUGUI>();
        }

        private void HandleTimerChanged(float timer)
        {
            // display negative times as 0
            timer = Mathf.Clamp(timer, 0, _MaxTimer);

            var value = Mathf.FloorToInt(timer);
            if (_isSubseconds) value = Mathf.FloorToInt(10 * (timer - value));

            _text.text = _isSubseconds ? $"{value:0}" : $"{value}";
        }

        private void OnDisable()
        {
            Omnibus.Game.Timer.OnChanged -= this.HandleTimerChanged;
        }

        private void OnEnable()
        {
            Omnibus.Game.Timer.OnChanged += this.HandleTimerChanged;
            this.HandleTimerChanged(Omnibus.Game.Timer.Value);
        }
    }
}
