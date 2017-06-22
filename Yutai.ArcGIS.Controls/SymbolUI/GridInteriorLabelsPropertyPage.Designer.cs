using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class GridInteriorLabelsPropertyPage
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
            this.chkShowInteriorLabels = new CheckEdit();
            this.groupBox1 = new GroupBox();
            this.label9 = new Label();
            this.label8 = new Label();
            this.label7 = new Label();
            this.txtHatchIntervalYSecond = new TextEdit();
            this.txtHatchIntervalYMinute = new TextEdit();
            this.txtHatchIntervalXSecond = new TextEdit();
            this.txtHatchIntervalXMinute = new TextEdit();
            this.label3 = new Label();
            this.label4 = new Label();
            this.txtHatchIntervalYDegree = new TextEdit();
            this.txtHatchIntervalXDegree = new TextEdit();
            this.label5 = new Label();
            this.label6 = new Label();
            this.chkShowInteriorLabels.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.txtHatchIntervalYSecond.Properties.BeginInit();
            this.txtHatchIntervalYMinute.Properties.BeginInit();
            this.txtHatchIntervalXSecond.Properties.BeginInit();
            this.txtHatchIntervalXMinute.Properties.BeginInit();
            this.txtHatchIntervalYDegree.Properties.BeginInit();
            this.txtHatchIntervalXDegree.Properties.BeginInit();
            base.SuspendLayout();
            this.chkShowInteriorLabels.Location = new Point(24, 16);
            this.chkShowInteriorLabels.Name = "chkShowInteriorLabels";
            this.chkShowInteriorLabels.Properties.Caption = "显示内部格网标注";
            this.chkShowInteriorLabels.Size = new Size(144, 19);
            this.chkShowInteriorLabels.TabIndex = 0;
            this.chkShowInteriorLabels.CheckedChanged += new EventHandler(this.chkShowInteriorLabels_CheckedChanged);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtHatchIntervalYSecond);
            this.groupBox1.Controls.Add(this.txtHatchIntervalYMinute);
            this.groupBox1.Controls.Add(this.txtHatchIntervalXSecond);
            this.groupBox1.Controls.Add(this.txtHatchIntervalXMinute);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtHatchIntervalYDegree);
            this.groupBox1.Controls.Add(this.txtHatchIntervalXDegree);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new Point(24, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(248, 128);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "内部格网标注间隔";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(176, 24);
            this.label9.Name = "label9";
            this.label9.Size = new Size(17, 17);
            this.label9.TabIndex = 25;
            this.label9.Text = "秒";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(136, 24);
            this.label8.Name = "label8";
            this.label8.Size = new Size(17, 17);
            this.label8.TabIndex = 24;
            this.label8.Text = "分";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(96, 24);
            this.label7.Name = "label7";
            this.label7.Size = new Size(17, 17);
            this.label7.TabIndex = 23;
            this.label7.Text = "度";
            this.txtHatchIntervalYSecond.EditValue = "";
            this.txtHatchIntervalYSecond.Location = new Point(160, 80);
            this.txtHatchIntervalYSecond.Name = "txtHatchIntervalYSecond";
            this.txtHatchIntervalYSecond.Size = new Size(32, 23);
            this.txtHatchIntervalYSecond.TabIndex = 22;
            this.txtHatchIntervalYSecond.EditValueChanged += new EventHandler(this.txtHatchIntervalYSecond_EditValueChanged);
            this.txtHatchIntervalYMinute.EditValue = "";
            this.txtHatchIntervalYMinute.Location = new Point(120, 80);
            this.txtHatchIntervalYMinute.Name = "txtHatchIntervalYMinute";
            this.txtHatchIntervalYMinute.Size = new Size(32, 23);
            this.txtHatchIntervalYMinute.TabIndex = 21;
            this.txtHatchIntervalYMinute.EditValueChanged += new EventHandler(this.txtHatchIntervalYMinute_EditValueChanged);
            this.txtHatchIntervalXSecond.EditValue = "";
            this.txtHatchIntervalXSecond.Location = new Point(160, 48);
            this.txtHatchIntervalXSecond.Name = "txtHatchIntervalXSecond";
            this.txtHatchIntervalXSecond.Size = new Size(32, 23);
            this.txtHatchIntervalXSecond.TabIndex = 20;
            this.txtHatchIntervalXSecond.EditValueChanged += new EventHandler(this.txtHatchIntervalXSecond_EditValueChanged);
            this.txtHatchIntervalXMinute.EditValue = "";
            this.txtHatchIntervalXMinute.Location = new Point(120, 48);
            this.txtHatchIntervalXMinute.Name = "txtHatchIntervalXMinute";
            this.txtHatchIntervalXMinute.Size = new Size(32, 23);
            this.txtHatchIntervalXMinute.TabIndex = 19;
            this.txtHatchIntervalXMinute.EditValueChanged += new EventHandler(this.txtHatchIntervalXMinute_EditValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(160, 72);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0, 17);
            this.label3.TabIndex = 18;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(160, 48);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0, 17);
            this.label4.TabIndex = 17;
            this.txtHatchIntervalYDegree.EditValue = "";
            this.txtHatchIntervalYDegree.Location = new Point(64, 80);
            this.txtHatchIntervalYDegree.Name = "txtHatchIntervalYDegree";
            this.txtHatchIntervalYDegree.Size = new Size(48, 23);
            this.txtHatchIntervalYDegree.TabIndex = 16;
            this.txtHatchIntervalYDegree.EditValueChanged += new EventHandler(this.txtHatchIntervalYDegree_EditValueChanged);
            this.txtHatchIntervalXDegree.EditValue = "";
            this.txtHatchIntervalXDegree.Location = new Point(64, 48);
            this.txtHatchIntervalXDegree.Name = "txtHatchIntervalXDegree";
            this.txtHatchIntervalXDegree.Size = new Size(48, 23);
            this.txtHatchIntervalXDegree.TabIndex = 15;
            this.txtHatchIntervalXDegree.EditValueChanged += new EventHandler(this.txtHatchIntervalXDegree_EditValueChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(16, 80);
            this.label5.Name = "label5";
            this.label5.Size = new Size(29, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "经度";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(16, 48);
            this.label6.Name = "label6";
            this.label6.Size = new Size(29, 17);
            this.label6.TabIndex = 13;
            this.label6.Text = "纬度";
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.chkShowInteriorLabels);
            base.Name = "GridInteriorLabelsPropertyPage";
            base.Size = new Size(328, 240);
            base.Load += new EventHandler(this.GridInteriorLabelsPropertyPage_Load);
            this.chkShowInteriorLabels.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.txtHatchIntervalYSecond.Properties.EndInit();
            this.txtHatchIntervalYMinute.Properties.EndInit();
            this.txtHatchIntervalXSecond.Properties.EndInit();
            this.txtHatchIntervalXMinute.Properties.EndInit();
            this.txtHatchIntervalYDegree.Properties.EndInit();
            this.txtHatchIntervalXDegree.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private CheckEdit chkShowInteriorLabels;
        private GroupBox groupBox1;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private TextEdit txtHatchIntervalXDegree;
        private TextEdit txtHatchIntervalXMinute;
        private TextEdit txtHatchIntervalXSecond;
        private TextEdit txtHatchIntervalYDegree;
        private TextEdit txtHatchIntervalYMinute;
        private TextEdit txtHatchIntervalYSecond;
    }
}