namespace Moyba.Collectables
{
    public interface ICoin : ICollectable
    {
        CoinDenomination Denomination { get; }
    }
}
