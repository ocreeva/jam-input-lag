using Moyba.Contracts;
using Moyba.SFX;

namespace Moyba.UI.Dialogs
{
    public class PauseDialog_MusicToggle : PauseDialog_AMuteToggle
    {
        protected override IMuteable Muteable => Omnibus.SFX.Music;
    }
}
