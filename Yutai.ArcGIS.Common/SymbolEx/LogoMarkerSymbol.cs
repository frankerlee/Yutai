using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.Connection.Local;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.SymbolEx
{
	[ComVisible(true)]
	[Guid("EFEBF67C-2AA9-4182-BE5A-CCFAA6D70D15")]
	[ProgId("JLK.SymbolEx.LogoMarkerSymbol")]
	public class LogoMarkerSymbol : ISymbol, IMarkerSymbol, IDisplayName, IClone, IPersistVariant, ISymbolRotation, IMapLevel, IMarkerMask, IPropertySupport, ILogoMarkerSymbol
	{
		public const string GUID = "JLK.SymbolEx.LogoMarkerSymbol";

		private const string m_sDisplayName = "LogoMarkerSymbol";

		private const int m_lCurrPersistVers = 1;

		private IDisplayTransformation m_trans;

		private esriRasterOpCode m_lROP2Old;

		private int m_lhDC;

		private int m_lOldPen;

		private int m_lOldBrush;

		private int m_lPen;

		private int m_lBrushTop;

		private int m_lBrushLeft;

		private int m_lBrushRight;

		private double m_dDeviceRatio;

		private double m_dDeviceXOffset;

		private double m_dDeviceYOffset;

		private Utility.POINTAPI[] m_coords = new Utility.POINTAPI[7];

		private double m_dDeviceRadius;

		private esriRasterOpCode m_lROP2;

		private double m_dSize;

		private double m_dXOffset;

		private double m_dYOffset;

		private double m_dAngle;

		private bool m_bRotWithTrans;

		private double m_dMapRotation;

		private int m_lMapLevel;

		private IColor m_colorTop;

		private IColor m_colorLeft;

		private IColor m_colorRight;

		private IColor m_colorBorder;

		string ESRI.ArcGIS.Display.IDisplayName.NameString
		{
			get
			{
				return "LogoMarkerSymbol";
			}
		}

		int ESRI.ArcGIS.Display.IMapLevel.MapLevel
		{
			get
			{
				return this.m_lMapLevel;
			}
			set
			{
				this.m_lMapLevel = value;
			}
		}

		double ESRI.ArcGIS.Display.IMarkerSymbol.Angle
		{
			get
			{
				return this.m_dAngle;
			}
			set
			{
				if (value > 360)
				{
					value = value - (double)(Convert.ToInt32(value / 360) * 360);
				}
				this.m_dAngle = value;
			}
		}

		IColor ESRI.ArcGIS.Display.IMarkerSymbol.Color
		{
			get
			{
				return ((IClone)this.m_colorTop).Clone() as IColor;
			}
			set
			{
				this.m_colorTop = (value as IClone).Clone() as IColor;
			}
		}

		double ESRI.ArcGIS.Display.IMarkerSymbol.Size
		{
			get
			{
				return this.m_dSize;
			}
			set
			{
				this.m_dSize = value;
			}
		}

		double ESRI.ArcGIS.Display.IMarkerSymbol.XOffset
		{
			get
			{
				return this.m_dXOffset;
			}
			set
			{
				this.m_dXOffset = value;
			}
		}

		double ESRI.ArcGIS.Display.IMarkerSymbol.YOffset
		{
			get
			{
				return this.m_dYOffset;
			}
			set
			{
				this.m_dYOffset = value;
			}
		}

		esriRasterOpCode ESRI.ArcGIS.Display.ISymbol.ROP2
		{
			get
			{
				return this.m_lROP2;
			}
			set
			{
				if (Convert.ToInt32(value) >= 1)
				{
					this.m_lROP2 = value;
				}
				else
				{
					this.m_lROP2 = esriRasterOpCode.esriROPCopyPen;
				}
			}
		}

		bool ESRI.ArcGIS.Display.ISymbolRotation.RotateWithTransform
		{
			get
			{
				return this.m_bRotWithTrans;
			}
			set
			{
				this.m_bRotWithTrans = value;
			}
		}

		UID ESRI.ArcGIS.esriSystem.IPersistVariant.ID
		{
			get
			{
				return new UID()
				{
					Value = "JLK.SymbolEx.LogoMarkerSymbol"
				};
			}
		}

		IColor ILogoMarkerSymbol.ColorBorder
		{
			get
			{
				return (this.m_colorBorder as IClone).Clone() as IColor;
			}
			set
			{
				this.m_colorBorder = (value as IClone).Clone() as IColor;
			}
		}

		IColor ILogoMarkerSymbol.ColorLeft
		{
			get
			{
				return (this.m_colorLeft as IClone).Clone() as IColor;
			}
			set
			{
				this.m_colorLeft = (value as IClone).Clone() as IColor;
			}
		}

		IColor ILogoMarkerSymbol.ColorRight
		{
			get
			{
				return (this.m_colorRight as IClone).Clone() as IColor;
			}
			set
			{
				this.m_colorRight = (value as IClone).Clone() as IColor;
			}
		}

		IColor ILogoMarkerSymbol.ColorTop
		{
			get
			{
				return (this.m_colorTop as IClone).Clone() as IColor;
			}
			set
			{
				this.m_colorTop = (value as IClone).Clone() as IColor;
			}
		}

		public LogoMarkerSymbol()
		{
			this.Initialize();
		}

		public bool Applies(object pUnk)
		{
			IColor color = pUnk as IColor;
			ILogoMarkerSymbol logoMarkerSymbol = pUnk as ILogoMarkerSymbol;
			return ((color != null ? false : null == logoMarkerSymbol) ? false : true);
		}

		public object Apply(object newObject)
		{
            return null;
			//object current = null;
			//IColor color = newObject as IColor;
			//if (null != color)
			//{
			//	current = ((IPropertySupport)this)[newObject];
			//	((IMarkerSymbol)this).Color = color;
			//}
			//current = ((IPropertySupport)this)[newObject];
			//((IClone)this).Assign((IClone)newObject);
			//return current;
		}

		/// <summary>
		/// Required method for ArcGIS Component Category registration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryRegistration(Type registerType)
		{
			string str = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			MarkerSymbol.Register(str);
		}

		/// <summary>
		/// Required method for ArcGIS Component Category unregistration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryUnregistration(Type registerType)
		{
			string str = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			MarkerSymbol.Unregister(str);
		}

		private void CalcCoords(double x, ref double y)
		{
			this.m_coords[0].x = Convert.ToInt32(x);
			this.m_coords[0].y = Convert.ToInt32(y);
			double num = 0;
			num = Math.Sqrt(this.m_dDeviceRadius * this.m_dDeviceRadius / 2);
			this.m_coords[2].x = Convert.ToInt32(x + this.m_dDeviceXOffset);
			this.m_coords[2].y = Convert.ToInt32(y - this.m_dDeviceYOffset);
			this.m_coords[1].x = this.m_coords[2].x - Convert.ToInt32(num);
			this.m_coords[1].y = this.m_coords[2].y - Convert.ToInt32(num);
			this.m_coords[4].x = this.m_coords[2].x + Convert.ToInt32(num);
			this.m_coords[4].y = this.m_coords[2].y + Convert.ToInt32(num);
			this.m_coords[3].x = this.m_coords[1].x;
			this.m_coords[3].y = this.m_coords[4].y;
			this.m_coords[5].x = this.m_coords[2].x - Convert.ToInt32(this.m_dDeviceRadius);
			this.m_coords[5].y = this.m_coords[2].y - Convert.ToInt32(this.m_dDeviceRadius);
			this.m_coords[6].x = this.m_coords[2].x + Convert.ToInt32(this.m_dDeviceRadius);
			this.m_coords[6].y = this.m_coords[2].y + Convert.ToInt32(this.m_dDeviceRadius);
			this.RotateCoords();
		}

		public bool CanApply(object pUnk)
		{
			return ((IPropertySupport)this).Applies(pUnk);
		}

		void ESRI.ArcGIS.Display.IMarkerMask.QueryMarkerMask(int hDC, ITransformation Transform, IGeometry Geometry, IPolygon Boundary)
		{
			if (!(Geometry == null | Boundary == null))
			{
				if (Transform is IDisplayTransformation)
				{
					if (Geometry is IPoint)
					{
						Boundary.SetEmpty();
						IPoint geometry = Geometry as IPoint;
						IDisplayTransformation transform = (IDisplayTransformation)Transform;
						this.QueryBoundsFromGeom(hDC, ref transform, ref Boundary, ref geometry);
						ITopologicalOperator boundary = Boundary as ITopologicalOperator;
						if (!boundary.IsKnownSimple)
						{
							if (!boundary.IsSimple)
							{
								boundary.Simplify();
							}
						}
					}
				}
			}
		}

		void ESRI.ArcGIS.Display.ISymbol.Draw(IGeometry Geometry)
		{
			if (!(this.m_lhDC == 0 | this.m_colorTop == null | this.m_colorLeft == null | this.m_colorRight == null | this.m_colorBorder == null))
			{
				if (Geometry != null)
				{
					if (Geometry is IPoint)
					{
						IPoint geometry = (IPoint)Geometry;
						int num = 0;
						int num1 = 0;
						Utility.FromMapPoint(this.m_trans, ref geometry, ref num, ref num1);
						double num2 = Convert.ToDouble(num1);
						this.CalcCoords(Convert.ToDouble(num), ref num2);
						int num3 = 0;
						this.m_lOldBrush = Utility.SelectObject(this.m_lhDC, this.m_lBrushTop);
						num3 = Utility.Chord(this.m_lhDC, this.m_coords[5].x, this.m_coords[5].y, this.m_coords[6].x, this.m_coords[6].y, this.m_coords[4].x, this.m_coords[4].y, this.m_coords[1].x, this.m_coords[1].y);
						Utility.SelectObject(this.m_lhDC, this.m_lBrushLeft);
						num3 = Utility.Polygon(this.m_lhDC, ref this.m_coords[1], 3);
						Utility.SelectObject(this.m_lhDC, this.m_lBrushRight);
						num3 = Utility.Polygon(this.m_lhDC, ref this.m_coords[2], 3);
						Utility.SelectObject(this.m_lhDC, this.m_lOldBrush);
					}
				}
			}
		}

		void ESRI.ArcGIS.Display.ISymbol.QueryBoundary(int hDC, ITransformation displayTransform, IGeometry Geometry, IPolygon Boundary)
		{
			if (!(Geometry == null | Boundary == null))
			{
				if (Geometry is IPoint)
				{
					Boundary.SetEmpty();
					IPoint geometry = (IPoint)Geometry;
					IDisplayTransformation displayTransformation = (IDisplayTransformation)displayTransform;
					this.QueryBoundsFromGeom(hDC, ref displayTransformation, ref Boundary, ref geometry);
				}
			}
		}

		void ESRI.ArcGIS.Display.ISymbol.ResetDC()
		{
			this.m_lROP2 = (esriRasterOpCode)Utility.SetROP2(this.m_lhDC, Convert.ToInt32(this.m_lROP2Old));
			Utility.SelectObject(this.m_lhDC, this.m_lOldPen);
			Utility.DeleteObject(this.m_lPen);
			Utility.SelectObject(this.m_lhDC, this.m_lOldBrush);
			Utility.DeleteObject(this.m_lBrushTop);
			Utility.DeleteObject(this.m_lBrushLeft);
			Utility.DeleteObject(this.m_lBrushRight);
			this.m_trans = null;
			this.m_lhDC = 0;
		}

		void ESRI.ArcGIS.Display.ISymbol.SetupDC(int hDC, ITransformation Transformation)
		{
			this.m_trans = Transformation as IDisplayTransformation;
			this.m_lhDC = hDC;
			this.SetupDeviceRatio(this.m_lhDC, this.m_trans);
			this.m_dDeviceRadius = this.m_dSize / 2 * this.m_dDeviceRatio;
			this.m_dDeviceXOffset = this.m_dXOffset * this.m_dDeviceRatio;
			this.m_dDeviceYOffset = this.m_dYOffset * this.m_dDeviceRatio;
			if (!this.m_bRotWithTrans)
			{
				this.m_dMapRotation = 0;
			}
			else
			{
				this.m_dMapRotation = this.m_trans.Rotation;
			}
			this.m_lPen = Utility.CreatePen(0, Convert.ToInt32(1 * this.m_dDeviceRatio), Convert.ToInt32(this.m_colorBorder.RGB));
			this.m_lROP2Old = (esriRasterOpCode)Utility.SetROP2(hDC, Convert.ToInt32(this.m_lROP2));
			this.m_lBrushTop = Utility.CreateSolidBrush(Convert.ToInt32(this.m_colorTop.RGB));
			this.m_lBrushLeft = Utility.CreateSolidBrush(Convert.ToInt32(this.m_colorLeft.RGB));
			this.m_lBrushRight = Utility.CreateSolidBrush(Convert.ToInt32(this.m_colorRight.RGB));
			this.m_lOldPen = Utility.SelectObject(hDC, this.m_lPen);
		}

		void ESRI.ArcGIS.esriSystem.IClone.Assign(IClone src)
		{
			ILogoMarkerSymbol logoMarkerSymbol = null;
			IMarkerSymbol markerSymbol = null;
			IMarkerSymbol angle = null;
			if (src != null)
			{
				if (src is ILogoMarkerSymbol)
				{
					logoMarkerSymbol = src as ILogoMarkerSymbol;
					this.m_colorBorder = logoMarkerSymbol.ColorBorder;
					this.m_colorLeft = logoMarkerSymbol.ColorLeft;
					this.m_colorRight = logoMarkerSymbol.ColorRight;
					this.m_colorTop = logoMarkerSymbol.ColorTop;
					markerSymbol = src as IMarkerSymbol;
					angle = this;
					angle.Angle = markerSymbol.Angle;
					angle.Size = markerSymbol.Size;
					angle.XOffset = markerSymbol.XOffset;
					angle.YOffset = markerSymbol.YOffset;
					//this. = (src as ISymbol).ROP2;
					//this.RotateWithTransform = (src as ISymbolRotation).RotateWithTransform;
					//this.MapLevel = (src as IMapLevel).MapLevel;
				}
			}
		}

		IClone ESRI.ArcGIS.esriSystem.IClone.Clone()
		{
			IClone logoMarkerSymbol = null;
			logoMarkerSymbol = new LogoMarkerSymbol() as IClone;
			logoMarkerSymbol.Assign(this);
			return logoMarkerSymbol;
		}

		bool ESRI.ArcGIS.esriSystem.IClone.IsEqual(IClone other)
		{
			bool angle = false;
			ILogoMarkerSymbol logoMarkerSymbol = null;
			ILogoMarkerSymbol logoMarkerSymbol1 = null;
			IMarkerSymbol markerSymbol = null;
			IMarkerSymbol markerSymbol1 = null;
			ISymbol symbol = null;
			ISymbol symbol1 = null;
			IDisplayName displayName = null;
			IDisplayName displayName1 = null;
			ISymbolRotation symbolRotation = null;
			ISymbolRotation symbolRotation1 = null;
			IMapLevel mapLevel = null;
			IMapLevel mapLevel1 = null;
			if (other != null)
			{
				if (other is ILogoMarkerSymbol)
				{
					logoMarkerSymbol = other as ILogoMarkerSymbol;
					logoMarkerSymbol1 = this;
					System.Drawing.Color color = ColorTranslator.FromOle(Convert.ToInt32(logoMarkerSymbol1.ColorBorder.RGB));
					angle = angle & color.Equals(ColorTranslator.FromOle(Convert.ToInt32(logoMarkerSymbol.ColorBorder.RGB)));
					color = ColorTranslator.FromOle(Convert.ToInt32(logoMarkerSymbol1.ColorLeft.RGB));
					angle = angle & color.Equals(ColorTranslator.FromOle(Convert.ToInt32(logoMarkerSymbol.ColorLeft.RGB)));
					color = ColorTranslator.FromOle(Convert.ToInt32(logoMarkerSymbol1.ColorRight.RGB));
					angle = angle & color.Equals(ColorTranslator.FromOle(Convert.ToInt32(logoMarkerSymbol.ColorRight.RGB)));
					color = ColorTranslator.FromOle(Convert.ToInt32(logoMarkerSymbol1.ColorTop.RGB));
					angle = angle & color.Equals(ColorTranslator.FromOle(Convert.ToInt32(logoMarkerSymbol.ColorTop.RGB)));
					markerSymbol = other as IMarkerSymbol;
					markerSymbol1 = this;
					angle = angle & markerSymbol1.Angle == markerSymbol.Angle;
					color = ColorTranslator.FromOle(Convert.ToInt32(markerSymbol1.Color.RGB));
					angle = angle & color.Equals(ColorTranslator.FromOle(Convert.ToInt32(markerSymbol.Color.RGB)));
					angle = angle & markerSymbol1.Size == markerSymbol.Size;
					angle = angle & markerSymbol1.XOffset == markerSymbol.XOffset;
					angle = angle & markerSymbol1.YOffset == markerSymbol.YOffset;
					symbol = other as ISymbol;
					symbol1 = this;
					angle = angle & symbol1.ROP2 == symbol.ROP2;
					displayName = other as IDisplayName;
					displayName1 = this;
					angle = angle & (displayName1.NameString == displayName.NameString);
					symbolRotation = other as ISymbolRotation;
					symbolRotation1 = this;
					angle = angle & symbolRotation1.RotateWithTransform == symbolRotation.RotateWithTransform;
					mapLevel = other as IMapLevel;
					mapLevel1 = this;
					angle = angle & mapLevel1.MapLevel == mapLevel.MapLevel;
				}
			}
			return angle;
		}

		bool ESRI.ArcGIS.esriSystem.IClone.IsIdentical(IClone other)
		{
			bool flag = false;
			if (other != null)
			{
				if (other is ILogoMarkerSymbol)
				{
					flag = (ILogoMarkerSymbol)other == this;
				}
			}
			return flag;
		}

		void ESRI.ArcGIS.esriSystem.IPersistVariant.Load(IVariantStream Stream)
		{
			int num = 0;
			num = Convert.ToInt32(Stream.Read());
			if (num > 1 | num <= 0)
			{
				throw new Exception("Failed to read from stream");
			}
			this.InitializeMembers();
			if (num == 1)
			{
				this.m_lROP2 = (esriRasterOpCode)Stream.Read();
				this.m_dSize = Convert.ToDouble(Stream.Read());
				this.m_dXOffset = Convert.ToDouble(Stream.Read());
				this.m_dYOffset = Convert.ToDouble(Stream.Read());
				this.m_dAngle = Convert.ToDouble(Stream.Read());
				this.m_bRotWithTrans = Convert.ToBoolean(Stream.Read());
				this.m_lMapLevel = Convert.ToInt32(Stream.Read());
				this.m_colorTop = Stream.Read() as IColor;
				this.m_colorLeft = Stream.Read() as IColor;
				this.m_colorRight = Stream.Read() as IColor;
				this.m_colorBorder = Stream.Read() as IColor;
			}
		}

		void ESRI.ArcGIS.esriSystem.IPersistVariant.Save(IVariantStream Stream)
		{
			Stream.Write(1);
			Stream.Write(this.m_lROP2);
			Stream.Write(this.m_dSize);
			Stream.Write(this.m_dXOffset);
			Stream.Write(this.m_dYOffset);
			Stream.Write(this.m_dAngle);
			Stream.Write(this.m_bRotWithTrans);
			Stream.Write(this.m_lMapLevel);
			Stream.Write(this.m_colorTop);
			Stream.Write(this.m_colorLeft);
			Stream.Write(this.m_colorRight);
			Stream.Write(this.m_colorBorder);
		}

		~LogoMarkerSymbol()
		{
			this.Terminate();
		}

		public object get_Current(object pUnk)
		{
			object color;
			if (null == pUnk as IColor)
			{
				color = ((IClone)this).Clone();
			}
			else
			{
				color = ((IMarkerSymbol)this).Color;
			}
			return color;
		}

		private void Initialize()
		{
			this.InitializeMembers();
		}

		private void InitializeMembers()
		{
			this.m_lhDC = 0;
			this.m_lOldPen = 0;
			this.m_lPen = 0;
			this.m_lOldBrush = 0;
			this.m_lBrushTop = 0;
			this.m_lBrushLeft = 0;
			this.m_lBrushRight = 0;
			this.m_dDeviceRadius = 0;
			this.m_trans = null;
			IColor rGBColor = null;
			rGBColor = Converter.ToRGBColor(System.Drawing.Color.Red);
			this.m_colorTop = ((IClone)rGBColor).Clone() as IColor;
			rGBColor = Converter.ToRGBColor(System.Drawing.Color.OrangeRed);
			this.m_colorLeft = ((IClone)rGBColor).Clone() as IColor;
			rGBColor = Converter.ToRGBColor(System.Drawing.Color.Pink);
			this.m_colorRight = ((IClone)rGBColor).Clone() as IColor;
			rGBColor = Converter.ToRGBColor(System.Drawing.Color.Black);
			this.m_colorBorder = ((IClone)rGBColor).Clone() as IColor;
			this.m_lROP2 = esriRasterOpCode.esriROPCopyPen;
			this.m_dSize = 10;
			this.m_dAngle = 0;
			this.m_dXOffset = 0;
			this.m_dYOffset = 0;
			this.m_bRotWithTrans = true;
		}

		private double PointsToMap(ITransformation displayTransform, double dPointSize)
		{
			double num = 0;
			num = (displayTransform != null ? ((IDisplayTransformation)displayTransform).FromPoints(dPointSize) : dPointSize * this.m_dDeviceRatio);
			return num;
		}

		private void QueryBoundsFromGeom(int hDC, ref IDisplayTransformation transform, ref IPolygon boundary, ref IPoint point)
		{
			double map = 0;
			double num = 0;
			double map1 = 0;
			num = this.PointsToMap(transform, this.m_dSize);
			if (this.m_dXOffset != 0)
			{
				map = this.PointsToMap(transform, this.m_dXOffset);
			}
			if (this.m_dYOffset != 0)
			{
				map1 = this.PointsToMap(transform, this.m_dYOffset);
			}
			point.PutCoords(point.X + map, point.Y + map1);
			this.SetupDeviceRatio(hDC, transform);
			IPointCollection pointCollection = null;
			ISegmentCollection segmentCollection = null;
			double num1 = 0;
			double num2 = 0;
			pointCollection = (IPointCollection)boundary;
			segmentCollection = (ISegmentCollection)boundary;
			num2 = num / 2;
			num1 = Math.Sqrt(num2 * num2 / 2);
			object value = Missing.Value;
			pointCollection.AddPoint(Utility.CreatePoint(point.X + num1, point.Y - num1), ref value, ref value);
			pointCollection.AddPoint(Utility.CreatePoint(point.X - num1, point.Y - num1), ref value, ref value);
			pointCollection.AddPoint(Utility.CreatePoint(point.X - num1, point.Y + num1), ref value, ref value);
			IPoint point1 = pointCollection.Point[0];
			segmentCollection.AddSegment((ISegment)Utility.CreateCircArc(point, pointCollection.Point[2], ref point1), ref value, ref value);
			ITransform2D transform2D = null;
			if (this.m_dAngle + this.m_dMapRotation != 0)
			{
				transform2D = boundary as ITransform2D;
				transform2D.Rotate(point, Utility.Radians(this.m_dAngle + this.m_dMapRotation));
			}
		}

		[ComRegisterFunction]
		[ComVisible(false)]
		private static void RegisterFunction(Type registerType)
		{
			LogoMarkerSymbol.ArcGISCategoryRegistration(registerType);
		}

		private void RotateCoords()
		{
			double mDAngle = 0;
			mDAngle = 360 - (this.m_dAngle + this.m_dMapRotation);
			short i = 0;
			Utility.POINTAPI mCoords = new Utility.POINTAPI();
			for (i = 0; i <= 4; i = (short)(i + 1))
			{
				if (i != 2)
				{
					mCoords = this.m_coords[i];
					this.m_coords[i].x = this.m_coords[2].x + Convert.ToInt32((double)(mCoords.x - this.m_coords[2].x) * Math.Cos(Utility.Radians(mDAngle))) - Convert.ToInt32((double)(mCoords.y - this.m_coords[2].y) * Math.Sin(Utility.Radians(mDAngle)));
					this.m_coords[i].y = this.m_coords[2].y + Convert.ToInt32((double)(mCoords.x - this.m_coords[2].x) * Math.Sin(Utility.Radians(mDAngle))) + Convert.ToInt32((double)(mCoords.y - this.m_coords[2].y) * Math.Cos(Utility.Radians(mDAngle)));
				}
			}
		}

		private void SetupDeviceRatio(int hDC, IDisplayTransformation displayTransform)
		{
			if (displayTransform != null)
			{
				if (displayTransform.Resolution != 0)
				{
					this.m_dDeviceRatio = displayTransform.Resolution / 72;
					if (displayTransform.ReferenceScale != 0)
					{
						this.m_dDeviceRatio = this.m_dDeviceRatio * displayTransform.ReferenceScale / displayTransform.ScaleRatio;
					}
				}
			}
			else if (hDC == 0)
			{
				this.m_dDeviceRatio = (double)(1 / (Utility.TwipsPerPixelX() / 20));
			}
			else
			{
				this.m_dDeviceRatio = Convert.ToDouble(Utility.GetDeviceCaps(hDC, 88)) / 72;
			}
		}

		private void Terminate()
		{
			this.m_trans = null;
			this.m_colorTop = null;
			this.m_colorLeft = null;
			this.m_colorRight = null;
			this.m_colorBorder = null;
		}

		[ComUnregisterFunction]
		[ComVisible(false)]
		private static void UnregisterFunction(Type registerType)
		{
			LogoMarkerSymbol.ArcGISCategoryUnregistration(registerType);
		}
	}
}