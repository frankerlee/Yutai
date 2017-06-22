using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class BulidGN_Weights
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
 private void InitializeComponent()
        {
            ListViewItem item = new ListViewItem(new string[] { "", "" }, -1);
            this.groupBox1 = new GroupBox();
            this.radioGroup1 = new RadioGroup();
            this.label1 = new Label();
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.btnDelete = new SimpleButton();
            this.btnAddRow = new SimpleButton();
            this.groupBox1.SuspendLayout();
            this.radioGroup1.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.radioGroup1);
            this.groupBox1.Location = new Point(16, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(208, 56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "是否设置权重";
            this.radioGroup1.Location = new Point(16, 24);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Columns = 2;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "否"), new RadioGroupItem(null, "是") });
            this.radioGroup1.Size = new Size(144, 24);
            this.radioGroup1.TabIndex = 1;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 72);
            this.label1.Name = "label1";
            this.label1.Size = new Size(116, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "设置权重名称和类型";
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2 });
            this.listView1.Enabled = false;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Items.AddRange(new ListViewItem[] { item });
            this.listView1.Location = new Point(16, 96);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(216, 128);
            this.listView1.TabIndex = 2;
            this.listView1.View = View.Details;
            this.listView1.MouseDown += new MouseEventHandler(this.listView1_MouseDown);
            this.listView1.DoubleClick += new EventHandler(this.listView1_DoubleClick);
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader_0.Text = "权重名";
            this.columnHeader_0.Width = 71;
            this.columnHeader_1.Text = "类型";
            this.columnHeader_1.Width = 70;
            this.columnHeader_2.Text = "位门";
            this.columnHeader_2.Width = 66;
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new Point(240, 128);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(40, 24);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnAddRow.Enabled = false;
            this.btnAddRow.Location = new Point(240, 96);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new Size(40, 24);
            this.btnAddRow.TabIndex = 8;
            this.btnAddRow.Text = "添加";
            this.btnAddRow.Click += new EventHandler(this.btnAddRow_Click);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAddRow);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.groupBox1);
            base.Name = "BulidGN_Weights";
            base.Size = new Size(296, 240);
            base.Load += new EventHandler(this.BulidGN_Weights_Load);
            this.groupBox1.ResumeLayout(false);
            this.radioGroup1.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnAddRow;
        private SimpleButton btnDelete;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private System.Windows.Forms.ComboBox comboBox_0;
        private System.Windows.Forms.ComboBox comboBox_1;
        private GroupBox groupBox1;
        private int int_0;
        private int int_1;
        private Label label1;
        private ListView listView1;
        private RadioGroup radioGroup1;
        private TextBox textBox_0;
    }
}