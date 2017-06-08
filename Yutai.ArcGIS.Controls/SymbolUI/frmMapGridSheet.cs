using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public class frmMapGridSheet : Form
    {
        private Container components = null;
        private IMapGrid m_pMapGrid = null;
        private Panel panel1;
        private XtraTabControl xtraTabControl1;

        public frmMapGridSheet(IMapGrid pMapGrid)
        {
            this.InitializeComponent();
            this.m_pMapGrid = pMapGrid;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMapGridSheet));
            this.panel1 = new Panel();
            this.xtraTabControl1 = new XtraTabControl();
            this.xtraTabControl1.BeginInit();
            base.SuspendLayout();
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x105);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(400, 0x30);
            this.panel1.TabIndex = 0;
            this.xtraTabControl1.Dock = DockStyle.Fill;
            this.xtraTabControl1.Location = new Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.Size = new Size(400, 0x105);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.Text = "xtraTabControl1";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(400, 0x135);
            base.Controls.Add(this.xtraTabControl1);
            base.Controls.Add(this.panel1);
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.Name = "frmMapGridSheet";
            this.Text = "参考系";
            base.Load += new EventHandler(this.frmMapGridSheet_Load);
            this.xtraTabControl1.EndInit();
            base.ResumeLayout(false);
        }
    }
}

