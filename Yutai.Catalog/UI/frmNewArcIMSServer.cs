namespace Yutai.Catalog.UI
{
    using ESRI.ArcGIS.Carto;
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.GISClient;
    using Yutai.Catalog;
 
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class frmNewArcIMSServer : Form
    {
        private bool bool_0;
        private Button btnCancel;
        private Button btnGetAllResource;
        private Button btnOK;
        private CheckBox checkEdit1;
        private CheckBox chkSaveUserNameAndPsw;
        private double double_0;
        private double double_1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private IAGSServerConnection iagsserverConnection_0;
        private IContainer icontainer_0;
        private IGxObject igxObject_0 = null;
        private IImageDisplay iimageDisplay_0;
        private IList ilist_0 = new ArrayList();
        private Image image_0;
        private ImageList imageList_0;
        private IMapDescription imapDescription_0;
        private IMapServer imapServer_0;
        private int int_0;
        private int int_1;
        private int int_2;
        private int int_3;
        private Label label1;
        private Label label2;
        private Label label3;
        private ComboBox rdoResourceUse;
        private CheckedListBox ResourcescheckedListBox;
        private string string_0;
        private string string_1;
        private Button txtPassword;
        private Button txtServer;
        private Button txtUser;

        public frmNewArcIMSServer()
        {
            this.InitializeComponent();
        }

        private void btnGetAllResource_Click(object sender, EventArgs e)
        {
            this.ResourcescheckedListBox.Items.Clear();
            IAGSServerConnectionFactory factory = new AGSServerConnectionFactory();
            IPropertySet pConnectionProperties = this.method_5();
            IAGSServerConnection connection = null;
            try
            {
                connection = factory.Open(pConnectionProperties, 0);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
                return;
            }
            IAGSEnumServerObjectName serverObjectNames = connection.ServerObjectNames;
            serverObjectNames.Reset();
            for (IAGSServerObjectName name2 = serverObjectNames.Next(); name2 != null; name2 = serverObjectNames.Next())
            {
                this.ilist_0.Add(name2);
                this.ResourcescheckedListBox.Items.Add(new Class7(name2));
            }
            connection = null;
            factory = null;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if ((this.rdoResourceUse.SelectedIndex == 1) && (this.ResourcescheckedListBox.CheckedItems.Count == 0))
            {
                MessageBox.Show("请选择所用资源!");
            }
            else
            {
                new AGSServerConnectionFactory();
                IPropertySet set = this.method_5();
                IAGSServerConnectionName name = new AGSServerConnectionName {
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
                string path = Environment.SystemDirectory.Substring(0, 2) + @"\Documents and Settings\Administrator\Application Data\ESRI\ArcCatalog\";
                if (Directory.Exists(path))
                {
                    string str2 = path + this.txtServer.Text + ".ags";
                    str2 = this.method_4(str2);
                    IGxAGSConnection connection2 = new GxAGSConnection();
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
                    connection2.AGSServerConnectionName = name;
                    connection2.SelectedServerObjects = obj2;
                    connection2.SaveToFile(str2);
                    this.igxObject_0 = connection2 as IGxObject;
                }
                base.DialogResult = DialogResult.OK;
                base.Close();
            }
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmNewArcIMSServer));
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.groupBox2 = new GroupBox();
            this.checkEdit1 = new CheckBox();
            this.chkSaveUserNameAndPsw = new CheckBox();
            this.txtPassword = new Button();
            this.txtUser = new Button();
            this.label3 = new Label();
            this.label2 = new Label();
            this.btnCancel = new Button();
            this.btnGetAllResource = new Button();
            this.rdoResourceUse = new ComboBox();
            this.groupBox3 = new GroupBox();
            this.ResourcescheckedListBox = new CheckedListBox();
            this.btnOK = new Button();
            this.txtServer = new Button();
            this.label1 = new Label();
            this.groupBox2.SuspendLayout();
          
            this.groupBox3.SuspendLayout();
         
            base.SuspendLayout();
           
            this.imageList_0.TransparentColor = Color.Transparent;
           
            this.groupBox2.Controls.Add(this.checkEdit1);
            this.groupBox2.Controls.Add(this.chkSaveUserNameAndPsw);
            this.groupBox2.Controls.Add(this.txtPassword);
            this.groupBox2.Controls.Add(this.txtUser);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new Point(8, 280);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xfc, 0x94);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "帐户";
            this.checkEdit1.Location = new Point(20, 20);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Text = "显示认证服务";
            this.checkEdit1.Size = new Size(0x70, 0x13);
            this.checkEdit1.TabIndex = 5;
            this.chkSaveUserNameAndPsw.Location = new Point(0x10, 0x70);
            this.chkSaveUserNameAndPsw.Name = "chkSaveUserNameAndPsw";
            this.chkSaveUserNameAndPsw.Text= "保存用户名/密码";
            this.chkSaveUserNameAndPsw.Size = new Size(0x70, 0x13);
            this.chkSaveUserNameAndPsw.TabIndex = 4;
            this.txtPassword.Text="";
            this.txtPassword.Location = new Point(0x44, 0x4c);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new Size(120, 0x15);
            this.txtPassword.TabIndex = 3;
            this.txtUser.Text="";
            this.txtUser.Location = new Point(0x44, 0x2c);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new Size(120, 0x15);
            this.txtUser.TabIndex = 2;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x10, 80);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x2f, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "密  码:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x30);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x2f, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "用户名:";
            this.btnCancel.Location = new Point(0xbc, 0x1b4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(60, 0x1c);
            this.btnCancel.TabIndex = 0x11;
            this.btnCancel.Text = "取消";
            this.btnGetAllResource.Enabled = false;
            this.btnGetAllResource.Location = new Point(20, 0x48);
            this.btnGetAllResource.Name = "btnGetAllResource";
            this.btnGetAllResource.Size = new Size(0x5c, 0x18);
            this.btnGetAllResource.TabIndex = 0x15;
            this.btnGetAllResource.Text = "得到列表";
            this.btnGetAllResource.Click += new EventHandler(this.btnGetAllResource_Click);
            this.rdoResourceUse.Location = new Point(20, 0x18);
            this.rdoResourceUse.Name = "rdoResourceUse";
         
            this.rdoResourceUse.Items.AddRange(new object[] { "所有资源", "使用以下资源" });
            this.rdoResourceUse.Size = new Size(0x88, 40);
            this.rdoResourceUse.TabIndex = 0x13;
            this.rdoResourceUse.SelectedIndexChanged += new EventHandler(this.rdoResourceUse_SelectedIndexChanged);
            this.groupBox3.Controls.Add(this.ResourcescheckedListBox);
            this.groupBox3.Controls.Add(this.btnGetAllResource);
            this.groupBox3.Controls.Add(this.rdoResourceUse);
            this.groupBox3.Location = new Point(12, 0x34);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0xfc, 0xd0);
            this.groupBox3.TabIndex = 0x13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "选择所用资源";
            this.ResourcescheckedListBox.Location = new Point(20, 0x68);
            this.ResourcescheckedListBox.Name = "ResourcescheckedListBox";
            this.ResourcescheckedListBox.Size = new Size(0xc4, 0x54);
            this.ResourcescheckedListBox.TabIndex = 0x16;
            this.ResourcescheckedListBox.ItemCheck += new ItemCheckEventHandler(this.ResourcescheckedListBox_ItemCheck);
            this.btnOK.Enabled = false;
            this.btnOK.Location = new Point(0x70, 0x1b4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(60, 0x1c);
            this.btnOK.TabIndex = 20;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.txtServer.Text = "http://";
            this.txtServer.Location = new Point(0x54, 12);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new Size(120, 0x15);
            this.txtServer.TabIndex = 0x16;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x18, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x2f, 12);
            this.label1.TabIndex = 0x15;
            this.label1.Text = "服务器:";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x110, 470);
            base.Controls.Add(this.txtServer);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.groupBox2);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
           
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmNewArcIMSServer";
            this.Text = "添加ArcIMS服务器";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
          
            this.groupBox3.ResumeLayout(false);
          
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void method_0(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.imapServer_0 = null;
            try
            {
                IAGSServerConnectionFactory factory = new AGSServerConnectionFactory();
                IPropertySet pConnectionProperties = new PropertySet();
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

        private void method_3(object sender, EventArgs e)
        {
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
            return new PropertySet();
        }

        private void method_6(object sender, EventArgs e)
        {
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
                this.ResourcescheckedListBox.Enabled = true;
            }
        }

        private void ResourcescheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
        }

        public IGxObject NewObject
        {
            get
            {
                return this.igxObject_0;
            }
        }

        private class Class7
        {
            private IAGSServerObjectName iagsserverObjectName_0 = null;

            public Class7(IAGSServerObjectName iagsserverObjectName_1)
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

