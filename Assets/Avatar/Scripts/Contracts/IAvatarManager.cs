using Moyba.Contracts;

namespace Moyba.Avatar
{
    public interface IAvatarManager
    {
        IReadOnlyBoolValue IsGrounded { get; }
        IAvatarKinematics Kinematics { get; }
    }
}
