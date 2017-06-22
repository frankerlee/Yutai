using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class PageSetupControl : UserControl
    {
        private Container container_0 = null;
        protected IMapFrame m_pMapFrame = null;
        protected IPageLayout m_pPageLayout = null;

        public PageSetupControl()
        {
            this.InitializeComponent();
        }

 public void Do()
        {
            try
            {
                double width = Convert.ToDouble(this.txtPageWidth.Text);
                double height = Convert.ToDouble(this.txtPageHeight.Text);
                this.m_pPageLayout.Page.PutCustomSize(width, height);
                double xMin = Convert.ToDouble(this.txtX.Text);
                double yMin = Convert.ToDouble(this.txtY.Text);
                width = Convert.ToDouble(this.txtWidth.Text);
                height = Convert.ToDouble(this.txtHeight.Text);
                IEnvelope envelope = new EnvelopeClass();
                envelope.PutCoords(xMin, yMin, xMin + width, yMin + height);
                (this.m_pMapFrame as IElement).Geometry = envelope;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

 private void method_0()
        {
            double num;
            double num2;
            this.m_pPageLayout.Page.QuerySize(out num, out num2);
            this.txtPageWidth.Text = num.ToString("0.###");
            this.txtPageHeight.Text = num2.ToString("0.###");
            IEnvelope outEnvelope = new EnvelopeClass();
            (this.m_pMapFrame as IElement).Geometry.QueryEnvelope(outEnvelope);
            this.txtX.Text = outEnvelope.XMin.ToString("0.###");
            this.txtY.Text = outEnvelope.YMin.ToString("0.###");
            this.txtWidth.Text = outEnvelope.Width.ToString("0.###");
            this.txtHeight.Text = outEnvelope.Height.ToString("0.###");
        }

        private void PageSetupControl_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        public IMapFrame MapFrame
        {
            set
            {
                this.m_pMapFrame = value;
            }
        }

        public IPageLayout PageLayout
        {
            set
            {
                this.m_pPageLayout = value;
            }
        }
    }
}

