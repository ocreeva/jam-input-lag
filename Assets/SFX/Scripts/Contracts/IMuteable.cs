namespace Moyba.SFX
{
    // NOTE: muteable, not mutable; i.e. able to be muted
    public interface IMuteable
    {
        event SimpleEventHandler OnMuted;
        event SimpleEventHandler OnUnmuted;

        bool IsMuted { get; set; }
    }
}
