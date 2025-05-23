using System;

namespace Moyba.Contracts
{
    public class ABoolValue : IBoolValue
    {
        [NonSerialized] private bool _value;

        public ABoolValue() => this.OnBoolean(this.OnFalse, this.OnTrue);

        public bool Value
        {
            get => _value;
            set => _ContractUtility.Set(value, ref _value, onChanged: this.OnChanged);
        }

        public event ValueEventHandler<bool> OnChanged;
        public event SimpleEventHandler OnFalse;
        public event SimpleEventHandler OnTrue;
    }
}
