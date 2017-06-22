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
    partial class frmTextSymbolEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTextSymbolEdit));
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.groupBox2 = new GroupBox();
            this.cboScale = new ComboBoxEdit();
            this.btnOnt2One = new SimpleButton();
            this.btnFixZoomOut = new SimpleButton();
            this.btnFixZoomIn = new SimpleButton();
            this.symbolItem1 = new SymbolItem();
            this.groupBox1 = new GroupBox();
            this.tabControl1 = new TabControl();
            this.cboUnit = new ComboBoxEdit();
            this.cboTextType = new ComboBoxEdit();
            this.label2 = new Label();
            this.label1 = new Label();
            this.groupBox2.SuspendLayout();
            this.cboScale.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.cboUnit.Properties.BeginInit();
            this.cboTextType.Properties.BeginInit();
            base.SuspendLayout();
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(564, 361);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(56, 24);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "取消";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(492, 361);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.groupBox2.Controls.Add(this.cboScale);
            this.groupBox2.Controls.Add(this.btnOnt2One);
            this.groupBox2.Controls.Add(this.btnFixZoomOut);
            this.groupBox2.Controls.Add(this.btnFixZoomIn);
            this.groupBox2.Controls.Add(this.symbolItem1);
            this.groupBox2.Location = new Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(248, 344);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "预览";
            this.cboScale.EditValue = "";
            this.cboScale.Location = new Point(120, 296);
            this.cboScale.Name = "cboScale";
            this.cboScale.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboScale.Properties.Items.AddRange(new object[] { "400%", "200%", "100%", "75%", "50%" });
            this.cboScale.Size = new Size(72, 21);
            this.cboScale.TabIndex = 24;
            this.cboScale.SelectedIndexChanged += new EventHandler(this.cboScale_SelectedIndexChanged);
            this.btnOnt2One.Image = (Image) resources.GetObject("btnOnt2One.Image");
            this.btnOnt2One.Location = new Point(88, 296);
            this.btnOnt2One.Name = "btnOnt2One";
            this.btnOnt2One.Size = new Size(22, 22);
            this.btnOnt2One.TabIndex = 23;
            this.btnOnt2One.Click += new EventHandler(this.btnOnt2One_Click);
            this.btnFixZoomOut.Image = (Image) resources.GetObject("btnFixZoomOut.Image");
            this.btnFixZoomOut.Location = new Point(64, 296);
            this.btnFixZoomOut.Name = "btnFixZoomOut";
            this.btnFixZoomOut.Size = new Size(22, 22);
            this.btnFixZoomOut.TabIndex = 22;
            this.btnFixZoomOut.Click += new EventHandler(this.btnFixZoomOut_Click);
            this.btnFixZoomIn.Image = (Image) resources.GetObject("btnFixZoomIn.Image");
            this.btnFixZoomIn.Location = new Point(32, 296);
            this.btnFixZoomIn.Name = "btnFixZoomIn";
            this.btnFixZoomIn.Size = new Size(22, 22);
            this.btnFixZoomIn.TabIndex = 21;
            this.btnFixZoomIn.Click += new EventHandler(this.btnFixZoomIn_Click);
            this.symbolItem1.BackColor = SystemColors.ControlLight;
            this.symbolItem1.Location = new Point(16, 24);
            this.symbolItem1.Name = "symbolItem1";
            this.symbolItem1.Size = new Size(208, 256);
            this.symbolItem1.Symbol = null;
            this.symbolItem1.TabIndex = 1;
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Controls.Add(this.cboUnit);
            this.groupBox1.Controls.Add(this.cboTextType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(276, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(408, 344);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "属性";
            this.tabControl1.Location = new Point(16, 48);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(384, 288);
            this.tabControl1.TabIndex = 11;
            this.cboUnit.EditValue = "点";
            this.cboUnit.Location = new Point(240, 19);
            this.cboUnit.Name = "cboUnit";
            this.cboUnit.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboUnit.Properties.Items.AddRange(new object[] { "点", "英寸", "厘米", "毫米" });
            this.cboUnit.Size = new Size(144, 21);
            this.cboUnit.TabIndex = 10;
            this.cboUnit.SelectedIndexChanged += new EventHandler(this.cboUnit_SelectedIndexChanged);
            this.cboTextType.EditValue = "文本符号";
            this.cboTextType.Location = new Point(56, 18);
            this.cboTextType.Name = "cboTextType";
            this.cboTextType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboTextType.Properties.Items.AddRange(new object[] { "文本符号", "分式注记" });
            this.cboTextType.Size = new Size(128, 21);
            this.cboTextType.TabIndex = 9;
            this.cboTextType.SelectedIndexChanged += new EventHandler(this.cboTextType_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(200, 24);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "单位";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "类型";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(683, 388);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "frmTextSymbolEdit";
            this.Text = "文本符号编辑器";
            base.Load += new EventHandler(this.frmTextSymbolEdit_Load);
            this.groupBox2.ResumeLayout(false);
            this.cboScale.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.cboUnit.Properties.EndInit();
            this.cboTextType.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
        private SimpleButton btnCancel;
        private SimpleButton btnFixZoomIn;
        private SimpleButton btnFixZoomOut;
        private SimpleButton btnOK;
        private SimpleButton btnOnt2One;
        private ComboBoxEdit cboScale;
        private ComboBoxEdit cboTextType;
        private ComboBoxEdit cboUnit;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private ISymbol m_pOldSymbol;
        private SymbolItem symbolItem1;
        private TabControl tabControl1;
    }
}