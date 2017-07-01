using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Forms;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdGeometrySmooth : YutaiCommand
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
                else if (_context.FocusMap.SelectionCount != 0)
                {
                    IEnumFeature featureSelection = _context.FocusMap.FeatureSelection as IEnumFeature;
                    featureSelection.Reset();
                    IFeature feature = featureSelection.Next();
                    while (feature != null)
                    {
                        if (feature.Shape == null)
                        {
                            flag = false;
                            return flag;
                        }
                        else if ((feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint
                            ? true
                            : feature.Shape.GeometryType == esriGeometryType.esriGeometryMultipoint))
                        {
                            flag = false;
                            return flag;
                        }
                        else if (Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace == (feature.Class as IDataset).Workspace)
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

        public CmdGeometrySmooth(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "平滑";
            this.m_category = "高级编辑工具";
            this.m_name = "Edit_GeometrySmooth";
            this.m_message = "平滑选择的线和多边形对象";
            this.m_toolTip = "平滑选择的线和多边形对象";
            this.m_bitmap = Properties.Resources.icon_edit_geometrysmooth;

            _context = hook as IAppContext;
            this._key = "Edit_GeometrySmooth";
            base._itemType = RibbonItemType.Button;
        }

        public override void OnClick()
        {
            try
            {
                frmMaxAllowableOffset _frmMaxAllowableOffset = new frmMaxAllowableOffset()
                {
                    Text = "平滑"
                };
                if (_frmMaxAllowableOffset.ShowDialog() == DialogResult.OK)
                {
                    double maxAllowableOffset = _frmMaxAllowableOffset.MaxAllowableOffset;
                    IEnumFeature featureSelection = _context.FocusMap.FeatureSelection as IEnumFeature;
                    featureSelection.Reset();
                    IFeature feature = featureSelection.Next();
                    IWorkspaceEdit editWorkspace = Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace;
                    editWorkspace.StartEditOperation();
                    IPolycurve shape = null;
                    while (feature != null)
                    {
                        shape = feature.Shape as IPolycurve;
                        if (shape != null && editWorkspace == (feature.Class as IDataset).Workspace)
                        {
                            shape.Smooth(maxAllowableOffset);
                            feature.Shape = shape;
                            feature.Store();
                        }
                        feature = featureSelection.Next();
                    }
                    editWorkspace.StopEditOperation();
                    _context.ActiveView.Refresh();
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