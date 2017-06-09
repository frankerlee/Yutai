using System;
using System.Collections.Generic;
using System.Xml;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Symbol;

namespace Yutai.ArcGIS.Common.ExtendClass
{
	public class JLKLegendAssiatant
	{
		private string m_Title = "图例";

		private int m_colum = 3;

		private bool m_HasBorder = true;

		private double m_colspace = 10;

		private double m_rowspace = 10;

		private double m_itemwidth = 10;

		private double m_itemheight = 10;

		private ITextSymbol m_pTextSymbol = null;

		private ITextSymbol m_TitleTextSymbol = null;

		private bool m_isdrawline = false;

		/// <summary>
		/// 符号与文字间距
		/// </summary>
		private double m_space = 1;

		/// <summary>
		/// 每项的符号是否绘制边框
		/// </summary>
		private bool m_ItemHasBorder = false;

		private IList<ISymbol> m_symbolLists = new List<ISymbol>();

		private IList<ISymbol> m_backsymbolLists = new List<ISymbol>();

		private IList<string> m_symbolDescriptions = new List<string>();

		internal IEnvelope m_pEnvelop = null;

		/// <summary>
		/// 绘制背景
		/// </summary>
		public bool DrawBackgroup
		{
			get;
			set;
		}

		public JLKLegendAssiatant()
		{
		}

		private ISymbol ConvertstringToSymbol(string data)
		{
			int i;
			byte[] numArray = Convert.FromBase64String(data);
			int length = (int)numArray.Length - 16;
			byte[] numArray1 = new byte[16];
			for (i = 0; i < 16; i++)
			{
				numArray1[i] = numArray[i];
			}
			Guid guid = new Guid(numArray1);
			IPersistStream persistStream = (IPersistStream)Activator.CreateInstance(Type.GetTypeFromCLSID(guid));
			byte[] numArray2 = new byte[length];
			for (i = 0; i < length; i++)
			{
				numArray2[i] = numArray[i + 16];
			}
			IMemoryBlobStream memoryBlobStreamClass = new MemoryBlobStream();
			((IMemoryBlobStreamVariant)memoryBlobStreamClass).ImportFromVariant(numArray2);
			persistStream.Load(memoryBlobStreamClass);
			return persistStream as ISymbol;
		}

		/// <summary>
		/// </summary>
		/// <param name="pt">定位点</param>
		public void Create(IActiveView pAV, IPoint pt)
		{
			int count = this.m_symbolLists.Count / this.m_colum;
			if ((double)count * this.m_colspace < (double)this.m_symbolLists.Count)
			{
				count++;
			}
			int num = 0;
			double num1 = 0;
			double height = 0;
			IGroupElement groupElementClass = new GroupElement() as IGroupElement;
			pt.X = pt.X + 0.1;
			pt.Y = pt.Y + 0.1;
			if (this.m_Title.Length > 0)
			{
				IElement element = this.CreateTitleElement(pt);
				IEnvelope envelopeClass = new Envelope() as IEnvelope;
				element.QueryBounds(pAV.ScreenDisplay, envelopeClass);
				height = envelopeClass.Height;
				groupElementClass.AddElement(element);
			}
			double x = pt.X;
			double y = pt.Y - height;
			IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
			IEnvelope envelope = new Envelope() as IEnvelope;
			for (int i = 0; i < this.m_symbolLists.Count; i++)
			{
				ISymbol item = this.m_symbolLists[i];
				pointClass.PutCoords(x, y);
				IElement element1 = this.CreateElement(pointClass, this.m_symbolLists[i], this.m_backsymbolLists[i] as IFillSymbol, this.m_symbolDescriptions[i]);
				element1.QueryBounds(pAV.ScreenDisplay, envelope);
				if (!(element1 is IGroupElement))
				{
					groupElementClass.AddElement(element1);
				}
				else
				{
					for (int j = 0; j < (element1 as IGroupElement).ElementCount; j++)
					{
						groupElementClass.AddElement((element1 as IGroupElement).Element[j]);
					}
				}
				(groupElementClass as IGroupElement2).Refresh();
				num1 = (num1 > envelope.Width ? num1 : envelope.Width);
				y = y - this.m_itemheight - this.m_rowspace;
				num++;
				if (num == count)
				{
					y = pt.Y - height;
					x = x + (num1 + this.m_colspace);
					num = 0;
					num1 = 0;
				}
			}
			IEnvelope envelopeClass1 = new Envelope() as IEnvelope;
			(groupElementClass as IElement).QueryBounds(pAV.ScreenDisplay, envelopeClass1);
			envelopeClass1.Expand(0.05, 0.05, false);
			try
			{
				if (this.m_HasBorder)
				{
					IRgbColor rgbColorClass = new RgbColor()
					{
						Red = 255,
						Blue = 255,
						Green = 255
					};
					groupElementClass.AddElement(this.CreatePolygonElement(envelopeClass1, rgbColorClass));
				}
				(groupElementClass as IElement).QueryBounds(pAV.ScreenDisplay, envelopeClass1);
				(groupElementClass as ITransform2D).Scale(envelopeClass1.UpperLeft, 1, 1);
				(pAV as IGraphicsContainer).AddElement(groupElementClass as IElement, -1);
			}
			catch (Exception exception)
			{
			}
			pAV.PartialRefresh(esriViewDrawPhase.esriViewGraphics, groupElementClass, null);
		}

