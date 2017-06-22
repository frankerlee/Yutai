using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.Historical;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;


namespace Yutai.ArcGIS.Controls.Controls.GPSLib
{
    partial class DestinationPropertyPage
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
            this.lbl = new Label();
            this.btnSymbol = new NewSymbolButton();
            this.label1 = new Label();
            this.txtLabel = new TextBox();
            this.label2 = new Label();
            this.btnLabelSymbol = new NewSymbolButton();
            this.btnBearingSymbol = new NewSymbolButton();
            this.label3 = new Label();
            this.chkShowBearingSymbol = new CheckBox();
            base.SuspendLayout();
            this.lbl.AutoSize = true;
            this.lbl.Location = new Point(20, 14);
            this.lbl.Name = "lbl";
            this.lbl.Size = new Size(29, 12);
            this.lbl.TabIndex = 0;
            this.lbl.Text = "符号";
            this.btnSymbol.Location = new Point(93, 14);
            this.btnSymbol.Name = "btnSymbol";
            this.btnSymbol.Size = new Size(75, 37);
            this.btnSymbol.Style = null;
            this.btnSymbol.TabIndex = 1;
            this.btnSymbol.Click += new EventHandler(this.btnSymbol_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(20, 68);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "标记";
            this.txtLabel.Location = new Point(93, 68);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new Size(134, 21);
            this.txtLabel.TabIndex = 3;
            this.txtLabel.TextChanged += new EventHandler(this.txtLabel_TextChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(20, 117);
            this.label2.Name = "label2";
            this.label2.Size = new Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "标记符号";
            this.btnLabelSymbol.Location = new Point(93, 105);
            this.btnLabelSymbol.Name = "btnLabelSymbol";
            this.btnLabelSymbol.Size = new Size(120, 37);
            this.btnLabelSymbol.Style = null;
            this.btnLabelSymbol.TabIndex = 5;
            this.btnLabelSymbol.Click += new EventHandler(this.btnLabelSymbol_Click);
            this.btnBearingSymbol.Location = new Point(93, 202);
            this.btnBearingSymbol.Name = "btnBearingSymbol";
            this.btnBearingSymbol.Size = new Size(75, 37);
            this.btnBearingSymbol.Style = null;
            this.btnBearingSymbol.TabIndex = 7;
            this.btnBearingSymbol.Click += new EventHandler(this.btnBearingSymbol_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(20, 214);
            this.label3.Name = "label3";
            this.label3.Size = new Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "方向符号";
            this.chkShowBearingSymbol.AutoSize = true;
            this.chkShowBearingSymbol.Location = new Point(24, 159);
            this.chkShowBearingSymbol.Name = "chkShowBearingSymbol";
            this.chkShowBearingSymbol.Size = new Size(144, 16);
            this.chkShowBearingSymbol.TabIndex = 8;
            this.chkShowBearingSymbol.Text = "显示到目标的方向符号";
            this.chkShowBearingSymbol.UseVisualStyleBackColor = true;
            this.chkShowBearingSymbol.CheckedChanged += new EventHandler(this.chkShowBearingSymbol_CheckedChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.chkShowBearingSymbol);
            base.Controls.Add(this.btnBearingSymbol);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.btnLabelSymbol);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txtLabel);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.btnSymbol);
            base.Controls.Add(this.lbl);
            base.Name = "DestinationPropertyPage";
            base.Size = new Size(274, 263);
            base.Load += new EventHandler(this.DestinationPropertyPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private IContainer components = null;
        private NewSymbolButton btnBearingSymbol;
        private NewSymbolButton btnLabelSymbol;
        private NewSymbolButton btnSymbol;
        private CheckBox chkShowBearingSymbol;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label lbl;
        private TextBox txtLabel;
    }
}