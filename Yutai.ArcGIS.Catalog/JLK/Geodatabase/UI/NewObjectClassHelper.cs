namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Carto;
    using ESRI.ArcGIS.Display;
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.Geometry;
    using JLK.Utility;
    using JLK.Utility.CodeDomainEx;
    using JLK.Utility.Geodatabase;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class NewObjectClassHelper
    {
        private bool bool_0 = false;
        private bool bool_1 = true;
        private bool bool_2 = true;
        private bool bool_3 = false;
        private bool bool_4 = false;
        private bool bool_5 = false;
        private bool bool_6 = false;
        private bool bool_7 = false;
        private bool bool_8 = false;
        private esriFeatureType esriFeatureType_0 = esriFeatureType.esriFTSimple;
        private esriGeometryType esriGeometryType_0 = esriGeometryType.esriGeometryPolygon;
        public SortedList<IField, JLK.Utility.CodeDomainEx.CodeDomainEx> FieldDomains = new SortedList<IField, JLK.Utility.CodeDomainEx.CodeDomainEx>();
        private IFeatureClass ifeatureClass_0 = null;
        private IFeatureDataset ifeatureDataset_0 = null;
        private IField ifield_0 = null;
        private IFields ifields_0 = null;
        private IObjectClass iobjectClass_0 = null;
        private ISpatialReference ispatialReference_0 = null;
        private IWorkspace iworkspace_0 = null;
        public IAnnotateLayerPropertiesCollection2 m_pAnnoPropertiesColn = new AnnotateLayerPropertiesCollectionClass();
        public static NewObjectClassHelper m_pObjectClassHelper;
        public ISymbolCollection2 m_pSymbolColl = new SymbolCollectionClass();
        public double m_RefScale = 500.0;
        public esriUnits m_Units = esriUnits.esriUnknownUnits;
        private string string_0 = "";
        private string string_1 = "";

        static NewObjectClassHelper()
        {
            old_acctor_mc();
        }

        public IFeatureDataset CreateFeatureDataset()
        {
            try
            {
                return (this.iworkspace_0 as IFeatureWorkspace).CreateFeatureDataset(this.string_0, this.ispatialReference_0);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            return null;
        }

        public IObjectClass CreateObjectClass()
        {
            IObjectClass class2 = null;
            COMException exception;
            if (this.bool_2)
            {
                IFeatureClass class4;
                if (this.ifeatureDataset_0 != null)
                {
                    if ((this.ifeatureDataset_0.Workspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureClass, this.Name))
                    {
                        MessageBox.Show("要素已存在，请重新给定要素名");
                        return null;
                    }
                    if (this.esriFeatureType_0 == esriFeatureType.esriFTAnnotation)
                    {
                        class4 = null;
                        if (this.IsRelatedFeature)
                        {
                            class4 = this.ifeatureClass_0;
                        }
                        try
                        {
                            class2 = this.method_1(this.Name, this.m_RefScale, this.ifields_0, this.ifeatureDataset_0, class4, this.iworkspace_0 as IFeatureWorkspaceAnno, this.m_Units, this.m_pAnnoPropertiesColn, this.m_pSymbolColl);
                        }
                        catch (COMException exception1)
                        {
                            exception = exception1;
                            if (exception.ErrorCode == -2147220960)
                            {
                                MessageBox.Show("该数据库版本较低,请先升级数据库!");
                            }
                            else
                            {
                                MessageBox.Show(exception.Message);
                            }
                            class2 = null;
                        }
                    }
                    else
                    {
                        try
                        {
                            class2 = this.ifeatureDataset_0.CreateFeatureClass(this.Name, this.ifields_0, null, null, this.esriFeatureType_0, this.ifield_0.Name, "");
                        }
                        catch (COMException exception3)
                        {
                            exception = exception3;
                            if (exception.ErrorCode == -2147220960)
                            {
                                MessageBox.Show("该数据库版本较低,请先升级数据库!");
                            }
                            else
                            {
                                MessageBox.Show(exception.Message);
                            }
                            class2 = null;
                        }
                    }
                }
                else
                {
                    if (ObjectClassShareData.m_IsShapeFile)
                    {
                        if (File.Exists(this.iworkspace_0.PathName + @"\" + this.Name + ".shp"))
                        {
                            MessageBox.Show("文件已存在，请重新给定要素名");
                            return null;
                        }
                        try
                        {
                            class2 = (this.iworkspace_0 as IFeatureWorkspace).CreateFeatureClass(this.Name, this.ifields_0, null, null, this.esriFeatureType_0, this.ifield_0.Name, "");
                            goto Label_03FA;
                        }
                        catch (Exception exception2)
                        {
                            CErrorLog.writeErrorLog(this, exception2, "创建shape文件");
                            return null;
                        }
                    }
                    if ((this.iworkspace_0 as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureClass, this.Name))
                    {
                        MessageBox.Show("要素已存在，请重新给定要素名");
                        return null;
                    }
                    if (this.esriFeatureType_0 == esriFeatureType.esriFTAnnotation)
                    {
                        class4 = null;
                        if (this.IsRelatedFeature)
                        {
                            class4 = this.ifeatureClass_0;
                        }
                        try
                        {
                            class2 = this.method_1(this.Name, this.m_RefScale, this.ifields_0, null, class4, this.iworkspace_0 as IFeatureWorkspaceAnno, this.m_Units, this.m_pAnnoPropertiesColn, this.m_pSymbolColl);
                            goto Label_03FA;
                        }
                        catch (COMException exception5)
                        {
                            exception = exception5;
                            if (exception.ErrorCode == -2147220960)
                            {
                                MessageBox.Show("该数据库版本较低,请先升级数据库!");
                            }
                            else
                            {
                                MessageBox.Show(exception.Message);
                            }
                            class2 = null;
                            goto Label_03FA;
                        }
                    }
                    try
                    {
                        class2 = (this.iworkspace_0 as IFeatureWorkspace).CreateFeatureClass(this.Name, this.ifields_0, null, null, this.esriFeatureType_0, this.ifield_0.Name, "");
                    }
                    catch (COMException exception6)
                    {
                        exception = exception6;
                        if (exception.ErrorCode == -2147155646)
                        {
                            MessageBox.Show("对象名 [" + this.Name + "] 不符合命名要求，请输入合适的对象名!");
                        }
                        else if (exception.ErrorCode == -2147220960)
                        {
                            MessageBox.Show("该数据库版本较低,请先升级数据库!");
                        }
                        else
                        {
                            MessageBox.Show(exception.Message);
                        }
                        class2 = null;
                    }
                }
            }
            else
            {
                try
                {
                    class2 = (this.iworkspace_0 as IFeatureWorkspace).CreateTable(this.Name, this.ifields_0, null, null, "") as IObjectClass;
                }
                catch (COMException exception7)
                {
                    exception = exception7;
                    if (exception.ErrorCode == -2147155646)
                    {
                        MessageBox.Show("对象名 [" + this.Name + "] 不符合命名要求，请输入合适的对象名!");
                    }
                    else if (exception.ErrorCode == -2147220960)
                    {
                        MessageBox.Show("该数据库版本较低,请先升级数据库!");
                    }
                    else
                    {
                        MessageBox.Show(exception.Message);
                    }
                    class2 = null;
                }
            }
        Label_03FA:
            if (class2 != null)
            {
                if (!ObjectClassShareData.m_IsShapeFile && (this.AliasName.Length > 0))
                {
                    (class2 as IClassSchemaEdit).AlterAliasName(this.AliasName);
                }
                this.method_2(this.Name);
            }
            return class2;
        }

        public bool FieldDomanIsExit(IField ifield_1)
        {
            for (int i = 0; i < this.FieldDomains.Keys.Count; i++)
            {
                IField field = this.FieldDomains.Keys[i];
                if (field == ifield_1)
                {
                    return true;
                }
            }
            return false;
        }

        public void GetDomain(out double double_0, out double double_1, out double double_2, out double double_3)
        {
            this.ispatialReference_0.GetDomain(out double_0, out double_1, out double_2, out double_3);
        }

        public void GetMDomain(out double double_0, out double double_1)
        {
            double_0 = 0.0;
            double_1 = 0.0;
            try
            {
                this.ispatialReference_0.GetMDomain(out double_0, out double_1);
            }
            catch
            {
            }
        }

        public void GetZDomain(out double double_0, out double double_1)
        {
            double_0 = 0.0;
            double_1 = 0.0;
            try
            {
                this.ispatialReference_0.GetZDomain(out double_0, out double_1);
            }
            catch
            {
            }
        }

        public void InitFields()
        {
            IFields requiredFields;
            if (this.IsFeatureClass)
            {
                IFeatureClassDescription description;
                IGeometryDef geometryDef;
                if (this.FeatureType == esriFeatureType.esriFTSimple)
                {
                    description = new FeatureClassDescriptionClass();
                    requiredFields = (description as IObjectClassDescription).RequiredFields;
                    this.ifield_0 = requiredFields.get_Field(requiredFields.FindField(description.ShapeFieldName));
                    geometryDef = (this.ifield_0 as IFieldEdit2).GeometryDef;
                    (geometryDef as IGeometryDefEdit).GeometryType = m_pObjectClassHelper.ShapeType;
                    if (this.ispatialReference_0 != null)
                    {
                        (geometryDef as IGeometryDefEdit).SpatialReference = this.ispatialReference_0;
                    }
                    (geometryDef as IGeometryDefEdit).HasZ = this.bool_3;
                    (geometryDef as IGeometryDefEdit).HasM = this.bool_4;
                    (this.ifield_0 as IFieldEdit).GeometryDef = geometryDef as IGeometryDefEdit;
                    this.ifields_0 = requiredFields;
                }
                else if (this.FeatureType == esriFeatureType.esriFTAnnotation)
                {
                    description = new AnnotationFeatureClassDescriptionClass();
                    requiredFields = (description as IObjectClassDescription).RequiredFields;
                    this.ifield_0 = requiredFields.get_Field(requiredFields.FindField(description.ShapeFieldName));
                    geometryDef = (this.ifield_0 as IFieldEdit2).GeometryDef;
                    if (this.ispatialReference_0 != null)
                    {
                        (geometryDef as IGeometryDefEdit).SpatialReference = this.ispatialReference_0;
                    }
                    this.ifields_0 = requiredFields;
                }
                else if (this.FeatureType == esriFeatureType.esriFTDimension)
                {
                    description = new DimensionClassDescriptionClass();
                    requiredFields = (description as IObjectClassDescription).RequiredFields;
                    this.ifield_0 = requiredFields.get_Field(requiredFields.FindField(description.ShapeFieldName));
                    geometryDef = (this.ifield_0 as IFieldEdit2).GeometryDef;
                    if (this.ispatialReference_0 != null)
                    {
                        (geometryDef as IGeometryDefEdit).SpatialReference = this.ispatialReference_0;
                    }
                    this.ifields_0 = requiredFields;
                }
            }
            else
            {
                IObjectClassDescription description2 = new FeatureClassDescriptionClass();
                requiredFields = description2.RequiredFields;
                this.ifields_0 = requiredFields;
            }
        }

        private void method_0(ISpatialReference ispatialReference_1)
        {
            if (this.ifield_0 != null)
            {
                IGeometryDef geometryDef = (this.ifield_0 as IFieldEdit2).GeometryDef;
                if (ispatialReference_1 != null)
                {
                    (geometryDef as IGeometryDefEdit).SpatialReference = ispatialReference_1;
                }
                (this.ifield_0 as IFieldEdit).GeometryDef = geometryDef as IGeometryDefEdit;
            }
        }

        private IFeatureClass method_1(string string_2, double double_0, IFields ifields_1, IFeatureDataset ifeatureDataset_1, IFeatureClass ifeatureClass_1, IFeatureWorkspaceAnno ifeatureWorkspaceAnno_0, esriUnits esriUnits_0, IAnnotateLayerPropertiesCollection2 iannotateLayerPropertiesCollection2_0, ISymbolCollection2 isymbolCollection2_0)
        {
            IObjectClassDescription description = new AnnotationFeatureClassDescriptionClass();
            IGraphicsLayerScale referenceScale = new GraphicsLayerScaleClass {
                ReferenceScale = double_0,
                Units = esriUnits_0
            };
            UID instanceCLSID = description.InstanceCLSID;
            UID classExtensionCLSID = description.ClassExtensionCLSID;
            return ifeatureWorkspaceAnno_0.CreateAnnotationClass(string_2, ifields_1, instanceCLSID, classExtensionCLSID, "Shape", "", ifeatureDataset_1, ifeatureClass_1, iannotateLayerPropertiesCollection2_0, referenceScale, isymbolCollection2_0, true);
        }

        private void method_2(string string_2)
        {
            for (int i = 0; i < this.ifields_0.FieldCount; i++)
            {
                IField key = this.ifields_0.get_Field(i);
                if ((key.Editable && ((((key.Type != esriFieldType.esriFieldTypeOID) && (key.Type != esriFieldType.esriFieldTypeGeometry)) && (key.Type != esriFieldType.esriFieldTypeRaster)) && (key.Type != esriFieldType.esriFieldTypeBlob))) && this.FieldDomains.ContainsKey(key))
                {
                    JLK.Utility.CodeDomainEx.CodeDomainEx ex = this.FieldDomains[key];
                    if (ex != null)
                    {
                        CodeDomainManage.AddFieldCodeDoaminMap(string_2, key.Name, ex.DomainID);
                    }
                    else
                    {
                        CodeDomainManage.DeleteCodeDoaminMap(string_2, key.Name);
                    }
                }
            }
        }

        private static void old_acctor_mc()
        {
            m_pObjectClassHelper = null;
        }

        public void SetDomain(double double_0, double double_1, double double_2, double double_3)
        {
            this.ispatialReference_0.SetDomain(double_0, double_1, double_2, double_3);
        }

        public void SetMDomain(double double_0, double double_1)
        {
            this.ispatialReference_0.SetMDomain(double_0, double_1);
        }

        public void SetZDomain(double double_0, double double_1)
        {
            this.ispatialReference_0.SetZDomain(double_0, double_1);
        }

        public string AliasName
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public IFeatureDataset FeatureDataset
        {
            get
            {
                return this.ifeatureDataset_0;
            }
            set
            {
                this.ifeatureDataset_0 = value;
                this.iworkspace_0 = this.ifeatureDataset_0.Workspace;
                ObjectClassShareData.m_IsShapeFile = false;
                this.bool_6 = GeodatabaseTools.GetGeoDatasetPrecision(this.iworkspace_0 as IGeodatabaseRelease);
                this.bool_2 = true;
                this.ispatialReference_0 = (this.ifeatureDataset_0 as IGeoDataset).SpatialReference;
            }
        }

        public esriFeatureType FeatureType
        {
            get
            {
                return this.esriFeatureType_0;
            }
            set
            {
                this.esriFeatureType_0 = value;
            }
        }

        public IFields Fields
        {
            get
            {
                return this.ifields_0;
            }
            set
            {
                this.ifields_0 = value;
            }
        }

        public bool HasM
        {
            get
            {
                return this.bool_4;
            }
            set
            {
                this.bool_4 = value;
            }
        }

        public bool HasZ
        {
            get
            {
                return this.bool_3;
            }
            set
            {
                this.bool_3 = value;
            }
        }

        public bool IsEdit
        {
            get
            {
                return this.bool_8;
            }
            set
            {
                this.bool_8 = value;
            }
        }

        public bool IsFeatureClass
        {
            get
            {
                return this.bool_2;
            }
            set
            {
                this.bool_2 = value;
            }
        }

        public bool IsHighPrecision
        {
            get
            {
                return this.bool_6;
            }
        }

        public bool IsNewFeatureDataset
        {
            get
            {
                return this.bool_7;
            }
            set
            {
                this.bool_7 = value;
            }
        }

        public bool IsRelatedFeature
        {
            get
            {
                return this.bool_5;
            }
            set
            {
                this.bool_5 = value;
            }
        }

        public bool IsShapefile
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        public double MResolution
        {
            get
            {
                if (this.ispatialReference_0 == null)
                {
                    return 0.0;
                }
                return (this.ispatialReference_0 as ISpatialReferenceResolution).MResolution;
            }
            set
            {
                if (this.ispatialReference_0 != null)
                {
                    (this.ispatialReference_0 as ISpatialReferenceResolution).MResolution = value;
                }
            }
        }

        public double MTolerance
        {
            get
            {
                if (this.ispatialReference_0 == null)
                {
                    return 0.0;
                }
                return (this.ispatialReference_0 as ISpatialReferenceTolerance).MTolerance;
            }
            set
            {
                if (this.ispatialReference_0 != null)
                {
                    (this.ispatialReference_0 as ISpatialReferenceTolerance).MTolerance = value;
                }
            }
        }

        public string Name
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public IObjectClass ObjectClass
        {
            get
            {
                return this.iobjectClass_0;
            }
            set
            {
                this.FieldDomains.Clear();
                this.bool_8 = true;
                this.iobjectClass_0 = value;
                if (this.iobjectClass_0 is IFeatureClass)
                {
                    this.esriGeometryType_0 = (this.iobjectClass_0 as IFeatureClass).ShapeType;
                    this.bool_2 = true;
                    this.ispatialReference_0 = (this.iobjectClass_0 as IGeoDataset).SpatialReference;
                    this.bool_6 = (this.ispatialReference_0 as IControlPrecision2).IsHighPrecision;
                    this.ifields_0 = this.iobjectClass_0.Fields;
                    this.string_0 = (this.iobjectClass_0 as IDataset).Name;
                    this.string_1 = this.iobjectClass_0.AliasName;
                    IFields fields = (this.iobjectClass_0 as IFeatureClass).Fields;
                    this.ifield_0 = fields.get_Field(fields.FindField((this.iobjectClass_0 as IFeatureClass).ShapeFieldName));
                    this.bool_3 = this.ifield_0.GeometryDef.HasZ;
                    this.bool_4 = this.ifield_0.GeometryDef.HasM;
                }
                else
                {
                    this.bool_2 = false;
                }
            }
        }

        public IFeatureClass RelatedFeatureClass
        {
            get
            {
                return this.ifeatureClass_0;
            }
            set
            {
                this.ifeatureClass_0 = value;
            }
        }

        public IField ShapeFild
        {
            get
            {
                return this.ifield_0;
            }
            set
            {
                this.ifield_0 = value;
            }
        }

        public esriGeometryType ShapeType
        {
            get
            {
                return this.esriGeometryType_0;
            }
            set
            {
                this.esriGeometryType_0 = value;
            }
        }

        public ISpatialReference SpatialReference
        {
            get
            {
                return this.ispatialReference_0;
            }
            set
            {
                this.ispatialReference_0 = value;
                this.method_0(this.ispatialReference_0);
            }
        }

        public bool UseDefaultDomain
        {
            get
            {
                return this.bool_1;
            }
            set
            {
                this.bool_1 = value;
            }
        }

        public IWorkspace Workspace
        {
            get
            {
                return this.iworkspace_0;
            }
            set
            {
                this.iworkspace_0 = value;
                this.ifeatureDataset_0 = null;
                this.bool_6 = GeodatabaseTools.GetGeoDatasetPrecision(this.iworkspace_0 as IGeodatabaseRelease);
            }
        }

        public double XYResolution
        {
            get
            {
                if (this.ispatialReference_0 == null)
                {
                    return 0.0;
                }
                return (this.ispatialReference_0 as ISpatialReferenceResolution).get_XYResolution(true);
            }
            set
            {
                if (this.ispatialReference_0 != null)
                {
                    (this.ispatialReference_0 as ISpatialReferenceResolution).set_XYResolution(true, value);
                }
            }
        }

        public double XYTolerance
        {
            get
            {
                if (this.ispatialReference_0 == null)
                {
                    return 0.0;
                }
                return (this.ispatialReference_0 as ISpatialReferenceTolerance).XYTolerance;
            }
            set
            {
                if (this.ispatialReference_0 != null)
                {
                    (this.ispatialReference_0 as ISpatialReferenceTolerance).XYTolerance = value;
                }
            }
        }

        public double ZResolution
        {
            get
            {
                if (this.ispatialReference_0 == null)
                {
                    return 0.0;
                }
                return (this.ispatialReference_0 as ISpatialReferenceResolution).get_ZResolution(true);
            }
            set
            {
                if (this.ispatialReference_0 != null)
                {
                    (this.ispatialReference_0 as ISpatialReferenceResolution).set_ZResolution(true, value);
                }
            }
        }

        public double ZTolerance
        {
            get
            {
                if (this.ispatialReference_0 == null)
                {
                    return 0.0;
                }
                return (this.ispatialReference_0 as ISpatialReferenceTolerance).ZTolerance;
            }
            set
            {
                if (this.ispatialReference_0 != null)
                {
                    (this.ispatialReference_0 as ISpatialReferenceTolerance).ZTolerance = value;
                }
            }
        }
    }
}

