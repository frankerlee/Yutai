using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Shared;

namespace Yutai.ArcGIS.Common.Editor
{
    public class CreateFeatureTool
    {
        public CreateFeatureTool()
        {
        }

        public static void CreateDimensionFeature(IDimensionShape pDimensionShape, esriDimensionType pDimensionType,
            IActiveView pActiveView, IFeatureLayer pFeatureLayer)
        {
            if (pDimensionShape != null)
            {
                IWorkspaceEdit workspace = (IWorkspaceEdit) ((IDataset) pFeatureLayer.FeatureClass).Workspace;
                workspace.StartEditOperation();
                IFeature esriDimensionType0 = pFeatureLayer.FeatureClass.CreateFeature();
                IRowSubtypes rowSubtype = (IRowSubtypes) esriDimensionType0;
                try
                {
                    rowSubtype.InitDefaultValues();
                }
                catch (Exception exception)
                {
                    //Logger.Current.Error("",exception, "");
                    Logger.Current.Warn(exception.Message);
                }
                (esriDimensionType0 as IDimensionFeature).DimensionType = pDimensionType;
                (esriDimensionType0 as IDimensionFeature).DimensionShape = pDimensionShape;
                EditorEvent.NewRow(esriDimensionType0);
                esriDimensionType0.Store();
                workspace.StopEditOperation();
                EditorEvent.AfterNewRow(esriDimensionType0);
                pActiveView.FocusMap.ClearSelection();
                pActiveView.FocusMap.SelectFeature(pFeatureLayer, esriDimensionType0);
                pActiveView.Refresh();
            }
        }

