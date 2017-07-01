using System;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Controls.Editor.UI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdProportionSplit : YutaiCommand
    {
        public override bool Enabled
        {
            get
            {
                bool result;
                try
                {
                    if (_context.FocusMap == null)
                    {
                        result = false;
                    }
                    else if (Yutai.ArcGIS.Common.Editor.Editor.EditMap != null &&
                             Yutai.ArcGIS.Common.Editor.Editor.EditMap != _context.FocusMap)
                    {
                        result = false;
                    }
                    else if (_context.FocusMap.SelectionCount != 1)
                    {
                        result = false;
                    }
                    else
                    {
                        IEnumFeature enumFeature = _context.FocusMap.FeatureSelection as IEnumFeature;
                        enumFeature.Reset();
                        IFeature feature = enumFeature.Next();
                        if (feature != null && feature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline &&
                            Yutai.ArcGIS.Common.Editor.Editor.CheckWorkspaceEdit(feature.Class as IDataset,
                                "IsBeingEdited"))
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                    }
                }
                catch
                {
                    result = false;
                }
                return result;
            }
        }

        public CmdProportionSplit(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "比例细分";
            this.m_category = "编辑器";
            this.m_name = "Edit_ProportionSplit";
            this.m_toolTip = "比例细分";
            this.m_bitmap = Properties.Resources.icon_edit_proportion;
            _context = hook as IAppContext;
            this._key = "Edit_ProportionSplit";
            base._itemType = RibbonItemType.Button;
        }

        public override void OnClick()
        {
            IEnumFeature featureSelection = _context.FocusMap.FeatureSelection as IEnumFeature;
            featureSelection.Reset();
            IFeature feature = featureSelection.Next();
            (new frmProportionSplit()
            {
                Feature = feature
            }).ShowDialog();
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }
    }
}