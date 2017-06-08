using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;
using System.Windows.Forms;

namespace Yutai.Services.Serialization
{
    public interface ISecureContext : IAppContext
    {
        IPluginManager PluginManager { get; }
        Control GetDockPanelObject(string dockName);

        XmlProject YutaiProject { get; set; }
    }
}
