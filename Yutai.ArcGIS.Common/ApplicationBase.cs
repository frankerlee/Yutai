using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Events;

namespace Yutai.ArcGIS.Common
{
    public class ApplicationBase : IApplication, IApplicationEvents
    {
        private AxMapControl axMapControl_0 = null;
        private AxMapControl axMapControl_1 = null;
        private bool bool_0 = true;
        private bool bool_1 = false;
      
        private bool bool_2 = true;
        private bool bool_3 = true;
      
        private IActiveViewEvents_Event iactiveViewEvents_Event_0 = null;
        private IEngineSnapEnvironment iengineSnapEnvironment_0 = null;
        private IGeometry igeometry_0 = null;
        private IGeometry igeometry_1 = null;
        private IGlobeControlDefault iglobeControlDefault_0 = null;
        private ILayer ilayer_0 = null;
        private ILayer ilayer_1 = null;
        private IMap imap_0 = null;
        private IMapControl2 imapControl2_0 = null;
        private IMapControl2 imapControl2_1 = null;
        private IMapDocument imapDocument_0 = null;
        private IPageLayoutControl2 ipageLayoutControl2_0 = null;
        private ISceneControlDefault isceneControlDefault_0 = null;
        private ISelectionEnvironment iselectionEnvironment_0 = null;
        private ISnapEnvironment isnapEnvironment_0 = null;
        
        private List<AfterDraw> list_0 = new List<AfterDraw>();
     
        internal static IOperationStack m_pOperationStack;
        private static IStyleGallery m_pStyleGallery;
        private MapAndPageLayoutControls mapAndPageLayoutControls_0 = null;
        private MapAndPageLayoutControlsold mapAndPageLayoutControlsold_0 = null;
        private object object_0 = null;
        private object object_1 = null;
        private object object_2 = null;
       
        private string string_0 = "";
        private string string_1 = "";
        private ToolTip toolTip_0 = new ToolTip();

        public event MapReplacedHandler MapReplaced;

        public event OnActiveHookChangedHandler OnActiveHookChanged;

        public event OnApplicationClosedHandler OnApplicationClosed;

        public event OnCurrentLayerChangeHandler OnCurrentLayerChange;

        public event OnCurrentToolChangedHandler OnCurrentToolChanged;

        public event OnDockWindowsEventHandler OnDockWindowsEvent;

        public event OnHideDockWindowEventHandler OnHideDockWindowEvent;

        public event OnLayerDeletedHandler OnLayerDeleted;

        public event OnMapClipChangedEventHandler OnMapClipChangedEvent;

        public event OnMapCloseEventHandler OnMapCloseEvent;

        public event OnMapDocumentChangedEventHandler OnMapDocumentChangedEvent;

        public event OnMapDocumentSaveEventHandler OnMapDocumentSaveEvent;

        public event OnMessageEventHandler OnMessageEvent;

        public event OnMessageEventHandlerEx OnMessageEventEx;

        public event OnShowCommandStringHandler OnShowCommandString;

        public event OnUpdateUIEventHandler OnUpdateUIEvent;

        static ApplicationBase()
        {
            old_acctor_mc();
        }

        public ApplicationBase()
        {
            this.IsSupportZD = false;
            this.MapCommands = new List<object>();
            this.iselectionEnvironment_0 = new SelectionEnvironment();
            this.iengineSnapEnvironment_0 = new EngineEditor() as IEngineSnapEnvironment;
            IEngineEditProperties2 properties = this.iengineSnapEnvironment_0 as IEngineEditProperties2;
            properties.SnapTips = true;
            this.InitConfig();
            ApplicationRef.Application = this;
            this.UseSnap = true;
            this.IsSnapEndPoint = true;
            this.IsSnapBoundary = true;
            this.IsSnapPoint = true;
            this.IsSnapVertexPoint = true;
            this.iengineSnapEnvironment_0.SnapTolerance = 10.0;
        }

        public void AcvtiveHookChanged(object object_3)
        {
            if (this.OnActiveHookChanged != null)
            {
                this.OnActiveHookChanged(object_3);
            }
        }

        public void AddAfterDrawCallBack(AfterDraw afterDraw_0)
        {
            this.list_0.Add(afterDraw_0);
        }

