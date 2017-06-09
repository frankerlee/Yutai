using System;
using System.Reflection;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.ExtendClass
{
	/// <summary>
	/// 接图表实现类。
	/// </summary>
	[Guid("22503CDC-88A0-45f7-8D06-65DBC8A6F07E")]
	public class JTBElement : IBoundsProperties, IClone, IElement, IElementProperties, IElementProperties2, IGraphicElement, ITransform2D, IJTBElement, IPersistStream, IPersist, IDocumentVersionSupportGEN
	{
		private IDisplay m_pCachedDisplay = null;

		private ISelectionTracker m_pSelectionTracker = null;

		private IFillSymbol m_pFillSymbol1;

		private IFillSymbol m_pFillSymbol2;

		private bool m_AutoTransform = false;

		private IEnvelope m_pGeometry = null;

		private string m_sElementName = "JTBElement";

		private string m_sElementType = "JTBElement";

		private object m_vCustomProperty = null;

		private bool m_bLocked = false;

		private ITextSymbol m_pTextSym = null;

		private ILineSymbol m_pLineSymbol = null;

		private string m_LeftTopName = "";

		private string m_TopName = "";

		private string m_RightTopName = "";

		private string m_LeftName = "";

		private string m_RightName = "";

		private string m_LeftBottomName = "";

		private string m_BottomName = "";

		private string m_RightBottomName = "";

		private bool m_FixedAspectRatio = true;

		private IEnvelope m_pTempEnvelop = new Envelope() as IEnvelope;

		private IPointCollection m_pTempPolyline = new Polyline();

		private string m_TFName = "";

		/// <summary>
		/// </summary>
		public bool AutoTransform
		{
			get
			{
				return this.m_AutoTransform;
			}
			set
			{
				this.m_AutoTransform = value;
			}
		}

		/// <summary>
		/// </summary>
		public string BottomName
		{
			get
			{
				return this.m_BottomName;
			}
			set
			{
				this.m_BottomName = value;
			}
		}

		/// <summary>
		/// </summary>
		public object CustomProperty
		{
			get
			{
				return this.m_vCustomProperty;
			}
			set
			{
				this.m_vCustomProperty = value;
			}
		}

		/// <summary>
		/// </summary>
		public bool FixedAspectRatio
		{
			get
			{
				return this.m_FixedAspectRatio;
			}
			set
			{
				this.m_FixedAspectRatio = value;
			}
		}

		/// <summary>
		/// 获取石否固定尺寸。
		/// </summary>
		public bool FixedSize
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// </summary>
		public IGeometry Geometry
		{
			get
			{
				return this.m_pGeometry;
			}
			set
			{
				if (!(value is IPoint))
				{
					this.m_pGeometry = value.Envelope;
				}
				else
				{
					this.m_pGeometry.CenterAt(value as IPoint);
				}
			}
		}

		/// <summary>
		/// </summary>
		public UID ID
		{
			get
			{
				return new UID()
				{
					Value = "{BA816142-C912-4F52-A3AF-846CCC5326BD}"
				};
			}
		}

		/// <summary>
		/// </summary>
		public string LeftBottomName
		{
			get
			{
				return this.m_LeftBottomName;
			}
			set
			{
				this.m_LeftBottomName = value;
			}
		}

		/// <summary>
		/// </summary>
		public string LeftName
		{
			get
			{
				return this.m_LeftName;
			}
			set
			{
				this.m_LeftName = value;
			}
		}

		/// <summary>
		/// </summary>
		public string LeftTopName
		{
			get
			{
				return this.m_LeftTopName;
			}
			set
			{
				this.m_LeftTopName = value;
			}
		}

		/// <summary>
		/// </summary>
		public ILineSymbol LineSymbol
		{
			get
			{
				return this.m_pLineSymbol;
			}
			set
			{
				this.m_pLineSymbol = value;
			}
		}

		/// <summary>
		/// </summary>
		public bool Locked
		{
			get
			{
				return this.m_bLocked;
			}
			set
			{
				this.m_bLocked = value;
			}
		}

		/// <summary>
		/// </summary>
		public string Name
		{
			get
			{
				return this.m_sElementName;
			}
			set
			{
				this.m_sElementName = value;
			}
		}

		/// <summary>
		/// </summary>
		internal IPropertySet PropertySet
		{
			get
			{
				IPropertySet propertySetClass = new PropertySet();
				propertySetClass.SetProperty("Geometry", this.m_pGeometry);
				propertySetClass.SetProperty("CustomProperty", this.m_vCustomProperty);
				propertySetClass.SetProperty("Locked", this.m_bLocked);
				propertySetClass.SetProperty("LineSymbol", this.m_pLineSymbol);
				propertySetClass.SetProperty("TextSymbol", this.m_pTextSym);
				propertySetClass.SetProperty("LeftBottomName", this.m_LeftBottomName);
				propertySetClass.SetProperty("RightBottomName", this.m_RightBottomName);
				propertySetClass.SetProperty("BottomName", this.m_BottomName);
				propertySetClass.SetProperty("LeftTopName", this.m_LeftTopName);
				propertySetClass.SetProperty("RightTopName", this.m_RightTopName);
				propertySetClass.SetProperty("TopName", this.m_TopName);
				propertySetClass.SetProperty("LeftName", this.m_LeftName);
				propertySetClass.SetProperty("RightName", this.m_RightName);
				propertySetClass.SetProperty("ElementName", this.m_sElementName);
				propertySetClass.SetProperty("ElementType", this.m_sElementType);
				propertySetClass.SetProperty("TFName", this.m_TFName);
				return propertySetClass;
			}
			set
			{
				IPropertySet propertySet = value;
				this.m_pGeometry = propertySet.GetProperty("Geometry") as IEnvelope;
				this.m_vCustomProperty = propertySet.GetProperty("CustomProperty");
				this.m_bLocked = (bool)propertySet.GetProperty("Locked");
				this.m_pLineSymbol = propertySet.GetProperty("LineSymbol") as ILineSymbol;
				this.m_pTextSym = propertySet.GetProperty("TextSymbol") as ITextSymbol;
				this.m_LeftBottomName = propertySet.GetProperty("LeftBottomName").ToString();
				this.m_RightBottomName = propertySet.GetProperty("RightBottomName").ToString();
				this.m_BottomName = propertySet.GetProperty("BottomName").ToString();
				this.m_LeftTopName = propertySet.GetProperty("LeftTopName").ToString();
				this.m_RightTopName = propertySet.GetProperty("RightTopName").ToString();
				this.m_TopName = propertySet.GetProperty("TopName").ToString();
				this.m_LeftName = propertySet.GetProperty("LeftName").ToString();
				this.m_RightName = propertySet.GetProperty("RightName").ToString();
				this.m_sElementName = propertySet.GetProperty("ElementName").ToString();
				this.m_sElementType = propertySet.GetProperty("ElementType").ToString();
				this.m_TFName = propertySet.GetProperty("TFName").ToString();
			}
		}

		/// <summary>
		/// </summary>
		public double ReferenceScale
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		/// <summary>
		/// </summary>
		public string RightBottomName
		{
			get
			{
				return this.m_RightBottomName;
			}
			set
			{
				this.m_RightBottomName = value;
			}
		}

		/// <summary>
		/// </summary>
		public string RightName
		{
			get
			{
				return this.m_RightName;
			}
			set
			{
				this.m_RightName = value;
			}
		}

		/// <summary>
		/// </summary>
		public string RightTopName
		{
			get
			{
				return this.m_RightTopName;
			}
			set
			{
				this.m_RightTopName = value;
			}
		}

		/// <summary>
		/// </summary>
		public ISelectionTracker SelectionTracker
		{
			get
			{
				return this.m_pSelectionTracker;
			}
		}

		/// <summary>
		/// </summary>
		public ISpatialReference SpatialReference
		{
			get
			{
				return this.m_pGeometry.SpatialReference;
			}
			set
			{
				this.m_pGeometry.SpatialReference = value;
			}
		}

		/// <summary>
		/// </summary>
		protected object State
		{
			get
			{
				object obj;
				IPropertySet propertySetClass = new PropertySet();
				propertySetClass.SetProperty("Geometry", this.m_pGeometry);
				propertySetClass.SetProperty("LineSymbol", this.m_pLineSymbol);
				propertySetClass.SetProperty("FillSymbol1", this.m_pFillSymbol1);
				propertySetClass.SetProperty("FillSymbol2", this.m_pFillSymbol2);
				propertySetClass.SetProperty("TextSymbol", this.m_pTextSym);
				propertySetClass.SetProperty("AutoTransform", this.m_AutoTransform);
				propertySetClass.SetProperty("Locked", this.m_bLocked);
				propertySetClass.SetProperty("FixedAspectRatio", this.m_FixedAspectRatio);
				propertySetClass.SetProperty("ElementName", this.m_sElementName);
				propertySetClass.SetProperty("ElementType", this.m_sElementType);
				propertySetClass.SetProperty("CustomProperty", this.m_vCustomProperty);
				propertySetClass.SetProperty("LeftBottomName", this.m_LeftBottomName);
				propertySetClass.SetProperty("RightBottomName", this.m_RightBottomName);
				propertySetClass.SetProperty("BottomName", this.m_BottomName);
				propertySetClass.SetProperty("LeftTopName", this.m_LeftTopName);
				propertySetClass.SetProperty("RightTopName", this.m_RightTopName);
				propertySetClass.SetProperty("TopName", this.m_TopName);
				propertySetClass.SetProperty("LeftName", this.m_LeftName);
				propertySetClass.SetProperty("RightName", this.m_RightName);
				propertySetClass.SetProperty("TFName", this.m_TFName);
				IMemoryBlobStream memoryBlobStreamClass = new MemoryBlobStream();
				((IPersistStream)propertySetClass).Save(memoryBlobStreamClass, 1);
				((IMemoryBlobStreamVariant)memoryBlobStreamClass).ExportToVariant(out obj);
				return obj;
			}
			set
			{
				IPropertySet propertySetClass = new PropertySet();
				IMemoryBlobStream memoryBlobStreamClass = new MemoryBlobStream();
				((IMemoryBlobStreamVariant)memoryBlobStreamClass).ImportFromVariant(value);
				(propertySetClass as IPersistStream).Load(memoryBlobStreamClass);
				this.m_pGeometry = propertySetClass.GetProperty("Geometry") as IEnvelope;
				this.m_pLineSymbol = propertySetClass.GetProperty("LineSymbol") as ILineSymbol;
				this.m_pFillSymbol1 = propertySetClass.GetProperty("FillSymbol1") as IFillSymbol;
				this.m_pFillSymbol2 = propertySetClass.GetProperty("FillSymbol2") as IFillSymbol;
				this.m_pTextSym = propertySetClass.GetProperty("TextSymbol") as ITextSymbol;
				this.m_AutoTransform = (bool)propertySetClass.GetProperty("AutoTransform");
				this.m_bLocked = (bool)propertySetClass.GetProperty("Locked");
				this.m_FixedAspectRatio = (bool)propertySetClass.GetProperty("FixedAspectRatio");
				this.m_sElementName = propertySetClass.GetProperty("ElementName") as string;
				this.m_sElementType = propertySetClass.GetProperty("ElementType") as string;
				this.m_vCustomProperty = propertySetClass.GetProperty("CustomProperty");
				this.m_LeftBottomName = propertySetClass.GetProperty("LeftBottomName") as string;
				this.m_RightBottomName = propertySetClass.GetProperty("RightBottomName") as string;
				this.m_BottomName = propertySetClass.GetProperty("BottomName") as string;
				this.m_LeftTopName = propertySetClass.GetProperty("LeftTopName") as string;
				this.m_RightTopName = propertySetClass.GetProperty("RightTopName") as string;
				this.m_TopName = propertySetClass.GetProperty("TopName") as string;
				this.m_LeftName = propertySetClass.GetProperty("LeftName") as string;
				this.m_RightName = propertySetClass.GetProperty("RightName") as string;
				this.m_TFName = propertySetClass.GetProperty("TFName").ToString();
			}
		}

		/// <summary>
		/// </summary>
		public ITextSymbol TextSymbol
		{
			get
			{
				return this.m_pTextSym;
			}
			set
			{
				this.m_pTextSym = value;
			}
		}

		/// <summary>
		/// </summary>
		public string TFName
		{
			get
			{
				return this.m_TFName;
			}
			set
			{
				this.m_TFName = value;
			}
		}

		/// <summary>
		/// </summary>
		public string TopName
		{
			get
			{
				return this.m_TopName;
			}
			set
			{
				this.m_TopName = value;
			}
		}

		/// <summary>
		/// </summary>
		public string Type
		{
			get
			{
				return this.m_sElementType;
			}
			set
			{
				this.m_sElementType = value;
			}
		}

		/// <summary>
		/// </summary>
		public JTBElement()
		{
			this.m_pGeometry = new Envelope() as IEnvelope;
			this.m_pGeometry.PutCoords(4, 4, 8.5, 6.4);
			this.m_pSelectionTracker = new PolygonTracker()
			{
				Locked = true,
				ShowHandles = true
			};
			this.m_pTextSym = new TextSymbol()
			{
				Size = 12
			};
			this.m_pFillSymbol1 = new SimpleFillSymbol();
			(this.m_pFillSymbol1 as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSNull;
			this.m_pFillSymbol2 = new SimpleFillSymbol();
			(this.m_pFillSymbol2 as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSBackwardDiagonal;
			this.m_pLineSymbol = new SimpleLineSymbol();
		}

		/// <summary>
		/// </summary>
		/// <param name="Display"></param>
		public void Activate(IDisplay Display)
		{
			this.m_pCachedDisplay = Display;
			this.m_pSelectionTracker.Display = this.m_pCachedDisplay as IScreenDisplay;
			this.RefreshTracker();
		}

		/// <summary>
		/// </summary>
		/// <param name="src"></param>
		public void Assign(IClone src)
		{
			src = this;
		}

		/// <summary>
		/// </summary>
		/// <returns></returns>
		public bool CanRotate()
		{
			return false;
		}

		/// <summary>
		/// </summary>
		/// <returns></returns>
		public IClone Clone()
		{
			return new JTBElement()
			{
				State = this.State
			};
		}

		public object ConvertToSupportedObject(esriArcGISVersion docVersion)
		{
			return new GroupElement();
		}

		/// <summary>
		/// </summary>
		public void Deactivate()
		{
		}

		/// <summary>
		/// </summary>
		/// <param name="Display"></param>
		/// <param name="trackCancel"></param>
		public void Draw(IDisplay Display, ITrackCancel trackCancel)
		{
			Display.SetSymbol(null);
			Display.SetSymbol(this.m_pFillSymbol1 as ISymbol);
			Display.DrawRectangle(this.m_pGeometry);
			double width = this.m_pGeometry.Width / 3;
			double height = this.m_pGeometry.Height / 3;
			this.m_pTempEnvelop.PutCoords(this.m_pGeometry.XMin + width, this.m_pGeometry.YMin + height, this.m_pGeometry.XMin + width + width, this.m_pGeometry.YMin + height + height);
			Display.SetSymbol(null);
			Display.SetSymbol(this.m_pFillSymbol2 as ISymbol);
			Display.DrawRectangle(this.m_pTempEnvelop);
			this.m_pTempPolyline = new Polyline();
		    IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
			object value = Missing.Value;
			pointClass.PutCoords(this.m_pGeometry.XMin, this.m_pGeometry.YMin + height);
			this.m_pTempPolyline.AddPoint(pointClass, ref value, ref value);
		    pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(this.m_pGeometry.XMax, this.m_pGeometry.YMin + height);
			this.m_pTempPolyline.AddPoint(pointClass, ref value, ref value);
			Display.SetSymbol(null);
			Display.SetSymbol(this.m_pLineSymbol as ISymbol);
			Display.DrawPolyline(this.m_pTempPolyline as IGeometry);
			this.m_pTempPolyline = new Polyline();
		    pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(this.m_pGeometry.XMin, this.m_pGeometry.YMin + height + height);
			this.m_pTempPolyline.AddPoint(pointClass, ref value, ref value);
		    pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(this.m_pGeometry.XMax, this.m_pGeometry.YMin + height + height);
			this.m_pTempPolyline.AddPoint(pointClass, ref value, ref value);
			Display.DrawPolyline(this.m_pTempPolyline as IGeometry);
			this.m_pTempPolyline = new Polyline();
		    pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(this.m_pGeometry.XMin + width, this.m_pGeometry.YMin);
			this.m_pTempPolyline.AddPoint(pointClass, ref value, ref value);
		    pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(this.m_pGeometry.XMin + width, this.m_pGeometry.YMax);
			this.m_pTempPolyline.AddPoint(pointClass, ref value, ref value);
			Display.DrawPolyline(this.m_pTempPolyline as IGeometry);
			this.m_pTempPolyline = new Polyline();
		    pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(this.m_pGeometry.XMin + width + width, this.m_pGeometry.YMin);
			this.m_pTempPolyline.AddPoint(pointClass, ref value, ref value);
		    pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(this.m_pGeometry.XMin + width + width, this.m_pGeometry.YMax);
			this.m_pTempPolyline.AddPoint(pointClass, ref value, ref value);
			Display.DrawPolyline(this.m_pTempPolyline as IGeometry);
			Display.SetSymbol(null);
			Display.SetSymbol(this.m_pTextSym as ISymbol);
		    pointClass = new ESRI.ArcGIS.Geometry.Point();
			if ((this.m_LeftName == null ? false : this.LeftName.Length > 0))
			{
				pointClass.PutCoords(this.m_pGeometry.XMin + width / 2, this.m_pGeometry.YMin + height + height / 2);
				Display.DrawText(pointClass, this.m_LeftName);
			}
			if ((this.m_LeftTopName == null ? false : this.m_LeftTopName.Length > 0))
			{
				pointClass.PutCoords(this.m_pGeometry.XMin + width / 2, this.m_pGeometry.YMin + height + height + height / 2);
				Display.DrawText(pointClass, this.m_LeftTopName);
			}
			if ((this.m_LeftBottomName == null ? false : this.m_LeftBottomName.Length > 0))
			{
				pointClass.PutCoords(this.m_pGeometry.XMin + width / 2, this.m_pGeometry.YMin + height / 2);
				Display.DrawText(pointClass, this.m_LeftBottomName);
			}
			if ((this.m_RightName == null ? false : this.m_RightName.Length > 0))
			{
				pointClass.PutCoords(this.m_pGeometry.XMax - width / 2, this.m_pGeometry.YMin + height + height / 2);
				Display.DrawText(pointClass, this.m_RightName);
			}
			if ((this.m_RightTopName == null ? false : this.m_RightTopName.Length > 0))
			{
				pointClass.PutCoords(this.m_pGeometry.XMax - width / 2, this.m_pGeometry.YMax - height / 2);
				Display.DrawText(pointClass, this.m_RightTopName);
			}
			if ((this.m_RightBottomName == null ? false : this.m_RightBottomName.Length > 0))
			{
				pointClass.PutCoords(this.m_pGeometry.XMax - width / 2, this.m_pGeometry.YMin + height / 2);
				Display.DrawText(pointClass, this.m_RightBottomName);
			}
			if ((this.m_TopName == null ? false : this.m_TopName.Length > 0))
			{
				pointClass.PutCoords(this.m_pGeometry.XMin + width + width / 2, this.m_pGeometry.YMax - height / 2);
				Display.DrawText(pointClass, this.m_TopName);
			}
			if ((this.m_BottomName == null ? false : this.m_BottomName.Length > 0))
			{
				pointClass.PutCoords(this.m_pGeometry.XMin + width + width / 2, this.m_pGeometry.YMin + height / 2);
				Display.DrawText(pointClass, this.m_BottomName);
			}
			Display.SetSymbol(null);
		}

		/// <summary>
		/// </summary>
		/// <param name="pClassID"></param>
		public void GetClassID(out Guid pClassID)
		{
			pClassID = new Guid("22503CDC-88A0-45f7-8D06-65DBC8A6F07E");
		}

		/// <summary>
		/// </summary>
		/// <param name="pcbSize"></param>
		public void GetSizeMax(out _ULARGE_INTEGER pcbSize)
		{
			(new PropertySet() as IPersistStream).GetSizeMax(out pcbSize);
		}

		/// <summary>
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="Tolerance"></param>
		/// <returns></returns>
		public bool HitTest(double x, double y, double Tolerance)
		{
		    IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
			pointClass.PutCoords(x, y);
			IRelationalOperator polygonClass = new Polygon() as IRelationalOperator;
			this.QueryOutline(this.m_pCachedDisplay, polygonClass as IPolygon);
			return !polygonClass.Disjoint(pointClass);
		}

		/// <summary>
		/// </summary>
		public void IsDirty()
		{
		}

		/// <summary>
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool IsEqual(IClone other)
		{
			bool flag;
			if (!(other is JTBElement))
			{
				flag = false;
			}
			else
			{
				System.Array state = (System.Array)this.State;
				System.Array arrays = (System.Array)(other as JTBElement).State;
				if (arrays.Length == state.Length)
				{
					int num = 0;
					while (num < state.Length)
					{
						if (state.GetValue(num) == arrays.GetValue(num))
						{
							num++;
						}
						else
						{
							flag = false;
							return flag;
						}
					}
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		/// <summary>
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool IsIdentical(IClone other)
		{
			return (other != this ? false : true);
		}

		public bool IsSupportedAtVersion(esriArcGISVersion docVersion)
		{
			return (esriArcGISVersion.esriArcGISVersion83 != docVersion ? true : false);
		}

		/// <summary>
		/// </summary>
		/// <param name="Stream"></param>
		public void Load(IVariantStream Stream)
		{
			this.m_pGeometry = Stream.Read() as IEnvelope;
			this.m_pLineSymbol = Stream.Read() as ILineSymbol;
			this.m_pTextSym = Stream.Read() as ITextSymbol;
			this.m_LeftBottomName = Stream.Read() as string;
			this.m_RightBottomName = Stream.Read() as string;
			this.m_BottomName = Stream.Read() as string;
			this.m_LeftTopName = Stream.Read() as string;
			this.m_RightTopName = Stream.Read() as string;
			this.m_TopName = Stream.Read() as string;
			this.m_LeftName = Stream.Read() as string;
			this.m_RightName = Stream.Read() as string;
			this.m_sElementName = Stream.Read() as string;
			this.m_sElementType = Stream.Read() as string;
			this.m_bLocked = (bool)Stream.Read();
			this.m_vCustomProperty = Stream.Read();
		}

		/// <summary>
		/// </summary>
		/// <param name="pstm"></param>
		public void Load(IStream pstm)
		{
			IPropertySet propertySetClass = new PropertySet();
			(propertySetClass as IPersistStream).Load(pstm);
			this.PropertySet = propertySetClass;
		}

		/// <summary>
		/// </summary>
		/// <param name="dx"></param>
		/// <param name="dy"></param>
		public void Move(double dx, double dy)
		{
			(this.m_pGeometry as ITransform2D).Move(dx, dy);
			this.RefreshTracker();
		}

		/// <summary>
		/// </summary>
		/// <param name="v"></param>
		public void MoveVector(ILine v)
		{
			(this.m_pGeometry as ITransform2D).MoveVector(v);
			this.RefreshTracker();
		}

		private string NewElementName()
		{
			return "";
		}

		/// <summary>
		/// </summary>
		/// <param name="Display"></param>
		/// <param name="Bounds"></param>
		public void QueryBounds(IDisplay Display, IEnvelope Bounds)
		{
			IGeometry polygonClass = new Polygon() as IGeometry;
			this.QueryOutline(this.m_pCachedDisplay, polygonClass as IPolygon);
			polygonClass.QueryEnvelope(Bounds);
		}

		/// <summary>
		/// </summary>
		/// <param name="Display"></param>
		/// <param name="Outline"></param>
		public void QueryOutline(IDisplay Display, IPolygon Outline)
		{
			IDisplayTransformation displayTransformation = this.m_pCachedDisplay.DisplayTransformation;
			Outline.SetEmpty();
			ISymbol mPFillSymbol1 = this.m_pFillSymbol1 as ISymbol;
			mPFillSymbol1.QueryBoundary(Display.hDC, displayTransformation, this.m_pGeometry, Outline);
		}

		private void RefreshTracker()
		{
			if (this.m_pCachedDisplay != null)
			{
				IGeometry polygonClass = new Polygon() as IGeometry;
				this.QueryOutline(this.m_pCachedDisplay, polygonClass as IPolygon);
				this.m_pSelectionTracker.Geometry = polygonClass;
			}
		}

		/// <summary>
		/// </summary>
		/// <param name="Origin"></param>
		/// <param name="rotationAngle"></param>
		public void Rotate(IPoint Origin, double rotationAngle)
		{
			(this.m_pGeometry as ITransform2D).Rotate(Origin, rotationAngle);
			this.RefreshTracker();
		}

		/// <summary>
		/// </summary>
		/// <param name="Stream"></param>
		public void Save(IVariantStream Stream)
		{
			Stream.Write(this.m_pGeometry);
			Stream.Write(this.m_pLineSymbol);
			Stream.Write(this.m_pTextSym);
			Stream.Write(this.m_LeftBottomName);
			Stream.Write(this.m_RightBottomName);
			Stream.Write(this.m_BottomName);
			Stream.Write(this.m_LeftTopName);
			Stream.Write(this.m_RightTopName);
			Stream.Write(this.m_TopName);
			Stream.Write(this.m_LeftName);
			Stream.Write(this.m_RightName);
			Stream.Write(this.m_sElementName);
			Stream.Write(this.m_sElementType);
			Stream.Write(this.m_bLocked);
			Stream.Write(this.m_vCustomProperty);
		}

		/// <summary>
		/// </summary>
		/// <param name="pstm"></param>
		/// <param name="fClearDirty"></param>
		public void Save(IStream pstm, int fClearDirty)
		{
			(this.PropertySet as IPersistStream).Save(pstm, fClearDirty);
		}

		/// <summary>
		/// </summary>
		/// <param name="Origin"></param>
		/// <param name="sx"></param>
		/// <param name="sy"></param>
		public void Scale(IPoint Origin, double sx, double sy)
		{
			(this.m_pGeometry as ITransform2D).Scale(Origin, sx, sy);
			this.RefreshTracker();
		}

		/// <summary>
		/// </summary>
		/// <param name="direction"></param>
		/// <param name="transformation"></param>
		public void Transform(esriTransformDirection direction, ITransformation transformation)
		{
			(this.m_pGeometry as ITransform2D).Transform(direction, transformation);
			this.RefreshTracker();
		}
	}
}