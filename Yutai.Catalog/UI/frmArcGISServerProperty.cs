namespace Yutai.Catalog.UI
{
    using ESRI.ArcGIS.Carto;
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.GISClient;
   
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class frmArcGISServerProperty : Form
    {
        private ESRI.ArcGIS.esriSystem.Array array_0 = null;
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Button btnCancel;
        private Button btnGetAllResource;
        private Button btnOK;
        private CheckBox chkSaveUserNameAndPsw;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private IAGSServerConnection iagsserverConnection_0 = null;
        private IAGSServerConnectionName iagsserverConnectionName_0 = null;
        private IContainer icontainer_0;
        private IList ilist_0 = new ArrayList();
        private ImageList imageList_0;
        private IMapServer imapServer_0;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private CheckedListBox ResourcescheckedListBox;
        private string string_0 = "";
        private string string_1;
        private Button txtName;
        private Button txtPassword;
        private Button txtServer;
        private IContainer components;
        private ComboBox rdoServerType;
        private Label label5;
        private ComboBox rdoResourceUse;
        private Label label6;
        private Button txtUser;

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
                this.array_0 = obj2 as ESRI.ArcGIS.esriSystem.Array ;
                base.DialogResult = DialogResult.OK;
                base.Close();
            }
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
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

         //this.rdoServerType.Location = new Point(8, 40);
         //   this.rdoServerType.Name = "rdoServerType";
         //   this.rdoServerType.Properties.Appearance.BackColor = SystemColors.Control;
         //   this.rdoServerType.Properties.Appearance.Options.UseBackColor = true;
         //   this.rdoServerType.Properties.BorderStyle = BorderStyles.NoBorder;
         //   this.rdoServerType.Properties.Columns = 2;
         //   this.rdoServerType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "本地服务器"), new RadioGroupItem(null, "互联网服务器") });
         //   this.rdoServerType.Size = new Size(0xb8, 0x18);
         //   this.rdoServerType.TabIndex = 15;

            // this.rdoResourceUse.Location = new Point(20, 0x18);
            //this.rdoResourceUse.Name = "rdoResourceUse";
            //this.rdoResourceUse.Properties.Appearance.BackColor = SystemColors.Control;
            //this.rdoResourceUse.Properties.Appearance.Options.UseBackColor = true;
            //this.rdoResourceUse.Properties.BorderStyle = BorderStyles.NoBorder;
            //this.rdoResourceUse.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "所有资源"), new RadioGroupItem(null, "只使用下列资源") });
            //this.rdoResourceUse.Size = new Size(0x88, 40);
            //this.rdoResourceUse.TabIndex = 0x13;
            //this.rdoResourceUse.SelectedIndexChanged += new EventHandler(this.rdoResourceUse_SelectedIndexChanged);

    private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.imageList_0 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtName = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkSaveUserNameAndPsw = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.Button();
            this.txtUser = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnGetAllResource = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ResourcescheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.rdoServerType = new System.Windows.Forms.ComboBox();
            this.rdoResourceUse = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList_0
            // 
            this.imageList_0.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList_0.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList_0.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoServerType);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtServer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 100);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "连接ArcGIS Server服务器";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(72, 16);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(120, 21);
            this.txtName.TabIndex = 18;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_EditValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "名字:";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(73, 69);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(120, 21);
            this.txtServer.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "服务器:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkSaveUserNameAndPsw);
            this.groupBox2.Controls.Add(this.txtPassword);
            this.groupBox2.Controls.Add(this.txtUser);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(8, 336);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(252, 116);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "帐户";
            // 
            // chkSaveUserNameAndPsw
            // 
            this.chkSaveUserNameAndPsw.Location = new System.Drawing.Point(16, 88);
            this.chkSaveUserNameAndPsw.Name = "chkSaveUserNameAndPsw";
            this.chkSaveUserNameAndPsw.Size = new System.Drawing.Size(148, 19);
            this.chkSaveUserNameAndPsw.TabIndex = 4;
            this.chkSaveUserNameAndPsw.Text = "保存用户名/密码";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(68, 52);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(120, 21);
            this.txtPassword.TabIndex = 3;
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(68, 20);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(120, 21);
            this.txtUser.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "密  码:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "用户名:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(188, 460);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 28);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "取消";
            // 
            // btnGetAllResource
            // 
            this.btnGetAllResource.Enabled = false;
            this.btnGetAllResource.Location = new System.Drawing.Point(20, 72);
            this.btnGetAllResource.Name = "btnGetAllResource";
            this.btnGetAllResource.Size = new System.Drawing.Size(92, 24);
            this.btnGetAllResource.TabIndex = 21;
            this.btnGetAllResource.Text = "得到列表";
            this.btnGetAllResource.Click += new System.EventHandler(this.btnGetAllResource_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdoResourceUse);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.ResourcescheckedListBox);
            this.groupBox3.Controls.Add(this.btnGetAllResource);
            this.groupBox3.Location = new System.Drawing.Point(8, 116);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(252, 208);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "选择所用资源";
            // 
            // ResourcescheckedListBox
            // 
            this.ResourcescheckedListBox.Location = new System.Drawing.Point(20, 104);
            this.ResourcescheckedListBox.Name = "ResourcescheckedListBox";
            this.ResourcescheckedListBox.Size = new System.Drawing.Size(196, 84);
            this.ResourcescheckedListBox.TabIndex = 22;
            // 
            // btnOK
            // 
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(112, 460);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(60, 28);
            this.btnOK.TabIndex = 20;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "服务类型:";
            // 
            // rdoServerType
            // 
            this.rdoServerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rdoServerType.FormattingEnabled = true;
            this.rdoServerType.Items.AddRange(new object[] {
            "本地服务",
            "互联网服务"});
            this.rdoServerType.Location = new System.Drawing.Point(73, 43);
            this.rdoServerType.Name = "rdoServerType";
            this.rdoServerType.Size = new System.Drawing.Size(118, 20);
            this.rdoServerType.TabIndex = 20;
            // 
            // rdoResourceUse
            // 
            this.rdoResourceUse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rdoResourceUse.FormattingEnabled = true;
            this.rdoResourceUse.Items.AddRange(new object[] {
            "所有资源",
            "只使用下列资源"});
            this.rdoResourceUse.Location = new System.Drawing.Point(85, 31);
            this.rdoResourceUse.Name = "rdoResourceUse";
            this.rdoResourceUse.Size = new System.Drawing.Size(131, 20);
            this.rdoResourceUse.TabIndex = 24;
            this.rdoResourceUse.SelectedIndexChanged += new System.EventHandler(this.rdoResourceUse_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 23;
            this.label6.Text = "服务类型:";
            // 
            // frmArcGISServerProperty
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(272, 496);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmArcGISServerProperty";
            this.Text = "ArcGIS Server连接属性";
            this.Load += new System.EventHandler(this.frmArcGISServerProperty_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

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
            IPropertySet set = new PropertySet();
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
            set.SetProperty("HIDEUSERPROPERTY", this.chkSaveUserNameAndPsw.Text);
            return set;
        }

        private bool method_6(string string_2)
        {
            if (this.array_0 != null)
            {
                for (int i = 0; i < this.array_0.Count; i++)
                {
                    string str = (string) this.array_0.Element[i].ToString();
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

        public ESRI.ArcGIS.esriSystem.Array SelectObjectNames
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

        private class Class5
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

