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

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class NorthArrowPropertyPage
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
            this.btnNorthArrorSelector = new SimpleButton();
            this.symbolItem1 = new SymbolItem();
            this.groupBox2 = new GroupBox();
            this.colorEdit1 = new ColorEdit();
            this.label4 = new Label();
            this.txtAngle = new TextEdit();
            this.label3 = new Label();
            this.txtCalibrationAngle = new SpinEdit();
            this.label2 = new Label();
            this.txtSize = new SpinEdit();
            this.label1 = new Label();
            this.groupBox3 = new GroupBox();
            this.btnNorthMarkerSymbolSelector = new SimpleButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.colorEdit1.Properties.BeginInit();
            this.txtAngle.Properties.BeginInit();
            this.txtCalibrationAngle.Properties.BeginInit();
            this.txtSize.Properties.BeginInit();
            this.groupBox3.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnNorthArrorSelector);
            this.groupBox1.Controls.Add(this.symbolItem1);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(120, 192);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "预览";
            this.btnNorthArrorSelector.Location = new Point(24, 160);
            this.btnNorthArrorSelector.Name = "btnNorthArrorSelector";
            this.btnNorthArrorSelector.Size = new Size(72, 24);
            this.btnNorthArrorSelector.TabIndex = 2;
            this.btnNorthArrorSelector.Text = "指北针";
            this.btnNorthArrorSelector.Click += new EventHandler(this.btnNorthArrorSelector_Click);
            this.symbolItem1.BackColor = SystemColors.ControlLight;
            this.symbolItem1.Location = new Point(16, 24);
            this.symbolItem1.Name = "symbolItem1";
            this.symbolItem1.Size = new Size(96, 128);
            this.symbolItem1.Symbol = null;
            this.symbolItem1.TabIndex = 1;
            this.groupBox2.Controls.Add(this.colorEdit1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtAngle);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtCalibrationAngle);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtSize);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new Point(136, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(168, 120);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "常规";
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(88, 40);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(48, 21);
            this.colorEdit1.TabIndex = 7;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(88, 16);
            this.label4.Name = "label4";
            this.label4.Size = new Size(35, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "颜色:";
            this.txtAngle.EditValue = "";
            this.txtAngle.Location = new Point(88, 88);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Size = new Size(48, 21);
            this.txtAngle.TabIndex = 5;
            this.txtAngle.EditValueChanged += new EventHandler(this.txtAngle_EditValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(88, 64);
            this.label3.Name = "label3";
            this.label3.Size = new Size(35, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "角度:";
            this.txtCalibrationAngle.EditValue = 0;
            this.txtCalibrationAngle.Location = new Point(8, 88);
            this.txtCalibrationAngle.Name = "txtCalibrationAngle";
            this.txtCalibrationAngle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtCalibrationAngle.Properties.UseCtrlIncrement = false;
            this.txtCalibrationAngle.Size = new Size(56, 21);
            this.txtCalibrationAngle.TabIndex = 3;
            this.txtCalibrationAngle.EditValueChanged += new EventHandler(this.txtCalibrationAngle_EditValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(16, 64);
            this.label2.Name = "label2";
            this.label2.Size = new Size(48, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "校准角:";
            this.txtSize.EditValue = 0;
            this.txtSize.Location = new Point(16, 40);
            this.txtSize.Name = "txtSize";
            this.txtSize.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtSize.Properties.UseCtrlIncrement = false;
            this.txtSize.Size = new Size(56, 21);
            this.txtSize.TabIndex = 1;
            this.txtSize.EditValueChanged += new EventHandler(this.txtSize_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "大小:";
            this.groupBox3.Controls.Add(this.btnNorthMarkerSymbolSelector);
            this.groupBox3.Location = new Point(136, 128);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(168, 72);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "点符号";
            this.btnNorthMarkerSymbolSelector.Location = new Point(48, 40);
            this.btnNorthMarkerSymbolSelector.Name = "btnNorthMarkerSymbolSelector";
            this.btnNorthMarkerSymbolSelector.Size = new Size(72, 24);
            this.btnNorthMarkerSymbolSelector.TabIndex = 0;
            this.btnNorthMarkerSymbolSelector.Text = "点符号";
            this.btnNorthMarkerSymbolSelector.Click += new EventHandler(this.btnNorthMarkerSymbolSelector_Click);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "NorthArrowPropertyPage";
            base.Size = new Size(320, 216);
            base.Load += new EventHandler(this.NorthArrowPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.colorEdit1.Properties.EndInit();
            this.txtAngle.Properties.EndInit();
            this.txtCalibrationAngle.Properties.EndInit();
            this.txtSize.Properties.EndInit();
            this.groupBox3.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private SimpleButton btnNorthArrorSelector;
        private SimpleButton btnNorthMarkerSymbolSelector;
        private ColorEdit colorEdit1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private IStyleGallery m_pSG;
        private SymbolItem symbolItem1;
        private TextEdit txtAngle;
        private SpinEdit txtCalibrationAngle;
        private SpinEdit txtSize;
    }
}