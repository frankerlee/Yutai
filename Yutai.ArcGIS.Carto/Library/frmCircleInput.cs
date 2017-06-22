using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.Library
{
    public partial class frmCircleInput : Form
    {
        private IContainer icontainer_0 = null;
        private IGeometry igeometry_0 = null;
        private IMap imap_0 = null;

        public frmCircleInput()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            double num = double.Parse(this.txtXCoor.Text);
            double num2 = double.Parse(this.txtYCoor.Text);
            double radius = double.Parse(this.txtRadio.Text);
            if (this.imap_0.MapUnits != esriUnits.esriKilometers)
            {
                num *= 1000.0;
                num2 *= 1000.0;
                radius *= 1000.0;
            }
            IPoint centerPoint = new PointClass {
                X = num,
                Y = num2
            };
            ICircularArc arc = new CircularArcClass();
            (arc as IConstructCircularArc).ConstructCircle(centerPoint, radius, false);
            ISegmentCollection segments = new PolygonClass();
            object before = Missing.Value;
            segments.AddSegment(arc as ISegment, ref before, ref before);
            this.igeometry_0 = segments as IGeometry;
            base.DialogResult = DialogResult.OK;
        }

 private void frmCircleInput_Load(object sender, EventArgs e)
        {
        }

 private void method_0(object sender, EventArgs e)
        {
            double.Parse(this.txtXCoor.Text);
            double.Parse(this.txtYCoor.Text);
        }

        public IGeometry Geometry
        {
            get
            {
                return this.igeometry_0;
            }
            set
            {
                this.igeometry_0 = value;
            }
        }

        public IMap Map
        {
            set
            {
                this.imap_0 = value;
            }
        }
    }
}

