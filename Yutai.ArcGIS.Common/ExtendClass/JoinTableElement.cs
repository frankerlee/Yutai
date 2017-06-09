using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using stdole;

namespace Yutai.ArcGIS.Common.ExtendClass
{
	[Guid("92A30BE1-E188-4ca7-9CAC-D1B45514A001")]
	public class JoinTableElement : IBoundsProperties, IClone, IElement, IElementProperties, IElementProperties2, IGraphicElement, ITransform2D, IPersistStream, IPersist, IDocumentVersionSupportGEN, IJoinTableElement
	{
		private double width = 4.5;

		private double height = 2.4;

		private IElement m_pGroupElement = null;

		private List<ITextElement> m_pTextElementList = new List<ITextElement>(9);

		private List<bool> m_pTextElementAdd = new List<bool>(9);

		private IEnvelope m_pEnv = new Envelope() as IEnvelope;

		public bool AutoTransform
		{
			get
			{
				return (this.m_pGroupElement as IElementProperties).AutoTransform;
			}
			set
			{
				(this.m_pGroupElement as IElementProperties).AutoTransform = value;
			}
		}

		public object CustomProperty
		{
			get
			{
				return (this.m_pGroupElement as IElementProperties).CustomProperty;
			}
			set
			{
				(this.m_pGroupElement as IElementProperties).CustomProperty = value;
			}
		}

		public bool FixedAspectRatio
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		public bool FixedSize
		{
			get
			{
				return true;
			}
		}

		public IGeometry Geometry
		{
			get
			{
				return this.m_pGroupElement.Geometry;
			}
			set
			{
				this.m_pEnv = value.Envelope;
				this.m_pGroupElement.Geometry = value;
			}
		}

		public ILineSymbol LineSymbol
		{
			get;
			set;
		}

		public bool Locked
		{
			get
			{
				return this.m_pGroupElement.Locked;
			}
			set
			{
				this.m_pGroupElement.Locked = value;
			}
		}

		public string Name
		{
			get
			{
				return (this.m_pGroupElement as IElementProperties).Name;
			}
			set
			{
				(this.m_pGroupElement as IElementProperties).Name = value;
			}
		}

		internal IPropertySet PropertySet
		{
			get
			{
				IPropertySet propertySetClass = new PropertySet();
				propertySetClass.SetProperty("Element", this.m_pGroupElement);
				propertySetClass.SetProperty("TextSymbol", this.TextSymbol);
				propertySetClass.SetProperty("LineSymbol", this.LineSymbol);
				for (int i = 0; i < this.m_pTextElementList.Count; i++)
				{
					if (i != 4)
					{
						propertySetClass.SetProperty(string.Concat("TextElement", i.ToString()), this.m_pTextElementList[i]);
						propertySetClass.SetProperty(string.Concat("TextElementAdd", i.ToString()), this.m_pTextElementAdd[i]);
					}
				}
				return propertySetClass;
			}
			set
			{
				IPropertySet propertySet = value;
				this.m_pGroupElement = propertySet.GetProperty("Element") as IElement;
				this.TextSymbol = propertySet.GetProperty("TextSymbol") as ITextSymbol;
				this.LineSymbol = propertySet.GetProperty("LineSymbol") as ILineSymbol;
				try
				{
					for (int i = 0; i < 9; i++)
					{
						if (i == 4)
						{
							this.m_pTextElementList.Add(null);
							this.m_pTextElementAdd.Add(false);
						}
						else
						{
							this.m_pTextElementList.Add(propertySet.GetProperty(string.Concat("TextElement", i.ToString())) as ITextElement);
							this.m_pTextElementAdd.Add(Convert.ToBoolean(propertySet.GetProperty(string.Concat("TextElementAdd", i.ToString()))));
						}
					}
				}
				catch (Exception exception)
				{
				}
			}
		}

		public double ReferenceScale
		{
			get
			{
				return (this.m_pGroupElement as IElementProperties2).ReferenceScale;
			}
			set
			{
				(this.m_pGroupElement as IElementProperties2).ReferenceScale = value;
			}
		}

