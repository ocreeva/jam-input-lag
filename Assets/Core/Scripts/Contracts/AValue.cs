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
}
