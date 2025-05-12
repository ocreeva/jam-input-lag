using UnityEngine;

namespace Moyba.Avatar
{
    [CreateAssetMenu(fileName = "Omnibus.Avatar.asset", menuName = "Omnibus/Avatar")]
    public class AvatarManager : ScriptableObject, IAvatarManager
    {
        public IAvatarKinematics Kinematics { get; internal set; } = AvatarKinematics.Stub; 
    }
}
