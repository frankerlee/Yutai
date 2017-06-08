namespace JLK.Catalog.UI
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.GISClient;
    using JLK.Editors;
    using JLK.Editors.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class AGSGeneralPropertyPage : UserControl
    {
        private bool bool_0 = false;
        private ComboBoxEdit cboLogLevel;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private IAGSServerConnectionAdmin iagsserverConnectionAdmin_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextEdit txtLogPath;
        private TextEdit txtLogSize;
        private TextEdit txtTimeOut;

        public AGSGeneralPropertyPage()
        {
            this.InitializeComponent();
        }

        private void AGSGeneralPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        public void Apply()
        {
            IPropertySet properties = this.iagsserverConnectionAdmin_0.ServerObjectAdmin.Properties;
            try
            {
                int num = int.Parse(this.txtTimeOut.Text.Trim());
                properties.SetProperty("ConfigurationStartTimeout", num);
            }
            catch
            {
            }
            properties.SetProperty("LogPath", this.txtLogPath.Text);
            try
            {
                int num2 = int.Parse(this.txtLogSize.Text);
                properties.SetProperty("LogSize", num2);
            }
            catch
            {
            }
            properties.SetProperty("LogLevel", this.cboLogLevel.SelectedIndex);
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.groupBox1 = new GroupBox();
            this.txtLogSize = new TextEdit();
            this.txtLogPath = new TextEdit();
            this.cboLogLevel = new ComboBoxEdit();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.txtTimeOut = new TextEdit();
            this.groupBox1.SuspendLayout();
            this.txtLogSize.Properties.BeginInit();
            this.txtLogPath.Properties.BeginInit();
            this.cboLogLevel.Properties.BeginInit();
            this.txtTimeOut.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x74, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器对象启动超时";
            this.groupBox1.Controls.Add(this.txtLogSize);
            this.groupBox1.Controls.Add(this.txtLogPath);
            this.groupBox1.Controls.Add(this.cboLogLevel);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new Point(0x10, 0x40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x138, 0x98);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "服务器日志属性";
            this.txtLogSize.EditValue = "";
            this.txtLogSize.Location = new Point(0x70, 0x38);
            this.txtLogSize.Name = "txtLogSize";
            this.txtLogSize.Size = new Size(120, 0x17);
            this.txtLogSize.TabIndex = 6;
            this.txtLogPath.EditValue = "";
            this.txtLogPath.Location = new Point(0x70, 0x18);
            this.txtLogPath.Name = "txtLogPath";
            this.txtLogPath.Size = new Size(160, 0x17);
            this.txtLogPath.TabIndex = 5;
            this.cboLogLevel.EditValue = "3";
            this.cboLogLevel.Location = new Point(0x70, 0x58);
            this.cboLogLevel.Name = "cboLogLevel";
            this.cboLogLevel.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboLogLevel.Properties.Items.AddRange(new object[] { "0", "1", "2", "3", "4", "5" });
            this.cboLogLevel.Size = new Size(0x48, 0x17);
            this.cboLogLevel.TabIndex = 4;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x10, 0x58);
            this.label4.Name = "label4";
            this.label4.Size = new Size(60, 0x11);
            this.label4.TabIndex = 3;
            this.label4.Text = "日志级别:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x10, 0x38);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x55, 0x11);
            this.label3.TabIndex = 2;
            this.label3.Text = "日志文件大小:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x18);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x55, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "日志文件路径:";
            this.txtTimeOut.EditValue = "";
            this.txtTimeOut.Location = new Point(0x88, 0x10);
            this.txtTimeOut.Name = "txtTimeOut";
            this.txtTimeOut.Size = new Size(120, 0x17);
            this.txtTimeOut.TabIndex = 2;
            base.Controls.Add(this.txtTimeOut);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label1);
            base.Name = "AGSGeneralPropertyPage";
            base.Size = new Size(360, 0xf8);
            base.Load += new EventHandler(this.AGSGeneralPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtLogSize.Properties.EndInit();
            this.txtLogPath.Properties.EndInit();
            this.cboLogLevel.Properties.EndInit();
            this.txtTimeOut.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void method_0()
        {
            object obj2;
            object obj3;
            IPropertySet properties = this.iagsserverConnectionAdmin_0.ServerObjectAdmin.Properties;
            properties.GetAllProperties(out obj2, out obj3);
            this.txtTimeOut.Text = properties.GetProperty("ConfigurationStartTimeout").ToString();
            this.txtLogPath.Text = properties.GetProperty("LogPath").ToString();
            this.txtLogSize.Text = properties.GetProperty("LogSize").ToString();
            this.cboLogLevel.SelectedIndex = (int) properties.GetProperty("LogLevel");
        }

        public IAGSServerConnectionAdmin AGSServerConnectionAdmin
        {
            set
            {
                this.iagsserverConnectionAdmin_0 = value;
            }
        }
    }
}

