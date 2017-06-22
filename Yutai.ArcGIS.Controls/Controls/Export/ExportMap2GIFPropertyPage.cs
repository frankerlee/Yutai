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
    internal partial class ExportMap2GIFPropertyPage : UserControl
    {
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
            uint num = rgb & 16711680;
            b = (int) (num >> 16);
            num = rgb & 65280;
            g = (int) (num >> 8);
            num = rgb & 255;
            r = (int) num;
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

