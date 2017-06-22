using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class NewNetworkDatasetDirectionPropertyPage
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
            this.rdoTrue = new RadioButton();
            this.rdoFalse = new RadioButton();
            this.label1 = new Label();
            this.btnDirections = new SimpleButton();
            base.SuspendLayout();
            this.rdoTrue.AutoSize = true;
            this.rdoTrue.Checked = true;
            this.rdoTrue.Location = new Point(24, 63);
            this.rdoTrue.Name = "rdoTrue";
            this.rdoTrue.Size = new Size(35, 16);
            this.rdoTrue.TabIndex = 0;
            this.rdoTrue.TabStop = true;
            this.rdoTrue.Text = "是";
            this.rdoTrue.UseVisualStyleBackColor = true;
            this.rdoTrue.CheckedChanged += new EventHandler(this.rdoTrue_CheckedChanged);
            this.rdoFalse.AutoSize = true;
            this.rdoFalse.Location = new Point(24, 41);
            this.rdoFalse.Name = "rdoFalse";
            this.rdoFalse.Size = new Size(35, 16);
            this.rdoFalse.TabIndex = 1;
            this.rdoFalse.Text = "否";
            this.rdoFalse.UseVisualStyleBackColor = true;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(22, 15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(173, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "是否为网络要素集添加方向设置";
            this.btnDirections.Enabled = false;
            this.btnDirections.Location = new Point(61, 108);
            this.btnDirections.Name = "btnDirections";
            this.btnDirections.Size = new Size(85, 23);
            this.btnDirections.TabIndex = 3;
            this.btnDirections.Text = "方向设置...";
            this.btnDirections.Click += new EventHandler(this.btnDirections_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.btnDirections);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.rdoFalse);
            base.Controls.Add(this.rdoTrue);
            base.Name = "NewNetworkDatasetDirectionPropertyPage";
            base.Size = new Size(326, 265);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnDirections;
        private Label label1;
        private RadioButton rdoFalse;
        private RadioButton rdoTrue;
    }
}