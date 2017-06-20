using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class frmEditGDBConnection : Form
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
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label lblVersion;
        private string string_0;
        private string string_1 = "DEFAULT";
        private string string_2 = "";
        private TextEdit txtDatabase;
        private TextEdit txtInstance;
        private TextEdit txtPassword;
        private TextEdit txtServer;
        private TextEdit txtUser;

        public frmEditGDBConnection()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void btnChangeVersion_Click(object sender, EventArgs e)
        {
            IWorkspace workspace = this.method_4();
            if (workspace != null)
            {
                IEnumVersionInfo versions = (workspace as IVersionedWorkspace2).Versions;
                frmSelectVersion version = new frmSelectVersion {
                    EnumVersionInfo = versions
                };
                if (version.ShowDialog() == DialogResult.OK)
                {
                    this.string_1 = version.VersionName;
                    this.lblVersion.Text = this.string_1;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                IPropertySet connectionProperties = this.method_3(this.chkSaveUserandPsw.Checked, this.chkSaveVersion.Checked);
                File.Delete(this.string_2);
                string directoryName = Path.GetDirectoryName(this.string_2);
                IWorkspaceFactory factory = new SdeWorkspaceFactoryClass();
                factory.Create(directoryName, Path.GetFileName(this.string_2), connectionProperties, 0);
                base.DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                Logger.Current.Error("",exception, "");
            }
            base.Close();
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            IPropertySet connectionProperties = new PropertySetClass();
            string str = this.txtServer.Text.Trim();
            connectionProperties.SetProperty("SERVER", str);
            str = this.txtInstance.Text.Trim();
            connectionProperties.SetProperty("INSTANCE", str);
            str = this.txtDatabase.Text.Trim();
            if (str.Length >= 0)
            {
                connectionProperties.SetProperty("DATABASE", str);
            }
            str = this.txtUser.Text.Trim();
            connectionProperties.SetProperty("USER", str);
            str = this.txtPassword.Text.Trim();
            connectionProperties.SetProperty("PASSWORD", str);
            connectionProperties.SetProperty("VERSION", this.string_1);
            IWorkspaceFactory factory = new SdeWorkspaceFactoryClass();
            try
            {
                factory.Open(connectionProperties, 0);
                this.bool_0 = true;
                this.btnTestConnection.Enabled = false;
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

        private void frmEditGDBConnection_Load(object sender, EventArgs e)
        {
            if (this.string_2 != null)
            {
                IWorkspaceName name = new WorkspaceNameClass {
                    WorkspaceFactoryProgID = "esriDataSourcesGDB.SdeWorkspaceFactory",
                    PathName = this.string_2
                };
                IPropertySet connectionProperties = name.ConnectionProperties;
                try
                {
                    this.txtServer.Text = connectionProperties.GetProperty("SERVER") as string;
                }
                catch
                {
                }
                try
                {
                    this.txtInstance.Text = connectionProperties.GetProperty("INSTANCE") as string;
                }
                catch
                {
                }
                try
                {
                    this.txtDatabase.Text = connectionProperties.GetProperty("DATABASE") as string;
                }
                catch
                {
                }
                this.chkSaveUserandPsw.EditValue = false;
                try
                {
                    this.txtUser.Text = connectionProperties.GetProperty("USER") as string;
                    if (this.txtUser.Text.Length > 0)
                    {
                        this.chkSaveUserandPsw.EditValue = true;
                    }
                }
                catch
                {
                }
                try
                {
                    this.txtPassword.Text = connectionProperties.GetProperty("PASSWORD") as string;
                }
                catch
                {
                }
                try
                {
                    this.string_1 = connectionProperties.GetProperty("VERSION") as string;
                    this.lblVersion.Text = this.string_1;
                }
                catch
                {
                }
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditGDBConnection));
            this.groupBox1 = new GroupBox();
            this.btnTestConnection = new SimpleButton();
            this.chkSaveUserandPsw = new CheckEdit();
            this.txtPassword = new TextEdit();
            this.txtUser = new TextEdit();
            this.label5 = new Label();
            this.label4 = new Label();
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
            this.groupBox1.SuspendLayout();
            this.chkSaveUserandPsw.Properties.BeginInit();
            this.txtPassword.Properties.BeginInit();
            this.txtUser.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.chkSaveVersion.Properties.BeginInit();
            this.txtServer.Properties.BeginInit();
            this.txtInstance.Properties.BeginInit();
            this.txtDatabase.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnTestConnection);
            this.groupBox1.Controls.Add(this.chkSaveUserandPsw);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtUser);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new Point(8, 0x68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x110, 120);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "帐号";
            this.btnTestConnection.Enabled = false;
            this.btnTestConnection.Location = new Point(0xc0, 0x58);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new Size(0x40, 0x18);
            this.btnTestConnection.TabIndex = 4;
            this.btnTestConnection.Text = "测试连接";
            this.btnTestConnection.Click += new EventHandler(this.btnTestConnection_Click);
            this.chkSaveUserandPsw.Location = new Point(0x40, 0x58);
            this.chkSaveUserandPsw.Name = "chkSaveUserandPsw";
            this.chkSaveUserandPsw.Properties.Caption = "保存名称和密码";
            this.chkSaveUserandPsw.Size = new Size(120, 0x13);
            this.chkSaveUserandPsw.TabIndex = 3;
            this.chkSaveUserandPsw.CheckedChanged += new EventHandler(this.chkSaveUserandPsw_CheckedChanged);
            this.txtPassword.EditValue = "";
            this.txtPassword.Location = new Point(0x40, 0x38);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Size = new Size(0xc0, 0x15);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.EditValueChanged += new EventHandler(this.txtPassword_EditValueChanged);
            this.txtUser.EditValue = "";
            this.txtUser.Location = new Point(0x40, 0x18);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new Size(0xc0, 0x15);
            this.txtUser.TabIndex = 1;
            this.txtUser.EditValueChanged += new EventHandler(this.txtUser_EditValueChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x10, 0x3a);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x23, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "密码:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x10, 0x1c);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x2f, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "用户名:";
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
            this.groupBox2.Location = new Point(8, 240);
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
            this.btnOK.Location = new Point(0x90, 0x148);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x40, 0x18);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0xe0, 0x148);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x128, 0x16d);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.txtDatabase);
            base.Controls.Add(this.txtInstance);
            base.Controls.Add(this.txtServer);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmEditGDBConnection";
            base.ShowInTaskbar = false;
            this.Text = "空间数据库连接属性";
            base.Load += new EventHandler(this.frmEditGDBConnection_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.chkSaveUserandPsw.Properties.EndInit();
            this.txtPassword.Properties.EndInit();
            this.txtUser.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.chkSaveVersion.Properties.EndInit();
            this.txtServer.Properties.EndInit();
            this.txtInstance.Properties.EndInit();
            this.txtDatabase.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void method_0()
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

        private string method_1(string string_3)
        {
            string str = string_3.Substring(0, string_3.Length - 4);
            for (int i = 1; File.Exists(string_3); i++)
            {
                string_3 = str + " (" + i.ToString() + ").sde";
            }
            return string_3;
        }

        private IPropertySet method_2()
        {
            IPropertySet set = new PropertySetClass();
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
            return set;
        }

        private IPropertySet method_3(bool bool_1, bool bool_2)
        {
            IPropertySet set = new PropertySetClass();
            string str = this.txtServer.Text.Trim();
            set.SetProperty("SERVER", str);
            str = this.txtInstance.Text.Trim();
            set.SetProperty("INSTANCE", str);
            str = this.txtDatabase.Text.Trim();
            if (str.Length >= 0)
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
                set.SetProperty("VERSION", this.string_1);
            }
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
                MessageBox.Show(exception.Message);
            }
            return null;
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

        public string PathName
        {
            set
            {
                this.string_2 = value;
            }
        }
    }
}