		public string Row1Col1Text
		{
			get;
			set;
		}

		public string Row1Col2Text
		{
			get;
			set;
		}

		public string Row1Col3Text
		{
			get;
			set;
		}

		public string Row2Col1Text
		{
			get;
			set;
		}

		public string Row2Col3Text
		{
			get;
			set;
		}

		public string Row3Col1Text
		{
			get;
			set;
		}

		public string Row3Col2Text
		{
			get;
			set;
		}

		public string Row3Col3Text
		{
			get;
			set;
		}

		public ISelectionTracker SelectionTracker
		{
			get
			{
				return this.m_pGroupElement.SelectionTracker;
			}
		}

		public ISpatialReference SpatialReference
		{
			get
			{
				return (this.m_pGroupElement as IGraphicElement).SpatialReference;
			}
			set
			{
				(this.m_pGroupElement as IGraphicElement).SpatialReference = value;
			}
		}

		public ITextSymbol TextSymbol
		{
			get;
			set;
		}

		public string Type
		{
			get
			{
				return (this.m_pGroupElement as IElementProperties).Type;
			}
			set
			{
				(this.m_pGroupElement as IElementProperties).Type = value;
			}
		}

		public JoinTableElement()
		{
            this.LineSymbol = new CartographicLineSymbol
            {
                Color = new RgbColor
                {
                    Red = 0,
                    Blue = 0,
                    Green = 0
                },
                Cap = esriLineCapStyle.esriLCSSquare,
                Join = esriLineJoinStyle.esriLJSMitre
            };
            this.TextSymbol = new TextSymbol
            {
                HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter,
                VerticalAlignment = esriTextVerticalAlignment.esriTVACenter
            };
            IFontDisp font = this.TextSymbol.Font;
            font.Size = 7.09m;
            this.TextSymbol.Font = font;
            this.Row1Col1Text = "";
            this.Row1Col2Text = "";
            this.Row1Col3Text = "";
            this.Row2Col1Text = "";
            this.Row2Col3Text = "";
            this.Row3Col1Text = "";
            this.Row3Col2Text = "";
            this.Row3Col3Text = "";
        }

		public void Activate(IDisplay Display)
		{
			this.m_pGroupElement.Activate(Display);
		}

		public void Assign(IClone src)
		{
			(this.m_pGroupElement as IClone).Assign(src);
		}

		public bool CanRotate()
		{
			return (this.m_pGroupElement as IElementProperties2).CanRotate();
		}

		public IClone Clone()
		{
			return (this.m_pGroupElement as IClone).Clone();
		}

		public object ConvertToSupportedObject(esriArcGISVersion docVersion)
		{
			return this.m_pGroupElement;
		}

