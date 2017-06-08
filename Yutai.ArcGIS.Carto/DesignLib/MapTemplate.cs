using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class MapTemplate
    {
        private double double_0 = 500.0;
        private double double_1 = 10.0;
        private double double_10 = 13.0;
        private double double_2 = 10.0;
        private double double_3 = 250.0;
        private double double_4 = 250.0;
        private double double_5 = 50.0;
        private double double_6 = 50.0;
        private double double_7 = 1.0;
        private double double_8 = 0.1;
        private double double_9 = 13.0;
        private ISymbol isymbol_0 = new SimpleLineSymbolClass();
        private ISymbol isymbol_1 = null;
        private short short_0 = 0;
        private SpheroidType spheroidType_0 = SpheroidType.Xian1980;
        private string string_0 = "";
        private string string_1 = "模板";
        private string string_2 = "宋体";
        private StripType stripType_0 = StripType.STThreeDeg;

        public MapTemplate()
        {
            this.isymbol_1 = new SimpleMarkerSymbolClass();
            (this.isymbol_1 as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSCross;
            (this.isymbol_1 as ISimpleMarkerSymbol).Size = 28.0;
        }

        public double BigFontSize
        {
            get
            {
                return this.double_9;
            }
            set
            {
                this.double_9 = value;
            }
        }

        public ISymbol BorderSymbol
        {
            get
            {
                return this.isymbol_0;
            }
            set
            {
                this.isymbol_0 = value;
            }
        }

        public string FontName
        {
            get
            {
                return this.string_2;
            }
            set
            {
                this.string_2 = value;
            }
        }

        public ISymbol GridSymbol
        {
            get
            {
                return this.isymbol_1;
            }
            set
            {
                this.isymbol_1 = value;
            }
        }

        public double Height
        {
            get
            {
                return this.double_6;
            }
            set
            {
                this.double_6 = value;
            }
        }

        public double InOutSpace
        {
            get
            {
                return this.double_7;
            }
            set
            {
                this.double_7 = value;
            }
        }

        public string LegendInfo
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public string Name
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public double OutBorderWidth
        {
            get
            {
                return this.double_8;
            }
            set
            {
                this.double_8 = value;
            }
        }

        public double Scale
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

        public double SmallFontSize
        {
            get
            {
                return this.double_10;
            }
            set
            {
                this.double_10 = value;
            }
        }

        public SpheroidType SpheroidType
        {
            get
            {
                return this.spheroidType_0;
            }
            set
            {
                this.spheroidType_0 = value;
            }
        }

        public double StartX
        {
            get
            {
                return this.double_3;
            }
            set
            {
                this.double_3 = value;
            }
        }

        public double StartY
        {
            get
            {
                return this.double_4;
            }
            set
            {
                this.double_4 = value;
            }
        }

        public StripType StripType
        {
            get
            {
                return this.stripType_0;
            }
            set
            {
                this.stripType_0 = value;
            }
        }

        public short TKType
        {
            get
            {
                return this.short_0;
            }
            set
            {
                this.short_0 = value;
            }
        }

        public double Width
        {
            get
            {
                return this.double_5;
            }
            set
            {
                this.double_5 = value;
            }
        }

        public double XInterval
        {
            get
            {
                return this.double_1;
            }
            set
            {
                this.double_1 = value;
            }
        }

        public double YInterval
        {
            get
            {
                return this.double_2;
            }
            set
            {
                this.double_2 = value;
            }
        }
    }
}

