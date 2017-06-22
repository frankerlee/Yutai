using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Controls.Controls.GPSLib
{
    partial class SnappingPage
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
            this.checkedListBox1 = new CheckedListBox();
            this.label2 = new Label();
            this.lblUnit = new Label();
            this.txtSnapDistance = new TextBox();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(13, 11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(137, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "捕捉到以下图层的要素上";
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new Point(15, 35);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new Size(238, 132);
            this.checkedListBox1.TabIndex = 1;
            this.checkedListBox1.ItemCheck += new ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(20, 185);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "容差";
            this.lblUnit.AutoSize = true;
            this.lblUnit.Location = new Point(212, 185);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new Size(0, 12);
            this.lblUnit.TabIndex = 3;
            this.txtSnapDistance.Location = new Point(78, 181);
            this.txtSnapDistance.Name = "txtSnapDistance";
            this.txtSnapDistance.Size = new Size(111, 21);
            this.txtSnapDistance.TabIndex = 4;
            this.txtSnapDistance.TextChanged += new EventHandler(this.txtSnapDistance_TextChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.txtSnapDistance);
            base.Controls.Add(this.lblUnit);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.checkedListBox1);
            base.Controls.Add(this.label1);
            base.Name = "SnappingPage";
            base.Size = new Size(274, 241);
            base.Load += new EventHandler(this.SnappingPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private IContainer components = null;
        private CheckedListBox checkedListBox1;
        private Label label1;
        private Label label2;
        private Label lblUnit;
        private TextBox txtSnapDistance;
    }
}