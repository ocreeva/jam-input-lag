namespace Moyba.Input
{
    public interface IInputManager
    {
        IAvatarInput Avatar { get; }
        IDebugInput Debug { get; }
        IGameInput Game { get; }
    }
}
