using UnityEngine;

namespace Moyba.Contracts
{
    public abstract class AManager : ScriptableObject
    {
        protected void _Set<T>(
            T value,
            ref T field,
            bool includeIdempotent = false,
            ValueEventHandler<T> onChanging = null,
            ValueEventHandler<T> onChanged = null)
        => _ContractUtility.Set(value, ref field, includeIdempotent, onChanging, onChanged);

        protected void _Set(
            bool value,
            ref bool field,
            bool includeIdempotent = false,
            ValueEventHandler<bool> onChanging = null,
            ValueEventHandler<bool> onChanged = null,
            SimpleEventHandler onFalse = null,
            SimpleEventHandler onTrue = null)
        => _ContractUtility.Set(value, ref field, includeIdempotent, onChanging, onChanged, onFalse, onTrue);
    }
}
