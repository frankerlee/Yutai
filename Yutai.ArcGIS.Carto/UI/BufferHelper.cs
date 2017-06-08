using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class BufferHelper
    {
        public bool bDissolve = true;
        public bool bUseSelect = true;
        public static BufferHelper m_BufferHelper;
        public int m_BufferType = 0;
        public int m_Count = 3;
        public double m_dblRadius = 1.0;
        public double m_dblStep = 1.0;
        public string m_FeatClassName = "";
        public string m_FieldName = "";
        public int m_OutputType = 0;
        public IFeatureLayer m_pFeatureLayer = null;
        public IMap m_pFocusMap;
        public int m_PolygonType = 0;
        public IFeatureClass m_pOutFC = null;
        public IFeatureWorkspace m_pOutFeatureWorksapce = null;
        public int m_SourceType = 0;
        public esriUnits m_Units = esriUnits.esriKilometers;

        static BufferHelper()
        {
            old_acctor_mc();
        }

        public void Do()
        {
            IFeatureCursorBuffer2 buffer = new FeatureCursorBufferClass {
                BufferSpatialReference = this.m_pFocusMap.SpatialReference,
                DataFrameSpatialReference = this.m_pFocusMap.SpatialReference,
                SourceSpatialReference = this.m_pFocusMap.SpatialReference,
                TargetSpatialReference = this.m_pFocusMap.SpatialReference
            };
            if (this.m_SourceType == 0)
            {
                int num;
                bool flag;
                buffer.GraphicsLayer2(this.m_pFocusMap.ActiveGraphicsLayer as IGraphicsLayer, this.bUseSelect, out num, out flag);
            }
            else
            {
                IFeatureCursor cursor = null;
                if (this.bUseSelect)
                {
                    if ((this.m_pFeatureLayer as IFeatureSelection).SelectionSet.Count == 0)
                    {
                        cursor = this.m_pFeatureLayer.Search(null, false);
                    }
                    else
                    {
                        ICursor cursor2;
                        (this.m_pFeatureLayer as IFeatureSelection).SelectionSet.Search(null, false, out cursor2);
                        cursor = cursor2 as IFeatureCursor;
                    }
                }
                else
                {
                    cursor = this.m_pFeatureLayer.Search(null, false);
                }
                buffer.FeatureCursor = cursor;
            }
            buffer.set_Units(this.m_pFocusMap.MapUnits, this.m_Units);
            if (this.m_BufferType == 0)
            {
                buffer.ValueDistance = this.m_dblRadius;
            }
            else if (this.m_BufferType == 1)
            {
                buffer.FieldDistance = this.m_FieldName;
            }
            else
            {
                buffer.set_RingDistance(this.m_Count, this.m_dblStep);
            }
            if (this.m_PolygonType == 0)
            {
                buffer.PolygonBufferType = esriBufferType.esriBufferAll;
            }
            else if (this.m_PolygonType == 1)
            {
                buffer.PolygonBufferType = esriBufferType.esriBufferOutside;
            }
            else if (this.m_PolygonType == 2)
            {
                buffer.PolygonBufferType = esriBufferType.esriBufferInside;
            }
            else
            {
                buffer.PolygonBufferType = esriBufferType.esriBufferOutsideIncludeInside;
            }
            buffer.Dissolve = this.bDissolve;
            try
            {
                if (this.m_OutputType == 0)
                {
                    (buffer as IBufferProcessingParameter).SaveAsGraphics = true;
                    ICompositeGraphicsLayer basicGraphicsLayer = this.m_pFocusMap.BasicGraphicsLayer as ICompositeGraphicsLayer;
                    buffer.BufferToGraphics(basicGraphicsLayer);
                }
                else if (this.m_OutputType == 1)
                {
                    (buffer as IBufferProcessingParameter).InputHasPolygons = false;
                    (buffer as IBufferProcessingParameter).SimplifyShapes = true;
                    buffer.Buffer((this.m_pOutFC as IDataset).FullName as IFeatureClassName);
                }
                else
                {
                    IObjectClassDescription description = new FeatureClassDescriptionClass();
                    IFields requiredFields = description.RequiredFields;
                    int index = requiredFields.FindField((description as IFeatureClassDescription).ShapeFieldName);
                    IFieldEdit edit = requiredFields.get_Field(index) as IFieldEdit;
                    IGeometryDefEdit geometryDef = edit.GeometryDef as IGeometryDefEdit;
                    geometryDef.SpatialReference = this.m_pFocusMap.SpatialReference;
                    geometryDef.GeometryType = esriGeometryType.esriGeometryPolygon;
                    IFeatureClass o = this.m_pOutFeatureWorksapce.CreateFeatureClass(this.m_FeatClassName, requiredFields, null, null, esriFeatureType.esriFTSimple, "Shape", "");
                    IFeatureClassName fullName = (o as IDataset).FullName as IFeatureClassName;
                    Marshal.ReleaseComObject(o);
                    o = null;
                    GC.Collect();
                    buffer.Buffer(fullName);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private static void old_acctor_mc()
        {
            m_BufferHelper = null;
        }
    }
}

