using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class CheckOutSetupCtrl
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
           System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckOutSetupCtrl));
            this.label1 = new Label();
            this.label2 = new Label();
            this.rdoType = new RadioGroup();
            this.label3 = new Label();
            this.txtOutGDB = new TextEdit();
            this.chkResueSchema = new CheckEdit();
            this.label4 = new Label();
            this.txtCheckOutName = new TextEdit();
            this.btnSelectOutGDB = new SimpleButton();
            this.rdoType.Properties.BeginInit();
            this.txtOutGDB.Properties.BeginInit();
            this.chkResueSchema.Properties.BeginInit();
            this.txtCheckOutName.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(79, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "检出数据,从:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(104, 8);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0, 17);
            this.label2.TabIndex = 1;
            this.rdoType.Location = new Point(16, 32);
            this.rdoType.Name = "rdoType";
            this.rdoType.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "数据"), new RadioGroupItem(null, "仅方案") });
            this.rdoType.Size = new Size(88, 48);
            this.rdoType.TabIndex = 2;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(16, 96);
            this.label3.Name = "label3";
            this.label3.Size = new Size(85, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "检出到数据库:";
            this.txtOutGDB.EditValue = "";
            this.txtOutGDB.Location = new Point(16, 120);
            this.txtOutGDB.Name = "txtOutGDB";
            this.txtOutGDB.Size = new Size(192, 23);
            this.txtOutGDB.TabIndex = 4;
            this.chkResueSchema.Location = new Point(16, 152);
            this.chkResueSchema.Name = "chkResueSchema";
            this.chkResueSchema.Properties.Caption = "原来空间数据库被检出，则重用方案";
            this.chkResueSchema.Size = new Size(216, 19);
            this.chkResueSchema.TabIndex = 5;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(16, 176);
            this.label4.Name = "label4";
            this.label4.Size = new Size(60, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "检出名称:";
            this.txtCheckOutName.EditValue = "";
            this.txtCheckOutName.Location = new Point(16, 192);
            this.txtCheckOutName.Name = "txtCheckOutName";
            this.txtCheckOutName.Size = new Size(192, 23);
            this.txtCheckOutName.TabIndex = 7;
            this.btnSelectOutGDB.Image = (Image) resources.GetObject("btnSelectOutGDB.Image");
            this.btnSelectOutGDB.Location = new Point(216, 120);
            this.btnSelectOutGDB.Name = "btnSelectOutGDB";
            this.btnSelectOutGDB.Size = new Size(24, 24);
            this.btnSelectOutGDB.TabIndex = 9;
            base.Controls.Add(this.btnSelectOutGDB);
            base.Controls.Add(this.txtCheckOutName);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.chkResueSchema);
            base.Controls.Add(this.txtOutGDB);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.rdoType);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "CheckOutSetupCtrl";
            base.Size = new Size(304, 272);
            base.Load += new EventHandler(this.CheckOutSetupCtrl_Load);
            this.rdoType.Properties.EndInit();
            this.txtOutGDB.Properties.EndInit();
            this.chkResueSchema.Properties.EndInit();
            this.txtCheckOutName.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnSelectOutGDB;
        private CheckEdit chkResueSchema;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private RadioGroup rdoType;
        private TextEdit txtCheckOutName;
        private TextEdit txtOutGDB;
    }
}