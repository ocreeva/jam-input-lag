namespace Moyba.Contracts
{
    public class ABoolValue : AValue<bool>, IBoolValue
    {
        public ABoolValue()
        {
            this.OnChanged += this.HandleChanged;
        }

        public event SimpleEventHandler OnFalse;
        public event SimpleEventHandler OnTrue;

        private void HandleChanged(bool value) => (value ? this.OnTrue : this.OnFalse)?.Invoke();
    }

    public class ABoolValue<TEntity> : AValue<TEntity, bool>, IBoolValue<TEntity>, IBoolValue
    {
        public ABoolValue(TEntity entity) : base(entity)
        {
            ((IEventableBoolValue<TEntity>)this).OnChanged += this.HandleChanged;
            this.OnFalseWithEntity += this.HandleFalse;
            this.OnTrueWithEntity += this.HandleTrue;
        }

        public event SimpleEventHandler OnFalse;
        private event SimpleEventHandler<TEntity> OnFalseWithEntity;
        event SimpleEventHandler<TEntity> IEventableBoolValue<TEntity>.OnFalse
        {
            add => this.OnFalseWithEntity += value;
            remove => this.OnFalseWithEntity -= value;
        }

        public event SimpleEventHandler OnTrue;
        private event SimpleEventHandler<TEntity> OnTrueWithEntity;
        event SimpleEventHandler<TEntity> IEventableBoolValue<TEntity>.OnTrue
        {
            add => this.OnTrueWithEntity += value;
            remove => this.OnTrueWithEntity -= value;
        }

        private void HandleChanged(TEntity entity, bool value) => (value ? this.OnTrueWithEntity : this.OnFalseWithEntity)?.Invoke(entity);
        private void HandleFalse(TEntity _) => this.OnFalse?.Invoke();
        private void HandleTrue(TEntity _) => this.OnTrue?.Invoke();
    }
}
