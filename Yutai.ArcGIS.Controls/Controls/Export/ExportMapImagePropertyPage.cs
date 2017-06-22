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
    internal partial class ExportMapImagePropertyPage : UserControl
    {
        private bool m_CanDo = false;
        private IExport m_pExport = null;

        public ExportMapImagePropertyPage()
        {
            this.InitializeComponent();
        }

        private void cboImageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                switch (this.cboImageType.SelectedIndex)
                {
                    case 0:
                        (this.m_pExport as IExportImage).ImageType = esriExportImageType.esriExportImageTypeBiLevelMask;
                        break;

                    case 1:
                        (this.m_pExport as IExportImage).ImageType = esriExportImageType.esriExportImageTypeBiLevelThreshold;
                        break;

                    case 2:
                        (this.m_pExport as IExportImage).ImageType = esriExportImageType.esriExportImageTypeGrayscale;
                        break;

                    case 3:
                        (this.m_pExport as IExportImage).ImageType = esriExportImageType.esriExportImageTypeIndexed;
                        break;

                    case 4:
                        (this.m_pExport as IExportImage).ImageType = esriExportImageType.esriExportImageTypeTrueColor;
                        break;
                }
            }
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor backgroundColor = (this.m_pExport as IExportImage).BackgroundColor;
                this.UpdateColorFromColorEdit(this.colorEdit1, backgroundColor);
                (this.m_pExport as IExportImage).BackgroundColor = backgroundColor;
            }
        }

 private void ExportMapGeneralPropertyPage_Load(object sender, EventArgs e)
        {
            this.txtHeight.Value = (this.m_pExport as IExportImage).Height;
            this.txtWidth.Value = (this.m_pExport as IExportImage).Width;
            switch ((this.m_pExport as IExportImage).ImageType)
            {
                case esriExportImageType.esriExportImageTypeBiLevelMask:
                    this.cboImageType.SelectedIndex = 0;
                    break;

                case esriExportImageType.esriExportImageTypeBiLevelThreshold:
                    this.cboImageType.SelectedIndex = 1;
                    break;

                case esriExportImageType.esriExportImageTypeGrayscale:
                    this.cboImageType.SelectedIndex = 2;
                    break;

                case esriExportImageType.esriExportImageTypeIndexed:
                    this.cboImageType.SelectedIndex = 3;
                    break;

                case esriExportImageType.esriExportImageTypeTrueColor:
                    this.cboImageType.SelectedIndex = 4;
                    break;
            }
            this.SetColorEdit(this.colorEdit1, (this.m_pExport as IExportImage).BackgroundColor);
            this.m_CanDo = true;
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

        private void txtResampleRatio_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                try
                {
                    double num = (double) this.txtWidth.Value;
                    if ((num > 0.0) && (num < 6.0))
                    {
                        (this.m_pExport as IExportImage).Width = (int) num;
                    }
                }
                catch
                {
                }
            }
        }

        private void txtResolution_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                try
                {
                    double num = (double) this.txtHeight.Value;
                    if (num > 0.0)
                    {
                        (this.m_pExport as IExportImage).Height = (int) num;
                    }
                }
                catch
                {
                }
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

