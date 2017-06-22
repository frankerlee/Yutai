using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class ExportChangeSetupCtrl
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
           System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportChangeSetupCtrl));
            this.lblInfo = new Label();
            this.txtDeltaFile = new TextEdit();
            this.btnSelectOutGDB = new SimpleButton();
            this.label1 = new Label();
            this.lblCheckOutName = new Label();
            this.lblCheckOutGDB = new Label();
            this.label3 = new Label();
            this.txtDeltaFile.Properties.BeginInit();
            base.SuspendLayout();
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new Point(16, 16);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new Size(196, 17);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "要创建的增量数据库或增量xml文件";
            this.txtDeltaFile.EditValue = "";
            this.txtDeltaFile.Location = new Point(16, 40);
            this.txtDeltaFile.Name = "txtDeltaFile";
            this.txtDeltaFile.Size = new Size(224, 23);
            this.txtDeltaFile.TabIndex = 1;
            this.btnSelectOutGDB.Image = (Image) resources.GetObject("btnSelectOutGDB.Image");
            this.btnSelectOutGDB.Location = new Point(248, 40);
            this.btnSelectOutGDB.Name = "btnSelectOutGDB";
            this.btnSelectOutGDB.Size = new Size(24, 24);
            this.btnSelectOutGDB.TabIndex = 10;
            this.btnSelectOutGDB.Click += new EventHandler(this.btnSelectOutGDB_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 88);
            this.label1.Name = "label1";
            this.label1.Size = new Size(60, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "检出名称:";
            this.lblCheckOutName.AutoSize = true;
            this.lblCheckOutName.Location = new Point(120, 88);
            this.lblCheckOutName.Name = "lblCheckOutName";
            this.lblCheckOutName.Size = new Size(0, 17);
            this.lblCheckOutName.TabIndex = 12;
            this.lblCheckOutGDB.AutoSize = true;
            this.lblCheckOutGDB.Location = new Point(120, 136);
            this.lblCheckOutGDB.Name = "lblCheckOutGDB";
            this.lblCheckOutGDB.Size = new Size(0, 17);
            this.lblCheckOutGDB.TabIndex = 14;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(16, 136);
            this.label3.Name = "label3";
            this.label3.Size = new Size(72, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "导出数据库:";
            base.Controls.Add(this.lblCheckOutGDB);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.lblCheckOutName);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.btnSelectOutGDB);
            base.Controls.Add(this.txtDeltaFile);
            base.Controls.Add(this.lblInfo);
            base.Name = "ExportChangeSetupCtrl";
            base.Size = new Size(304, 264);
            base.Load += new EventHandler(this.ExportChangeSetupCtrl_Load);
            this.txtDeltaFile.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnSelectOutGDB;
        private Label label1;
        private Label label3;
        private Label lblCheckOutGDB;
        private Label lblCheckOutName;
        private Label lblInfo;
        private TextEdit txtDeltaFile;
    }
}