using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmNewReprensentationWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewReprensentationWizard));
            this.panel1 = new Panel();
            this.btnNext = new Button();
            this.btnLast = new Button();
            this.btnCancel = new Button();
            base.SuspendLayout();
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(476, 314);
            this.panel1.TabIndex = 15;
            this.btnNext.Location = new System.Drawing.Point(326, 326);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(64, 28);
            this.btnNext.TabIndex = 14;
            this.btnNext.Text = "下一步>";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnLast.Enabled = false;
            this.btnLast.Location = new System.Drawing.Point(252, 326);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(64, 28);
            this.btnLast.TabIndex = 12;
            this.btnLast.Text = "<上一步";
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(396, 326);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(64, 28);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(476, 366);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.btnLast);
            base.Controls.Add(this.btnCancel);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmNewReprensentationWizard";
            this.Text = "新建制图表现向导";
            base.Load += new EventHandler(this.frmNewReprensentationWizard_Load);
            base.ResumeLayout(false);
        }

       
        private Button btnCancel;
        private Button btnLast;
        private Button btnNext;
        private Panel panel1;
    }
}