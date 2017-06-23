﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    partial class ElementPosition
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
            this.groupBox1 = new GroupBox();
            this.rdoLowM = new RadioButton();
            this.rdoLeftM = new RadioButton();
            this.rdoLL = new RadioButton();
            this.rdoRL = new RadioButton();
            this.rdoRM = new RadioButton();
            this.rdoRU = new RadioButton();
            this.rdoUM = new RadioButton();
            this.rdoLU = new RadioButton();
            this.lblOffsetX = new Label();
            this.label1 = new Label();
            this.txtOffsetX = new TextBox();
            this.txtOffsetY = new TextBox();
            this.rdoUpperLeft = new RadioButton();
            this.rdoUpperRight = new RadioButton();
            this.rdoBottonLeft = new RadioButton();
            this.rdoBottomRight = new RadioButton();
            this.label2 = new Label();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rdoBottomRight);
            this.groupBox1.Controls.Add(this.rdoBottonLeft);
            this.groupBox1.Controls.Add(this.rdoUpperRight);
            this.groupBox1.Controls.Add(this.rdoUpperLeft);
            this.groupBox1.Controls.Add(this.rdoLowM);
            this.groupBox1.Controls.Add(this.rdoLeftM);
            this.groupBox1.Controls.Add(this.rdoLL);
            this.groupBox1.Controls.Add(this.rdoRL);
            this.groupBox1.Controls.Add(this.rdoRM);
            this.groupBox1.Controls.Add(this.rdoRU);
            this.groupBox1.Controls.Add(this.rdoUM);
            this.groupBox1.Controls.Add(this.rdoLU);
            this.groupBox1.Location = new Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(289, 132);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "放置位置";
            this.rdoLowM.AutoSize = true;
            this.rdoLowM.Location = new Point(106, 109);
            this.rdoLowM.Name = "rdoLowM";
            this.rdoLowM.Size = new Size(47, 16);
            this.rdoLowM.TabIndex = 7;
            this.rdoLowM.Text = "下中";
            this.rdoLowM.UseVisualStyleBackColor = true;
            this.rdoLowM.CheckedChanged += new EventHandler(this.rdoLU_CheckedChanged);
            this.rdoLeftM.AutoSize = true;
            this.rdoLeftM.Location = new Point(6, 65);
            this.rdoLeftM.Name = "rdoLeftM";
            this.rdoLeftM.Size = new Size(47, 16);
            this.rdoLeftM.TabIndex = 6;
            this.rdoLeftM.Text = "左中";
            this.rdoLeftM.UseVisualStyleBackColor = true;
            this.rdoLeftM.CheckedChanged += new EventHandler(this.rdoLU_CheckedChanged);
            this.rdoLL.AutoSize = true;
            this.rdoLL.Location = new Point(6, 87);
            this.rdoLL.Name = "rdoLL";
            this.rdoLL.Size = new Size(47, 16);
            this.rdoLL.TabIndex = 5;
            this.rdoLL.Text = "左下";
            this.rdoLL.UseVisualStyleBackColor = true;
            this.rdoLL.CheckedChanged += new EventHandler(this.rdoLU_CheckedChanged);
            this.rdoRL.AutoSize = true;
            this.rdoRL.Location = new Point(212, 87);
            this.rdoRL.Name = "rdoRL";
            this.rdoRL.Size = new Size(47, 16);
            this.rdoRL.TabIndex = 4;
            this.rdoRL.Text = "右下";
            this.rdoRL.UseVisualStyleBackColor = true;
            this.rdoRL.CheckedChanged += new EventHandler(this.rdoLU_CheckedChanged);
            this.rdoRM.AutoSize = true;
            this.rdoRM.Location = new Point(212, 65);
            this.rdoRM.Name = "rdoRM";
            this.rdoRM.Size = new Size(47, 16);
            this.rdoRM.TabIndex = 3;
            this.rdoRM.Text = "右中";
            this.rdoRM.UseVisualStyleBackColor = true;
            this.rdoRM.CheckedChanged += new EventHandler(this.rdoLU_CheckedChanged);
            this.rdoRU.AutoSize = true;
            this.rdoRU.Location = new Point(212, 42);
            this.rdoRU.Name = "rdoRU";
            this.rdoRU.Size = new Size(47, 16);
            this.rdoRU.TabIndex = 2;
            this.rdoRU.Text = "右上";
            this.rdoRU.UseVisualStyleBackColor = true;
            this.rdoRU.CheckedChanged += new EventHandler(this.rdoLU_CheckedChanged);
            this.rdoUM.AutoSize = true;
            this.rdoUM.Location = new Point(106, 20);
            this.rdoUM.Name = "rdoUM";
            this.rdoUM.Size = new Size(47, 16);
            this.rdoUM.TabIndex = 1;
            this.rdoUM.Text = "上中";
            this.rdoUM.UseVisualStyleBackColor = true;
            this.rdoUM.CheckedChanged += new EventHandler(this.rdoLU_CheckedChanged);
            this.rdoLU.AutoSize = true;
            this.rdoLU.Checked = true;
            this.rdoLU.Location = new Point(6, 42);
            this.rdoLU.Name = "rdoLU";
            this.rdoLU.Size = new Size(47, 16);
            this.rdoLU.TabIndex = 0;
            this.rdoLU.TabStop = true;
            this.rdoLU.Text = "左上";
            this.rdoLU.UseVisualStyleBackColor = true;
            this.rdoLU.CheckedChanged += new EventHandler(this.rdoLU_CheckedChanged);
            this.lblOffsetX.AutoSize = true;
            this.lblOffsetX.Location = new Point(29, 157);
            this.lblOffsetX.Name = "lblOffsetX";
            this.lblOffsetX.Size = new Size(53, 12);
            this.lblOffsetX.TabIndex = 1;
            this.lblOffsetX.Text = "水平偏移";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(29, 190);
            this.label1.Name = "label1";
            this.label1.Size = new Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "竖直偏移";
            this.txtOffsetX.Location = new Point(88, 154);
            this.txtOffsetX.Name = "txtOffsetX";
            this.txtOffsetX.Size = new Size(90, 21);
            this.txtOffsetX.TabIndex = 3;
            this.txtOffsetX.Text = "0";
            this.txtOffsetX.TextChanged += new EventHandler(this.txtOffsetY_TextChanged);
            this.txtOffsetY.Location = new Point(88, 187);
            this.txtOffsetY.Name = "txtOffsetY";
            this.txtOffsetY.Size = new Size(90, 21);
            this.txtOffsetY.TabIndex = 4;
            this.txtOffsetY.Text = "0";
            this.txtOffsetY.TextChanged += new EventHandler(this.txtOffsetY_TextChanged);
            this.rdoUpperLeft.AutoSize = true;
            this.rdoUpperLeft.Location = new Point(53, 20);
            this.rdoUpperLeft.Name = "rdoUpperLeft";
            this.rdoUpperLeft.Size = new Size(47, 16);
            this.rdoUpperLeft.TabIndex = 8;
            this.rdoUpperLeft.Text = "上左";
            this.rdoUpperLeft.UseVisualStyleBackColor = true;
            this.rdoUpperRight.AutoSize = true;
            this.rdoUpperRight.Location = new Point(159, 20);
            this.rdoUpperRight.Name = "rdoUpperRight";
            this.rdoUpperRight.Size = new Size(47, 16);
            this.rdoUpperRight.TabIndex = 9;
            this.rdoUpperRight.Text = "上右";
            this.rdoUpperRight.UseVisualStyleBackColor = true;
            this.rdoBottonLeft.AutoSize = true;
            this.rdoBottonLeft.Location = new Point(53, 109);
            this.rdoBottonLeft.Name = "rdoBottonLeft";
            this.rdoBottonLeft.Size = new Size(47, 16);
            this.rdoBottonLeft.TabIndex = 10;
            this.rdoBottonLeft.Text = "下左";
            this.rdoBottonLeft.UseVisualStyleBackColor = true;
            this.rdoBottomRight.AutoSize = true;
            this.rdoBottomRight.Location = new Point(168, 109);
            this.rdoBottomRight.Name = "rdoBottomRight";
            this.rdoBottomRight.Size = new Size(47, 16);
            this.rdoBottomRight.TabIndex = 11;
            this.rdoBottomRight.Text = "下右";
            this.rdoBottomRight.UseVisualStyleBackColor = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new Point(60, 45);
            this.label2.Name = "label2";
            this.label2.Size = new Size(130, 58);
            this.label2.TabIndex = 12;
            this.label2.Text = "图框";
            this.label2.TextAlign = ContentAlignment.MiddleCenter;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.txtOffsetY);
            base.Controls.Add(this.txtOffsetX);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.lblOffsetX);
            base.Controls.Add(this.groupBox1);
            base.Name = "ElementPosition";
            base.Size = new Size(325, 223);
            base.Load += new EventHandler(this.ElementPosition_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label lblOffsetX;
        private RadioButton rdoBottomRight;
        private RadioButton rdoBottonLeft;
        private RadioButton rdoLeftM;
        private RadioButton rdoLL;
        private RadioButton rdoLowM;
        private RadioButton rdoLU;
        private RadioButton rdoRL;
        private RadioButton rdoRM;
        private RadioButton rdoRU;
        private RadioButton rdoUM;
        private RadioButton rdoUpperLeft;
        private RadioButton rdoUpperRight;
        private TextBox txtOffsetX;
        private TextBox txtOffsetY;
    }
}