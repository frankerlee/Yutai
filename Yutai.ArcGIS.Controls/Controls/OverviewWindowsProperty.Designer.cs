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
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Controls.Controls
{
    partial class OverviewWindowsProperty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OverviewWindowsProperty));
            this.label1 = new Label();
            this.comboBoxEdit1 = new ComboBoxEdit();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.label2 = new Label();
            this.label3 = new Label();
            this.colorEdit1 = new ColorEdit();
            this.btnFillSymbol = new NewSymbolButton();
            this.checkEdit1 = new CheckEdit();
            this.comboBoxEdit1.Properties.BeginInit();
            this.colorEdit1.Properties.BeginInit();
            this.checkEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "参考图层";
            this.comboBoxEdit1.EditValue = "";
            this.comboBoxEdit1.Location = new Point(8, 32);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.comboBoxEdit1.Size = new Size(240, 21);
            this.comboBoxEdit1.TabIndex = 1;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(162, 155);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(226, 155);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(56, 24);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 80);
            this.label2.Name = "label2";
            this.label2.Size = new Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "范围符号";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(160, 88);
            this.label3.Name = "label3";
            this.label3.Size = new Size(41, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "背景色";
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(208, 80);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(48, 21);
            this.colorEdit1.TabIndex = 6;
            this.btnFillSymbol.Location = new Point(80, 72);
            this.btnFillSymbol.Name = "btnFillSymbol";
            this.btnFillSymbol.Size = new Size(64, 32);
            this.btnFillSymbol.Style = null;
            this.btnFillSymbol.TabIndex = 7;
            this.btnFillSymbol.Click += new EventHandler(this.btnFillSymbol_Click);
            this.checkEdit1.Location = new Point(12, 113);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "随主视图缩放";
            this.checkEdit1.Size = new Size(167, 19);
            this.checkEdit1.TabIndex = 8;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(292, 191);
            base.Controls.Add(this.checkEdit1);
            base.Controls.Add(this.btnFillSymbol);
            base.Controls.Add(this.colorEdit1);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.comboBoxEdit1);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "OverviewWindowsProperty";
            this.Text = "鹰眼属性";
            base.Load += new EventHandler(this.OverviewWindowsProperty_Load);
            this.comboBoxEdit1.Properties.EndInit();
            this.colorEdit1.Properties.EndInit();
            this.checkEdit1.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Container components = null;
        private SimpleButton btnCancel;
        private NewSymbolButton btnFillSymbol;
        private SimpleButton btnOK;
        private CheckEdit checkEdit1;
        private ColorEdit colorEdit1;
        private ComboBoxEdit comboBoxEdit1;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}