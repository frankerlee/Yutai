using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Forms;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdLengthTool : YutaiCommand
    {
        public CmdLengthTool(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_sketch_length;
            this.m_caption = "长度";
            this.m_category = "Edit";
            this.m_message = "限制生成线段的长度";
            this.m_name = "Edit_LengthTool";
            this._key = "Edit_LengthTool";
            this.m_toolTip = "限制生成线段的长度";
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
            frmInputValue frmInputValue = new frmInputValue();
            frmInputValue.Text = "输入长度";
            if (SketchShareEx.IsFixLength)
            {
                frmInputValue.InputValue = SketchShareEx.FixLength;
            }
            else
            {
                ILine line = new Line();
                line.PutCoords(SketchShareEx.LastPoint, SketchShareEx.m_pAnchorPoint);
                frmInputValue.InputValue = line.Length;
            }
            if (frmInputValue.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.FixLength(frmInputValue.InputValue);
            }
        }

        private void FixLength(double length)
        {
            SketchShareEx.FixLength = length;
            SketchShareEx.IsFixLength = true;
            if (SketchShareEx.IsFixDirection)
            {
                double num = SketchShareEx.FixLength*Math.Cos(SketchShareEx.FixDirection*3.1415926535897931/180.0);
                double num2 = SketchShareEx.FixLength*Math.Sin(SketchShareEx.FixDirection*3.1415926535897931/180.0);
                num = SketchShareEx.LastPoint.X + num;
                num2 = SketchShareEx.LastPoint.Y + num2;
                SketchShareEx.m_pAnchorPoint.PutCoords(num, num2);
                SketchShareEx.SketchMouseDown(SketchShareEx.m_pAnchorPoint, _context.ActiveView,
                    Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer);
                SketchShareEx.IsFixDirection = false;
                SketchShareEx.IsFixLength = false;
            }
        }
    }
}