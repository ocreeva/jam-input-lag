using System;
using UnityEngine;

namespace Moyba.Contracts
{
    public abstract class AValueTrait<TManager, TValue> : ATrait<TManager>, IValueTrait<TValue>
        where TManager : ScriptableObject
    {
        [NonSerialized] private TValue _value;

        public event ValueEventHandler<TValue> OnValueChanged;
        public event ValueEventHandler<TValue> OnValueChanging;

        public TValue Value
        {
            get => _value;
            set => _Set(value, ref _value, changing: this.OnValueChanging, changed: this.OnValueChanged);
        }

        protected abstract class AValueTraitStub<TTrait> : ATraitStub<TTrait>
            where TTrait : AValueTrait<TManager, TValue>
        {
            public event ValueEventHandler<TValue> OnValueChanged;
            public event ValueEventHandler<TValue> OnValueChanging;

            public TValue Value
            {
                get => default;
                set => _SetFail(nameof(Value));
            }

            protected override void TransferEvents(TTrait trait)
            {
                (this.OnValueChanged, trait.OnValueChanged) = (trait.OnValueChanged, this.OnValueChanged);
                (this.OnValueChanging, trait.OnValueChanging) = (trait.OnValueChanging, this.OnValueChanging);
            }
        }
    }

    public abstract class AValueTrait<TEntity, TManager, TValue> : ATrait<TEntity, TManager>
        where TEntity : AnEntity<TManager>
        where TManager : ScriptableObject
    {
        private TValue _value;

        public event ValueEventHandler<TValue> OnValueChanged;
        public event ValueEventHandler<TValue> OnValueChanging;

        public TValue Value
        {
            get => _value;
            set => _Set(value, ref _value, changing: this.OnValueChanging, changed: this.OnValueChanged);
        }
    }
}
