namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Catalog;
    using JLK.Editors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmMultiDataConvert : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Container container_0 = null;
        private MultiObjectClassSelectControl multiObjectClassSelectControl_0 = new MultiObjectClassSelectControl();
        private Panel panel1;
        private Panel panel2;

        public frmMultiDataConvert()
        {
            this.InitializeComponent();
            this.multiObjectClassSelectControl_0.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.multiObjectClassSelectControl_0);
        }

        public void Add(IGxObjectFilter igxObjectFilter_0)
        {
            this.multiObjectClassSelectControl_0.Add(igxObjectFilter_0);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.multiObjectClassSelectControl_0.CanDo())
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                this.multiObjectClassSelectControl_0.Do();
                System.Windows.Forms.Cursor.Current = Cursors.Default;
                base.DialogResult = DialogResult.OK;
            }
        }

        public void Clear()
        {
            this.multiObjectClassSelectControl_0.Clear();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmMultiDataConvert_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmMultiDataConvert));
            this.panel1 = new Panel();
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.panel2 = new Panel();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x109);
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
            this.panel2.Size = new Size(360, 0x109);
            this.panel2.TabIndex = 1;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(360, 0x125);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmMultiDataConvert";
            this.Text = "多要素类转换";
            base.Load += new EventHandler(this.frmMultiDataConvert_Load);
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public esriDatasetType ImportDatasetType
        {
            set
            {
                this.multiObjectClassSelectControl_0.ImportDatasetType = value;
            }
        }

        public IGxObject InGxObject
        {
            set
            {
                this.multiObjectClassSelectControl_0.InGxObject = value;
            }
        }

        public bool IsAnnotation
        {
            set
            {
                this.multiObjectClassSelectControl_0.IsAnnotation = value;
            }
        }

        public IGxObject OutGxObject
        {
            set
            {
                this.multiObjectClassSelectControl_0.OutGxObject = value;
            }
        }
    }
}

