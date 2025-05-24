namespace Moyba.UI
{
    public interface IUIManager
    {
        void Hide(Overlay overlay, bool immediately = false);
        void Show(Overlay overlay, bool immediately = false);
    }
}
