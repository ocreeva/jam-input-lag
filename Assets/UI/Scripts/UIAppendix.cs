using System;
using System.Collections;
using Moyba.Contracts;
using UnityEngine;

namespace Moyba.UI
{
    public class UIAppendix : ATrait<UIManager>
    {
        [Header("Configuration")]
        [SerializeField, Range(0f, 10f)] private float _fadeInDelay = 0;
        [SerializeField, Range(0f, 10f)] private float _fadeInTime = 1;

        [Header("Components")]
        [SerializeField] private GameObject _dialogBackground;
        [SerializeField] private GameObject _pauseDialog;
        [SerializeField] private CanvasGroup _levelCompleteOverlay;

        [NonSerialized] private bool _isPaused;

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.Appendix, null), "is replacing a non-null instance.");
            _manager.Appendix = this;
        }

        private IEnumerator Coroutine_OverlayFadeIn(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.gameObject.SetActive(true);

            if (_fadeInDelay > 0f)
            {
                for (var elapsed = 0f; elapsed < 1f; elapsed += Time.deltaTime / _fadeInDelay) yield return null;
            }

            if (_fadeInTime > 0f)
            {
                for (var alpha = 0f; alpha < 1f; alpha += Time.deltaTime / _fadeInTime)
                {
                    canvasGroup.alpha = alpha;
                    yield return null;
                }
            }

            canvasGroup.alpha = 1f;
        }

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.Appendix, this), "is removing a different instance.");
            _manager.Appendix = null;
        }

        private void HandleAvatarIsOnTarget()
        => this.StartCoroutine(Coroutine_OverlayFadeIn(_levelCompleteOverlay));

        private void HandleGamePause()
        {
            _isPaused = !_isPaused;

            _dialogBackground.SetActive(_isPaused);
            _pauseDialog.SetActive(_isPaused);

            Time.timeScale = _isPaused ? 0.0f : 1.0f;
        }

        private void OnDisable()
        {
            Omnibus.Avatar.IsOnTarget.OnTrue -= this.HandleAvatarIsOnTarget;
            Omnibus.Input.Game.OnPause -= this.HandleGamePause;
        }

        private void OnEnable()
        {
            Omnibus.Avatar.IsOnTarget.OnTrue += this.HandleAvatarIsOnTarget;
            Omnibus.Input.Game.OnPause += this.HandleGamePause;
        }
    }
}
