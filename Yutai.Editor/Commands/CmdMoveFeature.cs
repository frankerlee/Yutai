using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Forms;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    class CmdMoveFeature : YutaiCommand
    {
        public CmdMoveFeature(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_sketch_direction;
            this.m_caption = "移动对象...";
            this.m_category = "Edit";
            this.m_message = "移动对象...";
            this.m_name = "Edit_MoveFeatures";
            this._key = "Edit_MoveFeatures";
            this.m_toolTip = "移动对象...";
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
                bool result;
                if (_context.FocusMap == null)
                {
                    result = false;
                }
                else if (_context.FocusMap.LayerCount == 0)
                {
                    result = false;
                }

                else
                {
                    if (Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace != null)
                    {
                        IEnumFeature enumFeature = _context.FocusMap.FeatureSelection as IEnumFeature;
                        enumFeature.Reset();
                        IFeature feature = enumFeature.Next();
                        if (feature != null)
                        {
                            result = true;
                            return result;
                        }
                    }
                    result = false;
                }
                return result;
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            frmInputValue1 frmInputValue = new frmInputValue1();
            frmInputValue.Text = "输入移动增量";
            if (frmInputValue.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                double inputValue = frmInputValue.InputValue1;
                double inputValue2 = frmInputValue.InputValue2;
                Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StartEditOperation();
                IEnumFeature enumFeature = _context.FocusMap.FeatureSelection as IEnumFeature;
                enumFeature.Reset();
                for (IFeature feature = enumFeature.Next(); feature != null; feature = enumFeature.Next())
                {
                    ITransform2D transform2D = feature.Shape as ITransform2D;
                    if (transform2D != null)
                    {
                        transform2D.Move(inputValue, inputValue2);
                        feature.Shape = (transform2D as IGeometry);
                        feature.Store();
                    }
                }
                Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StopEditOperation();
                (_context.FocusMap as IActiveView).Refresh();
            }
        }
    }
}