using System;
using System.Reflection;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using OLECilentLib;
using _ULARGE_INTEGER = ESRI.ArcGIS.esriSystem._ULARGE_INTEGER;

namespace Yutai.ArcGIS.Common.ExtendClass
{
	/// <summary>
	/// OLE对象元素类
	/// </summary>
	[Guid("33C44881-30B6-43d8-B56E-38B00011F9D2")]
	public class OleFrame : IOleFrame, IElement, IClone, ITransform2D, IElementProperties, IElementProperties2, IBoundsProperties, IPersistStream, IPersist
	{
		private IDisplay m_pCachedDisplay = null;

		private IOLEClientEx m_oleclient = null;

		private IEnvelope m_pGeometry = null;

		private ISelectionTracker m_pSelectionTracker = null;

		private bool m_bLocked = false;

		private object m_pCustomProperty = null;

		private string m_sElementName = "OleFrame";

		private string m_sElementType = "OldFrame";

		private IFillSymbol m_pFillSymbol1;

		private double m_OldObjectX = 0;

		private double m_OldObjectY = 0;

		private double m_radio = 1;

		private int m_hWnd = 0;

		private bool m_FixedAspectRatio = true;

		/// <summary>
		/// </summary>
		public bool AutoTransform
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		/// <summary>
		/// </summary>
		public object CustomProperty
		{
			get
			{
				return this.m_pCustomProperty;
			}
			set
			{
				this.m_pCustomProperty = value;
			}
		}

		/// <summary>
		/// </summary>
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

