using System;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Forms;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdDirectionTool: YutaiCommand
    {
        public CmdDirectionTool(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_sketch_direction;
            this.m_caption = "方向";
            this.m_category = "Edit";
            this.m_message = "限制生成线段的方向";
            this.m_name = "Edit_DirectionTool";
            this._key = "Edit_DirectionTool";
            this.m_toolTip = "限制生成线段的方向";
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
                return SkectchAssist.CheckEnable(_context);
            }
        }




        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            frmInputValue frmInputValue = new frmInputValue();
            frmInputValue.Text = "输入方向";
            if (SketchShareEx.IsFixDirection)
            {
                frmInputValue.InputValue = SketchShareEx.FixDirection;
            }
            else
            {
                ILine line = new Line();
                line.PutCoords(SketchShareEx.LastPoint, SketchShareEx.m_pAnchorPoint);
                frmInputValue.InputValue = 180.0 * line.Angle / 3.1415926535897931;
            }
            if (frmInputValue.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.FixDirection(frmInputValue.InputValue);
            }
        }

        private void FixDirection(double angle)
        {
            SketchShareEx.FixDirection = angle;
            SketchShareEx.IsFixDirection = true;
            if (SketchShareEx.IsFixLength)
            {
                double num = SketchShareEx.FixLength * Math.Cos(SketchShareEx.FixDirection * 3.1415926535897931 / 180.0);
                double num2 = SketchShareEx.FixLength * Math.Sin(SketchShareEx.FixDirection * 3.1415926535897931 / 180.0);
                num = SketchShareEx.LastPoint.X + num;
                num2 = SketchShareEx.LastPoint.Y + num2;
                SketchShareEx.m_pAnchorPoint.PutCoords(num, num2);
                SketchShareEx.SketchMouseDown(SketchShareEx.m_pAnchorPoint, _context.ActiveView, Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer);
                SketchShareEx.IsFixDirection = false;
                SketchShareEx.IsFixLength = false;
            }
        }
    }
}