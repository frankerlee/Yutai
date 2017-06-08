using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Framework
{
    public interface IFramework
    {
        event OnActiveHookChangedHandler OnActiveHookChanged;

        event OnDockWindowsEventHandler OnDockWindowsEvent;

        event OnFrameworkClosedHandler OnFrameworkClosed;

        event OnMapDocumentChangedEventHandler OnMapDocumentChangedEvent;

        event OnMapDocumentSaveEventHandler OnMapDocumentSaveEvent;

        void Excute(string string_0);
        void HandleCommand(ICommand icommand_0);
        void HandleCommandLine(string string_0);
        void Init();
        void LoadCommand(string string_0);
        void UpdateUI();

        Control ActiveControl { get; set; }

        IApplication Application { get; }

        IBarManager BarManager { get; set; }

        string CommandLines { set; }

        ICommandLineWindows CommandLineWindows { get; set; }

        object ContainerHook { get; set; }

        ILayer CurrentLayer { get; set; }

        IDockManagerWrap DockManager { get; set; }

        object Hook { get; set; }

        Form MainForm { get; set; }

        object MainHook { get; set; }

        IMapControl2 NavigationMap { get; set; }

        object SecondaryHook { get; set; }

        IStyleGallery StyleGallery { set; }
        IAppContext AppContext { get; set; }
    }
}