        public void AddCommands(ICommand icommand_0)
        {
            foreach (object obj2 in this.MapCommands)
            {
                if ((obj2 is ICommand) && ((obj2 as ICommand).Name == icommand_0.Name))
                {
                    return;
                }
            }
            this.MapCommands.Add(icommand_0);
        }

        public void Close()
        {
            if (this.OnApplicationClosed != null)
            {
                this.OnApplicationClosed();
            }
        }

        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        private static extern bool DestroyIcon(IntPtr intptr_0);
        public void DockWindows(object object_3, Bitmap bitmap_0)
        {
            if (this.OnDockWindowsEvent != null)
            {
                this.OnDockWindowsEvent(object_3, bitmap_0);
            }
            else if (object_3 is Form)
            {
                ((Form) object_3).Show();
            }
            else if (object_3 is Control)
            {
                Form form = new Form();
                Control control = (Control) object_3;
                if (bitmap_0 != null)
                {
                    Icon icon = Icon.FromHandle(bitmap_0.GetHicon());
                    form.Icon = icon;
                }
                form.Size = control.Size;
                control.Dock = DockStyle.Fill;
                form.Controls.Add(control);
                form.ShowDialog();
            }
        }

        public void HideDockWindow(object object_3)
        {
            if (this.OnHideDockWindowEvent != null)
            {
                this.OnHideDockWindowEvent(object_3);
            }
            else if (object_3 is Form)
            {
                (object_3 as Form).Hide();
            }
        }

        public void HideToolTip()
        {
        }

