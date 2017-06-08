using System.Drawing;
using ESRI.ArcGIS.Carto;
using Yutai.Plugins.Enums;

namespace Yutai.ArcGIS.Common
{
    public delegate void OnActiveHookChangedHandler(object object_0);
    public delegate void OnApplicationClosedHandler();
    public delegate void OnCurrentLayerChangeHandler(ILayer ilayer_0, ILayer ilayer_1);
    public delegate void OnCurrentToolChangedHandler(object object_0);
    public delegate void OnDockWindowsEventHandler(object object_0, Bitmap bitmap_0);
    public delegate void OnEndEditingHandler();
    public delegate void OnHideDockWindowEventHandler(object object_0);
    public delegate void OnIncrementHandler(int int_0);
    public delegate void OnLayerDeletedHandler(ILayer ilayer_0);
    public delegate void OnMapClipChangedEventHandler(object object_0);
    public delegate void OnMapCloseEventHandler();
    public delegate void OnMapDocumentChangedEventHandler();

    public delegate void OnMapDocumentSaveEventHandler(string string_0);

    public delegate void OnMessageEventHandler(object object_0);
    public delegate void OnMessageEventHandlerEx(int int_0, object object_0);
    public delegate void OnMousePostionHandler(string string_0, string string_1);
    public delegate void OnResetHandler(bool bool_0);
    public delegate void OnReSetMessageHandler(string string_0);
    public delegate void OnSetAutoProcessHandler();

    public delegate void OnSetMainMessageHandler(string string_0);

    public delegate void OnSetMaxValueHandler(int int_0);
    public delegate void OnSetMessageHandler(string string_0);
    public delegate void OnSetPostionHandler(int int_0);

    public delegate bool OnShowCommandStringHandler(string string_0, CommandTipsType commandTipsType_0);
    public delegate void OnStartEditingHandler();
    public delegate void OnUpdateUIEventHandler();
    public delegate void MapReplacedHandler(object object_0);
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