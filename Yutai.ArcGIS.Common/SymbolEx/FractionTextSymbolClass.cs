using System;
using System.Reflection;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using stdole;

namespace Yutai.ArcGIS.Common.SymbolEx
{
	[ComVisible(true)]
	[Guid("39D4A3B7-E1CF-4701-B407-458C8643FA1F")]
	[ProgId("JLK.SymbolEx.FractionTextSymbolClass")]
	public class FractionTextSymbolClass : ISymbol, ITextSymbol, IDisplayName, IClone, IPersistStream, IPersist, IPersistVariant, ISymbolRotation, IMapLevel, ITextParserSupport, IMask, IQueryGeometry, IMarginProperties, ITextDrawSupport, ICharacterOrientation, IWordBoundaries, IPropertySupport, IDocumentVersionSupportGEN, IFractionTextSymbol
	{
		public const string GUID = "JLK.SymbolEx.FractionTextSymbolClass";

		private const string m_sDisplayName = "FractionTextSymbolClass";

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

		private double m_dDeviceRadius;

		private esriRasterOpCode m_lROP2;

		private bool m_bRotWithTrans;

		private double m_dMapRotation;

		private int m_lMapLevel;

		private double m_Angle = 0;

		private IColor m_pColor;

		private IFontDisp m_pFont;

		private esriTextHorizontalAlignment m_HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;

		private bool m_RightToLeft = false;

		private double m_Size = 0;

		private string m_Text = "";

		private esriTextVerticalAlignment m_VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;

		private ITextSymbol m_NumeratorTextSymbol;

		private ILineSymbol m_LineSymbol;

		private ITextSymbol m_DenominatorTextSymbol;

		private string m_NumeratorText;

		private string m_DenominatorText;

		private IFillSymbol m_pMaskSymbol = new SimpleFillSymbol();

		public double Angle
		{
			get
			{
				return this.m_Angle;
			}
			set
			{
				this.m_Angle = value;
			}
		}

		public ITextBackground Background
		{
			get
			{
				return (this.m_DenominatorTextSymbol as IFormattedTextSymbol).Background;
			}
			set
			{
			}
		}

		public int BreakCharacter
		{
			get
			{
				return 1;
			}
			set
			{
			}
		}

		public esriTextCase Case
		{
			get
			{
				return esriTextCase.esriTCAllCaps;
			}
			set
			{
			}
		}

		public double CharacterSpacing
		{
			get
			{
				return 4;
			}
			set
			{
			}
		}

		public double CharacterWidth
		{
			get
			{
				return 4;
			}
			set
			{
			}
		}

		public bool Clip
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		public IColor Color
		{
			get
			{
				IColor color;
				if (this.m_pColor == null)
				{
					color = null;
				}
				else
				{
					color = ((IClone)this.m_pColor).Clone() as IColor;
				}
				return color;
			}
			set
			{
				this.m_pColor = (value as IClone).Clone() as IColor;
			}
		}

		public string DenominatorText
		{
			get
			{
				return this.m_DenominatorText;
			}
			set
			{
				this.m_DenominatorText = value;
				if (this.m_DenominatorTextSymbol != null)
				{
					this.m_DenominatorTextSymbol.Text = this.m_DenominatorText;
				}
			}
		}

		public ITextSymbol DenominatorTextSymbol
		{
			get
			{
				ITextSymbol textSymbol;
				if (this.m_DenominatorTextSymbol == null)
				{
					textSymbol = null;
				}
				else
				{
					textSymbol = ((IClone)this.m_DenominatorTextSymbol).Clone() as ITextSymbol;
				}
				return textSymbol;
			}
			set
			{
				IClone clone = value as IClone;
				if (clone != null)
				{
					this.m_DenominatorTextSymbol = clone.Clone() as ITextSymbol;
					this.m_DenominatorTextSymbol.Text = this.m_DenominatorText;
				}
			}
		}

		public esriTextDirection Direction
		{
			get
			{
				return esriTextDirection.esriTDHorizontal;
			}
			set
			{
			}
		}

