using System;
using System.Collections;
using UnityEngine;

namespace Moyba.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIOverlay : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField, Range(0f, 10f)] private float _fadeInDelay = 0f;
        [SerializeField, Range(0.001f, 10f)] private float _fadeInTime = 0.5f;
        [SerializeField, Range(0f, 10f)] private float _fadeOutDelay = 0f;
        [SerializeField, Range(0.001f, 10f)] private float _fadeOutTime = 0.2f;

        [NonSerialized] private CanvasGroup _canvasGroup;

        [NonSerialized] private Coroutine _coroutine;
        [NonSerialized] private State _state;

        public void TransitionTo(State state)
        {
            if (state == _state) return;

            if (_coroutine != null) this.StopCoroutine(_coroutine);

            this.gameObject.SetActive(true);

            _coroutine = this.StartCoroutine(Coroutine_TransitionTo(state));
        }

        private void Awake()
        {
            _canvasGroup = this.GetComponent<CanvasGroup>();
        }

        private IEnumerator Coroutine_TransitionTo(State state)
        {
            _state = state;

            switch (state)
            {
                case State.FadeIn:
                    for (var delay = _fadeInDelay; delay > 0; delay -= Time.deltaTime) yield return null;
                    for (; _canvasGroup.alpha < 1f; _canvasGroup.alpha = Mathf.Clamp01(_canvasGroup.alpha + Time.deltaTime / _fadeInTime)) yield return null;
                    break;

                case State.FadeOut:
                    for (var delay = _fadeOutDelay; delay > 0; delay -= Time.deltaTime) yield return null;
                    for (; _canvasGroup.alpha > 0f; _canvasGroup.alpha = Mathf.Clamp01(_canvasGroup.alpha - Time.deltaTime / _fadeOutTime)) yield return null;
                    this.gameObject.SetActive(false);
                    break;

                case State.Hide:
                    _canvasGroup.alpha = 0f;
                    this.gameObject.SetActive(false);
                    break;

                case State.Show:
                    _canvasGroup.alpha = 1f;
                    break;
            }

            _state = State.None;
            _coroutine = null;
        }

        public enum State
        {
            None = 0,
            FadeIn,
            FadeOut,
            Hide,
            Show,
        }
    }
}
