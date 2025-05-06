using UnityEngine;

namespace Moyba.Input
{
    [CreateAssetMenu(fileName = "Omnibus.Input.asset", menuName = "Omnibus/Input")]
    public class InputManager : ScriptableObject, IInputManager
    {
        internal Controls Controls { get; private set; }

        private void OnEnable()
        {
            if (this.Controls == null) this.Controls = new Controls();
        }
    }
}
