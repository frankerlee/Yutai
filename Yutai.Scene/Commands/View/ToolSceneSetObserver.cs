using System;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Scene.Commands.View
{
    public  class ToolSceneSetObserver : YutaiTool
    {
        private System.Windows.Forms.Cursor cursor_0;
        private IAppContext _context;
        private IScenePlugin _plugin;


        public override bool Enabled
        {
            get
            {
                bool result;
                if (_plugin == null) return false;
                if (this._plugin.Scene == null || !this._plugin.SceneVisible || (this._plugin.Scene as IBasicMap).LayerCount <= 0) return false;

                ICamera camera = this._plugin.Camera;
                result = (camera.ProjectionType != esri3DProjectionType.esriOrthoProjection);
               
                return result;
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
        public ToolSceneSetObserver(IAppContext context, IScenePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }
        public override void OnCreate(object object_0)
        {
            _context = object_0 as IAppContext;
            _key = "Scene_SetObserver";
            _itemType = RibbonItemType.Tool;
            this.m_category = "视图";
            this.m_caption = "设置观察点";
            this.m_toolTip = "设置观察点";
            this.m_name = "Scene_SetObserver";
            this.m_message = "设置观察点";

            this.m_bitmap = Properties.Resources.observer;
          
            this.cursor_0 = new System.Windows.Forms.Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Scene.Resources.Cursor.observer.cur"));
           
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
                IPoint observer = camera.Observer;
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
                double num4 = (point.X - observer.X) / (double)num3;
                double num5 = (point.Y - observer.Y) / (double)num3;
                double num6 = (point.Z - observer.Z) / (double)num3;
                for (int i = 0; i <= num3; i++)
                {
                    point.X = observer.X + (double)i * num4;
                    point.Y = observer.Y + (double)i * num5;
                    point.Z = observer.Z + (double)i * num6;
                    camera.Observer = point;
                    sceneGraph.ActiveViewer.Redraw(true);
                }
            }
        }
    }
}