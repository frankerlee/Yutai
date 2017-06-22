using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    //[ToolboxItem(false)]
    partial class BackgroundSymbolPropertyPage
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
            this.label2 = new Label();
            this.txtWidth = new SpinEdit();
            this.symbolItem1 = new SymbolItem();
            this.btnChangeSymbol = new SimpleButton();
            this.label3 = new Label();
            this.colorEdit2 = new ColorEdit();
            this.colorEdit1.Properties.BeginInit();
            this.txtWidth.Properties.BeginInit();
            this.colorEdit2.Properties.BeginInit();
            base.SuspendLayout();
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(80, 16);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(48, 23);
            this.colorEdit1.TabIndex = 0;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 19);
            this.label1.Name = "label1";
            this.label1.Size = new Size(48, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "填充色:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 84);
            this.label2.Name = "label2";
            this.label2.Size = new Size(72, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "轮廓线宽度:";
            int[] bits = new int[4];
            this.txtWidth.EditValue = new decimal(bits);
            this.txtWidth.Location = new Point(80, 80);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtWidth.Properties.UseCtrlIncrement = false;
            this.txtWidth.Size = new Size(64, 23);
            this.txtWidth.TabIndex = 3;
            this.txtWidth.EditValueChanged += new EventHandler(this.txtWidth_EditValueChanged);
            this.symbolItem1.BackColor = SystemColors.ControlLight;
            this.symbolItem1.Location = new Point(160, 24);
            this.symbolItem1.Name = "symbolItem1";
            this.symbolItem1.Size = new Size(176, 88);
            this.symbolItem1.Symbol = null;
            this.symbolItem1.TabIndex = 4;
            this.btnChangeSymbol.Location = new Point(184, 128);
            this.btnChangeSymbol.Name = "btnChangeSymbol";
            this.btnChangeSymbol.Size = new Size(144, 32);
            this.btnChangeSymbol.TabIndex = 5;
            this.btnChangeSymbol.Text = "更改符号...";
            this.btnChangeSymbol.Click += new EventHandler(this.btnChangeSymbol_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 50);
            this.label3.Name = "label3";
            this.label3.Size = new Size(72, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "轮廓线颜色:";
            this.colorEdit2.EditValue = Color.Empty;
            this.colorEdit2.Location = new Point(80, 48);
            this.colorEdit2.Name = "colorEdit2";
            this.colorEdit2.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit2.Size = new Size(48, 23);
            this.colorEdit2.TabIndex = 6;
            this.colorEdit2.EditValueChanged += new EventHandler(this.colorEdit2_EditValueChanged);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.colorEdit2);
            base.Controls.Add(this.btnChangeSymbol);
            base.Controls.Add(this.symbolItem1);
            base.Controls.Add(this.txtWidth);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.colorEdit1);
            base.Name = "BackgroundSymbolPropertyPage";
            base.Size = new Size(360, 184);
            base.Load += new EventHandler(this.BorderSymbolPropertyPage_Load);
            this.colorEdit1.Properties.EndInit();
            this.txtWidth.Properties.EndInit();
            this.colorEdit2.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private SimpleButton btnChangeSymbol;
        private ColorEdit colorEdit1;
        private ColorEdit colorEdit2;
        private Label label1;
        private Label label2;
        private Label label3;
        private SymbolItem symbolItem1;
        private SpinEdit txtWidth;
        private IAppContext _context;
    }
}