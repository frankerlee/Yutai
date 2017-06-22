using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class FeatureSelectionSetCtrl
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
        private void InitializeComponent()
        {
            this.radioGroup1 = new RadioGroup();
            this.label1 = new Label();
            this.btnSymbol = new StyleButton();
            this.colorEdit1 = new ColorEdit();
            this.radioGroup1.Properties.BeginInit();
            this.colorEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.radioGroup1.Location = new Point(8, 8);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = Color.Transparent;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "使用选择集中的符号"), new RadioGroupItem(null, "使用指定符号"), new RadioGroupItem(null, "使用指定颜色") });
            this.radioGroup1.Size = new Size(184, 176);
            this.radioGroup1.TabIndex = 1;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(79, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "显示选择要素";
            this.btnSymbol.Location = new Point(40, 104);
            this.btnSymbol.Name = "btnSymbol";
            this.btnSymbol.Size = new Size(96, 32);
            this.btnSymbol.Style = null;
            this.btnSymbol.TabIndex = 4;
            this.btnSymbol.Click += new EventHandler(this.btnSymbol_Click);
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(40, 168);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(56, 23);
            this.colorEdit1.TabIndex = 5;
            base.Controls.Add(this.colorEdit1);
            base.Controls.Add(this.btnSymbol);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.radioGroup1);
            base.Name = "FeatureSelectionSetCtrl";
            base.Size = new Size(352, 264);
            base.Load += new EventHandler(this.FeatureSelectionSetCtrl_Load);
            this.radioGroup1.Properties.EndInit();
            this.colorEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private StyleButton btnSymbol;
        private ColorEdit colorEdit1;
        private Label label1;
        private RadioGroup radioGroup1;
    }
}