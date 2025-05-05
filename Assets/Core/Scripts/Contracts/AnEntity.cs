using UnityEngine;

namespace Moyba.Contracts
{
    public abstract class AnEntity<TManager> : AContract
        where TManager : ScriptableObject
    {
        [SerializeField] protected TManager _manager;

#if UNITY_EDITOR
        protected virtual void Reset()
        {
            _manager = _ContractUtility.LoadOmnibusAsset<TManager>();
        }
#endif
    }
}
