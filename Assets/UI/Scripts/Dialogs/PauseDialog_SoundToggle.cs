using Moyba.Contracts;

namespace Moyba.UI.Dialogs
{
    public class PauseDialog_SoundToggle : PauseDialog_AMuteToggle
    {
        protected override IBoolValue ChannelIsMuted => Omnibus.SFX.SoundIsMuted;
    }
}
