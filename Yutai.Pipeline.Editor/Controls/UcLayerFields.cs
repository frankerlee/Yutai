using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Editor.Forms;
using Yutai.Pipeline.Editor.Helper;

namespace Yutai.Pipeline.Editor.Controls
{
    public partial class UcLayerFields : UserControl
    {
        private IFeatureLayer _featureLayer;
        private List<IField> _selectedFieldList;
        private IDictionary<int, int> _fieldMappingInts;
        public UcLayerFields()
        {
            InitializeComponent();
        }

        public UcLayerFields(IFeatureLayer featureLayer)
        {
            InitializeComponent();
            FeatureLayer = featureLayer;
            DefaultFields();
        }

        public bool Checked
        {
            get { return chkLayer.Checked; }
            set { chkLayer.Checked = value; }
        }

        public IFeatureLayer FeatureLayer
        {
            get { return _featureLayer; }
            set
            {
                _featureLayer = value;
                chkLayer.Text = _featureLayer.Name;
            }
        }

        public List<IField> SelectedFieldList => _selectedFieldList;

        public IDictionary<int, int> FieldMappingInts => _fieldMappingInts;

        private void btnSelectFields_Click(object sender, EventArgs e)
        {
            FrmSelectFields frm = new FrmSelectFields(_featureLayer.FeatureClass.Fields, _selectedFieldList);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                _selectedFieldList = frm.SelectedFieldList;
            }
        }

        public void DefaultFields()
        {
            _selectedFieldList = new List<IField>();
            IFields fields = _featureLayer.FeatureClass.Fields;
            for (int i = 0; i < fields.FieldCount; i++)
            {
                _selectedFieldList.Add(fields.Field[i]);
            }
        }

        private void chkLayer_MouseMove(object sender, MouseEventArgs e)
        {
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        private void chkLayer_MouseLeave(object sender, EventArgs e)
        {
            this.BorderStyle = BorderStyle.None;
        }

        private void btnSelectFields_MouseMove(object sender, MouseEventArgs e)
        {
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        private void btnSelectFields_MouseLeave(object sender, EventArgs e)
        {
            this.BorderStyle = BorderStyle.None;
        }

        public IFeatureClass CreateFeatureClass(IWorkspace workspace, IFeatureDataset featureDataset = null, ISpatialReference spatialReference = null)
        {
            try
            {
                _fieldMappingInts = new Dictionary<int, int>();
                IFeatureWorkspace featureWorkspace = workspace as IFeatureWorkspace;
                if (featureWorkspace == null)
                    return null;
                IFeatureClassDescription featureClassDescription = new FeatureClassDescriptionClass();
                IObjectClassDescription objectClassDescription = featureClassDescription as IObjectClassDescription;
                IFields requiredFields = objectClassDescription.RequiredFields;
                IFieldsEdit requiredFieldsEdit = requiredFields as IFieldsEdit;
                for (int i = 0; i < requiredFields.FieldCount; i++)
                {
                    if (requiredFieldsEdit.Field[i].Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        IFieldEdit requiredFieldEdit = requiredFieldsEdit.Field[i] as IFieldEdit;
                        IGeometryDef geometryDef = new GeometryDefClass();
                        IGeometryDefEdit geometryDefEdit = geometryDef as IGeometryDefEdit;
                        geometryDefEdit.GeometryType_2 = _featureLayer.FeatureClass.ShapeType;
                        geometryDefEdit.SpatialReference_2 = spatialReference ?? FeatureClassUtil.GetGeometryDef(_featureLayer.FeatureClass).SpatialReference;
                        requiredFieldEdit.GeometryDef_2 = geometryDefEdit;
                    }
                }
                for (int i = 0; i < _selectedFieldList.Count; i++)
                {
                    IField field = _selectedFieldList[i];
                    if (requiredFieldsEdit.FindField(field.Name) >= 0)
                        continue;
                    IField newField = new FieldClass();
                    IFieldEdit newFieldEdit = newField as IFieldEdit;
                    newFieldEdit.Name_2 = field.Name;
                    newFieldEdit.AliasName_2 = field.AliasName;
                    newFieldEdit.DefaultValue_2 = field.DefaultValue;
                    newFieldEdit.Domain_2 = field.Domain;
                    newFieldEdit.DomainFixed_2 = field.DomainFixed;
                    newFieldEdit.Editable_2 = field.Editable;
                    newFieldEdit.IsNullable_2 = field.IsNullable;
                    newFieldEdit.Length_2 = field.Length;
                    newFieldEdit.Precision_2 = field.Precision;
                    newFieldEdit.Required_2 = field.Required;
                    newFieldEdit.Scale_2 = field.Scale;
                    newFieldEdit.Type_2 = field.Type;
                    requiredFieldsEdit.AddField(newFieldEdit);
                }

                IFieldChecker fieldChecker = new FieldCheckerClass();
                IEnumFieldError enumFieldError = null;
                IFields validatedFields = null;
                fieldChecker.ValidateWorkspace = workspace;
                fieldChecker.Validate(requiredFields, out enumFieldError, out validatedFields);
                string esriName = ((IDataset)_featureLayer.FeatureClass).Name;
                IFeatureClass featureClass = null;
                if (featureDataset == null)
                    featureClass = featureWorkspace.CreateFeatureClass(esriName, requiredFields, objectClassDescription.InstanceCLSID,
                        objectClassDescription.ClassExtensionCLSID, esriFeatureType.esriFTSimple,
                        _featureLayer.FeatureClass.ShapeFieldName, "");
                else
                    featureClass = featureDataset.CreateFeatureClass(esriName, requiredFields, objectClassDescription.InstanceCLSID,
                        objectClassDescription.ClassExtensionCLSID, esriFeatureType.esriFTSimple,
                        _featureLayer.FeatureClass.ShapeFieldName, "");

                foreach (IField field in _selectedFieldList)
                {
                    if (field.Editable)
                    {
                        _fieldMappingInts.Add(featureClass.FindField(field.Name), _featureLayer.FeatureClass.FindField(field.Name));
                    }
                }

                return featureClass;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public IFeatureClass CreateShapefile(string workspacePath, ISpatialReference spatialReference = null)
        {
            Type factoryType = Type.GetTypeFromProgID("esriDataSourcesFile.ShapefileWorkspaceFactory");
            IWorkspaceFactory shapefileWorkspaceFactory = Activator.CreateInstance(factoryType) as IWorkspaceFactory;
            if (shapefileWorkspaceFactory == null)
                return null;
            IWorkspace workspace = shapefileWorkspaceFactory.OpenFromFile(workspacePath, 0);
            return CreateFeatureClass(workspace, null, spatialReference);
        }
    }
}
