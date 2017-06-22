using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto
{
    internal abstract class BaseMap
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private double double_0 = 0.0;
        private double double_1 = 0.0;
        private IPoint ipoint_0 = null;
        private IPoint ipoint_1 = null;
        private IPoint ipoint_2 = null;
        private IPoint ipoint_3 = null;
        protected ICompositeGraphicsLayer m_GraphicsLayer = null;
        protected bool m_IsAdminSys = true;
        protected IActiveView m_pActiveView = null;
        protected double m_ReferenceScale = 10000.0;
        private string string_0 = null;
        private string string_1 = "";
        private string string_2 = "";
        private string string_3 = "";
        protected string styleFile = "";

        protected BaseMap()
        {
        }

        public bool Delete()
        {
            return true;
        }

        public abstract void Draw();
        public virtual void DrawBackFrame()
        {
        }

        public abstract void DrawInsideFrame();
        public abstract void DrawLegend();
        public abstract void DrawOutFrame();
        public abstract void DrawRemark();
        public abstract void DrawTitle();
        protected ITextSymbol FontStyle(double double_2, esriTextHorizontalAlignment esriTextHorizontalAlignment_0, esriTextVerticalAlignment esriTextVerticalAlignment_0)
        {
            ITextSymbol symbol = new TextSymbolClass();
            IRgbColor color = new RgbColorClass {
                Blue = 0,
                Red = 0,
                Green = 0
            };
            symbol.Size = double_2;
            symbol.Color = color;
            symbol.HorizontalAlignment = esriTextHorizontalAlignment_0;
            symbol.VerticalAlignment = esriTextVerticalAlignment_0;
            return symbol;
        }

        public IFillSymbol GetBackStyle()
        {
            ISimpleFillSymbol symbol = new SimpleFillSymbolClass();
            new SimpleLineSymbolClass();
            IRgbColor color = new RgbColorClass {
                Red = 255,
                Blue = 255,
                Green = 255
            };
            symbol.Color = color;
            symbol.Outline = null;
            return symbol;
        }

        public bool Load()
        {
            return true;
        }

        public bool Save()
        {
            return true;
        }

        public IActiveView ActiveView
        {
            set
            {
                this.m_pActiveView = value;
            }
        }

        public ICompositeGraphicsLayer GraphicsLayer
        {
            set
            {
                this.m_GraphicsLayer = value;
            }
        }

        public double InOutDist
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

        public IPoint LeftLow
        {
            get
            {
                return this.ipoint_1;
            }
            set
            {
                this.ipoint_1 = value;
            }
        }

        public IPoint LeftUp
        {
            get
            {
                return this.ipoint_0;
            }
            set
            {
                this.ipoint_0 = value;
            }
        }

        public double MapReferenceScale
        {
            set
            {
                this.m_ReferenceScale = value;
            }
        }

        public string MapTH
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

        public string MapTM
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

        public string MapType
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

        public bool NeedGrid
        {
            get
            {
                return this.bool_1;
            }
            set
            {
                this.bool_1 = value;
            }
        }

        public bool NeedLegend
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        public IPoint RightLow
        {
            get
            {
                return this.ipoint_3;
            }
            set
            {
                this.ipoint_3 = value;
            }
        }

        public IPoint RightUp
        {
            get
            {
                return this.ipoint_2;
            }
            set
            {
                this.ipoint_2 = value;
            }
        }

        public string StyleFile
        {
            get
            {
                return this.styleFile;
            }
            set
            {
                this.styleFile = value;
            }
        }

        public double TitleDist
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
    }
}

