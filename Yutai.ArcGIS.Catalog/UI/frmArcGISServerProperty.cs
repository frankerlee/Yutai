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
    public partial class frmArcGISServerProperty : Form
    {
        private System.Array array_0 = null;
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IAGSServerConnection iagsserverConnection_0 = null;
        private IAGSServerConnectionName iagsserverConnectionName_0 = null;
        private IList ilist_0 = new ArrayList();
        private string string_0 = "";

        public frmArcGISServerProperty()
        {
            this.InitializeComponent();
        }

        private void btnGetAllResource_Click(object sender, EventArgs e)
        {
            this.method_7();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.string_0 = this.txtName.Text.Trim();
            if ((this.rdoResourceUse.SelectedIndex == 1) && (this.ResourcescheckedListBox.CheckedItems.Count == 0))
            {
                MessageBox.Show("请选择所用资源!");
            }
            else
            {
                object obj2 = null;
                if ((this.rdoResourceUse.SelectedIndex == 1) && (this.ResourcescheckedListBox.CheckedItems.Count > 0))
                {
                    string[] strArray = new string[this.ResourcescheckedListBox.CheckedItems.Count];
                    for (int i = 0; i < this.ResourcescheckedListBox.CheckedItems.Count; i++)
                    {
                        strArray[i] = this.ResourcescheckedListBox.CheckedItems[i].ToString();
                    }
                    obj2 = strArray;
                }
                this.array_0 = obj2 as System.Array;
                base.DialogResult = DialogResult.OK;
                base.Close();
            }
        }

 private void frmArcGISServerProperty_Load(object sender, EventArgs e)
        {
            this.bool_0 = true;
            this.txtName.Text = this.string_0;
            if (this.iagsserverConnectionName_0 != null)
            {
                this.rdoServerType.SelectedIndex = ((int) this.iagsserverConnectionName_0.ConnectionType) - 1;
                if (this.rdoServerType.SelectedIndex == 0)
                {
                    this.txtServer.Text = this.iagsserverConnectionName_0.ConnectionProperties.GetProperty("MACHINE").ToString();
                }
                else
                {
                    this.txtServer.Text = this.iagsserverConnectionName_0.ConnectionProperties.GetProperty("url").ToString();
                }
                if (this.array_0 != null)
                {
                    this.rdoResourceUse.SelectedIndex = 1;
                }
                else
                {
                    this.rdoResourceUse.SelectedIndex = 0;
                }
                if (this.bool_1)
                {
                    this.method_7();
                }
            }
            this.btnOK.Enabled = this.bool_1;
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

        private void method_1(ref IMapDescription imapDescription_0, ref IMapServer imapServer_1)
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

        private void method_3(object sender, EventArgs e)
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

        private string method_4(string string_2)
        {
            string str = string_2.Substring(0, string_2.Length - 4);
            for (int i = 1; File.Exists(string_2); i++)
            {
                string_2 = str + " (" + i.ToString() + ").ags";
            }
            return string_2;
        }

        private IPropertySet method_5()
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

        private bool method_6(string string_2)
        {
            if (this.array_0 != null)
            {
                for (int i = 0; i < this.array_0.Length; i++)
                {
                    string str = (string) this.array_0.GetValue(i);
                    if (str == string_2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void method_7()
        {
            this.ResourcescheckedListBox.Items.Clear();
            IAGSServerConnection connection = (this.iagsserverConnectionName_0 as IName).Open() as IAGSServerConnection;
            IAGSEnumServerObjectName serverObjectNames = connection.ServerObjectNames;
            serverObjectNames.Reset();
            for (IAGSServerObjectName name2 = serverObjectNames.Next(); name2 != null; name2 = serverObjectNames.Next())
            {
                string name = name2.Name;
                this.ResourcescheckedListBox.Items.Add(name, this.method_6(name));
            }
            connection = null;
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
                this.btnGetAllResource.Enabled = this.bool_1;
                this.ResourcescheckedListBox.Enabled = true;
            }
        }

        private void txtName_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.btnOK.Enabled = this.txtName.Text.Trim().Length > 0;
            }
        }

        public IAGSServerConnectionName AGSServerConnectionName
        {
            get
            {
                return this.iagsserverConnectionName_0;
            }
            set
            {
                this.iagsserverConnectionName_0 = value;
            }
        }

        public string AGSServerName
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public bool IsConnect
        {
            set
            {
                this.bool_1 = value;
            }
        }

        public System.Array SelectObjectNames
        {
            get
            {
                return this.array_0;
            }
            set
            {
                this.array_0 = value;
            }
        }

        private partial class Class5
        {
            private IAGSServerObjectName iagsserverObjectName_0 = null;

            public Class5(IAGSServerObjectName iagsserverObjectName_1)
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
                get
                {
                    return this.iagsserverObjectName_0;
                }
            }
        }
    }
}

