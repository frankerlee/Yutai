namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Editors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    internal class GDBGeneralCtrl : UserControl
    {
        private SimpleButton btnConfigKey;
        private SimpleButton btnProperty;
        private SimpleButton btnUnRegister;
        private SimpleButton btnUpdatePersonGDB;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IWorkspace iworkspace_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label lblCheckOutInfo;
        private GroupBox lblConfigKey;
        private Label lblGDBRelease;
        private Label lblType;
        private TextEdit textEditCheckOutName;
        private TextEdit textEditName;

        public GDBGeneralCtrl()
        {
            this.InitializeComponent();
        }

        private void btnConfigKey_Click(object sender, EventArgs e)
        {
            new frmConfigKey { Configuration = this.iworkspace_0 as IWorkspaceConfiguration }.ShowDialog();
        }

        private void btnProperty_Click(object sender, EventArgs e)
        {
        }

        private void btnUnRegister_Click(object sender, EventArgs e)
        {
        }

        private void btnUpdatePersonGDB_Click(object sender, EventArgs e)
        {
            try
            {
                IGeodatabaseRelease release = this.iworkspace_0 as IGeodatabaseRelease;
                if (release.CanUpgrade)
                {
                    release.Upgrade();
                }
                this.lblGDBRelease.Text = "此数据库与你所使用的ArcGIS版本匹配";
                this.btnUpdatePersonGDB.Enabled = false;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
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

        private void GDBGeneralCtrl_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.textEditName = new TextEdit();
            this.lblType = new Label();
            this.groupBox1 = new GroupBox();
            this.btnUpdatePersonGDB = new SimpleButton();
            this.lblGDBRelease = new Label();
            this.groupBox2 = new GroupBox();
            this.btnUnRegister = new SimpleButton();
            this.btnProperty = new SimpleButton();
            this.textEditCheckOutName = new TextEdit();
            this.label3 = new Label();
            this.lblCheckOutInfo = new Label();
            this.lblConfigKey = new GroupBox();
            this.btnConfigKey = new SimpleButton();
            this.label4 = new Label();
            this.textEditName.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.textEditCheckOutName.Properties.BeginInit();
            this.lblConfigKey.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x30);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "类型";
            this.textEditName.EditValue = "";
            this.textEditName.Location = new Point(0x33, 11);
            this.textEditName.Name = "textEditName";
            this.textEditName.Properties.ReadOnly = true;
            this.textEditName.Size = new Size(280, 0x15);
            this.textEditName.TabIndex = 2;
            this.lblType.Location = new Point(0x40, 0x30);
            this.lblType.Name = "lblType";
            this.lblType.Size = new Size(280, 0x18);
            this.lblType.TabIndex = 3;
            this.groupBox1.Controls.Add(this.btnUpdatePersonGDB);
            this.groupBox1.Controls.Add(this.lblGDBRelease);
            this.groupBox1.Location = new Point(0x10, 0x148);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x148, 0x70);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "升级状态";
            this.btnUpdatePersonGDB.Location = new Point(0xb8, 80);
            this.btnUpdatePersonGDB.Name = "btnUpdatePersonGDB";
            this.btnUpdatePersonGDB.Size = new Size(0x80, 0x18);
            this.btnUpdatePersonGDB.TabIndex = 1;
            this.btnUpdatePersonGDB.Text = "升级个人数据库";
            this.btnUpdatePersonGDB.Click += new EventHandler(this.btnUpdatePersonGDB_Click);
            this.lblGDBRelease.Location = new Point(0x10, 0x18);
            this.lblGDBRelease.Name = "lblGDBRelease";
            this.lblGDBRelease.Size = new Size(0x120, 40);
            this.lblGDBRelease.TabIndex = 0;
            this.groupBox2.Controls.Add(this.btnUnRegister);
            this.groupBox2.Controls.Add(this.btnProperty);
            this.groupBox2.Controls.Add(this.textEditCheckOutName);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblCheckOutInfo);
            this.groupBox2.Location = new Point(0x10, 80);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x148, 0x88);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "离线编辑";
            this.btnUnRegister.Location = new Point(200, 0x68);
            this.btnUnRegister.Name = "btnUnRegister";
            this.btnUnRegister.Size = new Size(0x68, 0x18);
            this.btnUnRegister.TabIndex = 4;
            this.btnUnRegister.Text = "取消注册为检出";
            this.btnUnRegister.Click += new EventHandler(this.btnUnRegister_Click);
            this.btnProperty.Location = new Point(0x70, 0x68);
            this.btnProperty.Name = "btnProperty";
            this.btnProperty.Size = new Size(0x40, 0x18);
            this.btnProperty.TabIndex = 3;
            this.btnProperty.Text = "属性...";
            this.btnProperty.Click += new EventHandler(this.btnProperty_Click);
            this.textEditCheckOutName.EditValue = "";
            this.textEditCheckOutName.Location = new Point(80, 0x48);
            this.textEditCheckOutName.Name = "textEditCheckOutName";
            this.textEditCheckOutName.Size = new Size(0xd8, 0x15);
            this.textEditCheckOutName.TabIndex = 2;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x10, 0x4d);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "检出名称";
            this.lblCheckOutInfo.Location = new Point(0x10, 0x20);
            this.lblCheckOutInfo.Name = "lblCheckOutInfo";
            this.lblCheckOutInfo.Size = new Size(0x128, 0x20);
            this.lblCheckOutInfo.TabIndex = 0;
            this.lblConfigKey.Controls.Add(this.btnConfigKey);
            this.lblConfigKey.Controls.Add(this.label4);
            this.lblConfigKey.Location = new Point(0x10, 0xe0);
            this.lblConfigKey.Name = "lblConfigKey";
            this.lblConfigKey.Size = new Size(0x148, 0x60);
            this.lblConfigKey.TabIndex = 6;
            this.lblConfigKey.TabStop = false;
            this.lblConfigKey.Text = "配置关键字";
            this.btnConfigKey.Location = new Point(0x88, 0x40);
            this.btnConfigKey.Name = "btnConfigKey";
            this.btnConfigKey.Size = new Size(0x70, 0x18);
            this.btnConfigKey.TabIndex = 1;
            this.btnConfigKey.Text = "配置关键字...";
            this.btnConfigKey.Click += new EventHandler(this.btnConfigKey_Click);
            this.label4.Location = new Point(0x10, 0x20);
            this.label4.Name = "label4";
            this.label4.Size = new Size(280, 0x18);
            this.label4.TabIndex = 0;
            base.Controls.Add(this.lblConfigKey);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.lblType);
            base.Controls.Add(this.textEditName);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "GDBGeneralCtrl";
            base.Size = new Size(0x160, 0x1d0);
            base.Load += new EventHandler(this.GDBGeneralCtrl_Load);
            this.textEditName.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.textEditCheckOutName.Properties.EndInit();
            this.lblConfigKey.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void method_0()
        {
            this.textEditName.Text = this.iworkspace_0.PathName;
            switch (this.iworkspace_0.Type)
            {
                case esriWorkspaceType.esriLocalDatabaseWorkspace:
                    if (!(Path.GetExtension(this.iworkspace_0.PathName).ToLower() == ".mdb"))
                    {
                        this.lblType.Text = "文件型空间数据库";
                        this.lblConfigKey.Text = "点击按钮列出数据库定义的所有关键字";
                        this.btnConfigKey.Enabled = true;
                        break;
                    }
                    this.lblType.Text = "个人空间数据库";
                    this.lblConfigKey.Text = "个人空间数据库不支持配置关键字";
                    this.btnConfigKey.Enabled = false;
                    break;

                case esriWorkspaceType.esriRemoteDatabaseWorkspace:
                    this.lblType.Text = "ArcSDE Geodatebase连接";
                    this.lblConfigKey.Text = "点击按钮列出数据库定义的所有关键字";
                    this.btnConfigKey.Enabled = true;
                    break;
            }
            if (this.iworkspace_0 is IWorkspaceReplicas)
            {
                IEnumReplica replicas = (this.iworkspace_0 as IWorkspaceReplicas).Replicas;
                replicas.Reset();
                IReplica replica2 = replicas.Next();
                string name = "";
                if (replica2 != null)
                {
                    this.lblCheckOutInfo.Text = "这是个检出数据库。该数据库包含从另外的数据库导出的数据。";
                    this.textEditCheckOutName.Enabled = true;
                    name = replica2.Name;
                    this.textEditCheckOutName.Text = name;
                    this.btnProperty.Enabled = true;
                    this.btnUnRegister.Enabled = true;
                }
                else
                {
                    this.lblCheckOutInfo.Text = "该数据库不包含从另外的数据库导出的数据。";
                    this.textEditCheckOutName.Enabled = false;
                    this.btnProperty.Enabled = false;
                    this.btnUnRegister.Enabled = false;
                }
            }
            else
            {
                this.lblCheckOutInfo.Text = "该数据库不包含从另外的数据库导出的数据。";
                this.textEditCheckOutName.Enabled = false;
                this.btnProperty.Enabled = false;
                this.btnUnRegister.Enabled = false;
            }
            try
            {
                IGeodatabaseRelease release = this.iworkspace_0 as IGeodatabaseRelease;
                if (release.CurrentRelease)
                {
                    this.lblGDBRelease.Text = "此数据库与你所使用的ArcGIS版本匹配";
                    this.btnUpdatePersonGDB.Enabled = false;
                }
                else if (release.CanUpgrade)
                {
                    this.lblGDBRelease.Text = "此数据库与你所使用的ArcGIS版本不匹配";
                    this.btnUpdatePersonGDB.Enabled = true;
                }
                else
                {
                    this.btnUpdatePersonGDB.Enabled = false;
                }
            }
            catch
            {
                this.btnUpdatePersonGDB.Enabled = false;
            }
        }

        public IWorkspace Workspace
        {
            set
            {
                this.iworkspace_0 = value;
            }
        }
    }
}

