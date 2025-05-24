using Moyba.Contracts;
using UnityEngine;

namespace Moyba.UI
{
    [CreateAssetMenu(fileName = "Omnibus.UI.asset", menuName = "Omnibus/UI")]
    public class UIManager : AManager, IUIManager
    {
        public UIAppendix Appendix { get; set; }

        public void Hide(Overlay overlay, bool immediately)
        => this.Appendix.Hide(overlay, immediately);

        public void Show(Overlay overlay, bool immediately)
        => this.Appendix.Show(overlay, immediately);
    }
}
