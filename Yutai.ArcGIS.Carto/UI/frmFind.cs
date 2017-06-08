using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class frmFind : Form, IDockContent
    {
        private SimpleButton btnCancel;
        private SimpleButton btnFind;
        private SimpleButton btnStop;
        private FindControl findControl_0 = new FindControl();
        private IApplication iapplication_0 = null;
        private IContainer icontainer_0 = null;
        private Panel panel1;

        public frmFind()
        {
            this.InitializeComponent();
            this.findControl_0.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.findControl_0);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            IArray array = this.findControl_0.Find();
            this.btnFind.Enabled = false;
            this.btnStop.Enabled = true;
            frmFindResult result = new frmFindResult {
                Text = "在" + this.findControl_0.m_strFindLayers + "的" + this.findControl_0.m_strFindField + "中查找" + this.findControl_0.m_strSearch + "的结果",
                FindResults = array,
                ActiveView = this.iapplication_0.ActiveView
            };
            this.iapplication_0.DockWindows(result, null);
            this.btnFind.Enabled = true;
            this.btnStop.Enabled = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.btnFind.Enabled = true;
            this.btnStop.Enabled = false;
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            this.panel1 = new Panel();
            this.btnFind = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.btnStop = new SimpleButton();
            base.SuspendLayout();
            this.panel1.Dock = DockStyle.Left;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1d0, 0x8d);
            this.panel1.TabIndex = 0;
            this.btnFind.Location = new Point(0x1d8, 8);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new Size(0x38, 0x18);
            this.btnFind.TabIndex = 1;
            this.btnFind.Text = "查找";
            this.btnFind.Click += new EventHandler(this.btnFind_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x1d8, 0x48);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x38, 0x18);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.btnStop.Enabled = false;
            this.btnStop.Location = new Point(0x1d8, 40);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new Size(0x38, 0x18);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "停止";
            this.btnStop.Click += new EventHandler(this.btnStop_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x220, 0x8d);
            base.Controls.Add(this.btnStop);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnFind);
            base.Controls.Add(this.panel1);
            base.Name = "frmFind";
            this.Text = "查找";
            base.ResumeLayout(false);
        }

        public IApplication Application
        {
            set
            {
                this.iapplication_0 = value;
                this.findControl_0.FocusMap = this.iapplication_0.FocusMap;
            }
        }

        public DockingStyle DefaultDockingStyle
        {
            get
            {
                return DockingStyle.Bottom;
            }
        }

        public IMap FocusMap
        {
            set
            {
                this.findControl_0.FocusMap = value;
            }
        }

        string IDockContent.Name
        {
            get
            {
                return base.Name;
            }
        }

        int IDockContent.Width
        {
            get
            {
                return base.Width;
            }
        }
    }
}

