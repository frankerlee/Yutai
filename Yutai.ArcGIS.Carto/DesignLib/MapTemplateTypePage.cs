using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class MapTemplateTypePage : UserControl
    {
        private IContainer icontainer_0 = null;
        private Label label1;
        private Label label2;
        internal int m_MapTemplateType = 0;
        private RadioButton radioButton2;
        private RadioButton rdoRectangle;

        public MapTemplateTypePage()
        {
            this.InitializeComponent();
        }

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
            this.rdoRectangle = new RadioButton();
            this.radioButton2 = new RadioButton();
            this.label1 = new Label();
            this.label2 = new Label();
            base.SuspendLayout();
            this.rdoRectangle.AutoSize = true;
            this.rdoRectangle.Checked = true;
            this.rdoRectangle.Location = new Point(0x13, 0x17);
            this.rdoRectangle.Name = "rdoRectangle";
            this.rdoRectangle.Size = new Size(0x47, 0x10);
            this.rdoRectangle.TabIndex = 0;
            this.rdoRectangle.TabStop = true;
            this.rdoRectangle.Text = "矩形分幅";
            this.rdoRectangle.UseVisualStyleBackColor = true;
            this.rdoRectangle.CheckedChanged += new EventHandler(this.rdoRectangle_CheckedChanged);
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new Point(0x13, 0x5c);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new Size(0x47, 0x10);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "梯形分幅";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new EventHandler(this.radioButton2_CheckedChanged);
            this.label1.Location = new Point(0x13, 0x2e);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xde, 0x2b);
            this.label1.TabIndex = 2;
            this.label1.Text = "用于大比例尺地图模板创建，其图幅是矩形。";
            this.label2.Location = new Point(0x13, 0x7b);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0xde, 0x31);
            this.label2.TabIndex = 3;
            this.label2.Text = "用于中小比例尺地图模板创建，其图幅是梯形。";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.radioButton2);
            base.Controls.Add(this.rdoRectangle);
            base.Name = "MapTemplateTypePage";
            base.Size = new Size(0x10f, 0xe8);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton2.Checked)
            {
                this.m_MapTemplateType = 1;
            }
        }

        private void rdoRectangle_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoRectangle.Checked)
            {
                this.m_MapTemplateType = 0;
            }
        }
    }
}

