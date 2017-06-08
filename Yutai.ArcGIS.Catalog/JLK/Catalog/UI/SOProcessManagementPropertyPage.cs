namespace JLK.Catalog.UI
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Server;
    using JLK.Editors;
    using JLK.Editors.Controls;
    using JLK.Utils;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class SOProcessManagementPropertyPage : UserControl
    {
        private ComboBoxEdit cboIsolationLevel;
        private Container container_0 = null;
        private IServerObjectConfiguration iserverObjectConfiguration_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TimeEdit timeEdit1;
        private TextEdit txtRecycleInterval;

        public SOProcessManagementPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            int num = 0x8ca0;
            try
            {
                num = (int) (double.Parse(this.txtRecycleInterval.Text) * 3600.0);
            }
            catch
            {
            }
            IPropertySet recycleProperties = this.iserverObjectConfiguration_0.RecycleProperties;
            recycleProperties.SetProperty("Interval", num.ToString());
            string str = this.timeEdit1.Time.Hour.ToString("00") + "." + this.timeEdit1.Time.Minute.ToString("00");
            recycleProperties.SetProperty("Start", str);
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
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.cboIsolationLevel = new ComboBoxEdit();
            this.label5 = new Label();
            this.txtRecycleInterval = new TextEdit();
            this.timeEdit1 = new TimeEdit();
            this.cboIsolationLevel.Properties.BeginInit();
            this.txtRecycleInterval.Properties.BeginInit();
            this.timeEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4f, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "配置实例数目";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x68);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1c8, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "回收进程每隔一段时间将停止所有的进程并且把它们重新启动,以提高性能和稳定性";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x18, 0x90);
            this.label3.Name = "label3";
            this.label3.Size = new Size(110, 0x11);
            this.label3.TabIndex = 2;
            this.label3.Text = "回收进程时间间隔:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x18, 0xb8);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x23, 0x11);
            this.label4.TabIndex = 3;
            this.label4.Text = "开始:";
            this.cboIsolationLevel.EditValue = "每一个实例一个进程(高孤立性)";
            this.cboIsolationLevel.Location = new Point(0x18, 0x30);
            this.cboIsolationLevel.Name = "cboIsolationLevel";
            this.cboIsolationLevel.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboIsolationLevel.Properties.Items.AddRange(new object[] { "每一个实例一个进程(高孤立性)", "仅一个进程(低孤立性)" });
            this.cboIsolationLevel.Size = new Size(0x150, 0x17);
            this.cboIsolationLevel.TabIndex = 4;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x100, 0x90);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 0x11);
            this.label5.TabIndex = 5;
            this.label5.Text = "小时";
            this.txtRecycleInterval.EditValue = "10";
            this.txtRecycleInterval.Location = new Point(0x88, 0x88);
            this.txtRecycleInterval.Name = "txtRecycleInterval";
            this.txtRecycleInterval.Size = new Size(0x60, 0x17);
            this.txtRecycleInterval.TabIndex = 6;
            this.timeEdit1.EditValue = new DateTime(0x7d6, 1, 0x1b, 0x17, 5, 0, 0);
            this.timeEdit1.Location = new Point(0x88, 0xb8);
            this.timeEdit1.Name = "timeEdit1";
            this.timeEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.timeEdit1.Properties.DisplayFormat.FormatString = "t";
            this.timeEdit1.Properties.DisplayFormat.FormatType = FormatType.Custom;
            this.timeEdit1.Properties.EditFormat.FormatString = "h.m";
            this.timeEdit1.Size = new Size(120, 0x17);
            this.timeEdit1.TabIndex = 7;
            base.Controls.Add(this.timeEdit1);
            base.Controls.Add(this.txtRecycleInterval);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.cboIsolationLevel);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "SOProcessManagementPropertyPage";
            base.Size = new Size(0x1f0, 0x100);
            base.Load += new EventHandler(this.SOProcessManagementPropertyPage_Load);
            this.cboIsolationLevel.Properties.EndInit();
            this.txtRecycleInterval.Properties.EndInit();
            this.timeEdit1.Properties.EndInit();
            base.ResumeLayout(false);
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
                this.txtRecycleInterval.Text = ((double) (num / 0xe10)).ToString();
                this.timeEdit1.Text = this.iserverObjectConfiguration_0.RecycleProperties.GetProperty("Start").ToString();
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