		/// <summary>
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
				this.m_pGeometry = value.Envelope;
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
				System.Runtime.InteropServices.ComTypes.IStream stream = null;
				ComStream.CreateStreamOnHGlobal(0, false, out stream);
				this.m_oleclient.Save((OLECilentLib.IStream)stream, 1);
				ComStream comStream = new ComStream(ref stream);
				byte[] numArray = new byte[comStream.Length];
				comStream.Position = (long)0;
				comStream.Read(numArray, 0, (int)comStream.Length);
				propertySetClass.SetProperty("OleClient1", numArray);
				propertySetClass.SetProperty("Geometry", this.m_pGeometry);
				propertySetClass.SetProperty("FillSymbol", this.m_pFillSymbol1);
				propertySetClass.SetProperty("ElementName", this.m_sElementName);
				propertySetClass.SetProperty("ElementType", this.m_sElementType);
				propertySetClass.SetProperty("CustomProperty", this.m_pCustomProperty);
				propertySetClass.SetProperty("Locked", this.m_bLocked);
				return propertySetClass;
			}
			set
			{
				IPropertySet propertySet = value;
				byte[] property = propertySet.GetProperty("OleClient1") as byte[];
				System.Runtime.InteropServices.ComTypes.IStream stream = null;
				ComStream.CreateStreamOnHGlobal(0, false, out stream);
				ComStream comStream = new ComStream(ref stream)
				{
					Position = (long)0
				};
				comStream.Write(property, 0, (int)property.Length);
				this.m_oleclient.Load((OLECilentLib.IStream)stream);
				this.m_pGeometry = propertySet.GetProperty("Geometry") as IEnvelope;
				this.m_pFillSymbol1 = propertySet.GetProperty("FillSymbol") as IFillSymbol;
				this.m_sElementName = propertySet.GetProperty("ElementName").ToString();
				this.m_sElementType = propertySet.GetProperty("ElementType").ToString();
				this.m_pCustomProperty = propertySet.GetProperty("CustomProperty");
				this.m_bLocked = (bool)propertySet.GetProperty("Locked");
				if (this.m_pGeometry.Width != this.m_pGeometry.Height)
				{
					this.m_radio = this.m_pGeometry.Width / this.m_pGeometry.Height;
				}
				else
				{
					this.m_radio = 1;
				}
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
		public ISelectionTracker SelectionTracker
		{
			get
			{
				return this.m_pSelectionTracker;
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
		public OleFrame()
		{
			this.m_oleclient = new OLEClientEx();
			this.m_pSelectionTracker = new EnvelopeTracker()
			{
				Locked = false,
				ShowHandles = true
			};
			this.m_pGeometry = new Envelope() as IEnvelope;
			this.m_pGeometry.PutCoords(4, 4, 18.5, 16.4);
			this.m_pFillSymbol1 = new SimpleFillSymbol();
			(this.m_pFillSymbol1 as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSNull;
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
		/// Required method for ArcGIS Component Category registration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryRegistration(System.Type registerType)
		{
			string str = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			MxCommands.Register(str);
		}

		/// <summary>
		/// Required method for ArcGIS Component Category unregistration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryUnregistration(System.Type registerType)
		{
			string str = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			MxCommands.Unregister(str);
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
			return new OleFrame()
			{
				PropertySet = this.PropertySet
			};
		}

        /// <summary>
        /// 创建OLE对象元素
        /// </summary>
        /// <param name="Display">屏幕显示对象</param>
        /// <param name="hWnd">窗口句柄</param>
        /// <returns>成功true</returns>
        public bool CreateOleClientItem(IDisplay Display, int hWnd)
        {
            this.m_hWnd = hWnd;
            bool result;
            if (!this.m_oleclient.Create(hWnd))
            {
                result = false;
            }
            else
            {
                int num;
                int num2;
                this.m_oleclient.GetSize(out num, out num2);
                if (num >= 0 && num2 >= 0)
                {
                    IPoint point = Display.DisplayTransformation.ToMapPoint(num, num2);
                    point.X = Math.Abs(point.X);
                    point.Y = Math.Abs(point.Y);
                    this.m_OldObjectX = point.X;
                    this.m_OldObjectY = point.Y;
                    this.m_radio = this.m_OldObjectX / this.m_OldObjectY;
                    if (num == num2)
                    {
                        if (this.m_OldObjectY > 14.0)
                        {
                            this.m_pGeometry.PutCoords(4.0, 4.0, 18.0, 18.0);
                        }
                        else
                        {
                            this.m_pGeometry.PutCoords(4.0, 4.0, 4.0 + this.m_OldObjectY, 4.0 + this.m_OldObjectY);
                        }
                    }
                    else if (num > num2)
                    {
                        double num3;
                        if (this.m_OldObjectX > 14.0)
                        {
                            num3 = 14.0;
                        }
                        else
                        {
                            num3 = this.m_OldObjectX;
                        }
                        double num4 = num3 / this.m_radio;
                        this.m_pGeometry.PutCoords(4.0, 4.0, 4.0 + num3, 4.0 + num4);
                    }
                    else if (num < num2)
                    {
                        double num4;
                        if (this.m_OldObjectY > 14.0)
                        {
                            num4 = 14.0;
                        }
                        else
                        {
                            num4 = this.m_OldObjectY;
                        }
                        double num3 = num4 * this.m_radio;
                        this.m_pGeometry.PutCoords(4.0, 4.0, 4.0 + num3, 4.0 + num4);
                    }
                }
                result = true;
            }
            return result;
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
			int num;
			int num1;
			int num2;
			int num3;
			IPoint pointClass = new Point();
			pointClass.PutCoords(this.m_pGeometry.XMin, this.m_pGeometry.YMin);
			Display.DisplayTransformation.FromMapPoint(pointClass, out num, out num1);
			pointClass.PutCoords(this.m_pGeometry.XMax, this.m_pGeometry.YMax);
			Display.DisplayTransformation.FromMapPoint(pointClass, out num2, out num3);
			double mRadio = (double)(num2 - num);
			double mRadio1 = (double)(num1 - num3);
			if (mRadio < mRadio1)
			{
				mRadio1 = mRadio / this.m_radio;
			}
			else if (mRadio > mRadio1)
			{
				mRadio = mRadio1 * this.m_radio;
			}
			this.m_oleclient.Draw(Display.hDC, (double)num, (double)num3, (double)num + mRadio, (double)num3 + mRadio1);
		}

		/// <summary>
		/// </summary>
		/// <param name="hWnd"></param>
		public void Edit(int hWnd)
		{
		}

		/// <summary>
		/// </summary>
		/// <param name="pClassID"></param>
		public void GetClassID(out Guid pClassID)
		{
			pClassID = new Guid("33C44881-30B6-43d8-B56E-38B00011F9D2");
		}

		/// <summary>
		/// </summary>
		/// <param name="pcbSize"></param>
		public void GetSizeMax(out _ULARGE_INTEGER pcbSize)
		{
			(this.PropertySet as IPersistStream).GetSizeMax(out pcbSize);
		}

		/// <summary>
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="Tolerance"></param>
		/// <returns></returns>
		public bool HitTest(double x, double y, double Tolerance)
		{
			IPoint pointClass = new Point();
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
			flag = (!(other is OleFrame) ? false : this.PropertySet.IsEqual((other as OleFrame).PropertySet));
			return flag;
		}

		/// <summary>
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool IsIdentical(IClone other)
		{
			return other == this;
		}

		/// <summary>
		/// </summary>
		/// <param name="pstm"></param>
		public void Load(ESRI.ArcGIS.esriSystem.IStream pstm)
		{
			IPropertySet propertySetClass = new PropertySet();
			try
			{
				(propertySetClass as IPersistStream).Load(pstm);
				this.PropertySet = propertySetClass;
			}
			catch (Exception exception)
			{
			}
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

		private void OleFrame_OnOLESave()
		{
			if (this.OLEEditComplete != null)
			{
				this.OLEEditComplete(this);
			}
		}

		/// <summary>
		/// </summary>
		/// <param name="Display"></param>
		/// <param name="Bounds"></param>
		public void QueryBounds(IDisplay Display, IEnvelope Bounds)
		{
			IPolygon polygonClass = new Polygon() as IPolygon;
			polygonClass.SetEmpty();
			((ISymbol)this.m_pFillSymbol1).QueryBoundary(Display.hDC, Display.DisplayTransformation, this.m_pGeometry, polygonClass);
			Bounds.XMin = polygonClass.Envelope.XMin;
			Bounds.XMax = polygonClass.Envelope.XMax;
			Bounds.YMin = polygonClass.Envelope.YMin;
			Bounds.YMax = polygonClass.Envelope.YMax;
			Bounds.SpatialReference = polygonClass.Envelope.SpatialReference;
		}

		/// <summary>
		/// </summary>
		/// <param name="Display"></param>
		/// <param name="Outline"></param>
		public void QueryOutline(IDisplay Display, IPolygon Outline)
		{
			IPolygon polygonClass = new Polygon() as IPolygon;
			polygonClass.SetEmpty();
			((ISymbol)this.m_pFillSymbol1).QueryBoundary(Display.hDC, Display.DisplayTransformation, this.m_pGeometry, polygonClass);
			((IPointCollection)Outline).AddPointCollection((IPointCollection)polygonClass);
		}

		private void RefreshTracker()
		{
			if (this.m_pCachedDisplay != null)
			{
				IGeometry polygonClass = new Polygon() as IGeometry;
				this.QueryOutline(this.m_pCachedDisplay, polygonClass as IPolygon);
				this.m_pSelectionTracker.Geometry = polygonClass.Envelope;
			}
		}

		[ComRegisterFunction]
		[ComVisible(false)]
		private static void RegisterFunction(System.Type registerType)
		{
			OleFrame.ArcGISCategoryRegistration(registerType);
		}

		/// <summary>
		/// </summary>
		/// <param name="Origin"></param>
		/// <param name="rotationAngle"></param>
		public void Rotate(IPoint Origin, double rotationAngle)
		{
		}

		/// <summary>
		/// </summary>
		/// <param name="pstm"></param>
		/// <param name="fClearDirty"></param>
		public void Save(ESRI.ArcGIS.esriSystem.IStream pstm, int fClearDirty)
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
			object value = Missing.Value;
			IPointCollection polygonClass = new Polygon();
			polygonClass.AddPoint(this.m_pGeometry.LowerLeft, ref value, ref value);
			polygonClass.AddPoint(this.m_pGeometry.LowerRight, ref value, ref value);
			polygonClass.AddPoint(this.m_pGeometry.UpperRight, ref value, ref value);
			polygonClass.AddPoint(this.m_pGeometry.UpperLeft, ref value, ref value);
			(polygonClass as IPolygon).Close();
			(polygonClass as IGeometry).SpatialReference = this.m_pGeometry.SpatialReference;
			(polygonClass as ITransform2D).Transform(direction, transformation);
			this.m_pGeometry = (polygonClass as IPolygon).Envelope;
			this.RefreshTracker();
		}

		[ComUnregisterFunction]
		[ComVisible(false)]
		private static void UnregisterFunction(System.Type registerType)
		{
			OleFrame.ArcGISCategoryUnregistration(registerType);
		}

		public event OLEEditCompleteHandler OLEEditComplete;
	}
}