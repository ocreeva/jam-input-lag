using Moyba.Contracts;

namespace Moyba.UI.Dialogs
{
    public class PauseDialog_MusicToggle : PauseDialog_AMuteToggle
    {
        protected override IBoolValue ChannelIsMuted => Omnibus.SFX.MusicIsMuted;
    }
}
