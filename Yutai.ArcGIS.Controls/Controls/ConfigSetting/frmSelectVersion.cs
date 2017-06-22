using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Controls.ConfigSetting
{
    internal partial class frmSelectVersion : Form
    {
        private IList m_pList = new ArrayList();

        public frmSelectVersion()
        {
            this.InitializeComponent();
        }

        private void cboVersions_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_VersionName = this.cboVersions.Text;
            this.txtDescription.Text = this.m_pList[this.cboVersions.SelectedIndex].ToString();
        }

 private void frmSelectVersion_Load(object sender, EventArgs e)
        {
            this.InitControl();
        }

        private void InitControl()
        {
            if (this.m_pEnumVersionInfo != null)
            {
                this.m_pEnumVersionInfo.Reset();
                this.m_pList.Clear();
                for (IVersionInfo info = this.m_pEnumVersionInfo.Next(); info != null; info = this.m_pEnumVersionInfo.Next())
                {
                    this.cboVersions.Properties.Items.Add(info.VersionName);
                    this.m_pList.Add(info.Description);
                }
                if (this.cboVersions.Properties.Items.Count > 0)
                {
                    this.cboVersions.SelectedIndex = 0;
                }
            }
        }

 public IEnumVersionInfo EnumVersionInfo
        {
            set
            {
                this.m_pEnumVersionInfo = value;
            }
        }

        public string VersionName
        {
            get
            {
                return this.m_VersionName;
            }
        }
    }
}

