using System;
using System.Collections;
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
    public partial class frmNewArcGISServer93 : Form
    {
        private IGxObject igxObject_0 = null;
        private IList ilist_0 = new ArrayList();

        public frmNewArcGISServer93()
        {
            if (CatalogLicenseProviderCheck.Check())
            {
                this.InitializeComponent();
            }
        }

        private void btnGetAllResource_Click(object sender, EventArgs e)
        {
            this.ResourcescheckedListBox.Items.Clear();
            IAGSServerConnectionFactory factory = new AGSServerConnectionFactoryClass();
            IPropertySet pConnectionProperties = this.method_4();
            IAGSServerConnection connection = null;
            try
            {
                connection = factory.Open(pConnectionProperties, 0);
            }
            catch
            {
                MessageBox.Show("不支持GET,POST等方法");
                return;
            }
            try
            {
                IAGSEnumServerObjectName serverObjectNames = connection.ServerObjectNames;
                serverObjectNames.Reset();
                for (IAGSServerObjectName name2 = serverObjectNames.Next();
                    name2 != null;
                    name2 = serverObjectNames.Next())
                {
                    this.ilist_0.Add(name2);
                    this.ResourcescheckedListBox.Items.Add(new Class0(name2));
                }
                connection = null;
                factory = null;
            }
            catch
            {
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if ((this.rdoResourceUse.SelectedIndex == 1) && (this.ResourcescheckedListBox.CheckedItems.Count == 0))
            {
                MessageBox.Show("请选择所用资源!");
            }
            else
            {
                new AGSServerConnectionFactoryClass();
                IPropertySet set = this.method_4();
                IAGSServerConnectionName name = new AGSServerConnectionNameClass
                {
                    ConnectionProperties = set
                };
                try
                {
                    IAGSServerConnection connection = (name as IName).Open() as IAGSServerConnection;
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
                string path = Environment.SystemDirectory.Substring(0, 2) +
                              @"\Documents and Settings\Administrator\Application Data\ESRI\ArcCatalog\";
                if (Directory.Exists(path))
                {
                    string str2 = path + this.txtServer.Text + ".ags";
                    str2 = this.method_3(str2);
                    IGxAGSConnection connection2 = new GxAGSConnection();
                    object obj2 = null;
                    if ((this.rdoResourceUse.SelectedIndex == 1) &&
                        (this.ResourcescheckedListBox.CheckedItems.Count > 0))
                    {
                        string[] strArray = new string[this.ResourcescheckedListBox.CheckedItems.Count];
                        for (int i = 0; i < this.ResourcescheckedListBox.CheckedItems.Count; i++)
                        {
                            strArray[i] = this.ResourcescheckedListBox.CheckedItems[i].ToString();
                        }
                        obj2 = strArray;
                    }
                    connection2.AGSServerConnectionName = name;
                    connection2.SelectedServerObjects = obj2;
                    connection2.SaveToFile(str2);
                    this.igxObject_0 = connection2 as IGxObject;
                }
                base.DialogResult = DialogResult.OK;
                base.Close();
            }
        }

        private void method_0(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.imapServer_0 = null;
            try
            {
                IAGSServerConnectionFactory factory = new AGSServerConnectionFactoryClass();
                IPropertySet pConnectionProperties = new PropertySetClass();
                this.iagsserverConnection_0 = factory.Open(pConnectionProperties, 0);
                IAGSEnumServerObjectName serverObjectNames = this.iagsserverConnection_0.ServerObjectNames;
                while (serverObjectNames.Next() != null)
                {
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception exception)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(exception.Message, "An error has occurred");
            }
        }

        private void method_1(ref IMapDescription imapDescription_1, ref IMapServer imapServer_1)
        {
        }

        private IMapServer method_2(string string_2)
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

        private string method_3(string string_2)
        {
            string str = string_2.Substring(0, string_2.Length - 4);
            for (int i = 1; File.Exists(string_2); i++)
            {
                string_2 = str + " (" + i.ToString() + ").ags";
            }
            return string_2;
        }

        private IPropertySet method_4()
        {
            IPropertySet set = new PropertySetClass();
            if (this.rdoServerType.SelectedIndex == 0)
            {
                set.SetProperty("CONNECTIONTYPE", 1);
                set.SetProperty("MACHINE", this.txtServer.Text);
                set.SetProperty("USER", "");
                set.SetProperty("PASSWORD", "");
                set.SetProperty("HIDEUSERPROPERTY", false);
                return set;
            }
            set.SetProperty("CONNECTIONTYPE", 2);
            set.SetProperty("URL", this.txtServer.Text);
            set.SetProperty("USER", this.txtUser.Text.Trim());
            set.SetProperty("PASSWORD", this.txtPassword.Text);
            set.SetProperty("HIDEUSERPROPERTY", this.chkSaveUserNameAndPsw.EditValue);
            return set;
        }

        private void rdoResourceUse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdoResourceUse.SelectedIndex == 0)
            {
                this.btnGetAllResource.Enabled = false;
                this.ResourcescheckedListBox.Enabled = false;
            }
            else
            {
                if (this.rdoServerType.SelectedIndex == 0)
                {
                    this.btnGetAllResource.Enabled = this.txtServer.Text.Trim().Length > 0;
                }
                else if (this.txtServer.Text.Trim().IndexOf("http://") == 0)
                {
                    this.btnGetAllResource.Enabled = this.txtServer.Text.Trim().Length > 7;
                }
                else
                {
                    this.btnGetAllResource.Enabled = this.txtServer.Text.Trim().Length > 0;
                }
                this.ResourcescheckedListBox.Enabled = true;
            }
        }

        private void rdoServerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdoServerType.SelectedIndex == 0)
            {
                this.txtUser.Enabled = false;
                this.txtPassword.Enabled = false;
                this.chkSaveUserNameAndPsw.Enabled = false;
                this.txtServer.Text = "";
                this.btnGetAllResource.Enabled = false;
                this.btnOK.Enabled = false;
            }
            else
            {
                this.txtUser.Enabled = true;
                this.txtPassword.Enabled = true;
                this.chkSaveUserNameAndPsw.Enabled = true;
                this.txtServer.Text = "http://";
                this.btnGetAllResource.Enabled = false;
                this.btnOK.Enabled = false;
            }
        }

        private void ResourcescheckedListBox_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
        {
        }

        private void txtServer_EditValueChanged(object sender, EventArgs e)
        {
            if (this.rdoServerType.SelectedIndex == 0)
            {
                if (this.rdoResourceUse.SelectedIndex == 1)
                {
                    this.btnGetAllResource.Enabled = this.txtServer.Text.Trim().Length > 0;
                }
                this.btnOK.Enabled = this.txtServer.Text.Trim().Length > 0;
            }
            else if (this.txtServer.Text.Trim().IndexOf("http://") == 0)
            {
                if (this.rdoResourceUse.SelectedIndex == 1)
                {
                    this.btnGetAllResource.Enabled = this.txtServer.Text.Trim().Length > 7;
                }
                this.btnOK.Enabled = this.txtServer.Text.Trim().Length > 7;
            }
            else
            {
                if (this.rdoResourceUse.SelectedIndex == 1)
                {
                    this.btnGetAllResource.Enabled = this.txtServer.Text.Trim().Length > 0;
                }
                this.btnOK.Enabled = this.txtServer.Text.Trim().Length > 0;
            }
        }

        public IGxObject NewObject
        {
            get { return this.igxObject_0; }
        }

        private partial class Class0
        {
            private IAGSServerObjectName iagsserverObjectName_0 = null;

            public Class0(IAGSServerObjectName iagsserverObjectName_1)
            {
                this.iagsserverObjectName_0 = iagsserverObjectName_1;
            }

            public override string ToString()
            {
                if (this.iagsserverObjectName_0 != null)
                {
                    return this.iagsserverObjectName_0.Name;
                }
                return "";
            }

            public IAGSServerObjectName AGSServerObjectName
            {
                get { return this.iagsserverObjectName_0; }
            }
        }
    }
}