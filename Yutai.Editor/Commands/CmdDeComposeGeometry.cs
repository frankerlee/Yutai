using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdDeComposeGeometry : YutaiCommand
    {
        public override bool Enabled
        {
            get
            {
                bool flag;
                if (_context.FocusMap != null)
                {
                    if ((Yutai.ArcGIS.Common.Editor.Editor.EditMap == null
                        ? true
                        : Yutai.ArcGIS.Common.Editor.Editor.EditMap == _context.FocusMap))
                    {
                        flag = false;
                        return flag;
                    }


                    if (Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace == null)
                    {
                        flag = false;
                    }
                    else if (_context.FocusMap.LayerCount == 0)
                    {
                        flag = false;
                    }
                    else if (_context.FocusMap.SelectionCount != 0)
                    {
                        IEnumFeature featureSelection = _context.FocusMap.FeatureSelection as IEnumFeature;
                        featureSelection.Reset();
                        IFeature feature = featureSelection.Next();
                        while (feature != null)
                        {
                            if (Yutai.ArcGIS.Common.Editor.Editor.CheckWorkspaceEdit(feature.Class as IDataset,
                                "IsBeingEdited"))
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
                }
                else
                {
                    flag = false;
                }
                return flag;
            }
        }

        public CmdDeComposeGeometry(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "分解";
            this.m_category = "高级编辑工具";
            this.m_name = "Edit_DeComposeGeometry";
            this.m_message = "分解多部分要素为单要素";
            this.m_toolTip = "分解多部分要素";
            this.m_bitmap = Properties.Resources.icon_edit_decomposegeometry;

            _context = hook as IAppContext;
            this._key = "Edit_DeComposeGeometry";
            base._itemType = RibbonItemType.Button;
        }

        public override void OnClick()
        {
            try
            {
                IEnumFeature featureSelection = _context.FocusMap.FeatureSelection as IEnumFeature;
                featureSelection.Reset();
                IFeature feature = featureSelection.Next();
                bool flag = false;
                Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StartEditOperation();
                while (feature != null)
                {
                    flag = Yutai.ArcGIS.Common.Editor.Editor.DeComposeGeometry(feature,
                        feature.ShapeCopy as IGeometryCollection);
                    feature = featureSelection.Next();
                }
                Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StopEditOperation();
                if (flag)
                {
                    _context.FocusMap.FeatureSelection.Clear();
                    _context.ActiveView.Refresh();
                }
                else
                {
                    MessageBox.Show("没有选择多部分要素");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }
    }
}