using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Collectibles
{
    public class Coin : AnEntity<CollectiblesManager>, ICoin
    {
        [SerializeField] private CoinDenomination _denomination = CoinDenomination.Bronze;

        public event SimpleEventHandler OnPickup;

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
            Omnibus.SFX.Play(SFX.SoundClip.Coin);

            this.OnPickup?.Invoke();

            GameObject.Destroy(this.gameObject);
        }
    }

    public partial class CollectiblesConfiguration
    {

    }
}
