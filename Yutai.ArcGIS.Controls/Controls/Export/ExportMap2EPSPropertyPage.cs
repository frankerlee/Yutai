using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Output;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    internal class ExportMap2EPSPropertyPage : UserControl
    {
        private ComboBoxEdit cboColorspace;
        private ComboBoxEdit cboImageCompression;
        private ComboBoxEdit cboPSLangugeLevel;
        private CheckEdit chkEmbedFonts;
        private CheckEdit chkPolygonizeMarkers;
        private ComboBoxEdit comboBoxEdit1;
        private Container components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private bool m_CanDo = false;
        private IExport m_pExport = null;

        public ExportMap2EPSPropertyPage()
        {
            this.InitializeComponent();
        }

        private void cboColorspace_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo && (this.cboColorspace.SelectedIndex != -1))
            {
                (this.m_pExport as IExportColorspaceSettings).Colorspace = (esriExportColorspace) this.cboColorspace.SelectedIndex;
            }
        }

        private void cboEmbedFonts_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                (this.m_pExport as IExportPS).EmbedFonts = this.chkEmbedFonts.Checked;
            }
        }

        private void cboImageCompression_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo && (this.cboImageCompression.SelectedIndex != -1))
            {
                (this.m_pExport as IExportPS).ImageCompression = (esriExportImageCompression) this.cboImageCompression.SelectedIndex;
            }
        }

        private void cboPSLangugeLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo && (this.cboPSLangugeLevel.SelectedIndex != -1))
            {
                (this.m_pExport as IExportPS).LanguageLevel = (esriExportPSLanguageLevel) (this.cboPSLangugeLevel.SelectedIndex + 2);
            }
        }

        private void chkPolygonizeMarkers_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                (this.m_pExport as IExportVectorOptions).PolygonizeMarkers = this.chkPolygonizeMarkers.Checked;
            }
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo && (this.comboBoxEdit1.SelectedIndex != -1))
            {
                (this.m_pExport as IExportPS).Image = (esriExportPSImage) this.comboBoxEdit1.SelectedIndex;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void ExportMap2EMFPropertyPage_Load(object sender, EventArgs e)
        {
            if (this.m_pExport != null)
            {
                this.chkPolygonizeMarkers.Checked = (this.m_pExport as IExportVectorOptions).PolygonizeMarkers;
                this.cboColorspace.SelectedIndex = (int) (this.m_pExport as IExportColorspaceSettings).Colorspace;
                this.chkEmbedFonts.Checked = (this.m_pExport as IExportPS).EmbedFonts;
                this.cboPSLangugeLevel.SelectedIndex = ((int) (this.m_pExport as IExportPS).LanguageLevel) - 2;
                this.cboImageCompression.SelectedIndex = (int) (this.m_pExport as IExportPS).ImageCompression;
                this.comboBoxEdit1.SelectedIndex = (int) (this.m_pExport as IExportPS).Image;
                this.m_CanDo = true;
            }
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
            this.chkPolygonizeMarkers.Location = new Point(8, 0x80);
            this.chkPolygonizeMarkers.Name = "chkPolygonizeMarkers";
            this.chkPolygonizeMarkers.Properties.Caption = "转换标记符号为多边形";
            this.chkPolygonizeMarkers.Size = new Size(0x90, 0x13);
            this.chkPolygonizeMarkers.TabIndex = 4;
            this.chkPolygonizeMarkers.CheckedChanged += new EventHandler(this.chkPolygonizeMarkers_CheckedChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x55, 0x11);
            this.label2.TabIndex = 5;
            this.label2.Text = "目标颜色空间:";
            this.cboColorspace.EditValue = "RGB";
            this.cboColorspace.Location = new Point(0x60, 8);
            this.cboColorspace.Name = "cboColorspace";
            this.cboColorspace.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboColorspace.Properties.Items.AddRange(new object[] { "RGB", "CMYK" });
            this.cboColorspace.Size = new Size(0x60, 0x17);
            this.cboColorspace.TabIndex = 6;
            this.cboColorspace.SelectedIndexChanged += new EventHandler(this.cboColorspace_SelectedIndexChanged);
            this.cboPSLangugeLevel.EditValue = "2";
            this.cboPSLangugeLevel.Location = new Point(0x60, 0x24);
            this.cboPSLangugeLevel.Name = "cboPSLangugeLevel";
            this.cboPSLangugeLevel.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboPSLangugeLevel.Properties.Items.AddRange(new object[] { "2", "3" });
            this.cboPSLangugeLevel.Size = new Size(0x60, 0x17);
            this.cboPSLangugeLevel.TabIndex = 8;
            this.cboPSLangugeLevel.SelectedIndexChanged += new EventHandler(this.cboPSLangugeLevel_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 0x24);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x48, 0x11);
            this.label3.TabIndex = 7;
            this.label3.Text = "PS语言等级:";
            this.chkEmbedFonts.Location = new Point(8, 0x98);
            this.chkEmbedFonts.Name = "chkEmbedFonts";
            this.chkEmbedFonts.Properties.Caption = "内置所有文档字体";
            this.chkEmbedFonts.Size = new Size(0x90, 0x13);
            this.chkEmbedFonts.TabIndex = 9;
            this.chkEmbedFonts.CheckedChanged += new EventHandler(this.cboEmbedFonts_CheckedChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x40);
            this.label1.Name = "label1";
            this.label1.Size = new Size(60, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "图像压缩:";
            this.cboImageCompression.EditValue = "无";
            this.cboImageCompression.Location = new Point(0x60, 0x40);
            this.cboImageCompression.Name = "cboImageCompression";
            this.cboImageCompression.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboImageCompression.Properties.Items.AddRange(new object[] { "无", "RLE", "Deflate", "LZW" });
            this.cboImageCompression.Size = new Size(0x60, 0x17);
            this.cboImageCompression.TabIndex = 10;
            this.cboImageCompression.SelectedIndexChanged += new EventHandler(this.cboImageCompression_SelectedIndexChanged);
            this.comboBoxEdit1.EditValue = "正片";
            this.comboBoxEdit1.Location = new Point(0x60, 0x60);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.comboBoxEdit1.Properties.Items.AddRange(new object[] { "正片", "负片" });
            this.comboBoxEdit1.Size = new Size(0x60, 0x17);
            this.comboBoxEdit1.TabIndex = 12;
            this.comboBoxEdit1.SelectedIndexChanged += new EventHandler(this.comboBoxEdit1_SelectedIndexChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 0x60);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x23, 0x11);
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
            base.Size = new Size(0xd8, 0xb8);
            base.Load += new EventHandler(this.ExportMap2EMFPropertyPage_Load);
            this.chkPolygonizeMarkers.Properties.EndInit();
            this.cboColorspace.Properties.EndInit();
            this.cboPSLangugeLevel.Properties.EndInit();
            this.chkEmbedFonts.Properties.EndInit();
            this.cboImageCompression.Properties.EndInit();
            this.comboBoxEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }

        public IExport Export
        {
            set
            {
                this.m_pExport = value;
            }
        }
    }
}