		public void Create(IActiveView pAV, IEnvelope pEnvelop)
		{
			IPoint upperLeft = pEnvelop.UpperLeft;
			int count = this.m_symbolLists.Count / this.m_colum;
			if ((double)count * this.m_colspace < (double)this.m_symbolLists.Count)
			{
				count++;
			}
			int num = 0;
			double num1 = 0;
			double height = 0;
			upperLeft.X = upperLeft.X + 0.1;
			upperLeft.Y = upperLeft.Y + 0.1;
			IGroupElement groupElementClass = new GroupElement() as IGroupElement;
			if (this.m_Title.Length > 0)
			{
				IElement element = this.CreateTitleElement(upperLeft);
				IEnvelope envelopeClass = new Envelope() as IEnvelope;
				element.QueryBounds(pAV.ScreenDisplay, envelopeClass);
				height = envelopeClass.Height;
				groupElementClass.AddElement(element);
			}
			double x = upperLeft.X;
			double y = upperLeft.Y - height;
			IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
			IEnvelope envelope = new Envelope() as IEnvelope;
			for (int i = 0; i < this.m_symbolLists.Count; i++)
			{
				ISymbol item = this.m_symbolLists[i];
				pointClass.PutCoords(x, y);
				IElement element1 = this.CreateElement(pointClass, this.m_symbolLists[i], this.m_backsymbolLists[i] as IFillSymbol, this.m_symbolDescriptions[i]);
				element1.QueryBounds(pAV.ScreenDisplay, envelope);
				if (!(element1 is IGroupElement))
				{
					groupElementClass.AddElement(element1);
				}
				else
				{
					for (int j = 0; j < (element1 as IGroupElement).ElementCount; j++)
					{
						groupElementClass.AddElement((element1 as IGroupElement).Element[j]);
					}
				}
				num1 = (num1 > envelope.Width ? num1 : envelope.Width);
				y = y - this.m_itemheight - this.m_rowspace;
				num++;
				if (num == count)
				{
					y = upperLeft.Y - height;
					x = x + (num1 + this.m_colspace);
					num = 0;
				}
			}
			(groupElementClass as IElement).QueryBounds(pAV.ScreenDisplay, envelope);
			double width = pEnvelop.Width / envelope.Width;
			double height1 = pEnvelop.Height / envelope.Height;
			pEnvelop.Expand(-0.05, -0.05, false);
			(groupElementClass as ITransform2D).Scale(pEnvelop.UpperLeft, width, height1);
			pEnvelop.Expand(0.05, 0.05, false);
			if (this.m_HasBorder)
			{
				groupElementClass.AddElement(this.CreatePolygonElement(pEnvelop));
			}
			(pAV as IGraphicsContainer).AddElement(groupElementClass as IElement, -1);
			pAV.PartialRefresh(esriViewDrawPhase.esriViewGraphics, groupElementClass, null);
		}

