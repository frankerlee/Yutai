using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using stdole;
using Yutai.ArcGIS.Carto.MapCartoTemplateLib;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mef;
using Yutai.Plugins.Mvp;
using Yutai.Plugins.Printing.Forms;
using Yutai.Plugins.Printing.Menu;
using Yutai.Services.Serialization;
using Yutai.Shared;

namespace Yutai.Plugins.Printing
{
    [YutaiPlugin()]
    public class PrintingPlugin : BasePlugin
    {
        private IAppContext _context;
        private MenuGenerator _menuGenerator;
        private IPrintingConfig _printConfig;
        private MapTemplateGallery _templateGallery;
        private IGeometryArray _fenceArray;
        private IActiveViewEvents_Event _activeViewEvents;
        private bool _drawFence;
        private ISymbol _fillSymbol;
        private ISymbol _lineSymbol;
        private ITextSymbol _textSymbol;

        private List<IPrintPageInfo> _pageInfos;
        private bool _drawPage = false;
        private bool _isDeign;
        private bool _isLayout;

        public event EventHandler<FenceAddedArgs> PrintFenceAdded;

        protected override void RegisterServices(IApplicationContainer container)
        {
            CompositionRoot.Compose(container);
            _printConfig = new PrintingConfig() as IPrintingConfig;
            FileInfo newFileInfo = new FileInfo(Application.StartupPath+"\\plugins\\configs\\MapTemplate.mdb");
            _printConfig.TemplateConnectionString = newFileInfo.Exists ? newFileInfo.FullName : "";
        }

        public override void Initialize(IAppContext context)
        {
            _context = context;
            _menuGenerator = context.Container.GetInstance<MenuGenerator>();
            ISecureContext secureContext=context as ISecureContext;
            _drawFence = true;
            _isDeign = false;
            _isLayout = false;
            if (secureContext.YutaiProject != null)
            {
                XmlPlugin xmlPlugin = secureContext.YutaiProject.FindPlugin("5e933989-b5a4-4a45-a5b7-2d9ded61df0f");
                if (xmlPlugin != null)
                {
                    string fileName = xmlPlugin.ConfigXML;
                    if (string.IsNullOrEmpty(fileName))
                    {
                        _printConfig.TemplateConnectionString = BuildDefaultConnectionString();
                    }
                    else
                    {
                        fileName = FileHelper.GetFullPath(fileName);
                        _printConfig.LoadFromXml(fileName);

                    }
                }
            }
            if (string.IsNullOrEmpty(_printConfig.TemplateConnectionString))
            {
                _printConfig.TemplateConnectionString = BuildDefaultConnectionString();
            }
            _templateGallery =new MapTemplateGallery();
            _templateGallery.SetWorkspace(_printConfig.TemplateConnectionString);

            ((IAppContextEvents)_context).OnActiveHookChanged+= OnOnActiveHookChanged;
            _activeViewEvents = ((IActiveViewEvents_Event)_context.FocusMap);
            _activeViewEvents.AfterDraw += ActiveViewEventsOnAfterDraw;

            if (_fillSymbol == null)
            {
                _fillSymbol = SymbolHelper.CreateTransparentFillSymbol(Color.Blue) as ISymbol;
                _lineSymbol = SymbolHelper.CreateSimpleLineSymbol(Color.Blue, 1.5) as ISymbol;
             
            }
        }

        public List<IPrintPageInfo> PageInfos
        {
            set { _pageInfos = value; }
            get { return _pageInfos; }
        }

        public bool DrawPage
        {
            get { return _drawPage;}
            set { _drawPage = value; }
        }

        private void OnOnActiveHookChanged(object object0)
        {
            if (_activeViewEvents != null)
            {
                _activeViewEvents.AfterDraw -= ActiveViewEventsOnAfterDraw;
            }
            _activeViewEvents = ((IActiveViewEvents_Event)_context.FocusMap);
            _activeViewEvents.AfterDraw+= ActiveViewEventsOnAfterDraw;
        }

