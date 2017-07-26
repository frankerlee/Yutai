using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Plugins.Template.Concretes;
using Yutai.Plugins.Template.Controls;
using Yutai.Plugins.Template.Interfaces;

namespace Yutai.Plugins.Template.Forms
{
    public partial class frmEditTemplate : Form
    {
        private IObjectTemplate _template;
        private IObjectTemplate _tmpTemplate;
        private ITemplateDatabase _database;
        List<ComboxItemInfo> _featureTypes = new List<ComboxItemInfo>();
        List<ComboxItemInfo> _geometryTypes = new List<ComboxItemInfo>();
        List<ComboxItemInfo> _datasets = new List<ComboxItemInfo>();
        List<ComboxItemInfo> _fieldTypes = new List<ComboxItemInfo>();
        List<ComboxItemInfo> _domains = new List<ComboxItemInfo>();
        DataTable _fieldTable = new DataTable("FieldTable");
        private bool isFig = false;
        private bool _isChanged = false;
        private bool _hasChanged = false;

        public frmEditTemplate()
        {
            InitializeComponent();
            isFig = true;
            _featureTypes.Add(new ComboxItemInfo("Simple", "简单要素"));
            _featureTypes.Add(new ComboxItemInfo("Annotation", "文本要素"));
            _featureTypes.Add(new ComboxItemInfo("Complex", "复杂要素"));
            _geometryTypes.Add(new ComboxItemInfo("Point", "点图形", "Simple"));
            _geometryTypes.Add(new ComboxItemInfo("Polyline", "线图形", "Simple"));
            _geometryTypes.Add(new ComboxItemInfo("Polygon", "面图形", "Simple"));
            _geometryTypes.Add(new ComboxItemInfo("Polygon", "文本面", "Annotation"));
            _geometryTypes.Add(new ComboxItemInfo("MultiPatch", "三维面", "Complex"));

            cmbFeatureType.DataSource = _featureTypes;
            cmbFeatureType.DisplayMember = "Display";
            cmbFeatureType.ValueMember = "Value";

        

            DataColumn column = new DataColumn("FieldName", typeof(string));
            column.Caption = "字段名称";
            column.AllowDBNull = false;
            _fieldTable.Columns.Add(column);

            column = new DataColumn("AliasName", typeof(string));
            column.Caption = "字段别名";
            column.AllowDBNull = true;
            _fieldTable.Columns.Add(column);

            column = new DataColumn("AllowNull", typeof(bool));
            column.Caption = "允许空值";
            column.AllowDBNull = true;
            column.DefaultValue = true;
            _fieldTable.Columns.Add(column);

            column = new DataColumn("IsKey", typeof(bool));
            column.Caption = "是否键值";
            column.AllowDBNull = true;
            column.DefaultValue = false;
            _fieldTable.Columns.Add(column);

            column = new DataColumn("FieldType", typeof(string));
            column.Caption = "类型";
            column.DefaultValue = "String";
            column.AllowDBNull = false;
            _fieldTable.Columns.Add(column);

            column = new DataColumn("FieldLength", typeof(int));
            column.Caption = "长度";
            column.DefaultValue = 50;
            column.AllowDBNull = true;
            _fieldTable.Columns.Add(column);

            column = new DataColumn("Precision", typeof(int));
            column.Caption = "小数位";
            column.DefaultValue = 0;
            column.AllowDBNull = true;
            _fieldTable.Columns.Add(column);

            column = new DataColumn("DomainName", typeof(string));
            column.Caption = "数据字典";
            column.DefaultValue = "";
            column.AllowDBNull = true;
            _fieldTable.Columns.Add(column);
            gridControl1.DataSource = _fieldTable;

            _fieldTypes.Add(new ComboxItemInfo("SmallInteger", "短整形"));
            _fieldTypes.Add(new ComboxItemInfo("Integer", "长整形"));
            _fieldTypes.Add(new ComboxItemInfo("Single", "单精度"));
            _fieldTypes.Add(new ComboxItemInfo("Double", "双精度"));
            _fieldTypes.Add(new ComboxItemInfo("String", "文字"));
            _fieldTypes.Add(new ComboxItemInfo("Date", "日期型"));
            _fieldTypes.Add(new ComboxItemInfo("Blob", "二进制"));
            _fieldTypes.Add(new ComboxItemInfo("Guid", "Guid"));
            _fieldTypes.Add(new ComboxItemInfo("Raster", "栅格"));
            _fieldTypes.Add(new ComboxItemInfo("GlobalID", "GlobalID"));
            repositoryItemLookUpEdit1.DataSource = _fieldTypes;
            repositoryItemLookUpEdit1.ValueMember = "Value";
            repositoryItemLookUpEdit1.DisplayMember = "Display";
            btnImportFC.Visible = false;


            GridColumn delLinkHiper = new GridColumn();
            delLinkHiper.Caption = "删除";
            delLinkHiper.Visible = true;
            delLinkHiper.ColumnEdit = btnDeleteField;
            gridView1.Columns.Add(delLinkHiper);
            delLinkHiper.VisibleIndex = 8;

            gridView1.BestFitColumns();

            isFig = false;

        }

