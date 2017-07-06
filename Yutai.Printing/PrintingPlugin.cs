using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
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
            _fenceArray.Add(fence);
        }




        public IGeometryArray Fences
        {
            get { return _fenceArray; }
            set { _fenceArray = value; }
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