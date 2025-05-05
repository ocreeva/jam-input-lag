using UnityEngine;

namespace Moyba.Contracts
{
    public abstract class AContract : MonoBehaviour
    {
        protected void _Set<T>(
            T value,
            ref T field,
            ValueEventHandler<T> changing = null,
            ValueEventHandler<T> changed = null,
            bool includeIdempotent = false)
        => _ContractUtility.Set(value, ref field, changing, changed, includeIdempotent);

        protected void _Set(
            bool value,
            ref bool field,
            SimpleEventHandler onFalse = null,
            SimpleEventHandler onTrue = null,
            bool includeIdempotent = false)
        => _ContractUtility.Set(value, ref field, onFalse, onTrue, includeIdempotent);
    }
}
