using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    public class frmBufferWizard : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnLast;
        private SimpleButton btnNext;
        private BufferGeometrySetCtrl bufferGeometrySetCtrl_0 = new BufferGeometrySetCtrl();
        private BufferOutputSetCtrl bufferOutputSetCtrl_0 = new BufferOutputSetCtrl();
        private BufferParameterSetCtrl bufferParameterSetCtrl_0 = new BufferParameterSetCtrl();
        private Container container_0 = null;
        private int int_0 = 0;
        private Panel panel1;

        public frmBufferWizard()
        {
            this.InitializeComponent();
            BufferHelper.m_BufferHelper = new BufferHelper();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    return;

                case 1:
                    this.bufferGeometrySetCtrl_0.Visible = true;
                    this.bufferParameterSetCtrl_0.Visible = false;
                    this.btnLast.Enabled = false;
                    break;

                case 2:
                    this.bufferParameterSetCtrl_0.Visible = true;
                    this.bufferOutputSetCtrl_0.Visible = false;
                    this.btnNext.Text = "下一步>";
                    return;
            }
            this.int_0--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    this.bufferGeometrySetCtrl_0.Visible = false;
                    this.bufferParameterSetCtrl_0.Visible = true;
                    this.bufferParameterSetCtrl_0.Init();
                    this.btnLast.Enabled = true;
                    break;

                case 1:
                    this.bufferParameterSetCtrl_0.Visible = false;
                    this.bufferOutputSetCtrl_0.Visible = true;
                    this.bufferOutputSetCtrl_0.Init();
                    this.btnNext.Text = "完成";
                    break;

                case 2:
                    BufferHelper.m_BufferHelper.Do();
                    return;
            }
            this.int_0++;
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmBufferWizard_Load(object sender, EventArgs e)
        {
            this.bufferGeometrySetCtrl_0.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.bufferGeometrySetCtrl_0);
            this.bufferParameterSetCtrl_0.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.bufferParameterSetCtrl_0);
            this.bufferParameterSetCtrl_0.Visible = false;
            this.bufferOutputSetCtrl_0.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.bufferOutputSetCtrl_0);
            this.bufferOutputSetCtrl_0.Visible = false;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBufferWizard));
            this.btnNext = new SimpleButton();
            this.btnLast = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.panel1 = new Panel();
            base.SuspendLayout();
            this.btnNext.Location = new Point(400, 0x150);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x40, 0x18);
            this.btnNext.TabIndex = 11;
            this.btnNext.Text = "下一步>";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnLast.Enabled = false;
            this.btnLast.Location = new Point(0x148, 0x150);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(0x40, 0x18);
            this.btnLast.TabIndex = 9;
            this.btnLast.Text = "<上一步";
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x1d8, 0x150);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取消";
            this.panel1.Location = new Point(8, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x218, 0x138);
            this.panel1.TabIndex = 13;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x228, 0x175);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.btnLast);
            base.Controls.Add(this.btnCancel);
            base.Icon = ((System.Drawing.Icon)resources.GetObject("$Icon"));
            base.Name = "frmBufferWizard";
            this.Text = "缓冲区向导";
            base.Load += new EventHandler(this.frmBufferWizard_Load);
            base.ResumeLayout(false);
        }

        public IMap FocusMap
        {
            set
            {
                BufferHelper.m_BufferHelper.m_pFocusMap = value;
            }
        }
    }
}

