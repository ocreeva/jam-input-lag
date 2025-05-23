namespace Moyba
{
    public delegate void ValueEventHandler<TValue>(TValue value);
    public delegate void ValueEventHandler<TSource, TValue>(TSource source, TValue value);
}