		public IElement CreateElement(IActiveView pAV, IPoint pt)
		{
			IElement element;
			double num = 0;
			int count = this.m_symbolLists.Count / this.m_colum;
			if (count * this.m_colum < this.m_symbolLists.Count)
			{
				count++;
			}
			int num1 = 0;
			double num2 = 0;
			double height = 0;
			double y = pt.Y + 0.1;
			IGroupElement groupElementClass = new GroupElement() as IGroupElement;
			IEnvelope envelopeClass = new Envelope() as IEnvelope;
			envelopeClass.PutCoords(pt.X + 0.1, pt.Y + 0.1, pt.X + 0.1, pt.Y + 0.1);
			IRgbColor rgbColorClass = new RgbColor()
			{
				Red = 255,
				Blue = 255,
				Green = 255
			};
			IElement element1 = this.CreatePolygonElement(envelopeClass, rgbColorClass);
			groupElementClass.AddElement(element1);
			pt.X = pt.X + 0.1;
			pt.Y = pt.Y + 0.1;
			IElement element2 = null;
			if (this.m_Title.Length > 0)
			{
				element2 = this.CreateTitleElement(pt);
				IEnvelope envelope = new Envelope() as IEnvelope;
				element2.QueryBounds(pAV.ScreenDisplay, envelope);
				height = envelope.Height;
				groupElementClass.AddElement(element2);
			}
			double x = pt.X;
			double mItemheight = pt.Y - height - this.m_rowspace;
			IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
			IEnvelope envelopeClass1 = new Envelope() as IEnvelope;
			List<double> nums = new List<double>();
			List<double> nums1 = new List<double>();
			if (height > 0)
			{
				nums.Add(mItemheight + this.m_rowspace / 2);
			}
			bool flag = false;
			for (int i = 0; i < this.m_symbolLists.Count; i++)
			{
				ISymbol item = this.m_symbolLists[i];
				pointClass.PutCoords(x, mItemheight);
				element = this.CreateElement(pointClass, this.m_symbolLists[i], this.m_backsymbolLists[i] as IFillSymbol, this.m_symbolDescriptions[i]);
				element.QueryBounds(pAV.ScreenDisplay, envelopeClass1);
				if (!(element is IGroupElement))
				{
					groupElementClass.AddElement(element);
				}
				else
				{
					int elementCount = (element as IGroupElement).ElementCount;
					for (int j = 0; j < (element as IGroupElement).ElementCount; j++)
					{
						groupElementClass.AddElement((element as IGroupElement).Element[j]);
					}
				}
				num2 = (num2 > envelopeClass1.Width ? num2 : envelopeClass1.Width);
				mItemheight = mItemheight - this.m_itemheight - this.m_rowspace;
				num1++;
				if (num1 == count)
				{
					mItemheight = pt.Y - height - this.m_rowspace;
					nums1.Add(x + this.m_itemwidth + this.m_space / 2);
					x = x + (num2 + this.m_colspace);
					nums1.Add(x - this.m_colspace / 2);
					num1 = 0;
					num2 = 0;
					flag = true;
				}
				else if (!flag)
				{
					nums.Add(mItemheight + this.m_rowspace / 2);
				}
			}
			if (num1 != count)
			{
				mItemheight = pt.Y - height - this.m_rowspace;
				nums1.Add(x + this.m_itemwidth + this.m_space / 2);
				x = x + (num2 + this.m_colspace);
				nums1.Add(x - this.m_colspace / 2);
			}
			IEnvelope envelope1 = new Envelope() as IEnvelope;
			(groupElementClass as IElement).QueryBounds(pAV.ScreenDisplay, envelope1);
			(groupElementClass as IElement).Activate(pAV.ScreenDisplay);
			envelope1.Expand(0.05, 0.05, false);
			try
			{
				if (!this.m_HasBorder)
				{
					groupElementClass.DeleteElement(element1);
				}
				else
				{
					IRgbColor rgbColorClass1 = new RgbColor()
					{
						Red = 255,
						Blue = 255,
						Green = 255
					};
					(groupElementClass as IGroupElement3).ReplaceElement(element1, this.CreatePolygonElement(envelope1, rgbColorClass1));
				}
				(groupElementClass as IElement).QueryBounds(pAV.ScreenDisplay, envelope1);
				if (this.m_isdrawline)
				{
					foreach (double num3 in nums)
					{
						element = this.CreateLineElement(envelope1.XMin, num3, envelope1.XMax, num3);
						groupElementClass.AddElement(element);
					}
					double yMax = envelope1.YMax;
					if (nums.Count > 0)
					{
						if (height > 0)
						{
							yMax = nums[0];
						}
					}
					if (nums1.Count > 0)
					{
						nums1.RemoveAt(nums1.Count - 1);
					}
					foreach (double num3 in nums1)
					{
						element = this.CreateLineElement(num3, envelope1.YMin, num3, yMax);
						groupElementClass.AddElement(element);
					}
					(groupElementClass as IElement).QueryBounds(pAV.ScreenDisplay, envelope1);
				}
				this.m_pEnvelop = envelope1;
			}
			catch (Exception exception)
			{
			}
			if (element2 != null)
			{
			    IPoint point = new ESRI.ArcGIS.Geometry.Point();
				point.PutCoords((envelope1.XMin + envelope1.XMax) / 2, y);
				element2.Geometry = point;
				(element2 as ITextElement).Symbol = this.FontStyle(20, esriTextHorizontalAlignment.esriTHACenter, esriTextVerticalAlignment.esriTVATop);
			}
			(groupElementClass as IElementProperties).Name = "图例";
			(groupElementClass as IElement).Geometry = envelope1;
			return groupElementClass as IElement;
		}

