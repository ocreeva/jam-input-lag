using UnityEngine;

namespace Moyba.Contracts
{
    public abstract class ATrait<TManager> : AContract, IEnableable
        where TManager : ScriptableObject
    {
        [SerializeField] protected internal TManager _manager;

#if UNITY_EDITOR
        protected virtual void Reset()
        {
            _manager = _ContractUtility.LoadOmnibusAsset<TManager>();
        }
#endif

        protected void _Set<TValue>(
            TValue value,
            ref TValue field,
            ValueEventHandler<TValue> onChanged = null)
        => _ContractUtility.Set(value, ref field, onChanged);

        protected abstract class ATraitStub<TTrait> : IEnableable
            where TTrait : ATrait<TManager>
        {
            private bool? _enabled;

            public bool enabled
            {
                get => _enabled.GetValueOrDefault(true);
                set => _enabled = value;
            }

            public void TransferControlFrom(TTrait trait)
            {
                _enabled = trait.enabled;

                this.TransferEvents(trait);
            }

            public void TransferControlTo(TTrait trait)
            {
                if (_enabled.HasValue) trait.enabled = _enabled.Value;

                this.TransferEvents(trait);
            }

            protected virtual void TransferEvents(TTrait trait) { }

            [System.Diagnostics.Conditional("UNITY_ASSERTIONS")]
            protected void _SetFail(string paramName)
            => Debug.LogWarning($"{this.GetType().Name} is setting a value for '{paramName}'.");

            [System.Diagnostics.Conditional("UNITY_ASSERTIONS")]
            protected void _CallFail(string methodName)
            => Debug.LogWarning($"{this.GetType().Name} is invoking '{methodName}'.");
        }
    }

    public abstract class ATrait<TManager, TEntity> : AContract
        where TManager : ScriptableObject
        where TEntity : AnEntity<TManager, TEntity>
    {
        [SerializeField] protected TEntity _entity;

#if UNITY_EDITOR
        protected virtual void Reset()
        {
            _entity = this.GetComponent<TEntity>() ?? this.GetComponentInParent<TEntity>();
        }
#endif

        protected void _Set<TValue>(
            TValue value,
            ref TValue field,
            ValueEventHandler<TEntity, TValue> onChanged = null)
        => _ContractUtility.Set(_entity, value, ref field, onChanged);
    }
}
