using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class LabelLineElementProperty
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
            this.symbolItem1 = new SymbolItem();
            this.txtWidth = new SpinEdit();
            this.label2 = new Label();
            this.label1 = new Label();
            this.colorEdit1 = new ColorEdit();
            this.txtWidth.Properties.BeginInit();
            this.colorEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.symbolItem1.BackColor = SystemColors.ControlLight;
            this.symbolItem1.Location = new Point(165, 21);
            this.symbolItem1.Name = "symbolItem1";
            this.symbolItem1.Size = new Size(176, 88);
            this.symbolItem1.Symbol = null;
            this.symbolItem1.TabIndex = 9;
            int[] bits = new int[4];
            this.txtWidth.EditValue = new decimal(bits);
            this.txtWidth.Location = new Point(61, 53);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtWidth.Size = new Size(80, 21);
            this.txtWidth.TabIndex = 8;
            this.txtWidth.EditValueChanged += new EventHandler(this.txtWidth_EditValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(13, 61);
            this.label2.Name = "label2";
            this.label2.Size = new Size(35, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "宽度:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "颜色:";
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(61, 13);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(48, 21);
            this.colorEdit1.TabIndex = 5;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.symbolItem1);
            base.Controls.Add(this.txtWidth);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.colorEdit1);
            base.Name = "LabelLineElementProperty";
            base.Size = new Size(358, 182);
            base.Load += new EventHandler(this.LabelLineElementProperty_Load);
            this.txtWidth.Properties.EndInit();
            this.colorEdit1.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private ColorEdit colorEdit1;
        private Label label1;
        private Label label2;
        private SymbolItem symbolItem1;
        private SpinEdit txtWidth;
    }
}