		bool ESRI.ArcGIS.Display.ICharacterOrientation.CJKCharactersRotation
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		string ESRI.ArcGIS.Display.IDisplayName.NameString
		{
			get
			{
				return "FractionTextSymbolClass";
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

		double ESRI.ArcGIS.Display.IMarginProperties.Margin
		{
			get
			{
				return 1;
			}
			set
			{
			}
		}

		double ESRI.ArcGIS.Display.IMask.MaskSize
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		esriMaskStyle ESRI.ArcGIS.Display.IMask.MaskStyle
		{
			get
			{
				return esriMaskStyle.esriMSNone;
			}
			set
			{
			}
		}

		IFillSymbol ESRI.ArcGIS.Display.IMask.MaskSymbol
		{
			get
			{
				return this.m_pMaskSymbol;
			}
			set
			{
				this.m_pMaskSymbol = value;
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
				return false;
			}
			set
			{
			}
		}

		ITextParser ESRI.ArcGIS.Display.ITextParserSupport.TextParser
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		UID ESRI.ArcGIS.esriSystem.IPersistVariant.ID
		{
			get
			{
				return new UID()
				{
					Value = "JLK.SymbolEx.FractionTextSymbolClass"
				};
			}
		}

		public IFillSymbol FillSymbol
		{
			get
			{
				return (this.m_DenominatorTextSymbol as IFormattedTextSymbol).FillSymbol;
			}
			set
			{
			}
		}

		public double FlipAngle
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public IFontDisp Font
		{
			get
			{
				return this.m_pFont;
			}
			set
			{
				this.m_pFont = value;
			}
		}

		public esriTextHorizontalAlignment HorizontalAlignment
		{
			get
			{
				return this.m_HorizontalAlignment;
			}
			set
			{
				this.m_HorizontalAlignment = value;
			}
		}

		public bool Kerning
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public double Leading
		{
			get
			{
				return 10;
			}
			set
			{
			}
		}

		public ILineSymbol LineSymbol
		{
			get
			{
				ILineSymbol lineSymbol;
				if (this.m_LineSymbol == null)
				{
					lineSymbol = null;
				}
				else
				{
					lineSymbol = ((IClone)this.m_LineSymbol).Clone() as ILineSymbol;
				}
				return lineSymbol;
			}
			set
			{
				IClone clone = value as IClone;
				if (clone != null)
				{
					this.m_LineSymbol = clone.Clone() as ILineSymbol;
				}
			}
		}

		public string NumeratorText
		{
			get
			{
				return this.m_NumeratorText;
			}
			set
			{
				this.m_NumeratorText = value;
				if (this.m_NumeratorTextSymbol != null)
				{
					this.m_NumeratorTextSymbol.Text = this.m_NumeratorText;
				}
			}
		}

		public ITextSymbol NumeratorTextSymbol
		{
			get
			{
				ITextSymbol textSymbol;
				if (this.m_NumeratorTextSymbol == null)
				{
					textSymbol = null;
				}
				else
				{
					textSymbol = ((IClone)this.m_NumeratorTextSymbol).Clone() as ITextSymbol;
				}
				return textSymbol;
			}
			set
			{
				IClone clone = value as IClone;
				if (clone != null)
				{
					this.m_NumeratorTextSymbol = clone.Clone() as ITextSymbol;
					this.m_NumeratorTextSymbol.Text = this.m_NumeratorText;
				}
			}
		}

		public esriTextPosition Position
		{
			get
			{
				return esriTextPosition.esriTPNormal;
			}
			set
			{
			}
		}

		internal IPropertySet PropertySet
		{
			get
			{
				IPropertySet propertySetClass = new PropertySet();
				propertySetClass.SetProperty("DenominatorText", this.m_DenominatorText);
				propertySetClass.SetProperty("DenominatorTextSymbol", this.m_DenominatorTextSymbol);
				propertySetClass.SetProperty("HorizontalAlignment", this.m_HorizontalAlignment);
				propertySetClass.SetProperty("LineSymbol", this.m_LineSymbol);
				propertySetClass.SetProperty("NumeratorText", this.m_NumeratorText);
				propertySetClass.SetProperty("NumeratorTextSymbol", this.m_NumeratorTextSymbol);
				propertySetClass.SetProperty("Color", this.m_pColor);
				propertySetClass.SetProperty("Font", this.m_pFont);
				propertySetClass.SetProperty("MaskSymbol", this.m_pMaskSymbol);
				propertySetClass.SetProperty("RightToLeft", this.m_RightToLeft);
				propertySetClass.SetProperty("VerticalAlignment", this.m_VerticalAlignment);
				return propertySetClass;
			}
			set
			{
				IPropertySet propertySet = value;
				this.m_DenominatorText = propertySet.GetProperty("DenominatorText").ToString();
				this.m_DenominatorTextSymbol = propertySet.GetProperty("DenominatorTextSymbol") as ITextSymbol;
				this.m_HorizontalAlignment = (esriTextHorizontalAlignment)propertySet.GetProperty("HorizontalAlignment");
				this.m_LineSymbol = propertySet.GetProperty("LineSymbol") as ILineSymbol;
				this.m_NumeratorText = propertySet.GetProperty("NumeratorText").ToString();
				this.m_NumeratorTextSymbol = propertySet.GetProperty("NumeratorTextSymbol") as ITextSymbol;
				this.m_pColor = propertySet.GetProperty("Color") as IColor;
				this.m_pFont = propertySet.GetProperty("Font") as IFontDisp;
				this.m_pMaskSymbol = propertySet.GetProperty("MaskSymbol") as IFillSymbol;
				this.m_RightToLeft = (bool)propertySet.GetProperty("RightToLeft");
				this.m_VerticalAlignment = (esriTextVerticalAlignment)propertySet.GetProperty("VerticalAlignment");
			}
		}

		public bool RightToLeft
		{
			get
			{
				return this.m_RightToLeft;
			}
			set
			{
				this.m_RightToLeft = value;
			}
		}

		public IColor ShadowColor
		{
			get
			{
				return this.Color;
			}
			set
			{
			}
		}

		public double ShadowXOffset
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public double ShadowYOffset
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public double Size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				this.m_Size = value;
			}
		}

