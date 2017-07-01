using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public partial class frmMapGridSheet : Form
    {
        private IMapGrid m_pMapGrid = null;

        public frmMapGridSheet(IMapGrid pMapGrid)
        {
            this.InitializeComponent();
            this.m_pMapGrid = pMapGrid;
        }

        private void frmMapGridSheet_Load(object sender, EventArgs e)
        {
            XtraTabPage page;
            IPropertyPage page2;
            if (this.m_pMapGrid is IProjectedGrid)
            {
                page = new XtraTabPage();
                page2 = new GridAxisPropertyPage();
                page.Text = page2.Title;
                page.Controls.Add(page2 as UserControl);
                this.xtraTabControl1.TabPages.Add(page);
                page = new XtraTabPage();
                page2 = new LabelFormatPropertyPage();
                page.Text = page2.Title;
                page.Controls.Add(page2 as UserControl);
                this.xtraTabControl1.TabPages.Add(page);
                page = new XtraTabPage();
                page2 = new TickSymbolPropertyPage();
                page.Text = page2.Title;
                page.Controls.Add(page2 as UserControl);
                this.xtraTabControl1.TabPages.Add(page);
                page = new XtraTabPage();
                page2 = new MeasureCoordinatePropertyPage();
                page.Text = page2.Title;
                page.Controls.Add(page2 as UserControl);
                this.xtraTabControl1.TabPages.Add(page);
                page = new XtraTabPage();
                page2 = new MeasuredGridPropertyPage();
                page.Text = page2.Title;
                page.Controls.Add(page2 as UserControl);
                this.xtraTabControl1.TabPages.Add(page);
            }
            else if (this.m_pMapGrid is IMgrsGrid)
            {
                page = new XtraTabPage();
                page2 = new MGRSPropertyPage();
                page.Text = page2.Title;
                page.Controls.Add(page2 as UserControl);
                this.xtraTabControl1.TabPages.Add(page);
                page = new XtraTabPage();
                page2 = new GridAxisPropertyPage();
                page.Text = page2.Title;
                page.Controls.Add(page2 as UserControl);
                this.xtraTabControl1.TabPages.Add(page);
                page = new XtraTabPage();
                page2 = new LabelFormatPropertyPage();
                page.Text = page2.Title;
                page.Controls.Add(page2 as UserControl);
                this.xtraTabControl1.TabPages.Add(page);
                page = new XtraTabPage();
                page2 = new TickSymbolPropertyPage();
                page.Text = page2.Title;
                page.Controls.Add(page2 as UserControl);
                this.xtraTabControl1.TabPages.Add(page);
            }
            else if (this.m_pMapGrid is IGraticule)
            {
                page = new XtraTabPage();
                page2 = new GridAxisPropertyPage();
                page.Text = page2.Title;
                page.Controls.Add(page2 as UserControl);
                this.xtraTabControl1.TabPages.Add(page);
                page = new XtraTabPage();
                page2 = new GridInteriorLabelsPropertyPage();
                page.Text = page2.Title;
                page.Controls.Add(page2 as UserControl);
                this.xtraTabControl1.TabPages.Add(page);
                page = new XtraTabPage();
                page2 = new LabelFormatPropertyPage();
                page.Text = page2.Title;
                page.Controls.Add(page2 as UserControl);
                this.xtraTabControl1.TabPages.Add(page);
                page = new XtraTabPage();
                page2 = new TickSymbolPropertyPage();
                page.Text = page2.Title;
                page.Controls.Add(page2 as UserControl);
                this.xtraTabControl1.TabPages.Add(page);
                page = new XtraTabPage();
                page2 = new GridHatchPropertyPage();
                page.Text = page2.Title;
                page.Controls.Add(page2 as UserControl);
                this.xtraTabControl1.TabPages.Add(page);
                page = new XtraTabPage();
                page2 = new MeasuredGridPropertyPage();
                page.Text = page2.Title;
                page.Controls.Add(page2 as UserControl);
                this.xtraTabControl1.TabPages.Add(page);
            }
            else if (this.m_pMapGrid is ICustomOverlayGrid)
            {
                page = new XtraTabPage();
                page2 = new CustomOverlayGridPropertyPage();
                page.Text = page2.Title;
                page.Controls.Add(page2 as UserControl);
                this.xtraTabControl1.TabPages.Add(page);
                page = new XtraTabPage();
                page2 = new GridAxisPropertyPage();
                page.Text = page2.Title;
                page.Controls.Add(page2 as UserControl);
                this.xtraTabControl1.TabPages.Add(page);
                page = new XtraTabPage();
                page2 = new LabelFormatPropertyPage();
                page.Text = page2.Title;
                page.Controls.Add(page2 as UserControl);
                this.xtraTabControl1.TabPages.Add(page);
                page = new XtraTabPage();
                page2 = new TickSymbolPropertyPage();
                page.Text = page2.Title;
                page.Controls.Add(page2 as UserControl);
                this.xtraTabControl1.TabPages.Add(page);
            }
            else if (this.m_pMapGrid is IIndexGrid)
            {
                page = new XtraTabPage();
                page2 = new GridAxisPropertyPage();
                page.Text = page2.Title;
                page.Controls.Add(page2 as UserControl);
                this.xtraTabControl1.TabPages.Add(page);
                page = new XtraTabPage();
                page2 = new IndexGridProperyPage();
                page.Text = page2.Title;
                page.Controls.Add(page2 as UserControl);
                this.xtraTabControl1.TabPages.Add(page);
                page = new XtraTabPage();
                page2 = new LabelFormatPropertyPage();
                page.Text = page2.Title;
                page.Controls.Add(page2 as UserControl);
                this.xtraTabControl1.TabPages.Add(page);
                page = new XtraTabPage();
                page2 = new TickSymbolPropertyPage();
                page.Text = page2.Title;
                page.Controls.Add(page2 as UserControl);
                this.xtraTabControl1.TabPages.Add(page);
            }
        }
    }
}