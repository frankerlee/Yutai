using Yutai.Plugins.Events;

namespace Yutai.ArcGIS.Common
{
    public interface IApplicationEvents
    {
        event MapReplacedHandler MapReplaced;

        event OnActiveHookChangedHandler OnActiveHookChanged;

        event OnApplicationClosedHandler OnApplicationClosed;

        event OnCurrentLayerChangeHandler OnCurrentLayerChange;

        event OnCurrentToolChangedHandler OnCurrentToolChanged;

        event OnDockWindowsEventHandler OnDockWindowsEvent;

        event OnHideDockWindowEventHandler OnHideDockWindowEvent;

        event OnLayerDeletedHandler OnLayerDeleted;

        event OnMapClipChangedEventHandler OnMapClipChangedEvent;

        event OnMapCloseEventHandler OnMapCloseEvent;

        event OnMapDocumentChangedEventHandler OnMapDocumentChangedEvent;

        event OnMapDocumentSaveEventHandler OnMapDocumentSaveEvent;

        event OnMessageEventHandler OnMessageEvent;

        event OnMessageEventHandlerEx OnMessageEventEx;

        event OnShowCommandStringHandler OnShowCommandString;

        event OnUpdateUIEventHandler OnUpdateUIEvent;
    }
}

