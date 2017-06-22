using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmAddGDBConnection : Form
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private frmSDEConnectionDetialInfo.HISTORICALTYPE historicaltype_0 = frmSDEConnectionDetialInfo.HISTORICALTYPE.VERSION;
        private string string_1 = "DEFAULT";

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
                Logger.Current.Error("",exception, "");
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

 private void groupBox1_Enter(object sender, EventArgs e)
        {
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
                        case 2147751273:
                        case 2147751169:
                            MessageBox.Show("连接数据库失败", "连接");
                            return null;

                        case 2147751274:
                            MessageBox.Show("连接数据库失败\r\n该服务器上的SDE没有启动", "连接");
                            return null;

                        case 2147500037:
                            MessageBox.Show("连接数据库失败", "连接");
                            return null;
                    }
                }
                Logger.Current.Error("",exception, "");
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

