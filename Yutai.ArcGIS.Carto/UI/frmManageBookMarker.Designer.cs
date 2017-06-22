using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmManageBookMarker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManageBookMarker));
            this.listView1 = new ListView();
            this.btnZoomTo = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.btnDeleteAll = new SimpleButton();
            this.btnClose = new SimpleButton();
            base.SuspendLayout();
            this.listView1.Location = new Point(8, 16);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(192, 144);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.btnZoomTo.Enabled = false;
            this.btnZoomTo.Location = new Point(208, 16);
            this.btnZoomTo.Name = "btnZoomTo";
            this.btnZoomTo.Size = new Size(64, 24);
            this.btnZoomTo.TabIndex = 1;
            this.btnZoomTo.Text = "缩放到";
            this.btnZoomTo.Click += new EventHandler(this.btnZoomTo_Click);
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new Point(208, 48);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(64, 24);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnDeleteAll.Location = new Point(208, 80);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new Size(64, 24);
            this.btnDeleteAll.TabIndex = 3;
            this.btnDeleteAll.Text = "删除所有";
            this.btnDeleteAll.Click += new EventHandler(this.btnDeleteAll_Click);
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new Point(208, 136);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(64, 24);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "关闭";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(292, 170);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.btnDeleteAll);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnZoomTo);
            base.Controls.Add(this.listView1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmManageBookMarker";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "管理书签";
            base.Load += new EventHandler(this.frmManageBookMarker_Load);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnClose;
        private SimpleButton btnDelete;
        private SimpleButton btnDeleteAll;
        private SimpleButton btnZoomTo;
        private ListView listView1;
    }
}