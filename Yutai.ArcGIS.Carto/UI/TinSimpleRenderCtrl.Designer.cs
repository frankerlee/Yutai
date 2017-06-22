using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class TinSimpleRenderCtrl
    {
        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

       
 private void InitializeComponent()
        {
            this.groupBox2 = new GroupBox();
            this.label3 = new Label();
            this.txtDescription = new MemoEdit();
            this.label2 = new Label();
            this.txtLabel = new TextEdit();
            this.groupBox1 = new GroupBox();
            this.btnStyle = new StyleButton();
            this.groupBox2.SuspendLayout();
            this.txtDescription.Properties.BeginInit();
            this.txtLabel.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtDescription);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtLabel);
            this.groupBox2.Location = new Point(13, 112);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(360, 152);
            this.groupBox2.TabIndex = 55;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "图例";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 64);
            this.label3.Name = "label3";
            this.label3.Size = new Size(29, 12);
            this.label3.TabIndex = 55;
            this.label3.Text = "说明";
            this.txtDescription.EditValue = "";
            this.txtDescription.Location = new Point(48, 56);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Size(272, 88);
            this.txtDescription.TabIndex = 54;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 24);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 12);
            this.label2.TabIndex = 53;
            this.label2.Text = "标注";
            this.txtLabel.EditValue = "";
            this.txtLabel.Location = new Point(48, 24);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new Size(272, 21);
            this.txtLabel.TabIndex = 52;
            this.groupBox1.Controls.Add(this.btnStyle);
            this.groupBox1.Location = new Point(13, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(360, 96);
            this.groupBox1.TabIndex = 54;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "符号";
            this.btnStyle.Location = new Point(64, 16);
            this.btnStyle.Name = "btnStyle";
            this.btnStyle.Size = new Size(136, 64);
            this.btnStyle.Style = null;
            this.btnStyle.TabIndex = 42;
            this.btnStyle.Click += new EventHandler(this.btnStyle_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "TinSimpleRenderCtrl";
            base.Size = new Size(437, 269);
            base.Load += new EventHandler(this.TinSimpleRenderCtrl_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.txtDescription.Properties.EndInit();
            this.txtLabel.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private StyleButton btnStyle;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ITinSingleSymbolRenderer itinSingleSymbolRenderer_0;
        private Label label2;
        private Label label3;
        private MemoEdit txtDescription;
        private TextEdit txtLabel;
    }
}