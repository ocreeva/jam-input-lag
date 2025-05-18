using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Collectables
{
    public class Coin : AnEntity<CollectablesManager>, ICoin
    {
        [SerializeField] private CoinDenomination _denomination = CoinDenomination.Bronze;

        public Coordinate Coordinate { get; private set; }

        public CoinDenomination Denomination => _denomination;

        private void Awake()
        {
            this.Coordinate = new Coordinate(this.transform.position);
        }

        private void OnDisable()
        {
            _manager.Deregister(this);
        }

        private void OnEnable()
        {
            _manager.Register(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"{this.Denomination} pickup: {other.gameObject.name}");
        }
    }
}
