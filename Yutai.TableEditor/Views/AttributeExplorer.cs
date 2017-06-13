using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.TableEditor.Editor;

namespace Yutai.Plugins.TableEditor.Views
{
    public partial class AttributeExplorer : Form, IAttributeExplorerView
    {
        private IAppContext _context;
        private IMapView _mapView;
        private IFeatureLayer _featureLayer;
        private DataTable _dataTable;
        private string _strGeometry;
        public AttributeExplorer(IAppContext context, IMapView mapView, IFeatureLayer featureLayer, string strGeometry)
        {
            InitializeComponent();
            _context = context;
            _mapView = mapView;
            _featureLayer = featureLayer;
            _dataTable = CreateDataTable(_featureLayer.FeatureClass.Fields);
            _strGeometry = strGeometry;
            LoadData(_dataTable, featureLayer.FeatureClass);
            txtField.FeatureClass = featureLayer.FeatureClass;
        }

        private DataTable CreateDataTable(IFields pFields)
        {
            DataTable pDataTable = new DataTable();
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                try
                {
                    IField field = pFields.Field[i];
                    DataColumn dataColumn = new DataColumn(field.Name)
                    {
                        Caption = field.AliasName
                    };
                    if (!(field.Domain is ICodedValueDomain))
                    {
                        if (field.Type == esriFieldType.esriFieldTypeDouble)
                        {
                            dataColumn.DataType = Type.GetType("System.Double");
                        }
                        else if (field.Type == esriFieldType.esriFieldTypeInteger)
                        {
                            dataColumn.DataType = Type.GetType("System.Int32");
                        }
                        else if (field.Type == esriFieldType.esriFieldTypeSmallInteger)
                        {
                            dataColumn.DataType = Type.GetType("System.Int16");
                        }
                        else if (field.Type == esriFieldType.esriFieldTypeSingle)
                        {
                            dataColumn.DataType = Type.GetType("System.Double");
                        }
                        else if (field.Type == esriFieldType.esriFieldTypeDate)
                        {
                            dataColumn.DataType = Type.GetType("System.DateTime");
                        }
                    }
                    if (field.Type == esriFieldType.esriFieldTypeBlob)
                    {
                        dataColumn.ReadOnly = true;
                    }
                    else if (field.Type != esriFieldType.esriFieldTypeGeometry)
                    {
                        dataColumn.ReadOnly = !field.Editable;
                    }
                    else
                    {
                        dataColumn.ReadOnly = true;
                    }
                    pDataTable.Columns.Add(dataColumn);
                }
                catch (Exception exception)
                {
                }
            }
            return pDataTable;
        }

        private void LoadData(DataTable pDataTable, IFeatureClass pFeatureClass)
        {
            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, false);
            IFeature pFeature;
            IFields pFields = pFeatureClass.Fields;
            object[] mStrGeometry = new object[pFields.FieldCount];
            while ((pFeature = pFeatureCursor.NextFeature()) != null)
            {
                for (int i = 0; i < pFields.FieldCount; i++)
                {
                    IField pField = pFields.Field[i];
                    if (pField.Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        mStrGeometry[i] = _strGeometry;
                    }
                    else if (pField.Type != esriFieldType.esriFieldTypeBlob)
                    {
                        object value = pFeature.Value[i];
                        mStrGeometry[i] = value;
                    }
                    else
                    {
                        mStrGeometry[i] = "二进制数据";
                    }
                }
                pDataTable.Rows.Add(mStrGeometry);
            }
            Marshal.ReleaseComObject(pFeatureCursor);
        }

        private void Filter(string fieldName, string strFilter)
        {
            DataView pDataView = _dataTable.DefaultView;
            pDataView.RowFilter = $" {fieldName} like '%{strFilter}%' ";
            colOID.DataPropertyName = _featureLayer.FeatureClass.OIDFieldName;
            colValue.DataPropertyName = fieldName;
            dataGridView.AutoGenerateColumns = false;
            dataGridView.DataSource = pDataView;
        }

        private void txtField_SelectComlateEvent(object sender, EventArgs e)
        {
            txtStrFilter.Focus();
            string fieldName = txtField.Field.Name;
            string strFilter = txtStrFilter.Text;
            Filter(fieldName, strFilter);
        }

        private void txtStrFilter_TextChanged(object sender, EventArgs e)
        {
            string fieldName = txtField.Field.Name;
            string strFilter = txtStrFilter.Text;
            Filter(fieldName, strFilter);
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (chkZoomToFeature.Checked == false)
                return;
            if (this.dataGridView.CurrentRow == null)
                return;
            int oid = Convert.ToInt32(this.dataGridView.CurrentRow.Cells[0].Value);
            if (oid <= -1)
                return;
            _mapView.ZoomToFeature(_featureLayer, oid);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

