using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Output;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    partial class ExportMap2EPSPropertyPage
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
            this.chkPolygonizeMarkers = new CheckEdit();
            this.label2 = new Label();
            this.cboColorspace = new ComboBoxEdit();
            this.cboPSLangugeLevel = new ComboBoxEdit();
            this.label3 = new Label();
            this.chkEmbedFonts = new CheckEdit();
            this.label1 = new Label();
            this.cboImageCompression = new ComboBoxEdit();
            this.comboBoxEdit1 = new ComboBoxEdit();
            this.label4 = new Label();
            this.chkPolygonizeMarkers.Properties.BeginInit();
            this.cboColorspace.Properties.BeginInit();
            this.cboPSLangugeLevel.Properties.BeginInit();
            this.chkEmbedFonts.Properties.BeginInit();
            this.cboImageCompression.Properties.BeginInit();
            this.comboBoxEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.chkPolygonizeMarkers.Location = new Point(8, 128);
            this.chkPolygonizeMarkers.Name = "chkPolygonizeMarkers";
            this.chkPolygonizeMarkers.Properties.Caption = "转换标记符号为多边形";
            this.chkPolygonizeMarkers.Size = new Size(144, 19);
            this.chkPolygonizeMarkers.TabIndex = 4;
            this.chkPolygonizeMarkers.CheckedChanged += new EventHandler(this.chkPolygonizeMarkers_CheckedChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new Size(85, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "目标颜色空间:";
            this.cboColorspace.EditValue = "RGB";
            this.cboColorspace.Location = new Point(96, 8);
            this.cboColorspace.Name = "cboColorspace";
            this.cboColorspace.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboColorspace.Properties.Items.AddRange(new object[] { "RGB", "CMYK" });
            this.cboColorspace.Size = new Size(96, 23);
            this.cboColorspace.TabIndex = 6;
            this.cboColorspace.SelectedIndexChanged += new EventHandler(this.cboColorspace_SelectedIndexChanged);
            this.cboPSLangugeLevel.EditValue = "2";
            this.cboPSLangugeLevel.Location = new Point(96, 36);
            this.cboPSLangugeLevel.Name = "cboPSLangugeLevel";
            this.cboPSLangugeLevel.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboPSLangugeLevel.Properties.Items.AddRange(new object[] { "2", "3" });
            this.cboPSLangugeLevel.Size = new Size(96, 23);
            this.cboPSLangugeLevel.TabIndex = 8;
            this.cboPSLangugeLevel.SelectedIndexChanged += new EventHandler(this.cboPSLangugeLevel_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 36);
            this.label3.Name = "label3";
            this.label3.Size = new Size(72, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "PS语言等级:";
            this.chkEmbedFonts.Location = new Point(8, 152);
            this.chkEmbedFonts.Name = "chkEmbedFonts";
            this.chkEmbedFonts.Properties.Caption = "内置所有文档字体";
            this.chkEmbedFonts.Size = new Size(144, 19);
            this.chkEmbedFonts.TabIndex = 9;
            this.chkEmbedFonts.CheckedChanged += new EventHandler(this.cboEmbedFonts_CheckedChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 64);
            this.label1.Name = "label1";
            this.label1.Size = new Size(60, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "图像压缩:";
            this.cboImageCompression.EditValue = "无";
            this.cboImageCompression.Location = new Point(96, 64);
            this.cboImageCompression.Name = "cboImageCompression";
            this.cboImageCompression.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboImageCompression.Properties.Items.AddRange(new object[] { "无", "RLE", "Deflate", "LZW" });
            this.cboImageCompression.Size = new Size(96, 23);
            this.cboImageCompression.TabIndex = 10;
            this.cboImageCompression.SelectedIndexChanged += new EventHandler(this.cboImageCompression_SelectedIndexChanged);
            this.comboBoxEdit1.EditValue = "正片";
            this.comboBoxEdit1.Location = new Point(96, 96);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.comboBoxEdit1.Properties.Items.AddRange(new object[] { "正片", "负片" });
            this.comboBoxEdit1.Size = new Size(96, 23);
            this.comboBoxEdit1.TabIndex = 12;
            this.comboBoxEdit1.SelectedIndexChanged += new EventHandler(this.comboBoxEdit1_SelectedIndexChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 96);
            this.label4.Name = "label4";
            this.label4.Size = new Size(35, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "图像:";
            base.Controls.Add(this.comboBoxEdit1);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.cboImageCompression);
            base.Controls.Add(this.chkEmbedFonts);
            base.Controls.Add(this.cboPSLangugeLevel);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.cboColorspace);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.chkPolygonizeMarkers);
            base.Controls.Add(this.label1);
            base.Name = "ExportMap2EPSPropertyPage";
            base.Size = new Size(216, 184);
            base.Load += new EventHandler(this.ExportMap2EMFPropertyPage_Load);
            this.chkPolygonizeMarkers.Properties.EndInit();
            this.cboColorspace.Properties.EndInit();
            this.cboPSLangugeLevel.Properties.EndInit();
            this.chkEmbedFonts.Properties.EndInit();
            this.cboImageCompression.Properties.EndInit();
            this.comboBoxEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private ComboBoxEdit cboColorspace;
        private ComboBoxEdit cboImageCompression;
        private ComboBoxEdit cboPSLangugeLevel;
        private CheckEdit chkEmbedFonts;
        private CheckEdit chkPolygonizeMarkers;
        private ComboBoxEdit comboBoxEdit1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}