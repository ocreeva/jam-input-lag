using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Game.UI
{
    public class StartUI_PlayButton : ATrait<GameManager>
    {
        [Header("Configuration")]
        [SerializeField] private SceneID _sceneID;

        public void Play()
        {
            Omnibus.UI.Show(Moyba.UI.Overlay.ScreenFade);

            _manager.Appendix.TransitionToScene(_sceneID);
        }
    }
}
