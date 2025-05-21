namespace Moyba.Collectibles
{
    public interface ICollectible
    {
        event SimpleEventHandler OnPickup;

        Coordinate Coordinate { get; }
    }
}
