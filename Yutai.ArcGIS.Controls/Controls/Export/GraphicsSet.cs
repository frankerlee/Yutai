using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    internal class GraphicsSet : UserControl
    {
        private SimpleButton btnDelete;
        private SimpleButton btnSelectField;
        private ComboBoxEdit cboFields;
        private ComboBoxEdit cboHorField;
        private LVColumnHeader columnHeader1;
        private LVColumnHeader columnHeader2;
        private Container components = null;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label5;
        private EditListView listView1;
        private TextEdit txtTitle;

        public GraphicsSet()
        {
            this.InitializeComponent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = this.listView1.SelectedIndices.Count - 1; i >= 0; i--)
            {
                this.listView1.Items.RemoveAt(this.listView1.SelectedIndices[i]);
            }
        }

        private void btnSelectField_Click(object sender, EventArgs e)
        {
            if (GraphicHelper.pGraphicHelper.DataSource != null)
            {
                IFields fields;
                if (GraphicHelper.pGraphicHelper.DataSource is ISelectionSet)
                {
                    fields = (GraphicHelper.pGraphicHelper.DataSource as ISelectionSet).Target.Fields;
                }
                else
                {
                    fields = (GraphicHelper.pGraphicHelper.DataSource as ITable).Fields;
                }
                int index = fields.FindFieldByAliasName(this.cboFields.Text);
                IField field = fields.get_Field(index);
                ListViewItem item = new ListViewItem(new string[] { field.Name, field.AliasName });
                this.listView1.Items.Add(item);
            }
        }

        private void cboFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboFields.SelectedIndex != -1)
            {
                this.btnSelectField.Enabled = true;
            }
            else
            {
                this.btnSelectField.Enabled = false;
            }
        }

        private void cboHorField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboHorField.SelectedIndex != -1)
            {
                GraphicHelper.pGraphicHelper.HorFieldName = this.cboHorField.Text;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool Do()
        {
            if (this.listView1.Items.Count == 0)
            {
                MessageBox.Show("请至少添加一个数字字段!");
                return false;
            }
            GraphicHelper.pGraphicHelper.FiledNames.Clear();
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                ListViewItem item = this.listView1.Items[i];
                GraphicHelper.pGraphicHelper.Add(item.SubItems[1].Text);
            }
            return GraphicHelper.pGraphicHelper.Show();
        }

        private void ExportToExcelSet_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        public void Init()
        {
            this.cboHorField.Properties.Items.Clear();
            this.cboFields.Properties.Items.Clear();
            this.listView1.Items.Clear();
            if (GraphicHelper.pGraphicHelper.Cursor != null)
            {
                IFields fields;
                if (GraphicHelper.pGraphicHelper.DataSource is ISelectionSet)
                {
                    fields = (GraphicHelper.pGraphicHelper.DataSource as ISelectionSet).Target.Fields;
                }
                else
                {
                    fields = (GraphicHelper.pGraphicHelper.DataSource as ITable).Fields;
                }
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                    esriFieldType type = field.Type;
                    switch (type)
                    {
                        case esriFieldType.esriFieldTypeOID:
                        case esriFieldType.esriFieldTypeSmallInteger:
                        case esriFieldType.esriFieldTypeString:
                        case esriFieldType.esriFieldTypeInteger:
                        case esriFieldType.esriFieldTypeSingle:
                            this.cboHorField.Properties.Items.Add(field.AliasName);
                            break;
                    }
                    if ((((type == esriFieldType.esriFieldTypeDouble) || (type == esriFieldType.esriFieldTypeSmallInteger)) || (type == esriFieldType.esriFieldTypeInteger)) || (type == esriFieldType.esriFieldTypeSingle))
                    {
                        this.cboFields.Properties.Items.Add(field.AliasName);
                    }
                }
                if (this.cboHorField.Properties.Items.Count > 0)
                {
                    this.cboHorField.SelectedIndex = 0;
                }
                if (this.cboFields.Properties.Items.Count > 0)
                {
                    this.cboFields.SelectedIndex = 0;
                }
            }
        }

        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(GraphicsSet));
            this.label1 = new Label();
            this.txtTitle = new TextEdit();
            this.groupBox1 = new GroupBox();
            this.btnDelete = new SimpleButton();
            this.btnSelectField = new SimpleButton();
            this.cboFields = new ComboBoxEdit();
            this.label5 = new Label();
            this.listView1 = new EditListView();
            this.columnHeader1 = new LVColumnHeader();
            this.columnHeader2 = new LVColumnHeader();
            this.cboHorField = new ComboBoxEdit();
            this.label2 = new Label();
            this.txtTitle.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.cboFields.Properties.BeginInit();
            this.cboHorField.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 12);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "标题:";
            this.txtTitle.EditValue = "";
            this.txtTitle.Location = new Point(0x38, 8);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new Size(0xf8, 0x17);
            this.txtTitle.TabIndex = 2;
            this.txtTitle.EditValueChanged += new EventHandler(this.txtTitle_EditValueChanged);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnSelectField);
            this.groupBox1.Controls.Add(this.cboFields);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.listView1);
            this.groupBox1.Controls.Add(this.cboHorField);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new Point(0x10, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x128, 200);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图表设置";
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = (Image) resources.GetObject("btnDelete.Image");
            this.btnDelete.Location = new Point(0x108, 0x80);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x18, 0x18);
            this.btnDelete.TabIndex = 0x12;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnSelectField.Enabled = false;
            this.btnSelectField.Image = (Image) resources.GetObject("btnSelectField.Image");
            this.btnSelectField.Location = new Point(0x108, 0x60);
            this.btnSelectField.Name = "btnSelectField";
            this.btnSelectField.Size = new Size(0x18, 0x18);
            this.btnSelectField.TabIndex = 0x11;
            this.btnSelectField.Click += new EventHandler(this.btnSelectField_Click);
            this.cboFields.EditValue = "";
            this.cboFields.Location = new Point(0x48, 0x38);
            this.cboFields.Name = "cboFields";
            this.cboFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFields.Size = new Size(0xd0, 0x17);
            this.cboFields.TabIndex = 0x10;
            this.cboFields.SelectedIndexChanged += new EventHandler(this.cboFields_SelectedIndexChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(8, 0x38);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 0x11);
            this.label5.TabIndex = 15;
            this.label5.Text = "字段";
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2 });
            this.listView1.ComboBoxBgColor = Color.White;
            this.listView1.ComboBoxFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.listView1.EditBgColor = Color.White;
            this.listView1.EditFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new Point(0x10, 0x58);
            this.listView1.LockRowCount = 0;
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(240, 0x68);
            this.listView1.TabIndex = 14;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader1.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.columnHeader1.Text = "字段";
            this.columnHeader1.Width = 0x6d;
            this.columnHeader2.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.columnHeader2.Text = "别名";
            this.columnHeader2.Width = 0x7d;
            this.cboHorField.EditValue = "";
            this.cboHorField.Location = new Point(0x48, 0x18);
            this.cboHorField.Name = "cboHorField";
            this.cboHorField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboHorField.Size = new Size(0xd0, 0x17);
            this.cboHorField.TabIndex = 10;
            this.cboHorField.SelectedIndexChanged += new EventHandler(this.cboHorField_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x1a);
            this.label2.Name = "label2";
            this.label2.Size = new Size(60, 0x11);
            this.label2.TabIndex = 3;
            this.label2.Text = "横轴字段:";
            this.BackColor = SystemColors.ControlLight;
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.txtTitle);
            base.Controls.Add(this.label1);
            base.Name = "GraphicsSet";
            base.Size = new Size(320, 0xf8);
            base.Load += new EventHandler(this.ExportToExcelSet_Load);
            this.txtTitle.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.cboFields.Properties.EndInit();
            this.cboHorField.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                this.btnDelete.Enabled = true;
            }
            else
            {
                this.btnDelete.Enabled = false;
            }
        }

        private void txtTitle_EditValueChanged(object sender, EventArgs e)
        {
            GraphicHelper.pGraphicHelper.Title = this.txtTitle.Text;
        }
    }
}

