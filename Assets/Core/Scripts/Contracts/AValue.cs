using System;

namespace Moyba.Contracts
{
    public class AValue<TValue> : IValue<TValue>
    {
        [NonSerialized] private TValue _value;

        public TValue Value
        {
            get => _value;
            set => _ContractUtility.Set(value, ref _value, includeIdempotent: false, onChanged: this.OnChanged, onChanging: this.OnChanging);
        }

        public event ValueEventHandler<TValue> OnChanged;
        public event ValueEventHandler<TValue> OnChanging;
    }
}
