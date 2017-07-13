using System;
using System.Reflection;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Scene.Forms;
using Yutai.Plugins.Scene.Helpers;

namespace Yutai.Plugins.Scene.Commands.View
{
    public class ToolSceneDrawFlyByPath : YutaiTool
    {
        internal static frmFlyByProps m_frm;

        private IAppContext _context;
        private IScenePlugin _plugin;

        private IPointCollection ipointCollection_0;

        private ISurface isurface_0;

        public override bool Enabled
        {
            get
            {
                bool result;
                if (_plugin == null) return false;
                if (this._plugin.Scene == null || !this._plugin.SceneVisible || (this._plugin.Scene as IBasicMap).LayerCount <= 0) return false;

                if ((this._plugin.Scene as IBasicMap).LayerCount == 0)
                {
                    result = false;
                }
                else if (this._plugin.CurrentLayer == null)
                {
                    result = false;
                }
                else
                {
                    this.isurface_0 = SurfaceInfo.GetSurfaceFromLayer(this._plugin.CurrentLayer);
                    result = (this.isurface_0 != null && FlyByUtils.bStillDrawing);
                }
                return result;
            }
        }

        static ToolSceneDrawFlyByPath()
        {
            // 注意: 此类型已标记为 'beforefieldinit'.
            ToolSceneDrawFlyByPath.old_acctor_mc();
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public ToolSceneDrawFlyByPath(IAppContext context, IScenePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }
        public override void OnCreate(object object_0)
        {
            _context = object_0 as IAppContext;
            _key = "Scene_DrawFlyByPath";
            _itemType = RibbonItemType.Tool;
            this.m_bitmap = Properties.Resources.drawfrompath;
            this.m_cursor = new System.Windows.Forms.Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Scene.Resources.Cursor.DrawFlyByPath.cur"));
            FlyByUtils.bStillDrawing = true;
            this.m_caption = "绘制锚定点集";
            this.m_name = "Scene_DrawFlyByPath";
            this.m_toolTip = "绘制锚定点集";
            this.m_category = "3D";
        }

        public override void OnClick()
        {
            _plugin.CurrentTool = this;
            this.ipointCollection_0 = null;
            ToolSceneDrawFlyByPath.m_frm.Application = this._plugin;
            IPolyline polyline = this.method_0();
            if (polyline != null)
            {
                this.ipointCollection_0 = (polyline as IPointCollection);
                this.method_1();
            }
        }

        private IPolyline method_0()
        {
            IScene scene = this._plugin.Scene;
            IPolyline result;
            if (scene.SelectionCount == 1)
            {
                IEnumFeature enumFeature = scene.FeatureSelection as IEnumFeature;
                IFeature feature = enumFeature.Next();
                if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                {
                    IPolyline polyline = feature.ShapeCopy as IPolyline;
                    result = polyline;
                    return result;
                }
            }
            result = null;
            return result;
        }

      

        public override void OnDblClick()
        {
            this.method_1();
        }

        public override void OnKeyDown(int int_0, int int_1)
        {
            if (int_0 == 111)
            {
                this.method_1();
            }
        }

        private void method_1()
        {
            if (this.ipointCollection_0 != null && this.ipointCollection_0.PointCount > 1)
            {
                FlyByUtils.bStillDrawing = false;
                ToolSceneDrawFlyByPath.m_frm = new frmFlyByProps(_context,_plugin);
                ToolSceneDrawFlyByPath.m_frm.Application = this._plugin;
                ToolSceneDrawFlyByPath.m_frm.Init(this.ipointCollection_0);
                ToolSceneDrawFlyByPath.m_frm.Show();
                this._plugin.CurrentTool = null;
                this.ipointCollection_0 = null;
            }
        }

        public override void OnMouseDown(int int_0, int int_1, int int_2, int int_3)
        {
            IPoint point = Utils3D.XYToPoint(this._plugin.SceneGraph, int_2, int_3);
            if (point != null)
            {
                point.Z = this.isurface_0.GetElevation(point);
                FlyByUtils.AddFlyByGraphic(this._plugin.SceneGraph, point, FlyByUtils.FlyByElementType.FLYBY_ANCHORS, System.Drawing.Color.Red, System.Drawing.Color.Red, System.Drawing.Color.Red, System.Drawing.Color.Red, false);
                Utils3D.FlashLocation3D(this._plugin.SceneGraph, point);
                if (this.ipointCollection_0 == null)
                {
                    this.ipointCollection_0 = new Polyline();
                }
                object value = Missing.Value;
                this.ipointCollection_0.AddPoint(point, ref value, ref value);
                if (this.ipointCollection_0.PointCount > 1)
                {
                    IPolyline polyline = (this.ipointCollection_0 as IClone).Clone() as IPolyline;
                    Utils3D.MakeZMAware(polyline, true);
                    double maxSegmentLength = polyline.Length / 50.0;
                    if (this.ipointCollection_0.PointCount > 2)
                    {
                        polyline.Smooth(0.0);
                        polyline.Densify(maxSegmentLength, 0.0);
                    }
                    FlyByUtils.AddFlyByGraphic(this._plugin.SceneGraph, polyline, FlyByUtils.FlyByElementType.FLYBY_PATH, System.Drawing.Color.Red, System.Drawing.Color.Red, System.Drawing.Color.Red, System.Drawing.Color.Red, true);
                }
            }
        }

        private static void old_acctor_mc()
        {
            ToolSceneDrawFlyByPath.m_frm = new frmFlyByProps();
        }
    }
}