namespace JLK.Catalog.UI
{
    using ESRI.ArcGIS.GISClient;
    using ESRI.ArcGIS.Server;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class ServerLogPropertyPage : UserControl
    {
        private Button btnSetDir;
        private ComboBox comboBox1;
        [CompilerGenerated]
        private IAGSServerConnectionAdmin iagsserverConnectionAdmin_0;
        private IContainer icontainer_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBox1;
        private TextBox textBox2;

        public ServerLogPropertyPage()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.textBox1 = new TextBox();
            this.textBox2 = new TextBox();
            this.label4 = new Label();
            this.btnSetDir = new Button();
            this.comboBox1 = new ComboBox();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(20, 0x37);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "日志文件路径";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x16, 0x52);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x4d, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "日志保留时间";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x16, 0x70);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "日志级别";
            this.textBox1.Location = new Point(0x67, 0x34);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x12d, 0x15);
            this.textBox1.TabIndex = 3;
            this.textBox2.Location = new Point(0x67, 0x4f);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(0x7a, 0x15);
            this.textBox2.TabIndex = 4;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0xe9, 0x52);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x11, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "天";
            this.btnSetDir.Location = new Point(0x19b, 0x31);
            this.btnSetDir.Name = "btnSetDir";
            this.btnSetDir.Size = new Size(0x21, 0x17);
            this.btnSetDir.TabIndex = 6;
            this.btnSetDir.Text = "...";
            this.btnSetDir.UseVisualStyleBackColor = true;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] { "关", "严重", "警告", "信息", "精细", "详细", "调试" });
            this.comboBox1.Location = new Point(0x69, 0x6d);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0x79, 20);
            this.comboBox1.TabIndex = 7;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.comboBox1);
            base.Controls.Add(this.btnSetDir);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.textBox2);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "ServerLogPropertyPage";
            base.Size = new Size(0x1c7, 0x113);
            base.Load += new EventHandler(this.ServerLogPropertyPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void ServerLogPropertyPage_Load(object sender, EventArgs e)
        {
            IServerLog serverLog = (this.AGSServerConnectionAdmin.ServerObjectAdmin as IServerObjectAdmin8).ServerLog;
        }

        public IAGSServerConnectionAdmin AGSServerConnectionAdmin
        {
            [CompilerGenerated]
            get
            {
                return this.iagsserverConnectionAdmin_0;
            }
            [CompilerGenerated]
            set
            {
                this.iagsserverConnectionAdmin_0 = value;
            }
        }
    }
}

