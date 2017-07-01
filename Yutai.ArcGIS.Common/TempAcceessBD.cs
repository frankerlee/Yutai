using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Path = System.IO.Path;


namespace Yutai.ArcGIS.Common
{
    public class TempAcceessBD
    {
        private string string_0 = "";

        private IWorkspaceName iworkspaceName_0 = null;

        private string string_1 = "";

        public TempAcceessBD()
        {
            this.string_0 = Path.Combine(Application.StartupPath, "tempPath");
            this.method_0();
        }

        protected void AddGeometry(IFeatureClass ifeatureClass_0, IGeometry igeometry_0)
        {
            try
            {
                IFeature igeometry0 = ifeatureClass_0.CreateFeature();
                igeometry0.Shape = igeometry_0;
                igeometry0.Store();
            }
            catch
            {
            }
        }

        public IFeatureClass CreateFeatureClass(IGeometry igeometry_0)
        {
            IFeatureClass featureClass;
            try
            {
                IFeatureClassDescription featureClassDescriptionClass =
                    new FeatureClassDescription() as IFeatureClassDescription;
                IFields requiredFields = (featureClassDescriptionClass as IObjectClassDescription).RequiredFields;
                IField field =
                    requiredFields.Field[requiredFields.FindField(featureClassDescriptionClass.ShapeFieldName)];
                IGeometryDef geometryDef = field.GeometryDef;
                if (
                    !(igeometry_0.GeometryType == esriGeometryType.esriGeometryMultipoint
                        ? false
                        : igeometry_0.GeometryType != esriGeometryType.esriGeometryPoint))
                {
                    (geometryDef as IGeometryDefEdit).GeometryType_2 = igeometry_0.GeometryType;
                }
                else if ((igeometry_0.GeometryType == esriGeometryType.esriGeometryPolygon
                    ? false
                    : igeometry_0.GeometryType != esriGeometryType.esriGeometryPolyline))
                {
                    featureClass = null;
                    return featureClass;
                }
                else
                {
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
                string str = "tempfc";
                IFeatureClass featureClass1 = (workspace as IFeatureWorkspace).CreateFeatureClass(str, requiredFields,
                    (featureClassDescriptionClass as IObjectClassDescription).InstanceCLSID,
                    (featureClassDescriptionClass as IObjectClassDescription).ClassExtensionCLSID,
                    esriFeatureType.esriFTSimple, featureClassDescriptionClass.ShapeFieldName, "");
                this.AddGeometry(featureClass1, igeometry_0);
                featureClass = featureClass1;
                return featureClass;
            }
            catch
            {
            }
            featureClass = null;
            return featureClass;
        }

        public void CreateTempDB()
        {
            string str = string.Concat(this.string_0, "\\tempAccess");
            this.string_1 = PahtAssistant.GetFinalFileName(str, ".mdb");
            IWorkspaceFactory accessWorkspaceFactoryClass = new AccessWorkspaceFactory();
            string directoryName = Path.GetDirectoryName(this.string_1);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.string_1);
            this.iworkspaceName_0 = accessWorkspaceFactoryClass.Create(directoryName, fileNameWithoutExtension, null, 0);
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
            if (!Directory.Exists(this.string_0))
            {
                Directory.CreateDirectory(this.string_0);
            }
        }
    }
}