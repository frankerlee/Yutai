using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using stdole;

namespace Yutai.ArcGIS.Common.ExtendClass
{
	[Guid("47DEC0C4-097A-425f-8A57-A5508571C9C4")]
	public class TableElement : IBoundsProperties, IClone, IElement, IElementProperties, IElementProperties2, IGraphicElement, ITransform2D, IPersistStream, IPersist, IDocumentVersionSupportGEN, ITableElement, IGraphicsComposite
	{
		private double width = 4.5;

		private double height = 2.4;

		private IElement m_pGroupElement = null;

		private IEnvelope m_pEnv = new Envelope() as IEnvelope;

		private SortedList<int, SortedList<int, IElement>> m_tabcell = new SortedList<int, SortedList<int, IElement>>();

		private SortedList<int, SortedList<int, string>> m_tabcellText = new SortedList<int, SortedList<int, string>>();

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

		public int ColumnNumber
		{
			get;
			set;
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
				return false;
			}
			set
			{
			}
		}

		public bool FixedSize
		{
			get
			{
				return false;
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

		/// <summary>
		/// 内部水平线
		/// </summary>
		public bool HasInnerHorizontalLine
		{
			get;
			set;
		}

		/// <summary>
		/// 内部竖直线
		/// </summary>
		public bool HasInnerVerticalLine
		{
			get;
			set;
		}

		/// <summary>
		/// 左边线
		/// </summary>
		public bool HasLeftBoundLine
		{
			get;
			set;
		}

		/// <summary>
		/// 下边线
		/// </summary>
		public bool HasLowerBoundLine
		{
			get;
			set;
		}

		/// <summary>
		/// 右边线
		/// </summary>
		public bool HasRightBoundLine
		{
			get;
			set;
		}

		/// <summary>
		/// 上边线
		/// </summary>
		public bool HasUpperBoundLine
		{
			get;
			set;
		}

		public double Height
		{
			get;
			set;
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

		/// <summary>
		/// 只有内部第一个水平线。仅当HasInnerHorizontalLine为true，该值才起作用
		/// </summary>
		public bool OnlyFirstHorizontalLine
		{
			get;
			set;
		}

		/// <summary>
		/// 只有内部第一个竖直线。仅当HasInnerVerticalLine为true，该值才起作用
		/// </summary>
		public bool OnlyFirstVerticalLine
		{
			get;
			set;
		}

		internal IPropertySet PropertySet
		{
			get
			{
				IPropertySet propertySetClass = new PropertySet();
				propertySetClass.SetProperty("Element", this.m_pGroupElement);
				propertySetClass.SetProperty("TextSymbol", this.TextSymbol);
				propertySetClass.SetProperty("LineSymbol", this.LineSymbol);
				try
				{
					propertySetClass.SetProperty("Row", this.RowNumber);
					propertySetClass.SetProperty("Col", this.ColumnNumber);
					for (int i = 0; i < this.RowNumber; i++)
					{
						for (int j = 0; j < this.ColumnNumber; j++)
						{
							IElement cellElement = this.GetCellElement(i, j);
							propertySetClass.SetProperty(string.Format("{0},{1}", i, j), this.FindElement(this.m_pGroupElement as IGroupElement, cellElement));
						}
					}
				}
				catch
				{
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
					this.RowNumber = Convert.ToInt32(value.GetProperty("Row"));
					this.ColumnNumber = Convert.ToInt32(value.GetProperty("Col"));
					for (int i = 0; i < this.RowNumber; i++)
					{
						for (int j = 0; j < this.ColumnNumber; j++)
						{
							int num = Convert.ToInt32(value.GetProperty(string.Format("{0},{1}", i, j)));
							if (num != -1)
							{
								this.SetTableCell(i, j, (this.m_pGroupElement as IGroupElement).Element[num]);
							}
						}
					}
				}
				catch
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

		public int RowNumber
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

		public double Width
		{
			get;
			set;
		}

		public TableElement()
		{
            this.LineSymbol = new CartographicLineSymbol
            {
                Color = new RgbColor
                {
                    Red = 0,
                    Blue = 0,
                    Green = 0
                },
                Join = esriLineJoinStyle.esriLJSMitre,
                Cap = esriLineCapStyle.esriLCSSquare
            };
            this.TextSymbol = new TextSymbol
            {
                HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft,
                VerticalAlignment = esriTextVerticalAlignment.esriTVACenter
            };
            IFontDisp font = this.TextSymbol.Font;
            font.Size = 7.09m;
            this.TextSymbol.Font = font;
            this.Width = 4.0;
            this.Height = 4.0;
            this.RowNumber = 2;
            this.ColumnNumber = 4;
            this.HasInnerHorizontalLine = true;
            this.HasInnerVerticalLine = true;
            this.HasLeftBoundLine = true;
            this.HasLowerBoundLine = true;
            this.HasRightBoundLine = true;
            this.HasUpperBoundLine = true;
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
		/// 绘制水平线
		/// </summary>
		/// <param name="pt"></param>
		/// <returns></returns>
		private IElement CreateHLine(IPoint pt)
		{
            IPoint point = new Point
            {
                X = pt.X,
                Y = pt.Y
            };
            IPoint point2 = new Point
            {
                X = pt.X + this.Width,
                Y = pt.Y
            };
            ILine lineClass = new Line();
            lineClass.FromPoint = point;
            lineClass.ToPoint = point2;
            IPolyline polyline = new Polyline() as IPolyline;
            object value = Missing.Value;
            (polyline as IPointCollection).AddPoint(point, ref value, ref value);
            (polyline as IPointCollection).AddPoint(point2, ref value, ref value);
            ILineElement lineElement = new LineElement() as ILineElement;
		    ((IElement)lineElement).Geometry = polyline;
		    lineElement.Symbol = this.LineSymbol;
		    return lineElement as IElement;
        }

		public void CreateTable(IActiveView pAV, IPoint Leftdown, object tabcell)
		{
			IPoint y;
			int i;
			this.m_tabcellText = tabcell as SortedList<int, SortedList<int, string>>;
			IGroupElement groupElementClass = new GroupElement() as IGroupElement;
			(groupElementClass as IElementProperties).Name = "表格";
			(groupElementClass as IElementProperties).Type = "表格";
            IEnvelope envelope1 = new Envelope() as IEnvelope;
		    envelope1.XMin = Leftdown.X;
		    envelope1.YMin = Leftdown.Y;
		    envelope1.XMax = Leftdown.X + this.Width;
		    envelope1.YMax = Leftdown.Y + this.Height;
		    IEnvelope envelopeClass = envelope1 as IEnvelope;
			IEnvelope envelope = envelopeClass;
			IFillSymbol simpleFillSymbolClass = new SimpleFillSymbol();
			(simpleFillSymbolClass as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSNull;
            IRectangleElement rectangleElement = new RectangleElement() as IRectangleElement;
		    ((IElement)rectangleElement).Geometry = envelope;
		    ((IFillShapeElement)rectangleElement).Symbol = simpleFillSymbolClass;
		    IRectangleElement rectangleElementClass = rectangleElement as IRectangleElement;
			groupElementClass.AddElement(rectangleElement as IElement);
            IPoint pointClass = new Point() 
			{
				X = Leftdown.X,
				Y = Leftdown.Y + this.Height
			};
			IPoint point = pointClass;
			double height = this.Height / (double)this.RowNumber;
			double width = this.Width / (double)this.ColumnNumber;
			if (this.HasUpperBoundLine)
			{
				groupElementClass.AddElement(this.CreateHLine(point));
			}
			if (this.HasLowerBoundLine)
			{
				groupElementClass.AddElement(this.CreateHLine(Leftdown));
			}
			if (this.HasInnerHorizontalLine)
			{
                IPoint pointClass1 = new Point() 
				{
					X = point.X,
					Y = point.Y - height
				};
				y = pointClass1;
				groupElementClass.AddElement(this.CreateHLine(y));
				if (!this.OnlyFirstHorizontalLine)
				{
					for (i = 0; i < this.RowNumber - 2; i++)
					{
						y.Y = y.Y - height;
						groupElementClass.AddElement(this.CreateHLine(y));
					}
				}
			}
			if (this.HasLeftBoundLine)
			{
				groupElementClass.AddElement(this.CreateVLine(Leftdown));
			}
			if (this.HasRightBoundLine)
			{
                IPoint pointClass2  = new Point() 
				{
					X = Leftdown.X + this.Width,
					Y = Leftdown.Y
				};
				groupElementClass.AddElement(this.CreateVLine(pointClass2));
			}
			if (this.HasInnerVerticalLine)
			{
                IPoint pointClass3  = new Point() 
				{
					X = Leftdown.X + width,
					Y = Leftdown.Y
				};
				y = pointClass3;
				groupElementClass.AddElement(this.CreateVLine(y));
				if (!this.OnlyFirstVerticalLine)
				{
					for (i = 0; i < this.ColumnNumber - 2; i++)
					{
						y.X = y.X + width;
						groupElementClass.AddElement(this.CreateVLine(y));
					}
				}
			}
			 IPoint pointClass4 = new Point() 
			{
				X = point.X,
				Y = point.Y
			};
			IPoint y1 = pointClass4;
			for (i = 0; i < this.RowNumber; i++)
			{
                IPoint pointClass5 = new Point() 
				{
					X = y1.X,
					Y = y1.Y
				};
				IPoint x = pointClass5;
				for (int j = 0; j < this.ColumnNumber; j++)
				{
					string cellText = this.GetCellText(i, j);
					if (cellText.Length > 0)
					{
						IElement element = this.CreateTabTextElement(pAV, cellText, x.X, x.Y - height / 2);
						groupElementClass.AddElement(element);
						this.SetTableCell(i, j, element);
					}
					x.X = x.X + width;
				}
				y1.Y = y1.Y - height;
			}
			this.m_pGroupElement = groupElementClass as IElement;
		}

		private IElement CreateTabTextElement(IActiveView pAV, string text, double x, double y)
		{
            ITextElement textElement = new TextElement() as ITextElement;
		    textElement.Symbol = this.TextSymbol;
		    textElement.Text = text;
		    ITextElement textElementClass = textElement as ITextElement;
			IElement element = textElementClass as IElement;
		    IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
			IEnvelope envelopeClass = new Envelope() as IEnvelope;
			pointClass.PutCoords(x, y);
			element.Geometry = pointClass;
			element.QueryBounds(pAV.ScreenDisplay, envelopeClass);
			return element;
		}

		private IElement CreateVLine(IPoint pt)
		{
			IPoint pointClass = new Point() 
			{
				X = pt.X,
				Y = pt.Y
			};
			IPoint point = pointClass;
            IPoint pointClass1= new Point() 
			{
				X = pt.X,
				Y = pt.Y + this.Height
			};
			IPoint point1 = pointClass1;
			IPolyline polylineClass = new Polyline() as IPolyline;
			object value = Missing.Value;
			(polylineClass as IPointCollection).AddPoint(point, ref value, ref value);
			(polylineClass as IPointCollection).AddPoint(point1, ref value, ref value);
		    ILineElement element = new LineElement() as ILineElement;
		    ((IElement)element).Geometry = polylineClass;
		    element.Symbol = this.LineSymbol;
		    ILineElement lineElementClass = element as ILineElement;
			return lineElementClass as IElement;
		}

		public void Deactivate()
		{
			this.m_pGroupElement.Deactivate();
		}

		public void Draw(IDisplay Display, ITrackCancel TrackCancel)
		{
			this.m_pGroupElement.Draw(Display, TrackCancel);
		}

		private int FindElement(IGroupElement pGroupElement, IElement pElement)
		{
			int num;
			if (pElement != null)
			{
				int num1 = 0;
				while (num1 < pGroupElement.ElementCount)
				{
					if (pGroupElement.Element[num1] != pElement)
					{
						num1++;
					}
					else
					{
						num = num1;
						return num;
					}
				}
				num = -1;
			}
			else
			{
				num = -1;
			}
			return num;
		}

		public IEnumElement get_Graphics(IDisplay Display, object Data)
		{
			return ((this.m_pGroupElement as IClone).Clone() as IGroupElement).Elements;
		}

		public IElement GetCellElement(int row, int col)
		{
			IElement item;
			if (this.m_tabcell.ContainsKey(row))
			{
				SortedList<int, IElement> nums = this.m_tabcell[row];
				if (nums.ContainsKey(col))
				{
					item = nums[col];
					return item;
				}
			}
			item = null;
			return item;
		}

		private string GetCellText(int row, int col)
		{
			string item;
			if (this.m_tabcellText.ContainsKey(row))
			{
				SortedList<int, string> nums = this.m_tabcellText[row];
				if (nums.ContainsKey(col))
				{
					item = nums[col];
					return item;
				}
			}
			item = "";
			return item;
		}

		public void GetClassID(out Guid pClassID)
		{
			pClassID = new Guid("47DEC0C4-097A-425f-8A57-A5508571C9C4");
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
			if (this.m_pGroupElement != null)
			{
				this.m_pGroupElement.QueryOutline(Display, Outline);
			}
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

		public void SetTableCell(int row, int col, object element)
		{
			SortedList<int, IElement> nums;
			if (!this.m_tabcell.ContainsKey(row))
			{
				nums = new SortedList<int, IElement>()
				{
					{ col, element as IElement }
				};
				this.m_tabcell.Add(row, nums);
			}
			else
			{
				nums = this.m_tabcell[row];
				if (!nums.ContainsKey(col))
				{
					nums.Add(col, element as IElement);
				}
				else
				{
					nums[col] = element as IElement;
				}
			}
		}

		public void SetTableCellText(int row, int col, string element)
		{
			SortedList<int, string> nums;
			if (!this.m_tabcellText.ContainsKey(row))
			{
				nums = new SortedList<int, string>()
				{
					{ col, element }
				};
				this.m_tabcellText.Add(row, nums);
			}
			else
			{
				nums = this.m_tabcellText[row];
				if (!nums.ContainsKey(col))
				{
					nums.Add(col, element);
				}
				else
				{
					nums[col] = element;
				}
			}
		}

		public void Transform(esriTransformDirection direction, ITransformation transformation)
		{
			(this.m_pGroupElement as ITransform2D).Transform(direction, transformation);
		}
	}
}