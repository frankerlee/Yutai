namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.ADF;
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Catalog;
    using JLK.Catalog.UI;
    using JLK.Editors;
    using JLK.Editors.Controls;
    using JLK.Utility;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmTable2Domain : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private SimpleButton btnSelectInputFeatures;
        private ComboBoxEdit cboMergePolicy;
        private ComboBoxEdit cboNameField;
        private ComboBoxEdit cboSplitPolicy;
        private ComboBoxEdit cdoCodeField;
        private Container container_0 = null;
        private IWorkspaceDomains iworkspaceDomains_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private MemoEdit txtDes;
        private TextEdit txtDomainName;
        private TextEdit txtTable;

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
                    CErrorLog.writeErrorLog(this, exception, "");
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

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmTable2Domain_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmTable2Domain));
            this.label1 = new Label();
            this.txtDomainName = new TextEdit();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.cboNameField = new ComboBoxEdit();
            this.cdoCodeField = new ComboBoxEdit();
            this.cboMergePolicy = new ComboBoxEdit();
            this.cboSplitPolicy = new ComboBoxEdit();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.label6 = new Label();
            this.txtTable = new TextEdit();
            this.btnSelectInputFeatures = new SimpleButton();
            this.label7 = new Label();
            this.txtDes = new MemoEdit();
            this.txtDomainName.Properties.BeginInit();
            this.cboNameField.Properties.BeginInit();
            this.cdoCodeField.Properties.BeginInit();
            this.cboMergePolicy.Properties.BeginInit();
            this.cboSplitPolicy.Properties.BeginInit();
            this.txtTable.Properties.BeginInit();
            this.txtDes.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x2a);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x3b, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "域值名称:";
            this.txtDomainName.EditValue = "";
            this.txtDomainName.Location = new Point(0x48, 40);
            this.txtDomainName.Name = "txtDomainName";
            this.txtDomainName.Size = new Size(0x110, 0x15);
            this.txtDomainName.TabIndex = 1;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 160);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x3b, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "名称字段:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0xb8, 0x9e);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x3b, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "代码字段:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 0xc0);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x3b, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "合并策略:";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0xb8, 0xb9);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x3b, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "拆分策略:";
            this.cboNameField.EditValue = "";
            this.cboNameField.Location = new Point(0x48, 0x98);
            this.cboNameField.Name = "cboNameField";
            this.cboNameField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboNameField.Size = new Size(0x68, 0x15);
            this.cboNameField.TabIndex = 6;
            this.cdoCodeField.EditValue = "";
            this.cdoCodeField.Location = new Point(0xf8, 0x98);
            this.cdoCodeField.Name = "cdoCodeField";
            this.cdoCodeField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cdoCodeField.Size = new Size(0x68, 0x15);
            this.cdoCodeField.TabIndex = 7;
            this.cboMergePolicy.EditValue = "默认值";
            this.cboMergePolicy.Location = new Point(0x48, 0xb8);
            this.cboMergePolicy.Name = "cboMergePolicy";
            this.cboMergePolicy.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboMergePolicy.Properties.Items.AddRange(new object[] { "默认值", "总和值", "加权平均" });
            this.cboMergePolicy.Size = new Size(0x68, 0x15);
            this.cboMergePolicy.TabIndex = 8;
            this.cboSplitPolicy.EditValue = "默认值";
            this.cboSplitPolicy.Location = new Point(0xf8, 0xb8);
            this.cboSplitPolicy.Name = "cboSplitPolicy";
            this.cboSplitPolicy.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboSplitPolicy.Properties.Items.AddRange(new object[] { "默认值", "复制", "几何比例" });
            this.cboSplitPolicy.Size = new Size(0x68, 0x15);
            this.cboSplitPolicy.TabIndex = 9;
            this.btnOK.Location = new Point(0xd0, 0xe0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x40, 0x18);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x120, 0xe0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(8, 10);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x2f, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "选择表:";
            this.txtTable.EditValue = "";
            this.txtTable.Location = new Point(0x48, 8);
            this.txtTable.Name = "txtTable";
            this.txtTable.Properties.ReadOnly = true;
            this.txtTable.Size = new Size(0x110, 0x15);
            this.txtTable.TabIndex = 13;
            this.btnSelectInputFeatures.Image = (Image) manager.GetObject("btnSelectInputFeatures.Image");
            this.btnSelectInputFeatures.Location = new Point(0x160, 8);
            this.btnSelectInputFeatures.Name = "btnSelectInputFeatures";
            this.btnSelectInputFeatures.Size = new Size(0x18, 0x18);
            this.btnSelectInputFeatures.TabIndex = 14;
            this.btnSelectInputFeatures.Click += new EventHandler(this.btnSelectInputFeatures_Click);
            this.label7.AutoSize = true;
            this.label7.Location = new Point(8, 0x48);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x3b, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "域值说明:";
            this.txtDes.EditValue = "";
            this.txtDes.Location = new Point(0x48, 0x48);
            this.txtDes.Name = "txtDes";
            this.txtDes.Size = new Size(0x110, 0x48);
            this.txtDes.TabIndex = 0x10;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x182, 0x10f);
            base.Controls.Add(this.txtDes);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.btnSelectInputFeatures);
            base.Controls.Add(this.txtTable);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.cboSplitPolicy);
            base.Controls.Add(this.cboMergePolicy);
            base.Controls.Add(this.cdoCodeField);
            base.Controls.Add(this.cboNameField);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txtDomainName);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmTable2Domain";
            this.Text = "创建代码用域值";
            base.Load += new EventHandler(this.frmTable2Domain_Load);
            this.txtDomainName.Properties.EndInit();
            this.cboNameField.Properties.EndInit();
            this.cdoCodeField.Properties.EndInit();
            this.cboMergePolicy.Properties.EndInit();
            this.cboSplitPolicy.Properties.EndInit();
            this.txtTable.Properties.EndInit();
            this.txtDes.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
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

