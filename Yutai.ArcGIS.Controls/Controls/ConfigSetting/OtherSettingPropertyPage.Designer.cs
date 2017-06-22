using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Controls.ConfigSetting
{
    partial class OtherSettingPropertyPage
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
            this.btnSelectTable = new SimpleButton();
            this.txtLayerName = new TextEdit();
            this.label1 = new Label();
            this.checkEditLogin = new CheckEdit();
            this.checkEditInit = new CheckEdit();
            this.groupBox2 = new GroupBox();
            this.simpleButton1 = new SimpleButton();
            this.textEdit1 = new TextEdit();
            this.label2 = new Label();
            this.groupBox1.SuspendLayout();
            this.txtLayerName.Properties.BeginInit();
            this.checkEditLogin.Properties.BeginInit();
            this.checkEditInit.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.textEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnSelectTable);
            this.groupBox1.Controls.Add(this.txtLayerName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(16, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(232, 80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图层配置表信息设置";
            this.btnSelectTable.Location = new Point(120, 48);
            this.btnSelectTable.Name = "btnSelectTable";
            this.btnSelectTable.Size = new Size(72, 24);
            this.btnSelectTable.TabIndex = 3;
            this.btnSelectTable.Text = "选择表...";
            this.btnSelectTable.Click += new EventHandler(this.btnSelectTable_Click);
            this.txtLayerName.EditValue = "";
            this.txtLayerName.Location = new Point(48, 16);
            this.txtLayerName.Name = "txtLayerName";
            this.txtLayerName.Size = new Size(144, 21);
            this.txtLayerName.TabIndex = 2;
            this.txtLayerName.EditValueChanged += new EventHandler(this.txtLayerName_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "表名";
            this.checkEditLogin.Location = new Point(14, 114);
            this.checkEditLogin.Name = "checkEditLogin";
            this.checkEditLogin.Properties.Caption = "启用登录窗体";
            this.checkEditLogin.Size = new Size(120, 19);
            this.checkEditLogin.TabIndex = 1;
            this.checkEditLogin.CheckedChanged += new EventHandler(this.checkEditLogin_CheckedChanged);
            this.checkEditInit.Location = new Point(14, 320);
            this.checkEditInit.Name = "checkEditInit";
            this.checkEditInit.Properties.Caption = "初始化菜单文件";
            this.checkEditInit.Size = new Size(128, 19);
            this.checkEditInit.TabIndex = 2;
            this.checkEditInit.Visible = false;
            this.checkEditInit.CheckedChanged += new EventHandler(this.checkEditInit_CheckedChanged);
            this.groupBox2.Controls.Add(this.simpleButton1);
            this.groupBox2.Controls.Add(this.textEdit1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new Point(16, 308);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(232, 74);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "菜单配置表设置";
            this.groupBox2.Visible = false;
            this.simpleButton1.Location = new Point(120, 48);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(72, 24);
            this.simpleButton1.TabIndex = 3;
            this.simpleButton1.Text = "选择表...";
            this.simpleButton1.Click += new EventHandler(this.simpleButton1_Click_1);
            this.textEdit1.EditValue = "";
            this.textEdit1.Location = new Point(48, 16);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new Size(144, 21);
            this.textEdit1.TabIndex = 2;
            this.textEdit1.EditValueChanged += new EventHandler(this.textEdit1_EditValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 24);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "表名";
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.checkEditInit);
            base.Controls.Add(this.checkEditLogin);
            base.Controls.Add(this.groupBox1);
            base.Name = "OtherSettingPropertyPage";
            base.Size = new Size(264, 296);
            base.Load += new EventHandler(this.OtherSettingPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.txtLayerName.Properties.EndInit();
            this.checkEditLogin.Properties.EndInit();
            this.checkEditInit.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.textEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private SimpleButton btnSelectTable;
        private CheckEdit checkEditInit;
        private CheckEdit checkEditLogin;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private SimpleButton simpleButton1;
        private TextEdit textEdit1;
        private TextEdit txtLayerName;
    }
}