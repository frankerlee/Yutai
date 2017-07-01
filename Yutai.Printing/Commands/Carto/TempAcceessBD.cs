using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.IO;
using System.Windows.Forms;

namespace Yutai.Plugins.Printing.Commands.Carto
{
    public class TempAcceessBD
    {
        private string tmpPath = "";

        private IWorkspaceName iworkspaceName_0 = null;

        private string string_1 = "";

        public TempAcceessBD()
        {
            this.tmpPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "tempPath");
            this.method_0();
        }

        ~TempAcceessBD()
        {
            try
            {
                if (File.Exists(this.string_1))
                {
                    File.Delete(this.string_1);
                }
            }
            catch
            {
            }
        }

        private void method_0()
        {
            if (!Directory.Exists(this.tmpPath))
            {
                Directory.CreateDirectory(this.tmpPath);
            }
        }

        public void CreateTempDB()
        {
            string text = this.tmpPath + "\\tempAccess";
            this.string_1 = PahtAssistant.GetFinalFileName(text, ".mdb");
            IWorkspaceFactory workspaceFactory = new AccessWorkspaceFactory();
            string directoryName = System.IO.Path.GetDirectoryName(this.string_1);
            string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(this.string_1);
            this.iworkspaceName_0 = workspaceFactory.Create(directoryName, fileNameWithoutExtension, null, 0);
        }

        public IFeatureClass CreateFeatureClass(IGeometry igeometry_0)
        {
            IFeatureClass result;
            try
            {
                IFeatureClassDescription featureClassDescription =
                    new FeatureClassDescription() as IFeatureClassDescription;
                IFields requiredFields = (featureClassDescription as IObjectClassDescription).RequiredFields;
                int index = requiredFields.FindField(featureClassDescription.ShapeFieldName);
                IField field = requiredFields.get_Field(index);
                IGeometryDef geometryDef = field.GeometryDef;
                if (igeometry_0.GeometryType == esriGeometryType.esriGeometryMultipoint ||
                    igeometry_0.GeometryType == esriGeometryType.esriGeometryPoint)
                {
                    (geometryDef as IGeometryDefEdit).GeometryType_2 = igeometry_0.GeometryType;
                }
                else
                {
                    if (igeometry_0.GeometryType != esriGeometryType.esriGeometryPolygon &&
                        igeometry_0.GeometryType != esriGeometryType.esriGeometryPolyline)
                    {
                        result = null;
                        return result;
                    }
                    (geometryDef as IGeometryDefEdit).GeometryType_2 = igeometry_0.GeometryType;
                }
                if (igeometry_0.SpatialReference != null)
                {
                    igeometry_0.SpatialReference = igeometry_0.SpatialReference;
                }
                (field as IFieldEdit).GeometryDef_2 = geometryDef;
                if (this.iworkspaceName_0 == null)
                {
                    this.CreateTempDB();
                }
                IWorkspace workspace = (this.iworkspaceName_0 as IName).Open() as IWorkspace;
                string name = "tempfc";
                IFeatureClass featureClass = (workspace as IFeatureWorkspace).CreateFeatureClass(name, requiredFields,
                    (featureClassDescription as IObjectClassDescription).InstanceCLSID,
                    (featureClassDescription as IObjectClassDescription).ClassExtensionCLSID,
                    esriFeatureType.esriFTSimple, featureClassDescription.ShapeFieldName, "");
                this.AddGeometry(featureClass, igeometry_0);
                result = featureClass;
                return result;
            }
            catch
            {
            }
            result = null;
            return result;
        }

        protected void AddGeometry(IFeatureClass ifeatureClass_0, IGeometry igeometry_0)
        {
            try
            {
                IFeature feature = ifeatureClass_0.CreateFeature();
                feature.Shape = igeometry_0;
                feature.Store();
            }
            catch
            {
            }
        }
    }
}