using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class TopologyGeneralPropertyPage
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
 private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.groupBox1 = new GroupBox();
            this.lblStatus = new Label();
            this.txtName = new TextEdit();
            this.txtClusterTolerance = new TextEdit();
            this.lblTopoError1 = new Label();
            this.lblTopoError2 = new Label();
            this.groupBox1.SuspendLayout();
            this.txtName.Properties.BeginInit();
            this.txtClusterTolerance.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(16, 48);
            this.label2.Name = "label2";
            this.label2.Size = new Size(48, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "容限值:";
            this.groupBox1.Controls.Add(this.lblTopoError2);
            this.groupBox1.Controls.Add(this.lblTopoError1);
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Location = new Point(16, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(280, 152);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "状态";
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new Point(32, 32);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(0, 17);
            this.lblStatus.TabIndex = 0;
            this.txtName.EditValue = "";
            this.txtName.Location = new Point(64, 8);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(184, 23);
            this.txtName.TabIndex = 3;
            this.txtName.EditValueChanged += new EventHandler(this.txtName_EditValueChanged);
            this.txtClusterTolerance.EditValue = "";
            this.txtClusterTolerance.Location = new Point(64, 48);
            this.txtClusterTolerance.Name = "txtClusterTolerance";
            this.txtClusterTolerance.Size = new Size(184, 23);
            this.txtClusterTolerance.TabIndex = 4;
            this.txtClusterTolerance.EditValueChanged += new EventHandler(this.txtClusterTolerance_EditValueChanged);
            this.lblTopoError1.Location = new Point(16, 24);
            this.lblTopoError1.Name = "lblTopoError1";
            this.lblTopoError1.Size = new Size(136, 24);
            this.lblTopoError1.TabIndex = 1;
            this.lblTopoError2.Location = new Point(48, 56);
            this.lblTopoError2.Name = "lblTopoError2";
            this.lblTopoError2.Size = new Size(216, 56);
            this.lblTopoError2.TabIndex = 2;
            base.Controls.Add(this.txtClusterTolerance);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "TopologyGeneralPropertyPage";
            base.Size = new Size(344, 264);
            base.Load += new EventHandler(this.TopologyGeneralPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtName.Properties.EndInit();
            this.txtClusterTolerance.Properties.EndInit();
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label lblStatus;
        private Label lblTopoError1;
        private Label lblTopoError2;
        private TextEdit txtClusterTolerance;
        private TextEdit txtName;
    }
}