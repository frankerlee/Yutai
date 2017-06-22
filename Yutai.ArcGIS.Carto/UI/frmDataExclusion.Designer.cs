using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmDataExclusion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataExclusion));
            this.panel1 = new Panel();
            this.btnApply = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.tabControl1 = new TabControl();
            this.TPQuery = new TabPage();
            this.TPLegend = new TabPage();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.btnApply);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 397);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(392, 32);
            this.panel1.TabIndex = 0;
            this.btnApply.Enabled = false;
            this.btnApply.Location = new Point(320, 3);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(56, 24);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "应用";
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(256, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(56, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(192, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.tabControl1.Controls.Add(this.TPQuery);
            this.tabControl1.Controls.Add(this.TPLegend);
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Location = new Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(392, 397);
            this.tabControl1.TabIndex = 1;
            this.TPQuery.Location = new Point(4, 21);
            this.TPQuery.Name = "TPQuery";
            this.TPQuery.Size = new Size(384, 372);
            this.TPQuery.TabIndex = 0;
            this.TPQuery.Text = "查询";
            this.TPLegend.Location = new Point(4, 21);
            this.TPLegend.Name = "TPLegend";
            this.TPLegend.Size = new Size(384, 372);
            this.TPLegend.TabIndex = 1;
            this.TPLegend.Text = "图例";
            this.TPLegend.Visible = false;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(392, 429);
            base.Controls.Add(this.tabControl1);
            base.Controls.Add(this.panel1);
            
            base.Name = "frmDataExclusion";
            this.Text = "数据排除属性";
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnApply;
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Panel panel1;
        private TabControl tabControl1;
        private TabPage TPLegend;
        private TabPage TPQuery;
    }
}