using System;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdStretchLine : YutaiTool
    {
        private double double_0 = 16;

        private IFeature ifeature_0 = null;

        private IStretchLineFeedback istretchLineFeedback_0 = null;

        public override bool Enabled
        {
            get
            {
                bool result;
                try
                {
                    if (Yutai.ArcGIS.Common.Editor.Editor.EditMap == null)
                    {
                        result = false;
                    }
                    else if (Yutai.ArcGIS.Common.Editor.Editor.EditMap != null &&
                             Yutai.ArcGIS.Common.Editor.Editor.EditMap != _context.FocusMap)
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
                            if (feature.Shape.GeometryType != esriGeometryType.esriGeometryPolyline)
                            {
                                result = false;
                                return result;
                            }
                            if (Yutai.ArcGIS.Common.Editor.Editor.CheckWorkspaceEdit(feature.Class as IDataset,
                                "IsBeingEdited"))
                            {
                                result = true;
                                return result;
                            }
                        }
                        result = false;
                    }
                }
                catch
                {
                    result = false;
                }
                return result;
            }
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "拉伸线";
            this.m_category = "编辑器";
            this.m_name = "Edit_StretchLine";
            this.m_message = "拉伸线";
            this.m_toolTip = "拉伸线";

            _context = hook as IAppContext;
            this._key = "Edit_StretchLine";

            this.m_bitmap = Properties.Resources.icon_edit_strecthline;
            base._itemType = RibbonItemType.Tool;
        }

        public CmdStretchLine(IAppContext context)
        {
            OnCreate(context);
        }

        private IFeature FindFeature(IPoint ipoint_0)
        {
            IFeature feature = null;
            double mapUnits = Common.ConvertPixelsToMapUnits((IActiveView) _context.FocusMap, this.double_0);
            IEnvelope envelope = ipoint_0.Envelope;
            envelope.Height = mapUnits;
            envelope.Width = mapUnits;
            envelope.CenterAt(ipoint_0);
            ISpatialFilter spatialFilterClass = new SpatialFilter()
            {
                Geometry = envelope,
                SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
            };
            for (int i = 0; i < _context.FocusMap.LayerCount; i++)
            {
                IFeatureLayer layer = _context.FocusMap.Layer[i] as IFeatureLayer;
                if (layer != null && layer.Visible && layer.FeatureClass != null &&
                    layer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    spatialFilterClass.GeometryField = layer.FeatureClass.ShapeFieldName;
                    IFeatureCursor featureCursor = layer.Search(spatialFilterClass, false);
                    this.GetProximityFeature(ipoint_0, featureCursor, ref feature);
                    ComReleaser.ReleaseCOMObject(featureCursor);
                }
            }
            return feature;
        }

        private void GetProximityFeature(IPoint ipoint_0, IFeatureCursor ifeatureCursor_0, ref IFeature ifeature_1)
        {
            ifeature_1 = null;
            IProximityOperator ipoint0 = (IProximityOperator) ipoint_0;
            IFeature feature = ifeatureCursor_0.NextFeature();
            if (feature != null)
            {
                if (ifeature_1 == null)
                {
                    ifeature_1 = feature;
                    feature = ifeatureCursor_0.NextFeature();
                }
                double num = ipoint0.ReturnDistance(ifeature_1.ShapeCopy);
                while (feature != null)
                {
                    double num1 = ipoint0.ReturnDistance(feature.Shape);
                    if (num1 < num)
                    {
                        num = num1;
                        ifeature_1 = feature;
                    }
                    feature = ifeatureCursor_0.NextFeature();
                }
            }
        }

        public override void OnMouseDown(int int_0, int int_1, int int_2, int int_3)
        {
            if (int_0 == 1)
            {
                IPoint mapPoint = _context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
                this.ifeature_0 = this.FindFeature(mapPoint);
                if (this.ifeature_0 != null)
                {
                    IPolyline shapeCopy = this.ifeature_0.ShapeCopy as IPolyline;
                    ILine lineClass = new Line();
                    lineClass.PutCoords(mapPoint, shapeCopy.FromPoint);
                    double length = lineClass.Length;
                    lineClass.PutCoords(mapPoint, shapeCopy.ToPoint);
                    double num = lineClass.Length;
                    this.istretchLineFeedback_0 = new StretchLineFeedback()
                    {
                        Display = _context.ActiveView.ScreenDisplay
                    };
                    this.istretchLineFeedback_0.Start(shapeCopy, mapPoint);
                    if (length <= num)
                    {
                        this.istretchLineFeedback_0.Anchor = shapeCopy.FromPoint;
                    }
                    else
                    {
                        this.istretchLineFeedback_0.Anchor = shapeCopy.ToPoint;
                    }
                }
            }
        }

        public override void OnMouseMove(int int_0, int int_1, int int_2, int int_3)
        {
            if (this.istretchLineFeedback_0 != null)
            {
                IPoint mapPoint = _context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
                this.istretchLineFeedback_0.MoveTo(mapPoint);
            }
        }

        public override void OnMouseUp(int int_0, int int_1, int int_2, int int_3)
        {
            if (this.istretchLineFeedback_0 != null)
            {
                IPolyline polyline = this.istretchLineFeedback_0.Stop();
                this.istretchLineFeedback_0 = null;
                IWorkspaceEdit workspace = (this.ifeature_0.Class as IDataset).Workspace as IWorkspaceEdit;
                workspace.StartEditOperation();
                try
                {
                    if ((this.ifeature_0.Shape as IZAware).ZAware)
                    {
                        (polyline as IZAware).ZAware = true;
                        (polyline as IZ).SetConstantZ((this.ifeature_0.Shape as IZ).ZMax);
                    }
                    this.ifeature_0.Shape = polyline;
                    this.ifeature_0.Store();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
                workspace.StopEditOperation();
                _context.ActiveView.Refresh();
            }
        }
    }
}