		/// <summary>
		/// 连接表
		/// </summary>
		public IElement CreateJionTab(IActiveView pAV, IPoint Leftdown)
		{
			IGroupElement groupElementClass = new GroupElement() as IGroupElement;
			(groupElementClass as IElementProperties).Name = "接图表";
			(groupElementClass as IElementProperties).Type = "接图表";
			ILineSymbol lineSymbol = null;
			double num = this.height / 3;
			double num1 = this.width / 3;
			double num2 = 0.3;
			object missing = System.Type.Missing;
			IElement lineElementClass = new LineElement();
			IElement element = new LineElement();
			IElement lineElementClass1 = new LineElement();
			IElement element1 = new LineElement();
			IElement lineElementClass2 = new LineElement();
			IPolyline polylineClass = new Polyline() as IPolyline;
			IPolyline polyline = new Polyline() as IPolyline;
			IPolyline polylineClass1 = new Polyline() as IPolyline;
            IPolyline polyline1 = new Polyline() as IPolyline;
            IPolyline polylineClass2 = new Polyline() as IPolyline;
            IPointCollection pointCollection = polylineClass as IPointCollection;
		    IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
		    IPoint point = new ESRI.ArcGIS.Geometry.Point();
		    IPoint pointClass1 = new ESRI.ArcGIS.Geometry.Point();
		    IPoint point1 = new ESRI.ArcGIS.Geometry.Point();
			try
			{
				lineSymbol = this.LineSymbol;
				(lineElementClass as ILineElement).Symbol = lineSymbol;
				(element as ILineElement).Symbol = lineSymbol;
				(lineElementClass1 as ILineElement).Symbol = lineSymbol;
				(element1 as ILineElement).Symbol = lineSymbol;
				(lineElementClass2 as ILineElement).Symbol = lineSymbol;
				point.PutCoords(Leftdown.X, Leftdown.Y + num2);
				pointClass.PutCoords(Leftdown.X, Leftdown.Y + num2 + this.height);
				pointClass1.PutCoords(Leftdown.X + this.width, Leftdown.Y + num2);
				point1.PutCoords(Leftdown.X + this.width, Leftdown.Y + num2 + this.height);
				pointCollection.AddPoint(pointClass, ref missing, ref missing);
				pointCollection.AddPoint(point, ref missing, ref missing);
				pointCollection.AddPoint(pointClass1, ref missing, ref missing);
				pointCollection.AddPoint(point1, ref missing, ref missing);
				pointCollection.AddPoint(pointClass, ref missing, ref missing);
				lineElementClass.Geometry = polylineClass;
				groupElementClass.AddElement(lineElementClass);
			    IPoint pointClass2 = new ESRI.ArcGIS.Geometry.Point();
			    IPoint point2 = new ESRI.ArcGIS.Geometry.Point();
				pointClass2.PutCoords(pointClass.X, pointClass.Y - num);
				point2.PutCoords(point1.X, point1.Y - num);
				pointCollection = polyline as IPointCollection;
				pointCollection.AddPoint(pointClass2, ref missing, ref missing);
				pointCollection.AddPoint(point2, ref missing, ref missing);
				element.Geometry = polyline;
				groupElementClass.AddElement(element);
				pointClass2.PutCoords(pointClass.X, pointClass.Y - num * 2);
				point2.PutCoords(point1.X, point1.Y - num * 2);
				pointCollection = polylineClass1 as IPointCollection;
				pointCollection.AddPoint(pointClass2, ref missing, ref missing);
				pointCollection.AddPoint(point2, ref missing, ref missing);
				lineElementClass1.Geometry = polylineClass1;
				groupElementClass.AddElement(lineElementClass1);
				pointClass2.PutCoords(pointClass.X + num1, pointClass.Y);
				point2.PutCoords(pointClass.X + num1, point.Y);
				pointCollection = polyline1 as IPointCollection;
				pointCollection.AddPoint(pointClass2, ref missing, ref missing);
				pointCollection.AddPoint(point2, ref missing, ref missing);
				element1.Geometry = polyline1;
				groupElementClass.AddElement(element1);
				pointClass2.PutCoords(pointClass.X + num1 * 2, pointClass.Y);
				point2.PutCoords(pointClass.X + num1 * 2, point.Y);
				pointCollection = polylineClass2 as IPointCollection;
				pointCollection.AddPoint(pointClass2, ref missing, ref missing);
				pointCollection.AddPoint(point2, ref missing, ref missing);
				lineElementClass2.Geometry = polylineClass2;
				groupElementClass.AddElement(lineElementClass2);
				IPolygon polygonClass = new Polygon() as IPolygon;
				IElement polygonElementClass = new PolygonElement();
				IPolygonElement polygonElement = polygonElementClass as IPolygonElement;
				ISimpleFillSymbol simpleFillSymbolClass = new SimpleFillSymbol();
				IFillShapeElement fillShapeElement = polygonElement as IFillShapeElement;
				IRgbColor rgbColorClass = new RgbColor()
				{
					Red = 0,
					Green = 0,
					Blue = 0
				};
				simpleFillSymbolClass.Outline = this.LineSymbol;
				simpleFillSymbolClass.Color = rgbColorClass;
				simpleFillSymbolClass.Style = esriSimpleFillStyle.esriSFSBackwardDiagonal;
				fillShapeElement.Symbol = simpleFillSymbolClass;
				pointCollection = polygonClass as IPointCollection;
				pointClass2.PutCoords(pointClass.X + num1, pointClass.Y - num);
				pointCollection.AddPoint(pointClass2, ref missing, ref missing);
				pointClass2.PutCoords(pointClass.X + num1 * 2, pointClass.Y - num);
				pointCollection.AddPoint(pointClass2, ref missing, ref missing);
				pointClass2.PutCoords(pointClass.X + num1 * 2, pointClass.Y - num * 2);
				pointCollection.AddPoint(pointClass2, ref missing, ref missing);
				pointClass2.PutCoords(pointClass.X + num1, pointClass.Y - num * 2);
				pointCollection.AddPoint(pointClass2, ref missing, ref missing);
				polygonClass.Close();
				polygonElementClass.Geometry = polygonClass;
				groupElementClass.AddElement(polygonElementClass);
				IEnvelope envelope = polygonClass.Envelope;
				IEnvelope envelopeClass = new Envelope() as IEnvelope;
				this.m_pTextElementList.Add(this.CreateJionTabTextElement(pAV, this.Row1Col1Text, pointClass.X + num1 / 2, pointClass.Y - num / 2, envelope) as ITextElement);
				this.m_pTextElementAdd.Add(true);
				groupElementClass.AddElement(this.m_pTextElementList[0] as IElement);
				this.m_pTextElementList.Add(this.CreateJionTabTextElement(pAV, this.Row1Col2Text, pointClass.X + 1.5 * num1, pointClass.Y - 0.5 * num, envelope) as ITextElement);
				this.m_pTextElementAdd.Add(true);
				groupElementClass.AddElement(this.m_pTextElementList[1] as IElement);
				this.m_pTextElementList.Add(this.CreateJionTabTextElement(pAV, this.Row1Col3Text, pointClass.X + 2.5 * num1, pointClass.Y - 0.5 * num, envelope) as ITextElement);
				this.m_pTextElementAdd.Add(true);
				groupElementClass.AddElement(this.m_pTextElementList[2] as IElement);
				this.m_pTextElementList.Add(this.CreateJionTabTextElement(pAV, this.Row2Col1Text, pointClass.X + num1 / 2, pointClass.Y - 1.5 * num, envelope) as ITextElement);
				this.m_pTextElementAdd.Add(true);
				groupElementClass.AddElement(this.m_pTextElementList[3] as IElement);
				this.m_pTextElementList.Add(null);
				this.m_pTextElementAdd.Add(false);
				this.m_pTextElementList.Add(this.CreateJionTabTextElement(pAV, this.Row2Col3Text, pointClass.X + 2.5 * num1, pointClass.Y - 1.5 * num, envelope) as ITextElement);
				this.m_pTextElementAdd.Add(true);
				groupElementClass.AddElement(this.m_pTextElementList[5] as IElement);
				this.m_pTextElementList.Add(this.CreateJionTabTextElement(pAV, this.Row3Col1Text, pointClass.X + num1 / 2, pointClass.Y - 2.5 * num, envelope) as ITextElement);
				this.m_pTextElementAdd.Add(true);
				groupElementClass.AddElement(this.m_pTextElementList[6] as IElement);
				this.m_pTextElementList.Add(this.CreateJionTabTextElement(pAV, this.Row3Col2Text, pointClass.X + 1.5 * num1, pointClass.Y - 2.5 * num, envelope) as ITextElement);
				this.m_pTextElementAdd.Add(true);
				groupElementClass.AddElement(this.m_pTextElementList[7] as IElement);
				this.m_pTextElementList.Add(this.CreateJionTabTextElement(pAV, this.Row3Col3Text, pointClass.X + 2.5 * num1, pointClass.Y - 2.5 * num, envelope) as ITextElement);
				this.m_pTextElementAdd.Add(true);
				groupElementClass.AddElement(this.m_pTextElementList[8] as IElement);
			}
			catch (Exception exception)
			{
			}
			this.m_pGroupElement = groupElementClass as IElement;
			return this.m_pGroupElement;
		}

