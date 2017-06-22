using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    partial class ElementTypeSelectPage
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
            this.rdoConstantTextElement = new RadioButton();
            this.rdoSingleTextElement = new RadioButton();
            this.rdoMultiTextElement = new RadioButton();
            this.rdoJoinTableElement = new RadioButton();
            this.rdoScaleText = new RadioButton();
            this.rdoCustomLegend = new RadioButton();
            this.rdoLegend = new RadioButton();
            this.rdoPicture = new RadioButton();
            this.rdoOle = new RadioButton();
            this.rdoScaleBar = new RadioButton();
            this.groupBox1 = new GroupBox();
            this.rdoNorth = new RadioButton();
            this.rdoDataGraphicElement = new RadioButton();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.rdoConstantTextElement.AutoSize = true;
            this.rdoConstantTextElement.Checked = true;
            this.rdoConstantTextElement.Location = new Point(17, 20);
            this.rdoConstantTextElement.Name = "rdoConstantTextElement";
            this.rdoConstantTextElement.Size = new Size(71, 16);
            this.rdoConstantTextElement.TabIndex = 1;
            this.rdoConstantTextElement.TabStop = true;
            this.rdoConstantTextElement.Text = "固定文本";
            this.rdoConstantTextElement.UseVisualStyleBackColor = true;
            this.rdoSingleTextElement.AutoSize = true;
            this.rdoSingleTextElement.Location = new Point(15, 42);
            this.rdoSingleTextElement.Name = "rdoSingleTextElement";
            this.rdoSingleTextElement.Size = new Size(71, 16);
            this.rdoSingleTextElement.TabIndex = 2;
            this.rdoSingleTextElement.Text = "单行文本";
            this.rdoSingleTextElement.UseVisualStyleBackColor = true;
            this.rdoMultiTextElement.AutoSize = true;
            this.rdoMultiTextElement.Location = new Point(15, 64);
            this.rdoMultiTextElement.Name = "rdoMultiTextElement";
            this.rdoMultiTextElement.Size = new Size(71, 16);
            this.rdoMultiTextElement.TabIndex = 4;
            this.rdoMultiTextElement.Text = "多行文本";
            this.rdoMultiTextElement.UseVisualStyleBackColor = true;
            this.rdoJoinTableElement.AutoSize = true;
            this.rdoJoinTableElement.Location = new Point(17, 86);
            this.rdoJoinTableElement.Name = "rdoJoinTableElement";
            this.rdoJoinTableElement.Size = new Size(59, 16);
            this.rdoJoinTableElement.TabIndex = 5;
            this.rdoJoinTableElement.Text = "接图表";
            this.rdoJoinTableElement.UseVisualStyleBackColor = true;
            this.rdoScaleText.AutoSize = true;
            this.rdoScaleText.Location = new Point(17, 108);
            this.rdoScaleText.Name = "rdoScaleText";
            this.rdoScaleText.Size = new Size(83, 16);
            this.rdoScaleText.TabIndex = 6;
            this.rdoScaleText.Text = "比例尺文本";
            this.rdoScaleText.UseVisualStyleBackColor = true;
            this.rdoCustomLegend.AutoSize = true;
            this.rdoCustomLegend.Location = new Point(17, 130);
            this.rdoCustomLegend.Name = "rdoCustomLegend";
            this.rdoCustomLegend.Size = new Size(83, 16);
            this.rdoCustomLegend.TabIndex = 7;
            this.rdoCustomLegend.Text = "自定义图例";
            this.rdoCustomLegend.UseVisualStyleBackColor = true;
            this.rdoLegend.AutoSize = true;
            this.rdoLegend.Location = new Point(17, 152);
            this.rdoLegend.Name = "rdoLegend";
            this.rdoLegend.Size = new Size(47, 16);
            this.rdoLegend.TabIndex = 8;
            this.rdoLegend.Text = "图例";
            this.rdoLegend.UseVisualStyleBackColor = true;
            this.rdoPicture.AutoSize = true;
            this.rdoPicture.Location = new Point(17, 174);
            this.rdoPicture.Name = "rdoPicture";
            this.rdoPicture.Size = new Size(47, 16);
            this.rdoPicture.TabIndex = 9;
            this.rdoPicture.Text = "图片";
            this.rdoPicture.UseVisualStyleBackColor = true;
            this.rdoOle.AutoSize = true;
            this.rdoOle.Location = new Point(17, 196);
            this.rdoOle.Name = "rdoOle";
            this.rdoOle.Size = new Size(65, 16);
            this.rdoOle.TabIndex = 10;
            this.rdoOle.Text = "OLE对象";
            this.rdoOle.UseVisualStyleBackColor = true;
            this.rdoScaleBar.AutoSize = true;
            this.rdoScaleBar.Location = new Point(17, 218);
            this.rdoScaleBar.Name = "rdoScaleBar";
            this.rdoScaleBar.Size = new Size(59, 16);
            this.rdoScaleBar.TabIndex = 11;
            this.rdoScaleBar.Text = "比例尺";
            this.rdoScaleBar.UseVisualStyleBackColor = true;
            this.groupBox1.Controls.Add(this.rdoDataGraphicElement);
            this.groupBox1.Controls.Add(this.rdoNorth);
            this.groupBox1.Controls.Add(this.rdoConstantTextElement);
            this.groupBox1.Controls.Add(this.rdoScaleBar);
            this.groupBox1.Controls.Add(this.rdoSingleTextElement);
            this.groupBox1.Controls.Add(this.rdoOle);
            this.groupBox1.Controls.Add(this.rdoMultiTextElement);
            this.groupBox1.Controls.Add(this.rdoPicture);
            this.groupBox1.Controls.Add(this.rdoJoinTableElement);
            this.groupBox1.Controls.Add(this.rdoLegend);
            this.groupBox1.Controls.Add(this.rdoScaleText);
            this.groupBox1.Controls.Add(this.rdoCustomLegend);
            this.groupBox1.Location = new Point(12, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(189, 286);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "元素类型";
            this.rdoNorth.AutoSize = true;
            this.rdoNorth.Location = new Point(17, 240);
            this.rdoNorth.Name = "rdoNorth";
            this.rdoNorth.Size = new Size(59, 16);
            this.rdoNorth.TabIndex = 12;
            this.rdoNorth.Text = "指北针";
            this.rdoNorth.UseVisualStyleBackColor = true;
            this.rdoNorth.CheckedChanged += new EventHandler(this.rdoNorth_CheckedChanged);
            this.rdoDataGraphicElement.AutoSize = true;
            this.rdoDataGraphicElement.Location = new Point(16, 262);
            this.rdoDataGraphicElement.Name = "rdoDataGraphicElement";
            this.rdoDataGraphicElement.Size = new Size(71, 16);
            this.rdoDataGraphicElement.TabIndex = 13;
            this.rdoDataGraphicElement.Text = "图表元素";
            this.rdoDataGraphicElement.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.groupBox1);
            base.Name = "ElementTypeSelectPage";
            base.Size = new Size(293, 320);
            base.Load += new EventHandler(this.ElementTypeSelectPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private GroupBox groupBox1;
        private RadioButton rdoConstantTextElement;
        private RadioButton rdoCustomLegend;
        private RadioButton rdoDataGraphicElement;
        private RadioButton rdoJoinTableElement;
        private RadioButton rdoLegend;
        private RadioButton rdoMultiTextElement;
        private RadioButton rdoNorth;
        private RadioButton rdoOle;
        private RadioButton rdoPicture;
        private RadioButton rdoScaleBar;
        private RadioButton rdoScaleText;
        private RadioButton rdoSingleTextElement;
    }
}