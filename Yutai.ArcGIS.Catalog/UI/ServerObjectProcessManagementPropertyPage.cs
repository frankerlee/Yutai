using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;

namespace Yutai.ArcGIS.Catalog.UI
{
    internal partial class ServerObjectProcessManagementPropertyPage : UserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IAGSServerConnectionAdmin iagsserverConnectionAdmin_0 = null;
        private IServerObjectConfiguration iserverObjectConfiguration_0 = null;
        private string string_0 = "Started";

        public ServerObjectProcessManagementPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                int num = 36000;
                try
                {
                    num = (int) (double.Parse(this.txtRecycleInterval.Text) * 3600.0);
                }
                catch
                {
                }
                IPropertySet recycleProperties = this.iserverObjectConfiguration_0.RecycleProperties;
                recycleProperties.SetProperty("Interval", num.ToString());
                string str = this.timeEdit1.Time.Hour.ToString("00") + this.timeEdit1.Time.Minute.ToString(".00");
                recycleProperties.SetProperty("Start", str);
            }
        }

        private void cboIsolationLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

 private void method_0()
        {
            object obj2;
            object obj3;
            this.cboIsolationLevel.SelectedIndex = (int) this.iserverObjectConfiguration_0.IsolationLevel;
            IPropertySet recycleProperties = this.iserverObjectConfiguration_0.RecycleProperties;
            recycleProperties.GetAllProperties(out obj2, out obj3);
            try
            {
                int num = int.Parse(recycleProperties.GetProperty("Interval").ToString());
                this.txtRecycleInterval.Text = ((double) (num / 3600)).ToString();
                this.timeEdit1.Text = this.iserverObjectConfiguration_0.RecycleProperties.GetProperty("Start").ToString();
            }
            catch
            {
            }
            if (this.string_0 == "Stoped")
            {
                this.cboIsolationLevel.Enabled = false;
                this.txtRecycleInterval.Properties.ReadOnly = true;
                this.timeEdit1.Properties.ReadOnly = true;
            }
            else
            {
                this.cboIsolationLevel.Enabled = true;
                this.txtRecycleInterval.Properties.ReadOnly = false;
                this.timeEdit1.Properties.ReadOnly = false;
            }
        }

        private void ServerObjectProcessManagementPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_0 = true;
        }

        private void timeEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

        private void txtRecycleInterval_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
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

        public string Status
        {
            set
            {
                this.string_0 = value;
            }
        }
    }
}

