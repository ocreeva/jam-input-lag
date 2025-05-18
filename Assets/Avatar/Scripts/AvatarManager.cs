using Moyba.Contracts;
using UnityEngine;

namespace Moyba.Avatar
{
    [CreateAssetMenu(fileName = "Omnibus.Avatar.asset", menuName = "Omnibus/Avatar")]
    public class AvatarManager : AManager, IAvatarManager
    {
        public IReadOnlyBoolValue IsGrounded { get; internal set; } = AvatarIsGrounded.Stub;
        public IAvatarKinematics Kinematics { get; internal set; } = AvatarKinematics.Stub;
    }
}
