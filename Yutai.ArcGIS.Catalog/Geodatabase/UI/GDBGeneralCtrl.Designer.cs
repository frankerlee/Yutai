using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class GDBGeneralCtrl
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.textEditName = new TextEdit();
            this.lblType = new Label();
            this.groupBox1 = new GroupBox();
            this.btnUpdatePersonGDB = new SimpleButton();
            this.lblGDBRelease = new Label();
            this.groupBox2 = new GroupBox();
            this.btnUnRegister = new SimpleButton();
            this.btnProperty = new SimpleButton();
            this.textEditCheckOutName = new TextEdit();
            this.label3 = new Label();
            this.lblCheckOutInfo = new Label();
            this.lblConfigKey = new GroupBox();
            this.btnConfigKey = new SimpleButton();
            this.label4 = new Label();
            this.textEditName.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.textEditCheckOutName.Properties.BeginInit();
            this.lblConfigKey.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(16, 48);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "类型";
            this.textEditName.EditValue = "";
            this.textEditName.Location = new Point(51, 11);
            this.textEditName.Name = "textEditName";
            this.textEditName.Properties.ReadOnly = true;
            this.textEditName.Size = new Size(280, 21);
            this.textEditName.TabIndex = 2;
            this.lblType.Location = new Point(64, 48);
            this.lblType.Name = "lblType";
            this.lblType.Size = new Size(280, 24);
            this.lblType.TabIndex = 3;
            this.groupBox1.Controls.Add(this.btnUpdatePersonGDB);
            this.groupBox1.Controls.Add(this.lblGDBRelease);
            this.groupBox1.Location = new Point(16, 328);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(328, 112);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "升级状态";
            this.btnUpdatePersonGDB.Location = new Point(184, 80);
            this.btnUpdatePersonGDB.Name = "btnUpdatePersonGDB";
            this.btnUpdatePersonGDB.Size = new Size(128, 24);
            this.btnUpdatePersonGDB.TabIndex = 1;
            this.btnUpdatePersonGDB.Text = "升级个人数据库";
            this.btnUpdatePersonGDB.Click += new EventHandler(this.btnUpdatePersonGDB_Click);
            this.lblGDBRelease.Location = new Point(16, 24);
            this.lblGDBRelease.Name = "lblGDBRelease";
            this.lblGDBRelease.Size = new Size(288, 40);
            this.lblGDBRelease.TabIndex = 0;
            this.groupBox2.Controls.Add(this.btnUnRegister);
            this.groupBox2.Controls.Add(this.btnProperty);
            this.groupBox2.Controls.Add(this.textEditCheckOutName);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblCheckOutInfo);
            this.groupBox2.Location = new Point(16, 80);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(328, 136);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "离线编辑";
            this.btnUnRegister.Location = new Point(200, 104);
            this.btnUnRegister.Name = "btnUnRegister";
            this.btnUnRegister.Size = new Size(104, 24);
            this.btnUnRegister.TabIndex = 4;
            this.btnUnRegister.Text = "取消注册为检出";
            this.btnUnRegister.Click += new EventHandler(this.btnUnRegister_Click);
            this.btnProperty.Location = new Point(112, 104);
            this.btnProperty.Name = "btnProperty";
            this.btnProperty.Size = new Size(64, 24);
            this.btnProperty.TabIndex = 3;
            this.btnProperty.Text = "属性...";
            this.btnProperty.Click += new EventHandler(this.btnProperty_Click);
            this.textEditCheckOutName.EditValue = "";
            this.textEditCheckOutName.Location = new Point(80, 72);
            this.textEditCheckOutName.Name = "textEditCheckOutName";
            this.textEditCheckOutName.Size = new Size(216, 21);
            this.textEditCheckOutName.TabIndex = 2;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(16, 77);
            this.label3.Name = "label3";
            this.label3.Size = new Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "检出名称";
            this.lblCheckOutInfo.Location = new Point(16, 32);
            this.lblCheckOutInfo.Name = "lblCheckOutInfo";
            this.lblCheckOutInfo.Size = new Size(296, 32);
            this.lblCheckOutInfo.TabIndex = 0;
            this.lblConfigKey.Controls.Add(this.btnConfigKey);
            this.lblConfigKey.Controls.Add(this.label4);
            this.lblConfigKey.Location = new Point(16, 224);
            this.lblConfigKey.Name = "lblConfigKey";
            this.lblConfigKey.Size = new Size(328, 96);
            this.lblConfigKey.TabIndex = 6;
            this.lblConfigKey.TabStop = false;
            this.lblConfigKey.Text = "配置关键字";
            this.btnConfigKey.Location = new Point(136, 64);
            this.btnConfigKey.Name = "btnConfigKey";
            this.btnConfigKey.Size = new Size(112, 24);
            this.btnConfigKey.TabIndex = 1;
            this.btnConfigKey.Text = "配置关键字...";
            this.btnConfigKey.Click += new EventHandler(this.btnConfigKey_Click);
            this.label4.Location = new Point(16, 32);
            this.label4.Name = "label4";
            this.label4.Size = new Size(280, 24);
            this.label4.TabIndex = 0;
            base.Controls.Add(this.lblConfigKey);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.lblType);
            base.Controls.Add(this.textEditName);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "GDBGeneralCtrl";
            base.Size = new Size(352, 464);
            base.Load += new EventHandler(this.GDBGeneralCtrl_Load);
            this.textEditName.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.textEditCheckOutName.Properties.EndInit();
            this.lblConfigKey.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnConfigKey;
        private SimpleButton btnProperty;
        private SimpleButton btnUnRegister;
        private SimpleButton btnUpdatePersonGDB;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label lblCheckOutInfo;
        private GroupBox lblConfigKey;
        private Label lblGDBRelease;
        private Label lblType;
        private TextEdit textEditCheckOutName;
        private TextEdit textEditName;
    }
}