using System;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Forms;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdGeometryGeneralize : YutaiCommand
    {
        public override bool Enabled
        {
            get
            {
                bool result;
                if (_context.FocusMap == null)
                {
                    result = false;
                }
                else if (Yutai.ArcGIS.Common.Editor.Editor.EditMap != null &&
                         Yutai.ArcGIS.Common.Editor.Editor.EditMap != _context.FocusMap)
                {
                    result = false;
                }
                else if (Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace == null)
                {
                    result = false;
                }
                else if (_context.FocusMap.LayerCount == 0)
                {
                    result = false;
                }
                else if (_context.FocusMap.SelectionCount == 0)
                {
                    result = false;
                }
                else
                {
                    IEnumFeature enumFeature = _context.FocusMap.FeatureSelection as IEnumFeature;
                    enumFeature.Reset();
                    for (IFeature feature = enumFeature.Next(); feature != null; feature = enumFeature.Next())
                    {
                        if (feature.Shape == null)
                        {
                            result = false;
                            return result;
                        }
                        if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint ||
                            feature.Shape.GeometryType == esriGeometryType.esriGeometryMultipoint)
                        {
                            result = false;
                            return result;
                        }
                        if (Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace == (feature.Class as IDataset).Workspace)
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

        public CmdGeometryGeneralize(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "综合";
            this.m_category = "高级编辑工具";
            this.m_name = "Edit_GeometryGeneralize";
            this.m_message = "综合选择的线和多边形对象";
            this.m_toolTip = "综合";
            this.m_bitmap = Properties.Resources.icon_edit_geometrygeneralize;

            _context = hook as IAppContext;
            this._key = "Edit_GeometryGeneralize";
            base._itemType = RibbonItemType.Button;
        }

        public override void OnClick()
        {
            try
            {
                frmMaxAllowableOffset _frmMaxAllowableOffset = new frmMaxAllowableOffset()
                {
                    Text = "综合"
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
                            shape.Generalize(maxAllowableOffset);
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