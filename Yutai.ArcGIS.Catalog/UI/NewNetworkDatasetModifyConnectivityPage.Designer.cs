using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class NewNetworkDatasetModifyConnectivityPage
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
 private void InitializeComponent()
        {
            this.label1 = new Label();
            this.rdoFalse = new RadioButton();
            this.rdoTrue = new RadioButton();
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.comboBox1 = new ComboBox();
            this.listView1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(161, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "是否用高程字段值修改连通性";
            this.rdoFalse.AutoSize = true;
            this.rdoFalse.Checked = true;
            this.rdoFalse.Location = new System.Drawing.Point(19, 25);
            this.rdoFalse.Name = "rdoFalse";
            this.rdoFalse.Size = new Size(35, 16);
            this.rdoFalse.TabIndex = 7;
            this.rdoFalse.TabStop = true;
            this.rdoFalse.Text = "否";
            this.rdoFalse.UseVisualStyleBackColor = true;
            this.rdoFalse.CheckedChanged += new EventHandler(this.rdoFalse_CheckedChanged);
            this.rdoTrue.AutoSize = true;
            this.rdoTrue.Location = new System.Drawing.Point(19, 56);
            this.rdoTrue.Name = "rdoTrue";
            this.rdoTrue.Size = new Size(35, 16);
            this.rdoTrue.TabIndex = 6;
            this.rdoTrue.Text = "是";
            this.rdoTrue.UseVisualStyleBackColor = true;
            this.rdoTrue.CheckedChanged += new EventHandler(this.rdoTrue_CheckedChanged);
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2 });
            this.listView1.Controls.Add(this.comboBox1);
            this.listView1.Enabled = false;
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(19, 91);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(326, 160);
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.MouseDoubleClick += new MouseEventHandler(this.listView1_MouseDoubleClick);
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader_0.Text = "源";
            this.columnHeader_0.Width = 101;
            this.columnHeader_1.Text = "End";
            this.columnHeader_1.Width = 98;
            this.columnHeader_2.Text = "Field";
            this.columnHeader_2.Width = 110;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(215, 130);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(121, 20);
            this.comboBox1.TabIndex = 10;
            this.comboBox1.Visible = false;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.rdoFalse);
            base.Controls.Add(this.rdoTrue);
            base.Name = "NewNetworkDatasetModifyConnectivityPage";
            base.Size = new Size(390, 293);
            base.Load += new EventHandler(this.NewNetworkDatasetModifyConnectivityPage_Load);
            this.listView1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private ComboBox comboBox1;
        private Label label1;
        private ListView listView1;
        private RadioButton rdoFalse;
        private RadioButton rdoTrue;
    }
}