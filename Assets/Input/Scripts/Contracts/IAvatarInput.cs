namespace Moyba.Input
{
    public interface IAvatarInput
    {
        event ValueEventHandler<int> OnSpeedChanged;
        event ValueEventHandler<int> OnStrafeChanged;
        event ValueEventHandler<int> OnTurnChanged;

        int Speed { get; }
        int Strafe { get; }
        int Turn { get; }
    }
}
