using System;
using Moyba.Contracts;
using UnityEngine;

namespace Moyba.UI
{
    public class UIAppendix : ATrait<UIManager>
    {
        [SerializeField] private GameObject _dialogBackground;
        [SerializeField] private GameObject _pauseDialog;

        [NonSerialized] private bool _isPaused;

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.Appendix, null), "is replacing a non-null instance.");
            _manager.Appendix = this;
        }

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.Appendix, this), "is removing a different instance.");
            _manager.Appendix = null;
        }

        private void HandleGamePause()
        {
            _isPaused = !_isPaused;

            _dialogBackground.SetActive(_isPaused);
            _pauseDialog.SetActive(_isPaused);

            Time.timeScale = _isPaused ? 0.0f : 1.0f;
        }

        private void OnDisable()
        {
            Omnibus.Input.Game.OnPause -= this.HandleGamePause;
        }

        private void OnEnable()
        {
            Omnibus.Input.Game.OnPause += this.HandleGamePause;
        }
    }
}
