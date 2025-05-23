using System;
using UnityEngine;

namespace Moyba.Contracts
{
    public abstract class AValueTrait<TManager, TValue> : ATrait<TManager>, IValue<TValue>
        where TManager : ScriptableObject
    {
        [NonSerialized] private TValue _value;

        public event ValueEventHandler<TValue> OnChanged;

        public TValue Value
        {
            get => _value;
            set => _Set(value, ref _value, onChanged: this.OnChanged);
        }

        protected abstract class AValueTraitStub<TTrait> : ATraitStub<TTrait>, IValue<TValue>
            where TTrait : AValueTrait<TManager, TValue>
        {
            public event ValueEventHandler<TValue> OnChanged;

            public TValue Value
            {
                get => default;
                set => _SetFail(nameof(Value));
            }

            protected override void TransferEvents(TTrait trait)
            {
                (this.OnChanged, trait.OnChanged) = (trait.OnChanged, this.OnChanged);
            }
        }
    }

    public abstract class AValueTrait<TManager, TEntity, TValue> : ATrait<TManager, TEntity>, IValue<TEntity, TValue>
        where TManager : ScriptableObject
        where TEntity : AnEntity<TManager, TEntity>
    {
        private TValue _value;

        public event ValueEventHandler<TEntity, TValue> OnChanged;

        public TValue Value
        {
            get => _value;
            set => _Set(value, ref _value, onChanged: this.OnChanged);
        }
    }
}
