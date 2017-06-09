using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class FeatureDatasetGeneralPage : UserControl
    {
        private bool bool_0 = false;
        private IContainer icontainer_0 = null;
        private Label label1;
        private TextEdit txtName;

        public event ValueChangedHandler ValueChanged;

        public FeatureDatasetGeneralPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                if (this.txtName.Text.Trim().Length == 0)
                {
                    return false;
                }
                NewObjectClassHelper.m_pObjectClassHelper.Name = this.txtName.Text;
            }
            return true;
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void FeatureDatasetGeneralPage_Load(object sender, EventArgs e)
        {
            if (NewObjectClassHelper.m_pObjectClassHelper.FeatureDataset != null)
            {
                this.txtName.Text = NewObjectClassHelper.m_pObjectClassHelper.FeatureDataset.Name;
                this.txtName.Properties.ReadOnly = true;
            }
        }

        private void InitializeComponent()
        {
            this.txtName = new TextEdit();
            this.label1 = new Label();
            this.txtName.Properties.BeginInit();
            base.SuspendLayout();
            this.txtName.EditValue = "";
            this.txtName.Location = new Point(0x35, 0x10);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(240, 0x15);
            this.txtName.TabIndex = 12;
            this.txtName.EditValueChanged += new EventHandler(this.txtName_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(13, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "名称";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.label1);
            base.Name = "FeatureDatasetGeneralPage";
            base.Size = new Size(0x131, 0x167);
            base.Load += new EventHandler(this.FeatureDatasetGeneralPage_Load);
            this.txtName.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void method_0()
        {
            this.bool_0 = true;
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, new EventArgs());
            }
        }

        private void txtName_EditValueChanged(object sender, EventArgs e)
        {
            this.method_0();
        }
    }
}