        public static void CreateFeature(IGeometry pGeometry, IActiveView pActiveView, IFeatureLayer pFeatureLayer,
            bool IsClearSelection)
        {
            double num;
            double num1;
            Exception exception;
            if (pGeometry != null && !pGeometry.IsEmpty)
            {
                IEnvelope envelope = pGeometry.Envelope;
                envelope.Expand(10, 10, false);
                try
                {
                    pGeometry.SpatialReference = pActiveView.FocusMap.SpatialReference;
                    int num2 = pFeatureLayer.FeatureClass.FindField(pFeatureLayer.FeatureClass.ShapeFieldName);
                    IGeometryDef geometryDef = pFeatureLayer.FeatureClass.Fields.Field[num2].GeometryDef;
                    if (geometryDef.HasZ)
                    {
                        ((IZAware) pGeometry).ZAware = true;
                        if (pGeometry is IZ)
                        {
                            IZ igeometry0 = (IZ) pGeometry;
                            geometryDef.SpatialReference.GetZDomain(out num, out num1);
                            igeometry0.SetConstantZ(num);
                        }
                        else if (pGeometry is IPoint)
                        {
                            geometryDef.SpatialReference.GetZDomain(out num, out num1);
                            (pGeometry as IPoint).Z = num;
                        }
                    }
                    if (geometryDef.HasM)
                    {
                        ((IMAware) pGeometry).MAware = true;
                    }
                    IWorkspaceEdit workspace = (IWorkspaceEdit) ((IDataset) pFeatureLayer.FeatureClass).Workspace;
                    workspace.StartEditOperation();
                    IFeature feature = pFeatureLayer.FeatureClass.CreateFeature();
                    feature.Shape = pGeometry;
                    try
                    {
                        ((IRowSubtypes) feature).InitDefaultValues();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        Logger.Current.Error(exception.Message);
                    }
                    if (Editor.CurrentEditTemplate != null)
                    {
                        Editor.CurrentEditTemplate.SetFeatureValue(feature);
                    }
                    EditorEvent.NewRow(feature);
                    feature.Store();
                    workspace.StopEditOperation();
                    EditorEvent.AfterNewRow(feature);
                    if (IsClearSelection)
                    {
                        if (pActiveView.FocusMap.SelectionCount > 0)
                        {
                            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                            pActiveView.FocusMap.ClearSelection();
                            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                        }
                    }
                    pActiveView.FocusMap.SelectFeature(pFeatureLayer, feature);
                    if (pGeometry.GeometryType != esriGeometryType.esriGeometryPoint)
                    {
                        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pFeatureLayer, envelope);
                    }
                    else
                    {
                        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pFeatureLayer, envelope);
                    }
                    if (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint)
                    {
                        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pFeatureLayer, envelope);
                    }
                }
                catch (COMException cOMException1)
                {
                    COMException cOMException = cOMException1;
                    if (cOMException.ErrorCode != -2147220936)
                    {
                        MessageBox.Show(cOMException.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        MessageBox.Show("几何坐标超出边界!", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    if (pGeometry.GeometryType != esriGeometryType.esriGeometryPoint)
                    {
                        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pFeatureLayer, envelope);
                    }
                    else
                    {
                        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pFeatureLayer, null);
                    }
                    Logger.Current.Error("", cOMException, null);
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                    Logger.Current.Error("", exception, null);
                    if (pGeometry.GeometryType != esriGeometryType.esriGeometryPoint)
                    {
                        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pFeatureLayer, envelope);
                    }
                    else
                    {
                        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pFeatureLayer, null);
                    }
                    MessageBox.Show(exception.Message);
                }
            }
        }

        public static void CreateFeature(IGeometry pGeometry, IActiveView pActiveView, IFeatureLayer pFeatureLayer)
        {
            double num;
            double num1;
            Exception exception;
            if (pGeometry != null && !pGeometry.IsEmpty)
            {
                IEnvelope envelope = pGeometry.Envelope;
                envelope.Expand(10, 10, false);
                try
                {
                    pGeometry.SpatialReference = pActiveView.FocusMap.SpatialReference;
                    int num2 = pFeatureLayer.FeatureClass.FindField(pFeatureLayer.FeatureClass.ShapeFieldName);
                    IGeometryDef geometryDef = pFeatureLayer.FeatureClass.Fields.Field[num2].GeometryDef;
                    if (geometryDef.HasZ)
                    {
                        ((IZAware) pGeometry).ZAware = true;
                        if (pGeometry is IZ)
                        {
                            IZ igeometry0 = (IZ) pGeometry;
                            geometryDef.SpatialReference.GetZDomain(out num, out num1);
                            igeometry0.SetConstantZ(num);
                        }
                        else if (pGeometry is IPoint)
                        {
                            geometryDef.SpatialReference.GetZDomain(out num, out num1);
                            (pGeometry as IPoint).Z = num;
                        }
                    }
                    if (geometryDef.HasM)
                    {
                        ((IMAware) pGeometry).MAware = true;
                    }
                    IWorkspaceEdit workspace = (IWorkspaceEdit) ((IDataset) pFeatureLayer.FeatureClass).Workspace;
                    workspace.StartEditOperation();
                    IFeature feature = pFeatureLayer.FeatureClass.CreateFeature();
                    feature.Shape = pGeometry;
                    try
                    {
                        ((IRowSubtypes) feature).InitDefaultValues();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        Logger.Current.Error(exception.Message);
                    }
                    if (Editor.CurrentEditTemplate != null)
                    {
                        Editor.CurrentEditTemplate.SetFeatureValue(feature);
                    }
                    EditorEvent.NewRow(feature);
                    feature.Store();
                    workspace.StopEditOperation();
                    EditorEvent.AfterNewRow(feature);
                    if (pActiveView.FocusMap.SelectionCount > 0)
                    {
                        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                        pActiveView.FocusMap.ClearSelection();
                        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                    }
                    pActiveView.FocusMap.SelectFeature(pFeatureLayer, feature);
                    if (pGeometry.GeometryType != esriGeometryType.esriGeometryPoint)
                    {
                        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pFeatureLayer, envelope);
                    }
                    else
                    {
                        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pFeatureLayer, envelope);
                    }
                    if (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint)
                    {
                        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pFeatureLayer, envelope);
                    }
                }
                catch (COMException cOMException1)
                {
                    COMException cOMException = cOMException1;
                    if (cOMException.ErrorCode != -2147220936)
                    {
                        MessageBox.Show(cOMException.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        MessageBox.Show("几何坐标超出边界!", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    if (pGeometry.GeometryType != esriGeometryType.esriGeometryPoint)
                    {
                        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pFeatureLayer, envelope);
                    }
                    else
                    {
                        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pFeatureLayer, null);
                    }
                    Logger.Current.Error("", cOMException, null);
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                    Logger.Current.Error("", exception, null);
                    if (pGeometry.GeometryType != esriGeometryType.esriGeometryPoint)
                    {
                        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pFeatureLayer, envelope);
                    }
                    else
                    {
                        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pFeatureLayer, null);
                    }
                    MessageBox.Show(exception.Message);
                }
            }
        }

        public static int CreateFeature(IGeometry pGeometry, IFeatureClass pFeatureClass)
        {
            double num;
            double num1;
            int oID;
            try
            {
                if ((pGeometry == null ? false : pFeatureClass != null))
                {
                    int num2 = pFeatureClass.FindField(pFeatureClass.ShapeFieldName);
                    IGeometryDef geometryDef = pFeatureClass.Fields.Field[num2].GeometryDef;
                    if (geometryDef.HasZ)
                    {
                        ((IZAware) pGeometry).ZAware = true;
                        if (pGeometry is IZ)
                        {
                            IZ igeometry0 = (IZ) pGeometry;
                            geometryDef.SpatialReference.GetZDomain(out num, out num1);
                            igeometry0.SetConstantZ(num);
                        }
                        else if (pGeometry is IPoint)
                        {
                            geometryDef.SpatialReference.GetZDomain(out num, out num1);
                            (pGeometry as IPoint).Z = num;
                        }
                    }
                    if (geometryDef.HasM)
                    {
                        ((IMAware) pGeometry).MAware = true;
                    }
                    IFeature feature = pFeatureClass.CreateFeature();
                    feature.Shape = pGeometry;
                    try
                    {
                        ((IRowSubtypes) feature).InitDefaultValues();
                    }
                    catch (Exception exception)
                    {
                        Logger.Current.Error("", exception, null);
                    }
                    EditorEvent.NewRow(feature);
                    feature.Store();
                    EditorEvent.AfterNewRow(feature);
                    oID = feature.OID;
                    return oID;
                }
            }
            catch (Exception exception1)
            {
                Logger.Current.Error("", exception1, null);
            }
            oID = -1;
            return oID;
        }

        public static void CreateFeature2(IGeometry pGeometry, IFeatureClass pFeatureClass)
        {
            double num;
            double num1;
            if (pGeometry != null && !pGeometry.IsEmpty)
            {
                pGeometry.Envelope.Expand(10, 10, false);
                try
                {
                    int num2 = pFeatureClass.FindField(pFeatureClass.ShapeFieldName);
                    IGeometryDef geometryDef = pFeatureClass.Fields.Field[num2].GeometryDef;
                    if (geometryDef.HasZ)
                    {
                        ((IZAware) pGeometry).ZAware = true;
                        if (pGeometry is IZ)
                        {
                            IZ igeometry0 = (IZ) pGeometry;
                            geometryDef.SpatialReference.GetZDomain(out num, out num1);
                            igeometry0.SetConstantZ(num);
                        }
                        else if (pGeometry is IPoint)
                        {
                            geometryDef.SpatialReference.GetZDomain(out num, out num1);
                            (pGeometry as IPoint).Z = num;
                        }
                    }
                    if (geometryDef.HasM)
                    {
                        ((IMAware) pGeometry).MAware = true;
                    }
                    IFeature feature = pFeatureClass.CreateFeature();
                    feature.Shape = pGeometry;
                    try
                    {
                        ((IRowSubtypes) feature).InitDefaultValues();
                    }
                    catch (Exception exception)
                    {
                        Logger.Current.Error("", exception, null);
                    }
                    EditorEvent.NewRow(feature);
                    feature.Store();
                }
                catch (COMException cOMException)
                {
                    Logger.Current.Error("", cOMException, null);
                }
                catch (Exception exception1)
                {
                    Logger.Current.Error("", exception1, null);
                }
            }
        }

        public static ITextElement MakeTextElement(string pString, double pAngle, IPoint pPoint)
        {
            ITextElement textElementClass = new TextElement() as ITextElement;


            textElementClass.ScaleText = true;
            textElementClass.Text = pString;

            (textElementClass as IGroupSymbolElement).SymbolID = 0;
            IElement ipoint0 = textElementClass as IElement;
            ipoint0.Geometry = pPoint;
            if (pAngle != 0)
            {
                (ipoint0 as ITransform2D).Rotate(pPoint, pAngle);
            }
            return textElementClass;
        }

        public static ITextElement MakeTextElement(string pString, double pAngle, IPoint pPoint, int int_0)
        {
            ITextElement textElementClass = new TextElement() as ITextElement;


            textElementClass.ScaleText = true;
            textElementClass.Text = pString;

            (textElementClass as IGroupSymbolElement).SymbolID = int_0;
            IElement ipoint0 = textElementClass as IElement;
            ipoint0.Geometry = pPoint;
            if (pAngle != 0)
            {
                (ipoint0 as ITransform2D).Rotate(pPoint, pAngle);
            }
            return textElementClass;
        }

        public static void SetGeometry(IGeometry pGeometry, IFeatureBuffer pFeatureBuffer, IFeatureClass pFeatureClass)
        {
            double num;
            double num1;
            try
            {
                int num2 = pFeatureClass.FindField(pFeatureClass.ShapeFieldName);
                IGeometryDef geometryDef = pFeatureClass.Fields.Field[num2].GeometryDef;
                if (geometryDef.HasZ)
                {
                    ((IZAware) pGeometry).ZAware = true;
                    if (pGeometry is IZ)
                    {
                        IZ igeometry0 = (IZ) pGeometry;
                        geometryDef.SpatialReference.GetZDomain(out num, out num1);
                        igeometry0.SetConstantZ(num);
                    }
                    else if (pGeometry is IPoint)
                    {
                        geometryDef.SpatialReference.GetZDomain(out num, out num1);
                        (pGeometry as IPoint).Z = num;
                    }
                }
                if (geometryDef.HasM)
                {
                    ((IMAware) pGeometry).MAware = true;
                }
                IFeature feature = pFeatureClass.CreateFeature();
                feature.Shape = pGeometry;
                try
                {
                    ((IRowSubtypes) feature).InitDefaultValues();
                }
                catch (Exception exception)
                {
                    Logger.Current.Error("", exception, null);
                }
                EditorEvent.NewRow(feature);
                feature.Store();
            }
            catch (COMException cOMException)
            {
                Logger.Current.Error("", cOMException, null);
            }
            catch (Exception exception1)
            {
                Logger.Current.Error("", exception1, null);
            }
        }

        public static void SetGeometry(IGeometry pGeometry, IFeature pFeature)
        {
            double num;
            double num1;
            IFeatureClass @class = pFeature.Class as IFeatureClass;
            try
            {
                int num2 = @class.FindField(@class.ShapeFieldName);
                IGeometryDef geometryDef = @class.Fields.Field[num2].GeometryDef;
                if (!geometryDef.HasZ)
                {
                    ((IZAware) pGeometry).ZAware = false;
                }
                else
                {
                    ((IZAware) pGeometry).ZAware = true;
                    if (pGeometry is IZ)
                    {
                        IZ igeometry0 = (IZ) pGeometry;
                        geometryDef.SpatialReference.GetZDomain(out num, out num1);
                        igeometry0.SetConstantZ(num);
                    }
                    else if (pGeometry is IPoint)
                    {
                        geometryDef.SpatialReference.GetZDomain(out num, out num1);
                        (pGeometry as IPoint).Z = num;
                    }
                }
                if (!geometryDef.HasM)
                {
                    ((IMAware) pGeometry).MAware = false;
                }
                else
                {
                    ((IMAware) pGeometry).MAware = true;
                }
                IFeature feature = @class.CreateFeature();
                (pGeometry as ITopologicalOperator6).SimplifyAsFeature();
                feature.Shape = pGeometry;
                try
                {
                    ((IRowSubtypes) feature).InitDefaultValues();
                }
                catch (Exception exception)
                {
                    Logger.Current.Error("", exception, null);
                }
                EditorEvent.NewRow(feature);
                feature.Store();
            }
            catch (COMException cOMException)
            {
                Logger.Current.Error("", cOMException, null);
            }
            catch (Exception exception1)
            {
                Logger.Current.Error("", exception1, null);
            }
        }
    }
}