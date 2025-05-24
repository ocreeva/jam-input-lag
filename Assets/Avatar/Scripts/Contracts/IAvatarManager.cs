using Moyba.Contracts;

namespace Moyba.Avatar
{
    public interface IAvatarManager
    {
        IAvatarCoordinate Coordinate { get; }
        IReadOnlyBoolValue IsGrounded { get; }
        IReadOnlyBoolValue IsOnTarget { get; }
        IBoolValue IsOutOfBounds { get; }
        IAvatarKinematics Kinematics { get; }
    }
}
