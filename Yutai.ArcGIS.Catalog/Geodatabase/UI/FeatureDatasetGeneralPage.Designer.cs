using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class FeatureDatasetGeneralPage
    {
        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

       
        private void InitializeComponent()
        {
            this.txtName = new TextEdit();
            this.label1 = new Label();
            this.txtName.Properties.BeginInit();
            base.SuspendLayout();
            this.txtName.EditValue = "";
            this.txtName.Location = new Point(53, 16);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(240, 21);
            this.txtName.TabIndex = 12;
            this.txtName.EditValueChanged += new EventHandler(this.txtName_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "名称";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.label1);
            base.Name = "FeatureDatasetGeneralPage";
            base.Size = new Size(305, 359);
            base.Load += new EventHandler(this.FeatureDatasetGeneralPage_Load);
            this.txtName.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Label label1;
        private TextEdit txtName;
    }
}