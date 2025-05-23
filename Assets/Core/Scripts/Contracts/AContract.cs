using UnityEngine;

namespace Moyba.Contracts
{
    public abstract class AContract : MonoBehaviour
    {
        protected void _Set<TValue>(
            TValue value,
            ref TValue field,
            ValueEventHandler<TValue> onChanged = null)
        => _ContractUtility.Set(value, ref field, onChanged);
    }
}
