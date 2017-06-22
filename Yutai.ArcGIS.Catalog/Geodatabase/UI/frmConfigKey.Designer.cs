using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmConfigKey
    {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfigKey));
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.label1 = new Label();
            this.btnOK = new SimpleButton();
            base.SuspendLayout();
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2 });
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new Point(16, 40);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(344, 200);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.columnHeader_0.Text = "名称";
            this.columnHeader_0.Width = 131;
            this.columnHeader_1.Text = "类型";
            this.columnHeader_1.Width = 85;
            this.columnHeader_2.Text = "描述";
            this.columnHeader_2.Width = 123;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "配置关键字";
            this.btnOK.Location = new Point(168, 248);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(392, 282);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.listView1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmConfigKey";
            this.Text = "配置关键字";
            base.Load += new EventHandler(this.frmConfigKey_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnOK;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private IWorkspaceConfiguration iworkspaceConfiguration_0;
        private Label label1;
        private ListView listView1;
    }
}