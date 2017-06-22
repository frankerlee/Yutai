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
    partial class OverviewWindowsAllLayerProperty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OverviewWindowsAllLayerProperty));
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.label2 = new Label();
            this.label3 = new Label();
            this.colorEdit1 = new ColorEdit();
            this.btnFillSymbol = new NewSymbolButton();
            this.checkEdit1 = new CheckEdit();
            this.colorEdit1.Properties.BeginInit();
            this.checkEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(166, 112);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(230, 112);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(56, 24);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "范围符号";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(164, 45);
            this.label3.Name = "label3";
            this.label3.Size = new Size(41, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "背景色";
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(212, 37);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(48, 21);
            this.colorEdit1.TabIndex = 6;
            this.btnFillSymbol.Location = new Point(84, 29);
            this.btnFillSymbol.Name = "btnFillSymbol";
            this.btnFillSymbol.Size = new Size(64, 32);
            this.btnFillSymbol.Style = null;
            this.btnFillSymbol.TabIndex = 7;
            this.btnFillSymbol.Click += new EventHandler(this.btnFillSymbol_Click);
            this.checkEdit1.Location = new Point(16, 70);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "随主视图缩放";
            this.checkEdit1.Size = new Size(167, 19);
            this.checkEdit1.TabIndex = 8;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(292, 155);
            base.Controls.Add(this.checkEdit1);
            base.Controls.Add(this.btnFillSymbol);
            base.Controls.Add(this.colorEdit1);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "OverviewWindowsAllLayerProperty";
            this.Text = "鹰眼属性";
            base.Load += new EventHandler(this.OverviewWindowsProperty_Load);
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
        private Label label2;
        private Label label3;
    }
}