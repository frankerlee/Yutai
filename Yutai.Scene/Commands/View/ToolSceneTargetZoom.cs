using System;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Scene.Commands.View
{
    public sealed class ToolSceneTargetZoom : YutaiTool
    {
        private System.Windows.Forms.Cursor cursor_0;

        private IAppContext _context;
        private IScenePlugin _plugin;

        public override bool Enabled
        {
            get
            {
                if (_plugin == null) return false;
                if (this._plugin.Scene == null || !this._plugin.SceneVisible || (this._plugin.Scene as IBasicMap).LayerCount <= 0) return false;
                return true;
            }
        }

        public override int Cursor
        {
            get
            {
                return this.cursor_0.Handle.ToInt32();
            }
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }
        public override void OnClick()
        {
            _plugin.CurrentTool = this;
        }

        public ToolSceneTargetZoom(IAppContext context, IScenePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }
        public override void OnCreate(object object_0)
        {
            _context = object_0 as IAppContext;
            _key = "Scene_TargetZoom";
            _itemType = RibbonItemType.Tool;
            this.m_category = "视图";
            this.m_caption = "缩放到目标";
            this.m_toolTip = "缩放到目标";
            this.m_name = "Scene_TargetZoom";
            this.m_message = "缩放到目标";

            this.m_bitmap = Properties.Resources.targetzoom;
            
            this.cursor_0 = new System.Windows.Forms.Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Scene.Resources.Cursor.targetzoom.cur"));
            
        }

      

        public override bool Deactivate()
        {
            return true;
        }

        public override void OnMouseUp(int int_0, int int_1, int int_2, int int_3)
        {
            ISceneGraph sceneGraph = this._plugin.SceneGraph;
            IPoint point;
            object obj;
            object obj2;
            sceneGraph.Locate(sceneGraph.ActiveViewer, int_2, int_3, esriScenePickMode.esriScenePickAll, true, out point, out obj, out obj2);
            if (point != null)
            {
                ICamera camera = this._plugin.Camera;
                if (camera.ProjectionType == esri3DProjectionType.esriOrthoProjection)
                {
                    camera.Target = point;
                    camera.Zoom(0.25);
                    sceneGraph.ActiveViewer.Redraw(true);
                }
                else
                {
                    IPoint target = camera.Target;
                    IPoint observer = camera.Observer;
                    camera.Target = point;
                    camera.PolarUpdate(0.1, 0.0, 0.0, true);
                    IPoint observer2 = camera.Observer;
                    double num;
                    double num2;
                    sceneGraph.GetDrawingTimeInfo(out num, out num2);
                    if (num < 0.01)
                    {
                        num = 0.01;
                    }
                    int num3 = (int)(2.0 / num);
                    if (num3 < 1)
                    {
                        num3 = 1;
                    }
                    if (num3 > 60)
                    {
                        num3 = 60;
                    }
                    double num4 = (observer2.X - observer.X) / (double)num3;
                    double num5 = (observer2.Y - observer.Y) / (double)num3;
                    double num6 = (observer2.Z - observer.Z) / (double)num3;
                    double num7 = (point.X - target.X) / (double)num3;
                    double num8 = (point.Y - target.Y) / (double)num3;
                    double num9 = (point.Z - target.Z) / (double)num3;
                    for (int i = 0; i < num3; i++)
                    {
                        observer2.X = observer.X + (double)i * num4;
                        observer2.Y = observer.Y + (double)i * num5;
                        observer2.Z = observer.Z + (double)i * num6;
                        point.X = target.X + (double)i * num7;
                        point.Y = target.Y + (double)i * num8;
                        point.Z = target.Z + (double)i * num9;
                        camera.Observer = observer2;
                        camera.Target = point;
                        sceneGraph.ActiveViewer.Redraw(true);
                    }
                }
            }
        }
    }
}