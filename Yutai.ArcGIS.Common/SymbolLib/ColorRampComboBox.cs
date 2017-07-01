using System.ComponentModel;
using System.Drawing;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.SymbolLib
{
    public class ColorRampComboBox : System.Windows.Forms.ComboBox
    {
        private Container container_0 = null;

        private IArray iarray_0 = new Array();

        public ColorRampComboBox()
        {
            this.method_0();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && this.container_0 != null)
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void method_0()
        {
            base.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            base.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            base.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.ColorRampComboBox_MeasureItem);
            base.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ColorRampComboBox_DrawItem);
        }

        private void ColorRampComboBox_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            if (this.iarray_0.Count != 0)
            {
                e.DrawBackground();
                System.IntPtr hdc = e.Graphics.GetHdc();
                this.DrawColorRamp(hdc.ToInt32(), e.Bounds, (IColorRamp) this.iarray_0.get_Element(e.Index));
                e.Graphics.ReleaseHdc(hdc);
            }
        }

        protected void DrawColorRamp(int int_0, Rectangle rectangle_0, IColorRamp icolorRamp_0)
        {
            IDisplayTransformation displayTransformation = new DisplayTransformation() as IDisplayTransformation;
            IEnvelope envelope = new Envelope() as IEnvelope;
            envelope.PutCoords((double) rectangle_0.Left, (double) rectangle_0.Top, (double) rectangle_0.Right,
                (double) rectangle_0.Bottom);
            tagRECT tagRECT;
            tagRECT.left = rectangle_0.Left;
            tagRECT.right = rectangle_0.Right;
            tagRECT.bottom = rectangle_0.Bottom;
            tagRECT.top = rectangle_0.Top;
            displayTransformation.set_DeviceFrame(ref tagRECT);
            displayTransformation.Bounds = envelope;
            IGradientFillSymbol gradientFillSymbol = new GradientFillSymbol();
            ILineSymbol outline = gradientFillSymbol.Outline;
            outline.Width = 0.0;
            gradientFillSymbol.Outline = outline;
            gradientFillSymbol.ColorRamp = icolorRamp_0;
            gradientFillSymbol.GradientAngle = 180.0;
            gradientFillSymbol.GradientPercentage = 1.0;
            gradientFillSymbol.IntervalCount = 100;
            gradientFillSymbol.Style = esriGradientFillStyle.esriGFSLinear;
            ISymbol symbol = (ISymbol) gradientFillSymbol;
            symbol.SetupDC(int_0, displayTransformation);
            object value = System.Reflection.Missing.Value;
            IPoint point = new ESRI.ArcGIS.Geometry.Point();
            IPointCollection pointCollection = new Polygon();
            point.PutCoords((double) (rectangle_0.Left + 3), (double) (rectangle_0.Top + 3));
            pointCollection.AddPoint(point, ref value, ref value);
            point.PutCoords((double) (rectangle_0.Right - 3), (double) (rectangle_0.Top + 3));
            pointCollection.AddPoint(point, ref value, ref value);
            point.PutCoords((double) (rectangle_0.Right - 3), (double) (rectangle_0.Bottom - 3));
            pointCollection.AddPoint(point, ref value, ref value);
            point.PutCoords((double) (rectangle_0.Left + 3), (double) (rectangle_0.Bottom - 3));
            pointCollection.AddPoint(point, ref value, ref value);
            point.PutCoords((double) (rectangle_0.Left + 3), (double) (rectangle_0.Top + 3));
            pointCollection.AddPoint(point, ref value, ref value);
            symbol.Draw((IGeometry) pointCollection);
            symbol.ResetDC();
        }

        private void ColorRampComboBox_MeasureItem(object sender, System.Windows.Forms.MeasureItemEventArgs e)
        {
            e.ItemHeight = 20;
        }

        public void Add(IStyleGalleryItem istyleGalleryItem_0)
        {
            this.iarray_0.Add(istyleGalleryItem_0.Item);
            base.Items.Add(istyleGalleryItem_0.Name);
        }

        public void Clear()
        {
            this.iarray_0.RemoveAll();
            base.Items.Clear();
        }

        public IColorRamp GetSelectColorRamp()
        {
            return (IColorRamp) this.iarray_0.get_Element(this.SelectedIndex);
        }
    }
}