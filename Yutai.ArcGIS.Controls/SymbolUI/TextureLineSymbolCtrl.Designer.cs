using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
   
    partial class TextureLineSymbolCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextureLineSymbolCtrl));
            this.groupBox2 = new GroupBox();
            this.axSceneControl1 = new AxSceneControl();
            this.btnSelectPicture = new SimpleButton();
            this.numericUpDownWidth = new SpinEdit();
            this.colorEditTransColor = new ColorEdit();
            this.colorEditForeColor = new ColorEdit();
            this.label1 = new Label();
            this.lblPathName = new Label();
            this.label6 = new Label();
            this.label4 = new Label();
            this.checkEdit1 = new CheckEdit();
            this.groupBox2.SuspendLayout();
            this.axSceneControl1.BeginInit();
            this.numericUpDownWidth.Properties.BeginInit();
            this.colorEditTransColor.Properties.BeginInit();
            this.colorEditForeColor.Properties.BeginInit();
            this.checkEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox2.Controls.Add(this.axSceneControl1);
            this.groupBox2.Location = new System.Drawing.Point(232, 64);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(176, 160);
            this.groupBox2.TabIndex = 90;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "3D预览";
            this.axSceneControl1.Location = new System.Drawing.Point(6, 20);
            this.axSceneControl1.Name = "axSceneControl1";
            this.axSceneControl1.OcxState = (AxHost.State) resources.GetObject("axSceneControl1.OcxState");
            this.axSceneControl1.Size = new Size(164, 130);
            this.axSceneControl1.TabIndex = 0;
            this.btnSelectPicture.Location = new System.Drawing.Point(8, 16);
            this.btnSelectPicture.Name = "btnSelectPicture";
            this.btnSelectPicture.Size = new Size(64, 24);
            this.btnSelectPicture.TabIndex = 96;
            this.btnSelectPicture.Text = "纹理...";
            this.btnSelectPicture.Click += new EventHandler(this.btnSelectPicture_Click);
            int[] bits = new int[4];
            this.numericUpDownWidth.EditValue = new decimal(bits);
            this.numericUpDownWidth.Location = new System.Drawing.Point(64, 136);
            this.numericUpDownWidth.Name = "numericUpDownWidth";
            this.numericUpDownWidth.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownWidth.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownWidth.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.numericUpDownWidth.Properties.MaxValue = new decimal(bits);
            this.numericUpDownWidth.Size = new Size(64, 21);
            this.numericUpDownWidth.TabIndex = 95;
            this.numericUpDownWidth.EditValueChanged += new EventHandler(this.numericUpDownWidth_EditValueChanged);
            this.colorEditTransColor.EditValue = Color.Empty;
            this.colorEditTransColor.Location = new System.Drawing.Point(64, 104);
            this.colorEditTransColor.Name = "colorEditTransColor";
            this.colorEditTransColor.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEditTransColor.Size = new Size(48, 21);
            this.colorEditTransColor.TabIndex = 94;
            this.colorEditTransColor.EditValueChanged += new EventHandler(this.colorEditTransColor_EditValueChanged);
            this.colorEditForeColor.EditValue = Color.Empty;
            this.colorEditForeColor.Location = new System.Drawing.Point(64, 64);
            this.colorEditForeColor.Name = "colorEditForeColor";
            this.colorEditForeColor.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEditForeColor.Size = new Size(48, 21);
            this.colorEditForeColor.TabIndex = 93;
            this.colorEditForeColor.EditValueChanged += new EventHandler(this.colorEditForeColor_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 144);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 92;
            this.label1.Text = "宽度";
            this.lblPathName.Location = new System.Drawing.Point(88, 16);
            this.lblPathName.Name = "lblPathName";
            this.lblPathName.Size = new Size(280, 24);
            this.lblPathName.TabIndex = 91;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 104);
            this.label6.Name = "label6";
            this.label6.Size = new Size(41, 12);
            this.label6.TabIndex = 98;
            this.label6.Text = "透明色";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 64);
            this.label4.Name = "label4";
            this.label4.Size = new Size(29, 12);
            this.label4.TabIndex = 97;
            this.label4.Text = "颜色";
            this.checkEdit1.Location = new System.Drawing.Point(144, 144);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "垂直方向";
            this.checkEdit1.Size = new Size(72, 19);
            this.checkEdit1.TabIndex = 99;
            this.checkEdit1.CheckedChanged += new EventHandler(this.checkEdit1_CheckedChanged);
            base.Controls.Add(this.checkEdit1);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.btnSelectPicture);
            base.Controls.Add(this.numericUpDownWidth);
            base.Controls.Add(this.colorEditTransColor);
            base.Controls.Add(this.colorEditForeColor);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.lblPathName);
            base.Controls.Add(this.groupBox2);
            base.Name = "TextureLineSymbolCtrl";
            base.Size = new Size(432, 280);
            base.Load += new EventHandler(this.TextureLineSymbolCtrl_Load);
            this.groupBox2.ResumeLayout(false);
            this.axSceneControl1.EndInit();
            this.numericUpDownWidth.Properties.EndInit();
            this.colorEditTransColor.Properties.EndInit();
            this.colorEditForeColor.Properties.EndInit();
            this.checkEdit1.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Container components = null;
        private AxSceneControl axSceneControl1;
        private SimpleButton btnSelectPicture;
        private CheckEdit checkEdit1;
        private ColorEdit colorEditForeColor;
        private ColorEdit colorEditTransColor;
        private GroupBox groupBox2;
        private Label label1;
        private Label label4;
        private Label label6;
        private Label lblPathName;
        private SpinEdit numericUpDownWidth;
    }
}