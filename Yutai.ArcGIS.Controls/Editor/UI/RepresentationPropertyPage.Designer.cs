using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class RepresentationPropertyPage
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
            this.label1 = new Label();
            this.comboBox1 = new ComboBox();
            this.checkBox1 = new CheckBox();
            this.panel1 = new Panel();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(2, 10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "制图表现规则";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new Point(4, 25);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(164, 20);
            this.comboBox1.TabIndex = 1;
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new Point(3, 51);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(48, 16);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "可见";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 76);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(177, 178);
            this.panel1.TabIndex = 3;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.checkBox1);
            base.Controls.Add(this.comboBox1);
            base.Controls.Add(this.label1);
            base.Name = "RepresentationPropertyPage";
            base.Size = new Size(177, 254);
            base.Load += new EventHandler(this.RepresentationPropertyPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private IContainer components = null;
        private CheckBox checkBox1;
        private ComboBox comboBox1;
        private Label label1;
        private Panel panel1;
            private int _id;
            private string _name;
            private IRepresentationRule _RepresentationRule;
    }
}