namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.DataSourcesGDB;
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Catalog;
    using JLK.Editors;
    using JLK.Editors.Controls;
    using JLK.Utility;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class frmAddGDBConnection : Form
    {
        private bool bool_0 = false;
        private SimpleButton btnCancel;
        private SimpleButton btnChangeVersion;
        private SimpleButton btnOK;
        private SimpleButton btnTestConnection;
        private CheckEdit chkSaveUserandPsw;
        private CheckEdit chkSaveVersion;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private frmSDEConnectionDetialInfo.HISTORICALTYPE historicaltype_0 = frmSDEConnectionDetialInfo.HISTORICALTYPE.VERSION;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label lblVersion;
        private RadioGroup rdoAuthentic;
        private string string_0;
        private string string_1 = "DEFAULT";
        private TextEdit txtDatabase;
        private TextEdit txtInstance;
        private TextEdit txtPassword;
        private TextEdit txtServer;
        private TextEdit txtUser;

        public frmAddGDBConnection()
        {
            if (CatalogLicenseProviderCheck.Check())
            {
                this.InitializeComponent();
                this.lblVersion.Tag = "DEFAULT";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void btnChangeVersion_Click(object sender, EventArgs e)
        {
            IWorkspace workspace = this.method_4();
            if (workspace != null)
            {
                frmSDEConnectionDetialInfo info = new frmSDEConnectionDetialInfo {
                    Workspace = workspace
                };
                if (info.ShowDialog() == DialogResult.OK)
                {
                    this.lblVersion.Tag = info.HistoricalInfo;
                    this.lblVersion.Text = info.HistoricalInfo.ToString();
                    this.historicaltype_0 = info.HISTORICAL;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                IPropertySet connectionProperties = this.method_3(this.chkSaveUserandPsw.Checked, this.chkSaveVersion.Checked);
                string path = Environment.SystemDirectory.Substring(0, 2) + @"\Documents and Settings\Administrator\Application Data\ESRI\ArcCatalog\";
                if (Directory.Exists(path))
                {
                    string str2 = path + "Connection to " + this.txtServer.Text.Trim() + ".sde";
                    str2 = this.method_1(str2);
                    IWorkspaceFactory factory = new SdeWorkspaceFactoryClass();
                    factory.Create(path, Path.GetFileName(str2), connectionProperties, 0);
                    this.string_0 = str2;
                    base.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception exception)
            {
                CErrorLog.writeErrorLog(this, exception, "");
            }
            base.Close();
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            IWorkspaceFactory factory = new SdeWorkspaceFactoryClass();
            try
            {
                factory.Open(this.method_2(), 0);
                this.bool_0 = true;
                this.btnTestConnection.Enabled = false;
                MessageBox.Show("连接成功！");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }

        private void chkSaveUserandPsw_CheckedChanged(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmAddGDBConnection));
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.groupBox2 = new GroupBox();
            this.btnChangeVersion = new SimpleButton();
            this.chkSaveVersion = new CheckEdit();
            this.lblVersion = new Label();
            this.txtServer = new TextEdit();
            this.txtInstance = new TextEdit();
            this.txtDatabase = new TextEdit();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.rdoAuthentic = new RadioGroup();
            this.groupBox1 = new GroupBox();
            this.txtUser = new TextEdit();
            this.label4 = new Label();
            this.chkSaveUserandPsw = new CheckEdit();
            this.label5 = new Label();
            this.txtPassword = new TextEdit();
            this.btnTestConnection = new SimpleButton();
            this.groupBox2.SuspendLayout();
            this.chkSaveVersion.Properties.BeginInit();
            this.txtServer.Properties.BeginInit();
            this.txtInstance.Properties.BeginInit();
            this.txtDatabase.Properties.BeginInit();
            this.rdoAuthentic.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.txtUser.Properties.BeginInit();
            this.chkSaveUserandPsw.Properties.BeginInit();
            this.txtPassword.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 12);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x2f, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "服务器:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x29);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "服务:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x10, 0x4b);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x2f, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "数据库:";
            this.groupBox2.Controls.Add(this.btnChangeVersion);
            this.groupBox2.Controls.Add(this.chkSaveVersion);
            this.groupBox2.Controls.Add(this.lblVersion);
            this.groupBox2.Location = new Point(13, 0x111);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x110, 80);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "版本";
            this.btnChangeVersion.Location = new Point(0xa8, 0x30);
            this.btnChangeVersion.Name = "btnChangeVersion";
            this.btnChangeVersion.Size = new Size(0x38, 0x18);
            this.btnChangeVersion.TabIndex = 2;
            this.btnChangeVersion.Text = "更改...";
            this.btnChangeVersion.Click += new EventHandler(this.btnChangeVersion_Click);
            this.chkSaveVersion.Location = new Point(0x10, 0x10);
            this.chkSaveVersion.Name = "chkSaveVersion";
            this.chkSaveVersion.Properties.Caption = "保存版本";
            this.chkSaveVersion.Size = new Size(0x58, 0x13);
            this.chkSaveVersion.TabIndex = 1;
            this.lblVersion.Location = new Point(0x10, 0x30);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new Size(0x80, 0x10);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "DEFAULT";
            this.txtServer.EditValue = "";
            this.txtServer.Location = new Point(0x40, 8);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new Size(0xd0, 0x15);
            this.txtServer.TabIndex = 1;
            this.txtServer.EditValueChanged += new EventHandler(this.txtServer_EditValueChanged);
            this.txtInstance.EditValue = "5151";
            this.txtInstance.Location = new Point(0x40, 0x26);
            this.txtInstance.Name = "txtInstance";
            this.txtInstance.Size = new Size(0xd0, 0x15);
            this.txtInstance.TabIndex = 2;
            this.txtInstance.EditValueChanged += new EventHandler(this.txtInstance_EditValueChanged);
            this.txtDatabase.EditValue = "";
            this.txtDatabase.Location = new Point(0x40, 0x48);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new Size(0xd0, 0x15);
            this.txtDatabase.TabIndex = 3;
            this.btnOK.Location = new Point(0x90, 0x16e);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x40, 0x18);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0xe0, 0x16e);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.rdoAuthentic.Location = new Point(3, -35);
            this.rdoAuthentic.Name = "rdoAuthentic";
            this.rdoAuthentic.Properties.Appearance.BackColor = Color.Transparent;
            this.rdoAuthentic.Properties.Appearance.Options.UseBackColor = true;
            this.rdoAuthentic.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoAuthentic.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "数据库验证"), new RadioGroupItem(null, "操作系统验证") });
            this.rdoAuthentic.Size = new Size(100, 0xef);
            this.rdoAuthentic.TabIndex = 1;
            this.rdoAuthentic.SelectedIndexChanged += new EventHandler(this.rdoAuthentic_SelectedIndexChanged);
            this.groupBox1.Controls.Add(this.txtUser);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.chkSaveUserandPsw);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.rdoAuthentic);
            this.groupBox1.Location = new Point(12, 0x6c);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x110, 160);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "帐号";
            this.groupBox1.Enter += new EventHandler(this.groupBox1_Enter);
            this.txtUser.EditValue = "";
            this.txtUser.Location = new Point(0x40, 0x2d);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new Size(0xc0, 0x15);
            this.txtUser.TabIndex = 12;
            this.txtUser.EditValueChanged += new EventHandler(this.txtUser_EditValueChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x10, 0x31);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x2f, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "用户名:";
            this.chkSaveUserandPsw.Location = new Point(0x40, 0x6d);
            this.chkSaveUserandPsw.Name = "chkSaveUserandPsw";
            this.chkSaveUserandPsw.Properties.Caption = "保存名称和密码";
            this.chkSaveUserandPsw.Size = new Size(120, 0x13);
            this.chkSaveUserandPsw.TabIndex = 15;
            this.chkSaveUserandPsw.CheckedChanged += new EventHandler(this.chkSaveUserandPsw_CheckedChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x10, 0x4f);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x23, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "密码:";
            this.txtPassword.EditValue = "";
            this.txtPassword.Location = new Point(0x40, 0x4d);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Size = new Size(0xc0, 0x15);
            this.txtPassword.TabIndex = 14;
            this.txtPassword.EditValueChanged += new EventHandler(this.txtPassword_EditValueChanged);
            this.btnTestConnection.Enabled = false;
            this.btnTestConnection.Location = new Point(13, 0x16c);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new Size(0x40, 0x18);
            this.btnTestConnection.TabIndex = 0x10;
            this.btnTestConnection.Text = "测试连接";
            this.btnTestConnection.Click += new EventHandler(this.btnTestConnection_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(310, 0x1a0);
            base.Controls.Add(this.btnTestConnection);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.txtDatabase);
            base.Controls.Add(this.txtInstance);
            base.Controls.Add(this.txtServer);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmAddGDBConnection";
            base.ShowInTaskbar = false;
            this.Text = "空间数据库连接";
            this.groupBox2.ResumeLayout(false);
            this.chkSaveVersion.Properties.EndInit();
            this.txtServer.Properties.EndInit();
            this.txtInstance.Properties.EndInit();
            this.txtDatabase.Properties.EndInit();
            this.rdoAuthentic.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.txtUser.Properties.EndInit();
            this.chkSaveUserandPsw.Properties.EndInit();
            this.txtPassword.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void method_0()
        {
            if (this.rdoAuthentic.SelectedIndex == 0)
            {
                if (this.txtUser.Text.Trim().Length == 0)
                {
                    this.chkSaveUserandPsw.Enabled = false;
                    this.btnTestConnection.Enabled = false;
                    this.btnChangeVersion.Enabled = false;
                }
                else if (this.txtPassword.Text.Trim().Length == 0)
                {
                    this.chkSaveUserandPsw.Enabled = false;
                    this.btnTestConnection.Enabled = false;
                    this.btnChangeVersion.Enabled = false;
                }
                else
                {
                    this.chkSaveUserandPsw.Enabled = true;
                    if (this.txtServer.Text.Trim().Length == 0)
                    {
                        this.btnTestConnection.Enabled = false;
                        this.btnChangeVersion.Enabled = false;
                    }
                    else if (this.txtInstance.Text.Trim().Length == 0)
                    {
                        this.btnTestConnection.Enabled = false;
                        this.btnChangeVersion.Enabled = false;
                    }
                    else
                    {
                        this.btnTestConnection.Enabled = true;
                        this.btnChangeVersion.Enabled = true;
                    }
                }
            }
            else if (this.txtServer.Text.Trim().Length == 0)
            {
                this.btnTestConnection.Enabled = false;
                this.btnChangeVersion.Enabled = false;
            }
            else if (this.txtInstance.Text.Trim().Length == 0)
            {
                this.btnTestConnection.Enabled = false;
                this.btnChangeVersion.Enabled = false;
            }
            else
            {
                this.btnTestConnection.Enabled = true;
                this.btnChangeVersion.Enabled = true;
            }
        }

        private string method_1(string string_2)
        {
            string str = string_2.Substring(0, string_2.Length - 4);
            for (int i = 1; File.Exists(string_2); i++)
            {
                string_2 = str + " (" + i.ToString() + ").sde";
            }
            return string_2;
        }

        private IPropertySet method_2()
        {
            IPropertySet set;
            if (this.rdoAuthentic.SelectedIndex == 0)
            {
                set = new PropertySetClass();
                string str = this.txtServer.Text.Trim();
                set.SetProperty("SERVER", str);
                str = this.txtInstance.Text.Trim();
                set.SetProperty("INSTANCE", str);
                str = this.txtDatabase.Text.Trim();
                if (str.Length >= 0)
                {
                    set.SetProperty("DATABASE", str);
                }
                str = this.txtUser.Text.Trim();
                set.SetProperty("USER", str);
                str = this.txtPassword.Text.Trim();
                set.SetProperty("PASSWORD", str);
                set.SetProperty("VERSION", this.string_1);
                set.SetProperty("AUTHENTICATION_MODE", "DBMS");
                return set;
            }
            string str2 = this.txtServer.Text.Trim();
            string str3 = this.txtInstance.Text.Trim();
            string str4 = this.txtDatabase.Text.Trim();
            set = new PropertySetClass();
            set.SetProperty("SERVER", str2);
            set.SetProperty("INSTANCE", str3);
            if (str4.Length >= 0)
            {
                set.SetProperty("DATABASE", str4);
            }
            set.SetProperty("VERSION", this.string_1);
            set.SetProperty("AUTHENTICATION_MODE", "OSA");
            return set;
        }

        private IPropertySet method_3(bool bool_1, bool bool_2)
        {
            IPropertySet set;
            if (this.rdoAuthentic.SelectedIndex != 0)
            {
                string str2 = this.txtServer.Text.Trim();
                string str3 = this.txtInstance.Text.Trim();
                string str4 = this.txtDatabase.Text.Trim();
                set = new PropertySetClass();
                set.SetProperty("SERVER", str2);
                set.SetProperty("INSTANCE", str3);
                if (str4.Length >= 0)
                {
                    set.SetProperty("DATABASE", str4);
                }
                switch (this.historicaltype_0)
                {
                    case frmSDEConnectionDetialInfo.HISTORICALTYPE.VERSION:
                        set.SetProperty("VERSION", this.lblVersion.Tag);
                        break;

                    case frmSDEConnectionDetialInfo.HISTORICALTYPE.HISTORICALTIMESTAMP:
                        set.SetProperty("HISTORICAL_TIMESTAMP", this.lblVersion.Tag);
                        break;

                    case frmSDEConnectionDetialInfo.HISTORICALTYPE.HISTORICALNAME:
                        set.SetProperty("HISTORICAL_NAME", this.lblVersion.Tag);
                        break;
                }
            }
            else
            {
                set = new PropertySetClass();
                string str = this.txtServer.Text.Trim();
                set.SetProperty("SERVER", str);
                str = this.txtInstance.Text.Trim();
                set.SetProperty("INSTANCE", str);
                str = this.txtDatabase.Text.Trim();
                if (str.Length > 0)
                {
                    set.SetProperty("DATABASE", str);
                }
                if (bool_1)
                {
                    str = this.txtUser.Text.Trim();
                    set.SetProperty("USER", str);
                    str = this.txtPassword.Text.Trim();
                    set.SetProperty("PASSWORD", str);
                }
                if (bool_2)
                {
                    switch (this.historicaltype_0)
                    {
                        case frmSDEConnectionDetialInfo.HISTORICALTYPE.VERSION:
                            set.SetProperty("VERSION", this.lblVersion.Tag);
                            break;

                        case frmSDEConnectionDetialInfo.HISTORICALTYPE.HISTORICALTIMESTAMP:
                            set.SetProperty("HISTORICAL_TIMESTAMP", this.lblVersion.Tag);
                            break;

                        case frmSDEConnectionDetialInfo.HISTORICALTYPE.HISTORICALNAME:
                            set.SetProperty("HISTORICAL_NAME", this.lblVersion.Tag);
                            break;
                    }
                }
                set.SetProperty("AUTHENTICATION_MODE", "DBMS");
                return set;
            }
            set.SetProperty("AUTHENTICATION_MODE", "OSA");
            return set;
        }

        private IWorkspace method_4()
        {
            IWorkspaceFactory factory = new SdeWorkspaceFactoryClass();
            try
            {
                return factory.Open(this.method_2(), 0);
            }
            catch (Exception exception)
            {
                if (exception is COMException)
                {
                    switch (((uint) (exception as COMException).ErrorCode))
                    {
                        case 0x80041569:
                        case 0x80041501:
                            MessageBox.Show("连接数据库失败", "连接");
                            return null;

                        case 0x8004156a:
                            MessageBox.Show("连接数据库失败\r\n该服务器上的SDE没有启动", "连接");
                            return null;

                        case 0x80004005:
                            MessageBox.Show("连接数据库失败", "连接");
                            return null;
                    }
                }
                CErrorLog.writeErrorLog(null, exception, "");
            }
            return null;
        }

        private void rdoAuthentic_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = this.rdoAuthentic.SelectedIndex == 0;
            this.txtUser.Enabled = flag;
            this.txtPassword.Enabled = flag;
            this.chkSaveUserandPsw.Enabled = flag;
            this.method_0();
        }

        private void txtInstance_EditValueChanged(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void txtPassword_EditValueChanged(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void txtServer_EditValueChanged(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void txtUser_EditValueChanged(object sender, EventArgs e)
        {
            this.method_0();
        }

        public string ConnectionPath
        {
            get
            {
                return this.string_0;
            }
        }
    }
}

