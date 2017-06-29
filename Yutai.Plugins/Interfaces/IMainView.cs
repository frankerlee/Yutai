using System;
using System.ComponentModel;
using DevExpress.Utils;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Events;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Interfaces
{
    public interface IMainView:IView
    {
        object DockingManager { get; }
        object MenuManager { get; }
        object RibbonManager { get; }
        object RibbonStatusBar { get; }
        object MapContainer { get; }
        IView View { get; }
        IMapControl3 MapControl { get; }

        AxMapControl MapControlContainer { get; }

        YutaiTool CurrentTool { get; set; }
        IActiveView ActiveView { get;}
        IMap FocusMap { get; }

        IPageLayoutControl3 PageLayoutControl { get;  }

        string ActiveViewType { get; }
        object ActiveGISControl { get; }

        void ActivateMap();
        void ActivatePageLayout();


        event EventHandler<CancelEventArgs> ViewClosing;
        event EventHandler<RenderedEventArgs> ViewUpdating;
        event EventHandler<EventArgs> ArcGISControlChanging;
        event Action BeforeShow;
        void Lock();
        void Unlock();
        void DoUpdateView(bool focusMap = true);
        void SetMapTooltip(string msg);
        string GetMapTooltip();


    }
}