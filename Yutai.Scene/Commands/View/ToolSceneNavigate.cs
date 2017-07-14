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
    public  class ToolSceneNavigate : YutaiTool
    {
        private IAppContext _context;
        private IScenePlugin _plugin;

        private bool bool_0;

        private bool bool_1;

        private bool bool_2 = false;

        private long long_0;

        private long long_1;

        private bool bool_3;

        private double double_0;

        private System.Windows.Forms.Cursor cursor_0;

        private System.Windows.Forms.Cursor cursor_1;

        private System.Windows.Forms.Cursor cursor_2;

        private System.Windows.Forms.Cursor cursor_3;

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
                int result;
                if (this.bool_1)
                {
                    result = this.cursor_3.Handle.ToInt32();
                }
                else
                {
                    result = this.cursor_0.Handle.ToInt32();
                }
                return result;
            }
        }

        [DllImport("user32")]
        public static extern int SetCursor(int int_0);

        [DllImport("user32")]
        public static extern int GetClientRect(int int_0, ref System.Drawing.Rectangle rectangle_0);
        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }
        public override void OnClick()
        {
            _plugin.CurrentTool = this;
        }
        public ToolSceneNavigate(IAppContext context, IScenePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }
        public override void OnCreate(object object_0)
        {
            _context = object_0 as IAppContext;
            _key = "Scene_Navigate";
            _itemType = RibbonItemType.Tool;
            this.m_category = "视图";
            this.m_caption = "导航";
            this.m_toolTip = "导航";
            this.m_name = "Scene_Navigate";
            this.m_message = "导航";

            this.m_bitmap = Properties.Resources.Navigation;
           
            this.cursor_0 = new System.Windows.Forms.Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Scene.Resources.Cursor.navigation.cur"));
            this.cursor_1 = new System.Windows.Forms.Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Scene.Resources.Cursor.movehand.cur"));
            this.cursor_2 = new System.Windows.Forms.Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Scene.Resources.Cursor.ZOOMINOUT.CUR"));
            this.cursor_3 = new System.Windows.Forms.Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Scene.Resources.Cursor.gesture.cur"));

            this.bool_1 = this._plugin==null?false: this._plugin.SceneGraph.GestureEnabled;
            this.bool_3 = false;
        }

      
        public override bool Deactivate()
        {
            return true;
        }

        public override void OnMouseDown(int int_0, int int_1, int int_2, int int_3)
        {
            if (int_0 == 3)
            {
                this.bool_2 = true;
            }
            else
            {
                this.bool_0 = true;
                this.long_0 = (long)int_2;
                this.long_1 = (long)int_3;
            }
        }

        public override void OnMouseMove(int int_0, int int_1, int int_2, int int_3)
        {
            if (this._plugin.Scene.LayerCount != 0 && this.bool_0 && ((long)int_2 - this.long_0 != 0L || (long)int_3 - this.long_1 != 0L))
            {
                long num = (long)int_2 - this.long_0;
                long num2 = (long)int_3 - this.long_1;
                if (int_0 == 2)
                {
                    ToolSceneNavigate.SetCursor(this.cursor_2.Handle.ToInt32());
                    if (num2 < 0L)
                    {
                        this._plugin.Camera.Zoom(1.1);
                    }
                    else if (num2 > 0L)
                    {
                        this._plugin.Camera.Zoom(0.9);
                    }
                }
                if (int_0 == 3)
                {
                    ToolSceneNavigate.SetCursor(this.cursor_1.Handle.ToInt32());
                    IPoint point = new Point();
                    point.PutCoords((double)this.long_0, (double)this.long_1);
                    IPoint point2 = new Point();
                    point2.PutCoords((double)int_2, (double)int_3);
                    this._plugin.Camera.Pan(point, point2);
                }
                if (int_0 == 1)
                {
                    if (!this.bool_1)
                    {
                        this._plugin.Camera.PolarUpdate(1.0, (double)num, (double)num2, true);
                    }
                    else if (this.bool_3)
                    {
                        this._plugin.Camera.PolarUpdate(1.0, (double)num, (double)num2, true);
                    }
                    else
                    {
                        System.Drawing.Rectangle rectangle = default(System.Drawing.Rectangle);
                        ToolSceneNavigate.GetClientRect(this._plugin.ActiveViewer.hWnd, ref rectangle);
                        if (num < 0L)
                        {
                            this.double_0 = (180.0 / (double)rectangle.Right - (double)rectangle.Left) * (double)(num - (long)this._plugin.ActiveViewer.GestureSensitivity);
                        }
                        else
                        {
                            this.double_0 = (180.0 / (double)rectangle.Right - (double)rectangle.Left) * (double)(num + (long)this._plugin.ActiveViewer.GestureSensitivity);
                        }
                        this.StartSpin();
                    }
                }
                this.long_0 = (long)int_2;
                this.long_1 = (long)int_3;
                this._plugin.ActiveViewer.Redraw(true);
            }
        }

        public override void OnMouseUp(int int_0, int int_1, int int_2, int int_3)
        {
            if (this.bool_1)
            {
                ToolSceneNavigate.SetCursor(this.cursor_3.Handle.ToInt32());
            }
            else
            {
                ToolSceneNavigate.SetCursor(this.cursor_0.Handle.ToInt32());
            }
            try
            {
            }
            catch
            {
            }
        }

        public void StartSpin()
        {
            this.bool_3 = true;
            IMessageDispatcher messageDispatcher = new MessageDispatcher();
            messageDispatcher.CancelOnClick = false;
            messageDispatcher.CancelOnEscPress = true;
            do
            {
                this._plugin.Camera.PolarUpdate(1.0, this.double_0, 0.0, true);
                this._plugin.ActiveViewer.Redraw(true);
                object obj;
                messageDispatcher.Dispatch(this._plugin.ActiveViewer.hWnd, false, out obj);
                if (this.bool_2)
                {
                    this.bool_3 = false;
                }
            }
            while (!this.bool_2);
            this.bool_2 = false;
        }

        public override void OnKeyDown(int int_0, int int_1)
        {
            if (int_0 == 27)
            {
                this.bool_2 = true;
                ToolSceneNavigate.SetCursor(this.cursor_0.Handle.ToInt32());
            }
            switch (int_1)
            {
                case 1:
                    ToolSceneNavigate.SetCursor(this.cursor_1.Handle.ToInt32());
                    break;
                case 2:
                    ToolSceneNavigate.SetCursor(this.cursor_2.Handle.ToInt32());
                    break;
                case 3:
                    if (!this.bool_3)
                    {
                        if (this.bool_1)
                        {
                            this._plugin.ActiveViewer.GestureEnabled = false;
                            this.bool_1 = false;
                            ToolSceneNavigate.SetCursor(this.cursor_0.Handle.ToInt32());
                        }
                        else
                        {
                            this._plugin.ActiveViewer.GestureEnabled = true;
                            this.bool_1 = true;
                            ToolSceneNavigate.SetCursor(this.cursor_3.Handle.ToInt32());
                        }
                    }
                    break;
            }
        }

        public override void OnKeyUp(int int_0, int int_1)
        {
            if (int_1 == 1)
            {
                switch (int_0)
                {
                    case 37:
                        this._plugin.Camera.Move(esriCameraMovementType.esriCameraMoveRight, 0.01);
                        break;
                    case 38:
                        this._plugin.Camera.Move(esriCameraMovementType.esriCameraMoveDown, 0.01);
                        break;
                    case 39:
                        this._plugin.Camera.Move(esriCameraMovementType.esriCameraMoveLeft, 0.01);
                        break;
                    case 40:
                        this._plugin.Camera.Move(esriCameraMovementType.esriCameraMoveUp, 0.01);
                        break;
                    default:
                        return;
                }
            }
            else if (int_1 == 2)
            {
                switch (int_0)
                {
                    case 37:
                        this._plugin.Camera.HTurnAround(-1.0);
                        break;
                    case 38:
                        this._plugin.Camera.Move(esriCameraMovementType.esriCameraMoveAway, 0.01);
                        break;
                    case 39:
                        this._plugin.Camera.HTurnAround(1.0);
                        break;
                    case 40:
                        this._plugin.Camera.Move(esriCameraMovementType.esriCameraMoveToward, 0.01);
                        break;
                    default:
                        return;
                }
            }
            else
            {
                double num = 5.0;
                double num2 = 2.0;
                double num3 = 2.0;
                switch (int_0)
                {
                    case 33:
                        this.double_0 *= 1.1;
                        break;
                    case 34:
                        this.double_0 /= 1.1;
                        break;
                    case 35:
                    case 36:
                        return;
                    case 37:
                        this._plugin.Camera.PolarUpdate(1.0, num3 * num, 0.0, true);
                        break;
                    case 38:
                        this._plugin.Camera.PolarUpdate(1.0, 0.0, -num2 * num, true);
                        break;
                    case 39:
                        this._plugin.Camera.PolarUpdate(1.0, -num3 * num, 0.0, true);
                        break;
                    case 40:
                        this._plugin.Camera.PolarUpdate(1.0, 0.0, num2 * num, true);
                        break;
                    default:
                        return;
                }
            }
            if (this.bool_1)
            {
                ToolSceneNavigate.SetCursor(this.cursor_3.Handle.ToInt32());
            }
            else
            {
                ToolSceneNavigate.SetCursor(this.cursor_0.Handle.ToInt32());
            }
            this._plugin.ActiveViewer.Redraw(true);
        }
    }
}