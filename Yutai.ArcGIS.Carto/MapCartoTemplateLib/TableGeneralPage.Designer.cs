using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    partial class TableGeneralPage
    {
        private IContainer icontainer_0;
        protected override void Dispose(bool bool_3)
        {
            if (bool_3 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_3);
        }

       
 private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtRow = new TextBox();
            this.txtCol = new TextBox();
            this.txtWidth = new TextBox();
            this.txtHeight = new TextBox();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.label6 = new Label();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(26, 77);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "行数";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(26, 115);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "列数";
            this.txtRow.Location = new Point(73, 74);
            this.txtRow.Name = "txtRow";
            this.txtRow.Size = new Size(194, 21);
            this.txtRow.TabIndex = 2;
            this.txtRow.Text = "2";
            this.txtRow.TextChanged += new EventHandler(this.txtHeight_TextChanged);
            this.txtCol.Location = new Point(73, 112);
            this.txtCol.Name = "txtCol";
            this.txtCol.Size = new Size(194, 21);
            this.txtCol.TabIndex = 3;
            this.txtCol.Text = "3";
            this.txtCol.TextChanged += new EventHandler(this.txtHeight_TextChanged);
            this.txtWidth.Location = new Point(73, 40);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new Size(194, 21);
            this.txtWidth.TabIndex = 7;
            this.txtWidth.Text = "4";
            this.txtWidth.TextChanged += new EventHandler(this.txtHeight_TextChanged);
            this.txtHeight.Location = new Point(73, 13);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new Size(194, 21);
            this.txtHeight.TabIndex = 6;
            this.txtHeight.Text = "4";
            this.txtHeight.TextChanged += new EventHandler(this.txtHeight_TextChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(26, 43);
            this.label3.Name = "label3";
            this.label3.Size = new Size(17, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "宽";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(26, 16);
            this.label4.Name = "label4";
            this.label4.Size = new Size(17, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "高";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(273, 16);
            this.label5.Name = "label5";
            this.label5.Size = new Size(29, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "厘米";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(273, 43);
            this.label6.Name = "label6";
            this.label6.Size = new Size(29, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "厘米";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.txtWidth);
            base.Controls.Add(this.txtHeight);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.txtCol);
            base.Controls.Add(this.txtRow);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "TableGeneralPage";
            base.Size = new Size(329, 227);
            base.Load += new EventHandler(this.TableGeneralPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private bool bool_2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private MapTemplateElement mapTemplateElement_0;
        private TextBox txtCol;
        private TextBox txtHeight;
        private TextBox txtRow;
        private TextBox txtWidth;
    }
}