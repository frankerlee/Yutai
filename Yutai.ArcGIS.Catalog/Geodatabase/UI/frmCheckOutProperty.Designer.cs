using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmCheckOutProperty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckOutProperty));
            this.CheckOutDatasetlist = new ListBox();
            this.lblParentVersion = new Label();
            this.lblCreateTime = new Label();
            this.lblOwner = new Label();
            this.lblName = new Label();
            this.label5 = new Label();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.simpleButton1 = new SimpleButton();
            this.btnOK = new SimpleButton();
            base.SuspendLayout();
            this.CheckOutDatasetlist.ItemHeight = 12;
            this.CheckOutDatasetlist.Location = new Point(8, 152);
            this.CheckOutDatasetlist.Name = "CheckOutDatasetlist";
            this.CheckOutDatasetlist.Size = new Size(232, 124);
            this.CheckOutDatasetlist.TabIndex = 19;
            this.lblParentVersion.Location = new Point(72, 96);
            this.lblParentVersion.Name = "lblParentVersion";
            this.lblParentVersion.Size = new Size(160, 24);
            this.lblParentVersion.TabIndex = 18;
            this.lblCreateTime.Location = new Point(72, 64);
            this.lblCreateTime.Name = "lblCreateTime";
            this.lblCreateTime.Size = new Size(160, 24);
            this.lblCreateTime.TabIndex = 17;
            this.lblOwner.Location = new Point(72, 40);
            this.lblOwner.Name = "lblOwner";
            this.lblOwner.Size = new Size(160, 24);
            this.lblOwner.TabIndex = 16;
            this.lblName.Location = new Point(72, 8);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(160, 24);
            this.lblName.TabIndex = 15;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(8, 128);
            this.label5.Name = "label5";
            this.label5.Size = new Size(59, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "检出数据:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 96);
            this.label4.Name = "label4";
            this.label4.Size = new Size(47, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "父版本:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 64);
            this.label3.Name = "label3";
            this.label3.Size = new Size(35, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "创建:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(35, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "属主:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "名字:";
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new Point(176, 288);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(56, 24);
            this.simpleButton1.TabIndex = 21;
            this.simpleButton1.Text = "取消";
            this.btnOK.Location = new Point(104, 288);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 20;
            this.btnOK.Text = "确定";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(256, 325);
            base.Controls.Add(this.simpleButton1);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.CheckOutDatasetlist);
            base.Controls.Add(this.lblParentVersion);
            base.Controls.Add(this.lblCreateTime);
            base.Controls.Add(this.lblOwner);
            base.Controls.Add(this.lblName);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            
            base.Name = "frmCheckOutProperty";
            this.Text = "frmCheckOutProperty";
            base.Load += new EventHandler(this.frmCheckOutProperty_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnOK;
        private ListBox CheckOutDatasetlist;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label lblCreateTime;
        private Label lblName;
        private Label lblOwner;
        private Label lblParentVersion;
        private SimpleButton simpleButton1;
    }
}