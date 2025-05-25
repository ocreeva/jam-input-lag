using Moyba.Contracts;
using UnityEngine;

namespace Moyba.UI.Overlays
{
    public class LevelComplete : MonoBehaviour
    {
        public void Transition()
        {
            Omnibus.SFX.Volume.Fade();

            Omnibus.UI.Show(Overlay.ScreenFade);

            Omnibus.UI.Hide(Overlay.LevelComplete);

            Omnibus.Game.TransitionToNextScene();
        }
    }
}
