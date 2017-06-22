using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmSpatialReference
    {
        protected override void Dispose(bool bool_5)
        {
            if (bool_5 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_5);
        }

       
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSpatialReference));
            this.tabControl1 = new TabControl();
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            base.SuspendLayout();
            this.tabControl1.Location = new System.Drawing.Point(8, 16);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(312, 432);
            this.tabControl1.TabIndex = 0;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(256, 464);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(56, 24);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(184, 464);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(330, 493);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.tabControl1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmSpatialReference";
            base.ShowInTaskbar = false;
            this.Text = "空间参考";
            base.Load += new EventHandler(this.frmSpatialReference_Load);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private CoordinateControl coordinateControl_0;
        private ISpatialReference ispatialReference_0;
        private MDomainControl mdomainControl_0;
        private TabControl tabControl1;
        private XYDomainControl xydomainControl_0;
        private ZDomainControl zdomainControl_0;
    }
}