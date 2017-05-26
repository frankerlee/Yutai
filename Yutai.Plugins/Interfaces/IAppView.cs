using System.Windows.Forms;

namespace Yutai.Plugins.Interfaces
{
    public interface IAppView
    {
        bool ShowChildView(Form form, bool modal = true);
        bool ShowChildView(Form form, IWin32Window parent, bool modal = true);
        void Update(bool focusMap = true);
        IWin32Window MainForm { get; }
        void Lock();
        void Unlock();
    }
}