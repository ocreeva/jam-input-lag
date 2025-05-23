namespace Moyba
{
    public delegate void SimpleEventHandler();
    public delegate void SimpleEventHandler<in TSource>(TSource source);
}
