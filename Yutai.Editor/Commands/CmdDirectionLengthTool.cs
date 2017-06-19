using System;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Forms;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdDirectionLengthTool : YutaiCommand
    {
        public CmdDirectionLengthTool(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_sketch_lengthdirection;
            this.m_caption = "方向/长度";
            this.m_category = "Edit";
            this.m_message = "生成指定长度和方向的线段";
            this.m_name = "Edit_DirectionLengthTool";
            this._key = "Edit_DirectionLengthTool";
            this.m_toolTip = "生成指定长度和方向的线段";
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
            frmInputValue1 frmInputValue = new frmInputValue1();
            frmInputValue.Text = "输入方向/长度";
            ILine line = new Line();
            line.PutCoords(SketchShareEx.LastPoint, SketchShareEx.m_pAnchorPoint);
            frmInputValue.InputValue1 = 180.0 * line.Angle / 3.1415926;
            frmInputValue.InputValue2 = line.Length;
            if (frmInputValue.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.ParseInput(frmInputValue.InputValue1, frmInputValue.InputValue2);
            }
        }

        private void ParseInput(double angle, double length)
        {
            angle = angle * 3.1415926535897931 / 180.0;
            double num = length * Math.Cos(angle);
            double num2 = length * Math.Sin(angle);
            num = SketchShareEx.LastPoint.X + num;
            num2 = SketchShareEx.LastPoint.Y + num2;
            SketchShareEx.m_pAnchorPoint.PutCoords(num, num2);
            SketchShareEx.SketchMouseDown(SketchShareEx.m_pAnchorPoint,_context.ActiveView, Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer);
            SketchShareEx.IsFixDirection = false;
            SketchShareEx.IsFixLength = false;
        }
    }
}