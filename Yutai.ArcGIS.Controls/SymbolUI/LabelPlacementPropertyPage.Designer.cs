using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class LabelPlacementPropertyPage
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
            this.groupBoxPoint = new GroupBox();
            this.pointFeatureLabelCtrl1 = new PointFeatureLabelCtrl();
            this.groupBoxLine = new GroupBox();
            this.lineFeaturePlaceSetCtrl1 = new LineFeaturePlaceSetCtrl();
            this.groupBoxFill = new GroupBox();
            this.fillFeaturePlaceCtrl1 = new FillFeaturePlaceCtrl();
            this.groupBox4 = new GroupBox();
            this.radioGroup1 = new RadioGroup();
            this.label1 = new Label();
            this.cboFeatureType = new ComboBoxEdit();
            this.groupBoxPoint.SuspendLayout();
            this.groupBoxLine.SuspendLayout();
            this.groupBoxFill.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.radioGroup1.Properties.BeginInit();
            this.cboFeatureType.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBoxPoint.Controls.Add(this.pointFeatureLabelCtrl1);
            this.groupBoxPoint.Location = new Point(8, 8);
            this.groupBoxPoint.Name = "groupBoxPoint";
            this.groupBoxPoint.Size = new Size(344, 272);
            this.groupBoxPoint.TabIndex = 0;
            this.groupBoxPoint.TabStop = false;
            this.groupBoxPoint.Text = "点设置";
            this.pointFeatureLabelCtrl1.Location = new Point(8, 16);
            this.pointFeatureLabelCtrl1.Name = "pointFeatureLabelCtrl1";
            this.pointFeatureLabelCtrl1.Size = new Size(232, 248);
            this.pointFeatureLabelCtrl1.TabIndex = 0;
            this.pointFeatureLabelCtrl1.Title = null;
            this.groupBoxLine.Controls.Add(this.lineFeaturePlaceSetCtrl1);
            this.groupBoxLine.Location = new Point(8, 8);
            this.groupBoxLine.Name = "groupBoxLine";
            this.groupBoxLine.Size = new Size(344, 272);
            this.groupBoxLine.TabIndex = 1;
            this.groupBoxLine.TabStop = false;
            this.groupBoxLine.Text = "线设置";
            this.lineFeaturePlaceSetCtrl1.Location = new Point(8, 16);
            this.lineFeaturePlaceSetCtrl1.Name = "lineFeaturePlaceSetCtrl1";
            this.lineFeaturePlaceSetCtrl1.Size = new Size(328, 232);
            this.lineFeaturePlaceSetCtrl1.TabIndex = 0;
            this.groupBoxFill.Controls.Add(this.fillFeaturePlaceCtrl1);
            this.groupBoxFill.Location = new Point(8, 8);
            this.groupBoxFill.Name = "groupBoxFill";
            this.groupBoxFill.Size = new Size(344, 272);
            this.groupBoxFill.TabIndex = 2;
            this.groupBoxFill.TabStop = false;
            this.groupBoxFill.Text = "多边形设置";
            this.fillFeaturePlaceCtrl1.Location = new Point(8, 16);
            this.fillFeaturePlaceCtrl1.Name = "fillFeaturePlaceCtrl1";
            this.fillFeaturePlaceCtrl1.Size = new Size(304, 224);
            this.fillFeaturePlaceCtrl1.TabIndex = 0;
            this.groupBox4.Controls.Add(this.radioGroup1);
            this.groupBox4.Location = new Point(8, 288);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(240, 112);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "重复标注";
            this.radioGroup1.Location = new Point(8, 24);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "删除重复标注"), new RadioGroupItem(null, "每个要素放置一个标注"), new RadioGroupItem(null, "每个要素的局部防置一个标注") });
            this.radioGroup1.Size = new Size(200, 80);
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(256, 288);
            this.label1.Name = "label1";
            this.label1.Size = new Size(54, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "要素类型";
            this.cboFeatureType.EditValue = "线";
            this.cboFeatureType.Location = new Point(256, 304);
            this.cboFeatureType.Name = "cboFeatureType";
            this.cboFeatureType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFeatureType.Properties.Items.AddRange(new object[] { "点", "线", "面" });
            this.cboFeatureType.Size = new Size(104, 23);
            this.cboFeatureType.TabIndex = 5;
            this.cboFeatureType.SelectedIndexChanged += new EventHandler(this.cboFeatureType_SelectedIndexChanged);
            base.Controls.Add(this.cboFeatureType);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.groupBox4);
            base.Controls.Add(this.groupBoxFill);
            base.Controls.Add(this.groupBoxLine);
            base.Controls.Add(this.groupBoxPoint);
            base.Name = "LabelPlacementPropertyPage";
            base.Size = new Size(368, 408);
            base.Load += new EventHandler(this.LabelPlacementPropertyPage_Load);
            this.groupBoxPoint.ResumeLayout(false);
            this.groupBoxLine.ResumeLayout(false);
            this.groupBoxFill.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.radioGroup1.Properties.EndInit();
            this.cboFeatureType.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private ComboBoxEdit cboFeatureType;
        private FillFeaturePlaceCtrl fillFeaturePlaceCtrl1;
        private GroupBox groupBox4;
        private GroupBox groupBoxFill;
        private GroupBox groupBoxLine;
        private GroupBox groupBoxPoint;
        private Label label1;
        private LineFeaturePlaceSetCtrl lineFeaturePlaceSetCtrl1;
        private PointFeatureLabelCtrl pointFeatureLabelCtrl1;
        private RadioGroup radioGroup1;
    }
}