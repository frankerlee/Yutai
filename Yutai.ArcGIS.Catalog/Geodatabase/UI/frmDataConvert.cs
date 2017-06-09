using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class frmDataConvert : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Container container_0 = null;
        private ObjectSelectControl objectSelectControl_0 = new ObjectSelectControl();
        private Panel panel1;
        private Panel panel2;

        public frmDataConvert()
        {
            this.InitializeComponent();
            this.objectSelectControl_0.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.objectSelectControl_0);
            this.objectSelectControl_0.ImportDatasetType = esriDatasetType.esriDTFeatureClass;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.objectSelectControl_0.CanDo())
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                try
                {
                    this.objectSelectControl_0.Do();
                }
                catch
                {
                }
                System.Windows.Forms.Cursor.Current = Cursors.Default;
                base.DialogResult = DialogResult.OK;
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

        private void frmDataConvert_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataConvert));
            this.panel1 = new Panel();
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.panel2 = new Panel();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0xd9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(360, 0x1c);
            this.panel1.TabIndex = 0;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x100, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnOK.Location = new Point(0xb0, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x40, 0x18);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(360, 0xd9);
            this.panel2.TabIndex = 1;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(360, 0xf5);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmDataConvert";
            this.Text = "数据转换";
            base.Load += new EventHandler(this.frmDataConvert_Load);
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public esriDatasetType ImportDatasetType
        {
            set
            {
                this.objectSelectControl_0.ImportDatasetType = value;
            }
        }

        public IGxObject InGxObject
        {
            set
            {
                this.objectSelectControl_0.InGxObject = value;
            }
        }

        public IGxObject OutGxObject
        {
            set
            {
                this.objectSelectControl_0.OutGxObject = value;
            }
        }
    }
}

