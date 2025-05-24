using Moyba.Contracts;
using UnityEngine;

namespace Moyba.UI
{
    [CreateAssetMenu(fileName = "Omnibus.UI.asset", menuName = "Omnibus/UI")]
    public class UIManager : AManager, IUIManager
    {
        public UIAppendix Appendix { get; set; }
    }
}
