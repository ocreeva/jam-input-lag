using System;
using UnityEngine;

namespace Moyba.Contracts
{
    public abstract class ABoolValueTrait<TManager> : ATrait<TManager>, IBoolValue
        where TManager : ScriptableObject
    {
        [NonSerialized] private bool _value;

        public event ValueEventHandler<bool> OnChanged;
        public event ValueEventHandler<bool> OnChanging;
        public event SimpleEventHandler OnFalse;
        public event SimpleEventHandler OnTrue;

        public bool Value
        {
            get => _value;
            set => _Set(value, ref _value,
                onChanged: this.OnChanged,
                onChanging: this.OnChanging,
                onFalse: this.OnFalse,
                onTrue: this.OnTrue);
        }

        protected abstract class AValueTraitStub<TTrait> : ATraitStub<TTrait>, IBoolValue
            where TTrait : ABoolValueTrait<TManager>
        {
            public event ValueEventHandler<bool> OnChanged;
            public event ValueEventHandler<bool> OnChanging;
            public event SimpleEventHandler OnFalse;
            public event SimpleEventHandler OnTrue;

            public bool Value
            {
                get => default;
                set => _SetFail(nameof(Value));
            }

            protected override void TransferEvents(TTrait trait)
            {
                (this.OnChanged, trait.OnChanged) = (trait.OnChanged, this.OnChanged);
                (this.OnChanging, trait.OnChanging) = (trait.OnChanging, this.OnChanging);
                (this.OnFalse, trait.OnFalse) = (trait.OnFalse, this.OnFalse);
                (this.OnTrue, trait.OnTrue) = (trait.OnTrue, this.OnTrue);
            }
        }
    }

    public abstract class ABoolValueTrait<TEntity, TManager> : ATrait<TEntity, TManager>, IBoolValue
        where TEntity : AnEntity<TManager>
        where TManager : ScriptableObject
    {
        private bool _value;

        public event SimpleEventHandler OnFalse;
        public event SimpleEventHandler OnTrue;
        public event ValueEventHandler<bool> OnChanged;
        public event ValueEventHandler<bool> OnChanging;

        public bool Value
        {
            get => _value;
            set => _Set(value, ref _value,
                onChanged: this.OnChanged,
                onChanging: this.OnChanging,
                onFalse: this.OnFalse,
                onTrue: this.OnTrue);
        }
    }
}
