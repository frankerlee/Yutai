using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Printing.Views
{
    public interface IAutoLayoutView : IMenuProvider
    {
        void Initialize(IAppContext context, PrintingPlugin plugin);
        void SetBuddyControl();
    }

    public enum AutoLayoutCommand
    {
        Close = 0,
    }
}
