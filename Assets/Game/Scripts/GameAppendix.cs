using System;
using System.Collections;
using Moyba.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Moyba.Game
{
    public class GameAppendix : ATrait<GameManager>
    {
        [Header("Configuration")]
        [SerializeField] private SceneID _nextSceneID = SceneID.Unspecified;

        [NonSerialized] private bool _isLoadingScene;

        public void TransitionToNextScene()
        => this.TransitionToScene(_nextSceneID);

        public void TransitionToScene(SceneID sceneID)
        {
            if (_isLoadingScene) return;

            this.StartCoroutine(Coroutine_TransitionToScene(sceneID));
        }

        private void Awake()
        {
            this._Assert(ReferenceEquals(_manager.Appendix, null), "is replacing a non-null instance.");
            _manager.Appendix = this;
        }

        private IEnumerator Coroutine_TransitionToScene(SceneID sceneID)
        {
            var sceneIndex = sceneID switch
            {
                SceneID.Unspecified => (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings,
                _ => (int)sceneID
            };
            var asyncSceneLoad = SceneManager.LoadSceneAsync(sceneIndex);
            asyncSceneLoad.allowSceneActivation = false;

            yield return new WaitForSeconds(2.0f);

            asyncSceneLoad.allowSceneActivation = true;
        }

        private void OnDestroy()
        {
            this._Assert(ReferenceEquals(_manager.Appendix, this), "is removing a different instance.");
            _manager.Appendix = null;
        }
    }
}
