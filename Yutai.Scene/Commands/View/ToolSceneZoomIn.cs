using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Scene.Commands.View
{
    public  class ToolSceneZoomIn : YutaiTool
    {
        
        private System.Windows.Forms.Cursor cursor_0;
        

        private long long_0;

        private long long_1;

        private bool bool_0;

        private System.Drawing.Pen pen_0;

        private System.Drawing.Brush brush_0;

        private System.Drawing.Graphics graphics_0 = null;

        private IEnvelope ienvelope_0 = null;

        private IAppContext _context;
        private IScenePlugin _plugin;

        public override bool Enabled
        {
            get
            {
                if (_plugin == null) return false;
                return this._plugin.Scene != null && this._plugin.SceneVisible && (this._plugin.Scene as IBasicMap).LayerCount > 0;
            }
        }

        public override int Cursor
        {
            get
            {
                return this.cursor_0.Handle.ToInt32();
            }
        }

        [DllImport("user32")]
        public static extern int GetWindowRect(int int_0, ref System.Drawing.Rectangle rectangle_0);

        public override void OnClick()
        {
            _plugin.CurrentTool = this;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public ToolSceneZoomIn(IAppContext context, IScenePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }
        public override void OnCreate(object object_0)
        {
            _context = object_0 as IAppContext;
            _key = "Scene_ZoomIn";
            _itemType = RibbonItemType.Tool;
            this.m_category = "三维视图";
            this.m_caption = "放大";
            this.m_toolTip = "放大";
            this.m_name = "Scene_ZoomIn";
            this.m_message = "放大";
            this.m_bitmap = Properties.Resources.zoomin;
            this.cursor_0 = new System.Windows.Forms.Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Scene.Resources.Cursor.ZoomIn.cur"));
            _context = object_0 as IAppContext;
        }

        public override bool Deactivate()
        {
            return true;
        }

        public override void OnKeyDown(int int_0, int int_1)
        {
            if (this.bool_0 && int_0 == 27)
            {
                ISceneViewer activeViewer = this._plugin.ActiveViewer;
                activeViewer.Redraw(true);
                this.bool_0 = false;
            }
        }

        public override void OnMouseDown(int int_0, int int_1, int int_2, int int_3)
        {
            if (int_0 != 2)
            {
                this.bool_0 = true;
                this.long_0 = (long)int_2;
                this.long_1 = (long)int_3;
                this.graphics_0 = System.Drawing.Graphics.FromHdc((IntPtr)this._plugin.ActiveViewer.hDC);
                this.CreateEnvelope(int_2, int_3, out this.ienvelope_0);
            }
        }

        public override void OnMouseMove(int int_0, int int_1, int int_2, int int_3)
        {
            if (this.bool_0)
            {
                if (this.ienvelope_0 != null)
                {
                    this.DrawRectangle(this.ienvelope_0);
                }
                this.CreateEnvelope(int_2, int_3, out this.ienvelope_0);
                this.DrawRectangle(this.ienvelope_0);
            }
        }

        public override void OnMouseUp(int int_0, int int_1, int int_2, int int_3)
        {
            if (this.bool_0)
            {
                try
                {
                    ICamera camera = this._plugin.Camera;
                    ISceneGraph sceneGraph = this._plugin.SceneGraph;
                    IEnvelope envelope;
                    this.CreateEnvelope(int_2, int_3, out envelope);
                    if (envelope.Width == 0.0 || envelope.Height == 0.0)
                    {
                        IPoint point;
                        object obj;
                        object obj2;
                        sceneGraph.Locate(sceneGraph.ActiveViewer, int_2, int_3, esriScenePickMode.esriScenePickAll, true, out point, out obj, out obj2);
                        if (point != null)
                        {
                            camera.Target = point;
                        }
                        camera.Zoom(0.75);
                    }
                    else if (camera.ProjectionType == esri3DProjectionType.esriPerspectiveProjection)
                    {
                        camera.ZoomToRect(envelope);
                    }
                    else
                    {
                        IPoint point;
                        object obj;
                        object obj2;
                        sceneGraph.Locate(sceneGraph.ActiveViewer, (int)(envelope.XMin + envelope.Width / 2.0), (int)(envelope.YMin + envelope.Height / 2.0), esriScenePickMode.esriScenePickAll, true, out point, out obj, out obj2);
                        if (point != null)
                        {
                            camera.Target = point;
                        }
                        System.Drawing.Rectangle rectangle = default(System.Drawing.Rectangle);
                        if (ToolSceneZoomIn.GetWindowRect(this._plugin.ActiveViewer.hWnd, ref rectangle) == 0)
                        {
                            return;
                        }
                        double num = envelope.Width;
                        double num2 = envelope.Height;
                        if (num > 0.0 && num2 > 0.0)
                        {
                            num /= (double)Math.Abs(rectangle.Right - rectangle.Left);
                            num2 /= (double)Math.Abs(rectangle.Top - rectangle.Bottom);
                            if (num > num2)
                            {
                                camera.Zoom(num);
                            }
                            else
                            {
                                camera.Zoom(num2);
                            }
                        }
                        else
                        {
                            camera.Zoom(0.75);
                        }
                    }
                }
                catch
                {
                }
                ISceneViewer activeViewer = this._plugin.ActiveViewer;
                activeViewer.Redraw(true);
                if (this.graphics_0 != null)
                {
                    this.graphics_0.Dispose();
                    this.graphics_0 = null;
                }
                this.bool_0 = false;
            }
        }

        public void CreateEnvelope(int int_0, int int_1, out IEnvelope ienvelope_1)
        {
            ienvelope_1 = new Envelope() as IEnvelope;
            if ((double)this.long_0 <= (double)int_0)
            {
                ienvelope_1.XMin = (double)this.long_0;
                ienvelope_1.XMax = (double)int_0;
            }
            else
            {
                ienvelope_1.XMin = (double)int_0;
                ienvelope_1.XMax = (double)this.long_0;
            }
            if ((double)this.long_1 <= (double)int_1)
            {
                ienvelope_1.YMin = (double)this.long_1;
                ienvelope_1.YMax = (double)int_1;
            }
            else
            {
                ienvelope_1.YMin = (double)int_1;
                ienvelope_1.YMax = (double)this.long_1;
            }
        }

        public void DrawRectangle(IEnvelope ienvelope_1)
        {
            ISceneViewer activeViewer = this._plugin.ActiveViewer;
            System.Drawing.Rectangle r = default(System.Drawing.Rectangle);
            System.Drawing.Point location = default(System.Drawing.Point);
            IPoint lowerLeft = ienvelope_1.LowerLeft;
            location.X = (int)lowerLeft.X;
            location.Y = (int)lowerLeft.Y;
            int width = (int)ienvelope_1.Width;
            int height = (int)ienvelope_1.Height;
            r.Location = location;
            r.Width = width;
            r.Height = height;
            System.Windows.Forms.Control control = System.Windows.Forms.Control.FromHandle(new IntPtr(activeViewer.hWnd));
            System.Windows.Forms.ControlPaint.DrawReversibleFrame(control.RectangleToScreen(r), System.Drawing.Color.Black, System.Windows.Forms.FrameStyle.Thick);
        }
    }
}
