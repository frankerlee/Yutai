using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using stdole;

namespace Yutai.ArcGIS.Common.SymbolEx
{
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("2017808c-bbc8-4827-bd1e-c2e94c57872c")]
    [ProgId("JLK.SymbolEx.MyText")]
    public class MyText : ISymbol, ITextSymbol, IDisplayName, IClone, IPersistVariant, ISymbolRotation, IMapLevel,
        ITextParserSupport, IMask, IQueryGeometry, IMarginProperties, ICharacterOrientation, IPropertySupport,
        IDocumentVersionSupportGEN, IMyText
    {
        public const string GUID = "JLK.SymbolEx.MyText";

        private const string m_sDisplayName = "MyText";

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

        private string m_NumeratorText;

        private string m_DenominatorText;

        private IFillSymbol m_pMaskSymbol = new SimpleFillSymbol();

        public double Angle
        {
            get { return this.m_Angle; }
            set { this.m_Angle = value; }
        }

        public int BreakCharacter
        {
            get { return 1; }
            set { }
        }

        public bool Clip
        {
            get { return true; }
            set { }
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
                    color = ((IClone) this.m_pColor).Clone() as IColor;
                }
                return color;
            }
            set { this.m_pColor = (value as IClone).Clone() as IColor; }
        }

        public string DenominatorText
        {
            get { return this.m_DenominatorText; }
            set { this.m_DenominatorText = value; }
        }

        bool ESRI.ArcGIS.Display.ICharacterOrientation.CJKCharactersRotation
        {
            get { return true; }
            set { }
        }

        string ESRI.ArcGIS.Display.IDisplayName.NameString
        {
            get { return "MyText"; }
        }

        int ESRI.ArcGIS.Display.IMapLevel.MapLevel
        {
            get { return this.m_lMapLevel; }
            set { this.m_lMapLevel = value; }
        }

        double ESRI.ArcGIS.Display.IMarginProperties.Margin
        {
            get { return 1; }
            set { }
        }

        double ESRI.ArcGIS.Display.IMask.MaskSize
        {
            get { return 0; }
            set { }
        }

        esriMaskStyle ESRI.ArcGIS.Display.IMask.MaskStyle
        {
            get { return esriMaskStyle.esriMSNone; }
            set { }
        }

        IFillSymbol ESRI.ArcGIS.Display.IMask.MaskSymbol
        {
            get { return this.m_pMaskSymbol; }
            set { this.m_pMaskSymbol = value; }
        }

        esriRasterOpCode ESRI.ArcGIS.Display.ISymbol.ROP2
        {
            get { return this.m_lROP2; }
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
            get { return false; }
            set { }
        }

        ITextParser ESRI.ArcGIS.Display.ITextParserSupport.TextParser
        {
            get { return null; }
            set { }
        }

        UID ESRI.ArcGIS.esriSystem.IPersistVariant.ID
        {
            get
            {
                return new UID()
                {
                    Value = "JLK.SymbolEx.MyText"
                };
            }
        }

        public IFontDisp Font
        {
            get { return this.m_pFont; }
            set { this.m_pFont = value; }
        }

        public esriTextHorizontalAlignment HorizontalAlignment
        {
            get { return this.m_HorizontalAlignment; }
            set { this.m_HorizontalAlignment = value; }
        }

        public string NumeratorText
        {
            get { return this.m_NumeratorText; }
            set { this.m_NumeratorText = value; }
        }

        public bool RightToLeft
        {
            get { return this.m_RightToLeft; }
            set { this.m_RightToLeft = value; }
        }

        public double Size
        {
            get { return this.m_Size; }
            set { this.m_Size = value; }
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
                string[] strArrays = this.m_Text.Split(new char[] {'@'});
                this.m_NumeratorText = strArrays[0];
                if ((int) strArrays.Length <= 0)
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
            get { return null; }
            set { }
        }

        public esriTextVerticalAlignment VerticalAlignment
        {
            get { return this.m_VerticalAlignment; }
            set { this.m_VerticalAlignment = value; }
        }

        public double XOffset
        {
            get { return 0; }
            set { }
        }

        public double YOffset
        {
            get { return 0; }
            set { }
        }

        public MyText()
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
                current = ((IPropertySupport) this).Current[newObject];
                ((ITextSymbol) this).Color = color;
            }
            if (newObject is IFractionTextSymbol)
            {
                current = ((IPropertySupport) this).Current[newObject];
                ((IClone) this).Assign((IClone) newObject);
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
            return ((IPropertySupport) this).Applies(pUnk);
        }

        public object ConvertToSupportedObject(esriArcGISVersion docVersion)
        {
            return this;
        }

        IGeometry ESRI.ArcGIS.Display.IQueryGeometry.GetGeometry(int hDC, ITransformation displayTransform,
            IGeometry drawGeometry)
        {
            IPolygon polygonClass = new Polygon() as IPolygon;
            polygonClass.SetEmpty();
            try
            {
                IDisplayTransformation displayTransformation = (IDisplayTransformation) displayTransform;
                this.QueryBoundsFromGeom(hDC, ref displayTransformation, ref polygonClass, ref drawGeometry);
            }
            catch
            {
            }
            return polygonClass;
        }

        void ESRI.ArcGIS.Display.IQueryGeometry.QueryEnvelope(int hDC, ITransformation displayTransform,
            IGeometry drawGeometry, IEnvelope envelope)
        {
            try
            {
                IPolygon polygonClass = new Polygon() as IPolygon;
                polygonClass.SetEmpty();
                IDisplayTransformation displayTransformation = (IDisplayTransformation) displayTransform;
                this.QueryBoundsFromGeom(hDC, ref displayTransformation, ref polygonClass, ref drawGeometry);
                IEnvelope envelope1 = polygonClass.Envelope;
                envelope.PutCoords(envelope1.XMin, envelope1.YMin, envelope1.XMax, envelope1.YMax);
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
                else if (Geometry != null)
                {
                    IPoint lowerLeft = Geometry.Envelope.LowerLeft;
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

        void ESRI.ArcGIS.Display.ISymbol.QueryBoundary(int hDC, ITransformation displayTransform, IGeometry Geometry,
            IPolygon boundary)
        {
            try
            {
                if (!(Geometry == null | boundary == null))
                {
                    boundary.SetEmpty();
                    IDisplayTransformation displayTransformation = (IDisplayTransformation) displayTransform;
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
                    this.m_NumeratorText = fractionTextSymbol.DenominatorText;
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
            IClone fractionTextSymbolClass = null;
            fractionTextSymbolClass = new FractionTextSymbolClass() as IClone;
            fractionTextSymbolClass.Assign(this);
            return fractionTextSymbolClass;
        }

        bool ESRI.ArcGIS.esriSystem.IClone.IsEqual(IClone other)
        {
            bool flag;
            IMyText myText = null;
            IMyText myText1 = null;
            ITextSymbol textSymbol = null;
            ITextSymbol textSymbol1 = null;
            if (!(other is IMyText))
            {
                flag = false;
            }
            else
            {
                myText = other as IMyText;
                myText1 = this;
                if (myText1.NumeratorText != myText.NumeratorText)
                {
                    flag = false;
                }
                else if (myText1.DenominatorText == myText.DenominatorText)
                {
                    textSymbol = other as ITextSymbol;
                    textSymbol1 = this;
                    if (textSymbol1.Angle != textSymbol.Angle)
                    {
                        flag = false;
                    }
                    else if (
                        !ColorTranslator.FromOle(Convert.ToInt32(textSymbol1.Color.RGB))
                            .Equals(ColorTranslator.FromOle(Convert.ToInt32(textSymbol.Color.RGB))))
                    {
                        flag = false;
                    }
                    else if (textSymbol1.Size != textSymbol.Size)
                    {
                        flag = false;
                    }
                    else if (textSymbol1.HorizontalAlignment != textSymbol.HorizontalAlignment)
                    {
                        flag = false;
                    }
                    else if (textSymbol1.VerticalAlignment != textSymbol.VerticalAlignment)
                    {
                        flag = false;
                    }
                    else if (textSymbol1.RightToLeft != textSymbol.RightToLeft)
                    {
                        flag = false;
                    }
                    //else if (this.ROP2 != (other as ISymbol).ROP2)
                    //{
                    //	flag = false;
                    //}
                    //else if (this.NameString != (other as IDisplayName).NameString)
                    //{
                    //	flag = false;
                    //}
                    //else if (this.RotateWithTransform == (other as ISymbolRotation).RotateWithTransform)
                    //{
                    //	flag = (this.MapLevel == (other as IMapLevel).MapLevel ? true : false);
                    //}
                    else
                    {
                        flag = false;
                    }
                }
                else
                {
                    flag = false;
                }
            }
            return flag;
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
                this.m_lROP2 = (esriRasterOpCode) Stream.Read();
                this.m_Size = Convert.ToDouble(Stream.Read());
                this.m_Angle = Convert.ToDouble(Stream.Read());
                this.m_HorizontalAlignment = (esriTextHorizontalAlignment) Stream.Read();
                this.m_VerticalAlignment = (esriTextVerticalAlignment) Stream.Read();
                this.m_RightToLeft = Convert.ToBoolean(Stream.Read());
                this.m_Text = Convert.ToString(Stream.Read());
                this.m_bRotWithTrans = Convert.ToBoolean(Stream.Read());
                this.m_lMapLevel = Convert.ToInt32(Stream.Read());
                this.m_NumeratorText = Convert.ToString(Stream.Read());
                this.m_DenominatorText = Convert.ToString(Stream.Read());
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
        }

        public object get_Current(object pUnk)
        {
            object color;
            if (null == pUnk as IColor)
            {
                color = ((IClone) this).Clone();
            }
            else
            {
                color = ((ITextSymbol) this).Color;
            }
            return color;
        }

        public void GetTextSize(int hDC, ITransformation Transformation, string Text, out double xSize, out double ySize)
        {
            xSize = 0;
            ySize = 0;
            double num = 0;
            double num1 = 0;
            double num2 = 0;
            double num3 = 0;
            ITextSymbol textSymbolClass = new TextSymbol()
            {
                Text = this.m_DenominatorText
            };
            textSymbolClass.GetTextSize(hDC, Transformation, Text, out num, out num1);
            textSymbolClass = new TextSymbol()
            {
                Text = this.m_NumeratorText
            };
            textSymbolClass.GetTextSize(hDC, Transformation, Text, out num2, out num3);
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
            this.m_NumeratorText = "AaBbYyZz";
            this.m_DenominatorText = "AaBbYyZz";
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

        public bool IsSupportedAtVersion(esriArcGISVersion docVersion)
        {
            return true;
        }

        private double PointsToMap(ITransformation displayTransform, double dPointSize)
        {
            double num = 0;
            num = (displayTransform != null
                ? ((IDisplayTransformation) displayTransform).FromPoints(dPointSize)
                : dPointSize*this.m_dDeviceRatio);
            return num;
        }

        private void QueryBoundsFromGeom(int hDC, ref IDisplayTransformation transform, ref IPolygon boundary,
            ref IGeometry point)
        {
            IPointCollection pointCollection = null;
            pointCollection = (IPointCollection) boundary;
            try
            {
                IPolygon polygonClass = new Polygon() as IPolygon;
                ITextSymbol textSymbolClass = new TextSymbol()
                {
                    Text = this.m_NumeratorText
                };
                (textSymbolClass as ISymbol).QueryBoundary(hDC, transform, point, polygonClass);
                IEnvelope envelope = polygonClass.Envelope;
                IPolygon polygon = new Polygon() as IPolygon;
                textSymbolClass = new TextSymbol()
                {
                    Text = this.m_DenominatorText
                };
                (textSymbolClass as ISymbol).QueryBoundary(hDC, transform, point, polygon);
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
                        transform2D.Rotate((point as IEnvelope).LowerLeft,
                            Utility.Radians(this.m_Angle + this.m_dMapRotation));
                    }
                }
            }
            catch (Exception exception)
            {
                exception.ToString();
            }
        }

        private void QueryBoundsFromGeom(int hDC, ref IDisplayTransformation transform, ref IPolygon boundary,
            ref IPoint point)
        {
            IPointCollection pointCollection = null;
            pointCollection = (IPointCollection) boundary;
            try
            {
                IPolygon polygonClass = new Polygon() as IPolygon;
                ITextSymbol textSymbolClass = new TextSymbol()
                {
                    Text = this.m_NumeratorText
                };
                (textSymbolClass as ISymbol).QueryBoundary(hDC, transform, point, polygonClass);
                IEnvelope envelope = polygonClass.Envelope;
                IPolygon polygon = new Polygon() as IPolygon;
                textSymbolClass = new TextSymbol()
                {
                    Text = this.m_DenominatorText
                };
                (textSymbolClass as ISymbol).QueryBoundary(hDC, transform, point, polygon);
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
            MyText.ArcGISCategoryRegistration(registerType);
        }

        private void SetupDeviceRatio(int hDC, IDisplayTransformation displayTransform)
        {
            if (displayTransform != null)
            {
                if (displayTransform.Resolution != 0)
                {
                    this.m_dDeviceRatio = displayTransform.Resolution/72;
                    if (displayTransform.ReferenceScale != 0)
                    {
                        this.m_dDeviceRatio = this.m_dDeviceRatio*displayTransform.ReferenceScale/
                                              displayTransform.ScaleRatio;
                    }
                }
            }
            else if (hDC == 0)
            {
                this.m_dDeviceRatio = (double) (1/(Utility.TwipsPerPixelX()/20));
            }
            else
            {
                this.m_dDeviceRatio = Convert.ToDouble(Utility.GetDeviceCaps(hDC, 88))/72;
            }
        }

        [ComUnregisterFunction]
        [ComVisible(false)]
        private static void UnregisterFunction(Type registerType)
        {
            MyText.ArcGISCategoryUnregistration(registerType);
        }
    }
}