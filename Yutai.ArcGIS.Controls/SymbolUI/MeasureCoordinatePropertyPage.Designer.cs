using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class MeasureCoordinatePropertyPage
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
            this.radioGroup = new RadioGroup();
            this.btnCoordinate = new SimpleButton();
            this.radioGroup.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.Location = new Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(72, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "使用数据框的当前系统";
            this.radioGroup.Location = new Point(24, 40);
            this.radioGroup.Name = "radioGroup";
            this.radioGroup.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "使用数据框的当前系统"), new RadioGroupItem(null, "使用另一种坐标系统") });
            this.radioGroup.Size = new Size(184, 56);
            this.radioGroup.TabIndex = 1;
            this.radioGroup.SelectedIndexChanged += new EventHandler(this.radioGroup_SelectedIndexChanged);
            this.btnCoordinate.Location = new Point(184, 72);
            this.btnCoordinate.Click += new EventHandler(this.btnCoordinate_Click);
            this.btnCoordinate.Name = "btnCoordinate";
            this.btnCoordinate.Size = new Size(64, 24);
            this.btnCoordinate.TabIndex = 2;
            this.btnCoordinate.Text = "属性";
            base.Controls.Add(this.btnCoordinate);
            base.Controls.Add(this.radioGroup);
            base.Controls.Add(this.label1);
            base.Name = "MeasureCoordinatePropertyPage";
            base.Size = new Size(256, 232);
            base.Load += new EventHandler(this.MeasureCoordinatePropertyPage_Load);
            this.radioGroup.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private SimpleButton btnCoordinate;
        private Label label1;
        private RadioGroup radioGroup;
    }
}