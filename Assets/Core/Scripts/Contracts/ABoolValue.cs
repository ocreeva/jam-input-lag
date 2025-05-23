namespace Moyba.Contracts
{
    public class ABoolValue : AValue<bool>, IBoolValue
    {
        public ABoolValue() => this.OnBoolean(this.OnFalse, this.OnTrue);

        public event SimpleEventHandler OnFalse;
        public event SimpleEventHandler OnTrue;
    }

    public class ABoolValue<TEntity> : AValue<TEntity, bool>, IBoolValue<TEntity>
    {
        public ABoolValue(TEntity entity) : base(entity) => this.OnBoolean(this.OnFalse, this.OnTrue);

        public event SimpleEventHandler<TEntity> OnFalse;
        public event SimpleEventHandler<TEntity> OnTrue;
    }
}
