using System;
using UnityEngine;

namespace Moyba.Contracts
{
    public abstract class ABoolValueTrait<TManager> : ATrait<TManager>, IBoolValue
        where TManager : ScriptableObject
    {
        [NonSerialized] private bool _value;

        protected ABoolValueTrait() => this.OnBoolean(this.OnFalse, this.OnTrue);

        public event ValueEventHandler<bool> OnChanged;
        public event SimpleEventHandler OnFalse;
        public event SimpleEventHandler OnTrue;

        public bool Value
        {
            get => _value;
            set => _Set(value, ref _value, onChanged: this.OnChanged);
        }

        protected abstract class ABoolValueTraitStub<TTrait> : ATraitStub<TTrait>, IBoolValue
            where TTrait : ABoolValueTrait<TManager>
        {
            protected ABoolValueTraitStub() => this.OnBoolean(this.OnFalse, this.OnTrue);

            public event ValueEventHandler<bool> OnChanged;
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

        protected ABoolValueTrait() => this.OnBoolean(this.OnFalse, this.OnTrue);

        public event ValueEventHandler<bool> OnChanged;
        public event SimpleEventHandler OnFalse;
        public event SimpleEventHandler OnTrue;

        public bool Value
        {
            get => _value;
            set => _Set(value, ref _value, onChanged: this.OnChanged);
        }
    }
}