        public void SetTemplate(IObjectTemplate template)
        {
            _template = template;
            LoadTemplateValues();
        }

        public IObjectTemplate GetTemplate()
        {
            return _template;
        }

        public void SetDatabase(ITemplateDatabase database)
        {
            _database = database;
        }

        private void LoadTemplateValues()
        {
            isFig = true;
            _isChanged = false;
            _hasChanged = false;
            _template.Database.LoadDatasets();
            foreach (var dataset in _template.Database.Datasets)
            {
                _datasets.Add(new ComboxItemInfo(dataset.Name, dataset.Name));
            }
            cmbDatasets.DataSource = _datasets;
            cmbDatasets.DisplayMember = "Display";
            cmbDatasets.ValueMember = "Value";
            txtName.Text = _template.Name;
            txtAliasName.Text = _template.AliasName;
            txtBaseName.Text = _template.BaseName;
            cmbFeatureType.SelectedValue = _template.FeatureTypeName;
            InitGeometryTypes();
            cmbDatasets.SelectedValue = _template.DatasetName;
            cmbGeometryTypes.SelectedValue = _template.GeometryTypeName;
            _fieldTable.Rows.Clear();
            foreach (YTField ytField in _template.Fields)
            {
                DataRow row = _fieldTable.NewRow();
                row["FieldName"] = ytField.Name;
                row["AliasName"] = ytField.AliasName;
                row["AllowNull"] = ytField.AllowNull;
                row["IsKey"] = ytField.IsKey;
                row["FieldType"] = ytField.FieldTypeName;
                row["FieldLength"] = ytField.Length;
                row["Precision"] = ytField.Precision;
                row["DomainName"] = ytField.DomainName;
                _fieldTable.Rows.Add(row);
            }
            _template.Database.LoadDomains();
            foreach (var domain in _template.Database.Domains)
            {
                _domains.Add(new ComboxItemInfo(domain.Name, domain.Name));
            }
            repositoryItemLookUpEdit2.DataSource = _domains;
            repositoryItemLookUpEdit2.ValueMember = "Value";
            repositoryItemLookUpEdit2.DisplayMember = "Display";
            isFig = false;
        }

        private void frmEditTemplate_Load(object sender, EventArgs e)
        {
            //generalPage.SetTemplate(_template);
            //generalPage.SetTemplate(_template);
        }