		protected object State
		{
			get
			{
				object obj;
				IPropertySet propertySetClass = new PropertySet();
				propertySetClass.SetProperty("DenominatorText", this.m_DenominatorText);
				propertySetClass.SetProperty("DenominatorTextSymbol", this.m_DenominatorTextSymbol);
				propertySetClass.SetProperty("HorizontalAlignment", this.m_HorizontalAlignment);
				propertySetClass.SetProperty("LineSymbol", this.m_LineSymbol);
				propertySetClass.SetProperty("NumeratorText", this.m_NumeratorText);
				propertySetClass.SetProperty("NumeratorTextSymbol", this.m_NumeratorTextSymbol);
				propertySetClass.SetProperty("Color", this.m_pColor);
				propertySetClass.SetProperty("Font", this.m_pFont);
				propertySetClass.SetProperty("MaskSymbol", this.m_pMaskSymbol);
				propertySetClass.SetProperty("RightToLeft", this.m_RightToLeft);
				propertySetClass.SetProperty("VerticalAlignment", this.m_VerticalAlignment);
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
				this.m_DenominatorText = propertySetClass.GetProperty("DenominatorText").ToString();
				this.m_DenominatorTextSymbol = propertySetClass.GetProperty("DenominatorTextSymbol") as ITextSymbol;
				this.m_HorizontalAlignment = (esriTextHorizontalAlignment)propertySetClass.GetProperty("HorizontalAlignment");
				this.m_LineSymbol = propertySetClass.GetProperty("LineSymbol") as ILineSymbol;
				this.m_NumeratorText = propertySetClass.GetProperty("NumeratorText").ToString();
				this.m_NumeratorTextSymbol = propertySetClass.GetProperty("NumeratorTextSymbol") as ITextSymbol;
				this.m_pColor = propertySetClass.GetProperty("Color") as IColor;
				this.m_pFont = propertySetClass.GetProperty("Font") as IFontDisp;
				this.m_pMaskSymbol = propertySetClass.GetProperty("MaskSymbol") as IFillSymbol;
				this.m_RightToLeft = (bool)propertySetClass.GetProperty("RightToLeft");
				this.m_VerticalAlignment = (esriTextVerticalAlignment)propertySetClass.GetProperty("VerticalAlignment");
			}
		}

