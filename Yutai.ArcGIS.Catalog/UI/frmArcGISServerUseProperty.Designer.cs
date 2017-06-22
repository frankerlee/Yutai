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
    partial class frmArcGISServerUseProperty
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
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.groupBox2 = new GroupBox();
            this.chkSaveUserNameAndPsw = new CheckEdit();
            this.txtPassword = new TextEdit();
            this.txtUser = new TextEdit();
            this.label3 = new Label();
            this.label2 = new Label();
            this.txtServer = new TextEdit();
            this.lblServerInfo = new Label();
            this.label5 = new Label();
            this.groupBox2.SuspendLayout();
            this.chkSaveUserNameAndPsw.Properties.BeginInit();
            this.txtPassword.Properties.BeginInit();
            this.txtUser.Properties.BeginInit();
            this.txtServer.Properties.BeginInit();
            base.SuspendLayout();
            this.btnOK.Location = new Point(228, 226);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(60, 28);
            this.btnOK.TabIndex = 24;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Location = new Point(313, 226);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(60, 28);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "取消";
            this.groupBox2.Controls.Add(this.chkSaveUserNameAndPsw);
            this.groupBox2.Controls.Add(this.txtPassword);
            this.groupBox2.Controls.Add(this.txtUser);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new Point(14, 93);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(388, 116);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "帐户";
            this.chkSaveUserNameAndPsw.EditValue = true;
            this.chkSaveUserNameAndPsw.Location = new Point(16, 88);
            this.chkSaveUserNameAndPsw.Name = "chkSaveUserNameAndPsw";
            this.chkSaveUserNameAndPsw.Properties.Caption = "保存用户名/密码";
            this.chkSaveUserNameAndPsw.Size = new Size(112, 19);
            this.chkSaveUserNameAndPsw.TabIndex = 4;
            this.txtPassword.EditValue = "";
            this.txtPassword.Location = new Point(68, 52);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Size = new Size(304, 21);
            this.txtPassword.TabIndex = 3;
            this.txtUser.EditValue = "";
            this.txtUser.Location = new Point(68, 20);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new Size(304, 21);
            this.txtUser.TabIndex = 2;
            this.txtUser.EditValueChanged += new EventHandler(this.txtUser_EditValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(16, 56);
            this.label3.Name = "label3";
            this.label3.Size = new Size(47, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "密  码:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(16, 24);
            this.label2.Name = "label2";
            this.label2.Size = new Size(47, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "用户名:";
            this.txtServer.EditValue = "http://";
            this.txtServer.Location = new Point(83, 16);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new Size(319, 21);
            this.txtServer.TabIndex = 16;
            this.lblServerInfo.AutoSize = true;
            this.lblServerInfo.Location = new Point(12, 19);
            this.lblServerInfo.Name = "lblServerInfo";
            this.lblServerInfo.Size = new Size(65, 12);
            this.lblServerInfo.TabIndex = 12;
            this.lblServerInfo.Text = "服务器URL:";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(81, 49);
            this.label5.Name = "label5";
            this.label5.Size = new Size(305, 12);
            this.label5.TabIndex = 23;
            this.label5.Text = "ArcGIS Server:http://myserver:6080/arcgis/services";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(433, 270);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.txtServer);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.lblServerInfo);
            base.Controls.Add(this.btnCancel);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmArcGISServerUseProperty";
            this.Text = "ArcGIS Server用户连接属性";
            base.Load += new EventHandler(this.frmArcGISServerUseProperty_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.chkSaveUserNameAndPsw.Properties.EndInit();
            this.txtPassword.Properties.EndInit();
            this.txtUser.Properties.EndInit();
            this.txtServer.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private bool bool_0;
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private CheckEdit chkSaveUserNameAndPsw;
        private GroupBox groupBox2;
        private IAGSServerConnection iagsserverConnection_0;
        private IAGSServerConnectionName iagsserverConnectionName_0;
        private IMapDescription imapDescription_0;
        private IMapServer imapServer_0;
        private IPropertySet ipropertySet_0;
        private Label label2;
        private Label label3;
        private Label label5;
        private Label lblServerInfo;
        private string string_0;
        private string string_1;
        private string string_2;
        private TextEdit txtPassword;
        private TextEdit txtServer;
        private TextEdit txtUser;
    }
}