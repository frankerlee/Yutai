using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Common.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class MarkFillControl
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
            this.colorEdit1 = new ColorEdit();
            this.label1 = new Label();
            this.btnFillMarker = new NewSymbolButton();
            this.btnOutline = new NewSymbolButton();
            this.radioGroupFillStyle = new RadioGroup();
            this.label5 = new Label();
            this.label4 = new Label();
            this.colorEdit1.Properties.BeginInit();
            this.radioGroupFillStyle.Properties.BeginInit();
            base.SuspendLayout();
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(64, 24);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(48, 23);
            this.colorEdit1.TabIndex = 6;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(24, 27);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "颜色";
            this.btnFillMarker.Location = new Point(208, 16);
            this.btnFillMarker.Name = "btnFillMarker";
            this.btnFillMarker.Size = new Size(96, 40);
            this.btnFillMarker.Style = null;
            this.btnFillMarker.TabIndex = 11;
            this.btnFillMarker.Click += new EventHandler(this.btnFillMarker_Click);
            this.btnOutline.Location = new Point(208, 72);
            this.btnOutline.Name = "btnOutline";
            this.btnOutline.Size = new Size(96, 40);
            this.btnOutline.Style = null;
            this.btnOutline.TabIndex = 12;
            this.btnOutline.Click += new EventHandler(this.btnOutline_Click);
            this.radioGroupFillStyle.Location = new Point(24, 64);
            this.radioGroupFillStyle.Name = "radioGroupFillStyle";
            this.radioGroupFillStyle.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroupFillStyle.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroupFillStyle.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroupFillStyle.Properties.Columns = 2;
            this.radioGroupFillStyle.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "格网"), new RadioGroupItem(null, "随机") });
            this.radioGroupFillStyle.Size = new Size(112, 24);
            this.radioGroupFillStyle.TabIndex = 13;
            this.radioGroupFillStyle.SelectedIndexChanged += new EventHandler(this.radioGroupFillStyle_SelectedIndexChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(136, 88);
            this.label5.Name = "label5";
            this.label5.Size = new Size(72, 17);
            this.label5.TabIndex = 80;
            this.label5.Text = "轮廓线符号:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(136, 28);
            this.label4.Name = "label4";
            this.label4.Size = new Size(72, 17);
            this.label4.TabIndex = 79;
            this.label4.Text = "填充点符号:";
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.radioGroupFillStyle);
            base.Controls.Add(this.btnOutline);
            base.Controls.Add(this.btnFillMarker);
            base.Controls.Add(this.colorEdit1);
            base.Controls.Add(this.label1);
            base.Name = "MarkFillControl";
            base.Size = new Size(360, 224);
            base.Load += new EventHandler(this.MarkFillControl_Load);
            this.colorEdit1.Properties.EndInit();
            this.radioGroupFillStyle.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private NewSymbolButton btnFillMarker;
        private NewSymbolButton btnOutline;
        private ColorEdit colorEdit1;
        private Label label1;
        private Label label4;
        private Label label5;
        private RadioGroup radioGroupFillStyle;
    }
}