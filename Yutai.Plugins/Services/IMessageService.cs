using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yutai.Plugins.Services
{
    public interface IMessageService
    {
        void Warn(string message);
        void Info(string message);
        bool Ask(string message);
        DialogResult AskWithCancel(string message);
    }
}