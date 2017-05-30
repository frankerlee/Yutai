namespace Yutai.Catalog.UI
{
    using ESRI.ArcGIS.GISClient;
    using ESRI.ArcGIS.Server;
    
    
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class ServerObjectGeneralPropertyPage : UserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Button btnPause;
        private Button btnStart;
        private Button btnStop;
        private ComboBox cboSOType;
        private ComboBox cboStartupType;
        private Container container_0 = null;
        private IAGSServerConnectionAdmin iagsserverConnectionAdmin_0 = null;
        private IServerObjectConfiguration iserverObjectConfiguration_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private string string_0 = "Started";
        private string string_1 = "";
        private string string_2 = "";
        private TextBox txtDescription;
        private Button txtSOName;
        private Button txtStatus;

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

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void InitializeComponent()
        {
            this.label2 = new Label();
            this.label1 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.txtSOName = new Button();
            this.txtDescription = new TextBox();
            this.txtStatus = new Button();
            this.label5 = new Label();
            this.btnStart = new Button();
            this.btnStop = new Button();
            this.btnPause = new Button();
            this.cboSOType = new ComboBox();
            this.cboStartupType = new ComboBox();
        
            base.SuspendLayout();
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 0x11);
            this.label2.TabIndex = 2;
            this.label2.Text = "类型:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 0x11);
            this.label1.TabIndex = 3;
            this.label1.Text = "名字:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 0x4a);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 0x11);
            this.label3.TabIndex = 4;
            this.label3.Text = "描述:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 0xa3);
            this.label4.Name = "label4";
            this.label4.Size = new Size(60, 0x11);
            this.label4.TabIndex = 5;
            this.label4.Text = "启动类型:";
            this.txtSOName.Text="";
            this.txtSOName.Location = new Point(0x48, 8);
            this.txtSOName.Name = "txtSOName";
            this.txtSOName.Size = new Size(0x98, 0x17);
            this.txtSOName.TabIndex = 6;
            this.txtSOName.TextChanged += new EventHandler(this.txtSOName_EditValueChanged);
            this.txtDescription.Text="";
            this.txtDescription.Location = new Point(0x48, 0x48);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Size(0x98, 80);
            this.txtDescription.TabIndex = 8;
            this.txtDescription.TextChanged += new EventHandler(this.txtDescription_EditValueChanged);
            this.txtStatus.Location = new Point(0x48, 0xc0);
            this.txtStatus.Name = "txtStatus";
           
            this.txtStatus.Size = new Size(0x90, 0x17);
            this.txtStatus.TabIndex = 0;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x10, 200);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 0x11);
            this.label5.TabIndex = 1;
            this.label5.Text = "状态";
            this.btnStart.Location = new Point(40, 0xe0);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new Size(0x30, 0x18);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "开始";
            this.btnStart.Click += new EventHandler(this.btnStart_Click);
            this.btnStop.Location = new Point(0x68, 0xe0);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new Size(0x30, 0x18);
            this.btnStop.TabIndex = 11;
            this.btnStop.Text = "停止";
            this.btnStop.Click += new EventHandler(this.btnStop_Click);
            this.btnPause.Location = new Point(0xb0, 0xe0);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new Size(0x30, 0x18);
            this.btnPause.TabIndex = 12;
            this.btnPause.Text = "暂停";
            this.btnPause.Click += new EventHandler(this.btnPause_Click);
            this.cboSOType.Text = "MapServer";
            this.cboSOType.Location = new Point(0x48, 40);
            this.cboSOType.Name = "cboSOType";
           
            this.cboSOType.Items.AddRange(new object[] { "MapServer" });
            this.cboSOType.Size = new Size(0x98, 0x17);
            this.cboSOType.TabIndex = 13;
            this.cboSOType.SelectedIndexChanged += new EventHandler(this.cboSOType_SelectedIndexChanged);
            this.cboStartupType.Text = "自动";
            this.cboStartupType.Location = new Point(0x48, 160);
            this.cboStartupType.Name = "cboStartupType";
          
            this.cboStartupType.Items.AddRange(new object[] { "自动", "手动" });
            this.cboStartupType.Size = new Size(0x98, 0x17);
            this.cboStartupType.TabIndex = 15;
            this.cboStartupType.SelectedIndexChanged += new EventHandler(this.cboStartupType_SelectedIndexChanged);
            base.Controls.Add(this.cboStartupType);
            base.Controls.Add(this.cboSOType);
            base.Controls.Add(this.btnPause);
            base.Controls.Add(this.btnStop);
            base.Controls.Add(this.btnStart);
            base.Controls.Add(this.txtStatus);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.txtDescription);
            base.Controls.Add(this.txtSOName);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.label2);
            base.Name = "ServerObjectGeneralPropertyPage";
            base.Size = new Size(0x130, 280);
            base.Load += new EventHandler(this.ServerObjectGeneralPropertyPage_Load);
          
            base.ResumeLayout(false);
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
                    this.txtSOName.Enabled = true;
                    this.txtDescription.Enabled = true;
                    this.cboSOType.Enabled = true;
                    this.btnStart.Enabled = true;
                    this.btnPause.Enabled = false;
                    this.btnStop.Enabled = false;
                    this.string_0 = "停止";
                }
                else
                {
                    this.txtSOName.Enabled = false;
                    this.txtDescription.Enabled = false;
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

