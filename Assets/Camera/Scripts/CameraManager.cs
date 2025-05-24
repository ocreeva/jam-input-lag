using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Camera
{
    [CreateAssetMenu(fileName = "Omnibus.Camera.asset", menuName = "Omnibus/Camera")]
    public class CameraManager : AManager, ICameraManager
    {
        public ICameraAppendix Appendix { get; internal set; } = CameraAppendix.Stub;
    }
}