		private IElement CreateElement(IPoint pt, ISymbol pSymbol, IFillSymbol pBackSymbol, string des)
		{
			IElement element;
			if (pSymbol is IMarkerSymbol)
			{
				element = this.CreatePointElement(pt, pSymbol as IMarkerSymbol, pBackSymbol, des);
			}
			else if (pSymbol is ILineSymbol)
			{
				element = this.CreateLineElement(pt, pSymbol as ILineSymbol, pBackSymbol, des);
			}
			else if (!(pSymbol is IFillSymbol))
			{
				element = null;
			}
			else
			{
				element = this.CreatePolygonElement(pt, pSymbol as IFillSymbol, pBackSymbol, des);
			}
			return element;
		}

		private IElement CreateLineElement(double x1, double y1, double x2, double y2)
		{
			ILineSymbol simpleLineSymbolClass = new SimpleLineSymbol();
			IPolyline polylineClass = new Polyline() as IPolyline;
			object missing = Type.Missing;
			IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(x1, y1);
			(polylineClass as IPointCollection).AddPoint(pointClass, ref missing, ref missing);
			pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(x2, y2);
			(polylineClass as IPointCollection).AddPoint(pointClass, ref missing, ref missing);
			IElement lineElementClass = new LineElement()
			{
				Geometry = polylineClass
			};
			(lineElementClass as ILineElement).Symbol = simpleLineSymbolClass;
			return lineElementClass;
		}

		private IElement CreateLineElement(IPoint pt, ILineSymbol pSymbol, IFillSymbol pBackSymbol, string des)
		{
			IGroupElement groupElementClass = new GroupElement() as IGroupElement;
			IPolyline polylineClass = new Polyline() as IPolyline;
			object missing = Type.Missing;
			IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(pt.X + this.m_itemwidth / 10, pt.Y - this.m_itemheight / 2);
			(polylineClass as IPointCollection).AddPoint(pointClass, ref missing, ref missing);
			pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(pt.X + this.m_itemwidth * 0.9, pt.Y - this.m_itemheight / 2);
			(polylineClass as IPointCollection).AddPoint(pointClass, ref missing, ref missing);
			IElement lineElementClass = new LineElement()
			{
				Geometry = polylineClass
			};
			(lineElementClass as ILineElement).Symbol = pSymbol;
			if (pBackSymbol != null)
			{
				groupElementClass.AddElement(this.CreatePolygonElement(pt, pBackSymbol));
			}
			else if (this.m_ItemHasBorder)
			{
				groupElementClass.AddElement(this.CreatePolygonElement(pt));
			}
			groupElementClass.AddElement(lineElementClass);
			if (des.Length > 0)
			{
				groupElementClass.AddElement(this.CreateTextElement(pt, des, 10));
			}
			return groupElementClass as IElement;
		}

