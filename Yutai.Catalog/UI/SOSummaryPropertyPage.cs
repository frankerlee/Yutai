namespace Yutai.Catalog.UI
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Server;
    
    
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class SOSummaryPropertyPage : UserControl
    {
        private Container container_0 = null;
        private IServerObjectConfiguration iserverObjectConfiguration_0 = null;
        private Label label1;
        private Label label2;
        private TextBox memoEdit1;
        private ComboBox radioGroup1;

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

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.memoEdit1 = new TextBox();
            this.radioGroup1 = new ComboBox();
            this.label2 = new Label();
          
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x99, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "将创建以下server objects";
            this.memoEdit1.Text="";
            this.memoEdit1.Location = new Point(0x18, 0x30);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Size = new Size(0xe8, 0x80);
            this.memoEdit1.TabIndex = 1;
            this.radioGroup1.Location = new Point(0x20, 0xd8);
            this.radioGroup1.Name = "radioGroup1";
           
            this.radioGroup1.Items.AddRange(new object[] {  "否，以后再启动server object",  "是，现在启动server object" });
            this.radioGroup1.Size = new Size(0xb8, 0x38);
            this.radioGroup1.TabIndex = 2;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x18, 0xc0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0xab, 0x11);
            this.label2.TabIndex = 3;
            this.label2.Text = "是否现在启动server objects?";
            base.Controls.Add(this.label2);
            base.Controls.Add(this.radioGroup1);
            base.Controls.Add(this.memoEdit1);
            base.Controls.Add(this.label1);
            base.Name = "SOSummaryPropertyPage";
            base.Size = new Size(0x130, 0x120);
            base.Load += new EventHandler(this.SOSummaryPropertyPage_Load);
          
            base.ResumeLayout(false);
        }

        private void SOSummaryPropertyPage_Load(object sender, EventArgs e)
        {
            if (this.iserverObjectConfiguration_0 != null)
            {
                object obj2;
                object obj3;
                string str = "常规:\r\n";
                str = ((str + "    名字:" + this.iserverObjectConfiguration_0.Name + "\r\n") + "    类型:" + this.iserverObjectConfiguration_0.TypeName + "\r\n") + "    描述:" + this.iserverObjectConfiguration_0.Description + "\r\n";
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
                    str = ((str + "    是否缓冲:是\r\n") + "    最大实例数:" + this.iserverObjectConfiguration_0.MaxInstances.ToString() + "\r\n") + "    最小实例数:" + this.iserverObjectConfiguration_0.MinInstances.ToString() + "\r\n";
                }
                else
                {
                    str = (str + "    是否缓冲:否\r\n") + "    最大实例数:" + this.iserverObjectConfiguration_0.MaxInstances.ToString() + "\r\n";
                }
                str = ((str + "    最大使用时间:" + this.iserverObjectConfiguration_0.UsageTimeout.ToString() + "\r\n") + "    最大等待时间:" + this.iserverObjectConfiguration_0.WaitTimeout.ToString() + "\r\n") + "\r\n" + "进程:\r\n";
                IPropertySet recycleProperties = this.iserverObjectConfiguration_0.RecycleProperties;
                try
                {
                    if (this.iserverObjectConfiguration_0.IsolationLevel == esriServerIsolationLevel.esriServerIsolationHigh)
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
            get
            {
                return (this.radioGroup1.SelectedIndex == 1);
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

