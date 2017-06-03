using System;
using System.ComponentModel;
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
        object StatusBar { get; }
        object MapContainer { get; }
        IView View { get; }
        IMapControl2 MapControl { get; }
      
        event EventHandler<CancelEventArgs> ViewClosing;
        event EventHandler<RenderedEventArgs> ViewUpdating;
        event Action BeforeShow;
        void Lock();
        void Unlock();
        void DoUpdateView(bool focusMap = true);

    }
}