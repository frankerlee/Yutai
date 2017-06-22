using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Yutai.ArcGIS.Common.Data;

namespace Yutai.ArcGIS.Controls.Controls.ConfigSetting
{
    partial class OLEDBSettingPropertyPage
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

       
        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.btnTestConnection = new SimpleButton();
            this.txtPassword = new TextEdit();
            this.txtUser = new TextEdit();
            this.txtDatabase = new TextEdit();
            this.txtServer = new TextEdit();
            this.label5 = new Label();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label1 = new Label();
            this.label2 = new Label();
            this.radioGroup1 = new RadioGroup();
            this.groupBox2 = new GroupBox();
            this.btnSelectMDB = new SimpleButton();
            this.txtMDB = new TextEdit();
            this.label12 = new Label();
            this.groupBox1.SuspendLayout();
            this.txtPassword.Properties.BeginInit();
            this.txtUser.Properties.BeginInit();
            this.txtDatabase.Properties.BeginInit();
            this.txtServer.Properties.BeginInit();
            this.radioGroup1.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.txtMDB.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnTestConnection);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtUser);
            this.groupBox1.Controls.Add(this.txtDatabase);
            this.groupBox1.Controls.Add(this.txtServer);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(16, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(327, 162);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "属性表连接串信息设置";
            this.groupBox1.Enter += new EventHandler(this.groupBox1_Enter);
            this.btnTestConnection.Location = new Point(257, 130);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new Size(64, 24);
            this.btnTestConnection.TabIndex = 12;
            this.btnTestConnection.Text = "测试连接";
            this.btnTestConnection.Click += new EventHandler(this.btnTestConnection_Click);
            this.txtPassword.EditValue = "";
            this.txtPassword.Location = new Point(81, 103);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Size = new Size(240, 21);
            this.txtPassword.TabIndex = 10;
            this.txtPassword.EditValueChanged += new EventHandler(this.txtPassword_EditValueChanged);
            this.txtUser.EditValue = "";
            this.txtUser.Location = new Point(81, 75);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new Size(240, 21);
            this.txtUser.TabIndex = 9;
            this.txtUser.EditValueChanged += new EventHandler(this.txtUser_EditValueChanged);
            this.txtDatabase.EditValue = "";
            this.txtDatabase.Location = new Point(81, 47);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new Size(240, 21);
            this.txtDatabase.TabIndex = 8;
            this.txtDatabase.EditValueChanged += new EventHandler(this.txtDatabase_EditValueChanged);
            this.txtServer.EditValue = "";
            this.txtServer.Location = new Point(81, 19);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new Size(240, 21);
            this.txtServer.TabIndex = 6;
            this.txtServer.EditValueChanged += new EventHandler(this.txtServer_EditValueChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(15, 106);
            this.label5.Name = "label5";
            this.label5.Size = new Size(35, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "密码:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(12, 79);
            this.label4.Name = "label4";
            this.label4.Size = new Size(35, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "用户:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(14, 49);
            this.label3.Name = "label3";
            this.label3.Size = new Size(47, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "数据库:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(14, 22);
            this.label1.Name = "label1";
            this.label1.Size = new Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(14, 19);
            this.label2.Name = "label2";
            this.label2.Size = new Size(71, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "数据库类型:";
            this.radioGroup1.Location = new Point(88, 9);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Columns = 3;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "SQLServer"), new RadioGroupItem(null, "Oracle"), new RadioGroupItem(null, "MDB") });
            this.radioGroup1.Size = new Size(255, 32);
            this.radioGroup1.TabIndex = 3;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            this.groupBox2.Controls.Add(this.btnSelectMDB);
            this.groupBox2.Controls.Add(this.txtMDB);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new Point(16, 228);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(327, 72);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "个人数据库配置";
            this.btnSelectMDB.Location = new Point(273, 45);
            this.btnSelectMDB.Name = "btnSelectMDB";
            this.btnSelectMDB.Size = new Size(48, 24);
            this.btnSelectMDB.TabIndex = 13;
            this.btnSelectMDB.Text = "更改";
            this.btnSelectMDB.Click += new EventHandler(this.btnSelectMDB_Click);
            this.txtMDB.EditValue = "";
            this.txtMDB.Location = new Point(72, 18);
            this.txtMDB.Name = "txtMDB";
            this.txtMDB.Properties.ReadOnly = true;
            this.txtMDB.Size = new Size(249, 21);
            this.txtMDB.TabIndex = 6;
            this.label12.AutoSize = true;
            this.label12.Location = new Point(16, 21);
            this.label12.Name = "label12";
            this.label12.Size = new Size(47, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "数据库:";
            base.Controls.Add(this.label2);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.radioGroup1);
            base.Name = "OLEDBSettingPropertyPage";
            base.Size = new Size(376, 336);
            base.Load += new EventHandler(this.GDBSettingPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.txtPassword.Properties.EndInit();
            this.txtUser.Properties.EndInit();
            this.txtDatabase.Properties.EndInit();
            this.txtServer.Properties.EndInit();
            this.radioGroup1.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.txtMDB.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Container components = null;
        private SimpleButton btnSelectMDB;
        private SimpleButton btnTestConnection;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label12;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private RadioGroup radioGroup1;
        private TextEdit txtDatabase;
        private TextEdit txtMDB;
        private TextEdit txtPassword;
        private TextEdit txtServer;
        private TextEdit txtUser;
    }
}