using System;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using IActiveViewEvents_Event = ESRI.ArcGIS.Carto.IActiveViewEvents_Event;

namespace Yutai.Plugins.Scene.Commands.View
{
    public class CmdLinkMapAndScene : YutaiCommand
    {
        private IAppContext _context;
        private IScenePlugin _plugin;
        private bool _isLinked;
        private bool _isInternal;
        private bool _checked;
        private IActiveViewEvents_Event _mapEvents;
        private ISceneGraphEvents_Event _sceneEvents;
      
        public override bool Enabled
        {
            get
            {
                if (_plugin == null) return false;
                if( this._plugin.Scene == null || !this._plugin.SceneVisible || (this._plugin.Scene as IBasicMap).LayerCount <= 0)
                    return false;
                if (_context.MainView.ControlType != GISControlType.MapControl) return false;
                return true;

            }
        }
        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override bool Checked
        {
            get { return _isLinked; }
        }

        public CmdLinkMapAndScene(IAppContext context, IScenePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
            _isLinked = false;
            _isInternal = true;
        }
        public override void OnCreate(object object_0)
        {
            _context = object_0 as IAppContext;
            _key = "Scene_LinkMapControl";
            _itemType = RibbonItemType.CheckBox;
            this.m_caption = "窗口联动";
            this.m_name = "Scene_LinkMapControl";
            this.m_toolTip = "窗口联动";
            this.m_category = "视图";
            m_bitmap = Properties.Resources.icon_linkage;
            _needUpdateEvent = true;

        }



        public override void OnClick()
        {
            if (_mapEvents == null)
            {
                _mapEvents=_context.ActiveView as IActiveViewEvents_Event;
                _mapEvents.AfterDraw+= MapEventsOnAfterDraw;

                _sceneEvents = _plugin.SceneView.SceneControl.SceneGraph as ISceneGraphEvents_Event;
                //_sceneEvents.AfterDraw+= SceneEventsOnAfterDraw;
            }
            _isLinked = !_isLinked;
            _isInternal = false;
        }

        private void MapEventsOnAfterDraw(IDisplay display, esriViewDrawPhase phase)
        {
            if (!_isLinked) return;
            if (_isInternal) return;

            if (phase != esriViewDrawPhase.esriViewAll) return;
            _isInternal = true;
            IEnvelope pEnvelope = _context.ActiveView.Extent as IEnvelope;
            IScene scene = _plugin.Scene;
            IActiveView pActiveView = scene as IActiveView;
            pActiveView.Extent = pEnvelope;
            _isInternal = false;
        }

        private void SceneEventsOnAfterDraw(ISceneViewer pViewer)
        {
            if (!_isLinked) return;
            if (_isInternal) return;
         
            IScene scene = _plugin.Scene;
            ISceneViewer sceneViewer = _plugin.ActiveViewer;
            ICamera camera = sceneViewer.Camera as ICamera;
            IEnvelope pEnvelope = camera.OrthoViewingExtent;
            _isInternal = true;
            IEnvelope newEnvelope = new Envelope() as IEnvelope;
            newEnvelope.PutCoords(pEnvelope.XMin, pEnvelope.YMin, pEnvelope.XMax, pEnvelope.YMax);
            _context.ActiveView.Extent = newEnvelope;
            _isInternal = false;
        }

       

        private void MapEventsOnOnExtentUpdated(object displayTransformation, bool sizeChanged, object newEnvelope)
        {
            if (!_isLinked) return;
            if (_isInternal) return;

            _isInternal = true;
            IEnvelope pEnvelope = newEnvelope as IEnvelope;
            IScene scene = _plugin.Scene;
            IActiveView pActiveView=scene as IActiveView;
            pActiveView.Extent = pEnvelope;
            _isInternal = false;
        }
        
    }
}