using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class frmBufferWizard : Form
    {
        private BufferGeometrySetCtrl bufferGeometrySetCtrl_0 = new BufferGeometrySetCtrl();
        private BufferOutputSetCtrl bufferOutputSetCtrl_0 = new BufferOutputSetCtrl();
        private BufferParameterSetCtrl bufferParameterSetCtrl_0 = new BufferParameterSetCtrl();
        private Container container_0 = null;
        private int int_0 = 0;

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

        public IMap FocusMap
        {
            set { BufferHelper.m_BufferHelper.m_pFocusMap = value; }
        }
    }
}