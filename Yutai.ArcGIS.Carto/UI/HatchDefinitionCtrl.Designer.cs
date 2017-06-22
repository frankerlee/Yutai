using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Location;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class HatchDefinitionCtrl
    {
        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

       
        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.panel1 = new Panel();
            this.txtLength = new TextEdit();
            this.label3 = new Label();
            this.simpleButton1 = new SimpleButton();
            this.txtOffset = new TextEdit();
            this.label4 = new Label();
            this.btnHatchSymbol = new StyleButton();
            this.radioGroup1 = new RadioGroup();
            this.groupBox2 = new GroupBox();
            this.simpleButton2 = new SimpleButton();
            this.btnTextSymbol = new StyleButton();
            this.chkTextSymbol = new CheckEdit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.txtLength.Properties.BeginInit();
            this.txtOffset.Properties.BeginInit();
            this.radioGroup1.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.chkTextSymbol.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.simpleButton1);
            this.groupBox1.Controls.Add(this.txtOffset);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnHatchSymbol);
            this.groupBox1.Controls.Add(this.radioGroup1);
            this.groupBox1.Location = new Point(8, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(280, 112);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "刻度";
            this.panel1.Controls.Add(this.txtLength);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new Point(8, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(264, 40);
            this.panel1.TabIndex = 7;
            this.txtLength.EditValue = "0";
            this.txtLength.Location = new Point(64, 8);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new Size(80, 23);
            this.txtLength.TabIndex = 5;
            this.txtLength.EditValueChanged += new EventHandler(this.txtLength_EditValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 16);
            this.label3.Name = "label3";
            this.label3.Size = new Size(42, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "线长度";
            this.simpleButton1.Location = new Point(160, 80);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(88, 24);
            this.simpleButton1.TabIndex = 6;
            this.simpleButton1.Text = "刻度线方向...";
            this.simpleButton1.Click += new EventHandler(this.simpleButton1_Click);
            this.txtOffset.EditValue = "0";
            this.txtOffset.Location = new Point(72, 80);
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.Size = new Size(80, 23);
            this.txtOffset.TabIndex = 5;
            this.txtOffset.EditValueChanged += new EventHandler(this.txtOffset_EditValueChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 88);
            this.label4.Name = "label4";
            this.label4.Size = new Size(42, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "偏移量";
            this.btnHatchSymbol.Location = new Point(120, 16);
            this.btnHatchSymbol.Name = "btnHatchSymbol";
            this.btnHatchSymbol.Size = new Size(96, 24);
            this.btnHatchSymbol.Style = null;
            this.btnHatchSymbol.TabIndex = 1;
            this.radioGroup1.Location = new Point(8, 16);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Columns = 2;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "线"), new RadioGroupItem(null, "点") });
            this.radioGroup1.Size = new Size(112, 24);
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            this.groupBox2.Controls.Add(this.simpleButton2);
            this.groupBox2.Controls.Add(this.btnTextSymbol);
            this.groupBox2.Controls.Add(this.chkTextSymbol);
            this.groupBox2.Location = new Point(8, 123);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(280, 72);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "标注";
            this.simpleButton2.Location = new Point(128, 40);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(80, 24);
            this.simpleButton2.TabIndex = 7;
            this.simpleButton2.Text = "标注设置...";
            this.simpleButton2.Click += new EventHandler(this.simpleButton2_Click);
            this.btnTextSymbol.Location = new Point(16, 40);
            this.btnTextSymbol.Name = "btnTextSymbol";
            this.btnTextSymbol.Size = new Size(96, 24);
            this.btnTextSymbol.Style = null;
            this.btnTextSymbol.TabIndex = 2;
            this.chkTextSymbol.Location = new Point(16, 16);
            this.chkTextSymbol.Name = "chkTextSymbol";
            this.chkTextSymbol.Properties.Caption = "标注刻度线";
            this.chkTextSymbol.Size = new Size(88, 19);
            this.chkTextSymbol.TabIndex = 0;
            this.chkTextSymbol.CheckedChanged += new EventHandler(this.chkTextSymbol_CheckedChanged);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "HatchDefinitionCtrl";
            base.Size = new Size(296, 200);
            base.Load += new EventHandler(this.HatchDefinitionCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.txtLength.Properties.EndInit();
            this.txtOffset.Properties.EndInit();
            this.radioGroup1.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.chkTextSymbol.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private StyleButton btnHatchSymbol;
        private StyleButton btnTextSymbol;
        private CheckEdit chkTextSymbol;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label3;
        private Label label4;
        private Panel panel1;
        private RadioGroup radioGroup1;
        private SimpleButton simpleButton1;
        private SimpleButton simpleButton2;
        private TextEdit txtLength;
        private TextEdit txtOffset;
    }
}