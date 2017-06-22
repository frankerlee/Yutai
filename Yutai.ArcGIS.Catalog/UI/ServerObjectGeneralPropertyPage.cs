using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;

namespace Yutai.ArcGIS.Catalog.UI
{
    internal partial class ServerObjectGeneralPropertyPage : UserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IAGSServerConnectionAdmin iagsserverConnectionAdmin_0 = null;
        private IServerObjectConfiguration iserverObjectConfiguration_0 = null;
        private string string_0 = "Started";
        private string string_1 = "";
        private string string_2 = "";

        public ServerObjectGeneralPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                if (this.iserverObjectConfiguration_0 != null)
                {
                    this.iserverObjectConfiguration_0.Name = this.txtSOName.Text;
                    this.iserverObjectConfiguration_0.TypeName = this.cboSOType.Text;
                    this.iserverObjectConfiguration_0.Description = this.txtDescription.Text;
                    this.iserverObjectConfiguration_0.StartupType = (esriStartupType) this.cboStartupType.SelectedIndex;
                }
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (this.iagsserverConnectionAdmin_0 != null)
            {
                this.iagsserverConnectionAdmin_0.ServerObjectAdmin.PauseConfiguration(this.iserverObjectConfiguration_0.Name, this.iserverObjectConfiguration_0.TypeName);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (this.iagsserverConnectionAdmin_0 != null)
            {
                this.iagsserverConnectionAdmin_0.ServerObjectAdmin.StartConfiguration(this.iserverObjectConfiguration_0.Name, this.iserverObjectConfiguration_0.TypeName);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (this.iagsserverConnectionAdmin_0 != null)
            {
                this.iagsserverConnectionAdmin_0.ServerObjectAdmin.StopConfiguration(this.iserverObjectConfiguration_0.Name, this.iserverObjectConfiguration_0.TypeName);
            }
        }

        private void cboSOType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

        private void cboStartupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

 private void method_0()
        {
            if (this.iserverObjectConfiguration_0 != null)
            {
                this.txtSOName.Text = this.iserverObjectConfiguration_0.Name;
                this.cboSOType.Text = this.iserverObjectConfiguration_0.TypeName;
                this.txtDescription.Text = this.iserverObjectConfiguration_0.Description;
                this.txtStatus.Text = this.string_0;
                this.cboStartupType.SelectedIndex = (int) this.iserverObjectConfiguration_0.StartupType;
                if (this.string_0 == "Stopped")
                {
                    this.txtSOName.Properties.ReadOnly = false;
                    this.txtDescription.Properties.ReadOnly = false;
                    this.cboSOType.Enabled = true;
                    this.btnStart.Enabled = true;
                    this.btnPause.Enabled = false;
                    this.btnStop.Enabled = false;
                    this.string_0 = "停止";
                }
                else
                {
                    this.txtSOName.Properties.ReadOnly = true;
                    this.txtDescription.Properties.ReadOnly = true;
                    this.cboSOType.Enabled = false;
                    if (this.string_0 == "Paused")
                    {
                        this.btnStart.Enabled = true;
                        this.btnPause.Enabled = false;
                        this.btnStop.Enabled = false;
                        this.string_0 = "暂停";
                    }
                    else if (this.string_0 == "Started")
                    {
                        this.btnStart.Enabled = false;
                        this.btnPause.Enabled = true;
                        this.btnStop.Enabled = true;
                        this.string_0 = "启动";
                    }
                    else
                    {
                        this.btnStart.Enabled = false;
                        this.btnPause.Enabled = false;
                        this.btnStop.Enabled = false;
                        if (this.string_0 == "Starting")
                        {
                            this.string_0 = "启动...";
                        }
                        else if (this.string_0 == "Stopting")
                        {
                            this.string_0 = "停止...";
                        }
                        else if (this.string_0 == "Deleted")
                        {
                            this.string_0 = "删除";
                        }
                    }
                }
            }
        }

        private void ServerObjectGeneralPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_0 = true;
        }

        private void txtDescription_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

        private void txtSOName_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.string_1 = this.txtSOName.Text;
                this.bool_1 = true;
            }
        }

        public IAGSServerConnectionAdmin AGSConnectionAdmin
        {
            set
            {
                this.iagsserverConnectionAdmin_0 = value;
            }
        }

        public IServerObjectConfiguration ServerObjectConfiguration
        {
            get
            {
                return this.iserverObjectConfiguration_0;
            }
            set
            {
                this.iserverObjectConfiguration_0 = value;
            }
        }

        public string SOName
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public string SOType
        {
            get
            {
                return this.string_2;
            }
            set
            {
                this.string_2 = value;
            }
        }

        public string Status
        {
            set
            {
                this.string_0 = value;
            }
        }
    }
}

