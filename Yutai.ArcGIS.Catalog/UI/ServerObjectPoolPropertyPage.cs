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
    internal partial class ServerObjectPoolPropertyPage : UserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IAGSServerConnectionAdmin iagsserverConnectionAdmin_0 = null;
        private IServerObjectConfiguration iserverObjectConfiguration_0 = null;
        private string string_0 = "Started";

        public ServerObjectPoolPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                if (this.rdoIsPooled.SelectedIndex == 0)
                {
                    this.iserverObjectConfiguration_0.IsPooled = true;
                    this.iserverObjectConfiguration_0.MaxInstances = int.Parse(this.txtMaxInstances.Text);
                    this.iserverObjectConfiguration_0.MinInstances = int.Parse(this.txtMinInstances.Text);
                }
                else
                {
                    this.iserverObjectConfiguration_0.IsPooled = false;
                    this.iserverObjectConfiguration_0.MaxInstances = int.Parse(this.txtMaxInstances.Text);
                }
                this.iserverObjectConfiguration_0.WaitTimeout = int.Parse(this.txtMaxWaitTime.Text);
                this.iserverObjectConfiguration_0.UsageTimeout = int.Parse(this.txtMaxUsageTime.Text);
            }
        }

        private void method_0()
        {
            if (this.iserverObjectConfiguration_0.IsPooled)
            {
                this.rdoIsPooled.SelectedIndex = 0;
            }
            else
            {
                this.rdoIsPooled.SelectedIndex = 1;
            }
            this.txtMaxInstances.Text = this.iserverObjectConfiguration_0.MaxInstances.ToString();
            this.txtMaxInstances1.Text = this.iserverObjectConfiguration_0.MaxInstances.ToString();
            this.txtMinInstances.Text = this.iserverObjectConfiguration_0.MinInstances.ToString();
            this.txtMaxUsageTime.Text = this.iserverObjectConfiguration_0.UsageTimeout.ToString();
            this.txtMaxWaitTime.Text = this.iserverObjectConfiguration_0.WaitTimeout.ToString();
            if (this.string_0 == "Stoped")
            {
                this.rdoIsPooled.Enabled = true;
                this.txtMaxInstances.Properties.ReadOnly = false;
                this.txtMaxInstances1.Properties.ReadOnly = false;
                this.txtMinInstances.Properties.ReadOnly = false;
                this.txtMaxUsageTime.Properties.ReadOnly = false;
                this.txtMaxWaitTime.Properties.ReadOnly = false;
            }
            else
            {
                this.rdoIsPooled.Enabled = false;
                this.txtMaxInstances.Properties.ReadOnly = true;
                this.txtMaxInstances1.Properties.ReadOnly = true;
                this.txtMinInstances.Properties.ReadOnly = true;
                this.txtMaxUsageTime.Properties.ReadOnly = true;
                this.txtMaxWaitTime.Properties.ReadOnly = true;
            }
        }

        private void rdoIsPooled_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdoIsPooled.SelectedIndex == 0)
            {
                this.txtMaxInstances.Enabled = true;
                this.txtMinInstances.Enabled = true;
                this.txtMaxInstances1.Enabled = false;
            }
            else
            {
                this.txtMaxInstances1.Enabled = true;
                this.txtMaxInstances.Enabled = false;
                this.txtMinInstances.Enabled = false;
            }
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        private void ServerObjectPoolPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_1 = true;
        }

        private void txtMaxInstances_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        private void txtMaxInstances1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        private void txtMaxUsageTime_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        private void txtMaxWaitTime_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        private void txtMinInstances_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        public IAGSServerConnectionAdmin AGSConnectionAdmin
        {
            set { this.iagsserverConnectionAdmin_0 = value; }
        }

        public IServerObjectConfiguration ServerObjectConfiguration
        {
            get { return this.iserverObjectConfiguration_0; }
            set { this.iserverObjectConfiguration_0 = value; }
        }

        public string Status
        {
            set { this.string_0 = value; }
        }
    }
}