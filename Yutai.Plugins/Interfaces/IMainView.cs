using System;
using System.ComponentModel;
using DevExpress.Utils;
using ESRI.ArcGIS.Controls;
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
        IMapControl2 MapControl { get; }

        AxMapControl MapControlContainer { get; }

        event EventHandler<CancelEventArgs> ViewClosing;
        event EventHandler<RenderedEventArgs> ViewUpdating;
        event Action BeforeShow;
        void Lock();
        void Unlock();
        void DoUpdateView(bool focusMap = true);

     

        void SetMapTooltip(string msg);
        string GetMapTooltip();


    }
}