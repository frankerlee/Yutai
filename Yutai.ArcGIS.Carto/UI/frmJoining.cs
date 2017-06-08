using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Catalog;
using Yutai.Catalog.UI;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class frmJoining : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnJoiningType;
        private SimpleButton btnOK;
        private SimpleButton btnOpenTable;
        private ComboBoxEdit cboJoiningDataType;
        private ComboBoxEdit cboJoiningField;
        private ComboBoxEdit cboJoiningTable;
        private ComboBoxEdit cboJoiningTableField;
        private Container container_0 = null;
        private esriJoinType esriJoinType_0 = esriJoinType.esriLeftOuterJoin;
        private GroupBox groupBox1;
        private IBasicMap ibasicMap_0 = null;
        private ITable itable_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;

        public frmJoining()
        {
            this.InitializeComponent();
        }

        private void btnJoiningType_Click(object sender, EventArgs e)
        {
            frmJoinTypeSet set = new frmJoinTypeSet {
                JoinType = this.esriJoinType_0
            };
            if (set.ShowDialog() == DialogResult.OK)
            {
                this.esriJoinType_0 = set.JoinType;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (((this.cboJoiningField.SelectedIndex != -1) && (this.cboJoiningTable.SelectedIndex != -1)) && (this.cboJoiningTableField.SelectedIndex != -1))
            {
                ITable featureClass = (this.cboJoiningTable.SelectedItem as ObjectWrap).Object as ITable;
                if (featureClass is IFeatureLayer)
                {
                    featureClass = (featureClass as IFeatureLayer).FeatureClass as ITable;
                }
                JoiningRelatingHelper.JoinTableLayer(this.itable_0, this.cboJoiningField.Text, featureClass, this.cboJoiningTableField.Text);
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
                    this.cboJoiningTable.Properties.Items.Add(new ObjectWrap(dataset));
                }
            }
        }

        private void cboJoiningField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.cboJoiningField.SelectedIndex != -1) && (this.cboJoiningTable.SelectedIndex != -1))
            {
                this.method_2();
                if (this.cboJoiningTableField.Properties.Items.Count > 0)
                {
                    this.cboJoiningTableField.SelectedIndex = 0;
                }
            }
        }

        private void cboJoiningTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.cboJoiningTable.SelectedIndex != -1) && (this.cboJoiningField.SelectedIndex != -1))
            {
                this.method_2();
                if (this.cboJoiningTableField.Properties.Items.Count > 0)
                {
                    this.cboJoiningTableField.SelectedIndex = 0;
                }
            }
        }

        private void cboJoiningTableField_SelectedIndexChanged(object sender, EventArgs e)
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

        private void frmJoining_Load(object sender, EventArgs e)
        {
            this.method_1();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmJoining));
            this.label1 = new Label();
            this.groupBox1 = new GroupBox();
            this.cboJoiningTableField = new ComboBoxEdit();
            this.cboJoiningTable = new ComboBoxEdit();
            this.cboJoiningField = new ComboBoxEdit();
            this.btnJoiningType = new SimpleButton();
            this.btnOpenTable = new SimpleButton();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.cboJoiningDataType = new ComboBoxEdit();
            this.groupBox1.SuspendLayout();
            this.cboJoiningTableField.Properties.BeginInit();
            this.cboJoiningTable.Properties.BeginInit();
            this.cboJoiningField.Properties.BeginInit();
            this.cboJoiningDataType.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x7d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "连接到当前图层的数据";
            this.groupBox1.Controls.Add(this.cboJoiningTableField);
            this.groupBox1.Controls.Add(this.cboJoiningTable);
            this.groupBox1.Controls.Add(this.cboJoiningField);
            this.groupBox1.Controls.Add(this.btnJoiningType);
            this.groupBox1.Controls.Add(this.btnOpenTable);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new Point(8, 0x48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x110, 0xf8);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.cboJoiningTableField.EditValue = "";
            this.cboJoiningTableField.Location = new Point(8, 160);
            this.cboJoiningTableField.Name = "cboJoiningTableField";
            this.cboJoiningTableField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboJoiningTableField.Size = new Size(240, 0x15);
            this.cboJoiningTableField.TabIndex = 10;
            this.cboJoiningTableField.SelectedIndexChanged += new EventHandler(this.cboJoiningTableField_SelectedIndexChanged);
            this.cboJoiningTable.EditValue = "";
            this.cboJoiningTable.Location = new Point(8, 0x63);
            this.cboJoiningTable.Name = "cboJoiningTable";
            this.cboJoiningTable.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboJoiningTable.Size = new Size(200, 0x15);
            this.cboJoiningTable.TabIndex = 9;
            this.cboJoiningTable.SelectedIndexChanged += new EventHandler(this.cboJoiningTable_SelectedIndexChanged);
            this.cboJoiningField.EditValue = "";
            this.cboJoiningField.Location = new Point(8, 40);
            this.cboJoiningField.Name = "cboJoiningField";
            this.cboJoiningField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboJoiningField.Size = new Size(240, 0x15);
            this.cboJoiningField.TabIndex = 8;
            this.cboJoiningField.SelectedIndexChanged += new EventHandler(this.cboJoiningField_SelectedIndexChanged);
            this.btnJoiningType.Location = new Point(0xb0, 0xd0);
            this.btnJoiningType.Name = "btnJoiningType";
            this.btnJoiningType.Size = new Size(0x38, 0x18);
            this.btnJoiningType.TabIndex = 7;
            this.btnJoiningType.Text = "高级";
            this.btnJoiningType.Click += new EventHandler(this.btnJoiningType_Click);
            this.btnOpenTable.Image = (System.Drawing.Image)resources.GetObject("btnOpenTable.Image");
            this.btnOpenTable.Location = new Point(0xd8, 0x63);
            this.btnOpenTable.Name = "btnOpenTable";
            this.btnOpenTable.Size = new Size(0x18, 0x18);
            this.btnOpenTable.TabIndex = 6;
            this.btnOpenTable.Click += new EventHandler(this.btnOpenTable_Click);
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
            this.btnOK.DialogResult = DialogResult.OK;
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
            this.cboJoiningDataType.EditValue = "从表中连接数据";
            this.cboJoiningDataType.Location = new Point(8, 0x20);
            this.cboJoiningDataType.Name = "cboJoiningDataType";
            this.cboJoiningDataType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboJoiningDataType.Properties.Items.AddRange(new object[] { "从表中连接数据" });
            this.cboJoiningDataType.Size = new Size(0xe0, 0x15);
            this.cboJoiningDataType.TabIndex = 9;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x12a, 0x16c);
            base.Controls.Add(this.cboJoiningDataType);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = ((System.Drawing.Icon)resources.GetObject("$Icon"));
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmJoining";
            this.Text = "连接数据";
            base.Load += new EventHandler(this.frmJoining_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.cboJoiningTableField.Properties.EndInit();
            this.cboJoiningTable.Properties.EndInit();
            this.cboJoiningField.Properties.EndInit();
            this.cboJoiningDataType.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
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
                        if (!JoiningRelatingHelper.TableIsJoinLayer(this.itable_0, attributeTable))
                        {
                            this.cboJoiningTable.Properties.Items.Add(new ObjectWrap(layer));
                        }
                    }
                    else if (layer is IGroupLayer)
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
                                this.cboJoiningField.Properties.Items.Add(field.Name);
                                break;
                        }
                    }
                }
                if (this.cboJoiningField.Properties.Items.Count > 0)
                {
                    this.cboJoiningField.SelectedIndex = 0;
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
                                if (!JoiningRelatingHelper.TableIsJoinLayer(this.itable_0, attributeTable))
                                {
                                    this.cboJoiningTable.Properties.Items.Add(new ObjectWrap(layer));
                                }
                            }
                            else if (layer is IGroupLayer)
                            {
                                this.method_0(layer as ICompositeLayer);
                            }
                        }
                    }
                    IStandaloneTableCollection tables = this.ibasicMap_0 as IStandaloneTableCollection;
                    for (num = 0; num < tables.StandaloneTableCount; num++)
                    {
                        attributeTable = tables.get_StandaloneTable(num) as ITable;
                        if (!JoiningRelatingHelper.TableIsJoinLayer(this.itable_0, attributeTable))
                        {
                            this.cboJoiningTable.Properties.Items.Add(new ObjectWrap(attributeTable));
                        }
                    }
                    if (this.cboJoiningTable.Properties.Items.Count > 0)
                    {
                        this.cboJoiningTable.SelectedIndex = 0;
                    }
                }
            }
        }

        private void method_2()
        {
            this.cboJoiningTableField.Properties.Items.Clear();
            this.cboJoiningTableField.Text = "";
            ITable table = this.itable_0;
            int index = table.FindField(this.cboJoiningField.Text);
            if (index != -1)
            {
                IField field = table.Fields.get_Field(index);
                object obj2 = (this.cboJoiningTable.SelectedItem as ObjectWrap).Object;
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
                                this.cboJoiningTableField.Properties.Items.Add(field2.Name);
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
                                this.cboJoiningTableField.Properties.Items.Add(field2.Name);
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

