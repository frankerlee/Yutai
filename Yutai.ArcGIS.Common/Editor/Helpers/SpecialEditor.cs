using System;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Editor.Helpers
{
    internal class SpecialEditor
    {
        public SpecialEditor()
        {
        }

        public static void copyFeatureAttribute(IFeatureLayer ifeatureLayer_0, IFeatureLayer ifeatureLayer_1)
        {
            IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
            IFeatureCursor featureCursor = ifeatureLayer_0.FeatureClass.Search(null, false);
            for (IFeature i = featureCursor.NextFeature(); i != null; i = featureCursor.NextFeature())
            {
                (i.Shape as IPolygon as IArea).QueryLabelPoint(pointClass);
                IFeature value = ifeatureLayer_1.FeatureClass.CreateFeature();
                value.Shape = pointClass;
                for (int j = 1; j < i.Fields.FieldCount - 7; j++)
                {
                    if (value.Fields.Field[j].Editable && i.Value[j] != null)
                    {
                        value.Value[j] = i.Value[j];
                    }
                }
                try
                {
                    value.Store();
                }
                catch (Exception exception)
                {
                }
            }
            ComReleaser.ReleaseCOMObject(featureCursor);
        }

        public static void LinesSelfBreak(IFeatureClass ifeatureClass_0, string string_0)
        {
            if (ifeatureClass_0.ShapeType == esriGeometryType.esriGeometryPolyline)
            {
                IQueryFilter queryFilterClass = new QueryFilter()
                {
                    WhereClause = string_0
                };
                IFeatureCursor featureCursor = ifeatureClass_0.Search(queryFilterClass, false);
                IFeatureConstruction featureConstructionClass = new FeatureConstruction();
                IWorkspaceEdit workspace = (ifeatureClass_0 as IDataset).Workspace as IWorkspaceEdit;
                if (!workspace.IsBeingEdited())
                {
                    workspace.StartEditing(false);
                    workspace.StartEditOperation();
                }
                try
                {
                    featureConstructionClass.PlanarizeLinesFromCursor(null, ifeatureClass_0, featureCursor, -1);
                    workspace.StopEditOperation();
                    workspace.StopEditing(true);
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    MessageBox.Show(string.Concat("线打断出错. ", exception.Message));
                    workspace.AbortEditOperation();
                }
                ComReleaser.ReleaseCOMObject(featureCursor);
            }
            else
            {
                MessageBox.Show("目标层不是一个线层.");
            }
        }

        public static void LinesToPolygons(IFeatureClass ifeatureClass_0, IFeatureClass ifeatureClass_1)
        {
            if (ifeatureClass_0.ShapeType == esriGeometryType.esriGeometryPolygon)
            {
                IFeatureCursor featureCursor = ifeatureClass_1.Search(null, false);
                IEnvelope envelope = (ifeatureClass_1 as IGeoDataset).Extent.Envelope;
                IInvalidArea invalidAreaClass = new InvalidArea();
                IFeatureConstruction featureConstructionClass = new FeatureConstruction();
                IWorkspaceEdit workspace = (ifeatureClass_0 as IDataset).Workspace as IWorkspaceEdit;
                if (!workspace.IsBeingEdited())
                {
                    workspace.StartEditing(false);
                    workspace.StartEditOperation();
                }
                try
                {
                    featureConstructionClass.ConstructPolygonsFromFeaturesFromCursor(null, ifeatureClass_0, envelope,
                        true, false, featureCursor, invalidAreaClass, -1, null);
                    workspace.StopEditOperation();
                    workspace.StopEditing(true);
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    MessageBox.Show(string.Concat("Construct polygons failed. ", exception.Message));
                    workspace.AbortEditOperation();
                }
                ComReleaser.ReleaseCOMObject(featureCursor);
            }
            else
            {
                MessageBox.Show("目标层不是一个面层.");
            }
        }

        public static void restoreFeatureAttribute(IFeatureLayer ifeatureLayer_0, IFeatureLayer ifeatureLayer_1)
        {
            IFeatureSelection ifeatureLayer0 = ifeatureLayer_0 as IFeatureSelection;
            IFeatureCursor featureCursor = ifeatureLayer_0.FeatureClass.Search(null, false);
            IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
            IFeature value = featureCursor.NextFeature();
            while (value != null)
            {
                (value.Shape as IPolygon as IArea).QueryLabelPoint(pointClass);
                IQueryFilter queryFilterClass = new QueryFilter();
                double xMin = pointClass.Envelope.XMin - 0.001;
                string str = string.Concat("((XMIN > ", xMin.ToString(), ") AND ");
                xMin = pointClass.Envelope.XMax + 0.001;
                str = string.Concat(str, " (XMAX < ", xMin.ToString(), ") AND ");
                xMin = pointClass.Envelope.YMin - 0.001;
                str = string.Concat(str, " (YMIN > ", xMin.ToString(), ") AND ");
                xMin = pointClass.Envelope.YMax + 0.001;
                str = string.Concat(str, " (YMAX < ", xMin.ToString(), "))");
                queryFilterClass.WhereClause = str;
                IFeatureCursor featureCursor1 = ifeatureLayer_1.FeatureClass.Search(queryFilterClass, false);
                IFeature feature = featureCursor1.NextFeature();
                if (feature != null)
                {
                    IFeature feature1 = featureCursor1.NextFeature();
                    if ((feature == null ? true : feature1 != null))
                    {
                        ifeatureLayer0.Add(value);
                    }
                    else
                    {
                        for (int i = 1; i < value.Fields.FieldCount - 7; i++)
                        {
                            if (value.Fields.Field[i].Editable && feature.Value[i] != null)
                            {
                                value.Value[i] = feature.Value[i];
                                value.Store();
                            }
                        }
                    }
                    ComReleaser.ReleaseCOMObject(featureCursor1);
                    value = featureCursor.NextFeature();
                }
                else
                {
                    ifeatureLayer0.Add(value);
                    ComReleaser.ReleaseCOMObject(featureCursor1);
                    value = featureCursor.NextFeature();
                }
            }
            ComReleaser.ReleaseCOMObject(featureCursor);
        }
    }
}