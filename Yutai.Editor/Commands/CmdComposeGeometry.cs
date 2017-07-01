using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdComposeGeometry : YutaiCommand
    {
        public override bool Enabled
        {
            get
            {
                bool flag;
                if (_context.FocusMap == null)
                {
                    flag = false;
                }
                else if (_context.FocusMap.LayerCount == 0)
                {
                    flag = false;
                }
                else if (_context.FocusMap.SelectionCount >= 2)
                {
                    IEnumFeature featureSelection = _context.FocusMap.FeatureSelection as IEnumFeature;
                    featureSelection.Reset();
                    IFeature feature = featureSelection.Next();
                    while (feature != null)
                    {
                        if ((feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint
                            ? true
                            : feature.Shape.GeometryType == esriGeometryType.esriGeometryMultipoint))
                        {
                            flag = false;
                            return flag;
                        }
                        else
                        {
                            feature = featureSelection.Next();
                        }
                    }
                    featureSelection.Reset();
                    feature = featureSelection.Next();
                    while (feature != null)
                    {
                        if (Yutai.ArcGIS.Common.Editor.Editor.CheckLayerCanEdit(feature.Class as IFeatureClass))
                        {
                            flag = true;
                            return flag;
                        }
                        else
                        {
                            feature = featureSelection.Next();
                        }
                    }
                    flag = false;
                }
                else
                {
                    flag = false;
                }
                return flag;
            }
        }

        public CmdComposeGeometry(IAppContext context)
        {
            OnCreate(context);
        }


        public override void OnCreate(object hook)
        {
            this.m_caption = "合并要素";
            this.m_category = "高级编辑工具";
            this.m_name = "Edit_ComposeGeometry";
            this.m_message = "合并要素";
            this.m_toolTip = "合并要素";
            this.m_bitmap = Properties.Resources.icon_edit_union;

            _context = hook as IAppContext;
            this._key = "Edit_ComposeGeometry";
            base._itemType = RibbonItemType.Button;
        }


        public override void OnClick()
        {
            IEnumFeature featureSelection = _context.FocusMap.FeatureSelection as IEnumFeature;
            Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StartEditOperation();
            bool flag = Yutai.ArcGIS.Common.Editor.Editor.ComposeGeometry(featureSelection);
            Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StopEditOperation();
            if (flag)
            {
                (_context.FocusMap as IActiveView).Refresh();
            }
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }
    }
}