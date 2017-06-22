using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class ReprensationGeneralPage
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
            this.label5 = new Label();
            this.label4 = new Label();
            this.txtruleIDFldName = new TextBox();
            this.txtoverrideFldName = new TextBox();
            this.groupBox1 = new GroupBox();
            this.radioButton2 = new RadioButton();
            this.rdoRequireShapeOverride = new RadioButton();
            this.txtRepresentationName = new TextBox();
            this.label1 = new Label();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.label5.AutoSize = true;
            this.label5.Location = new Point(19, 62);
            this.label5.Name = "label5";
            this.label5.Size = new Size(53, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "重载字段";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(19, 36);
            this.label4.Name = "label4";
            this.label4.Size = new Size(53, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "规则字段";
            this.txtruleIDFldName.Location = new Point(101, 33);
            this.txtruleIDFldName.Name = "txtruleIDFldName";
            this.txtruleIDFldName.Size = new Size(165, 21);
            this.txtruleIDFldName.TabIndex = 5;
            this.txtoverrideFldName.Location = new Point(101, 59);
            this.txtoverrideFldName.Name = "txtoverrideFldName";
            this.txtoverrideFldName.Size = new Size(165, 21);
            this.txtoverrideFldName.TabIndex = 7;
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.rdoRequireShapeOverride);
            this.groupBox1.Location = new Point(21, 97);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(255, 70);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "制图几何对象编辑时的行为";
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new Point(12, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new Size(131, 16);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "改变要素的几何对象";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.rdoRequireShapeOverride.AutoSize = true;
            this.rdoRequireShapeOverride.Checked = true;
            this.rdoRequireShapeOverride.Location = new Point(12, 20);
            this.rdoRequireShapeOverride.Name = "rdoRequireShapeOverride";
            this.rdoRequireShapeOverride.Size = new Size(239, 16);
            this.rdoRequireShapeOverride.TabIndex = 0;
            this.rdoRequireShapeOverride.TabStop = true;
            this.rdoRequireShapeOverride.Text = "存储变化的几何对象作为制图表现的重载";
            this.rdoRequireShapeOverride.UseVisualStyleBackColor = true;
            this.txtRepresentationName.Location = new Point(101, 6);
            this.txtRepresentationName.Name = "txtRepresentationName";
            this.txtRepresentationName.Size = new Size(165, 21);
            this.txtRepresentationName.TabIndex = 15;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(18, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(83, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "制图表现名称:";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.txtruleIDFldName);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.txtoverrideFldName);
            base.Controls.Add(this.txtRepresentationName);
            base.Controls.Add(this.label1);
            base.Name = "ReprensationGeneralPage";
            base.Size = new Size(295, 215);
            base.Load += new EventHandler(this.ReprensationGeneralPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private GroupBox groupBox1;
        private Label label1;
        private Label label4;
        private Label label5;
        private RadioButton radioButton2;
        private RadioButton rdoRequireShapeOverride;
        private TextBox txtoverrideFldName;
        private TextBox txtRepresentationName;
        private TextBox txtruleIDFldName;
    }
}