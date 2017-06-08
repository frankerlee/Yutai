namespace JLK.Catalog
{
    using ESRI.ArcGIS.DataSourcesGDB;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmAddDatabaseServer : Form
    {
        private Button button1;
        private Button button2;
        private IContainer icontainer_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBox1;

        public frmAddDatabaseServer()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                IDataServerManager manager = new DataServerManagerClass {
                    ServerName = this.textBox1.Text.Trim()
                };
                manager.Connect();
                string pathName = Environment.SystemDirectory.Substring(0, 2) + @"\Documents and Settings\Administrator\Application Data\ESRI\ArcCatalog\";
                manager.CreateConnectionFile(pathName, manager.ServerName);
                base.DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
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
            this.button1 = new Button();
            this.label2 = new Label();
            this.textBox1 = new TextBox();
            this.label3 = new Label();
            this.button2 = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 0x22);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x6b, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "空间数据库服务器:";
            this.button1.Enabled = false;
            this.button1.FlatStyle = FlatStyle.Popup;
            this.button1.Location = new Point(0x5c, 0x70);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 1;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 0x48);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "示例:";
            this.textBox1.Location = new Point(0x7d, 0x1f);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x83, 0x15);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x7b, 0x48);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x77, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = @"myserver\sqlexpress";
            this.button2.DialogResult = DialogResult.Cancel;
            this.button2.FlatStyle = FlatStyle.Popup;
            this.button2.Location = new Point(0xcd, 0x70);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 5;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x124, 0xb0);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.label1);
            base.Name = "frmAddDatabaseServer";
            this.Text = "添加空间数据库服务器";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.button1.Enabled = this.textBox1.Text.Trim().Length > 0;
        }
    }
}

