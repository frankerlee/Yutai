using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;

namespace Yutai.ArcGIS.Catalog.UI
{
    public partial class frmNewArcGISServer : Form
    {
        private IContainer icontainer_0 = null;
        private IGxObject igxObject_0 = null;

        public frmNewArcGISServer()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (((this.radioGroup1.SelectedIndex >= 1) && !this.chkUseDefaultFolder.Checked) && (this.txtTempFolder.Text.Trim().Length == 0))
            {
                MessageBox.Show("请输入过渡文件夹");
            }
            else
            {
                IPropertySet set;
                object obj2;
                object obj3;
                string path = Environment.SystemDirectory.Substring(0, 2) + @"\Users\Administrator\AppData\Roaming\ESRI\Desktop10.2\ArcCatalog\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string str2 = "";
                string text = "";
                if (this.radioGroup1.SelectedIndex == 0)
                {
                    text = this.txtServer.Text;
                }
                else
                {
                    text = this.txtMUrl.Text;
                }
                string str4 = "";
                if (this.radioGroup1.SelectedIndex == 0)
                {
                    str4 = "(用户)";
                }
                else if (this.radioGroup1.SelectedIndex == 1)
                {
                    str4 = "(系统管理员)";
                }
                else if (this.radioGroup1.SelectedIndex == 2)
                {
                    str4 = "(发布者)";
                }
                if (text.IndexOf("http://") == 0)
                {
                    text = text.Substring(7);
                }
                else
                {
                    this.txtServer.Text = "http://" + this.txtServer.Text;
                }
                string[] strArray = text.Split(new char[] { '/' });
                string str5 = strArray[0];
                if (strArray.Length > 0)
                {
                    str2 = path + "arcgis on" + strArray[0].Replace(":", "_") + str4 + ".ags";
                }
                else
                {
                    str2 = path + "arcgis on" + str4 + ".ags";
                }
                str2 = this.method_1(str2);
                IAGSServerConnectionFactory factory = new AGSServerConnectionFactoryClass();
                if (this.radioGroup1.SelectedIndex == 0)
                {
                    set = this.method_2(str2, str5);
                }
                else
                {
                    set = this.method_3(str2, str5);
                }
                set.GetAllProperties(out obj2, out obj3);
                IAGSServerConnection connection = null;
                try
                {
                    connection = factory.Open(set, 0);
                    if (connection == null)
                    {
                        return;
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                    return;
                }
                if (Directory.Exists(path))
                {
                    IGxAGSConnection connection2 = new GxAGSConnection {
                        AGSServerConnectionName = connection.FullName as IAGSServerConnectionName
                    };
                    connection2.SaveToFile(str2);
                    this.igxObject_0 = connection2 as IGxObject;
                }
                base.DialogResult = DialogResult.OK;
            }
        }

        private void chkUseDefaultFolder_CheckedChanged(object sender, EventArgs e)
        {
            this.txtTempFolder.Enabled = this.chkUseDefaultFolder.Checked;
            this.btnSelect.Enabled = this.chkUseDefaultFolder.Checked;
        }

 private void frmNewArcGISServer_Load(object sender, EventArgs e)
        {
            this.txtTempFolder.Text = Path.GetTempPath();
        }

 private IMapServer method_0(string string_2)
        {
            IAGSEnumServerObjectName serverObjectNames = this.iagsserverConnection_0.ServerObjectNames;
            IAGSServerObjectName name2 = null;
            while ((name2 = serverObjectNames.Next()) != null)
            {
                if (name2.Name == string_2)
                {
                    break;
                }
            }
            IName name3 = name2 as IName;
            return (name3.Open() as IMapServer);
        }

        private string method_1(string string_2)
        {
            string str = string_2.Substring(0, string_2.Length - 4);
            for (int i = 1; File.Exists(string_2); i++)
            {
                string_2 = str + " (" + i.ToString() + ").ags";
            }
            return string_2;
        }

