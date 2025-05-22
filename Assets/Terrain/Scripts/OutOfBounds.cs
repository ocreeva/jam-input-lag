using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Terrain
{
    public class OutOfBounds : MonoBehaviour
    {
        private void OnTriggerEnter(Collider _)
        => Omnibus.Avatar.IsOutOfBounds.Value = true;

        void OnTriggerExit(Collider _)
        => Omnibus.Avatar.IsOutOfBounds.Value = false;
    }
}
