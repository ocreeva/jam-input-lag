using System;

namespace Moyba.Contracts
{
    public class AValue<TValue> : IValue<TValue>
    {
        [NonSerialized] private TValue _value;

        public event ValueEventHandler<TValue> OnChanged;

        public TValue Value
        {
            get => _value;
            set => _ContractUtility.Set(value, ref _value, onChanged: this.OnChanged);
        }
    }

    public class AValue<TEntity, TValue> : IValue<TEntity, TValue>, IValue<TValue>
    {
        [NonSerialized] private TEntity _entity;
        [NonSerialized] private TValue _value;

        public AValue(TEntity entity)
        {
            _entity = entity;
            this.OnChangedWithEntity += this.HandleChanged;
        }

        public event ValueEventHandler<TValue> OnChanged;
        private event ValueEventHandler<TEntity, TValue> OnChangedWithEntity;
        event ValueEventHandler<TEntity, TValue> IEventableValue<TEntity, TValue>.OnChanged
        {
            add => this.OnChangedWithEntity += value;
            remove => this.OnChangedWithEntity -= value;
        }

        public TValue Value
        {
            get => _value;
            set => _ContractUtility.Set(_entity, value, ref _value, onChanged: this.OnChangedWithEntity);
        }

        private void HandleChanged(TEntity _, TValue value) => this.OnChanged?.Invoke(value);
    }
}
