using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using stdole;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mef;
using Yutai.Plugins.Mvp;
using Yutai.Plugins.Scene.Menu;
using Yutai.Plugins.Scene.Views;

namespace Yutai.Plugins.Scene
{
    [YutaiPlugin()]
    public class ScenePlugin : BasePlugin,IScenePlugin
    {
        private IAppContext _context;
        private MenuGenerator _menuGenerator;
        private SceneViewService _sceneViewService;
        private object _hook;
        private ILayer _currentLayer;
        private YutaiTool _currentTool;
        private IStyleGallery _styleGallery;
        


        protected override void RegisterServices(IApplicationContainer container)
        {
            CompositionRoot.Compose(container);
        }

        public override void Initialize(IAppContext context)
        {
            _context = context;
            _menuGenerator = context.Container.GetInstance<MenuGenerator>();
            _sceneViewService = context.Container.GetInstance<SceneViewService>();
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
               yield break;}
        }


        public ISceneView SceneView
        {
            get
            {
                if (_sceneViewService == null) return null;
                return _sceneViewService.SceneView;
            }
        }

        public void CloseScene()
        {
            _sceneViewService.Hide();
        }

        public void ShowScene()
        {
            _sceneViewService.Show();
        }

        public ILayer CurrentLayer
        {
            get { return _currentLayer; }
            set { _currentLayer = value; }
        }

        public YutaiTool CurrentTool
        {
            get { return _currentTool; }
            set
            {
                _sceneViewService.SceneView.SceneControl.CurrentTool = value;
                _currentTool = value; }
        }

        public IStyleGallery StyleGallery
        {
            get { return _styleGallery; }
        }

        public bool SceneVisible
        {
            get
            {
                if (_sceneViewService == null) return false;
                return _sceneViewService.Visible;
            }
        }

        public void MapDocumentChanged()
        {
            
        }

        public void ResetCurrentTool()
        {
            if (_sceneViewService == null) return ;
            _sceneViewService.SceneView.SceneControl.CurrentTool = null;
        }

        public void SetStatus(string statusMsg)
        {
           _context.SetStatus(statusMsg);
        }

        public void SetStatus(int locIndex, string statusMsg)
        {
            _context.SetStatus(statusMsg);
        }

        public void UpdateUI()
        {
            _context.UpdateUI();
        }

        public void SetCurrentTool(ITool tool)
        {
            _sceneViewService.SceneView.SceneControl.CurrentTool = tool;
        }

        public object Hook
        {
            get { return this; }
            set { _hook = value; }
        }

        public IScene Scene
        {
            get
            {
                if (_sceneViewService == null) return null;
                return _sceneViewService.SceneView.Scene;
            }
        }

        public ISceneGraph SceneGraph
        {
            get
            {
                if (_sceneViewService == null) return null;
                return _sceneViewService.SceneView.SceneControl.SceneGraph;
            }
        }

        public ISceneViewer ActiveViewer
        {
            get
            {
                if (_sceneViewService == null) return null;
                return _sceneViewService.SceneView.SceneControl.SceneViewer;
            }
        }

        public ICamera Camera
        {
            get { if (_sceneViewService == null) return null;
                return _sceneViewService.SceneView.SceneControl.Camera; }
        }
    }

    public interface IScenePlugin:ISceneHookHelper
    {
        ISceneView SceneView { get; }

        void CloseScene();
        void ShowScene();

        ILayer CurrentLayer
        {
            get;
            set;
        }

        YutaiTool CurrentTool
        {
            get;
            set;
        }

        IStyleGallery StyleGallery
        {
            get;
        }

      
        bool SceneVisible { get; }

        void MapDocumentChanged();

        void ResetCurrentTool();

        void SetStatus(string statusMsg);

        void SetStatus(int locIndex, string statusMsg);

        void UpdateUI();
        void SetCurrentTool(ITool tool);
    }
   
}