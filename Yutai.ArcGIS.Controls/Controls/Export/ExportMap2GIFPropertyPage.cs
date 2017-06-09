using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Output;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    internal class ExportMap2GIFPropertyPage : UserControl
    {
        private ComboBoxEdit cboImageCompression;
        private ColorEdit colorEdit1;
        private Container components = null;
        private Label label1;
        private Label label4;
        private bool m_CanDo = false;
        private IExport m_pExport = null;

        public ExportMap2GIFPropertyPage()
        {
            this.InitializeComponent();
        }

        private void cboImageCompression_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                switch (this.cboImageCompression.SelectedIndex)
                {
                    case 0:
                        (this.m_pExport as IExportGIF).CompressionType = esriGIFCompression.esriGIFCompressionNone;
                        break;

                    case 1:
                        (this.m_pExport as IExportGIF).CompressionType = esriGIFCompression.esriGIFCompressionRLE;
                        break;

                    case 2:
                        (this.m_pExport as IExportGIF).CompressionType = esriGIFCompression.esriGIFCompressionLZW;
                        break;
                }
            }
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor transparentColor = (this.m_pExport as IExportGIF).TransparentColor;
                this.UpdateColorFromColorEdit(this.colorEdit1, transparentColor);
                (this.m_pExport as IExportGIF).TransparentColor = transparentColor;
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
                switch ((this.m_pExport as IExportGIF).CompressionType)
                {
                    case esriGIFCompression.esriGIFCompressionNone:
                        this.cboImageCompression.SelectedIndex = 0;
                        break;

                    case esriGIFCompression.esriGIFCompressionRLE:
                        this.cboImageCompression.SelectedIndex = 1;
                        break;

                    case esriGIFCompression.esriGIFCompressionLZW:
                        this.cboImageCompression.SelectedIndex = 2;
                        break;
                }
                this.SetColorEdit(this.colorEdit1, (this.m_pExport as IExportGIF).TransparentColor);
                this.m_CanDo = true;
            }
        }

        private void GetRGB(uint rgb, out int r, out int g, out int b)
        {
            uint num = rgb & 0xff0000;
            b = (int) (num >> 0x10);
            num = rgb & 0xff00;
            g = (int) (num >> 8);
            num = rgb & 0xff;
            r = (int) num;
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.cboImageCompression = new ComboBoxEdit();
            this.colorEdit1 = new ColorEdit();
            this.label4 = new Label();
            this.cboImageCompression.Properties.BeginInit();
            this.colorEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(60, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "图像压缩:";
            this.cboImageCompression.EditValue = "无";
            this.cboImageCompression.Location = new Point(0x60, 0x10);
            this.cboImageCompression.Name = "cboImageCompression";
            this.cboImageCompression.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboImageCompression.Properties.Items.AddRange(new object[] { "无", "RLE", "LZW" });
            this.cboImageCompression.Size = new Size(0x60, 0x17);
            this.cboImageCompression.TabIndex = 10;
            this.cboImageCompression.SelectedIndexChanged += new EventHandler(this.cboImageCompression_SelectedIndexChanged);
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(0x60, 0x40);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(0x30, 0x17);
            this.colorEdit1.TabIndex = 13;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 0x40);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x30, 0x11);
            this.label4.TabIndex = 12;
            this.label4.Text = "透明色:";
            base.Controls.Add(this.colorEdit1);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.cboImageCompression);
            base.Controls.Add(this.label1);
            base.Name = "ExportMap2GIFPropertyPage";
            base.Size = new Size(0xd8, 0xb8);
            base.Load += new EventHandler(this.ExportMap2EMFPropertyPage_Load);
            this.cboImageCompression.Properties.EndInit();
            this.colorEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private int RGB(int r, int g, int b)
        {
            uint num = 0;
            num |= (uint) b;
            num = num << 8;
            num |= (uint) g;
            num = num << 8;
            num |= (uint) r;
            return (int) num;
        }

        private void SetColorEdit(ColorEdit colorEdit, IColor pColor)
        {
            if (pColor.NullColor)
            {
                colorEdit.Color = Color.Empty;
            }
            else
            {
                int num;
                int num2;
                int num3;
                int rGB = pColor.RGB;
                this.GetRGB((uint) rGB, out num, out num2, out num3);
                colorEdit.Color = Color.FromArgb(pColor.Transparency, num, num2, num3);
            }
        }

        private void UpdateColorFromColorEdit(ColorEdit colorEdit, IColor pColor)
        {
            pColor.NullColor = colorEdit.Color.IsEmpty;
            if (!colorEdit.Color.IsEmpty)
            {
                pColor.Transparency = colorEdit.Color.A;
                pColor.RGB = this.RGB(colorEdit.Color.R, colorEdit.Color.G, colorEdit.Color.B);
            }
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

