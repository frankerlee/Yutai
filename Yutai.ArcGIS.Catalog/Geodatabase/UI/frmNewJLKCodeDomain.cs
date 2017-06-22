using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class frmNewJLKCodeDomain : Form
    {
        private IContainer icontainer_0 = null;
        private string string_0 = "";
        private string string_1 = "";
        private string string_2 = "";
        private string string_3 = "";
        private string string_4 = "";

        public frmNewJLKCodeDomain()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtCode.Text.Trim().Length == 0)
            {
                MessageBox.Show("请域值名称");
            }
            if (this.cboTable.SelectedIndex == -1)
            {
                MessageBox.Show("请选择代码表");
            }
            if (this.cboNameField.SelectedIndex == -1)
            {
                MessageBox.Show("请选择名称字段");
            }
            if (this.cboValueField.SelectedIndex == -1)
            {
                MessageBox.Show("请选择域值字段");
            }
            this.string_3 = this.txtCode.Text.Trim();
            this.string_4 = this.txtName.Text;
            this.string_1 = this.cboNameField.Text;
            this.string_2 = this.cboValueField.Text;
            this.string_0 = this.cboTable.Text;
            base.DialogResult = DialogResult.OK;
        }

        private void cboTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cboNameField.Items.Clear();
            this.cboValueField.Items.Clear();
            if (this.cboTable.SelectedItem is ObjectWrap)
            {
                ObjectWrap selectedItem = this.cboTable.SelectedItem as ObjectWrap;
                ITable table = selectedItem.Object as ITable;
                IFields fields = table.Fields;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                    if ((((field.Editable && (field.Type != esriFieldType.esriFieldTypeOID)) && ((field.Type != esriFieldType.esriFieldTypeRaster) && (field.Type != esriFieldType.esriFieldTypeGeometry))) && (field.Type != esriFieldType.esriFieldTypeBlob)) && (field.Type != esriFieldType.esriFieldTypeXML))
                    {
                        this.cboNameField.Items.Add(field.Name);
                        this.cboValueField.Items.Add(field.Name);
                    }
                }
                if (this.cboNameField.Items.Count > 0)
                {
                    this.cboNameField.SelectedIndex = 0;
                    this.cboValueField.SelectedIndex = 0;
                }
            }
        }

 private void frmNewJLKCodeDomain_Load(object sender, EventArgs e)
        {
            IEnumDataset dataset = this.iworkspace_0.get_Datasets(esriDatasetType.esriDTTable);
            dataset.Reset();
            for (IDataset dataset2 = dataset.Next(); dataset2 != null; dataset2 = dataset.Next())
            {
                if (dataset2 is ITable)
                {
                    this.cboTable.Items.Add(new ObjectWrap(dataset2));
                }
            }
            if (this.cboTable.Items.Count > 0)
            {
                this.cboTable.SelectedIndex = 0;
            }
        }

 private void txtCode_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void txtName_EditValueChanged(object sender, EventArgs e)
        {
        }

        public string DomainDescription
        {
            get
            {
                return this.string_4;
            }
            set
            {
                this.string_4 = value;
            }
        }

        public string DomainName
        {
            get
            {
                return this.string_3;
            }
            set
            {
                this.string_3 = value;
            }
        }

        public string NameField
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

        public string TableName
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

        public string ValueField
        {
            get
            {
                return this.string_2;
            }
            set
            {
                this.string_2 = value;
            }
        }

        public IWorkspace Workspace
        {
            set
            {
                this.iworkspace_0 = value;
            }
        }
    }
}

