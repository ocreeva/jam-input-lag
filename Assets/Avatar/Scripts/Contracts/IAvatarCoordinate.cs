using Moyba.Contracts;

namespace Moyba.Avatar
{
    public interface IAvatarCoordinate : IReadOnlyValue<Coordinate>
    {
        Coordinate LastGroundedValue { get; }
    }
}
