namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Editors;
    using JLK.Utility.Wrap;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class frmNewJLKCodeDomain : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private System.Windows.Forms.ComboBox cboNameField;
        private System.Windows.Forms.ComboBox cboTable;
        private System.Windows.Forms.ComboBox cboValueField;
        private IContainer icontainer_0 = null;
        private IWorkspace iworkspace_0;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private string string_0 = "";
        private string string_1 = "";
        private string string_2 = "";
        private string string_3 = "";
        private string string_4 = "";
        private TextEdit txtCode;
        private TextEdit txtName;

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

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
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

        private void InitializeComponent()
        {
            this.txtName = new TextEdit();
            this.label2 = new Label();
            this.txtCode = new TextEdit();
            this.label1 = new Label();
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.label3 = new Label();
            this.cboNameField = new System.Windows.Forms.ComboBox();
            this.cboValueField = new System.Windows.Forms.ComboBox();
            this.cboTable = new System.Windows.Forms.ComboBox();
            this.label4 = new Label();
            this.label5 = new Label();
            this.txtName.Properties.BeginInit();
            this.txtCode.Properties.BeginInit();
            base.SuspendLayout();
            this.txtName.EditValue = "";
            this.txtName.Location = new Point(0x47, 0x20);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(0xa5, 0x15);
            this.txtName.TabIndex = 0x11;
            this.txtName.EditValueChanged += new EventHandler(this.txtName_EditValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 0x25);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x29, 12);
            this.label2.TabIndex = 0x10;
            this.label2.Text = "描  述";
            this.txtCode.EditValue = "";
            this.txtCode.Location = new Point(0x47, 4);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new Size(0xa5, 0x15);
            this.txtCode.TabIndex = 15;
            this.txtCode.EditValueChanged += new EventHandler(this.txtCode_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x29, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "代  码";
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0xa5, 0x93);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnOK.Location = new Point(0x5f, 0x93);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x40, 0x18);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(12, 0x44);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x29, 12);
            this.label3.TabIndex = 0x12;
            this.label3.Text = "代码表";
            this.cboNameField.FormattingEnabled = true;
            this.cboNameField.Location = new Point(0x47, 90);
            this.cboNameField.Name = "cboNameField";
            this.cboNameField.Size = new Size(0xa5, 20);
            this.cboNameField.TabIndex = 20;
            this.cboValueField.FormattingEnabled = true;
            this.cboValueField.Location = new Point(0x47, 0x79);
            this.cboValueField.Name = "cboValueField";
            this.cboValueField.Size = new Size(0xa5, 20);
            this.cboValueField.TabIndex = 0x15;
            this.cboTable.FormattingEnabled = true;
            this.cboTable.Location = new Point(0x47, 0x40);
            this.cboTable.Name = "cboTable";
            this.cboTable.Size = new Size(0xa5, 20);
            this.cboTable.TabIndex = 0x16;
            this.cboTable.SelectedIndexChanged += new EventHandler(this.cboTable_SelectedIndexChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(12, 0x5d);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x35, 12);
            this.label4.TabIndex = 0x17;
            this.label4.Text = "名称字段";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(12, 0x7c);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x29, 12);
            this.label5.TabIndex = 0x18;
            this.label5.Text = "值字段";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x101, 0xc3);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.cboTable);
            base.Controls.Add(this.cboValueField);
            base.Controls.Add(this.cboNameField);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txtCode);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Name = "frmNewJLKCodeDomain";
            this.Text = "扩展域值";
            base.Load += new EventHandler(this.frmNewJLKCodeDomain_Load);
            this.txtName.Properties.EndInit();
            this.txtCode.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
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