		public string Text
		{
			get
			{
				if (this.m_Text.Length == 0)
				{
					this.m_Text = string.Concat(this.m_DenominatorText, "@", this.m_NumeratorText);
				}
				return this.m_Text;
			}
			set
			{
				this.m_Text = value;
				string[] strArrays = this.m_Text.Split(new char[] { '@' });
				this.m_NumeratorText = strArrays[0];
				if ((int)strArrays.Length <= 0)
				{
					this.m_DenominatorText = strArrays[0];
				}
				else
				{
					this.m_DenominatorText = strArrays[1];
				}
			}
		}

		public ITextPath TextPath
		{
			get
			{
				return (this.m_DenominatorTextSymbol as ISimpleTextSymbol).TextPath;
			}
			set
			{
			}
		}

		public bool TypeSetting
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public esriTextVerticalAlignment VerticalAlignment
		{
			get
			{
				return this.m_VerticalAlignment;
			}
			set
			{
				this.m_VerticalAlignment = value;
			}
		}

		public double WordSpacing
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public double XOffset
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public double YOffset
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public FractionTextSymbolClass()
		{
			this.InitializeMembers();
		}

		public bool Applies(object pUnk)
		{
			return true;
		}

		public object Apply(object newObject)
		{
			object current = null;
			IColor color = newObject as IColor;
			if (null != color)
			{
				current = ((IPropertySupport)this).get_Current(newObject);
				((ITextSymbol)this).Color = color;
			}
			if (newObject is IFractionTextSymbol)
			{
				current = ((IPropertySupport)this).get_Current(newObject);
                ((IClone)this).Assign((IClone)newObject);
			}
			return current;
		}

		/// <summary>
		/// Required method for ArcGIS Component Category registration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryRegistration(Type registerType)
		{
			string str = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			ESRI.ArcGIS.ADF.CATIDs.TextSymbol.Register(str);
		}

		/// <summary>
		/// Required method for ArcGIS Component Category unregistration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryUnregistration(Type registerType)
		{
			string str = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			ESRI.ArcGIS.ADF.CATIDs.TextSymbol.Unregister(str);
		}

		public bool CanApply(object pUnk)
		{
			return ((IPropertySupport)this).Applies(pUnk);
		}

		public object ConvertToSupportedObject(esriArcGISVersion docVersion)
		{
			return this;
		}

		IGeometry ESRI.ArcGIS.Display.IQueryGeometry.GetGeometry(int hDC, ITransformation displayTransform, IGeometry drawGeometry)
		{
			return (this.m_NumeratorTextSymbol as IQueryGeometry).GetGeometry(hDC, displayTransform, drawGeometry);
		}

		void ESRI.ArcGIS.Display.IQueryGeometry.QueryEnvelope(int hDC, ITransformation displayTransform, IGeometry drawGeometry, IEnvelope envelope)
		{
			try
			{
				(this.m_NumeratorTextSymbol as IQueryGeometry).QueryEnvelope(hDC, displayTransform, drawGeometry, envelope);
				return;
			}
			catch
			{
			}
		}

		void ESRI.ArcGIS.Display.ISymbol.Draw(IGeometry Geometry)
		{
			try
			{
				if (this.m_lhDC == 0)
				{
					return;
				}
				else if (Geometry == null)
				{
					return;
				}
				else if (Geometry is IPoint)
				{
					IPoint lowerLeft = Geometry.Envelope.LowerLeft;
					this.m_NumeratorTextSymbol.Text = this.m_NumeratorText;
					this.m_NumeratorTextSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
					this.m_NumeratorTextSymbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
					this.m_DenominatorTextSymbol.Text = this.m_DenominatorText;
					this.m_DenominatorTextSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
					this.m_DenominatorTextSymbol.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
					double num = 10;
					IPoint pointClass = new Point();
					pointClass.PutCoords(lowerLeft.X + num / 2, lowerLeft.Y + 5);
					(this.m_NumeratorTextSymbol as ISymbol).SetupDC(this.m_lhDC, this.m_trans);
					(this.m_NumeratorTextSymbol as ISymbol).Draw(pointClass);
					(this.m_NumeratorTextSymbol as ISymbol).ResetDC();
				}
				else
				{
					return;
				}
			}
			catch
			{
			}
		}

