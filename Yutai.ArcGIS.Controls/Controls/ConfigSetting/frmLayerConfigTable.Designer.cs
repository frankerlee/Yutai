using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Controls.ConfigSetting
{
    partial class frmLayerConfigTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLayerConfigTable));
            this.treeView1 = new TreeView();
            this.btnOk = new SimpleButton();
            this.btnCancel = new SimpleButton();
            base.SuspendLayout();
            this.treeView1.Location = new Point(8, 8);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new Size(248, 216);
            this.treeView1.TabIndex = 0;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new Point(104, 240);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new Size(64, 24);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "确定";
            this.btnOk.Click += new EventHandler(this.btnOk_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(184, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(64, 24);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(264, 273);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOk);
            base.Controls.Add(this.treeView1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmLayerConfigTable";
            this.Text = "选择图层配置表";
            base.Load += new EventHandler(this.frmLayerConfigTable_Load);
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private SimpleButton btnCancel;
        private SimpleButton btnOk;
        private TreeView treeView1;
    }
}