		private IElement CreatePointElement(IPoint pt, IMarkerSymbol pSymbol, IFillSymbol pBackSymbol, string des)
		{
			IGroupElement groupElementClass = new GroupElement() as IGroupElement;
			IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(pt.X + this.m_itemwidth / 2, pt.Y - this.m_itemheight / 2);
			IElement markerElementClass = new MarkerElement()
			{
				Geometry = pointClass
			};
			(markerElementClass as IMarkerElement).Symbol = pSymbol;
			if (pBackSymbol != null)
			{
				groupElementClass.AddElement(this.CreatePolygonElement(pt, pBackSymbol));
			}
			else if (this.m_ItemHasBorder)
			{
				groupElementClass.AddElement(this.CreatePolygonElement(pt));
			}
			groupElementClass.AddElement(markerElementClass);
			if (des.Length > 0)
			{
				groupElementClass.AddElement(this.CreateTextElement(pt, des, 10));
			}
			return groupElementClass as IElement;
		}

		private IElement CreatePolygonElement(IPoint pt)
		{
			IFillSymbol simpleFillSymbolClass = new SimpleFillSymbol();
			(simpleFillSymbolClass as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSNull;
			ILineSymbol simpleLineSymbolClass = new SimpleLineSymbol();
			IRgbColor rgbColorClass = new RgbColor()
			{
				Red = 0,
				Green = 0,
				Blue = 255
			};
			simpleLineSymbolClass.Color = rgbColorClass;
			simpleLineSymbolClass.Width = 1;
			simpleFillSymbolClass.Outline = simpleLineSymbolClass;
			IPolygon polygonClass = new Polygon() as IPolygon;
			object missing = Type.Missing;
			IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(pt.X, pt.Y);
			(polygonClass as IPointCollection).AddPoint(pointClass, ref missing, ref missing);
			pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(pt.X, pt.Y - this.m_itemheight);
			(polygonClass as IPointCollection).AddPoint(pointClass, ref missing, ref missing);
			pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(pt.X + this.m_itemwidth, pt.Y - this.m_itemheight);
			(polygonClass as IPointCollection).AddPoint(pointClass, ref missing, ref missing);
			pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(pt.X + this.m_itemwidth, pt.Y);
			(polygonClass as IPointCollection).AddPoint(pointClass, ref missing, ref missing);
			pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(pt.X, pt.Y);
			(polygonClass as IPointCollection).AddPoint(pointClass, ref missing, ref missing);
			IElement rectangleElementClass = new RectangleElement();
		    rectangleElementClass.Geometry = polygonClass.Envelope;
		    (rectangleElementClass as IFillShapeElement).Symbol = simpleFillSymbolClass;
			return rectangleElementClass;
		}

		private IElement CreatePolygonElement(IPoint pt, IFillSymbol pFillSymbol)
		{
			IPolygon polygonClass = new Polygon() as IPolygon;
			object missing = Type.Missing;
			IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(pt.X, pt.Y);
			(polygonClass as IPointCollection).AddPoint(pointClass, ref missing, ref missing);
			pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(pt.X, pt.Y - this.m_itemheight);
			(polygonClass as IPointCollection).AddPoint(pointClass, ref missing, ref missing);
			pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(pt.X + this.m_itemwidth, pt.Y - this.m_itemheight);
			(polygonClass as IPointCollection).AddPoint(pointClass, ref missing, ref missing);
			pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(pt.X + this.m_itemwidth, pt.Y);
			(polygonClass as IPointCollection).AddPoint(pointClass, ref missing, ref missing);
			pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(pt.X, pt.Y);
			(polygonClass as IPointCollection).AddPoint(pointClass, ref missing, ref missing);
			IElement rectangleElementClass = new RectangleElement();
		    rectangleElementClass.Geometry = polygonClass.Envelope;
		    (rectangleElementClass as IFillShapeElement).Symbol = pFillSymbol;
			return rectangleElementClass;
		}

		private IElement CreatePolygonElement(IEnvelope pEnv)
		{
			IFillSymbol simpleFillSymbolClass = new SimpleFillSymbol();
			(simpleFillSymbolClass as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSNull;
			IPolygon polygonClass = new Polygon() as IPolygon;
			object missing = Type.Missing;
			(polygonClass as IPointCollection).AddPoint(pEnv.LowerLeft, ref missing, ref missing);
			(polygonClass as IPointCollection).AddPoint(pEnv.UpperLeft, ref missing, ref missing);
			(polygonClass as IPointCollection).AddPoint(pEnv.UpperRight, ref missing, ref missing);
			(polygonClass as IPointCollection).AddPoint(pEnv.LowerRight, ref missing, ref missing);
			(polygonClass as IPointCollection).AddPoint(pEnv.LowerLeft, ref missing, ref missing);
			IElement rectangleElementClass = new RectangleElement();
		    rectangleElementClass.Geometry = polygonClass.Envelope;
		    (rectangleElementClass as IFillShapeElement).Symbol = simpleFillSymbolClass;
			return rectangleElementClass;
		}

		private IElement CreatePolygonElement(IEnvelope pEnv, IColor fillColor)
		{
			IFillSymbol simpleFillSymbolClass = new SimpleFillSymbol();
			(simpleFillSymbolClass as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSSolid;
			(simpleFillSymbolClass as ISimpleFillSymbol).Color = fillColor;
			IPolygon polygonClass = new Polygon() as IPolygon;
			object missing = Type.Missing;
			(polygonClass as IPointCollection).AddPoint(pEnv.LowerLeft, ref missing, ref missing);
			(polygonClass as IPointCollection).AddPoint(pEnv.UpperLeft, ref missing, ref missing);
			(polygonClass as IPointCollection).AddPoint(pEnv.UpperRight, ref missing, ref missing);
			(polygonClass as IPointCollection).AddPoint(pEnv.LowerRight, ref missing, ref missing);
			(polygonClass as IPointCollection).AddPoint(pEnv.LowerLeft, ref missing, ref missing);
			IElement rectangleElementClass = new RectangleElement()
			{
				Geometry = polygonClass.Envelope
			};
			(rectangleElementClass as IFillShapeElement).Symbol = simpleFillSymbolClass;
			return rectangleElementClass;
		}

		private IElement CreatePolygonElement(IPoint pt, IFillSymbol pSymbol, IFillSymbol pBackSymbol, string des)
		{
			IGroupElement groupElementClass = new GroupElement() as IGroupElement;
			IPolygon polygonClass = new Polygon() as IPolygon;
			object missing = Type.Missing;
			IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(pt.X, pt.Y);
			(polygonClass as IPointCollection).AddPoint(pointClass, ref missing, ref missing);
			pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(pt.X, pt.Y - this.m_itemheight);
			(polygonClass as IPointCollection).AddPoint(pointClass, ref missing, ref missing);
			pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(pt.X + this.m_itemwidth, pt.Y - this.m_itemheight);
			(polygonClass as IPointCollection).AddPoint(pointClass, ref missing, ref missing);
			pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(pt.X + this.m_itemwidth, pt.Y);
			(polygonClass as IPointCollection).AddPoint(pointClass, ref missing, ref missing);
			pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(pt.X, pt.Y);
			(polygonClass as IPointCollection).AddPoint(pointClass, ref missing, ref missing);
			IElement rectangleElementClass = new RectangleElement()
			{
				Geometry = polygonClass.Envelope
			};
			(rectangleElementClass as IFillShapeElement).Symbol = pSymbol;
			if (pBackSymbol != null)
			{
				groupElementClass.AddElement(this.CreatePolygonElement(pt, pBackSymbol));
			}
			else if (this.m_ItemHasBorder)
			{
				if (pSymbol.Outline != null)
				{
					IRgbColor color = pSymbol.Outline.Color as IRgbColor;
					if (color != null)
					{
						if ((color.Red < 212 || color.Green < 208 ? false : color.Blue >= 200))
						{
							groupElementClass.AddElement(this.CreatePolygonElement(pt));
						}
					}
				}
				else
				{
					groupElementClass.AddElement(this.CreatePolygonElement(pt));
				}
			}
			groupElementClass.AddElement(rectangleElementClass);
			if (des.Length > 0)
			{
				groupElementClass.AddElement(this.CreateTextElement(pt, des, 10));
			}
			return groupElementClass as IElement;
		}

		private IElement CreateTextElement(IPoint pt, string des, int size)
		{
			IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(pt.X + this.m_itemwidth + this.m_space, pt.Y - this.m_itemheight / 2);
			IElement textElementClass = new TextElement()
			{
				Geometry = pointClass
			};
			(textElementClass as ITextElement).Text = des;
			(textElementClass as ITextElement).Symbol = this.FontStyle((double)size, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVACenter);
			return textElementClass;
		}

		private IElement CreateTitleElement(IPoint pt)
		{
			IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(pt.X, pt.Y);
			IElement textElementClass = new TextElement()
			{
				Geometry = pointClass
			};
			(textElementClass as ITextElement).Text = this.m_Title;
			(textElementClass as ITextElement).Symbol = this.FontStyle(20, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
			return textElementClass;
		}

		/// <summary>
		/// 文字设置
		/// </summary>
		/// <param name="txtEle"></param>
		/// <param name="size"></param>
		/// <param name="hAlignment"></param>
		/// <param name="vAligment"></param>
		protected ITextSymbol FontStyle(double size, esriTextHorizontalAlignment hAlignment, esriTextVerticalAlignment vAligment)
		{
			ITextSymbol textSymbolClass = new TextSymbol()
			{
				Size = size,
				Color = ColorManage.CreatColor(0, 0, 0),
				HorizontalAlignment = hAlignment,
				VerticalAlignment = vAligment
			};
			return textSymbolClass;
		}

		private void GroupElement(IGraphicsContainer pGC, IEnvelope pEnvelop)
		{
			IGroupElement2 groupElementClass = null;
			IGraphicsContainerSelect graphicsContainerSelect = pGC as IGraphicsContainerSelect;
			IEnumElement selectedElements = graphicsContainerSelect.SelectedElements;
			selectedElements.Reset();
			IElement i = selectedElements.Next();
			if (groupElementClass == null)
			{
				groupElementClass = new GroupElement() as IGroupElement2;
				(groupElementClass as IElement).Geometry = pEnvelop;
				while (i != null)
				{
					groupElementClass.AddElement(i);
					groupElementClass.Refresh();
					i = selectedElements.Next();
				}
			}
			pGC.AddElement(groupElementClass as IElement, -1);
			(groupElementClass as IElement).QueryBounds((pGC as IActiveView).ScreenDisplay, pEnvelop);
			selectedElements.Reset();
			for (i = selectedElements.Next(); i != null; i = selectedElements.Next())
			{
				pGC.DeleteElement(i);
			}
			graphicsContainerSelect.SelectElement(groupElementClass as IElement);
		}

		public void Init(int type)
		{
			if (type == 1)
			{
				this.m_colspace = 0.1;
				this.m_rowspace = 0.1;
				this.m_itemwidth = 1;
				this.m_itemheight = 1;
				this.m_space = 0.1;
			}
			ISymbol simpleMarkerSymbolClass = new SimpleMarkerSymbol() as ISymbol;
			(simpleMarkerSymbolClass as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSCircle;
			this.m_symbolLists.Add(simpleMarkerSymbolClass);
			this.m_symbolDescriptions.Add("圆");
			simpleMarkerSymbolClass = new SimpleMarkerSymbol() as ISymbol;
			(simpleMarkerSymbolClass as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSCross;
			this.m_symbolLists.Add(simpleMarkerSymbolClass);
			this.m_symbolDescriptions.Add("十字形");
			simpleMarkerSymbolClass = new SimpleMarkerSymbol() as ISymbol;
			(simpleMarkerSymbolClass as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSDiamond;
			this.m_symbolLists.Add(simpleMarkerSymbolClass);
			this.m_symbolDescriptions.Add("菱形");
			simpleMarkerSymbolClass = new SimpleLineSymbol() as ISymbol;
			(simpleMarkerSymbolClass as ISimpleLineSymbol).Style = esriSimpleLineStyle.esriSLSDashDot;
			this.m_symbolLists.Add(simpleMarkerSymbolClass);
			this.m_symbolDescriptions.Add("线");
			simpleMarkerSymbolClass = new SimpleLineSymbol() as ISymbol;
			(simpleMarkerSymbolClass as ISimpleLineSymbol).Style = esriSimpleLineStyle.esriSLSSolid;
			this.m_symbolLists.Add(simpleMarkerSymbolClass);
			this.m_symbolDescriptions.Add("线2");
			simpleMarkerSymbolClass = new SimpleLineSymbol() as ISymbol;
			(simpleMarkerSymbolClass as ISimpleLineSymbol).Style = esriSimpleLineStyle.esriSLSDashDot;
			(simpleMarkerSymbolClass as ISimpleLineSymbol).Width = 3;
			this.m_symbolLists.Add(simpleMarkerSymbolClass);
			this.m_symbolDescriptions.Add("线3");
			simpleMarkerSymbolClass = new SimpleFillSymbol() as ISymbol;
			(simpleMarkerSymbolClass as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSBackwardDiagonal;
			this.m_symbolLists.Add(simpleMarkerSymbolClass);
			this.m_symbolDescriptions.Add("面1");
			simpleMarkerSymbolClass = new SimpleFillSymbol() as ISymbol;
			(simpleMarkerSymbolClass as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSHorizontal;
			this.m_symbolLists.Add(simpleMarkerSymbolClass);
			this.m_symbolDescriptions.Add("面2");
		}

		public void LoadXml(string xml)
		{
			if (xml.Length != 0)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(xml);
				this.ReadLegend(xmlDocument.DocumentElement);
			}
		}

		private void ReadLegend(XmlNode pXMLNode)
		{
			for (int i = 0; i < pXMLNode.Attributes.Count; i++)
			{
				XmlAttribute itemOf = pXMLNode.Attributes[i];
				string lower = itemOf.Name.ToLower();
				if (lower != null)
				{
					if (lower == "title")
					{
						this.m_Title = itemOf.Value;
					}
					else if (lower == "column")
					{
						this.m_colum = int.Parse(itemOf.Value);
					}
					else if (lower == "rowspace")
					{
						this.m_rowspace = double.Parse(itemOf.Value);
					}
					else if (lower == "columnspace")
					{
						this.m_colspace = double.Parse(itemOf.Value);
					}
					else if (lower == "hasborder")
					{
						try
						{
							this.m_HasBorder = bool.Parse(itemOf.Value);
						}
						catch
						{
						}
					}
					else if (lower == "drawtablecell")
					{
						try
						{
							this.m_isdrawline = bool.Parse(itemOf.Value);
						}
						catch
						{
						}
					}
				}
			}
			for (int j = 0; j < pXMLNode.ChildNodes.Count; j++)
			{
				XmlNode xmlNodes = pXMLNode.ChildNodes[j];
				if (xmlNodes.Name.ToLower() == "legenditems")
				{
					this.ReadLegendItems(xmlNodes);
				}
			}
		}

		private void ReadLegendItem(XmlNode pXMLNode)
		{
			ISymbol symbol = null;
			ISymbol symbol1 = null;
			string value = "";
			for (int i = 0; i < pXMLNode.Attributes.Count; i++)
			{
				XmlAttribute itemOf = pXMLNode.Attributes[i];
				string lower = itemOf.Name.ToLower();
				if (lower != null)
				{
					if (lower == "description")
					{
						value = itemOf.Value;
					}
					else if (lower == "symbol")
					{
						symbol = this.ConvertstringToSymbol(itemOf.Value);
					}
					else if (lower == "backsymbol")
					{
						symbol1 = this.ConvertstringToSymbol(itemOf.Value);
					}
				}
			}
			if (symbol != null)
			{
				this.m_symbolLists.Add(symbol);
				this.m_symbolDescriptions.Add(value);
				this.m_backsymbolLists.Add(symbol1);
			}
		}

		private void ReadLegendItems(XmlNode pXMLNode)
		{
			for (int i = 0; i < pXMLNode.Attributes.Count; i++)
			{
				XmlAttribute itemOf = pXMLNode.Attributes[i];
				string lower = itemOf.Name.ToLower();
				if (lower != null)
				{
					if (lower == "width")
					{
						this.m_itemwidth = double.Parse(itemOf.Value);
					}
					else if (lower == "height")
					{
						this.m_itemheight = double.Parse(itemOf.Value);
					}
					else if (lower == "labelspace")
					{
						this.m_space = double.Parse(itemOf.Value);
					}
					else if (lower == "hasborder")
					{
						try
						{
							this.m_ItemHasBorder = bool.Parse(itemOf.Value);
						}
						catch
						{
						}
					}
					else if (lower == "drawtablecell")
					{
						try
						{
						}
						catch
						{
						}
					}
				}
			}
			for (int j = 0; j < pXMLNode.ChildNodes.Count; j++)
			{
				XmlNode xmlNodes = pXMLNode.ChildNodes[j];
				if (xmlNodes.Name.ToLower() == "legenditem")
				{
					this.ReadLegendItem(xmlNodes);
				}
			}
		}
	}
}