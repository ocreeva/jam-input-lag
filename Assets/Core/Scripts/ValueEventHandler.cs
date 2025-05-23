namespace Moyba
{
    public delegate void ValueEventHandler<in TValue>(TValue value);
    public delegate void ValueEventHandler<in TSource, in TValue>(TSource source, TValue value);
}
