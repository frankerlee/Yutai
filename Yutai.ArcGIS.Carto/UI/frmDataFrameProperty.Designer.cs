using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmDataFrameProperty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataFrameProperty));
            this.tabControl1 = new TabControl();
            this.tabPageGeneral = new TabPage();
            this.tabPageCoordinate = new TabPage();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.tabPageMapDataFrame = new TabPage();
            this.tabControl1.SuspendLayout();
            base.SuspendLayout();
            this.tabControl1.Controls.Add(this.tabPageGeneral);
            this.tabControl1.Controls.Add(this.tabPageCoordinate);
            this.tabControl1.Controls.Add(this.tabPageMapDataFrame);
            this.tabControl1.Location = new Point(8, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(360, 408);
            this.tabControl1.TabIndex = 0;
            this.tabPageGeneral.Location = new Point(4, 22);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Size = new Size(352, 382);
            this.tabPageGeneral.TabIndex = 1;
            this.tabPageGeneral.Text = "常规";
            this.tabPageCoordinate.Location = new Point(4, 22);
            this.tabPageCoordinate.Name = "tabPageCoordinate";
            this.tabPageCoordinate.Size = new Size(352, 382);
            this.tabPageCoordinate.TabIndex = 0;
            this.tabPageCoordinate.Text = "坐标系统";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(208, 424);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(280, 424);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(56, 24);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.tabPageMapDataFrame.Location = new Point(4, 22);
            this.tabPageMapDataFrame.Name = "tabPageMapDataFrame";
            this.tabPageMapDataFrame.Size = new Size(352, 382);
            this.tabPageMapDataFrame.TabIndex = 2;
            this.tabPageMapDataFrame.Text = "数据框";
            this.tabPageMapDataFrame.UseVisualStyleBackColor = true;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(384, 453);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.tabControl1);
          
            base.Name = "frmDataFrameProperty";
            this.Text = "数据框属性";
            base.Load += new EventHandler(this.frmDataFrameProperty_Load);
            this.tabControl1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private TabControl tabControl1;
        private TabPage tabPageCoordinate;
        private TabPage tabPageGeneral;
        private TabPage tabPageMapDataFrame;
    }
}