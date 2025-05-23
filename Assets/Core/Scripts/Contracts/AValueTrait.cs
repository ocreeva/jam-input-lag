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

    public abstract class AValueTrait<TEntity, TManager, TValue> : ATrait<TEntity, TManager>, IValue<TValue>
        where TEntity : AnEntity<TManager>
        where TManager : ScriptableObject
    {
        private TValue _value;

        public event ValueEventHandler<TValue> OnChanged;

        public TValue Value
        {
            get => _value;
            set => _Set(value, ref _value, onChanged: this.OnChanged);
        }
    }
}
