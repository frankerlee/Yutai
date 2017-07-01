using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Server;

namespace Yutai.ArcGIS.Catalog.UI
{
    internal partial class SOProcessManagementPropertyPage : UserControl
    {
        private Container container_0 = null;
        private IServerObjectConfiguration iserverObjectConfiguration_0 = null;

        public SOProcessManagementPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            int num = 36000;
            try
            {
                num = (int) (double.Parse(this.txtRecycleInterval.Text)*3600.0);
            }
            catch
            {
            }
            IPropertySet recycleProperties = this.iserverObjectConfiguration_0.RecycleProperties;
            recycleProperties.SetProperty("Interval", num.ToString());
            string str = this.timeEdit1.Time.Hour.ToString("00") + "." + this.timeEdit1.Time.Minute.ToString("00");
            recycleProperties.SetProperty("Start", str);
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
                this.txtRecycleInterval.Text = ((double) (num/3600)).ToString();
                this.timeEdit1.Text =
                    this.iserverObjectConfiguration_0.RecycleProperties.GetProperty("Start").ToString();
            }
            catch
            {
            }
        }

        private void SOProcessManagementPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        public IServerObjectConfiguration ServerObjectConfiguration
        {
            get { return this.iserverObjectConfiguration_0; }
            set { this.iserverObjectConfiguration_0 = value; }
        }
    }
}