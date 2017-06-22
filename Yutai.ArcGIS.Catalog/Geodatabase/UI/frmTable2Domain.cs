using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmTable2Domain : Form
    {
        private Container container_0 = null;
        private IWorkspaceDomains iworkspaceDomains_0 = null;

        public frmTable2Domain()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.iworkspaceDomains_0 == null)
            {
                base.Close();
            }
            if (this.txtDomainName.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入域名");
            }
            else if (this.txtTable.Tag == null)
            {
                MessageBox.Show("请选择要创建域值的表");
            }
            else if (this.cboNameField.SelectedIndex == -1)
            {
                MessageBox.Show("请选择要名字字段");
            }
            else if (this.cboNameField.SelectedIndex == -1)
            {
                MessageBox.Show("请选择值字段");
            }
            else if (this.iworkspaceDomains_0.get_DomainByName(this.txtDomainName.Text) != null)
            {
                MessageBox.Show("该域名的域值已存在，请输入其他名称");
            }
            else
            {
                try
                {
                    ICodedValueDomain domain2 = new CodedValueDomainClass();
                    IDomain domain = domain2 as IDomain;
                    domain.Description = this.txtDes.Text;
                    domain.Name = this.txtDomainName.Text;
                    this.method_0(this.cboMergePolicy.SelectedIndex, domain);
                    this.method_1(this.cboSplitPolicy.SelectedIndex, domain);
                    ITable tag = this.txtTable.Tag as ITable;
                    int index = tag.Fields.FindFieldByAliasName(this.cdoCodeField.Text);
                    IField field = tag.Fields.get_Field(index);
                    domain.FieldType = field.Type;
                    int num2 = tag.Fields.FindField(this.cboNameField.Text);
                    ICursor o = tag.Search(null, false);
                    for (IRow row = o.NextRow(); row != null; row = o.NextRow())
                    {
                        domain2.AddCode(row.get_Value(index), row.get_Value(num2).ToString());
                    }
                    this.iworkspaceDomains_0.AddDomain(domain);
                    ComReleaser.ReleaseCOMObject(o);
                    base.DialogResult = DialogResult.OK;
                }
                catch (Exception exception)
                {
                    Logger.Current.Error("",exception, "");
                }
                base.Close();
            }
        }

        private void btnSelectInputFeatures_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile {
                Text = "选择表"
            };
            file.AddFilter(new MyGxFilterTables(), true);
            if (file.DoModalOpen() == DialogResult.OK)
            {
                IArray items = file.Items;
                if (items.Count != 0)
                {
                    IGxObject obj2 = items.get_Element(0) as IGxObject;
                    ITable dataset = (obj2 as IGxDataset).Dataset as ITable;
                    this.txtTable.Tag = dataset;
                    if (dataset != null)
                    {
                        this.txtTable.Text = obj2.FullName;
                        string[] strArray = obj2.Name.Split(new char[] { '.' });
                        string str = strArray[strArray.Length - 1];
                        this.txtDomainName.Text = str;
                        IFields fields = dataset.Fields;
                        for (int i = 0; i < fields.FieldCount; i++)
                        {
                            IField field = fields.get_Field(i);
                            if ((((field.Type != esriFieldType.esriFieldTypeBlob) && (field.Type != esriFieldType.esriFieldTypeGeometry)) && (field.Type != esriFieldType.esriFieldTypeRaster)) && (field.Type != esriFieldType.esriFieldTypeOID))
                            {
                                this.cdoCodeField.Properties.Items.Add(field.AliasName);
                                if (field.Type == esriFieldType.esriFieldTypeString)
                                {
                                    this.cboNameField.Properties.Items.Add(field.AliasName);
                                }
                            }
                        }
                        if (this.cdoCodeField.Properties.Items.Count > 0)
                        {
                            this.cdoCodeField.SelectedIndex = 0;
                        }
                        if (this.cboNameField.Properties.Items.Count > 0)
                        {
                            this.cboNameField.SelectedIndex = 0;
                        }
                    }
                }
            }
        }

 private void frmTable2Domain_Load(object sender, EventArgs e)
        {
        }

 private void method_0(int int_0, IDomain idomain_0)
        {
            switch (int_0)
            {
                case 0:
                    idomain_0.MergePolicy = esriMergePolicyType.esriMPTDefaultValue;
                    break;

                case 1:
                    idomain_0.MergePolicy = esriMergePolicyType.esriMPTSumValues;
                    break;

                case 2:
                    idomain_0.MergePolicy = esriMergePolicyType.esriMPTAreaWeighted;
                    break;
            }
        }

        private void method_1(int int_0, IDomain idomain_0)
        {
            switch (int_0)
            {
                case 0:
                    idomain_0.SplitPolicy = esriSplitPolicyType.esriSPTDefaultValue;
                    break;

                case 1:
                    idomain_0.SplitPolicy = esriSplitPolicyType.esriSPTDuplicate;
                    break;

                case 2:
                    idomain_0.SplitPolicy = esriSplitPolicyType.esriSPTGeometryRatio;
                    break;
            }
        }

        public IWorkspace Workspace
        {
            set
            {
                this.iworkspaceDomains_0 = value as IWorkspaceDomains;
            }
        }
    }
}

