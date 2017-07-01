using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Server;

namespace Yutai.ArcGIS.Catalog.UI
{
    internal partial class SOSummaryPropertyPage : UserControl
    {
        private Container container_0 = null;
        private IServerObjectConfiguration iserverObjectConfiguration_0 = null;

        public SOSummaryPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.iserverObjectConfiguration_0 == null)
            {
            }
        }

        private void SOSummaryPropertyPage_Load(object sender, EventArgs e)
        {
            if (this.iserverObjectConfiguration_0 != null)
            {
                object obj2;
                object obj3;
                string str = "常规:\r\n";
                str = ((str + "    名字:" + this.iserverObjectConfiguration_0.Name + "\r\n") + "    类型:" +
                       this.iserverObjectConfiguration_0.TypeName + "\r\n") + "    描述:" +
                      this.iserverObjectConfiguration_0.Description + "\r\n";
                if (this.iserverObjectConfiguration_0.StartupType == esriStartupType.esriSTAutomatic)
                {
                    str = str + "    启动类型:自动\r\n";
                }
                else
                {
                    str = str + "    启动类型:手动\r\n";
                }
                str = str + "\r\n" + "参数:\r\n";
                this.iserverObjectConfiguration_0.Properties.GetAllProperties(out obj2, out obj3);
                System.Array array = obj2 as System.Array;
                System.Array array2 = obj3 as System.Array;
                for (int i = 0; i < array.Length; i++)
                {
                    str = str + "    " + array.GetValue(i).ToString() + ":" + array2.GetValue(i).ToString() + "\r\n";
                }
                str = str + "\r\n" + "缓冲:\r\n";
                if (this.iserverObjectConfiguration_0.IsPooled)
                {
                    str = ((str + "    是否缓冲:是\r\n") + "    最大实例数:" +
                           this.iserverObjectConfiguration_0.MaxInstances.ToString() + "\r\n") + "    最小实例数:" +
                          this.iserverObjectConfiguration_0.MinInstances.ToString() + "\r\n";
                }
                else
                {
                    str = (str + "    是否缓冲:否\r\n") + "    最大实例数:" +
                          this.iserverObjectConfiguration_0.MaxInstances.ToString() + "\r\n";
                }
                str = ((str + "    最大使用时间:" + this.iserverObjectConfiguration_0.UsageTimeout.ToString() + "\r\n") +
                       "    最大等待时间:" + this.iserverObjectConfiguration_0.WaitTimeout.ToString() + "\r\n") + "\r\n" +
                      "进程:\r\n";
                IPropertySet recycleProperties = this.iserverObjectConfiguration_0.RecycleProperties;
                try
                {
                    if (this.iserverObjectConfiguration_0.IsolationLevel ==
                        esriServerIsolationLevel.esriServerIsolationHigh)
                    {
                        str = str + "    孤立性:高孤立性\r\n";
                    }
                    else
                    {
                        str = str + "    孤立性:低孤立性\r\n";
                    }
                    str = str + "    回收进程时间间隔:" + recycleProperties.GetProperty("Interval").ToString() + "秒\r\n";
                    str = str + "    回收开始时间:" + recycleProperties.GetProperty("Start").ToString() + "\r\n";
                }
                catch
                {
                }
                this.memoEdit1.Text = str;
            }
        }

        public bool isStart
        {
            get { return (this.radioGroup1.SelectedIndex == 1); }
        }

        public IServerObjectConfiguration ServerObjectConfiguration
        {
            get { return this.iserverObjectConfiguration_0; }
            set { this.iserverObjectConfiguration_0 = value; }
        }
    }
}