        protected void InitConfig()
        {
            string path = Application.StartupPath + @"\sysconfig.cfg";
            if (File.Exists(path))
            {
                using (TextReader reader = new StreamReader(path, Encoding.Default))
                {
                    string str2 = reader.ReadLine();
                    if (str2.Length > 0)
                    {
                        string[] strArray = str2.Split(new char[] { '=' });
                        if ((strArray.Length > 1) && (strArray[0].ToLower() == "pyramiddialogset"))
                        {
                            try
                            {
                                int num = Convert.ToInt32(strArray[1]);
                                this.PyramidPromptType = (PyramidPromptType) num;
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
        }

        public void LayerDeleted(ILayer ilayer_2)
        {
            if (this.OnLayerDeleted != null)
            {
                this.OnLayerDeleted(ilayer_2);
            }
        }

        public void MapClipChanged(object object_3)
        {
            if (this.OnMapClipChangedEvent != null)
            {
                this.OnMapClipChangedEvent(object_3);
            }
        }

        public void MapDocumentChanged()
        {
            if (this.OnMapDocumentChangedEvent != null)
            {
                this.OnMapDocumentChangedEvent();
            }
        }

        public void MapDocumentSave(string string_2)
        {
            if (this.OnMapDocumentSaveEvent != null)
            {
                this.OnMapDocumentSaveEvent(string_2);
            }
        }

        private void method_0(object object_3)
        {
            try
            {
                if (this.OnActiveHookChanged != null)
                {
                    this.OnActiveHookChanged(this.Hook);
                }
                if (this.iactiveViewEvents_Event_0 != null)
                {
                    this.iactiveViewEvents_Event_0.AfterDraw-=(new IActiveViewEvents_AfterDrawEventHandler(this.method_1));
                }
                this.iactiveViewEvents_Event_0 = this.mapAndPageLayoutControlsold_0.ActiveView as IActiveViewEvents_Event;
                this.iactiveViewEvents_Event_0.AfterDraw+=(new IActiveViewEvents_AfterDrawEventHandler(this.method_1));
            }
            catch
            {
            }
        }

        private void method_1(IDisplay idisplay_0, esriViewDrawPhase esriViewDrawPhase_0)
        {
            foreach (AfterDraw draw in this.list_0)
            {
                draw(idisplay_0, esriViewDrawPhase_0);
            }
        }

        private void method_2(object object_3)
        {
            if (this.OnActiveHookChanged != null)
            {
                this.OnActiveHookChanged(this.Hook);
            }
            if (this.iactiveViewEvents_Event_0 != null)
            {
                this.iactiveViewEvents_Event_0.AfterDraw-=(new IActiveViewEvents_AfterDrawEventHandler(this.method_1));
            }
            this.iactiveViewEvents_Event_0 = this.mapAndPageLayoutControls_0.ActiveView as IActiveViewEvents_Event;
            this.iactiveViewEvents_Event_0.AfterDraw+=(new IActiveViewEvents_AfterDrawEventHandler(this.method_1));
        }

        private void method_3(object object_3)
        {
            if (this.MapReplaced != null)
            {
                this.MapReplaced(object_3);
            }
        }

        private static void old_acctor_mc()
        {
            m_pOperationStack = new OperationStackClass();
            m_pStyleGallery = null;
        }

        public void RemoveAfterDrawCallBack(AfterDraw afterDraw_0)
        {
            this.list_0.Remove(afterDraw_0);
        }

        public void ResetCurrentTool()
        {
            if (this.imapControl2_0 != null)
            {
                this.imapControl2_0.CurrentTool = null;
            }
            else if (this.ipageLayoutControl2_0 != null)
            {
                this.ipageLayoutControl2_0.CurrentTool = null;
            }
            else if (this.isceneControlDefault_0 != null)
            {
                this.isceneControlDefault_0.CurrentTool = null;
            }
            else if (this.iglobeControlDefault_0 != null)
            {
                this.iglobeControlDefault_0.CurrentTool = null;
            }
            if (this.object_0 != null)
            {
                if (this.object_0 is IMapControl2)
                {
                    (this.object_0 as IMapControl2).CurrentTool = null;
                }
                else if (this.object_0 is IPageLayoutControl)
                {
                    (this.object_0 as IPageLayoutControl).CurrentTool = null;
                }
            }
        }

        public void SetStatus(string string_2)
        {
            if (this.OnMessageEvent != null)
            {
                this.OnMessageEvent(string_2);
            }
        }

        public void SetStatus(int int_0, string string_2)
        {
            if (this.OnMessageEventEx != null)
            {
                this.OnMessageEventEx(int_0, string_2);
            }
        }

        public void SetToolTip(string string_2)
        {
            if (this.EngineSnapEnvironment != null)
            {
                if ((this.EngineSnapEnvironment as IEngineEditProperties2).SnapTips)
                {
                    if (this.ActiveMapView != null)
                    {
                        this.toolTip_0.SetToolTip(this.ActiveMapView, string_2);
                    }
                }
                else if (!string.IsNullOrEmpty(this.toolTip_0.GetToolTip(this.ActiveMapView)))
                {
                    this.toolTip_0.SetToolTip(this.ActiveMapView, "");
                }
            }
        }

        public bool ShowCommandString(string string_2, CommandTipsType commandTipsType_0)
        {
            return ((this.OnShowCommandString != null) && this.OnShowCommandString(string_2, commandTipsType_0));
        }

        public void UpdateUI()
        {
            if (this.OnUpdateUIEvent != null)
            {
                this.OnUpdateUIEvent();
            }
        }

        public Control ActiveControl { get; set; }

        public AxMapControl ActiveMapView
        {
            get
            {
                return this.axMapControl_0;
            }
            set
            {
                this.axMapControl_0 = value;
                if (value != null)
                {
                    this.Hook = value.Object;
                }
            }
        }

        public IActiveView ActiveView
        {
            get
            {
                if (this.imapControl2_0 != null)
                {
                    return this.imapControl2_0.ActiveView;
                }
                if (this.ipageLayoutControl2_0 != null)
                {
                    return this.ipageLayoutControl2_0.ActiveView;
                }
                if (this.mapAndPageLayoutControls_0 != null)
                {
                    return this.mapAndPageLayoutControls_0.ActiveView;
                }
                if (this.mapAndPageLayoutControlsold_0 != null)
                {
                    return this.mapAndPageLayoutControlsold_0.ActiveView;
                }
                return null;
            }
        }

        public Form AppMainForm { get; set; }

        public IGeometry BufferGeometry
        {
            get
            {
                return this.igeometry_1;
            }
            set
            {
                this.igeometry_1 = value;
            }
        }

        public bool CanEdited
        {
            get
            {
                return this.bool_2;
            }
            set
            {
                this.bool_2 = value;
            }
        }

        public IGeometry ClipGeometry
        {
            get
            {
                return this.igeometry_0;
            }
            set
            {
                this.igeometry_0 = value;
            }
        }

        public object ContainerHook
        {
            get
            {
                return this.object_2;
            }
            set
            {
                this.object_2 = value;
            }
        }

        public ILayer CurrentLayer
        {
            get
            {
                return this.ilayer_1;
            }
            set
            {
                if (this.ilayer_1 != value)
                {
                    ILayer layer = this.ilayer_1;
                    this.ilayer_1 = value;
                    if (this.OnCurrentLayerChange != null)
                    {
                        this.OnCurrentLayerChange(layer, this.ilayer_1);
                    }
                    layer = null;
                }
            }
        }

        public ITool CurrentTool
        {
            get
            {
                if (this.imapControl2_0 != null)
                {
                    return this.imapControl2_0.CurrentTool;
                }
                if (this.ipageLayoutControl2_0 != null)
                {
                    return this.ipageLayoutControl2_0.CurrentTool;
                }
                if (this.isceneControlDefault_0 != null)
                {
                    return this.isceneControlDefault_0.CurrentTool;
                }
                if (this.iglobeControlDefault_0 != null)
                {
                    return this.iglobeControlDefault_0.CurrentTool;
                }
                if (this.mapAndPageLayoutControls_0 != null)
                {
                    if (this.mapAndPageLayoutControls_0.ActiveControl is IMapControl2)
                    {
                        return (this.mapAndPageLayoutControls_0.ActiveControl as IMapControl2).CurrentTool;
                    }
                    return (this.mapAndPageLayoutControls_0.ActiveControl as IPageLayoutControl2).CurrentTool;
                }
                return null;
            }
            set
            {
                if (this.imapControl2_0 != null)
                {
                    this.imapControl2_0.CurrentTool = value;
                }
                else if (this.ipageLayoutControl2_0 != null)
                {
                    this.ipageLayoutControl2_0.CurrentTool = value;
                }
                else if (this.isceneControlDefault_0 != null)
                {
                    this.isceneControlDefault_0.CurrentTool = value;
                }
                else if (this.iglobeControlDefault_0 != null)
                {
                    this.iglobeControlDefault_0.CurrentTool = value;
                }
                else if (this.mapAndPageLayoutControls_0 != null)
                {
                    if (this.mapAndPageLayoutControls_0.ActiveControl is IMapControl2)
                    {
                        (this.mapAndPageLayoutControls_0.ActiveControl as IMapControl2).CurrentTool = value;
                    }
                    else
                    {
                        (this.mapAndPageLayoutControls_0.ActiveControl as IPageLayoutControl2).CurrentTool = value;
                    }
                }
                if (this.OnCurrentToolChanged != null)
                {
                    this.OnCurrentToolChanged(value);
                }
            }
        }

        public bool DrawBuffer
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        public IEngineSnapEnvironment EngineSnapEnvironment
        {
            get
            {
                return this.iengineSnapEnvironment_0;
            }
        }

        public IMap FocusMap
        {
            get
            {
                if (this.imapControl2_0 != null)
                {
                    return this.imapControl2_0.Map;
                }
                if (this.ipageLayoutControl2_0 != null)
                {
                    return this.ipageLayoutControl2_0.ActiveView.FocusMap;
                }
                if (this.mapAndPageLayoutControls_0 != null)
                {
                    if (this.mapAndPageLayoutControls_0.ActiveControl is IMapControl2)
                    {
                        return (this.mapAndPageLayoutControls_0.ActiveControl as IMapControl2).Map;
                    }
                    return (this.mapAndPageLayoutControls_0.ActiveControl as IPageLayoutControl2).ActiveView.FocusMap;
                }
                if (this.mapAndPageLayoutControlsold_0 != null)
                {
                    if (this.mapAndPageLayoutControlsold_0.ActiveControl is IMapControl2)
                    {
                        return (this.mapAndPageLayoutControlsold_0.ActiveControl as IMapControl2).Map;
                    }
                    return (this.mapAndPageLayoutControlsold_0.ActiveControl as IPageLayoutControl2).ActiveView.FocusMap;
                }
                return null;
            }
        }

        public object Hook
        {
            get
            {
                if (this.imapControl2_0 != null)
                {
                    return this.imapControl2_0;
                }
                if (this.ipageLayoutControl2_0 != null)
                {
                    return this.ipageLayoutControl2_0;
                }
                if (this.isceneControlDefault_0 != null)
                {
                    return this.isceneControlDefault_0;
                }
                if (this.iglobeControlDefault_0 != null)
                {
                    return this.iglobeControlDefault_0;
                }
                if (this.mapAndPageLayoutControls_0 != null)
                {
                    return this.mapAndPageLayoutControls_0;
                }
                if (this.mapAndPageLayoutControlsold_0 != null)
                {
                    return this.mapAndPageLayoutControlsold_0;
                }
                return null;
            }
            set
            {
                try
                {
                    if (this.iactiveViewEvents_Event_0 != null)
                    {
                        this.iactiveViewEvents_Event_0.AfterDraw-=(new IActiveViewEvents_AfterDrawEventHandler(this.method_1));
                    }
                    if (value == null)
                    {
                        this.iglobeControlDefault_0 = null;
                        this.isceneControlDefault_0 = null;
                        this.ipageLayoutControl2_0 = null;
                        this.imapControl2_0 = null;
                        this.mapAndPageLayoutControls_0 = null;
                        this.mapAndPageLayoutControlsold_0 = null;
                        this.iactiveViewEvents_Event_0 = null;
                    }
                    else if (value is IMapControl2)
                    {
                        this.iglobeControlDefault_0 = null;
                        this.isceneControlDefault_0 = null;
                        this.ipageLayoutControl2_0 = null;
                        this.imapControl2_0 = (IMapControl2) value;
                        this.mapAndPageLayoutControls_0 = null;
                        this.iactiveViewEvents_Event_0 = this.imapControl2_0.ActiveView as IActiveViewEvents_Event;
                        try
                        {
                            this.iactiveViewEvents_Event_0.AfterDraw+=(new IActiveViewEvents_AfterDrawEventHandler(this.method_1));
                        }
                        catch
                        {
                        }
                        (value as IMapControlEvents2_Event).OnMapReplaced+=(new IMapControlEvents2_OnMapReplacedEventHandler(this.method_3));
                    }
                    else if (value is IPageLayoutControl2)
                    {
                        this.iglobeControlDefault_0 = null;
                        this.isceneControlDefault_0 = null;
                        this.imapControl2_0 = null;
                        this.ipageLayoutControl2_0 = (IPageLayoutControl2) value;
                        this.iactiveViewEvents_Event_0 = this.ipageLayoutControl2_0.ActiveView as IActiveViewEvents_Event;
                        this.iactiveViewEvents_Event_0.AfterDraw+=(new IActiveViewEvents_AfterDrawEventHandler(this.method_1));
                        this.mapAndPageLayoutControls_0 = null;
                    }
                    else if (value is ISceneControlDefault)
                    {
                        this.iglobeControlDefault_0 = null;
                        this.imapControl2_0 = null;
                        this.ipageLayoutControl2_0 = null;
                        this.isceneControlDefault_0 = value as ISceneControlDefault;
                        this.mapAndPageLayoutControls_0 = null;
                    }
                    else if (value is IGlobeControlDefault)
                    {
                        this.imapControl2_0 = null;
                        this.ipageLayoutControl2_0 = null;
                        this.isceneControlDefault_0 = null;
                        this.iglobeControlDefault_0 = value as IGlobeControlDefault;
                        this.mapAndPageLayoutControls_0 = null;
                    }
                    else if (value is MapAndPageLayoutControls)
                    {
                        this.imapControl2_0 = null;
                        this.ipageLayoutControl2_0 = null;
                        this.isceneControlDefault_0 = null;
                        this.iglobeControlDefault_0 = null;
                        this.mapAndPageLayoutControls_0 = value as MapAndPageLayoutControls;
                        this.iactiveViewEvents_Event_0 = this.mapAndPageLayoutControls_0.ActiveView as IActiveViewEvents_Event;
                        this.iactiveViewEvents_Event_0.AfterDraw+=(new IActiveViewEvents_AfterDrawEventHandler(this.method_1));
                        this.mapAndPageLayoutControls_0.OnActiveHookChanged += new OnActiveHookChangedHandler(this.method_2);
                    }
                    else if (value is MapAndPageLayoutControlsold)
                    {
                        this.imapControl2_0 = null;
                        this.ipageLayoutControl2_0 = null;
                        this.isceneControlDefault_0 = null;
                        this.iglobeControlDefault_0 = null;
                        this.mapAndPageLayoutControlsold_0 = value as MapAndPageLayoutControlsold;
                        this.iactiveViewEvents_Event_0 = this.mapAndPageLayoutControlsold_0.ActiveView as IActiveViewEvents_Event;
                        this.iactiveViewEvents_Event_0.AfterDraw+=(new IActiveViewEvents_AfterDrawEventHandler(this.method_1));
                        this.mapAndPageLayoutControlsold_0.OnActiveHookChanged += new OnActiveHookChangedHandler(this.method_0);
                    }
                    if (!this.bool_1)
                    {
                    }
                    if (!(this.bool_3 || (this.OnActiveHookChanged == null)))
                    {
                        this.OnActiveHookChanged(value);
                    }
                    this.bool_3 = false;
                    this.UpdateUI();
                }
                catch
                {
                }
            }
        }

        public bool IsClose { get; set; }

        public bool IsInEdit
        {
            get
            {
                return this.bool_1;
            }
            set
            {
                this.bool_1 = value;
                if (this.bool_1)
                {
                    this.ilayer_0 = this.ilayer_1;
                    this.imap_0 = this.FocusMap;
                    this.bool_2 = true;
                }
                else
                {
                    this.ilayer_0 = null;
                    this.imap_0 = null;
                    this.bool_2 = true;
                }
            }
        }

        public static bool IsPrintForm
        {
            get; set;
        }

        public bool IsSnapBoundary
        {
            get; set;
        }

        public bool IsSnapEndPoint
        {
            get; set;
        }

        public bool IsSnapIntersectionPoint
        {
            get; set;
        }

        public bool IsSnapMiddlePoint
        {
            get; set;
        }

        public bool IsSnapPoint
        {
            get; set;
        }

        public bool IsSnapSketch
        {
            get; set;
        }

        public bool IsSnapVertexPoint
        {
            get; set;
        }

        public bool IsSupportZD
        {
            get; set;
        }

        public object MainHook
        {
            get
            {
                return this.object_0;
            }
            set
            {
                this.object_0 = value;
            }
        }

        public List<object> MapCommands
        {
            get; set;
        }

        public string MapDocName
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public IMapDocument MapDocument
        {
            get
            {
                return this.imapDocument_0;
            }
            set
            {
                this.imapDocument_0 = value;
            }
        }

        public IMapControl2 NavigationMapControl
        {
            get
            {
                return this.imapControl2_1;
            }
            set
            {
                this.imapControl2_1 = value;
            }
        }

        public AxMapControl NavitorMapView
        {
            get
            {
                return this.axMapControl_1;
            }
            set
            {
                this.axMapControl_1 = value;
                if (value != null)
                {
                    this.NavigationMapControl = value.Object as IMapControl2;
                }
            }
        }

        public IOperationStack OperationStack
        {
            get
            {
                return m_pOperationStack;
            }
        }

        public IPageLayout PageLayout
        {
            get
            {
                if (this.ipageLayoutControl2_0 != null)
                {
                    return this.ipageLayoutControl2_0.PageLayout;
                }
                if ((this.mapAndPageLayoutControls_0 != null) && (this.mapAndPageLayoutControls_0.ActiveControl is IPageLayoutControl2))
                {
                    return (this.mapAndPageLayoutControls_0.ActiveControl as IPageLayoutControl2).PageLayout;
                }
                return null;
            }
        }

        public string PaintStyleName
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public PyramidPromptType PyramidPromptType
        {
            get; set;
        }

        public object SecondaryHook
        {
            get
            {
                return this.object_1;
            }
            set
            {
                this.object_1 = value;
            }
        }

        public IWorkspace SelectedWorkspace
        {
            get; set;
        }

        public ISelectionEnvironment SelectionEnvironment
        {
            get
            {
                return this.iselectionEnvironment_0;
            }
        }

        public ISnapEnvironment SnapEnvironment
        {
            get
            {
                return this.isnapEnvironment_0;
            }
        }

        public double SnapTolerance
        {
            get
            {
                return this.isnapEnvironment_0.SnapTolerance;
            }
            set
            {
                this.isnapEnvironment_0.SnapTolerance = value;
            }
        }

        public static IStyleGallery StyleGallery
        {
            get
            {
                return m_pStyleGallery;
            }
            set
            {
                m_pStyleGallery = value;
            }
        }

        public double Tolerance
        {
            get
            {
                return this.isnapEnvironment_0.SnapTolerance;
            }
            set
            {
                this.isnapEnvironment_0.SnapTolerance = value;
            }
        }

        public bool UpdateClickTool
        {
            get; set;
        }

        public bool UseSnap
        {
            get; set;
        }
    }
}

