using System;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Forms;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdAbsoluteXYTool : YutaiCommand
    {
        public CmdAbsoluteXYTool(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_sketch_absolutexy;
            this.m_caption = "X,Y坐标";
            this.m_category = "Edit";
            this.m_message = "X,Y坐标";
            this.m_name = "Edit_AbsoluteXYTool";
            this._key = "Edit_AbsoluteXYTool";
            this.m_toolTip = "X,Y坐标";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageBeforeText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get { return SkectchAssist.CheckEnable(_context); }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            frmInputValue1 frmInputValue = new frmInputValue1();
            frmInputValue.Text = "输入x, y值";
            frmInputValue.InputValue1 = SketchShareEx.m_pAnchorPoint.X;
            frmInputValue.InputValue2 = SketchShareEx.m_pAnchorPoint.Y;
            if (frmInputValue.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.ParseInput(frmInputValue.InputValue1, frmInputValue.InputValue2);
            }
        }

        private void ParseInput(double x, double y)
        {
            SketchShareEx.m_pAnchorPoint.PutCoords(x, y);
            SketchShareEx.SketchMouseDown(SketchShareEx.m_pAnchorPoint, _context.ActiveView,
                Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer);
            SketchShareEx.IsFixDirection = false;
            SketchShareEx.IsFixLength = false;
        }
    }
}