		private IElement CreateJionTabTextElement(IActiveView pAV, string text, double x, double y, IEnvelope pNewEnvelope)
		{
		    ITextElement textElement = new TextElement() as ITextElement;
		   ((ITextElement)textElement).Symbol = this.TextSymbol;
		    textElement.Text = text;
		    ITextElement textElementClass = textElement as ITextElement;
			IElement element = textElementClass as IElement;
		    IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
			IEnvelope envelopeClass = new Envelope() as IEnvelope;
			pointClass.PutCoords(x, y);
			element.Geometry = pointClass;
			this.TextSymbol.Text = text;
			IPolygon polygonClass = new Polygon() as IPolygon;
			element.QueryOutline(pAV.ScreenDisplay, polygonClass);
			element.QueryBounds(pAV.ScreenDisplay, envelopeClass);
			if (!envelopeClass.IsEmpty)
			{
				if (envelopeClass.Width > pNewEnvelope.Width)
				{
					double width = pNewEnvelope.Width / envelopeClass.Width;
					double height = pNewEnvelope.Height / envelopeClass.Height;
					(element as ITransform2D).Scale(pointClass, width, height);
				}
			}
			return element;
		}

		public void Deactivate()
		{
			this.m_pGroupElement.Deactivate();
		}

		public void Draw(IDisplay Display, ITrackCancel TrackCancel)
		{
			this.m_pGroupElement.Draw(Display, TrackCancel);
		}