        private void ActiveViewEventsOnAfterDraw(IDisplay display, esriViewDrawPhase phase)
        {
            
            if (!_drawFence) return;
            if (_fenceArray == null || _fenceArray.Count == 0) return;
            IScreenDisplay paramScreenDisplay = ((IActiveView)_context.FocusMap).ScreenDisplay;
            paramScreenDisplay.StartDrawing(paramScreenDisplay.hDC, -2);
            for (int i = 0; i < _fenceArray.Count; i++)
            {
                IGeometry fence = _fenceArray.Element[i];
                if (fence.IsEmpty) continue;
                if (fence is IPolyline)
                {
                    paramScreenDisplay.SetSymbol(_lineSymbol);
                    paramScreenDisplay.DrawPolyline(fence);
                }
                else if (fence is IPolygon)
                {
                    paramScreenDisplay.SetSymbol(_fillSymbol);
                    paramScreenDisplay.DrawPolygon(fence);
                }
            }
            if (_drawPage && _pageInfos != null && _pageInfos.Count > 0)
            {
                IFontDisp disp = new StdFont() as IFontDisp;
                disp.Name = "Arial";
                disp.Size = new decimal(16);
               
                foreach (IPrintPageInfo pageInfo in _pageInfos)
                {
                    IGeometry pageBoundary = pageInfo.Boundary;
                    paramScreenDisplay.SetSymbol(_fillSymbol);
                    paramScreenDisplay.DrawPolygon(pageBoundary);
                    _textSymbol = SymbolHelper.CreateTextSymbol(Color.Red, disp, 16, pageInfo.PageName);
                    paramScreenDisplay.SetSymbol(_textSymbol as ISymbol);
                    if (!string.IsNullOrEmpty(pageInfo.PageName))
                    {
                        IPoint centerPoint=new ESRI.ArcGIS.Geometry.Point();
                        IEnvelope pEnv = pageBoundary.Envelope;
                        centerPoint.PutCoords((pEnv.XMin+pEnv.Width/2.0),pEnv.YMin+pEnv.Height/2.0);
                        paramScreenDisplay.DrawText(centerPoint, pageInfo.PageName);
                    }

                }
            }
            paramScreenDisplay.FinishDrawing();
        }


        private void OnViewRefreshed(IActiveView view, esriViewDrawPhase phase, object data, IEnvelope envelope)
        {
            if (phase != esriViewDrawPhase.esriViewGraphics) return;
            if (view is IPageLayout) return;
            if (!_drawFence) return;
            if (_fenceArray == null || _fenceArray.Count==0) return;
            IScreenDisplay paramScreenDisplay = ((IActiveView) _context.FocusMap).ScreenDisplay;
            paramScreenDisplay.StartDrawing(paramScreenDisplay.hDC, -2);
            for (int i = 0; i < _fenceArray.Count; i++)
            {
                IGeometry fence = _fenceArray.Element[i];
                if (!fence.IsEmpty) continue;
               if (fence is IPolyline)
                {
                    paramScreenDisplay.SetSymbol(_lineSymbol);
                    paramScreenDisplay.DrawPolyline(fence);
                }
                else if (fence is IPolygon)
                {
                    paramScreenDisplay.SetSymbol(_fillSymbol);
                    paramScreenDisplay.DrawPolygon(fence);
                }
               

            }
            paramScreenDisplay.FinishDrawing();
        }

        private string BuildDefaultConnectionString()
        {
            FileInfo fileInfo = new FileInfo(System.IO.Path.Combine(Application.StartupPath + "\\plugins\\configs\\MapTemplate.mdb"));
            if (fileInfo.Exists)
            {
                string ext = fileInfo.Extension.Substring(1);
                return string.Format("dbclient={0};gdbname={1}", ext, fileInfo.FullName);
            }
            return "";
        }
        private void FireEvent<T>(EventHandler<T> handler, T args)
        {
            if (handler != null)
            {
                handler(this, args);
            }
        }

        public override IEnumerable<IConfigPage> ConfigPages
        {
            get {
               yield return _context.Container.GetInstance<TemplateConfigPage>(); }
        }

        public  IPrintingConfig PrintingConfig { get { return _printConfig; } set { _printConfig = value; } }

        public MapTemplateGallery TemplateGallery { get { return _templateGallery; } set { _templateGallery = value; } }

        public void FireFenceAdded(FenceAddedArgs e)
        {
            AddFence(e.Fence);
            FireEvent(PrintFenceAdded,e);
        }

        private void AddFence(IGeometry fence)
        {
            if (_fenceArray == null)
            {
                _fenceArray= new GeometryArray() as IGeometryArray;
            }
            if (fence.SpatialReference == null)
            {
                fence.SpatialReference = _context.FocusMap.SpatialReference;
            }
            _fenceArray.Add(fence);
        }




        public IGeometryArray Fences
        {
            get { return _fenceArray; }
            set { _fenceArray = value; }
        }

        public bool IsDeign
        {
            get { return _isDeign; }
            set { _isDeign = value; }
        }

        public bool IsLayout
        {
            get { return _isLayout; }
            set { _isLayout = value; }
        }

        public void ClearFence()
        {
            if (_fenceArray == null) return;
            _fenceArray.RemoveAll();
        }
    }

    public class PrintLayoutSetting
    {
        public string DefaultTemplateDatabase { get; set; }
        public bool IsEditingTemplate { get; set; }
    }

    public class FenceAddedArgs : EventArgs
    {
        private IGeometry _fence;

        public FenceAddedArgs(IGeometry fence)
        {
            _fence = fence;
        }

        public IGeometry Fence
        {
            get { return _fence; }
            set { _fence=value; }
        }
    }
}