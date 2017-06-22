using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;


namespace Yutai.ArcGIS.Carto.UI
{
    partial class LayerDataSourcePropertyPage
    {
        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

       
 private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.lblLR = new Label();
            this.lblBottom = new Label();
            this.lblTop = new Label();
            this.groupBox2 = new GroupBox();
            this.btnSetDatasources = new SimpleButton();
            this.memoEdit1 = new MemoEdit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.memoEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.lblLR);
            this.groupBox1.Controls.Add(this.lblBottom);
            this.groupBox1.Controls.Add(this.lblTop);
            this.groupBox1.Location = new System.Drawing.Point(16, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(304, 88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "范围";
            this.lblLR.Location = new System.Drawing.Point(20, 34);
            this.lblLR.Name = "lblLR";
            this.lblLR.Size = new Size(264, 15);
            this.lblLR.TabIndex = 2;
            this.lblLR.TextAlign = ContentAlignment.MiddleLeft;
            this.lblBottom.Location = new System.Drawing.Point(24, 64);
            this.lblBottom.Name = "lblBottom";
            this.lblBottom.Size = new Size(264, 15);
            this.lblBottom.TabIndex = 1;
            this.lblBottom.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTop.Location = new System.Drawing.Point(24, 17);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new Size(264, 15);
            this.lblTop.TabIndex = 0;
            this.lblTop.TextAlign = ContentAlignment.MiddleCenter;
            this.groupBox2.Controls.Add(this.btnSetDatasources);
            this.groupBox2.Controls.Add(this.memoEdit1);
            this.groupBox2.Location = new System.Drawing.Point(16, 112);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(304, 152);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据源";
            this.btnSetDatasources.Location = new System.Drawing.Point(192, 120);
            this.btnSetDatasources.Name = "btnSetDatasources";
            this.btnSetDatasources.Size = new Size(88, 24);
            this.btnSetDatasources.TabIndex = 1;
            this.btnSetDatasources.Text = "设置数据源...";
            this.btnSetDatasources.Click += new EventHandler(this.btnSetDatasources_Click);
            this.memoEdit1.EditValue = "";
            this.memoEdit1.Location = new System.Drawing.Point(16, 24);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Properties.ReadOnly = true;
            this.memoEdit1.Size = new Size(272, 88);
            this.memoEdit1.TabIndex = 0;
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "LayerDataSourcePropertyPage";
            base.Size = new Size(336, 296);
            base.Load += new EventHandler(this.LayerDataSourcePropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.memoEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnSetDatasources;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label lblBottom;
        private Label lblLR;
        private Label lblTop;
        private MemoEdit memoEdit1;
    }
}