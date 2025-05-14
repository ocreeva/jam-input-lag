using Moyba.Contracts;
using UnityEngine;

namespace Moyba.SFX
{
    [CreateAssetMenu(fileName = "Omnibus.SFX.asset", menuName = "Omnibus/SFX")]
    public class SFXManager : AManager, ISFXManager
    {
        public ISFXMusic Music { get; internal set; } = SFXMusic.Stub;
    }
}
