using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    partial class GroupElementExPropertyPage
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
            this.rdoFiexdSize = new RadioButton();
            this.rdoSameAsWidth = new RadioButton();
            this.rdoWidthScale = new RadioButton();
            this.txtScale = new TextBox();
            base.SuspendLayout();
            this.rdoFiexdSize.AutoSize = true;
            this.rdoFiexdSize.Location = new Point(21, 26);
            this.rdoFiexdSize.Name = "rdoFiexdSize";
            this.rdoFiexdSize.Size = new Size(71, 16);
            this.rdoFiexdSize.TabIndex = 2;
            this.rdoFiexdSize.Text = "固定大小";
            this.rdoFiexdSize.UseVisualStyleBackColor = true;
            this.rdoFiexdSize.CheckedChanged += new EventHandler(this.rdoWidthScale_CheckedChanged);
            this.rdoSameAsWidth.AutoSize = true;
            this.rdoSameAsWidth.Checked = true;
            this.rdoSameAsWidth.Location = new Point(21, 55);
            this.rdoSameAsWidth.Name = "rdoSameAsWidth";
            this.rdoSameAsWidth.Size = new Size(95, 16);
            this.rdoSameAsWidth.TabIndex = 3;
            this.rdoSameAsWidth.TabStop = true;
            this.rdoSameAsWidth.Text = "同内图廓宽度";
            this.rdoSameAsWidth.UseVisualStyleBackColor = true;
            this.rdoSameAsWidth.CheckedChanged += new EventHandler(this.rdoWidthScale_CheckedChanged);
            this.rdoWidthScale.AutoSize = true;
            this.rdoWidthScale.Location = new Point(21, 86);
            this.rdoWidthScale.Name = "rdoWidthScale";
            this.rdoWidthScale.Size = new Size(155, 16);
            this.rdoWidthScale.TabIndex = 4;
            this.rdoWidthScale.Text = "同内图廓宽度按比例缩放";
            this.rdoWidthScale.UseVisualStyleBackColor = true;
            this.rdoWidthScale.CheckedChanged += new EventHandler(this.rdoWidthScale_CheckedChanged);
            this.txtScale.Location = new Point(40, 108);
            this.txtScale.Name = "txtScale";
            this.txtScale.ReadOnly = true;
            this.txtScale.Size = new Size(136, 21);
            this.txtScale.TabIndex = 5;
            this.txtScale.Text = "1";
            this.txtScale.TextChanged += new EventHandler(this.txtScale_TextChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.txtScale);
            base.Controls.Add(this.rdoWidthScale);
            base.Controls.Add(this.rdoSameAsWidth);
            base.Controls.Add(this.rdoFiexdSize);
            base.Name = "GroupElementExPropertyPage";
            base.Size = new Size(252, 206);
            base.Load += new EventHandler(this.GroupElementExPropertyPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private MapCartoTemplateLib.MapTemplateElement mapTemplateElement_0;
        private RadioButton rdoFiexdSize;
        private RadioButton rdoSameAsWidth;
        private RadioButton rdoWidthScale;
        private TextBox txtScale;
    }
}