using System;
using UnityEngine;

namespace Moyba.Contracts
{
    public abstract class AnEntity<TManager, TEntity> : AContract
        where TManager : ScriptableObject
        where TEntity : AnEntity<TManager, TEntity>
    {
        [SerializeField] protected TManager _manager;

#if UNITY_EDITOR
        protected AnEntity()
        {
            if (this.GetType() != typeof(TEntity))
            {
                throw new InvalidCastException($"Generic type '{typeof(TEntity).FullName}' must match the class type '{this.GetType().FullName}'.");
            }
        }

        protected virtual void Reset()
        {
            _manager = _ContractUtility.LoadOmnibusAsset<TManager>();
        }
#endif

        protected void _Set<TValue>(
            TValue value,
            ref TValue field,
            ValueEventHandler<TEntity, TValue> onChanged = null)
        => _ContractUtility.Set((TEntity)this, value, ref field, onChanged);
    }
}
