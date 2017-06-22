using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class frmNewServerObject
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewServerObject));
            this.panel1 = new Panel();
            this.simpleButton3 = new SimpleButton();
            this.btnNext = new SimpleButton();
            this.btnLast = new SimpleButton();
            this.panel2 = new Panel();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.simpleButton3);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnLast);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 290);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(504, 40);
            this.panel1.TabIndex = 0;
            this.simpleButton3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton3.Location = new Point(368, 8);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new Size(80, 24);
            this.simpleButton3.TabIndex = 2;
            this.simpleButton3.Text = "取消";
            this.btnNext.Location = new Point(272, 8);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(80, 24);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "下一步";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnLast.Enabled = false;
            this.btnLast.Location = new Point(176, 8);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(80, 24);
            this.btnLast.TabIndex = 0;
            this.btnLast.Text = "上一步";
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(504, 290);
            this.panel2.TabIndex = 1;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(504, 330);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            
            base.Name = "frmNewServerObject";
            this.Text = "New ServerObject";
            base.Load += new EventHandler(this.frmNewServerObject_Load);
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnLast;
        private SimpleButton btnNext;
        private Panel panel1;
        private Panel panel2;
        private SimpleButton simpleButton3;
    }
}