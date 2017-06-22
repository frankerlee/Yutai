using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Yutai.ArcGIS.Common.ExtendClass;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmJTBElement
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
            this.groupBox1 = new GroupBox();
            this.txtRB = new TextEdit();
            this.txtB = new TextEdit();
            this.txtLB = new TextEdit();
            this.txtR = new TextEdit();
            this.txtC = new TextEdit();
            this.txtL = new TextEdit();
            this.txtRT = new TextEdit();
            this.txtT = new TextEdit();
            this.txtLT = new TextEdit();
            this.btnOK = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.groupBox1.SuspendLayout();
            this.txtRB.Properties.BeginInit();
            this.txtB.Properties.BeginInit();
            this.txtLB.Properties.BeginInit();
            this.txtR.Properties.BeginInit();
            this.txtC.Properties.BeginInit();
            this.txtL.Properties.BeginInit();
            this.txtRT.Properties.BeginInit();
            this.txtT.Properties.BeginInit();
            this.txtLT.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.txtRB);
            this.groupBox1.Controls.Add(this.txtB);
            this.groupBox1.Controls.Add(this.txtLB);
            this.groupBox1.Controls.Add(this.txtR);
            this.groupBox1.Controls.Add(this.txtC);
            this.groupBox1.Controls.Add(this.txtL);
            this.groupBox1.Controls.Add(this.txtRT);
            this.groupBox1.Controls.Add(this.txtT);
            this.groupBox1.Controls.Add(this.txtLT);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(304, 128);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "接图表";
            this.txtRB.EditValue = "";
            this.txtRB.Location = new Point(200, 88);
            this.txtRB.Name = "txtRB";
            this.txtRB.Size = new Size(88, 23);
            this.txtRB.TabIndex = 8;
            this.txtB.EditValue = "";
            this.txtB.Location = new Point(104, 88);
            this.txtB.Name = "txtB";
            this.txtB.Size = new Size(88, 23);
            this.txtB.TabIndex = 7;
            this.txtLB.EditValue = "";
            this.txtLB.Location = new Point(8, 88);
            this.txtLB.Name = "txtLB";
            this.txtLB.Size = new Size(88, 23);
            this.txtLB.TabIndex = 6;
            this.txtR.EditValue = "";
            this.txtR.Location = new Point(200, 56);
            this.txtR.Name = "txtR";
            this.txtR.Size = new Size(88, 23);
            this.txtR.TabIndex = 5;
            this.txtC.EditValue = "";
            this.txtC.Location = new Point(104, 56);
            this.txtC.Name = "txtC";
            this.txtC.Properties.Appearance.BackColor = SystemColors.HighlightText;
            this.txtC.Properties.Appearance.Options.UseBackColor = true;
            this.txtC.Size = new Size(88, 23);
            this.txtC.TabIndex = 4;
            this.txtL.EditValue = "";
            this.txtL.Location = new Point(8, 56);
            this.txtL.Name = "txtL";
            this.txtL.Size = new Size(88, 23);
            this.txtL.TabIndex = 3;
            this.txtRT.EditValue = "";
            this.txtRT.Location = new Point(200, 24);
            this.txtRT.Name = "txtRT";
            this.txtRT.Size = new Size(88, 23);
            this.txtRT.TabIndex = 2;
            this.txtT.EditValue = "";
            this.txtT.Location = new Point(104, 24);
            this.txtT.Name = "txtT";
            this.txtT.Size = new Size(88, 23);
            this.txtT.TabIndex = 1;
            this.txtLT.EditValue = "";
            this.txtLT.Location = new Point(8, 24);
            this.txtLT.Name = "txtLT";
            this.txtLT.Size = new Size(88, 23);
            this.txtLT.TabIndex = 0;
            this.btnOK.Location = new Point(168, 144);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(64, 24);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new Point(248, 144);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(64, 24);
            this.simpleButton2.TabIndex = 2;
            this.simpleButton2.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(328, 189);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox1);
            base.Name = "frmJTBElement";
            this.Text = "接图表属性";
            base.Load += new EventHandler(this.frmJTBElement_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtRB.Properties.EndInit();
            this.txtB.Properties.EndInit();
            this.txtLB.Properties.EndInit();
            this.txtR.Properties.EndInit();
            this.txtC.Properties.EndInit();
            this.txtL.Properties.EndInit();
            this.txtRT.Properties.EndInit();
            this.txtT.Properties.EndInit();
            this.txtLT.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnOK;
        private GroupBox groupBox1;
        private SimpleButton simpleButton2;
        private TextEdit txtB;
        private TextEdit txtC;
        private TextEdit txtL;
        private TextEdit txtLB;
        private TextEdit txtLT;
        private TextEdit txtR;
        private TextEdit txtRB;
        private TextEdit txtRT;
        private TextEdit txtT;
    }
}