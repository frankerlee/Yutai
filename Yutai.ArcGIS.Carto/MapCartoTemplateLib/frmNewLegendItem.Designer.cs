using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    partial class frmNewLegendItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewLegendItem));
            this.txtLegendItemName = new TextBox();
            this.labPoint = new Label();
            this.btnOK = new Button();
            this.button2 = new Button();
            this.groupBox1 = new GroupBox();
            this.btnStyle = new NewSymbolButton();
            this.rdoFillSymbol = new RadioButton();
            this.rdoLineSymbol = new RadioButton();
            this.rdoPointSymbol = new RadioButton();
            this.groupBox2 = new GroupBox();
            this.newSymbolButton1 = new NewSymbolButton();
            this.btnClear = new Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.txtLegendItemName.Location = new Point(81, 12);
            this.txtLegendItemName.Name = "txtLegendItemName";
            this.txtLegendItemName.Size = new Size(146, 21);
            this.txtLegendItemName.TabIndex = 38;
            this.txtLegendItemName.TextChanged += new EventHandler(this.txtLegendItemName_TextChanged);
            this.labPoint.AutoSize = true;
            this.labPoint.Location = new Point(4, 15);
            this.labPoint.Name = "labPoint";
            this.labPoint.Size = new Size(71, 12);
            this.labPoint.TabIndex = 39;
            this.labPoint.Text = "图例项描述:";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(46, 284);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(70, 23);
            this.btnOK.TabIndex = 70;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new Point(144, 284);
            this.button2.Name = "button2";
            this.button2.Size = new Size(70, 23);
            this.button2.TabIndex = 69;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.groupBox1.Controls.Add(this.btnStyle);
            this.groupBox1.Controls.Add(this.rdoFillSymbol);
            this.groupBox1.Controls.Add(this.rdoLineSymbol);
            this.groupBox1.Controls.Add(this.rdoPointSymbol);
            this.groupBox1.Location = new Point(6, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(221, 122);
            this.groupBox1.TabIndex = 72;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "符号";
            this.btnStyle.Location = new Point(42, 52);
            this.btnStyle.Name = "btnStyle";
            this.btnStyle.Size = new Size(106, 50);
            this.btnStyle.Style = null;
            this.btnStyle.TabIndex = 13;
            this.btnStyle.Click += new EventHandler(this.btnStyle_Click);
            this.rdoFillSymbol.AutoSize = true;
            this.rdoFillSymbol.Checked = true;
            this.rdoFillSymbol.Location = new Point(154, 20);
            this.rdoFillSymbol.Name = "rdoFillSymbol";
            this.rdoFillSymbol.Size = new Size(59, 16);
            this.rdoFillSymbol.TabIndex = 11;
            this.rdoFillSymbol.TabStop = true;
            this.rdoFillSymbol.Text = "面符号";
            this.rdoFillSymbol.UseVisualStyleBackColor = true;
            this.rdoFillSymbol.CheckedChanged += new EventHandler(this.rdoFillSymbol_CheckedChanged);
            this.rdoLineSymbol.AutoSize = true;
            this.rdoLineSymbol.Location = new Point(89, 20);
            this.rdoLineSymbol.Name = "rdoLineSymbol";
            this.rdoLineSymbol.Size = new Size(59, 16);
            this.rdoLineSymbol.TabIndex = 10;
            this.rdoLineSymbol.Text = "线符号";
            this.rdoLineSymbol.UseVisualStyleBackColor = true;
            this.rdoLineSymbol.CheckedChanged += new EventHandler(this.rdoLineSymbol_CheckedChanged);
            this.rdoPointSymbol.AutoSize = true;
            this.rdoPointSymbol.Location = new Point(16, 20);
            this.rdoPointSymbol.Name = "rdoPointSymbol";
            this.rdoPointSymbol.Size = new Size(59, 16);
            this.rdoPointSymbol.TabIndex = 9;
            this.rdoPointSymbol.Text = "点符号";
            this.rdoPointSymbol.UseVisualStyleBackColor = true;
            this.rdoPointSymbol.CheckedChanged += new EventHandler(this.rdoPointSymbol_CheckedChanged);
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Controls.Add(this.newSymbolButton1);
            this.groupBox2.Location = new Point(6, 182);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(221, 84);
            this.groupBox2.TabIndex = 73;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "背景符号";
            this.newSymbolButton1.Location = new Point(16, 20);
            this.newSymbolButton1.Name = "newSymbolButton1";
            this.newSymbolButton1.Size = new Size(106, 50);
            this.newSymbolButton1.Style = null;
            this.newSymbolButton1.TabIndex = 13;
            this.newSymbolButton1.Click += new EventHandler(this.newSymbolButton1_Click);
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClear.Location = new Point(157, 20);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(51, 23);
            this.btnClear.TabIndex = 71;
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(283, 338);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.txtLegendItemName);
            base.Controls.Add(this.labPoint);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
          
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmNewLegendItem";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "新增图例项";
            base.Load += new EventHandler(this.frmNewLegendItem_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Button btnClear;
        private Button btnOK;
        private NewSymbolButton btnStyle;
        private Button button2;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label labPoint;
        private NewSymbolButton newSymbolButton1;
        private RadioButton rdoFillSymbol;
        private RadioButton rdoLineSymbol;
        private RadioButton rdoPointSymbol;
        private TextBox txtLegendItemName;
    }
}