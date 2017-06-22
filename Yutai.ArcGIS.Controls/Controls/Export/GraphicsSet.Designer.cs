using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.ControlExtend;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    partial class GraphicsSet
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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
            this.label1.Location = new Point(16, 12);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "标题:";
            this.txtTitle.EditValue = "";
            this.txtTitle.Location = new Point(56, 8);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new Size(248, 23);
            this.txtTitle.TabIndex = 2;
            this.txtTitle.EditValueChanged += new EventHandler(this.txtTitle_EditValueChanged);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnSelectField);
            this.groupBox1.Controls.Add(this.cboFields);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.listView1);
            this.groupBox1.Controls.Add(this.cboHorField);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new Point(16, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(296, 200);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图表设置";
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = (Image) resources.GetObject("btnDelete.Image");
            this.btnDelete.Location = new Point(264, 128);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(24, 24);
            this.btnDelete.TabIndex = 18;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnSelectField.Enabled = false;
            this.btnSelectField.Image = (Image) resources.GetObject("btnSelectField.Image");
            this.btnSelectField.Location = new Point(264, 96);
            this.btnSelectField.Name = "btnSelectField";
            this.btnSelectField.Size = new Size(24, 24);
            this.btnSelectField.TabIndex = 17;
            this.btnSelectField.Click += new EventHandler(this.btnSelectField_Click);
            this.cboFields.EditValue = "";
            this.cboFields.Location = new Point(72, 56);
            this.cboFields.Name = "cboFields";
            this.cboFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFields.Size = new Size(208, 23);
            this.cboFields.TabIndex = 16;
            this.cboFields.SelectedIndexChanged += new EventHandler(this.cboFields_SelectedIndexChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(8, 56);
            this.label5.Name = "label5";
            this.label5.Size = new Size(29, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "字段";
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2 });
            this.listView1.ComboBoxBgColor = Color.White;
            this.listView1.ComboBoxFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.listView1.EditBgColor = Color.White;
            this.listView1.EditFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new Point(16, 88);
            this.listView1.LockRowCount = 0;
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(240, 104);
            this.listView1.TabIndex = 14;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader1.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.columnHeader1.Text = "字段";
            this.columnHeader1.Width = 109;
            this.columnHeader2.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.columnHeader2.Text = "别名";
            this.columnHeader2.Width = 125;
            this.cboHorField.EditValue = "";
            this.cboHorField.Location = new Point(72, 24);
            this.cboHorField.Name = "cboHorField";
            this.cboHorField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboHorField.Size = new Size(208, 23);
            this.cboHorField.TabIndex = 10;
            this.cboHorField.SelectedIndexChanged += new EventHandler(this.cboHorField_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 26);
            this.label2.Name = "label2";
            this.label2.Size = new Size(60, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "横轴字段:";
            this.BackColor = SystemColors.ControlLight;
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.txtTitle);
            base.Controls.Add(this.label1);
            base.Name = "GraphicsSet";
            base.Size = new Size(320, 248);
            base.Load += new EventHandler(this.ExportToExcelSet_Load);
            this.txtTitle.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.cboFields.Properties.EndInit();
            this.cboHorField.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private SimpleButton btnDelete;
        private SimpleButton btnSelectField;
        private ComboBoxEdit cboFields;
        private ComboBoxEdit cboHorField;
        private LVColumnHeader columnHeader1;
        private LVColumnHeader columnHeader2;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label5;
        private EditListView listView1;
        private TextEdit txtTitle;
    }
}