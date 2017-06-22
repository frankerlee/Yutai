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
    partial class TextureFillSymbolCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextureFillSymbolCtrl));
            this.groupBox2 = new GroupBox();
            this.axSceneControl1 = new AxSceneControl();
            this.btnSelectPicture = new SimpleButton();
            this.colorEditTransColor = new ColorEdit();
            this.colorEditForeColor = new ColorEdit();
            this.lblPathName = new Label();
            this.label6 = new Label();
            this.label4 = new Label();
            this.txtAngle = new SpinEdit();
            this.label2 = new Label();
            this.txtSize = new SpinEdit();
            this.label3 = new Label();
            this.groupBox2.SuspendLayout();
            this.axSceneControl1.BeginInit();
            this.colorEditTransColor.Properties.BeginInit();
            this.colorEditForeColor.Properties.BeginInit();
            this.txtAngle.Properties.BeginInit();
            this.txtSize.Properties.BeginInit();
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
            this.axSceneControl1.Size = new Size(154, 130);
            this.axSceneControl1.TabIndex = 0;
            this.btnSelectPicture.Location = new System.Drawing.Point(8, 16);
            this.btnSelectPicture.Name = "btnSelectPicture";
            this.btnSelectPicture.Size = new Size(64, 24);
            this.btnSelectPicture.TabIndex = 96;
            this.btnSelectPicture.Text = "纹理...";
            this.btnSelectPicture.Click += new EventHandler(this.btnSelectPicture_Click);
            this.colorEditTransColor.EditValue = Color.Empty;
            this.colorEditTransColor.Location = new System.Drawing.Point(64, 86);
            this.colorEditTransColor.Name = "colorEditTransColor";
            this.colorEditTransColor.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEditTransColor.Size = new Size(48, 21);
            this.colorEditTransColor.TabIndex = 94;
            this.colorEditTransColor.EditValueChanged += new EventHandler(this.colorEditTransColor_EditValueChanged);
            this.colorEditForeColor.EditValue = Color.Empty;
            this.colorEditForeColor.Location = new System.Drawing.Point(64, 48);
            this.colorEditForeColor.Name = "colorEditForeColor";
            this.colorEditForeColor.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEditForeColor.Size = new Size(48, 21);
            this.colorEditForeColor.TabIndex = 93;
            this.colorEditForeColor.EditValueChanged += new EventHandler(this.colorEditForeColor_EditValueChanged);
            this.lblPathName.Location = new System.Drawing.Point(88, 16);
            this.lblPathName.Name = "lblPathName";
            this.lblPathName.Size = new Size(280, 24);
            this.lblPathName.TabIndex = 91;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 88);
            this.label6.Name = "label6";
            this.label6.Size = new Size(41, 12);
            this.label6.TabIndex = 98;
            this.label6.Text = "透明色";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 56);
            this.label4.Name = "label4";
            this.label4.Size = new Size(29, 12);
            this.label4.TabIndex = 97;
            this.label4.Text = "颜色";
            int[] bits = new int[4];
            this.txtAngle.EditValue = new decimal(bits);
            this.txtAngle.Location = new System.Drawing.Point(56, 152);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtAngle.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.txtAngle.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.txtAngle.Properties.MaxValue = new decimal(bits);
            bits = new int[4];
            bits[0] = 100;
            bits[3] = -2147483648;
            this.txtAngle.Properties.MinValue = new decimal(bits);
            this.txtAngle.Size = new Size(72, 21);
            this.txtAngle.TabIndex = 102;
            this.txtAngle.EditValueChanged += new EventHandler(this.txtAngle_EditValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 160);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 12);
            this.label2.TabIndex = 101;
            this.label2.Text = "角度";
            bits = new int[4];
            this.txtSize.EditValue = new decimal(bits);
            this.txtSize.Location = new System.Drawing.Point(56, 120);
            this.txtSize.Name = "txtSize";
            this.txtSize.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtSize.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.txtSize.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.txtSize.Properties.MaxValue = new decimal(bits);
            this.txtSize.Size = new Size(64, 21);
            this.txtSize.TabIndex = 104;
            this.txtSize.EditValueChanged += new EventHandler(this.txtSize_EditValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 128);
            this.label3.Name = "label3";
            this.label3.Size = new Size(29, 12);
            this.label3.TabIndex = 103;
            this.label3.Text = "大小";
            base.Controls.Add(this.txtSize);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.txtAngle);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.btnSelectPicture);
            base.Controls.Add(this.colorEditTransColor);
            base.Controls.Add(this.colorEditForeColor);
            base.Controls.Add(this.lblPathName);
            base.Controls.Add(this.groupBox2);
            base.Name = "TextureFillSymbolCtrl";
            base.Size = new Size(416, 248);
            base.Load += new EventHandler(this.TextureLineSymbolCtrl_Load);
            this.groupBox2.ResumeLayout(false);
            this.axSceneControl1.EndInit();
            this.colorEditTransColor.Properties.EndInit();
            this.colorEditForeColor.Properties.EndInit();
            this.txtAngle.Properties.EndInit();
            this.txtSize.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Container components = null;
        private AxSceneControl axSceneControl1;
        private SimpleButton btnSelectPicture;
        private ColorEdit colorEditForeColor;
        private ColorEdit colorEditTransColor;
        private GroupBox groupBox2;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label6;
        private Label lblPathName;
        private SpinEdit txtAngle;
        private SpinEdit txtSize;
    }
}