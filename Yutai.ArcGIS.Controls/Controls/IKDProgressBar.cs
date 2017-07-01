namespace Yutai.ArcGIS.Controls.Controls
{
    public interface IKDProgressBar
    {
        void KDClose();
        void KDHide();
        void KDRefresh();
        void KDShow();

        string KDTitle { get; set; }
    }
}