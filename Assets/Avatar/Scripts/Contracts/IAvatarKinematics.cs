using UnityEngine;

namespace Moyba.Avatar
{
    public interface IAvatarKinematics
    {
        Vector3 Position { get; }

        void PoseForVictory();
        void TeleportTo(Vector3 position, bool shouldResetVelocity = false);
    }
}
