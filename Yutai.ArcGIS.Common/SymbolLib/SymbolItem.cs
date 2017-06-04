using System.ComponentModel;
using System.Drawing;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.SymbolLib
{
	public class SymbolItem : System.Windows.Forms.UserControl
	{
		private Container container_0 = null;

		private object object_0;

		private double double_0 = 1.0;

		private bool bool_0 = true;

		private bool bool_1 = false;

		[Browsable(false)]
		public object Symbol
		{
			get
			{
				return this.object_0;
			}
			set
			{
				this.object_0 = value;
				base.Invalidate();
			}
		}

		[DefaultValue(1.0), Description("缩放倍数")]
		public double ScaleRatio
		{
			get
			{
				return this.double_0;
			}
			set
			{
				this.double_0 = value;
			}
		}

		[DefaultValue(true), Description("是否绘制中心线")]
		public bool HasDrawLine
		{
			set
			{
				this.bool_0 = value;
			}
		}

		[DefaultValue(false), Description("是否绘折线")]
		public bool HasDrawPLine
		{
			get
			{
				return this.bool_1;
			}
			set
			{
				this.bool_1 = value;
			}
		}

		public SymbolItem()
		{
			this.InitializeComponent();
		}

		protected override void Dispose(bool bool_2)
		{
			if (bool_2 && this.container_0 != null)
			{
				this.container_0.Dispose();
			}
			base.Dispose(bool_2);
		}

		private void InitializeComponent()
		{
			this.BackColor = SystemColors.ControlLight;
			base.Name = "SymbolItem";
			base.Paint += new System.Windows.Forms.PaintEventHandler(this.SymbolItem_Paint);
		}

		private void SymbolItem_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if (this.object_0 != null)
			{
				if (this.bool_0 && this.object_0 is IMarkerSymbol)
				{
					System.Drawing.Point pt = new System.Drawing.Point(base.ClientSize.Width / 2, base.ClientRectangle.Top + 2);
					System.Drawing.Point pt2 = new System.Drawing.Point(base.ClientSize.Width / 2, base.ClientRectangle.Bottom - 2);
					e.Graphics.DrawLine(Pens.Green, pt, pt2);
					pt.X = base.ClientRectangle.Left + 2;
					pt.Y = base.ClientSize.Height / 2;
					pt2.X = base.ClientRectangle.Right - 2;
					pt2.Y = base.ClientSize.Height / 2;
					e.Graphics.DrawLine(Pens.Green, pt, pt2);
				}
				else if (this.object_0 is ITextSymbol)
				{
					System.Drawing.Point point = new System.Drawing.Point(base.ClientSize.Width / 2, base.ClientRectangle.Height + 2);
					e.Graphics.DrawEllipse(Pens.Black, point.X, point.Y, 2, 2);
				}
				System.IntPtr hdc = e.Graphics.GetHdc();
				this.DrawSymbol(hdc.ToInt32(), base.ClientRectangle, this.object_0);
				e.Graphics.ReleaseHdc(hdc);
			}
		}

		protected void DrawSymbol(int int_0, Rectangle rectangle_0, object object_1)
		{
			IDisplayTransformation displayTransformation = new DisplayTransformation() as IDisplayTransformation;
			IEnvelope envelope = new Envelope() as ESRI.ArcGIS.Geometry.IEnvelope;
			envelope.PutCoords((double)rectangle_0.Left, (double)rectangle_0.Top, (double)rectangle_0.Right, (double)rectangle_0.Bottom);
			tagRECT tagRECT;
			tagRECT.left = rectangle_0.Left;
			tagRECT.right = rectangle_0.Right;
			tagRECT.bottom = rectangle_0.Bottom;
			tagRECT.top = rectangle_0.Top;
			displayTransformation.set_DeviceFrame (ref tagRECT);
			displayTransformation.Bounds = envelope;
			if (this.double_0 < 1.0 && object_1 is ILineSymbol)
			{
				displayTransformation.Resolution = 48.0 / this.double_0;
			}
			else
			{
				displayTransformation.Resolution = 96.0;
			}
			displayTransformation.ReferenceScale = 1.0;
			displayTransformation.ScaleRatio = this.double_0;
			if (object_1 is IMarkerSymbol)
			{
				IStyleGalleryClass styleGalleryClass = new MarkerSymbolStyleGalleryClass();
				tagRECT tagRECT2 = default(tagRECT);
				tagRECT2.left = rectangle_0.Left;
				tagRECT2.right = rectangle_0.Right;
				tagRECT2.top = rectangle_0.Top;
				tagRECT2.bottom = rectangle_0.Bottom;
				styleGalleryClass.Preview(object_1, int_0, ref tagRECT2);
			}
			else if (object_1 is ILineSymbol)
			{
				IStyleGalleryClass styleGalleryClass = new LineSymbolStyleGalleryClass();
				tagRECT tagRECT2 = default(tagRECT);
				tagRECT2.left = rectangle_0.Left;
				tagRECT2.right = rectangle_0.Right;
				tagRECT2.top = rectangle_0.Top;
				tagRECT2.bottom = rectangle_0.Bottom;
				styleGalleryClass.Preview(object_1, int_0, ref tagRECT2);
			}
			else if (object_1 is IFillSymbol)
			{
				IStyleGalleryClass styleGalleryClass = new FillSymbolStyleGalleryClass();
				tagRECT tagRECT2 = default(tagRECT);
				tagRECT2.left = rectangle_0.Left;
				tagRECT2.right = rectangle_0.Right;
				tagRECT2.top = rectangle_0.Top;
				tagRECT2.bottom = rectangle_0.Bottom;
				styleGalleryClass.Preview(object_1, int_0, ref tagRECT2);
			}
			else
			{
				ISymbol symbol;
				if (object_1 is IColorRamp)
				{
					IGradientFillSymbol gradientFillSymbol = new GradientFillSymbol();
					ILineSymbol outline = gradientFillSymbol.Outline;
					outline.Width = 0.0;
					gradientFillSymbol.Outline = outline;
					gradientFillSymbol.ColorRamp = (IColorRamp)object_1;
					gradientFillSymbol.GradientAngle = 180.0;
					gradientFillSymbol.GradientPercentage = 1.0;
					gradientFillSymbol.IntervalCount = 100;
					gradientFillSymbol.Style = esriGradientFillStyle.esriGFSLinear;
					symbol = (ISymbol)gradientFillSymbol;
				}
				else if (object_1 is IColor)
				{
					symbol = (ISymbol)new ColorSymbol
					{
						Color = (IColor)object_1
					};
				}
				else if (object_1 is IAreaPatch)
				{
					symbol = new SimpleFillSymbol() as ISymbol;
					IRgbColor rgbColor = new RgbColor();
					rgbColor.Red = 227;
					rgbColor.Green = 236;
					rgbColor.Blue = 19;
					((IFillSymbol)symbol).Color = rgbColor;
				}
				else if (object_1 is ILinePatch)
				{
					symbol = new SimpleLineSymbol() as ISymbol;
				}
				else
				{
					if (object_1 is INorthArrow)
					{
						IStyleGalleryClass styleGalleryClass = new NorthArrowStyleGalleryClass();
						tagRECT tagRECT2 = default(tagRECT);
						tagRECT2.left = rectangle_0.Left;
						tagRECT2.right = rectangle_0.Right;
						tagRECT2.top = rectangle_0.Top;
						tagRECT2.bottom = rectangle_0.Bottom;
						styleGalleryClass.Preview(object_1, int_0, ref tagRECT2);
						return;
					}
					if (object_1 is ILegendItem)
					{
						IStyleGalleryClass styleGalleryClass = new LegendItemStyleGalleryClass();
						tagRECT tagRECT2 = default(tagRECT);
						tagRECT2.left = rectangle_0.Left;
						tagRECT2.right = rectangle_0.Right;
						tagRECT2.top = rectangle_0.Top;
						tagRECT2.bottom = rectangle_0.Bottom;
						styleGalleryClass.Preview(object_1, int_0, ref tagRECT2);
						return;
					}
					if (object_1 is ILabelStyle)
					{
						IStyleGalleryClass styleGalleryClass = new LabelStyleGalleryClass();
						tagRECT tagRECT2 = default(tagRECT);
						tagRECT2.left = rectangle_0.Left;
						tagRECT2.right = rectangle_0.Right;
						tagRECT2.top = rectangle_0.Top;
						tagRECT2.bottom = rectangle_0.Bottom;
						styleGalleryClass.Preview(object_1, int_0, ref tagRECT2);
						return;
					}
					if (object_1 is IMapSurround)
					{
						MapSurroundDraw mapSurroundDraw = new MapSurroundDraw(object_1 as IMapSurround);
						mapSurroundDraw.Draw(int_0, rectangle_0, 96.0, this.double_0);
						return;
					}
					if (object_1 is IBackground)
					{
						IDisplay display = new ScreenDisplay();
						display.StartDrawing(int_0, 0);
						display.DisplayTransformation = displayTransformation;
						IGeometry geometry = ((IBackground)object_1).GetGeometry(display, envelope);
						((IBackground)object_1).Draw(display, geometry);
						display.FinishDrawing();
						return;
					}
					if (object_1 is IShadow)
					{
						ShadowDraw shadowDraw = new ShadowDraw(object_1 as IShadow);
						shadowDraw.Draw(int_0, rectangle_0, 96.0, this.double_0);
						return;
					}
					if (object_1 is IRepresentationMarker)
					{
						RepresentationMarkerDraw representationMarkerDraw = new RepresentationMarkerDraw(object_1 as IRepresentationMarker);
						representationMarkerDraw.Draw(int_0, rectangle_0, 96.0, this.double_0);
						return;
					}
					if (object_1 is IRepresentationRuleItem)
					{
						try
						{
							RepresentationRuleDraw representationRuleDraw = new RepresentationRuleDraw(object_1);
							IRepresentationRule representationRule = (object_1 as IRepresentationRuleItem).RepresentationRule;
							IBasicSymbol arg_61E_0 = representationRule.get_Layer(0);
							representationRuleDraw.Draw(int_0, rectangle_0, 96.0, this.double_0);
							return;
						}
						catch
						{
							return;
						}
					}
					if (object_1 is IBorder)
					{
						IDisplay display = new ScreenDisplay();
						display.StartDrawing(int_0, 0);
						display.DisplayTransformation = displayTransformation;
						IPointCollection pointCollection = new Polyline();
						object value = System.Reflection.Missing.Value;
						IPoint point = new ESRI.ArcGIS.Geometry.Point();
						point.PutCoords((double)(rectangle_0.X + 4), (double)rectangle_0.Top);
						pointCollection.AddPoint(point, ref value, ref value);
						point.PutCoords((double)(rectangle_0.X + 4), (double)rectangle_0.Bottom);
						pointCollection.AddPoint(point, ref value, ref value);
						point.PutCoords((double)(rectangle_0.Right - 4), (double)rectangle_0.Bottom);
						pointCollection.AddPoint(point, ref value, ref value);
						IGeometry geometry = ((IBorder)object_1).GetGeometry(display, (IGeometry)pointCollection);
						((IBorder)object_1).Draw(display, geometry);
						display.FinishDrawing();
						return;
					}
					if (object_1 is IMapGrid)
					{
						IDisplay display = new ScreenDisplay();
						display.StartDrawing(int_0, 0);
						display.DisplayTransformation = displayTransformation;
						IPointCollection pointCollection = new Polyline();
						object value = System.Reflection.Missing.Value;
						IPoint point = new ESRI.ArcGIS.Geometry.Point();
						point.PutCoords((double)(rectangle_0.X + 4), (double)rectangle_0.Top);
						pointCollection.AddPoint(point, ref value, ref value);
						point.PutCoords((double)(rectangle_0.X + 4), (double)rectangle_0.Bottom);
						pointCollection.AddPoint(point, ref value, ref value);
						point.PutCoords((double)(rectangle_0.Right - 4), (double)rectangle_0.Bottom);
						pointCollection.AddPoint(point, ref value, ref value);
						IMapFrame mapFrame = new MapFrame() as IMapFrame;
						IMap map = new Map();
						mapFrame.Map = map;
						(map as IActiveView).Extent = (pointCollection as IGeometry).Envelope;
						(object_1 as IMapGrid).Draw(display, mapFrame);
						display.FinishDrawing();
						return;
					}
					return;
				}
				if (symbol is IPictureFillSymbol || symbol is IPictureLineSymbol)
				{
					symbol.SetupDC(int_0, displayTransformation);
				}
				else
				{
					symbol.SetupDC(int_0, displayTransformation);
				}
				if (symbol is IMarkerSymbol)
				{
					this.method_0((IMarkerSymbol)symbol, rectangle_0);
				}
				else if (symbol is ILineSymbol)
				{
					this.method_1((ILineSymbol)symbol, rectangle_0);
				}
				else if (symbol is IFillSymbol)
				{
					this.method_2((IFillSymbol)symbol, rectangle_0);
				}
				else if (symbol is ITextSymbol)
				{
					this.method_3((ITextSymbol)symbol, rectangle_0);
				}
				symbol.ResetDC();
			}
		}

		private void method_0(IMarkerSymbol imarkerSymbol_0, Rectangle rectangle_0)
		{
			IPoint point = new ESRI.ArcGIS.Geometry.Point();
			point.X = (double)((rectangle_0.Left + rectangle_0.Right) / 2);
			point.Y = (double)((rectangle_0.Bottom + rectangle_0.Top) / 2);
			((ISymbol)imarkerSymbol_0).Draw(point);
		}

		private void method_1(ILineSymbol ilineSymbol_0, Rectangle rectangle_0)
		{
			if (ilineSymbol_0 is IPictureLineSymbol)
			{
				if (((IPictureLineSymbol)ilineSymbol_0).Picture == null)
				{
					return;
				}
			}
			else if (ilineSymbol_0 is IMarkerLineSymbol || ilineSymbol_0 is IHashLineSymbol)
			{
				ITemplate template = ((ILineProperties)ilineSymbol_0).Template;
				if (template != null)
				{
					bool flag = false;
					int i = 0;
					while (i < template.PatternElementCount)
					{
						double num;
						double num2;
						template.GetPatternElement(i, out num, out num2);
						if (num + num2 <= 0.0)
						{
							i++;
						}
						else
						{
							flag = true;
							IL_89:
							if (flag)
							{
                                object value = System.Reflection.Missing.Value;
                                IPointCollection pointCollection = new Polyline();
                                IPoint point = new ESRI.ArcGIS.Geometry.Point();
                                if (this.bool_1)
                                {
                                    point.PutCoords((double)(rectangle_0.Left + 2), (double)(rectangle_0.Bottom + 2));
                                    pointCollection.AddPoint(point, ref value, ref value);
                                    point.PutCoords((double)((rectangle_0.Width - 4) / 3 + rectangle_0.Left + 2), (double)(rectangle_0.Top - 2));
                                    pointCollection.AddPoint(point, ref value, ref value);
                                    point.PutCoords((double)((rectangle_0.Width - 4) / 3 * 2 + rectangle_0.Left + 2), (double)(rectangle_0.Bottom + 2));
                                    pointCollection.AddPoint(point, ref value, ref value);
                                    point.PutCoords((double)(rectangle_0.Right - 2), (double)(rectangle_0.Top - 2));
                                    pointCollection.AddPoint(point, ref value, ref value);
                                }
                                else
                                {
                                    point.PutCoords((double)(rectangle_0.Left + 3), (double)((rectangle_0.Bottom + rectangle_0.Top) / 2));
                                    pointCollection.AddPoint(point, ref value, ref value);
                                    point.PutCoords((double)(rectangle_0.Right - 3), (double)((rectangle_0.Bottom + rectangle_0.Top) / 2));
                                    pointCollection.AddPoint(point, ref value, ref value);
                                }
            ((ISymbol)ilineSymbol_0).Draw((IGeometry)pointCollection);
                            }
							return;
						}
					}
					
				}
			}
			
		}

		private void method_2(IFillSymbol ifillSymbol_0, Rectangle rectangle_0)
		{
			object value = System.Reflection.Missing.Value;
			IPoint point = new ESRI.ArcGIS.Geometry.Point();
			IPointCollection pointCollection = new Polygon();
			point.PutCoords((double)(rectangle_0.Left + 3), (double)(rectangle_0.Top + 3));
			pointCollection.AddPoint(point, ref value, ref value);
			point.PutCoords((double)(rectangle_0.Right - 3), (double)(rectangle_0.Top + 3));
			pointCollection.AddPoint(point, ref value, ref value);
			point.PutCoords((double)(rectangle_0.Right - 3), (double)(rectangle_0.Bottom - 3));
			pointCollection.AddPoint(point, ref value, ref value);
			point.PutCoords((double)(rectangle_0.Left + 3), (double)(rectangle_0.Bottom - 3));
			pointCollection.AddPoint(point, ref value, ref value);
			point.PutCoords((double)(rectangle_0.Left + 3), (double)(rectangle_0.Top + 3));
			pointCollection.AddPoint(point, ref value, ref value);
			((ISymbol)ifillSymbol_0).Draw((IGeometry)pointCollection);
		}

		private void method_3(ITextSymbol itextSymbol_0, Rectangle rectangle_0)
		{
			IPoint point = new ESRI.ArcGIS.Geometry.Point();
			point.X = (double)((rectangle_0.Left + rectangle_0.Right) / 2);
			point.Y = (double)((rectangle_0.Bottom + rectangle_0.Top) / 2);
			if (itextSymbol_0 is ISimpleTextSymbol)
			{
				ISimpleTextSymbol simpleTextSymbol = (ISimpleTextSymbol)itextSymbol_0;
				string text = simpleTextSymbol.Text;
				bool clip = simpleTextSymbol.Clip;
				if (text.Length == 0)
				{
					simpleTextSymbol.Text = "AaBbYyZz";
				}
				simpleTextSymbol.Clip = true;
				((ISymbol)itextSymbol_0).Draw(point);
				simpleTextSymbol.Text = text;
				simpleTextSymbol.Clip = clip;
			}
			else
			{
				((ISymbol)itextSymbol_0).Draw(point);
			}
		}
	}
}
