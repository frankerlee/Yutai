namespace Yutai.Plugins.Events
{
    public interface IAppContextEvents
    {
        event OnMapDocumentChangedEventHandler OnMapDocumentChangedEvent;

        event OnMapCloseEventHandler OnMapCloseEvent;

        event OnMapClipChangedEventHandler OnMapClipChangedEvent;

        event OnDockWindowsEventHandler OnDockWindowsEvent;

        event OnHideDockWindowEventHandler OnHideDockWindowEvent;

        event OnMessageEventHandler OnMessageEvent;

        event OnMessageEventHandlerEx OnMessageEventEx;

        event OnActiveHookChangedHandler OnActiveHookChanged;

        event OnShowCommandStringHandler OnShowCommandString;

        event OnCurrentLayerChangeHandler OnCurrentLayerChange;

        event OnCurrentToolChangedHandler OnCurrentToolChanged;

        event OnLayerDeletedHandler OnLayerDeleted;

        event OnMapDocumentSaveEventHandler OnMapDocumentSaveEvent;

        event OnUpdateUIEventHandler OnUpdateUIEvent;

        event OnApplicationClosedHandler OnApplicationClosed;

        event MapReplacedHandler MapReplaced;

    }
}