using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class TickSymbolPropertyPage
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
            this.btnStyle = new StyleButton();
            this.radioGroup1 = new RadioGroup();
            this.groupBox1.SuspendLayout();
            this.radioGroup1.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnStyle);
            this.groupBox1.Controls.Add(this.radioGroup1);
            this.groupBox1.Location = new Point(16, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(240, 136);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "显示属性";
            this.btnStyle.Location = new Point(24, 96);
            this.btnStyle.Name = "btnStyle";
            this.btnStyle.Size = new Size(80, 32);
            this.btnStyle.Style = null;
            this.btnStyle.TabIndex = 1;
            this.btnStyle.Click += new EventHandler(this.btnStyle_Click);
            this.radioGroup1.Location = new Point(16, 16);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "格网线"), new RadioGroupItem(null, "格网刻度"), new RadioGroupItem(null, "不显示线或刻度") });
            this.radioGroup1.Size = new Size(136, 72);
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            base.Controls.Add(this.groupBox1);
            base.Name = "TickSymbolPropertyPage";
            base.Size = new Size(280, 240);
            base.Load += new EventHandler(this.TickSymbolPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.radioGroup1.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private StyleButton btnStyle;
        private GroupBox groupBox1;
        private RadioGroup radioGroup1;
    }
}