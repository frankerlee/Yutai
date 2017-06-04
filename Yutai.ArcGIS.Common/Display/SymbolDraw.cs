using System.Drawing;
using System.Reflection;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Display
{
	public class SymbolDraw
	{
		private static IGeometry m_pGeometry;

		static SymbolDraw()
		{
			SymbolDraw.old_acctor_mc();
		}

		public SymbolDraw()
		{
		}

		public static void DrawPolygonXOR(IDisplay idisplay_0, IPolygon ipolygon_0, bool bool_0)
		{
			try
			{
				IFillSymbol simpleFillSymbolClass = new SimpleFillSymbol();
				ISymbol symbol = simpleFillSymbolClass as ISymbol;
				symbol.ROP2 = esriRasterOpCode.esriROPXOrPen;
				IRgbColor rgbColorClass = new RgbColor()
				{
					UseWindowsDithering = false,
					Red = 45,
					Green = 45,
					Blue = 45
				};
				simpleFillSymbolClass.Color = rgbColorClass;
				ILineSymbol outline = simpleFillSymbolClass.Outline;
				(outline as ISymbol).ROP2 = esriRasterOpCode.esriROPXOrPen;
				rgbColorClass.UseWindowsDithering = false;
				rgbColorClass.Red = 145;
				rgbColorClass.Green = 145;
				rgbColorClass.Blue = 145;
				outline.Color = rgbColorClass;
				outline.Width = 0.1;
				simpleFillSymbolClass.Outline = outline;
				idisplay_0.StartDrawing(0, -1);
				idisplay_0.SetSymbol(symbol);
				if (ipolygon_0 == null)
				{
					if (SymbolDraw.m_pGeometry != null)
					{
						idisplay_0.DrawPolygon(SymbolDraw.m_pGeometry as IPolygon);
					}
					if (!bool_0)
					{
						SymbolDraw.m_pGeometry = null;
					}
				}
				else
				{
					idisplay_0.DrawPolygon(ipolygon_0);
					SymbolDraw.m_pGeometry = ipolygon_0;
				}
				idisplay_0.FinishDrawing();
			}
			catch
			{
			}
		}

		public static void DrawRectXOR(IDisplay idisplay_0, IEnvelope ienvelope_0, IFillSymbol ifillSymbol_0)
		{
			try
			{
				ISymbol ifillSymbol0 = ifillSymbol_0 as ISymbol;
				ifillSymbol0.ROP2 = esriRasterOpCode.esriROPXOrPen;
				ILineSymbol outline = ifillSymbol_0.Outline;
				(outline as ISymbol).ROP2 = esriRasterOpCode.esriROPXOrPen;
				ifillSymbol_0.Outline = outline;
				idisplay_0.StartDrawing(0, -1);
				idisplay_0.SetSymbol(ifillSymbol0);
				idisplay_0.DrawRectangle(ienvelope_0);
				idisplay_0.FinishDrawing();
			}
			catch
			{
			}
		}

		public static void DrawSymbol(int int_0, Rectangle rectangle_0, object object_0, double double_0)
		{
			tagRECT left = new tagRECT();
			ISymbol object0;
			IDisplay screenDisplayClass;
			IGeometry geometry;
			bool flag;
			IDisplayTransformation displayTransformationClass = new DisplayTransformation() as IDisplayTransformation;
			IEnvelope envelopeClass = new Envelope() as IEnvelope;
			envelopeClass.PutCoords((double)rectangle_0.Left, (double)rectangle_0.Top, (double)rectangle_0.Right, (double)rectangle_0.Bottom);
			left.left = rectangle_0.Left;
			left.right = rectangle_0.Right;
			left.bottom = rectangle_0.Bottom;
			left.top = rectangle_0.Top;
			displayTransformationClass.set_DeviceFrame(left);
			displayTransformationClass.Bounds = envelopeClass;
			if ((double_0 >= 1 ? true : !(object_0 is ILineSymbol)))
			{
				displayTransformationClass.Resolution = 72;
			}
			else
			{
				displayTransformationClass.Resolution = 36 / double_0;
			}
			displayTransformationClass.ReferenceScale = 1;
			displayTransformationClass.ScaleRatio = double_0;
			if (object_0 is ISymbol)
			{
				object0 = (ISymbol)object_0;
			}
			else if (object_0 is IColorRamp)
			{
				IGradientFillSymbol gradientFillSymbolClass = new GradientFillSymbol();
				ILineSymbol outline = gradientFillSymbolClass.Outline;
				outline.Width = 0;
				gradientFillSymbolClass.Outline = outline;
				gradientFillSymbolClass.ColorRamp = (IColorRamp)object_0;
				gradientFillSymbolClass.GradientAngle = 180;
				gradientFillSymbolClass.GradientPercentage = 1;
				gradientFillSymbolClass.IntervalCount = 100;
				gradientFillSymbolClass.Style = esriGradientFillStyle.esriGFSLinear;
				object0 = (ISymbol)gradientFillSymbolClass;
			}
			else if (object_0 is IColor)
			{
				object0 = (ISymbol)(new ColorSymbol()
				{
					Color = (IColor)object_0
				});
			}
			else if (!(object_0 is IAreaPatch))
			{
				if (object_0 is ILinePatch)
				{
					goto Label1;
				}
				if (object_0 is INorthArrow)
				{
					screenDisplayClass = new ScreenDisplay();
					screenDisplayClass.StartDrawing(int_0, 0);
					screenDisplayClass.DisplayTransformation = displayTransformationClass;
					((IMapSurround)object_0).Draw(screenDisplayClass, null, envelopeClass);
					screenDisplayClass.FinishDrawing();
					return;
				}
				else if (object_0 is IMapSurround)
				{
					screenDisplayClass = new ScreenDisplay();
					screenDisplayClass.StartDrawing(int_0, 0);
					screenDisplayClass.DisplayTransformation = displayTransformationClass;
					IEnvelope envelope = new Envelope() as IEnvelope;
					envelope.PutCoords((double)(rectangle_0.Left + 5), (double)(rectangle_0.Top + 5), (double)(rectangle_0.Right - 5), (double)(rectangle_0.Bottom - 5));
					((IMapSurround)object_0).Draw(screenDisplayClass, null, envelope);
					screenDisplayClass.FinishDrawing();
					return;
				}
				else if (object_0 is IBackground)
				{
					screenDisplayClass = new ScreenDisplay();
					screenDisplayClass.StartDrawing(int_0, 0);
					screenDisplayClass.DisplayTransformation = displayTransformationClass;
					geometry = ((IBackground)object_0).GetGeometry(screenDisplayClass, envelopeClass);
					((IBackground)object_0).Draw(screenDisplayClass, geometry);
					screenDisplayClass.FinishDrawing();
					return;
				}
				else if (object_0 is IShadow)
				{
					screenDisplayClass = new ScreenDisplay();
					screenDisplayClass.StartDrawing(int_0, 0);
					screenDisplayClass.DisplayTransformation = displayTransformationClass;
					geometry = ((IShadow)object_0).GetGeometry(screenDisplayClass, envelopeClass);
					((IShadow)object_0).Draw(screenDisplayClass, geometry);
					screenDisplayClass.FinishDrawing();
					return;
				}
				else if (object_0 is IBorder)
				{
					screenDisplayClass = new ScreenDisplay();
					screenDisplayClass.StartDrawing(int_0, 0);
					screenDisplayClass.DisplayTransformation = displayTransformationClass;
					IPointCollection polylineClass = new Polyline();
					object value = Missing.Value;
					IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
					pointClass.PutCoords((double)(rectangle_0.X + 4), (double)rectangle_0.Top);
					polylineClass.AddPoint(pointClass, ref value, ref value);
					pointClass.PutCoords((double)(rectangle_0.X + 4), (double)rectangle_0.Bottom);
					polylineClass.AddPoint(pointClass, ref value, ref value);
					pointClass.PutCoords((double)(rectangle_0.Right - 4), (double)rectangle_0.Bottom);
					polylineClass.AddPoint(pointClass, ref value, ref value);
					geometry = ((IBorder)object_0).GetGeometry(screenDisplayClass, (IGeometry)polylineClass);
					((IBorder)object_0).Draw(screenDisplayClass, geometry);
					screenDisplayClass.FinishDrawing();
					return;
				}
				else
				{
					return;
				}
			}
			else
			{
				object0 = new SimpleFillSymbol() as ISymbol;
				IRgbColor rgbColorClass = new RgbColor()
				{
					Red = 227,
					Green = 236,
					Blue = 19
				};
				((IFillSymbol)object0).Color = rgbColorClass;
			}
		Label2:
			flag = (object0 is IPictureFillSymbol ? false : !(object0 is IPictureLineSymbol));
			if (flag)
			{
				object0.SetupDC(int_0, displayTransformationClass);
			}
			else
			{
				object0.SetupDC(int_0, displayTransformationClass);
			}
			if (object0 is IMarkerSymbol)
			{
				SymbolDraw.DrawSymbol((IMarkerSymbol)object0, rectangle_0);
			}
			else if (object0 is ILineSymbol)
			{
				SymbolDraw.DrawSymbol((ILineSymbol)object0, rectangle_0, false);
			}
			else if (object0 is IFillSymbol)
			{
				SymbolDraw.DrawSymbol((IFillSymbol)object0, rectangle_0);
			}
			else if (object0 is ITextSymbol)
			{
				SymbolDraw.DrawSymbol((ITextSymbol)object0, rectangle_0);
			}
			object0.ResetDC();
			return;
		Label1:
			object0 = new SimpleLineSymbol() as ISymbol;
			goto Label2;
		}

		public static void DrawSymbol(IMarkerSymbol imarkerSymbol_0, Rectangle rectangle_0)
		{
			IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
		    pointClass.X = (double)((rectangle_0.Left + rectangle_0.Right) / 2);
		    pointClass.Y = (double)((rectangle_0.Bottom + rectangle_0.Top) / 2);
		    ((ISymbol)imarkerSymbol_0).Draw(pointClass);
		}

		public static void DrawSymbol(ILineSymbol ilineSymbol_0, Rectangle rectangle_0, bool bool_0)
		{
			double num;
			double num1;
			if (ilineSymbol_0 is IPictureLineSymbol)
			{
				if (((IPictureLineSymbol)ilineSymbol_0).Picture != null)
				{
					goto Label1;
				}
				return;
			}
			else if (ilineSymbol_0 is ITemplate)
			{
				bool flag = false;
				int num2 = 0;
				while (true)
				{
					if (num2 < ((ITemplate)ilineSymbol_0).PatternElementCount)
					{
						((ITemplate)ilineSymbol_0).GetPatternElement(num2, out num, out num1);
						if (num + num1 > 0)
						{
							flag = true;
							break;
						}
						else
						{
							num2++;
						}
					}
					else
					{
						break;
					}
				}
				if (!flag)
				{
					return;
				}
			}
		Label1:
			object value = Missing.Value;
			IPointCollection polylineClass = new Polyline();
			IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
			if (!bool_0)
			{
				pointClass.PutCoords((double)(rectangle_0.Left + 3), (double)((rectangle_0.Bottom + rectangle_0.Top) / 2));
				polylineClass.AddPoint(pointClass, ref value, ref value);
				pointClass.PutCoords((double)(rectangle_0.Right - 3), (double)((rectangle_0.Bottom + rectangle_0.Top) / 2));
				polylineClass.AddPoint(pointClass, ref value, ref value);
			}
			else
			{
				pointClass.PutCoords((double)(rectangle_0.Left + 2), (double)(rectangle_0.Bottom + 2));
				polylineClass.AddPoint(pointClass, ref value, ref value);
				pointClass.PutCoords((double)((rectangle_0.Width - 4) / 3 + rectangle_0.Left + 2), (double)(rectangle_0.Top - 2));
				polylineClass.AddPoint(pointClass, ref value, ref value);
				pointClass.PutCoords((double)((rectangle_0.Width - 4) / 3 * 2 + rectangle_0.Left + 2), (double)(rectangle_0.Bottom + 2));
				polylineClass.AddPoint(pointClass, ref value, ref value);
				pointClass.PutCoords((double)(rectangle_0.Right - 2), (double)(rectangle_0.Top - 2));
				polylineClass.AddPoint(pointClass, ref value, ref value);
			}
			((ISymbol)ilineSymbol_0).Draw((IGeometry)polylineClass);
		}

		public static void DrawSymbol(IFillSymbol ifillSymbol_0, Rectangle rectangle_0)
		{
			object value = Missing.Value;
			IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
			IPointCollection polygonClass = new Polygon();
			pointClass.PutCoords((double)(rectangle_0.Left + 3), (double)(rectangle_0.Top + 3));
			polygonClass.AddPoint(pointClass, ref value, ref value);
			pointClass.PutCoords((double)(rectangle_0.Right - 3), (double)(rectangle_0.Top + 3));
			polygonClass.AddPoint(pointClass, ref value, ref value);
			pointClass.PutCoords((double)(rectangle_0.Right - 3), (double)(rectangle_0.Bottom - 3));
			polygonClass.AddPoint(pointClass, ref value, ref value);
			pointClass.PutCoords((double)(rectangle_0.Left + 3), (double)(rectangle_0.Bottom - 3));
			polygonClass.AddPoint(pointClass, ref value, ref value);
			pointClass.PutCoords((double)(rectangle_0.Left + 3), (double)(rectangle_0.Top + 3));
			polygonClass.AddPoint(pointClass, ref value, ref value);
			((ISymbol)ifillSymbol_0).Draw((IGeometry)polygonClass);
		}

		public static void DrawSymbol(ITextSymbol itextSymbol_0, Rectangle rectangle_0)
		{
			IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
		    pointClass.X = (double)((rectangle_0.Left + rectangle_0.Right) / 2);
		    pointClass.Y = (double)((rectangle_0.Bottom + rectangle_0.Top) / 2);
		    ISimpleTextSymbol itextSymbol0 = (ISimpleTextSymbol)itextSymbol_0;
			string text = itextSymbol0.Text;
			bool clip = itextSymbol0.Clip;
			itextSymbol0.Text = "AaBbYyZz";
			itextSymbol0.Clip = true;
			((ISymbol)itextSymbol_0).Draw(pointClass);
			itextSymbol0.Text = text;
			itextSymbol0.Clip = clip;
		}

		private static void old_acctor_mc()
		{
			SymbolDraw.m_pGeometry = null;
		}
	}
}