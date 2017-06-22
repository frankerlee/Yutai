using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmTKWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTKWizard));
            this.btnNext = new Button();
            this.btnLast = new Button();
            this.btnCancel = new Button();
            this.panel1 = new Panel();
            base.SuspendLayout();
            this.btnNext.Location = new Point(363, 386);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(64, 24);
            this.btnNext.TabIndex = 10;
            this.btnNext.Text = "下一步>";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnLast.Enabled = false;
            this.btnLast.Location = new Point(289, 386);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(64, 24);
            this.btnLast.TabIndex = 8;
            this.btnLast.Text = "<上一步";
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(433, 386);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(64, 24);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(502, 368);
            this.panel1.TabIndex = 11;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(502, 423);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.btnLast);
            base.Controls.Add(this.btnCancel);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmTKWizard";
            this.Text = "图框向导";
            base.Load += new EventHandler(this.frmTKWizard_Load);
            base.ResumeLayout(false);
        }

       
        private Button btnCancel;
        private Button btnLast;
        private Button btnNext;
        private Panel panel1;
    }
}