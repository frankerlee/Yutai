namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Editors;
    using JLK.Editors.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class NewRelationClass_SetKey : UserControl
    {
        private ComboBoxEdit cbodestForeignKey1;
        private ComboBoxEdit cboDestion;
        private ComboBoxEdit cboOrigin;
        private ComboBoxEdit cboOriginPrimaryKey1;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Panel panel1;
        private Panel panel2;
        private TextEdit txtdestForeignKey;
        private TextEdit txtOriginPrimaryKey;

        public NewRelationClass_SetKey()
        {
            this.InitializeComponent();
        }

        private void cbodestForeignKey1_SelectedIndexChanged(object sender, EventArgs e)
        {
            NewRelationClassHelper.OriginForeignKey = this.cbodestForeignKey1.Text;
        }

        private void cboDestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            NewRelationClassHelper.destPrimaryKey = this.cboDestion.Text;
        }

        private void cboOrigin_SelectedIndexChanged(object sender, EventArgs e)
        {
            NewRelationClassHelper.OriginPrimaryKey = this.cboOrigin.Text;
            this.cboDestion.Properties.Items.Clear();
            IFields fields = NewRelationClassHelper.DestinationClass.Fields;
            int index = NewRelationClassHelper.OriginClass.Fields.FindField(this.cboOrigin.Text);
            if (index != -1)
            {
                esriFieldType type = NewRelationClassHelper.OriginClass.Fields.get_Field(index).Type;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                    if (((field.Type != esriFieldType.esriFieldTypeBlob) || (field.Type != esriFieldType.esriFieldTypeGeometry)) && (type == field.Type))
                    {
                        this.cboDestion.Properties.Items.Add(field.Name);
                    }
                }
            }
        }

        private void cboOriginPrimaryKey1_SelectedIndexChanged(object sender, EventArgs e)
        {
            NewRelationClassHelper.OriginPrimaryKey = this.cboOriginPrimaryKey1.Text;
            this.cbodestForeignKey1.Properties.Items.Clear();
            IFields fields = NewRelationClassHelper.DestinationClass.Fields;
            int index = NewRelationClassHelper.OriginClass.Fields.FindField(this.cboOriginPrimaryKey1.Text);
            if (index != -1)
            {
                esriFieldType type = NewRelationClassHelper.OriginClass.Fields.get_Field(index).Type;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                    if (((field.Type != esriFieldType.esriFieldTypeBlob) || (field.Type != esriFieldType.esriFieldTypeGeometry)) && (type == field.Type))
                    {
                        this.cbodestForeignKey1.Properties.Items.Add(field.Name);
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

        private void InitializeComponent()
        {
            this.panel1 = new Panel();
            this.groupBox2 = new GroupBox();
            this.txtdestForeignKey = new TextEdit();
            this.label4 = new Label();
            this.label2 = new Label();
            this.groupBox1 = new GroupBox();
            this.txtOriginPrimaryKey = new TextEdit();
            this.label3 = new Label();
            this.label1 = new Label();
            this.panel2 = new Panel();
            this.label6 = new Label();
            this.label5 = new Label();
            this.cboOriginPrimaryKey1 = new ComboBoxEdit();
            this.cbodestForeignKey1 = new ComboBoxEdit();
            this.cboDestion = new ComboBoxEdit();
            this.cboOrigin = new ComboBoxEdit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.txtdestForeignKey.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.txtOriginPrimaryKey.Properties.BeginInit();
            this.panel2.SuspendLayout();
            this.cboOriginPrimaryKey1.Properties.BeginInit();
            this.cbodestForeignKey1.Properties.BeginInit();
            this.cboDestion.Properties.BeginInit();
            this.cboOrigin.Properties.BeginInit();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new Point(0x10, 0x10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(440, 0xd0);
            this.panel1.TabIndex = 0;
            this.groupBox2.Controls.Add(this.cboDestion);
            this.groupBox2.Controls.Add(this.txtdestForeignKey);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new Point(0xe8, 0x10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(200, 0xb8);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "目标表/要素类";
            this.txtdestForeignKey.EditValue = "";
            this.txtdestForeignKey.Location = new Point(0x10, 0x88);
            this.txtdestForeignKey.Name = "txtdestForeignKey";
            this.txtdestForeignKey.Size = new Size(160, 0x17);
            this.txtdestForeignKey.TabIndex = 4;
            this.txtdestForeignKey.EditValueChanged += new EventHandler(this.txtdestForeignKey_EditValueChanged);
            this.label4.Location = new Point(8, 0x58);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0xb8, 0x30);
            this.label4.TabIndex = 2;
            this.label4.Text = "在关系表中指定外键字段名称，以引用到源表/要素类中的主关键字字段";
            this.label2.Location = new Point(8, 0x18);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0xb0, 0x10);
            this.label2.TabIndex = 1;
            this.label2.Text = "在目标表/要素类中选择主关键字字段";
            this.groupBox1.Controls.Add(this.cboOrigin);
            this.groupBox1.Controls.Add(this.txtOriginPrimaryKey);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(8, 0x10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xd8, 0xb8);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "源表/要素类";
            this.txtOriginPrimaryKey.EditValue = "";
            this.txtOriginPrimaryKey.Location = new Point(0x10, 0x88);
            this.txtOriginPrimaryKey.Name = "txtOriginPrimaryKey";
            this.txtOriginPrimaryKey.Size = new Size(160, 0x17);
            this.txtOriginPrimaryKey.TabIndex = 3;
            this.txtOriginPrimaryKey.EditValueChanged += new EventHandler(this.txtOriginPrimaryKey_EditValueChanged);
            this.label3.Location = new Point(8, 0x58);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0xb8, 0x30);
            this.label3.TabIndex = 1;
            this.label3.Text = "在关系表中指定外键字段名称，以引用到目标表/要素类中的主关键字字段";
            this.label1.Location = new Point(8, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xb0, 0x10);
            this.label1.TabIndex = 0;
            this.label1.Text = "在源表/要素类中选择主关键字";
            this.panel2.Controls.Add(this.cbodestForeignKey1);
            this.panel2.Controls.Add(this.cboOriginPrimaryKey1);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new Point(0x10, 0x10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0xe0, 0xd0);
            this.panel2.TabIndex = 1;
            this.label6.Location = new Point(0x20, 0x48);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0xb8, 0x30);
            this.label6.TabIndex = 5;
            this.label6.Text = "在目标表/要素类中指定外键字段名称，以引用到源表/要素类中的主关键字字段";
            this.label5.Location = new Point(0x18, 8);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0xb0, 0x10);
            this.label5.TabIndex = 3;
            this.label5.Text = "在源表/要素类中选择主关键字";
            this.cboOriginPrimaryKey1.EditValue = "";
            this.cboOriginPrimaryKey1.Location = new Point(0x20, 0x20);
            this.cboOriginPrimaryKey1.Name = "cboOriginPrimaryKey1";
            this.cboOriginPrimaryKey1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboOriginPrimaryKey1.Size = new Size(160, 0x17);
            this.cboOriginPrimaryKey1.TabIndex = 7;
            this.cboOriginPrimaryKey1.SelectedIndexChanged += new EventHandler(this.cboOriginPrimaryKey1_SelectedIndexChanged);
            this.cbodestForeignKey1.EditValue = "";
            this.cbodestForeignKey1.Location = new Point(40, 120);
            this.cbodestForeignKey1.Name = "cbodestForeignKey1";
            this.cbodestForeignKey1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cbodestForeignKey1.Size = new Size(0x98, 0x17);
            this.cbodestForeignKey1.TabIndex = 8;
            this.cbodestForeignKey1.SelectedIndexChanged += new EventHandler(this.cbodestForeignKey1_SelectedIndexChanged);
            this.cboDestion.EditValue = "";
            this.cboDestion.Location = new Point(0x10, 0x30);
            this.cboDestion.Name = "cboDestion";
            this.cboDestion.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboDestion.Size = new Size(160, 0x17);
            this.cboDestion.TabIndex = 5;
            this.cboDestion.SelectedIndexChanged += new EventHandler(this.cboDestion_SelectedIndexChanged);
            this.cboOrigin.EditValue = "";
            this.cboOrigin.Location = new Point(0x10, 40);
            this.cboOrigin.Name = "cboOrigin";
            this.cboOrigin.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboOrigin.Size = new Size(160, 0x17);
            this.cboOrigin.TabIndex = 4;
            this.cboOrigin.SelectedIndexChanged += new EventHandler(this.cboOrigin_SelectedIndexChanged);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Name = "NewRelationClass_SetKey";
            base.Size = new Size(0x1d8, 0x110);
            base.Load += new EventHandler(this.NewRelationClass_SetKey_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.txtdestForeignKey.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.txtOriginPrimaryKey.Properties.EndInit();
            this.panel2.ResumeLayout(false);
            this.cboOriginPrimaryKey1.Properties.EndInit();
            this.cbodestForeignKey1.Properties.EndInit();
            this.cboDestion.Properties.EndInit();
            this.cboOrigin.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void NewRelationClass_SetKey_Load(object sender, EventArgs e)
        {
            IFields fields;
            int num;
            IField field;
            if (NewRelationClassHelper.Cardinality == esriRelCardinality.esriRelCardinalityManyToMany)
            {
                this.panel1.Visible = true;
                this.panel2.Visible = false;
                this.cboOrigin.Properties.Items.Clear();
                fields = NewRelationClassHelper.OriginClass.Fields;
                for (num = 0; num < fields.FieldCount; num++)
                {
                    field = fields.get_Field(num);
                    if ((field.Type != esriFieldType.esriFieldTypeBlob) || (field.Type != esriFieldType.esriFieldTypeGeometry))
                    {
                        this.cboOrigin.Properties.Items.Add(field.Name);
                    }
                }
                if (this.cboOrigin.Properties.Items.Count > 0)
                {
                    this.cboOrigin.SelectedIndex = 0;
                }
            }
            else
            {
                this.panel1.Visible = false;
                this.panel2.Visible = true;
                this.cboOriginPrimaryKey1.Properties.Items.Clear();
                fields = NewRelationClassHelper.OriginClass.Fields;
                for (num = 0; num < fields.FieldCount; num++)
                {
                    field = fields.get_Field(num);
                    if ((field.Type != esriFieldType.esriFieldTypeBlob) || (field.Type != esriFieldType.esriFieldTypeGeometry))
                    {
                        this.cboOriginPrimaryKey1.Properties.Items.Add(field.Name);
                    }
                }
                if (this.cboOriginPrimaryKey1.Properties.Items.Count > 0)
                {
                    this.cboOriginPrimaryKey1.SelectedIndex = 0;
                }
            }
        }

        private void txtdestForeignKey_EditValueChanged(object sender, EventArgs e)
        {
            NewRelationClassHelper.destForeignKey = this.txtdestForeignKey.Text;
        }

        private void txtOriginPrimaryKey_EditValueChanged(object sender, EventArgs e)
        {
            NewRelationClassHelper.OriginForeignKey = this.txtOriginPrimaryKey.Text;
        }
    }
}

