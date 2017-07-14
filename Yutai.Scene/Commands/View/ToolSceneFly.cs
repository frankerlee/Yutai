using System;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Scene.Commands.View
{
    public  class ToolSceneFly : YutaiTool
    {
        private IAppContext _context;
        private IScenePlugin _plugin;

        private bool bool_0;

        private bool bool_1 = false;

        private long long_0;

        private long long_1;

        private double double_0;

        private IPoint ipoint_0;

        private IPoint ipoint_1;

        private double double_1;

        private double double_2;

        private double double_3;

        private int int_0;

        private System.Windows.Forms.Cursor cursor_0;

        private System.Windows.Forms.Cursor cursor_1;

        public override bool Enabled
        {
            get
            {
                bool result;
                if (_plugin == null) return false;
                if (this._plugin.Scene == null || !this._plugin.SceneVisible) return false;
                if ((this._plugin.Scene as IBasicMap).LayerCount == 0) return false;
                ICamera camera = this._plugin.Camera;
                result = (camera.ProjectionType != esri3DProjectionType.esriOrthoProjection);
                
                return result;
            }
        }

        public override int Cursor
        {
            get
            {
                int result;
                if (this.bool_0)
                {
                    result = this.cursor_1.Handle.ToInt32();
                }
                else
                {
                    result = this.cursor_0.Handle.ToInt32();
                }
                return result;
            }
        }

        [DllImport("user32")]
        public static extern int SetCursor(int int_1);

        [DllImport("user32")]
        public static extern int GetClientRect(int int_1, ref System.Drawing.Rectangle rectangle_0);


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }
        public override void OnClick()
        {
            _plugin.CurrentTool = this;
        }
        public ToolSceneFly(IAppContext context, IScenePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }
        public override void OnCreate(object object_0)
        {
            _context = object_0 as IAppContext;
            _key = "Scene_Fly";
            _itemType = RibbonItemType.Tool;
            this.m_category = "视图";
            this.m_caption = "飞行";
            this.m_toolTip = "飞行";
            this.m_name = "Scene_Fly";
            this.m_message = "飞行";

            this.m_bitmap = Properties.Resources.fly;
            
            this.cursor_0 = new System.Windows.Forms.Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Scene.Resources.Cursor.fly.cur"));
            this.cursor_1 = new System.Windows.Forms.Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Scene.Resources.Cursor.fly1.cur"));
           
            this.int_0 = 0;
        }

       

        public override bool Deactivate()
        {
            return true;
        }

        public override void OnMouseUp(int int_1, int int_2, int int_3, int int_4)
        {
            if (!this.bool_0)
            {
                this.long_0 = (long)int_3;
                this.long_1 = (long)int_4;
                if (this.int_0 == 0)
                {
                    this.StartFlight();
                }
            }
            else
            {
                if (int_1 == 1)
                {
                    this.int_0++;
                }
                else if (int_1 == 2)
                {
                    this.int_0--;
                }
                if (this.int_0 == 0)
                {
                    this.EndFlight();
                }
                else
                {
                    this.StartFlight();
                }
            }
        }

        public override void OnMouseMove(int int_1, int int_2, int int_3, int int_4)
        {
            if (this.bool_0)
            {
                this.long_0 = (long)int_3;
                this.long_1 = (long)int_4;
            }
        }

        public override void OnKeyUp(int int_1, int int_2)
        {
            if (this.bool_0)
            {
                if (int_1 == 40 || int_1 == 37)
                {
                    this.double_0 /= 2.0;
                }
                else if (int_1 == 38 || int_1 == 39)
                {
                    this.double_0 *= 2.0;
                }
                else if (int_1 == 27)
                {
                    this.bool_1 = true;
                }
            }
        }

        public void StartFlight()
        {
            this.bool_0 = true;
            IEnvelope extent = this._plugin.SceneGraph.Extent;
            if (!extent.IsEmpty)
            {
                double num;
                double num2;
                double num3;
                double num4;
                extent.QueryCoords(out num, out num2, out num3, out num4);
                if (num3 - num > num4 - num2)
                {
                    this.double_0 = (num3 - num) / 100.0;
                }
                else
                {
                    this.double_0 = (num4 - num2) / 100.0;
                }
                ICamera camera = this._plugin.Camera;
                this.ipoint_0 = camera.Observer;
                this.ipoint_1 = camera.Target;
                double num5 = this.ipoint_1.X - this.ipoint_0.X;
                double num6 = this.ipoint_1.Y - this.ipoint_0.Y;
                double num7 = this.ipoint_1.Z - this.ipoint_0.Z;
                this.double_2 = Math.Atan(num7 / Math.Sqrt(num5 * num5 + num6 * num6));
                this.double_3 = Math.Atan(num6 / num5);
                this.double_1 = Math.Sqrt(num5 * num5 + num6 * num6 + num7 * num7);
                ToolSceneFly.SetCursor(this.cursor_1.Handle.ToInt32());
                this.Flight();
            }
        }

        public void Flight()
        {
            IMessageDispatcher messageDispatcher = new MessageDispatcher();
            messageDispatcher.CancelOnClick = false;
            messageDispatcher.CancelOnEscPress = true;
            ISceneGraph sceneGraph = this._plugin.SceneGraph;
            ISceneViewer activeViewer = this._plugin.ActiveViewer;
            ICamera camera = this._plugin.Camera;
            this.bool_1 = false;
            do
            {
                double num;
                double num2;
                sceneGraph.GetDrawingTimeInfo(out num, out num2);
                if (num < 0.01)
                {
                    num = 0.01;
                }
                if (num > 1.0)
                {
                    num = 1.0;
                }
                System.Drawing.Rectangle rectangle = default(System.Drawing.Rectangle);
                if (ToolSceneFly.GetClientRect(this._plugin.ActiveViewer.hWnd, ref rectangle) == 0)
                {
                    return;
                }
                double num3 = 2.0 * ((double)this.long_0 / (double)rectangle.Right) - 1.0;
                double num4 = 2.0 * ((double)this.long_1 / (double)rectangle.Bottom) - 1.0;
                this.double_2 -= num * num4 * Math.Abs(num4);
                this.double_3 -= num * num3 * Math.Abs(num3);
                if (this.double_2 > 1.4137164000000002)
                {
                    this.double_2 = 1.4137164000000002;
                }
                if (this.double_2 < -1.4137164000000002)
                {
                    this.double_2 = -1.4137164000000002;
                }
                if (this.double_3 < 0.0)
                {
                    this.double_3 += 6.283184;
                }
                if (this.double_3 > 6.283184)
                {
                    this.double_3 -= 6.283184;
                }
                double num5 = Math.Cos(this.double_2) * Math.Cos(this.double_3);
                double num6 = Math.Cos(this.double_2) * Math.Sin(this.double_3);
                double num7 = Math.Sin(this.double_2);
                this.ipoint_1.X = this.ipoint_0.X + this.double_1 * num5;
                this.ipoint_1.Y = this.ipoint_0.Y + this.double_1 * num6;
                this.ipoint_1.Z = this.ipoint_0.Z + this.double_1 * num7;
                this.ipoint_0.X = this.ipoint_0.X + num * (double)(2 ^ this.int_0) * this.double_0 * num5;
                this.ipoint_0.Y = this.ipoint_0.Y + num * (double)(2 ^ this.int_0) * this.double_0 * num6;
                this.ipoint_1.X = this.ipoint_1.X + num * (double)(2 ^ this.int_0) * this.double_0 * num5;
                this.ipoint_1.Y = this.ipoint_1.Y + num * (double)(2 ^ this.int_0) * this.double_0 * num6;
                this.ipoint_0.Z = this.ipoint_0.Z + num * (double)(2 ^ this.int_0) * this.double_0 * num7;
                this.ipoint_1.Z = this.ipoint_1.Z + num * (double)(2 ^ this.int_0) * this.double_0 * num7;
                camera.Observer = this.ipoint_0;
                camera.Target = this.ipoint_1;
                camera.RollAngle = 10.0 * num3 * Math.Abs(num3);
                activeViewer.Redraw(true);
                object obj;
                messageDispatcher.Dispatch(this._plugin.ActiveViewer.hWnd, false, out obj);
                if (this.bool_1)
                {
                    this.EndFlight();
                }
            }
            while (this.bool_0 && !this.bool_1);
            this.bool_1 = false;
        }

        public void EndFlight()
        {
            this.bool_0 = false;
            ISceneGraph sceneGraph = this._plugin.SceneGraph;
            IPoint point = new Point();
            System.Drawing.Rectangle rectangle = default(System.Drawing.Rectangle);
            if (ToolSceneFly.GetClientRect(this._plugin.ActiveViewer.hWnd, ref rectangle) != 0)
            {
                object obj;
                object obj2;
                sceneGraph.Locate(sceneGraph.ActiveViewer, rectangle.Right / 2, rectangle.Bottom / 2, esriScenePickMode.esriScenePickAll, true, out point, out obj, out obj2);
            }
            ICamera camera = this._plugin.Camera;
            if (point != null)
            {
                camera.Target = point;
                camera.Observer = this.ipoint_0;
            }
            camera.RollAngle = 0.0;
            camera.PropertiesChanged();
            ToolSceneFly.SetCursor(this.cursor_1.Handle.ToInt32());
            this.int_0 = 0;
        }

        public override void OnKeyDown(int int_1, int int_2)
        {
            if (int_1 == 27)
            {
                this.bool_1 = true;
            }
        }
    }
}