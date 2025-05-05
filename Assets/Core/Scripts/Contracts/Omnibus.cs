using UnityEngine;

namespace Moyba.Contracts
{
    [DefaultExecutionOrder(-99)]
    public class Omnibus : MonoBehaviour
    {
        private static Omnibus _Instance;

        private void Awake()
        {
            if (Omnibus._Instance)
            {
                if (Omnibus._Instance != this) Object.Destroy(this.gameObject);
            }
            else
            {
                _Instance = this;
                Object.DontDestroyOnLoad(this.gameObject);
            }
        }
    }
}