		void ESRI.ArcGIS.Display.ISymbol.QueryBoundary(int hDC, ITransformation displayTransform, IGeometry Geometry, IPolygon boundary)
		{
			try
			{
				if (!(Geometry == null | boundary == null))
				{
					boundary.SetEmpty();
					IDisplayTransformation displayTransformation = (IDisplayTransformation)displayTransform;
					this.QueryBoundsFromGeom(hDC, ref displayTransformation, ref boundary, ref Geometry);
				}
				else
				{
					return;
				}
			}
			catch
			{
			}
		}

		void ESRI.ArcGIS.Display.ISymbol.ResetDC()
		{
			this.m_lROP2 = this.m_lROP2Old;
			this.m_trans = null;
			this.m_lhDC = 0;
		}

		void ESRI.ArcGIS.Display.ISymbol.SetupDC(int hDC, ITransformation Transformation)
		{
			this.m_trans = Transformation as IDisplayTransformation;
			this.m_lhDC = hDC;
			this.SetupDeviceRatio(this.m_lhDC, this.m_trans);
			if (!this.m_bRotWithTrans)
			{
				this.m_dMapRotation = 0;
			}
			else
			{
				this.m_dMapRotation = this.m_trans.Rotation;
			}
		}

		object ESRI.ArcGIS.Display.ITextDrawSupport.GetDrawPoints(int hDC, ITransformation pTransform, IGeometry pGeometry)
		{
			object obj;
			obj = (this.m_DenominatorTextSymbol != null ? (this.m_DenominatorTextSymbol as ITextDrawSupport).GetDrawPoints(hDC, pTransform, pGeometry) : null);
			return obj;
		}

		void ESRI.ArcGIS.Display.ITextDrawSupport.GetDrawText(string origText, out string pParsedText, out object pPositions)
		{
			pParsedText = "";
			pPositions = "";
			if (this.m_DenominatorTextSymbol != null)
			{
				(this.m_DenominatorTextSymbol as ITextDrawSupport).GetDrawText(origText, out pParsedText, out pPositions);
			}
		}

		void ESRI.ArcGIS.Display.IWordBoundaries.QueryWordBoundaries(int hDC, ITransformation displayTransform, IGeometry Geometry, IGeometryBag boundaries)
		{
			if (this.m_DenominatorTextSymbol != null)
			{
				(this.m_DenominatorTextSymbol as IWordBoundaries).QueryWordBoundaries(hDC, displayTransform, Geometry, boundaries);
			}
		}

		void ESRI.ArcGIS.esriSystem.IClone.Assign(IClone src)
		{
			if (src is IFractionTextSymbol)
			{
				try
				{
					ITextSymbol textSymbol = null;
					ITextSymbol angle = null;
					IFractionTextSymbol fractionTextSymbol = src as IFractionTextSymbol;
					this.m_DenominatorText = fractionTextSymbol.DenominatorText;
					this.m_DenominatorTextSymbol = fractionTextSymbol.DenominatorTextSymbol;
					this.m_NumeratorText = fractionTextSymbol.DenominatorText;
					this.m_NumeratorTextSymbol = fractionTextSymbol.NumeratorTextSymbol;
					this.m_LineSymbol = fractionTextSymbol.LineSymbol;
					textSymbol = src as ITextSymbol;
					angle = this;
					angle.Angle = textSymbol.Angle;
					angle.Size = textSymbol.Size;
					angle.Color = textSymbol.Color;
					angle.HorizontalAlignment = textSymbol.HorizontalAlignment;
					angle.RightToLeft = textSymbol.RightToLeft;
					angle.Text = textSymbol.Text;
					angle.VerticalAlignment = textSymbol.VerticalAlignment;
					//this.ROP2 = (src as ISymbol).ROP2;
					//this.RotateWithTransform = (src as ISymbolRotation).RotateWithTransform;
					//this.MapLevel = (src as IMapLevel).MapLevel;
				}
				catch
				{
				}
			}
		}

		IClone ESRI.ArcGIS.esriSystem.IClone.Clone()
		{
			IFractionTextSymbol fractionTextSymbolClass = null;
			IClone clone = null;
			fractionTextSymbolClass = new FractionTextSymbolClass();
			return clone;
		}

		bool ESRI.ArcGIS.esriSystem.IClone.IsEqual(IClone other)
		{
			return true;
		}

