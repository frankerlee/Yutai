using System;
using System.Reflection;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Forms;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    class CmdAddPointInGeometry : YutaiCommand
    {
        public CmdAddPointInGeometry(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_sketch_direction;
            this.m_caption = "在几何对象中插入点";
            this.m_category = "Edit";
            this.m_message = "在几何对象中插入点";
            this.m_name = "Edit_AddPointInGeometry";
            this._key = "Edit_AddPointInGeometry";
            this.m_toolTip = "在几何对象中插入点";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageBeforeText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get
            {
                return _context.FocusMap != null && _context.FocusMap.LayerCount != 0 && Yutai.ArcGIS.Common.Editor.Editor.EditMap == _context.FocusMap && (EditTools.EditFeature != null && EditTools.HitType == HitType.HitSegment);
            }
        }




        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {

            frmInputValue1 frmInputValue = new frmInputValue1();
            frmInputValue.Text = "输入X, Y值";
            frmInputValue.InputValue1 = EditTools.CurrentPosition.X;
            frmInputValue.InputValue2 = EditTools.CurrentPosition.Y;
            if (frmInputValue.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                double inputValue = frmInputValue.InputValue1;
                double inputValue2 = frmInputValue.InputValue2;
                IGeometry shape = EditTools.EditFeature.Shape;
                if (shape != null && EditTools.PartIndex != -1 && EditTools.PointIndex != -1 && (shape.GeometryType == esriGeometryType.esriGeometryPolyline || shape.GeometryType == esriGeometryType.esriGeometryPolygon))
                {
                    double num = 0.0;
                    int index = -1;
                    int num2 = -1;
                    double double_ = CommonHelper.ConvertPixelsToMapUnits((IActiveView)_context.FocusMap, 4.0);
                    IPoint point;
                    bool flag;
                    if (GeometryOperator.TestGeometryHit(double_, EditTools.CurrentPosition, shape, out point, ref num, ref index, ref num2, out flag) && !flag)
                    {
                        IPath path = (IPath)((IGeometryCollection)shape).get_Geometry(index);
                        IPoint point2 = new Point();
                        point2.PutCoords(inputValue, inputValue2);
                        object value = Missing.Value;
                        object obj = num2;
                        ((IPointCollection)path).AddPoint(point2, ref value, ref obj);
                        (shape as IGeometryCollection).GeometriesChanged();
                        Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StartEditOperation();
                        EditTools.EditFeature.Shape = shape;
                        EditTools.EditFeature.Store();
                        Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StopEditOperation();
                        (_context.FocusMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                    }
                }
            }
        }
    }
}