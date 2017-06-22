using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmNewGN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewGN));
            this.panel2 = new Panel();
            this.panel1 = new Panel();
            this.btnNext = new SimpleButton();
            this.btnLast = new SimpleButton();
            this.btnCnacel = new SimpleButton();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(448, 286);
            this.panel2.TabIndex = 3;
            this.panel1.Controls.Add(this.btnCnacel);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnLast);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 286);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(448, 37);
            this.panel1.TabIndex = 2;
            this.btnNext.Location = new Point(304, 8);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(56, 24);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "下一步";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnLast.Enabled = false;
            this.btnLast.Location = new Point(240, 8);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(56, 24);
            this.btnLast.TabIndex = 3;
            this.btnLast.Text = "上一步";
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.btnCnacel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCnacel.Location = new Point(366, 8);
            this.btnCnacel.Name = "btnCnacel";
            this.btnCnacel.Size = new Size(56, 24);
            this.btnCnacel.TabIndex = 5;
            this.btnCnacel.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(448, 323);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmNewGN";
            this.Text = "创建几何网络";
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnCnacel;
        private SimpleButton btnLast;
        private SimpleButton btnNext;
        private Panel panel1;
        private Panel panel2;
    }
}