using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Geodatabase;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class frmDataLoader
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
            this.panel2 = new Panel();
            this.panel1 = new Panel();
            this.btnNext = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.btnLast = new SimpleButton();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(474, 324);
            this.panel2.TabIndex = 3;
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnLast);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 324);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(474, 28);
            this.panel1.TabIndex = 2;
            this.btnNext.Location = new Point(167, 2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(64, 24);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "下一步";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(256, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(64, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnLast.Enabled = false;
            this.btnLast.Location = new Point(83, 2);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(64, 24);
            this.btnLast.TabIndex = 0;
            this.btnLast.Text = "上一步";
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(474, 352);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Name = "frmDataLoader";
            this.Text = "装载数据";
            base.Load += new EventHandler(this.frmDataLoader_Load);
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnLast;
        private SimpleButton btnNext;
        private IDataset idataset_0;
        private Panel panel1;
        private Panel panel2;
    }
}