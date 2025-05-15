using Moyba.Contracts;
using UnityEngine;

namespace Moyba.SFX
{
    [CreateAssetMenu(fileName = "Omnibus.SFX.asset", menuName = "Omnibus/SFX")]
    public class SFXManager : AManager, ISFXManager
    {
        public IBoolValue MusicIsMuted { get; internal set; } = SFX.MusicIsMuted.Stub;
        public IBoolValue SoundIsMuted { get; internal set; } = SFX.SoundIsMuted.Stub;
        public IValue<float> Volume { get; internal set; } = SFXVolume.Stub;
    }
}
