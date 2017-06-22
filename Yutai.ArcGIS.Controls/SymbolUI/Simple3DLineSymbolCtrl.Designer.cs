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
    partial class Simple3DLineSymbolCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Simple3DLineSymbolCtrl));
            this.cboStyle = new ComboBoxEdit();
            this.numericUpDownWidth = new SpinEdit();
            this.colorEdit1 = new ColorEdit();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.axSceneControl1 = new AxSceneControl();
            this.cboStyle.Properties.BeginInit();
            this.numericUpDownWidth.Properties.BeginInit();
            this.colorEdit1.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.axSceneControl1.BeginInit();
            base.SuspendLayout();
            this.cboStyle.EditValue = "";
            this.cboStyle.Location = new System.Drawing.Point(48, 40);
            this.cboStyle.Name = "cboStyle";
            this.cboStyle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboStyle.Properties.Items.AddRange(new object[] { "管状", "条带状", "墙状" });
            this.cboStyle.Size = new Size(112, 21);
            this.cboStyle.TabIndex = 88;
            this.cboStyle.SelectedIndexChanged += new EventHandler(this.cboStyle_SelectedIndexChanged);
            int[] bits = new int[4];
            this.numericUpDownWidth.EditValue = new decimal(bits);
            this.numericUpDownWidth.Location = new System.Drawing.Point(48, 72);
            this.numericUpDownWidth.Name = "numericUpDownWidth";
            this.numericUpDownWidth.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownWidth.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownWidth.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.numericUpDownWidth.Properties.MaxValue = new decimal(bits);
            this.numericUpDownWidth.Size = new Size(112, 21);
            this.numericUpDownWidth.TabIndex = 87;
            this.numericUpDownWidth.EditValueChanged += new EventHandler(this.numericUpDownWidth_EditValueChanged);
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new System.Drawing.Point(48, 8);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(48, 21);
            this.colorEdit1.TabIndex = 86;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 72);
            this.label3.Name = "label3";
            this.label3.Size = new Size(29, 12);
            this.label3.TabIndex = 85;
            this.label3.Text = "宽度";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 12);
            this.label2.TabIndex = 84;
            this.label2.Text = "样式";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 83;
            this.label1.Text = "颜色";
            this.groupBox2.Controls.Add(this.axSceneControl1);
            this.groupBox2.Location = new System.Drawing.Point(200, 40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(176, 160);
            this.groupBox2.TabIndex = 89;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "3D预览";
            this.axSceneControl1.Location = new System.Drawing.Point(6, 20);
            this.axSceneControl1.Name = "axSceneControl1";
            this.axSceneControl1.OcxState = (AxHost.State) resources.GetObject("axSceneControl1.OcxState");
            this.axSceneControl1.Size = new Size(154, 130);
            this.axSceneControl1.TabIndex = 0;
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.cboStyle);
            base.Controls.Add(this.numericUpDownWidth);
            base.Controls.Add(this.colorEdit1);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "Simple3DLineSymbolCtrl";
            base.Size = new Size(400, 232);
            base.Load += new EventHandler(this.Simple3DLineSymbolCtrl_Load);
            this.cboStyle.Properties.EndInit();
            this.numericUpDownWidth.Properties.EndInit();
            this.colorEdit1.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.axSceneControl1.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Container components = null;
        private AxSceneControl axSceneControl1;
        private ComboBoxEdit cboStyle;
        private ColorEdit colorEdit1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private SpinEdit numericUpDownWidth;
    }
}