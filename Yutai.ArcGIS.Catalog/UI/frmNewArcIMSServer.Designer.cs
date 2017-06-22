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
    partial class frmNewArcIMSServer
    {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewArcIMSServer));
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.groupBox2 = new GroupBox();
            this.checkEdit1 = new CheckEdit();
            this.chkSaveUserNameAndPsw = new CheckEdit();
            this.txtPassword = new TextEdit();
            this.txtUser = new TextEdit();
            this.label3 = new Label();
            this.label2 = new Label();
            this.btnCancel = new SimpleButton();
            this.btnGetAllResource = new SimpleButton();
            this.rdoResourceUse = new RadioGroup();
            this.groupBox3 = new GroupBox();
            this.ResourcescheckedListBox = new CheckedListBox();
            this.btnOK = new SimpleButton();
            this.txtServer = new TextEdit();
            this.label1 = new Label();
            this.groupBox2.SuspendLayout();
            this.checkEdit1.Properties.BeginInit();
            this.chkSaveUserNameAndPsw.Properties.BeginInit();
            this.txtPassword.Properties.BeginInit();
            this.txtUser.Properties.BeginInit();
            this.rdoResourceUse.Properties.BeginInit();
            this.groupBox3.SuspendLayout();
            this.txtServer.Properties.BeginInit();
            base.SuspendLayout();
            this.imageList_0.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Transparent;
            this.imageList_0.Images.SetKeyName(0, "");
            this.groupBox2.Controls.Add(this.checkEdit1);
            this.groupBox2.Controls.Add(this.chkSaveUserNameAndPsw);
            this.groupBox2.Controls.Add(this.txtPassword);
            this.groupBox2.Controls.Add(this.txtUser);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new Point(8, 280);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(252, 148);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "帐户";
            this.checkEdit1.Location = new Point(20, 20);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "显示认证服务";
            this.checkEdit1.Size = new Size(112, 19);
            this.checkEdit1.TabIndex = 5;
            this.chkSaveUserNameAndPsw.Location = new Point(16, 112);
            this.chkSaveUserNameAndPsw.Name = "chkSaveUserNameAndPsw";
            this.chkSaveUserNameAndPsw.Properties.Caption = "保存用户名/密码";
            this.chkSaveUserNameAndPsw.Size = new Size(112, 19);
            this.chkSaveUserNameAndPsw.TabIndex = 4;
            this.txtPassword.EditValue = "";
            this.txtPassword.Location = new Point(68, 76);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new Size(120, 21);
            this.txtPassword.TabIndex = 3;
            this.txtUser.EditValue = "";
            this.txtUser.Location = new Point(68, 44);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new Size(120, 21);
            this.txtUser.TabIndex = 2;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(16, 80);
            this.label3.Name = "label3";
            this.label3.Size = new Size(47, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "密  码:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(16, 48);
            this.label2.Name = "label2";
            this.label2.Size = new Size(47, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "用户名:";
            this.btnCancel.Location = new Point(188, 436);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(60, 28);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "取消";
            this.btnGetAllResource.Enabled = false;
            this.btnGetAllResource.Location = new Point(20, 72);
            this.btnGetAllResource.Name = "btnGetAllResource";
            this.btnGetAllResource.Size = new Size(92, 24);
            this.btnGetAllResource.TabIndex = 21;
            this.btnGetAllResource.Text = "得到列表";
            this.btnGetAllResource.Click += new EventHandler(this.btnGetAllResource_Click);
            this.rdoResourceUse.Location = new Point(20, 24);
            this.rdoResourceUse.Name = "rdoResourceUse";
            this.rdoResourceUse.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoResourceUse.Properties.Appearance.Options.UseBackColor = true;
            this.rdoResourceUse.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoResourceUse.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "所有资源"), new RadioGroupItem(null, "使用以下资源") });
            this.rdoResourceUse.Size = new Size(136, 40);
            this.rdoResourceUse.TabIndex = 19;
            this.rdoResourceUse.SelectedIndexChanged += new EventHandler(this.rdoResourceUse_SelectedIndexChanged);
            this.groupBox3.Controls.Add(this.ResourcescheckedListBox);
            this.groupBox3.Controls.Add(this.btnGetAllResource);
            this.groupBox3.Controls.Add(this.rdoResourceUse);
            this.groupBox3.Location = new Point(12, 52);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(252, 208);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "选择所用资源";
            this.ResourcescheckedListBox.Location = new Point(20, 104);
            this.ResourcescheckedListBox.Name = "ResourcescheckedListBox";
            this.ResourcescheckedListBox.Size = new Size(196, 84);
            this.ResourcescheckedListBox.TabIndex = 22;
            this.ResourcescheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ResourcescheckedListBox_ItemCheck);
            this.btnOK.Enabled = false;
            this.btnOK.Location = new Point(112, 436);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(60, 28);
            this.btnOK.TabIndex = 20;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.txtServer.EditValue = "http://";
            this.txtServer.Location = new Point(84, 12);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new Size(120, 21);
            this.txtServer.TabIndex = 22;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(24, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(47, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "服务器:";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(272, 470);
            base.Controls.Add(this.txtServer);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.groupBox2);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmNewArcIMSServer";
            this.Text = "添加ArcIMS服务器";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.checkEdit1.Properties.EndInit();
            this.chkSaveUserNameAndPsw.Properties.EndInit();
            this.txtPassword.Properties.EndInit();
            this.txtUser.Properties.EndInit();
            this.rdoResourceUse.Properties.EndInit();
            this.groupBox3.ResumeLayout(false);
            this.txtServer.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private bool bool_0;
        private SimpleButton btnCancel;
        private SimpleButton btnGetAllResource;
        private SimpleButton btnOK;
        private CheckEdit checkEdit1;
        private CheckEdit chkSaveUserNameAndPsw;
        private double double_0;
        private double double_1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private IAGSServerConnection iagsserverConnection_0;
        private IContainer icontainer_0;
        private IImageDisplay iimageDisplay_0;
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
        private RadioGroup rdoResourceUse;
        private CheckedListBox ResourcescheckedListBox;
        private string string_0;
        private string string_1;
        private TextEdit txtPassword;
        private TextEdit txtServer;
        private TextEdit txtUser;
    }
}