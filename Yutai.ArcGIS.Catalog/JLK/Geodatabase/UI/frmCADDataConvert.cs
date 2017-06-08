namespace JLK.Geodatabase.UI
{
    using JLK.Catalog;
    using JLK.Editors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmCADDataConvert : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private CADDataConvertControl caddataConvertControl_0 = new CADDataConvertControl();
        private Container container_0 = null;
        private Panel panel1;
        private Panel panel2;

        public frmCADDataConvert()
        {
            this.InitializeComponent();
            this.caddataConvertControl_0.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.caddataConvertControl_0);
        }

        public void Add(IGxObjectFilter igxObjectFilter_0)
        {
            this.caddataConvertControl_0.Add(igxObjectFilter_0);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.caddataConvertControl_0.CanDo())
            {
                Cursor.Current = Cursors.WaitCursor;
                this.caddataConvertControl_0.Do();
                Cursor.Current = Cursors.Default;
                base.Close();
            }
        }

        public void Clear()
        {
            this.caddataConvertControl_0.Clear();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmCADDataConvert_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmCADDataConvert));
            this.panel1 = new Panel();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.panel2 = new Panel();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x109);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(360, 0x1c);
            this.panel1.TabIndex = 0;
            this.btnOK.Location = new Point(0xb0, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x40, 0x18);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x100, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
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
            base.Name = "frmCADDataConvert";
            this.Text = "导入CAD数据";
            base.Load += new EventHandler(this.frmCADDataConvert_Load);
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public IGxObject InGxObject
        {
            set
            {
                this.caddataConvertControl_0.InGxObject = value;
            }
        }

        public IGxObject OutGxObject
        {
            set
            {
                this.caddataConvertControl_0.OutGxObject = value;
            }
        }

        public IGxObjectFilter OutGxObjectFilter
        {
            set
            {
                this.caddataConvertControl_0.OutGxObjectFilter = value;
            }
        }
    }
}

