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
    public partial class frmEditGDBConnection : Form
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private string string_1 = "DEFAULT";
        private string string_2 = "";

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