		bool ESRI.ArcGIS.esriSystem.IClone.IsIdentical(IClone other)
		{
			return (other != this ? false : true);
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
				this.m_Size = Convert.ToDouble(Stream.Read());
				this.m_Angle = Convert.ToDouble(Stream.Read());
				this.m_HorizontalAlignment = (esriTextHorizontalAlignment)Stream.Read();
				this.m_VerticalAlignment = (esriTextVerticalAlignment)Stream.Read();
				this.m_RightToLeft = Convert.ToBoolean(Stream.Read());
				this.m_Text = Convert.ToString(Stream.Read());
				this.m_bRotWithTrans = Convert.ToBoolean(Stream.Read());
				this.m_lMapLevel = Convert.ToInt32(Stream.Read());
				this.m_NumeratorText = Convert.ToString(Stream.Read());
				this.m_DenominatorText = Convert.ToString(Stream.Read());
				this.m_LineSymbol = Stream.Read() as ILineSymbol;
				this.m_NumeratorTextSymbol = Stream.Read() as ITextSymbol;
				this.m_DenominatorTextSymbol = Stream.Read() as ITextSymbol;
			}
		}

		void ESRI.ArcGIS.esriSystem.IPersistVariant.Save(IVariantStream Stream)
		{
			Stream.Write(1);
			Stream.Write(this.m_lROP2);
			Stream.Write(this.m_Size);
			Stream.Write(this.m_Angle);
			Stream.Write(this.m_HorizontalAlignment);
			Stream.Write(this.m_VerticalAlignment);
			Stream.Write(this.m_RightToLeft);
			Stream.Write(this.m_Text);
			Stream.Write(this.m_bRotWithTrans);
			Stream.Write(this.m_lMapLevel);
			Stream.Write(this.m_NumeratorText);
			Stream.Write(this.m_DenominatorText);
			Stream.Write(this.m_LineSymbol);
			Stream.Write(this.m_NumeratorTextSymbol);
			Stream.Write(this.m_DenominatorTextSymbol);
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
				color = ((ITextSymbol)this).Color;
			}
			return color;
		}

		public void GetClassID(out Guid pClassID)
		{
			pClassID = new Guid("39D4A3B7-E1CF-4701-B407-458C8643FA1F");
		}

		public void GetSizeMax(out _ULARGE_INTEGER pcbSize)
		{
			(new PropertySet() as IPersistStream).GetSizeMax(out pcbSize);
		}

		public void GetTextSize(int hDC, ITransformation Transformation, string Text, out double xSize, out double ySize)
		{
			xSize = 0;
			ySize = 0;
			double num = 0;
			double num1 = 0;
			double num2 = 0;
			double num3 = 0;
			this.m_DenominatorTextSymbol.GetTextSize(hDC, Transformation, Text, out num, out num1);
			this.m_NumeratorTextSymbol.GetTextSize(hDC, Transformation, Text, out num2, out num3);
			xSize = num + num2 + 6;
			ySize = num1 + num3 + 6;
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
			this.m_NumeratorTextSymbol = new TextSymbol();
			this.m_LineSymbol = new SimpleLineSymbol();
			this.m_DenominatorTextSymbol = new TextSymbol();
			this.m_NumeratorText = "AaBbYyZz";
			this.m_DenominatorText = "AaBbYyZz";
			this.m_NumeratorTextSymbol.Text = this.m_NumeratorText;
			this.m_NumeratorTextSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
			this.m_NumeratorTextSymbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
			this.m_DenominatorTextSymbol.Text = this.m_DenominatorText;
			this.m_DenominatorTextSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
			this.m_DenominatorTextSymbol.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
			this.m_lROP2 = esriRasterOpCode.esriROPCopyPen;
			this.m_Angle = 0;
			this.m_pColor = new RgbColor();
			this.m_pFont = new StdFont() as IFontDisp;
			this.m_HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
			this.m_RightToLeft = false;
			this.m_Size = 0;
			this.m_Text = "";
			this.m_VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;
			this.m_bRotWithTrans = true;
		}

		public void IsDirty()
		{
		}

		public bool IsSupportedAtVersion(esriArcGISVersion docVersion)
		{
			return true;
		}

		public void Load(IStream pstm)
		{
			IPropertySet propertySetClass = new PropertySet();
			(propertySetClass as IPersistStream).Load(pstm);
			this.PropertySet = propertySetClass;
		}

		private double PointsToMap(ITransformation displayTransform, double dPointSize)
		{
			double num = 0;
			num = (displayTransform != null ? ((IDisplayTransformation)displayTransform).FromPoints(dPointSize) : dPointSize * this.m_dDeviceRatio);
			return num;
		}

		private void QueryBoundsFromGeom(int hDC, ref IDisplayTransformation transform, ref IPolygon boundary, ref IGeometry point)
		{
			IPointCollection pointCollection = null;
			pointCollection = (IPointCollection)boundary;
			try
			{
				IPolygon polygonClass = new Polygon() as IPolygon;
				(this.m_NumeratorTextSymbol as ISymbol).QueryBoundary(hDC, transform, point, polygonClass);
				IEnvelope envelope = polygonClass.Envelope;
				IPolygon polygon = new Polygon() as IPolygon;
				(this.m_DenominatorTextSymbol as ISymbol).QueryBoundary(hDC, transform, point, polygon);
				envelope.Union(polygon.Envelope);
				envelope.Expand(4, 0, false);
				object value = Missing.Value;
				object obj = Missing.Value;
				pointCollection.AddPoint(envelope.LowerLeft, ref value, ref value);
				pointCollection.AddPoint(envelope.LowerRight, ref value, ref value);
				pointCollection.AddPoint(envelope.UpperRight, ref value, ref value);
				pointCollection.AddPoint(envelope.UpperLeft, ref value, ref value);
				pointCollection.AddPoint(envelope.LowerLeft, ref value, ref value);
				ITransform2D transform2D = null;
				if (this.m_Angle + this.m_dMapRotation != 0)
				{
					transform2D = boundary as ITransform2D;
					if (point is IPoint)
					{
						transform2D.Rotate(point as IPoint, Utility.Radians(this.m_Angle + this.m_dMapRotation));
					}
					else if (point is IEnvelope)
					{
						transform2D.Rotate((point as IEnvelope).LowerLeft, Utility.Radians(this.m_Angle + this.m_dMapRotation));
					}
				}
			}
			catch (Exception exception)
			{
				exception.ToString();
			}
		}

		private void QueryBoundsFromGeom(int hDC, ref IDisplayTransformation transform, ref IPolygon boundary, ref IPoint point)
		{
			IPointCollection pointCollection = null;
			pointCollection = (IPointCollection)boundary;
			try
			{
				IPolygon polygonClass = new Polygon() as IPolygon;
				(this.m_NumeratorTextSymbol as ISymbol).QueryBoundary(hDC, transform, point, polygonClass);
				IEnvelope envelope = polygonClass.Envelope;
				IPolygon polygon = new Polygon() as IPolygon;
				(this.m_DenominatorTextSymbol as ISymbol).QueryBoundary(hDC, transform, point, polygon);
				envelope.Union(polygon.Envelope);
				envelope.Expand(4, 0, false);
				object value = Missing.Value;
				object obj = Missing.Value;
				pointCollection.AddPoint(envelope.LowerLeft, ref value, ref value);
				pointCollection.AddPoint(envelope.LowerRight, ref value, ref value);
				pointCollection.AddPoint(envelope.UpperRight, ref value, ref value);
				pointCollection.AddPoint(envelope.UpperLeft, ref value, ref value);
				pointCollection.AddPoint(envelope.LowerLeft, ref value, ref value);
				ITransform2D transform2D = null;
				if (this.m_Angle + this.m_dMapRotation != 0)
				{
					transform2D = boundary as ITransform2D;
					transform2D.Rotate(point, Utility.Radians(this.m_Angle + this.m_dMapRotation));
				}
			}
			catch (Exception exception)
			{
				exception.ToString();
			}
		}

		[ComRegisterFunction]
		[ComVisible(false)]
		private static void RegisterFunction(Type registerType)
		{
			FractionTextSymbolClass.ArcGISCategoryRegistration(registerType);
		}

		public void Save(IStream pstm, int fClearDirty)
		{
			(this.PropertySet as IPersistStream).Save(pstm, fClearDirty);
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

		[ComUnregisterFunction]
		[ComVisible(false)]
		private static void UnregisterFunction(Type registerType)
		{
			FractionTextSymbolClass.ArcGISCategoryUnregistration(registerType);
		}
	}
}