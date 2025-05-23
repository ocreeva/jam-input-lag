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

    public class AValue<TEntity, TValue> : IValue<TEntity, TValue>
    {
        [NonSerialized] private TEntity _entity;
        [NonSerialized] private TValue _value;

        public AValue(TEntity entity) => _entity = entity;

        public event ValueEventHandler<TEntity, TValue> OnChanged;

        public TValue Value
        {
            get => _value;
            set => _ContractUtility.Set(_entity, value, ref _value, onChanged: this.OnChanged);
        }
    }
}
