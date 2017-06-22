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
    partial class NAWeightsPropertyPage
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
            this.cboJunWeight = new ComboBoxEdit();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.label3 = new Label();
            this.cboToFromEdgeWeight = new ComboBoxEdit();
            this.cbofromToEdgeWeight = new ComboBoxEdit();
            this.label2 = new Label();
            this.groupBox1.SuspendLayout();
            this.cboJunWeight.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.cboToFromEdgeWeight.Properties.BeginInit();
            this.cbofromToEdgeWeight.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.cboJunWeight);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(16, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(248, 88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "连接点权重";
            this.cboJunWeight.EditValue = "";
            this.cboJunWeight.Location = new System.Drawing.Point(16, 48);
            this.cboJunWeight.Name = "cboJunWeight";
            this.cboJunWeight.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboJunWeight.Size = new Size(208, 23);
            this.cboJunWeight.TabIndex = 1;
            this.cboJunWeight.SelectedIndexChanged += new EventHandler(this.cboJunWeight_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(103, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "连接点要素的权重";
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cboToFromEdgeWeight);
            this.groupBox2.Controls.Add(this.cbofromToEdgeWeight);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(16, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(248, 152);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "边权重";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 80);
            this.label3.Name = "label3";
            this.label3.Size = new Size(85, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "终点-起点权重";
            this.cboToFromEdgeWeight.EditValue = "";
            this.cboToFromEdgeWeight.Location = new System.Drawing.Point(20, 104);
            this.cboToFromEdgeWeight.Name = "cboToFromEdgeWeight";
            this.cboToFromEdgeWeight.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboToFromEdgeWeight.Size = new Size(208, 23);
            this.cboToFromEdgeWeight.TabIndex = 5;
            this.cboToFromEdgeWeight.SelectedIndexChanged += new EventHandler(this.cboToFromEdgeWeight_SelectedIndexChanged);
            this.cbofromToEdgeWeight.EditValue = "";
            this.cbofromToEdgeWeight.Location = new System.Drawing.Point(20, 48);
            this.cbofromToEdgeWeight.Name = "cbofromToEdgeWeight";
            this.cbofromToEdgeWeight.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cbofromToEdgeWeight.Size = new Size(208, 23);
            this.cbofromToEdgeWeight.TabIndex = 3;
            this.cbofromToEdgeWeight.SelectedIndexChanged += new EventHandler(this.cbofromToEdgeWeight_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 20);
            this.label2.Name = "label2";
            this.label2.Size = new Size(85, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "起点-终点权重";
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "NAWeightsPropertyPage";
            base.Size = new Size(312, 336);
            base.Load += new EventHandler(this.NAWeightsPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.cboJunWeight.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.cboToFromEdgeWeight.Properties.EndInit();
            this.cbofromToEdgeWeight.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private ComboBoxEdit cbofromToEdgeWeight;
        private ComboBoxEdit cboJunWeight;
        private ComboBoxEdit cboToFromEdgeWeight;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}