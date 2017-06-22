using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    partial class NAWeightFilterPropertyPage
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
            this.groupBox2 = new GroupBox();
            this.btnValiateEFWRange = new SimpleButton();
            this.chkEdgeapplyNot = new CheckEdit();
            this.txtEFWRange = new TextEdit();
            this.label5 = new Label();
            this.label3 = new Label();
            this.cboToFromEdgeWeight = new ComboBoxEdit();
            this.cbofromToEdgeWeight = new ComboBoxEdit();
            this.label2 = new Label();
            this.groupBox3 = new GroupBox();
            this.btnValiteJFWRange = new SimpleButton();
            this.chkJunapplyNot = new CheckEdit();
            this.txtJFWRange = new TextEdit();
            this.label1 = new Label();
            this.cboJunWeight = new ComboBoxEdit();
            this.label4 = new Label();
            this.groupBox2.SuspendLayout();
            this.chkEdgeapplyNot.Properties.BeginInit();
            this.txtEFWRange.Properties.BeginInit();
            this.cboToFromEdgeWeight.Properties.BeginInit();
            this.cbofromToEdgeWeight.Properties.BeginInit();
            this.groupBox3.SuspendLayout();
            this.chkJunapplyNot.Properties.BeginInit();
            this.txtJFWRange.Properties.BeginInit();
            this.cboJunWeight.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox2.Controls.Add(this.btnValiateEFWRange);
            this.groupBox2.Controls.Add(this.chkEdgeapplyNot);
            this.groupBox2.Controls.Add(this.txtEFWRange);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cboToFromEdgeWeight);
            this.groupBox2.Controls.Add(this.cbofromToEdgeWeight);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(16, 144);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(248, 144);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "边权重过滤器";
            this.btnValiateEFWRange.Location = new System.Drawing.Point(168, 112);
            this.btnValiateEFWRange.Name = "btnValiateEFWRange";
            this.btnValiateEFWRange.Size = new Size(64, 24);
            this.btnValiateEFWRange.TabIndex = 12;
            this.btnValiateEFWRange.Text = "验证";
            this.chkEdgeapplyNot.Location = new System.Drawing.Point(112, 112);
            this.chkEdgeapplyNot.Name = "chkEdgeapplyNot";
            this.chkEdgeapplyNot.Properties.Caption = "否";
            this.chkEdgeapplyNot.Size = new Size(48, 19);
            this.chkEdgeapplyNot.TabIndex = 9;
            this.chkEdgeapplyNot.CheckedChanged += new EventHandler(this.chkEdgeapplyNot_CheckedChanged);
            this.txtEFWRange.EditValue = "";
            this.txtEFWRange.Location = new System.Drawing.Point(112, 80);
            this.txtEFWRange.Name = "txtEFWRange";
            this.txtEFWRange.Size = new Size(120, 23);
            this.txtEFWRange.TabIndex = 8;
            this.txtEFWRange.EditValueChanged += new EventHandler(this.txtEFWRange_EditValueChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 80);
            this.label5.Name = "label5";
            this.label5.Size = new Size(54, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "权重范围";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 48);
            this.label3.Name = "label3";
            this.label3.Size = new Size(85, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "终点-起点权重";
            this.cboToFromEdgeWeight.EditValue = "";
            this.cboToFromEdgeWeight.Location = new System.Drawing.Point(112, 48);
            this.cboToFromEdgeWeight.Name = "cboToFromEdgeWeight";
            this.cboToFromEdgeWeight.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboToFromEdgeWeight.Size = new Size(120, 23);
            this.cboToFromEdgeWeight.TabIndex = 5;
            this.cboToFromEdgeWeight.SelectedIndexChanged += new EventHandler(this.cboToFromEdgeWeight_SelectedIndexChanged);
            this.cbofromToEdgeWeight.EditValue = "";
            this.cbofromToEdgeWeight.Location = new System.Drawing.Point(112, 16);
            this.cbofromToEdgeWeight.Name = "cbofromToEdgeWeight";
            this.cbofromToEdgeWeight.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cbofromToEdgeWeight.Size = new Size(120, 23);
            this.cbofromToEdgeWeight.TabIndex = 3;
            this.cbofromToEdgeWeight.SelectedIndexChanged += new EventHandler(this.cbofromToEdgeWeight_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 20);
            this.label2.Name = "label2";
            this.label2.Size = new Size(85, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "起点-终点权重";
            this.groupBox3.Controls.Add(this.btnValiteJFWRange);
            this.groupBox3.Controls.Add(this.chkJunapplyNot);
            this.groupBox3.Controls.Add(this.txtJFWRange);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.cboJunWeight);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(16, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(248, 120);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "连接点权重过滤器";
            this.btnValiteJFWRange.Location = new System.Drawing.Point(152, 80);
            this.btnValiteJFWRange.Name = "btnValiteJFWRange";
            this.btnValiteJFWRange.Size = new Size(64, 24);
            this.btnValiteJFWRange.TabIndex = 11;
            this.btnValiteJFWRange.Text = "验证";
            this.chkJunapplyNot.Location = new System.Drawing.Point(88, 88);
            this.chkJunapplyNot.Name = "chkJunapplyNot";
            this.chkJunapplyNot.Properties.Caption = "否";
            this.chkJunapplyNot.Size = new Size(48, 19);
            this.chkJunapplyNot.TabIndex = 10;
            this.chkJunapplyNot.CheckedChanged += new EventHandler(this.chkJunapplyNot_CheckedChanged);
            this.txtJFWRange.EditValue = "";
            this.txtJFWRange.Location = new System.Drawing.Point(88, 48);
            this.txtJFWRange.Name = "txtJFWRange";
            this.txtJFWRange.Size = new Size(144, 23);
            this.txtJFWRange.TabIndex = 3;
            this.txtJFWRange.EditValueChanged += new EventHandler(this.txtJFWRange_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 56);
            this.label1.Name = "label1";
            this.label1.Size = new Size(54, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "权重范围";
            this.cboJunWeight.EditValue = "";
            this.cboJunWeight.Location = new System.Drawing.Point(88, 18);
            this.cboJunWeight.Name = "cboJunWeight";
            this.cboJunWeight.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboJunWeight.Size = new Size(144, 23);
            this.cboJunWeight.TabIndex = 1;
            this.cboJunWeight.SelectedIndexChanged += new EventHandler(this.cboJunWeight_SelectedIndexChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 24);
            this.label4.Name = "label4";
            this.label4.Size = new Size(66, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "连接点权重";
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox3);
            base.Name = "NAWeightFilterPropertyPage";
            base.Size = new Size(312, 312);
            base.Load += new EventHandler(this.NAWeightFilterPropertyPage_Load);
            this.groupBox2.ResumeLayout(false);
            this.chkEdgeapplyNot.Properties.EndInit();
            this.txtEFWRange.Properties.EndInit();
            this.cboToFromEdgeWeight.Properties.EndInit();
            this.cbofromToEdgeWeight.Properties.EndInit();
            this.groupBox3.ResumeLayout(false);
            this.chkJunapplyNot.Properties.EndInit();
            this.txtJFWRange.Properties.EndInit();
            this.cboJunWeight.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private SimpleButton btnValiateEFWRange;
        private SimpleButton btnValiteJFWRange;
        private ComboBoxEdit cbofromToEdgeWeight;
        private ComboBoxEdit cboJunWeight;
        private ComboBoxEdit cboToFromEdgeWeight;
        private CheckEdit chkEdgeapplyNot;
        private CheckEdit chkJunapplyNot;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextEdit txtEFWRange;
        private TextEdit txtJFWRange;
    }
}