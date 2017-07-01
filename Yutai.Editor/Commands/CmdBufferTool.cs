using System;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Forms;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdBufferTool : YutaiCommand
    {
        public CmdBufferTool(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_edit_buffer;
            this.m_caption = "缓冲";
            this.m_category = "Edit";
            this.m_message = "缓冲";
            this.m_name = "Edit_Buffer";
            this._key = "Edit_Buffer";
            this.m_toolTip = "由选中要素创建缓冲区";
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
                return _context.FocusMap != null &&
                       (Yutai.ArcGIS.Common.Editor.Editor.EditMap == null ||
                        Yutai.ArcGIS.Common.Editor.Editor.EditMap == _context.FocusMap) &&
                       Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace != null && _context.FocusMap.SelectionCount != 0 &&
                       Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate != null &&
                       !(Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer.FeatureClass is
                           ICoverageFeatureClass);
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            frmInputValue frmInputValue = new frmInputValue();
            frmInputValue.Text = "距离";
            frmInputValue.InputValue = 0.0;
            if (frmInputValue.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                double inputValue = frmInputValue.InputValue;
                IEnumFeature enumFeature = _context.FocusMap.FeatureSelection as IEnumFeature;
                enumFeature.Reset();
                IFeature feature = enumFeature.Next();
                new GeometryBag();
                IFeatureClass featureClass =
                    Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer.FeatureClass;
                Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StartEditOperation();
                int index = featureClass.FindField(featureClass.ShapeFieldName);
                IGeometryDef geometryDef = featureClass.Fields.get_Field(index).GeometryDef;
                while (feature != null)
                {
                    try
                    {
                        ITopologicalOperator topologicalOperator = feature.ShapeCopy as ITopologicalOperator;
                        IGeometry geometry = topologicalOperator.Buffer(inputValue);
                        if (featureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                        {
                            IPolyline polyline = new Polyline() as IPolyline;
                            (polyline as ISegmentCollection).AddSegmentCollection(geometry as ISegmentCollection);
                            if (!polyline.IsEmpty)
                            {
                                IFeature feature2 = featureClass.CreateFeature();
                                if (geometryDef.HasZ)
                                {
                                    (polyline as IZAware).ZAware = true;
                                    double num;
                                    double constantZ;
                                    geometryDef.SpatialReference.GetZDomain(out num, out constantZ);
                                    (polyline as IZ).SetConstantZ(constantZ);
                                }
                                feature2.Shape = polyline;
                                feature2.Store();
                            }
                        }
                        else
                        {
                            IFeature feature2 = featureClass.CreateFeature();
                            if (geometryDef.HasZ)
                            {
                                (geometry as IZAware).ZAware = true;
                                double num;
                                double constantZ;
                                geometryDef.SpatialReference.GetZDomain(out num, out constantZ);
                                (geometry as IZ).SetConstantZ(constantZ);
                            }
                            feature2.Shape = geometry;
                            feature2.Store();
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                    }
                    feature = enumFeature.Next();
                }
                Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StopEditOperation();
                _context.ActiveView.Refresh();
            }
        }
    }
}