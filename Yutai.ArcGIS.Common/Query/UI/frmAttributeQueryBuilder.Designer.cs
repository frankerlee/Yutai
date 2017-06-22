using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.Query.UI
{
    partial class frmAttributeQueryBuilder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAttributeQueryBuilder));
            this.btnApply = new SimpleButton();
            this.btnClose = new SimpleButton();
            this.panel1 = new Panel();
            this.btnClear = new SimpleButton();
            base.SuspendLayout();
            this.btnApply.Location = new Point(272, 384);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(56, 24);
            this.btnApply.TabIndex = 5;
            this.btnApply.Text = "确定";
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            this.btnClose.Location = new Point(344, 384);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(56, 24);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(408, 376);
            this.panel1.TabIndex = 7;
            this.btnClear.Location = new Point(16, 384);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(56, 24);
            this.btnClear.TabIndex = 51;
            this.btnClear.Text = "清除";
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(408, 415);
            base.Controls.Add(this.btnClear);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.btnApply);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmAttributeQueryBuilder";
            this.Text = "查询生成器";
            base.Load += new EventHandler(this.frmAttributeQueryBuilder_Load);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnApply;
        private SimpleButton btnClear;
        private SimpleButton btnClose;
        private Panel panel1;
    }
}