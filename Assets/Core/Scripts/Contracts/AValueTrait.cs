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

    public abstract class AValueTrait<TManager, TEntity, TIEntity, TValue> : ATrait<TManager, TEntity>, IValue<TIEntity, TValue>, IValue<TValue>
        where TManager : ScriptableObject
        where TEntity : AnEntity<TManager, TEntity>, TIEntity
    {
        private TValue _value;

        protected AValueTrait()
        {
            this.OnChangedWithEntity += this.HandleChanged;
        }

        public event ValueEventHandler<TValue> OnChanged;
        private event ValueEventHandler<TIEntity, TValue> OnChangedWithEntity;
        event ValueEventHandler<TIEntity, TValue> IEventableValue<TIEntity, TValue>.OnChanged
        {
            add => this.OnChangedWithEntity += value;
            remove => this.OnChangedWithEntity -= value;
        }

        public TValue Value
        {
            get => _value;
            set => _Set(value, ref _value, onChanged: this.OnChangedWithEntity);
        }

        private void HandleChanged(TIEntity _, TValue value) => this.OnChanged?.Invoke(value);
    }
}
