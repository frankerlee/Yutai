using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;

namespace Yutai.ArcGIS.Catalog.UI
{
    public partial class frmArcGISServerUseProperty : Form
    {
       
        private IContainer icontainer_0 = null;
        private IGxObject igxObject_0 = null;
     

        public frmArcGISServerUseProperty()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string path = Environment.SystemDirectory.Substring(0, 2) + @"\Users\Administrator\AppData\Roaming\ESRI\Desktop10.2\ArcCatalog\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string text = this.txtServer.Text;
            if (text.IndexOf("http://") == 0)
            {
                text = text.Substring(7);
            }
            else
            {
                this.txtServer.Text = "http://" + this.txtServer.Text;
            }
            string[] strArray = text.Split(new char[] { '/' });
            IAGSServerConnectionFactory factory = new AGSServerConnectionFactoryClass();
            IPropertySet pConnectionProperties = this.method_2(this.ConnectionFile, strArray[0]);
            IAGSServerConnection connection = null;
            try
            {
                connection = factory.Open(pConnectionProperties, 0);
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
                connection2.SaveToFile(this.ConnectionFile);
                this.igxObject_0 = connection2 as IGxObject;
            }
            base.DialogResult = DialogResult.OK;
        }

 private void frmArcGISServerUseProperty_Load(object sender, EventArgs e)
        {
            IPropertySet connectionProperties = null;
            if (this.AGSServerConnectionName != null)
            {
                connectionProperties = this.AGSServerConnectionName.ConnectionProperties;
            }
            else
            {
                connectionProperties = this.ConnectionProperties;
            }
            this.txtServer.Text = Convert.ToString(connectionProperties.GetProperty("URL"));
            this.chkSaveUserNameAndPsw.Checked = !Convert.ToBoolean(connectionProperties.GetProperty("HIDEUSERPROPERTY"));
            if (this.chkSaveUserNameAndPsw.Checked)
            {
                this.txtUser.Text = Convert.ToString(connectionProperties.GetProperty("USER"));
                byte[] property = (byte[]) connectionProperties.GetProperty("PASSWORD");
                string str = Encoding.ASCII.GetString(property);
                this.txtPassword.Text = str;
            }
            this.groupBox2.Enabled = true;
        }

 private IMapServer method_0(string string_3)
        {
            IAGSEnumServerObjectName serverObjectNames = this.iagsserverConnection_0.ServerObjectNames;
            IAGSServerObjectName name2 = null;
            while ((name2 = serverObjectNames.Next()) != null)
            {
                if (name2.Name == string_3)
                {
                    break;
                }
            }
            IName name3 = name2 as IName;
            return (name3.Open() as IMapServer);
        }

        private string method_1(string string_3)
        {
            string str = string_3.Substring(0, string_3.Length - 4);
            for (int i = 1; File.Exists(string_3); i++)
            {
                string_3 = str + " (" + i.ToString() + ").ags";
            }
            return string_3;
        }

        private IPropertySet method_2(string string_3, string string_4)
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
            set.SetProperty("CONNECTIONMODE", 0);
            set.SetProperty("SERVERTYPE", 1);
            set.SetProperty("HTTPTIMEOUT", 60);
            set.SetProperty("MESSAGEFORMAT", 2);
            set.SetProperty("SoapUrl", "http://" + string_4 + "/arcgis/services");
            set.SetProperty("RestUrl", "http://" + string_4 + "/arcgis/rest");
            set.SetProperty("AdminURL", "http://" + string_4 + "/arcgis/admin");
            set.SetProperty("AdminTokenUrl", "http://" + string_4 + "/arcgis/admin/generateToken");
            set.SetProperty("SupportsRSA", true);
            if (this.chkSaveUserNameAndPsw.Checked)
            {
                set.SetProperty("Exponent", "1001");
            }
            if (this.chkSaveUserNameAndPsw.Checked)
            {
                set.SetProperty("PASSWORD", this.txtPassword.Text);
            }
            set.SetProperty("connectionfile", string_3);
            return set;
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

        public IAGSServerConnectionName AGSServerConnectionName
        {
            [CompilerGenerated]
            get
            {
                return this.iagsserverConnectionName_0;
            }
            [CompilerGenerated]
            set
            {
                this.iagsserverConnectionName_0 = value;
            }
        }

        public string ConnectionFile
        {
            [CompilerGenerated]
            get
            {
                return this.string_2;
            }
            [CompilerGenerated]
            set
            {
                this.string_2 = value;
            }
        }

        public IPropertySet ConnectionProperties
        {
            [CompilerGenerated]
            get
            {
                return this.ipropertySet_0;
            }
            [CompilerGenerated]
            set
            {
                this.ipropertySet_0 = value;
            }
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

