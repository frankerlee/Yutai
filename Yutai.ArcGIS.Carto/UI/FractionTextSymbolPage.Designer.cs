using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.ExtendClass;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class FractionTextSymbolPage
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.btnNumeratorTextSymbol = new NewSymbolButton();
            this.btnLineSymbol = new NewSymbolButton();
            this.label2 = new Label();
            this.btnDenominatorTextSymbol = new NewSymbolButton();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.txtNumeratorText = new TextBox();
            this.txtDenominatorText = new TextBox();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 103);
            this.label1.Name = "label1";
            this.label1.Size = new Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "分子符号样式";
            this.btnNumeratorTextSymbol.Location = new System.Drawing.Point(106, 85);
            this.btnNumeratorTextSymbol.Name = "btnNumeratorTextSymbol";
            this.btnNumeratorTextSymbol.Size = new Size(115, 49);
            this.btnNumeratorTextSymbol.Style = null;
            this.btnNumeratorTextSymbol.TabIndex = 1;
            this.btnNumeratorTextSymbol.Click += new EventHandler(this.btnNumeratorTextSymbol_Click);
            this.btnLineSymbol.Location = new System.Drawing.Point(106, 144);
            this.btnLineSymbol.Name = "btnLineSymbol";
            this.btnLineSymbol.Size = new Size(115, 34);
            this.btnLineSymbol.Style = null;
            this.btnLineSymbol.TabIndex = 3;
            this.btnLineSymbol.Click += new EventHandler(this.btnLineSymbol_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 155);
            this.label2.Name = "label2";
            this.label2.Size = new Size(89, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "分式线符号样式";
            this.btnDenominatorTextSymbol.Location = new System.Drawing.Point(106, 190);
            this.btnDenominatorTextSymbol.Name = "btnDenominatorTextSymbol";
            this.btnDenominatorTextSymbol.Size = new Size(115, 50);
            this.btnDenominatorTextSymbol.Style = null;
            this.btnDenominatorTextSymbol.TabIndex = 5;
            this.btnDenominatorTextSymbol.Click += new EventHandler(this.btnDenominatorTextSymbol_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 209);
            this.label3.Name = "label3";
            this.label3.Size = new Size(77, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "分母符号样式";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 22);
            this.label4.Name = "label4";
            this.label4.Size = new Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "分子文本";
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 47);
            this.label5.Name = "label5";
            this.label5.Size = new Size(53, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "分母文本";
            this.txtNumeratorText.Location = new System.Drawing.Point(95, 13);
            this.txtNumeratorText.Name = "txtNumeratorText";
            this.txtNumeratorText.Size = new Size(215, 21);
            this.txtNumeratorText.TabIndex = 8;
            this.txtNumeratorText.TextChanged += new EventHandler(this.txtNumeratorText_TextChanged);
            this.txtDenominatorText.Location = new System.Drawing.Point(95, 44);
            this.txtDenominatorText.Name = "txtDenominatorText";
            this.txtDenominatorText.Size = new Size(215, 21);
            this.txtDenominatorText.TabIndex = 9;
            this.txtDenominatorText.TextChanged += new EventHandler(this.txtDenominatorText_TextChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.txtDenominatorText);
            base.Controls.Add(this.txtNumeratorText);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.btnDenominatorTextSymbol);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.btnLineSymbol);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnNumeratorTextSymbol);
            base.Controls.Add(this.label1);
            base.Name = "FractionTextSymbolPage";
            base.Size = new Size(362, 276);
            base.Load += new EventHandler(this.FractionTextSymbolPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private NewSymbolButton btnDenominatorTextSymbol;
        private NewSymbolButton btnLineSymbol;
        private NewSymbolButton btnNumeratorTextSymbol;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtDenominatorText;
        private TextBox txtNumeratorText;
    }
}