		public void GetClassID(out Guid pClassID)
		{
			pClassID = new Guid("92A30BE1-E188-4ca7-9CAC-D1B45514A001");
		}

		public void GetSizeMax(out _ULARGE_INTEGER pcbSize)
		{
			(this.PropertySet as IPersistStream).GetSizeMax(out pcbSize);
		}

		public bool HitTest(double x, double y, double Tolerance)
		{
			return this.m_pGroupElement.HitTest(x, y, Tolerance);
		}

		public void IsDirty()
		{
		}

		public bool IsEqual(IClone other)
		{
			return (this.m_pGroupElement as IClone).IsEqual(other);
		}

		public bool IsIdentical(IClone other)
		{
			return (this.m_pGroupElement as IClone).IsIdentical(other);
		}

		public bool IsSupportedAtVersion(esriArcGISVersion docVersion)
		{
			return (esriArcGISVersion.esriArcGISVersion83 != docVersion ? true : false);
		}

		public void Load(IStream pstm)
		{
			IPropertySet propertySetClass = new PropertySet();
			(propertySetClass as IPersistStream).Load(pstm);
			this.PropertySet = propertySetClass;
		}

		public void Move(double dx, double dy)
		{
			(this.m_pGroupElement as ITransform2D).Move(dx, dy);
		}

		public void MoveVector(ILine v)
		{
			(this.m_pGroupElement as ITransform2D).MoveVector(v);
		}

		public void QueryBounds(IDisplay Display, IEnvelope Bounds)
		{
			if (this.m_pGroupElement != null)
			{
				this.m_pGroupElement.QueryBounds(Display, Bounds);
			}
		}

		public void QueryOutline(IDisplay Display, IPolygon Outline)
		{
			this.m_pGroupElement.QueryOutline(Display, Outline);
		}

		public void Rotate(IPoint Origin, double rotationAngle)
		{
			(this.m_pGroupElement as ITransform2D).Rotate(Origin, rotationAngle);
		}

		public void Save(IStream pstm, int fClearDirty)
		{
			(this.PropertySet as IPersistStream).Save(pstm, fClearDirty);
		}

		public void Scale(IPoint Origin, double sx, double sy)
		{
			(this.m_pGroupElement as ITransform2D).Scale(Origin, sx, sy);
		}

		public void SetJTB(string name, int index)
		{
			if (this.m_pTextElementList.Count >= 9)
			{
				ITextElement item = this.m_pTextElementList[index];
				item.Text = name;
				if (!this.m_pTextElementAdd[index])
				{
					(this.m_pGroupElement as IGroupElement).AddElement(item as IElement);
					this.m_pTextElementAdd[index] = true;
				}
			}
		}

		public void Transform(esriTransformDirection direction, ITransformation transformation)
		{
			(this.m_pGroupElement as ITransform2D).Transform(direction, transformation);
		}
	}
}