using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.UI
{
    public class RepresentationruleListBox : ListBox
    {
        private Container container_0 = null;
        private esriGeometryType esriGeometryType_0 = esriGeometryType.esriGeometryPolygon;
        private IRepresentationRules irepresentationRules_0 = null;

        public RepresentationruleListBox()
        {
            this.method_0();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        protected void DrawSymbol(int int_0, Rectangle rectangle_0, object object_0)
        {
            tagRECT grect;
            IDisplayTransformation transformation = new DisplayTransformationClass();
            IEnvelope envelope = new EnvelopeClass();
            envelope.PutCoords((double) rectangle_0.Left, (double) rectangle_0.Top, (double) rectangle_0.Right,
                (double) rectangle_0.Bottom);
            grect.left = rectangle_0.Left;
            grect.right = rectangle_0.Right;
            grect.bottom = rectangle_0.Bottom;
            grect.top = rectangle_0.Top;
            transformation.set_DeviceFrame(ref grect);
            transformation.Bounds = envelope;
            ISymbol symbol = object_0 as ISymbol;
            if (symbol != null)
            {
                symbol.SetupDC(int_0, transformation);
                if (symbol is IMarkerSymbol)
                {
                    this.method_3((IMarkerSymbol) symbol, rectangle_0);
                }
                else if (symbol is ILineSymbol)
                {
                    this.method_4((ILineSymbol) symbol, rectangle_0);
                }
                else if (symbol is IFillSymbol)
                {
                    this.method_5((IFillSymbol) symbol, rectangle_0);
                }
                symbol.ResetDC();
            }
        }

        public void Init(IRepresentationRules irepresentationRules_1)
        {
            int num;
            IRepresentationRule rule;
            base.Items.Clear();
            this.irepresentationRules_0 = irepresentationRules_1;
            this.irepresentationRules_0.Reset();
            this.irepresentationRules_0.Next(out num, out rule);
            while (rule != null)
            {
                string str = this.irepresentationRules_0.get_Name(num);
                base.Items.Add(new RepresentationRuleWrap(num, str, rule));
                this.irepresentationRules_0.Next(out num, out rule);
            }
        }

        private void method_0()
        {
            this.DrawMode = DrawMode.OwnerDrawVariable;
            base.MouseDown += new MouseEventHandler(this.RepresentationruleListBox_MouseDown);
            base.MeasureItem += new MeasureItemEventHandler(this.RepresentationruleListBox_MeasureItem);
            base.DrawItem += new DrawItemEventHandler(this.RepresentationruleListBox_DrawItem);
        }

        private IGeometry method_1(tagRECT tagRECT_0)
        {
            object before = Missing.Value;
            IGeometry geometry = new PolygonClass();
            IPoint inPoint = new PointClass();
            inPoint.PutCoords((double) tagRECT_0.left, (double) tagRECT_0.bottom);
            (geometry as IPointCollection).AddPoint(inPoint, ref before, ref before);
            inPoint = new PointClass();
            inPoint.PutCoords((double) tagRECT_0.left, (double) tagRECT_0.top);
            (geometry as IPointCollection).AddPoint(inPoint, ref before, ref before);
            inPoint = new PointClass();
            inPoint.PutCoords((double) tagRECT_0.right, (double) tagRECT_0.top);
            (geometry as IPointCollection).AddPoint(inPoint, ref before, ref before);
            inPoint = new PointClass();
            inPoint.PutCoords((double) tagRECT_0.right, (double) tagRECT_0.bottom);
            (geometry as IPointCollection).AddPoint(inPoint, ref before, ref before);
            inPoint = new PointClass();
            inPoint.PutCoords((double) tagRECT_0.left, (double) tagRECT_0.bottom);
            (geometry as IPointCollection).AddPoint(inPoint, ref before, ref before);
            return geometry;
        }

        private IGeometry method_2(tagRECT tagRECT_0)
        {
            IPoint inPoint = new PointClass();
            object before = Missing.Value;
            IGeometry geometry = new PolylineClass();
            inPoint.PutCoords((double) (tagRECT_0.left + 5), (double) ((tagRECT_0.top + tagRECT_0.bottom)/2));
            (geometry as IPointCollection).AddPoint(inPoint, ref before, ref before);
            inPoint = new PointClass();
            inPoint.PutCoords((double) (tagRECT_0.left + 5), (double) ((tagRECT_0.top + tagRECT_0.bottom)/2));
            (geometry as IPointCollection).AddPoint(inPoint, ref before, ref before);
            return geometry;
        }

        private void method_3(IMarkerSymbol imarkerSymbol_0, Rectangle rectangle_0)
        {
            if (!(imarkerSymbol_0 is IPictureMarkerSymbol) || (((IPictureMarkerSymbol) imarkerSymbol_0).Picture != null))
            {
                if (imarkerSymbol_0 is IMarker3DSymbol)
                {
                    try
                    {
                        if ((imarkerSymbol_0 as IMarker3DSymbol).MaterialCount == 0)
                        {
                            return;
                        }
                    }
                    catch
                    {
                        return;
                    }
                }
                IPoint geometry = new PointClass
                {
                    X = (rectangle_0.Left + rectangle_0.Right)/2,
                    Y = (rectangle_0.Bottom + rectangle_0.Top)/2
                };
                ((ISymbol) imarkerSymbol_0).Draw(geometry);
            }
        }

        private void method_4(ILineSymbol ilineSymbol_0, Rectangle rectangle_0)
        {
            if (ilineSymbol_0 is IPictureLineSymbol)
            {
                if (((IPictureLineSymbol) ilineSymbol_0).Picture == null)
                {
                    return;
                }
            }
            else if ((ilineSymbol_0 is IMarkerLineSymbol) || (ilineSymbol_0 is IHashLineSymbol))
            {
                ITemplate template = ((ILineProperties) ilineSymbol_0).Template;
                if (template != null)
                {
                    bool flag = false;
                    for (int i = 0; i < template.PatternElementCount; i++)
                    {
                        double num2;
                        double num3;
                        template.GetPatternElement(i, out num2, out num3);
                        if ((num2 + num3) > 0.0)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        return;
                    }
                }
            }
            object before = Missing.Value;
            IPointCollection points = new PolylineClass();
            IPoint inPoint = new PointClass();
            inPoint.PutCoords((double) (rectangle_0.Left + 3), (double) ((rectangle_0.Bottom + rectangle_0.Top)/2));
            points.AddPoint(inPoint, ref before, ref before);
            inPoint.PutCoords((double) (rectangle_0.Right - 3), (double) ((rectangle_0.Bottom + rectangle_0.Top)/2));
            points.AddPoint(inPoint, ref before, ref before);
            ((ISymbol) ilineSymbol_0).Draw((IGeometry) points);
        }

        private void method_5(IFillSymbol ifillSymbol_0, Rectangle rectangle_0)
        {
            if (!(ifillSymbol_0 is IPictureFillSymbol) || (((IPictureFillSymbol) ifillSymbol_0).Picture != null))
            {
                object before = Missing.Value;
                IPoint inPoint = new PointClass();
                IPointCollection points = new PolygonClass();
                inPoint.PutCoords((double) (rectangle_0.Left + 3), (double) (rectangle_0.Top + 3));
                points.AddPoint(inPoint, ref before, ref before);
                inPoint.PutCoords((double) (rectangle_0.Right - 3), (double) (rectangle_0.Top + 3));
                points.AddPoint(inPoint, ref before, ref before);
                inPoint.PutCoords((double) (rectangle_0.Right - 3), (double) (rectangle_0.Bottom - 3));
                points.AddPoint(inPoint, ref before, ref before);
                inPoint.PutCoords((double) (rectangle_0.Left + 3), (double) (rectangle_0.Bottom - 3));
                points.AddPoint(inPoint, ref before, ref before);
                inPoint.PutCoords((double) (rectangle_0.Left + 3), (double) (rectangle_0.Top + 3));
                points.AddPoint(inPoint, ref before, ref before);
                ((ISymbol) ifillSymbol_0).Draw((IGeometry) points);
            }
        }

        private void RepresentationruleListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (base.Items.Count != 0)
            {
                RepresentationRuleWrap wrap = (RepresentationRuleWrap) base.Items[e.Index];
                e.DrawBackground();
                string s = "(" + wrap.RuleID.ToString() + ") " + wrap.Name;
                Brush brush = new SolidBrush(e.ForeColor);
                e.Graphics.DrawString(s, this.Font, brush, (float) e.Bounds.X, (float) e.Bounds.Y);
                brush.Dispose();
                IntPtr hdc = e.Graphics.GetHdc();
                IOutputContext context = new OutputContextClass();
                IPoint currentScreenCenter = new PointClass();
                currentScreenCenter.PutCoords((double) (e.Bounds.X + (e.Bounds.Width/2)),
                    (double) ((e.Bounds.Y + (e.Bounds.Height/2)) + 10));
                tagRECT deviceRect = new tagRECT
                {
                    left = e.Bounds.Left + 10,
                    right = e.Bounds.Right - 10,
                    top = e.Bounds.Top + 10,
                    bottom = e.Bounds.Bottom - 5
                };
                context.Init(1.0, 1.5, 96.0, 0.0, currentScreenCenter, ref deviceRect, hdc.ToInt32());
                IGeometry geometry = null;
                if (this.esriGeometryType_0 == esriGeometryType.esriGeometryPolygon)
                {
                    geometry = this.method_1(deviceRect);
                }
                else if (this.esriGeometryType_0 == esriGeometryType.esriGeometryPolyline)
                {
                    geometry = this.method_2(deviceRect);
                }
                else
                {
                    geometry = new PointClass();
                    (geometry as IPoint).PutCoords((double) ((deviceRect.left + deviceRect.right)/2),
                        (double) ((deviceRect.top + deviceRect.bottom)/2));
                }
                wrap.RepresentationRule.Draw(-1, context, geometry, geometry.Envelope);
                e.Graphics.ReleaseHdc(hdc);
            }
        }

        private void RepresentationruleListBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 60;
        }

        private void RepresentationruleListBox_MouseDown(object sender, MouseEventArgs e)
        {
        }

        public esriGeometryType GeometryType
        {
            set { this.esriGeometryType_0 = value; }
        }

        internal class RepresentationRuleWrap
        {
            private int int_0;
            private IRepresentationRule irepresentationRule_0;
            private string string_0;

            internal RepresentationRuleWrap(int int_1, string string_1, IRepresentationRule irepresentationRule_1)
            {
                this.int_0 = int_1;
                this.string_0 = string_1;
                this.irepresentationRule_0 = irepresentationRule_1;
            }

            public string Name
            {
                get { return this.string_0; }
            }

            public IRepresentationRule RepresentationRule
            {
                get { return this.irepresentationRule_0; }
            }

            public int RuleID
            {
                get { return this.int_0; }
            }
        }
    }
}