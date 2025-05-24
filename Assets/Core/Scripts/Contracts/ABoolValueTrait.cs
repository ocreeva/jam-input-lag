using System;
using UnityEngine;

namespace Moyba.Contracts
{
    public abstract class ABoolValueTrait<TManager> : ATrait<TManager>, IBoolValue
        where TManager : ScriptableObject
    {
        [NonSerialized] private bool _value;

        protected ABoolValueTrait() => this.OnChanged += this.HandleChanged;

        public event ValueEventHandler<bool> OnChanged;
        public event SimpleEventHandler OnFalse;
        public event SimpleEventHandler OnTrue;

        public bool Value
        {
            get => _value;
            set => _Set(value, ref _value, onChanged: this.OnChanged);
        }

        private void HandleChanged(bool value) => (value ? this.OnTrue : this.OnFalse)?.Invoke();

        protected abstract class ABoolValueTraitStub<TTrait> : ATraitStub<TTrait>, IBoolValue
            where TTrait : ABoolValueTrait<TManager>
        {
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
                trait.OnChanged -= trait.HandleChanged;

                (this.OnChanged, trait.OnChanged) = (trait.OnChanged, this.OnChanged);
                (this.OnFalse, trait.OnFalse) = (trait.OnFalse, this.OnFalse);
                (this.OnTrue, trait.OnTrue) = (trait.OnTrue, this.OnTrue);

                trait.OnChanged += trait.HandleChanged;
            }
        }
    }

    public abstract class ABoolValueTrait<TManager, TEntity, TIEntity> : AValueTrait<TManager, TEntity, TIEntity, bool>, IBoolValue<TIEntity>, IBoolValue
        where TManager : ScriptableObject
        where TEntity : AnEntity<TManager, TEntity>, TIEntity
    {
        protected ABoolValueTrait()
        {
            ((IEventableValue<TIEntity, bool>)this).OnChanged += this.HandleChanged;
            this.OnFalseWithEntity += this.HandleFalse;
            this.OnTrueWithEntity += this.HandleTrue;
        }

        public event SimpleEventHandler OnFalse;
        private event SimpleEventHandler<TIEntity> OnFalseWithEntity;
        event SimpleEventHandler<TIEntity> IEventableBoolValue<TIEntity>.OnFalse
        {
            add => this.OnFalseWithEntity += value;
            remove => this.OnFalseWithEntity -= value;
        }

        public event SimpleEventHandler OnTrue;
        private event SimpleEventHandler<TIEntity> OnTrueWithEntity;
        event SimpleEventHandler<TIEntity> IEventableBoolValue<TIEntity>.OnTrue
        {
            add => this.OnTrueWithEntity += value;
            remove => this.OnTrueWithEntity -= value;
        }

        private void HandleChanged(TIEntity entity, bool value) => (value ? this.OnTrueWithEntity : this.OnFalseWithEntity)?.Invoke(entity);
        private void HandleFalse(TIEntity _) => this.OnFalse?.Invoke();
        private void HandleTrue(TIEntity _) => this.OnTrue?.Invoke();
    }
}
