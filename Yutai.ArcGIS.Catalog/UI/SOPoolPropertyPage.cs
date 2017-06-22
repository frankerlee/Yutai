using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Server;

namespace Yutai.ArcGIS.Catalog.UI
{
    internal partial class SOPoolPropertyPage : UserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private IServerObjectConfiguration iserverObjectConfiguration_0 = null;

        public SOPoolPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
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
        }

        private void rdoIsPooled_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdoIsPooled.SelectedIndex == 0)
            {
                this.txtMaxInstances.Enabled = true;
                this.txtMinInstances.Enabled = true;
                this.txtMaxInstances1.Enabled = false;
                if (this.bool_0)
                {
                    try
                    {
                        this.iserverObjectConfiguration_0.MaxInstances = int.Parse(this.txtMaxInstances.Text);
                        this.iserverObjectConfiguration_0.MinInstances = int.Parse(this.txtMinInstances.Text);
                    }
                    catch
                    {
                    }
                }
            }
            else
            {
                this.txtMaxInstances1.Enabled = true;
                this.txtMaxInstances.Enabled = false;
                this.txtMinInstances.Enabled = false;
                if (this.bool_0)
                {
                    try
                    {
                        this.iserverObjectConfiguration_0.MinInstances = 0;
                        this.iserverObjectConfiguration_0.MaxInstances = int.Parse(this.txtMaxInstances1.Text);
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void SOPoolPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_0 = true;
        }

        private void txtMaxInstances_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.iserverObjectConfiguration_0.MaxInstances = int.Parse(this.txtMaxInstances.Text);
                this.iserverObjectConfiguration_0.MinInstances = int.Parse(this.txtMinInstances.Text);
                if (this.iserverObjectConfiguration_0.MinInstances > this.iserverObjectConfiguration_0.MaxInstances)
                {
                    this.txtMinInstances.Text = this.txtMaxInstances.Text;
                }
            }
            catch
            {
            }
        }

        private void txtMaxInstances1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.iserverObjectConfiguration_0.MinInstances = 0;
                this.iserverObjectConfiguration_0.MaxInstances = int.Parse(this.txtMaxInstances.Text);
            }
            catch
            {
            }
        }

        private void txtMaxUsageTime_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.iserverObjectConfiguration_0.UsageTimeout = int.Parse(this.txtMaxUsageTime.Text);
            }
            catch
            {
            }
        }

        private void txtMaxWaitTime_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.iserverObjectConfiguration_0.WaitTimeout = int.Parse(this.txtMaxWaitTime.Text);
            }
            catch
            {
            }
        }

        private void txtMinInstances_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.iserverObjectConfiguration_0.MaxInstances = int.Parse(this.txtMaxInstances.Text);
                this.iserverObjectConfiguration_0.MinInstances = int.Parse(this.txtMinInstances.Text);
                if (this.iserverObjectConfiguration_0.MinInstances > this.iserverObjectConfiguration_0.MaxInstances)
                {
                    this.txtMaxInstances.Text = this.txtMinInstances.Text;
                }
            }
            catch
            {
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
    }
}

