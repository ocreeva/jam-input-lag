namespace Moyba.SFX
{
    public interface ISFXMusic
    {
        event SimpleEventHandler OnMuted;
        event SimpleEventHandler OnUnmuted;

        bool IsMuted { get; set; }
    }
}
