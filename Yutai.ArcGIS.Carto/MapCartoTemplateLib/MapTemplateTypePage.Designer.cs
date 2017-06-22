using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    partial class MapTemplateTypePage
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
 private void InitializeComponent()
        {
            this.rdoStandard = new RadioButton();
            this.radioButton2 = new RadioButton();
            this.label1 = new Label();
            this.label2 = new Label();
            base.SuspendLayout();
            this.rdoStandard.AutoSize = true;
            this.rdoStandard.Checked = true;
            this.rdoStandard.Location = new Point(19, 23);
            this.rdoStandard.Name = "rdoStandard";
            this.rdoStandard.Size = new Size(71, 16);
            this.rdoStandard.TabIndex = 0;
            this.rdoStandard.TabStop = true;
            this.rdoStandard.Text = "标准分幅";
            this.rdoStandard.UseVisualStyleBackColor = true;
            this.rdoStandard.CheckedChanged += new EventHandler(this.rdoStandard_CheckedChanged);
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new Point(19, 92);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new Size(71, 16);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "任意分幅";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new EventHandler(this.radioButton2_CheckedChanged);
            this.label1.Location = new Point(19, 46);
            this.label1.Name = "label1";
            this.label1.Size = new Size(222, 43);
            this.label1.TabIndex = 2;
            this.label1.Text = "按照地形图分幅标准要求创建，可以是梯形分幅，也可以是矩形分幅";
            this.label2.Location = new Point(19, 123);
            this.label2.Name = "label2";
            this.label2.Size = new Size(222, 49);
            this.label2.TabIndex = 3;
            this.label2.Text = "用于按照指定数据范围套合图框，其图幅是矩形。";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.radioButton2);
            base.Controls.Add(this.rdoStandard);
            base.Name = "MapTemplateTypePage";
            base.Size = new Size(271, 232);
            base.Load += new EventHandler(this.MapTemplateTypePage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Label label1;
        private Label label2;
        private MapCartoTemplateLib.MapTemplate mapTemplate_0;
        private RadioButton radioButton2;
        private RadioButton rdoStandard;
    }
}