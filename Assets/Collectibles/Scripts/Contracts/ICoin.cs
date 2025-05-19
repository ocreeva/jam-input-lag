namespace Moyba.Collectibles
{
    public interface ICoin : ICollectible
    {
        CoinDenomination Denomination { get; }
    }
}
