using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Forms;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    class CmdMoveVertToPoint : YutaiCommand
    {
        public CmdMoveVertToPoint(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_sketch_direction;
            this.m_caption = "移动节点到指定点";
            this.m_category = "Edit";
            this.m_message = "移动节点到指定点";
            this.m_name = "Edit_MoveVertexToPoint";
            this._key = "Edit_MoveVertexToPoint";
            this.m_toolTip = "移动节点到指定点";
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
                return _context.FocusMap != null && _context.FocusMap.LayerCount != 0 && (EditTools.EditFeature != null && EditTools.HitType == HitType.HitNode);
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
            if (frmInputValue.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                double inputValue = frmInputValue.InputValue1;
                double inputValue2 = frmInputValue.InputValue2;
                IGeometry shape = EditTools.EditFeature.Shape;
                if (shape != null && EditTools.PartIndex != -1 && EditTools.PointIndex != -1 && (shape.GeometryType == esriGeometryType.esriGeometryPolyline || shape.GeometryType == esriGeometryType.esriGeometryPolygon))
                {
                    IPointCollection pointCollection = (shape as IGeometryCollection).get_Geometry(EditTools.PartIndex) as IPointCollection;
                    IPoint point = pointCollection.get_Point(EditTools.PointIndex);
                    point.PutCoords(inputValue, inputValue2);
                    pointCollection.UpdatePoint(EditTools.PointIndex, point);
                    (shape as IGeometryCollection).GeometriesChanged();
                    Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StartEditOperation();
                    EditTools.EditFeature.Shape = shape;
                    EditTools.EditFeature.Store();
                    Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StopEditOperation();
                    (_context.FocusMap as IActiveView).Refresh();
                }
            }
        }

        
    }
}