        private IPropertySet method_2(string string_2, string string_3)
        {
            IPropertySet set = new PropertySetClass();
            if (this.chkSaveUserNameAndPsw.Checked)
            {
                set.SetProperty("USER", this.txtUser.Text.Trim());
            }
            set.SetProperty("Modulus", "dba16ec2c39b37a983b29026dca2859b28cc07bed0a9662bdea17d9fe486fed4d0e2e8a27ca1de05f186d2377da7ced5661e159d10abf5999258d11cb06b2fb3");
            if (!this.chkSaveUserNameAndPsw.Checked)
            {
                set.SetProperty("Exponent", "1001");
            }
            if (this.chkSaveUserNameAndPsw.Checked)
            {
                if (this.txtUser.Text.Trim().Length == 0)
                {
                    set.SetProperty("HIDEUSERPROPERTY", true);
                }
                else
                {
                    set.SetProperty("HIDEUSERPROPERTY", false);
                }
            }
            else
            {
                set.SetProperty("HIDEUSERPROPERTY", true);
            }
            set.SetProperty("URL", this.txtServer.Text);
            set.SetProperty("STAGINGFOLDER", "");
            set.SetProperty("USEDEFAULTSTAGINGFOLDER", true);
            set.SetProperty("ANONYMOUS", false);
            set.SetProperty("CONNECTIONMODE", this.radioGroup1.SelectedIndex);
            set.SetProperty("SERVERTYPE", 1);
            set.SetProperty("HTTPTIMEOUT", 60);
            set.SetProperty("MESSAGEFORMAT", 2);
            set.SetProperty("SoapUrl", "http://" + string_3 + "/arcgis/services");
            set.SetProperty("RestUrl", "http://" + string_3 + "/arcgis/rest");
            set.SetProperty("AdminURL", "http://" + string_3 + "/arcgis/admin");
            set.SetProperty("AdminTokenUrl", "http://" + string_3 + "/arcgis/admin/generateToken");
            set.SetProperty("SupportsRSA", true);
            if (this.chkSaveUserNameAndPsw.Checked)
            {
                set.SetProperty("Exponent", "1001");
            }
            if (this.chkSaveUserNameAndPsw.Checked)
            {
                set.SetProperty("PASSWORD", this.txtPassword.Text);
            }
            set.SetProperty("connectionfile", string_2);
            return set;
        }

        private IPropertySet method_3(string string_2, string string_3)
        {
            IPropertySet set = new PropertySetClass();
            if (this.chkSaveManage.Checked)
            {
                set.SetProperty("USER", this.txtUserManage.Text.Trim());
            }
            set.SetProperty("Modulus", "dba16ec2c39b37a983b29026dca2859b28cc07bed0a9662bdea17d9fe486fed4d0e2e8a27ca1de05f186d2377da7ced5661e159d10abf5999258d11cb06b2fb3");
            if (!this.chkSaveManage.Checked)
            {
                set.SetProperty("Exponent", "1001");
            }
            if (this.chkSaveManage.Checked)
            {
                if (this.txtUserManage.Text.Trim().Length == 0)
                {
                    set.SetProperty("HIDEUSERPROPERTY", true);
                }
                else
                {
                    set.SetProperty("HIDEUSERPROPERTY", false);
                }
            }
            else
            {
                set.SetProperty("HIDEUSERPROPERTY", true);
            }
            set.SetProperty("URL", this.txtMUrl.Text + "/admin");
            set.SetProperty("STAGINGFOLDER", this.txtTempFolder.Text);
            set.SetProperty("USEDEFAULTSTAGINGFOLDER", this.chkUseDefaultFolder.Checked);
            set.SetProperty("ANONYMOUS", false);
            set.SetProperty("CONNECTIONMODE", this.radioGroup1.SelectedIndex);
            set.SetProperty("SERVERTYPE", 1);
            set.SetProperty("HTTPTIMEOUT", 60);
            set.SetProperty("MESSAGEFORMAT", 2);
            set.SetProperty("SoapUrl", "http://" + string_3 + "/arcgis/services");
            set.SetProperty("RestUrl", "http://" + string_3 + "/arcgis/rest");
            set.SetProperty("AdminURL", "http://" + string_3 + "/arcgis/admin");
            set.SetProperty("AdminTokenUrl", "http://" + string_3 + "/arcgis/admin/generateToken");
            set.SetProperty("SupportsRSA", true);
            if (this.chkSaveManage.Checked)
            {
                set.SetProperty("Exponent", "1001");
            }
            if (this.chkSaveManage.Checked)
            {
                set.SetProperty("PASSWORD", this.txtPaswordMan.Text);
            }
            set.SetProperty("connectionfile", string_2);
            return set;
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.panel1.Visible = this.radioGroup1.SelectedIndex == 0;
            this.panel2.Visible = this.radioGroup1.SelectedIndex >= 1;
        }

        private void txtUser_EditValueChanged(object sender, EventArgs e)
        {
            if (this.txtUser.Text.Trim().Length == 0)
            {
                this.chkSaveUserNameAndPsw.Enabled = false;
            }
            else
            {
                this.chkSaveUserNameAndPsw.Enabled = true;
            }
        }

        private void txtUserManage_EditValueChanged(object sender, EventArgs e)
        {
            this.chkSaveManage.Enabled = true;
        }

        public IGxObject NewObject
        {
            get
            {
                return this.igxObject_0;
            }
        }
    }
}