        private void cmbFeatureType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isFig) return;
            InitGeometryTypes();
            FireChanged();
        }

        private void InitGeometryTypes()
        {
            if (cmbFeatureType.SelectedIndex < 0)
            {
                cmbGeometryTypes.DataSource = null;
                return;
            }
            string parentKey = cmbFeatureType.SelectedValue.ToString();
            List<ComboxItemInfo> _subGeometryTypes = (from geomType in _geometryTypes
                where geomType.Parent == parentKey
                select geomType).ToList();

            cmbGeometryTypes.DataSource = _subGeometryTypes;
            cmbGeometryTypes.DisplayMember = "Display";
            cmbGeometryTypes.ValueMember = "Value";
        }

        private void txtName_EditValueChanged(object sender, EventArgs e)
        {
            if (isFig) return;
            FireChanged();
        }

        private void txtBaseName_EditValueChanged(object sender, EventArgs e)
        {
            if (isFig) return;
            FireChanged();
        }

        private void txtAliasName_EditValueChanged(object sender, EventArgs e)
        {
            if (isFig) return;
            FireChanged();
        }

        private void cmbGeometryTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isFig) return;
            FireChanged();
        }

        private void cmbDatasets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isFig) return;
            FireChanged();
        }

        private void gridView1_CellValueChanged(object sender,
            DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (isFig) return;
            FireChanged();
        }

        private void FireChanged()
        {
            btnOK.Enabled = true;
            btnApply.Enabled = true;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            SaveTemplate();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (SaveTemplate())
                DialogResult = DialogResult.OK;
        }

        private bool SaveTemplate()
        {
            _tmpTemplate = new ObjectTemplate();
            _tmpTemplate.Name = txtName.EditValue==null?"": txtName.EditValue.ToString().Trim();
            _tmpTemplate.AliasName = txtAliasName.EditValue==null?"": txtAliasName.EditValue.ToString().Trim();
            _tmpTemplate.BaseName = txtBaseName.EditValue==null?"": txtBaseName.EditValue.ToString().Trim();
            _tmpTemplate.FeatureTypeName = cmbFeatureType.SelectedValue.ToString();
            _tmpTemplate.GeometryTypeName = cmbGeometryTypes.SelectedValue.ToString();
            _tmpTemplate.DatasetName = cmbDatasets.SelectedValue==null ? "":cmbDatasets.SelectedValue.ToString();
            string msg = "";
            for (int i = 0; i < _fieldTable.Rows.Count; i++)
            {
                DataRow row = _fieldTable.Rows[i];
                YTField pField = new YTField()
                {
                    Name = row["FieldName"].ToString(),
                    AliasName = row["AliasName"].ToString(),
                    FieldTypeName = row["FieldType"].ToString(),
                    AllowNull = (bool) row["AllowNull"],
                    IsKey = (bool) row["IsKey"],
                    Length = (int) row["FieldLength"],
                    Precision = (int) row["Precision"],
                    DomainName = row["DomainName"].ToString()
                };
                if (pField.IsValid(out msg) == false)
                {
                    MessageBox.Show(msg, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                _tmpTemplate.Fields.Add(pField);
            }


            int oid = _database.GetObjectID(_tmpTemplate.Name, enumTemplateObjectType.FeatureClass);
            if (_template.ID > 0)
            {
                if (_template.ID != oid)
                {
                    MessageBox.Show("已经有同名称的模板存在，请修改名称!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else
            {
                if (oid > 0)
                {
                    MessageBox.Show("已经有同名称的模板存在，请修改名称!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            try
            {
                _tmpTemplate.ID = _template.ID;
                _tmpTemplate.Database = _template.Database;
                _template.Database.SaveTemplate(_tmpTemplate);
                _template = _tmpTemplate;

            }
            catch (Exception ex)
            {

                MessageBox.Show("程序发生错误"+ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }



            return true;
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            btnImportFC.Visible = e.Page.TabIndex == 1;
        }

        private void btnImportFC_Click(object sender, EventArgs e)
        {
            frmOpenFile frm=new frmOpenFile();
            frm.AddFilter(new MyGxFilterFeatureClasses(), true);
            if (frm.DoModalOpen() != DialogResult.OK) return;
            IGxDataset gxObject = frm.SelectedItems[0] as IGxDataset;
            if (gxObject == null) return;
            DialogResult result = MessageBox.Show("导入字段时是否清空已有字段?", "询问", MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);
            if (result == DialogResult.Cancel) return;
            if (result == DialogResult.Yes)
            {
                _fieldTable.Rows.Clear();
            }
            IFeatureClass pClass = gxObject.Dataset as IFeatureClass;
            for (int i = 0; i < pClass.Fields.FieldCount; i++)
            {
                IField pField = pClass.Fields.Field[i];
                if (pField.Type == esriFieldType.esriFieldTypeGeometry || pField.Type == esriFieldType.esriFieldTypeOID)
                    continue;
                string pFldName = pField.Name;
                if (_fieldTable.Select("FieldName='" + pFldName + "'").Length > 0) continue;
                DataRow pRow=_fieldTable.NewRow();
                pRow["FieldName"] = pField.Name;
                pRow["AliasName"] = pField.AliasName;
                pRow["AllowNull"] = pField.IsNullable;
                pRow["FieldType"] =FieldHelper.ConvertToSimpleString(pField.Type);
                //pRow["IsKey"] = pField.DefaultValue==null ? "": pField.DefaultValue;
                pRow["FieldLength"] = pField.Length;
                pRow["Precision"] = pField.Precision;
                _fieldTable.Rows.Add(pRow);
            }
        }

        private void btnDeleteField_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除该字段吗？", "删除提示", MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
            {
                int[] selectRows = gridView1.GetSelectedRows();
               gridView1.DeleteSelectedRows();
            }
        }
    }
}
