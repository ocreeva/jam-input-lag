using System.Collections.Generic;
using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Collectibles
{
    [CreateAssetMenu(fileName = "Omnibus.Collectibles.asset", menuName = "Omnibus/Collectibles")]
    public class CollectiblesManager : AManager, ICollectiblesManager
    {
        private readonly IDictionary<Coordinate, ICoin> _coins = new Dictionary<Coordinate, ICoin>();

        public ICoin GetCoin(Coordinate coordinate)
        => _coins[coordinate];

        public bool TryGetCoin(Coordinate coordinate, out ICoin coin)
        => _coins.TryGetValue(coordinate, out coin);

        internal void Deregister(ICoin coin)
        => _coins.Remove(coin.Coordinate);

        internal void Register(ICoin coin)
        => _coins.Add(coin.Coordinate, coin);
    }
}
