using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.DesignLib
{
   
    partial class TFInfoPage
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
            this.label6 = new Label();
            this.label5 = new Label();
            this.txtYInterval = new TextBox();
            this.txtXInterval = new TextBox();
            this.label7 = new Label();
            this.label8 = new Label();
            this.txtStartMultiple = new TextBox();
            this.label9 = new Label();
            this.txtTitleSpace = new TextBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtOutBorderWidth = new TextBox();
            this.label3 = new Label();
            this.label4 = new Label();
            this.txtInOutDis = new TextBox();
            this.label10 = new Label();
            this.label11 = new Label();
            base.SuspendLayout();
            this.label6.AutoSize = true;
            this.label6.Location = new Point(194, 126);
            this.label6.Name = "label6";
            this.label6.Size = new Size(17, 12);
            this.label6.TabIndex = 19;
            this.label6.Text = "米";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(194, 95);
            this.label5.Name = "label5";
            this.label5.Size = new Size(17, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "米";
            this.txtYInterval.Location = new Point(114, 123);
            this.txtYInterval.Name = "txtYInterval";
            this.txtYInterval.Size = new Size(74, 21);
            this.txtYInterval.TabIndex = 17;
            this.txtYInterval.Text = "1000";
            this.txtYInterval.TextChanged += new EventHandler(this.txtInOutDis_TextChanged);
            this.txtXInterval.Location = new Point(114, 95);
            this.txtXInterval.Name = "txtXInterval";
            this.txtXInterval.Size = new Size(74, 21);
            this.txtXInterval.TabIndex = 16;
            this.txtXInterval.Text = "1000";
            this.txtXInterval.TextChanged += new EventHandler(this.txtInOutDis_TextChanged);
            this.label7.AutoSize = true;
            this.label7.Location = new Point(19, 126);
            this.label7.Name = "label7";
            this.label7.Size = new Size(77, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "格网竖直间距";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(19, 95);
            this.label8.Name = "label8";
            this.label8.Size = new Size(77, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "格网水平间距";
            this.txtStartMultiple.Location = new Point(114, 149);
            this.txtStartMultiple.Name = "txtStartMultiple";
            this.txtStartMultiple.Size = new Size(74, 21);
            this.txtStartMultiple.TabIndex = 21;
            this.txtStartMultiple.Text = "10";
            this.txtStartMultiple.TextChanged += new EventHandler(this.txtInOutDis_TextChanged);
            this.label9.AutoSize = true;
            this.label9.Location = new Point(19, 152);
            this.label9.Name = "label9";
            this.label9.Size = new Size(89, 12);
            this.label9.TabIndex = 20;
            this.label9.Text = "起始点坐标倍数";
            this.txtTitleSpace.Location = new Point(114, 11);
            this.txtTitleSpace.Name = "txtTitleSpace";
            this.txtTitleSpace.Size = new Size(74, 21);
            this.txtTitleSpace.TabIndex = 68;
            this.txtTitleSpace.Text = "0.1";
            this.txtTitleSpace.TextChanged += new EventHandler(this.txtInOutDis_TextChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(19, 14);
            this.label1.Name = "label1";
            this.label1.Size = new Size(89, 12);
            this.label1.TabIndex = 67;
            this.label1.Text = "图名与外框间距";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(194, 70);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 12);
            this.label2.TabIndex = 66;
            this.label2.Text = "厘米";
            this.txtOutBorderWidth.Location = new Point(114, 67);
            this.txtOutBorderWidth.Name = "txtOutBorderWidth";
            this.txtOutBorderWidth.Size = new Size(74, 21);
            this.txtOutBorderWidth.TabIndex = 65;
            this.txtOutBorderWidth.Text = "0.1";
            this.txtOutBorderWidth.TextChanged += new EventHandler(this.txtInOutDis_TextChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(19, 70);
            this.label3.Name = "label3";
            this.label3.Size = new Size(53, 12);
            this.label3.TabIndex = 64;
            this.label3.Text = "外框宽度";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(194, 41);
            this.label4.Name = "label4";
            this.label4.Size = new Size(29, 12);
            this.label4.TabIndex = 63;
            this.label4.Text = "厘米";
            this.txtInOutDis.Location = new Point(114, 38);
            this.txtInOutDis.Name = "txtInOutDis";
            this.txtInOutDis.Size = new Size(74, 21);
            this.txtInOutDis.TabIndex = 62;
            this.txtInOutDis.Text = "1";
            this.txtInOutDis.TextChanged += new EventHandler(this.txtInOutDis_TextChanged);
            this.label10.AutoSize = true;
            this.label10.Location = new Point(19, 41);
            this.label10.Name = "label10";
            this.label10.Size = new Size(65, 12);
            this.label10.TabIndex = 61;
            this.label10.Text = "内外框间距";
            this.label11.AutoSize = true;
            this.label11.Location = new Point(194, 14);
            this.label11.Name = "label11";
            this.label11.Size = new Size(29, 12);
            this.label11.TabIndex = 69;
            this.label11.Text = "厘米";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.label11);
            base.Controls.Add(this.txtStartMultiple);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.label9);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.txtTitleSpace);
            base.Controls.Add(this.txtYInterval);
            base.Controls.Add(this.txtXInterval);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label10);
            base.Controls.Add(this.txtOutBorderWidth);
            base.Controls.Add(this.txtInOutDis);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label4);
            base.Name = "TFInfoPage";
            base.Size = new Size(312, 196);
            base.Load += new EventHandler(this.TFInfoPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private TextBox txtInOutDis;
        private TextBox txtOutBorderWidth;
        private TextBox txtStartMultiple;
        private TextBox txtTitleSpace;
        private TextBox txtXInterval;
        private TextBox txtYInterval;
    }
}