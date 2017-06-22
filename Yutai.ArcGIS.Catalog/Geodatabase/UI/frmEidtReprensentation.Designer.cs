using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmEidtReprensentation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEidtReprensentation));
            this.btnNext = new Button();
            this.btnCancel = new Button();
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.tabPage2 = new TabPage();
            this.tabControl1.SuspendLayout();
            base.SuspendLayout();
            this.btnNext.Location = new Point(326, 326);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(64, 28);
            this.btnNext.TabIndex = 14;
            this.btnNext.Text = "确定";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(396, 326);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(64, 28);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = DockStyle.Top;
            this.tabControl1.Location = new Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(476, 320);
            this.tabControl1.TabIndex = 15;
            this.tabPage1.Location = new Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(468, 295);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "常规";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage2.Location = new Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new Size(468, 295);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "规则";
            this.tabPage2.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(476, 366);
            base.Controls.Add(this.tabControl1);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.btnCancel);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmEidtReprensentation";
            this.Text = "新建制图表现向导";
            base.Load += new EventHandler(this.frmEidtReprensentation_Load);
            this.tabControl1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private Button btnCancel;
        private Button btnNext;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
    }
}