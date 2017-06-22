using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmFind
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
            this.panel1 = new Panel();
            this.btnFind = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.btnStop = new SimpleButton();
            base.SuspendLayout();
            this.panel1.Dock = DockStyle.Left;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(464, 141);
            this.panel1.TabIndex = 0;
            this.btnFind.Location = new Point(472, 8);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new Size(56, 24);
            this.btnFind.TabIndex = 1;
            this.btnFind.Text = "查找";
            this.btnFind.Click += new EventHandler(this.btnFind_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(472, 72);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(56, 24);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.btnStop.Enabled = false;
            this.btnStop.Location = new Point(472, 40);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new Size(56, 24);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "停止";
            this.btnStop.Click += new EventHandler(this.btnStop_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(544, 141);
            base.Controls.Add(this.btnStop);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnFind);
            base.Controls.Add(this.panel1);
            base.Name = "frmFind";
            this.Text = "查找";
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnFind;
        private SimpleButton btnStop;
        private Panel panel1;
    }
}