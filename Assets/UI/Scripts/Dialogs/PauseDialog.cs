using Moyba.Contracts;

namespace Moyba.UI.Dialogs
{
    public class PauseDialog : ATrait<UIManager>, IDialog
    {
        public void HandleMusicToggle(bool value)
        => Omnibus.SFX.MusicIsMuted.Value = !value;

        public void HandleSoundToggle(bool value)
        => Omnibus.SFX.SoundIsMuted.Value = !value;

        public void HandleVolumeSlider(float value)
        => Omnibus.SFX.Volume.Value = value;
    }
}
