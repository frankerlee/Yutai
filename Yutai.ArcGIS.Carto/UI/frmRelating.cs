using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;


namespace Yutai.ArcGIS.Carto.UI
{
    internal class frmRelating : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private SimpleButton btnOpenTable;
        private ComboBoxEdit cboRelatingField;
        private ComboBoxEdit cboRelatingTable;
        private ComboBoxEdit cboRelatingTableField;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private IBasicMap ibasicMap_0 = null;
        private ITable itable_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextEdit txtName;

        public frmRelating()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (((this.cboRelatingField.SelectedIndex != -1) && (this.cboRelatingTable.SelectedIndex != -1)) && (this.cboRelatingTableField.SelectedIndex != -1))
            {
                if (this.txtName.Text.Trim().Length == 0)
                {
                    MessageBox.Show("请输入关联名!");
                }
                else
                {
                    ITable featureClass = (this.cboRelatingTable.SelectedItem as ObjectWrap).Object as ITable;
                    if (featureClass is IFeatureLayer)
                    {
                        featureClass = (featureClass as IFeatureLayer).FeatureClass as ITable;
                    }
                    JoiningRelatingHelper.RelateTableLayer(this.txtName.Text.Trim(), this.itable_0, this.cboRelatingField.Text, featureClass, this.cboRelatingTableField.Text);
                    base.DialogResult = DialogResult.OK;
                    base.Close();
                }
            }
        }

        private void btnOpenTable_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile();
            file.AddFilter(new MyGxFilterDatasets(), true);
            if (file.ShowDialog() == DialogResult.OK)
            {
                ITable dataset = (file.Items.get_Element(0) as IGxDataset).Dataset as ITable;
                if (dataset != null)
                {
                    this.cboRelatingTable.Properties.Items.Add(new ObjectWrap(dataset));
                }
            }
        }

        private void cboRelatingField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.cboRelatingField.SelectedIndex != -1) && (this.cboRelatingTable.SelectedIndex != -1))
            {
                this.method_2();
                if (this.cboRelatingTableField.Properties.Items.Count > 0)
                {
                    this.cboRelatingTableField.SelectedIndex = 0;
                }
            }
        }

        private void cboRelatingTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.cboRelatingTable.SelectedIndex != -1) && (this.cboRelatingField.SelectedIndex != -1))
            {
                this.method_2();
                if (this.cboRelatingTableField.Properties.Items.Count > 0)
                {
                    this.cboRelatingTableField.SelectedIndex = 0;
                }
            }
        }

        private void cboRelatingTableField_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmRelating_Load(object sender, EventArgs e)
        {
            this.method_1();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRelating));
            this.groupBox1 = new GroupBox();
            this.txtName = new TextEdit();
            this.label1 = new Label();
            this.btnOpenTable = new SimpleButton();
            this.cboRelatingTableField = new ComboBoxEdit();
            this.cboRelatingTable = new ComboBoxEdit();
            this.cboRelatingField = new ComboBoxEdit();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.groupBox1.SuspendLayout();
            this.txtName.Properties.BeginInit();
            this.cboRelatingTableField.Properties.BeginInit();
            this.cboRelatingTable.Properties.BeginInit();
            this.cboRelatingField.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnOpenTable);
            this.groupBox1.Controls.Add(this.cboRelatingTableField);
            this.groupBox1.Controls.Add(this.cboRelatingTable);
            this.groupBox1.Controls.Add(this.cboRelatingField);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x110, 0x138);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.txtName.EditValue = "";
            this.txtName.Location = new Point(8, 0xe0);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(0xe8, 0x15);
            this.txtName.TabIndex = 8;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 200);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x5f, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "4、连接关联名字";
            this.btnOpenTable.Image = (System.Drawing.Image)resources.GetObject("btnOpenTable.Image");
            this.btnOpenTable.Location = new Point(0xd8, 0x63);
            this.btnOpenTable.Name = "btnOpenTable";
            this.btnOpenTable.Size = new Size(0x18, 0x18);
            this.btnOpenTable.TabIndex = 6;
            this.btnOpenTable.Click += new EventHandler(this.btnOpenTable_Click);
            this.cboRelatingTableField.EditValue = "";
            this.cboRelatingTableField.Location = new Point(8, 0xa5);
            this.cboRelatingTableField.Name = "cboRelatingTableField";
            this.cboRelatingTableField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboRelatingTableField.Size = new Size(0xe8, 0x15);
            this.cboRelatingTableField.TabIndex = 5;
            this.cboRelatingTableField.SelectedIndexChanged += new EventHandler(this.cboRelatingTableField_SelectedIndexChanged);
            this.cboRelatingTable.EditValue = "";
            this.cboRelatingTable.Location = new Point(8, 0x63);
            this.cboRelatingTable.Name = "cboRelatingTable";
            this.cboRelatingTable.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboRelatingTable.Size = new Size(200, 0x15);
            this.cboRelatingTable.TabIndex = 4;
            this.cboRelatingTable.SelectedIndexChanged += new EventHandler(this.cboRelatingTable_SelectedIndexChanged);
            this.cboRelatingField.EditValue = "";
            this.cboRelatingField.Location = new Point(8, 0x2a);
            this.cboRelatingField.Name = "cboRelatingField";
            this.cboRelatingField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboRelatingField.Size = new Size(240, 0x15);
            this.cboRelatingField.TabIndex = 3;
            this.cboRelatingField.SelectedIndexChanged += new EventHandler(this.cboRelatingField_SelectedIndexChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 0x88);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x9b, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "3、选择表中需要连接的字段";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 0x4a);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0xfb, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "2、要连接到当前图层中的表，或从硬盘打开表";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x12);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0xb3, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "1、当前图层中要进行连接的字段";
            this.btnOK.Location = new Point(0x88, 0x148);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0xd0, 0x148);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x38, 0x18);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x12a, 0x16c);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = ((System.Drawing.Icon)resources.GetObject("$Icon"));
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmRelating";
            this.Text = "连接数据";
            base.Load += new EventHandler(this.frmRelating_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.txtName.Properties.EndInit();
            this.cboRelatingTableField.Properties.EndInit();
            this.cboRelatingTable.Properties.EndInit();
            this.cboRelatingField.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void method_0(ICompositeLayer icompositeLayer_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.get_Layer(i);
                if (layer != this.itable_0)
                {
                    if (layer is IAttributeTable)
                    {
                        ITable attributeTable = (layer as IAttributeTable).AttributeTable;
                        this.cboRelatingTable.Properties.Items.Add(new ObjectWrap(layer));
                    }
                    else if (layer is ICompositeLayer)
                    {
                        this.method_0(layer as ICompositeLayer);
                    }
                }
            }
        }

        private void method_1()
        {
            if (this.itable_0 != null)
            {
                int num;
                IFields fields = this.itable_0.Fields;
                if (fields != null)
                {
                    for (num = 0; num < fields.FieldCount; num++)
                    {
                        IField field = fields.get_Field(num);
                        switch (field.Type)
                        {
                            case esriFieldType.esriFieldTypeDouble:
                            case esriFieldType.esriFieldTypeInteger:
                            case esriFieldType.esriFieldTypeOID:
                            case esriFieldType.esriFieldTypeSingle:
                            case esriFieldType.esriFieldTypeSmallInteger:
                            case esriFieldType.esriFieldTypeString:
                                this.cboRelatingField.Properties.Items.Add(field.Name);
                                break;
                        }
                    }
                }
                if (this.cboRelatingField.Properties.Items.Count > 0)
                {
                    this.cboRelatingField.SelectedIndex = 0;
                }
                if (this.ibasicMap_0 != null)
                {
                    ITable attributeTable;
                    ILayer layer = null;
                    for (num = 0; num < this.ibasicMap_0.LayerCount; num++)
                    {
                        layer = this.ibasicMap_0.get_Layer(num);
                        if (layer != this.itable_0)
                        {
                            if (layer is IAttributeTable)
                            {
                                attributeTable = (layer as IAttributeTable).AttributeTable;
                                this.cboRelatingTable.Properties.Items.Add(new ObjectWrap(layer));
                            }
                            else if (layer is ICompositeLayer)
                            {
                                this.method_0(layer as ICompositeLayer);
                            }
                        }
                    }
                    ITableCollection tables = this.ibasicMap_0 as ITableCollection;
                    for (num = 0; num < tables.TableCount; num++)
                    {
                        attributeTable = tables.get_Table(num);
                        this.cboRelatingTable.Properties.Items.Add(new ObjectWrap(attributeTable));
                    }
                    if (this.cboRelatingTable.Properties.Items.Count > 0)
                    {
                        this.cboRelatingTable.SelectedIndex = 0;
                    }
                }
            }
        }

        private void method_2()
        {
            this.cboRelatingTableField.Properties.Items.Clear();
            this.cboRelatingTableField.Text = "";
            ITable table = this.itable_0;
            int index = table.FindField(this.cboRelatingField.Text);
            if (index != -1)
            {
                IField field = table.Fields.get_Field(index);
                object obj2 = (this.cboRelatingTable.SelectedItem as ObjectWrap).Object;
                ITable attributeTable = null;
                if (obj2 is IAttributeTable)
                {
                    attributeTable = (obj2 as IAttributeTable).AttributeTable;
                }
                else if (obj2 is ITable)
                {
                    attributeTable = obj2 as ITable;
                }
                if (attributeTable != null)
                {
                    int num2;
                    IField field2;
                    if (field.Type == esriFieldType.esriFieldTypeString)
                    {
                        for (num2 = 0; num2 < attributeTable.Fields.FieldCount; num2++)
                        {
                            field2 = attributeTable.Fields.get_Field(num2);
                            if (field2.Type == esriFieldType.esriFieldTypeString)
                            {
                                this.cboRelatingTableField.Properties.Items.Add(field2.Name);
                            }
                        }
                    }
                    else
                    {
                        for (num2 = 0; num2 < attributeTable.Fields.FieldCount; num2++)
                        {
                            field2 = attributeTable.Fields.get_Field(num2);
                            if ((((field2.Type == esriFieldType.esriFieldTypeDouble) || (field2.Type == esriFieldType.esriFieldTypeInteger)) || ((field2.Type == esriFieldType.esriFieldTypeOID) || (field2.Type == esriFieldType.esriFieldTypeSingle))) || (field2.Type == esriFieldType.esriFieldTypeSmallInteger))
                            {
                                this.cboRelatingTableField.Properties.Items.Add(field2.Name);
                            }
                        }
                    }
                }
            }
        }

        public ITable CurrentSelectItem
        {
            set
            {
                this.itable_0 = value;
            }
        }

        public IBasicMap FocusMap
        {
            set
            {
                this.ibasicMap_0 = value;
            }
        }

        internal class ObjectWrap
        {
            private object object_0 = null;

            public ObjectWrap(object object_1)
            {
                this.object_0 = object_1;
            }

            public override string ToString()
            {
                if (this.object_0 is ILayer)
                {
                    return (this.object_0 as ILayer).Name;
                }
                if (this.object_0 is IDataset)
                {
                    return (this.object_0 as IDataset).BrowseName;
                }
                return "";
            }

            public object Object
            {
                get
                {
                    return this.object_0;
                }
                set
                {
                    this.object_0 = null;
                }
            }
        }
    }
}

