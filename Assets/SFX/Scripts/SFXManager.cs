using Moyba.Contracts;
using UnityEngine;

namespace Moyba.SFX
{
    [CreateAssetMenu(fileName = "Omnibus.SFX.asset", menuName = "Omnibus/SFX")]
    public class SFXManager : AManager, ISFXManager
    {
        public IMuteable Music { get; internal set; } = SFXMusic.Stub;
        public IMuteable Sound { get; internal set; } = SFXSound.Stub;
        public IValueTrait<float> Volume { get; internal set; } = SFXVolume.Stub;
    }
}
