using Moyba.Contracts;
using Moyba.SFX;

namespace Moyba.UI.Dialogs
{
    public class PauseDialog_SoundToggle : PauseDialog_AMuteToggle
    {
        protected override IMuteable Muteable => Omnibus.SFX.Sound;
    }
}
