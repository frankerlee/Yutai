using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Output;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    public partial class frmExportMap : Form
    {
        private IActiveView m_pActiveView = null;
        private IExport m_pExport = null;
        private ILayer m_pLayer = null;

        public frmExportMap()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.btnOK.Enabled = false;
            this.btnCancel.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            this.Export();
            Cursor.Current = Cursors.Default;
            this.btnOK.Enabled = true;
            this.btnCancel.Enabled = true;
        }

        private void btnSelectOutLocation_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog {
                Filter = "EMF(*.emf)|*.emf|EPS(*.eps)|*.eps|AI(*.ai)|*.ai|PDF(*.pdf)|*.pdf|SVG(*.svg)|*.svg|BMP(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg|PNG(*.png)|*.png|TIFF(*.tif)|*.tif|GIF(*.gif)|*.gif"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = dialog.FileName;
                this.m_pExport = this.CreateExport(System.IO.Path.GetExtension(dialog.FileName).ToLower());
                if (this.m_pExport != null)
                {
                    this.m_pExport.ExportFileName = dialog.FileName;
                    try
                    {
                        this.m_pExport.Resolution = 300.0;
                        if (this.m_pExport is ISettingsInRegistry)
                        {
                            (this.m_pExport as ISettingsInRegistry).RestoreDefault();
                        }
                    }
                    catch
                    {
                    }
                    this.btnOK.Enabled = true;
                }
                else
                {
                    this.btnOK.Enabled = false;
                }
                this.SetTabControl(this.m_pExport);
            }
        }

        private IExport CreateExport(string ext)
        {
            switch (ext)
            {
                case ".emf":
                    return new ExportEMFClass();

                case ".eps":
                    return new ExportPSClass();

                case ".ai":
                    return new ExportAIClass();

                case ".pdf":
                    return new ExportPDFClass();

                case ".svg":
                    return new ExportSVGClass();

                case ".bmp":
                    return new ExportBMPClass();

                case ".jpg":
                    return new ExportJPEGClass();

                case ".png":
                    return new ExportPNGClass();

                case ".tif":
                    return new ExportTIFFClass();

                case ".gif":
                    return new ExportGIFClass();
            }
            return null;
        }

 public void Export()
        {
            if (this.m_pLayer == null)
            {
                this.Export(this.m_pActiveView);
            }
            else
            {
                int num;
                IList list = new ArrayList();
                IEnvelope extent = this.m_pActiveView.Extent;
                IMap pActiveView = this.m_pActiveView as IMap;
                for (num = 0; num < pActiveView.LayerCount; num++)
                {
                    ILayer layer = pActiveView.get_Layer(num);
                    if (layer.Visible)
                    {
                        list.Add(true);
                        layer.Visible = false;
                    }
                    else
                    {
                        list.Add(false);
                    }
                }
                this.m_pLayer.Visible = true;
                this.m_pActiveView.Extent = this.m_pLayer.AreaOfInterest;
                this.Export(this.m_pActiveView);
                this.m_pActiveView.Extent = extent;
                for (num = 0; num < pActiveView.LayerCount; num++)
                {
                    pActiveView.get_Layer(num).Visible = (bool) list[num];
                }
            }
        }

        private void Export(IActiveView pActiveView)
        {
            try
            {
                tagRECT grect;
                IExport pExport = this.m_pExport;
                int num2 = 96;
                int resolution = (int) pExport.Resolution;
                grect.left = 0;
                grect.top = 0;
                grect.right = pActiveView.ExportFrame.right * (resolution / num2);
                grect.bottom = pActiveView.ExportFrame.bottom * (resolution / num2);
                IEnvelope envelope = new EnvelopeClass();
                envelope.PutCoords((double) grect.left, (double) grect.top, (double) grect.right, (double) grect.bottom);
                pExport.PixelBounds = envelope;
                int hDC = pExport.StartExporting();
                pActiveView.Output(hDC, (int) pExport.Resolution, ref grect, null, null);
                try
                {
                    pExport.FinishExporting();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
                pExport.Cleanup();
            }
            catch
            {
            }
        }

 private void SetTabControl(IExport pExport)
        {
            this.tabControl1.TabPages.Clear();
            if (pExport == null)
            {
                this.tabControl1.Enabled = false;
            }
            else
            {
                TabPage page;
                UserControl control;
                this.tabControl1.Enabled = true;
                if (pExport is IExportEMF)
                {
                    control = new ExportMapGeneralPropertyPage();
                    (control as ExportMapGeneralPropertyPage).Export = pExport;
                    page = new TabPage("常规");
                    page.Controls.Add(control);
                    this.tabControl1.TabPages.Add(page);
                    control = new ExportMap2EMFPropertyPage();
                    (control as ExportMap2EMFPropertyPage).Export = pExport;
                    page = new TabPage("格式");
                    page.Controls.Add(control);
                    this.tabControl1.TabPages.Add(page);
                }
                else if (pExport is IExportAI)
                {
                    control = new ExportMapGeneralPropertyPage();
                    (control as ExportMapGeneralPropertyPage).Export = pExport;
                    page = new TabPage("常规");
                    page.Controls.Add(control);
                    this.tabControl1.TabPages.Add(page);
                }
                else if (pExport is IExportBMP)
                {
                    control = new ExportMapGeneralPropertyPage();
                    (control as ExportMapGeneralPropertyPage).Export = pExport;
                    page = new TabPage("常规");
                    page.Controls.Add(control);
                    this.tabControl1.TabPages.Add(page);
                    control = new ExportMapImagePropertyPage();
                    (control as ExportMapImagePropertyPage).Export = pExport;
                    page = new TabPage("图像设置");
                    page.Controls.Add(control);
                    this.tabControl1.TabPages.Add(page);
                }
                else if (pExport is IExportGIF)
                {
                    control = new ExportMapGeneralPropertyPage();
                    (control as ExportMapGeneralPropertyPage).Export = pExport;
                    page = new TabPage("常规");
                    page.Controls.Add(control);
                    this.tabControl1.TabPages.Add(page);
                    control = new ExportMap2GIFPropertyPage();
                    (control as ExportMap2GIFPropertyPage).Export = pExport;
                    page = new TabPage("格式");
                    page.Controls.Add(control);
                    this.tabControl1.TabPages.Add(page);
                    control = new ExportMapImagePropertyPage();
                    (control as ExportMapImagePropertyPage).Export = pExport;
                    page = new TabPage("图像设置");
                    page.Controls.Add(control);
                    this.tabControl1.TabPages.Add(page);
                }
                else if (pExport is IExportJPEG)
                {
                    control = new ExportMapGeneralPropertyPage();
                    (control as ExportMapGeneralPropertyPage).Export = pExport;
                    page = new TabPage("常规");
                    page.Controls.Add(control);
                    this.tabControl1.TabPages.Add(page);
                    control = new ExportMapImagePropertyPage();
                    (control as ExportMapImagePropertyPage).Export = pExport;
                    page = new TabPage("图像设置");
                    page.Controls.Add(control);
                    this.tabControl1.TabPages.Add(page);
                }
                else if (pExport is IExportPDF)
                {
                    control = new ExportMapGeneralPropertyPage();
                    (control as ExportMapGeneralPropertyPage).Export = pExport;
                    page = new TabPage("常规");
                    page.Controls.Add(control);
                    this.tabControl1.TabPages.Add(page);
                    control = new ExportMap2PDFPropertyPage();
                    (control as ExportMap2PDFPropertyPage).Export = pExport;
                    page = new TabPage("格式");
                    page.Controls.Add(control);
                    this.tabControl1.TabPages.Add(page);
                }
                else if (pExport is IExportPNG)
                {
                    control = new ExportMapGeneralPropertyPage();
                    (control as ExportMapGeneralPropertyPage).Export = pExport;
                    page = new TabPage("常规");
                    page.Controls.Add(control);
                    this.tabControl1.TabPages.Add(page);
                    control = new ExportMapImagePropertyPage();
                    (control as ExportMapImagePropertyPage).Export = pExport;
                    page = new TabPage("图像设置");
                    page.Controls.Add(control);
                    this.tabControl1.TabPages.Add(page);
                }
                else if (pExport is IExportPS)
                {
                    control = new ExportMapGeneralPropertyPage();
                    (control as ExportMapGeneralPropertyPage).Export = pExport;
                    page = new TabPage("常规");
                    page.Controls.Add(control);
                    this.tabControl1.TabPages.Add(page);
                    control = new ExportMap2EPSPropertyPage();
                    (control as ExportMap2EPSPropertyPage).Export = pExport;
                    page = new TabPage("格式");
                    page.Controls.Add(control);
                    this.tabControl1.TabPages.Add(page);
                }
                else if (pExport is IExportTIFF)
                {
                    control = new ExportMapGeneralPropertyPage();
                    (control as ExportMapGeneralPropertyPage).Export = pExport;
                    page = new TabPage("常规");
                    page.Controls.Add(control);
                    this.tabControl1.TabPages.Add(page);
                    control = new ExportMapImagePropertyPage();
                    (control as ExportMapImagePropertyPage).Export = pExport;
                    page = new TabPage("图像设置");
                    page.Controls.Add(control);
                    this.tabControl1.TabPages.Add(page);
                }
                else if (pExport is IExportSVG)
                {
                    control = new ExportMapGeneralPropertyPage();
                    (control as ExportMapGeneralPropertyPage).Export = pExport;
                    page = new TabPage("常规");
                    page.Controls.Add(control);
                    this.tabControl1.TabPages.Add(page);
                    control = new ExportMap2SVGPropertyPage();
                    (control as ExportMap2SVGPropertyPage).Export = pExport;
                    page = new TabPage("格式");
                    page.Controls.Add(control);
                    this.tabControl1.TabPages.Add(page);
                }
            }
        }

        public IActiveView ActiveView
        {
            set
            {
                this.m_pActiveView = value;
            }
        }

        public ILayer Layer
        {
            set
            {
                this.m_pLayer = value;
            }
        }
    }
}

