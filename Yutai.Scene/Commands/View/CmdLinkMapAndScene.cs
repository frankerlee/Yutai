using System;
using System.Runtime.InteropServices;
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
                

                _sceneEvents = _plugin.SceneView.SceneControl.SceneGraph as ISceneGraphEvents_Event;
               
            }
            _isLinked = !_isLinked;
            try
            {
                if (_isLinked)
                {
                    _mapEvents.AfterDraw += MapEventsOnAfterDraw;
                    _sceneEvents.AfterDraw += SceneEventsOnAfterDraw;
                }
                else
                {
                    _mapEvents.AfterDraw -= MapEventsOnAfterDraw;
                    _sceneEvents.AfterDraw -= SceneEventsOnAfterDraw;
                }
            }
            catch (Exception ex)
            {
                
               
            }
            _isInternal = false;
        }
        [DllImport("user32")]
        public static extern int GetWindowRect(int int_0, ref System.Drawing.Rectangle rectangle_0);

        private void MapEventsOnAfterDraw(IDisplay display, esriViewDrawPhase phase)
        {
            if (!_isLinked) return;
            if (_isInternal)
            {
                if (phase == esriViewDrawPhase.esriViewInitialized)
                {
                    _isInternal = false;
                    return;
                }
                    return;
            }

            if (phase != esriViewDrawPhase.esriViewInitialized) return;
            _isInternal = true;

            IActiveView pactiveview = (IActiveView)_context.FocusMap;
            ISceneGraph pScenegraph = (ISceneGraph)_plugin.SceneGraph;
            ICamera pCamera = (ICamera)_plugin.Camera;
            pCamera.SetDefaultsMBB(pactiveview.Extent);
            pScenegraph.ActiveViewer.Redraw(true);

            //  IEnvelope pEnvelope = _context.ActiveView.Extent as IEnvelope;
            //  IPoint centerPoint = new Point();
            //  centerPoint.PutCoords(pEnvelope.XMin+pEnvelope.Width/2.0,pEnvelope.YMin+pEnvelope.Height/2.0);
            //  IPoint pTarget = new Point();
            //  pTarget.PutCoords(centerPoint.X,centerPoint.Y);
            //  pTarget.Z = 0;

            //  IPoint ptObserver = new Point();
            //  ptObserver.X = centerPoint.X;
            //  ptObserver.Y = centerPoint.Y + 90;

            //  double height = pEnvelope.Width < pEnvelope.Height ? pEnvelope.Width : pEnvelope.Height;
            //  ptObserver.Z = height;

            //  ICamera camera = _plugin.Camera;
            //  camera.Target = pTarget;
            //  camera.Observer = ptObserver;
            //  camera.Inclination = 30;
            //// camera.Azimuth = 180;
            //  _plugin.SceneGraph.RefreshViewers();



            //ISceneGraph sceneGraph = this._plugin.SceneGraph;
            //if (camera.ProjectionType == esri3DProjectionType.esriPerspectiveProjection)
            //{
            //    camera.ZoomToRect(pEnvelope);
            //}
            //else
            //{
            //    IPoint point;
            //    object obj;
            //    object obj2;
            //    sceneGraph.Locate(sceneGraph.ActiveViewer, (int)(pEnvelope.XMin + pEnvelope.Width / 2.0), (int)(pEnvelope.YMin + pEnvelope.Height / 2.0), esriScenePickMode.esriScenePickAll, true, out point, out obj, out obj2);
            //    if (point != null)
            //    {
            //        camera.Target = point;
            //    }
            //    System.Drawing.Rectangle rectangle = default(System.Drawing.Rectangle);
            //    if (GetWindowRect(this._plugin.ActiveViewer.hWnd, ref rectangle) == 0)
            //    {
            //        return;
            //    }
            //    double num = pEnvelope.Width;
            //    double num2 = pEnvelope.Height;
            //    if (num > 0.0 && num2 > 0.0)
            //    {
            //        num /= (double)Math.Abs(rectangle.Right - rectangle.Left);
            //        num2 /= (double)Math.Abs(rectangle.Top - rectangle.Bottom);
            //        if (num > num2)
            //        {
            //            camera.Zoom(num);
            //        }
            //        else
            //        {
            //            camera.Zoom(num2);
            //        }
            //    }
            //    else
            //    {
            //        camera.Zoom(0.75);
            //    }
            //}

            _isInternal = false;
        }

        private void SceneEventsOnAfterDraw(ISceneViewer pViewer)
        {
            if (!_isLinked) return;
            if (_isInternal) return;
         
            _isInternal = true;

            ICamera scenecamera = _plugin.Camera;
            IVector3D pvector3D = new Vector3D() as IVector3D;
            pvector3D.ConstructDifference(scenecamera.Observer, scenecamera.Target);
            ISphere pShere = new Sphere();
            pShere.Center = scenecamera.Target;
            pShere.Radius = scenecamera.ViewingDistance * Math.Sin(scenecamera.ViewFieldAngle * Math.PI / 180) * 0.5;
            IEnvelope penve = pShere.Envelope;
            IActiveView focusMap = (IActiveView)_context.FocusMap;
            focusMap.Extent = penve;
            focusMap.Refresh();
            
        }

       

     
        
    }
}