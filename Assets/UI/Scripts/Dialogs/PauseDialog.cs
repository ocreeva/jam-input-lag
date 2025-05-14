using Moyba.Contracts;

namespace Moyba.UI.Dialogs
{
    public class PauseDialog : ATrait<UIManager>, IDialog
    {
        public void HandleMusicToggle(bool value)
        {
            Omnibus.SFX.Music.IsMuted = !value;
        }
    }
}
