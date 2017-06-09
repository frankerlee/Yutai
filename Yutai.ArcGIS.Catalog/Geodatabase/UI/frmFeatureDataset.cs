using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class frmFeatureDataset : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Container container_0 = null;
        private FeatureDatasetControl featureDatasetControl_0 = new FeatureDatasetControl();
        private Panel panel1;

        public frmFeatureDataset()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                this.featureDatasetControl_0.Apply();
                base.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "错误");
            }
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmFeatureDataset_Load(object sender, EventArgs e)
        {
            this.panel1.Controls.Add(this.featureDatasetControl_0);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFeatureDataset));
            this.panel1 = new Panel();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            base.SuspendLayout();
            this.panel1.Location = new Point(8, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x150, 320);
            this.panel1.TabIndex = 0;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x68, 0x170);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x40, 0x18);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Location = new Point(0xc0, 0x170);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x160, 0x195);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.panel1);
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.Name = "frmFeatureDataset";
            this.Text = "要素集";
            base.Load += new EventHandler(this.frmFeatureDataset_Load);
            base.ResumeLayout(false);
        }

        private void method_0()
        {
        }

        public IFeatureDataset FeatureDataset
        {
            get
            {
                return this.featureDatasetControl_0.FeatureDataset;
            }
            set
            {
                this.featureDatasetControl_0.FeatureDataset = value;
            }
        }

        public IFeatureWorkspace FeatureWorkspace
        {
            set
            {
                this.featureDatasetControl_0.FeatureWorkspace = value;
            }
        }

        public bool IsEdit
        {
            set
            {
                this.featureDatasetControl_0.IsEdit = value;
            }
        }
    }
}

