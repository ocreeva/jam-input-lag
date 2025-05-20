using System;
using Moyba.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace Moyba.Game.UI
{
    [RequireComponent(typeof(Image))]
    public class GameUI_LatencyColor : MonoBehaviour
    {
        [SerializeField] private Color[] _colors = new Color[4];

        [NonSerialized] private Image _image;

        private void Awake()
        {
            _image = this.GetComponent<Image>();
        }

        private void HandleLatencyChanged(float latency)
        {
            var floor = Math.Clamp(Mathf.FloorToInt(latency), 0, 3);
            if ((latency == floor) || (floor == 3))
            {
                _image.color = _colors[floor];
                return;
            }

            var primaryColor = _colors[floor];
            var secondaryColor = _colors[floor + 1];
            var secondaryShare = latency - floor;
            var primaryShare = 1 - secondaryShare;
            _image.color = primaryColor * primaryShare + secondaryColor * secondaryShare;
        }

        private void OnDisable()
        {
            Omnibus.Game.Signal.Latency.OnChanged -= this.HandleLatencyChanged;
        }

        private void OnEnable()
        {
            Omnibus.Game.Signal.Latency.OnChanged += this.HandleLatencyChanged;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_colors.Length != 4)
            {
                var previous = _colors;
                _colors = new Color[4];
                Array.Copy(previous, _colors, Math.Min(previous.Length, 4));
            }
        }
#endif

        private void Start()
        {
            this.HandleLatencyChanged(Omnibus.Game.Signal.Latency.Value);
        }
    }
}
