using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class frmScaleSet : Form
    {
        private Container container_0 = null;
        private IMap imap_0 = null;
        private IMapFrame imapFrame_0 = null;

        public frmScaleSet()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                IEnvelope envelope = new EnvelopeClass();
                this.imap_0.MapScale = Convert.ToDouble(this.txtScale.Text);
                double xMin = Convert.ToDouble(this.txtX.Text);
                double yMin = Convert.ToDouble(this.txtY.Text);
                for (int i = 0; i < (this.imapFrame_0 as IMapGrids).MapGridCount; i++)
                {
                    IMeasuredGrid grid = (this.imapFrame_0 as IMapGrids).get_MapGrid(i) as IMeasuredGrid;
                    if ((grid != null) && grid.FixedOrigin)
                    {
                        grid.XOrigin = xMin;
                        grid.YOrigin = yMin;
                    }
                }
                IEnvelope envelope2 = (this.imapFrame_0 as IElement).Geometry.Envelope;
                envelope.PutCoords(xMin, yMin, xMin + ((envelope2.Width * this.imap_0.MapScale) * 0.01), yMin + ((envelope2.Height * this.imap_0.MapScale) * 0.01));
                (this.imap_0 as IActiveView).Extent = envelope;
                (this.imap_0 as IActiveView).Refresh();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

 private void frmScaleSet_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

 private void method_0()
        {
            try
            {
                IEnvelope extent = (this.imap_0 as IActiveView).Extent;
                this.txtScale.Text = this.imap_0.MapScale.ToString("0.##");
                this.txtX.Text = extent.XMin.ToString("0.###");
                this.txtY.Text = extent.YMin.ToString("0.###");
            }
            catch
            {
            }
        }

        public IMapFrame MapFrame
        {
            set
            {
                this.imapFrame_0 = value;
                this.imap_0 = this.imapFrame_0.Map;
            }
        }
    }
}

