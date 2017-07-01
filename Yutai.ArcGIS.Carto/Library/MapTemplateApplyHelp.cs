using System.Runtime.CompilerServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Carto.MapCartoTemplateLib;

namespace Yutai.ArcGIS.Carto.Library
{
    public class MapTemplateApplyHelp
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private bool bool_2 = false;

        private double double_0 = 0.0;
        private double double_1 = 0.0;
        private double double_2;
        private double double_3;
        private double double_4;
        private double double_5;
        private double double_6 = 0.0;
        private double double_7 = 10000.0;
        private int int_0 = 0;
        private IPoint ipoint_0;
        private IPoint ipoint_1;
        private IPoint ipoint_2;
        private IPoint ipoint_3;
        private MapTemplate mapTemplate_0 = null;
        private string string_0 = "";

        public void ApplyMapTemplate(IPageLayout ipageLayout_0)
        {
            this.GetFocusMapFrame(ipageLayout_0);
            ((ipageLayout_0 as IActiveView).FocusMap as IMapClipOptions).ClipType = esriMapClipType.esriMapClipNone;
            if ((ipageLayout_0 as IActiveView).FocusMap is IMapAutoExtentOptions)
            {
                ((ipageLayout_0 as IActiveView).FocusMap as IMapAutoExtentOptions).AutoExtentType =
                    esriExtentTypeEnum.esriExtentDefault;
            }
            if (this.FixDataRange)
            {
                IEnvelope extent = ((ipageLayout_0 as IActiveView).FocusMap as IActiveView).Extent;
                this.mapTemplate_0.CreateTKByRect(ipageLayout_0 as IActiveView, extent);
            }
            else if (this.IsMapNo)
            {
                this.mapTemplate_0.CreateTrapezoidTK(ipageLayout_0,
                    MapNoAssistantFactory.CreateMapNoAssistant(this.string_0));
            }
            else if (this.mapTemplate_0.MapFramingType == MapFramingType.StandardFraming)
            {
                PointClass class2 = new PointClass
                {
                    X = this.double_0,
                    Y = this.double_1
                };
                this.mapTemplate_0.CreateTKN(ipageLayout_0 as IActiveView, class2);
            }
            (ipageLayout_0 as IGraphicsContainerSelect).UnselectAllElements();
        }

        public void ApplyMapTemplate(IPageLayout ipageLayout_0, IEnvelope ienvelope_0)
        {
            this.mapTemplate_0.CreateTKByRect(ipageLayout_0 as IActiveView, ienvelope_0);
        }

        public void ApplyMapTemplateEx(IPageLayout ipageLayout_0, IEnvelope ienvelope_0)
        {
            this.mapTemplate_0.CreateTKEx(ipageLayout_0 as IActiveView, ienvelope_0);
        }

        internal IMapFrame GetFocusMapFrame(IPageLayout ipageLayout_0)
        {
            IGraphicsContainer container = ipageLayout_0 as IGraphicsContainer;
            container.Reset();
            for (IElement element = container.Next(); element != null; element = container.Next())
            {
                if (element is IMapFrame)
                {
                    return (element as IMapFrame);
                }
            }
            return null;
        }

        public void SetJWD(double double_8, double double_9, double double_10, double double_11, double double_12)
        {
            this.bool_1 = false;
            this.bool_0 = true;
            this.double_2 = double_8;
            this.double_3 = double_9;
            this.double_4 = double_10;
            this.double_5 = double_11;
            this.double_7 = double_12;
        }

        public void SetRouneCoordinate(IPoint ipoint_4, IPoint ipoint_5, IPoint ipoint_6, IPoint ipoint_7,
            double double_8)
        {
            this.bool_1 = false;
            this.bool_0 = false;
            this.ipoint_0 = ipoint_4;
            this.ipoint_1 = ipoint_5;
            this.ipoint_2 = ipoint_6;
            this.ipoint_3 = ipoint_7;
            this.double_7 = double_8;
        }

        public MapTemplate CartoTemplateData
        {
            set { this.mapTemplate_0 = value; }
        }

        public int CoordinateType
        {
            set { this.int_0 = value; }
        }

        public bool FixDataRange { get; set; }

        public bool HasStrip
        {
            get { return this.bool_2; }
            set { this.bool_2 = value; }
        }

        public bool IsMapNo
        {
            get { return this.bool_1; }
        }

        public double JC
        {
            get { return this.double_4; }
        }

        public double JD
        {
            get { return this.double_2; }
        }

        public string MapNo
        {
            get { return this.string_0; }
            set
            {
                this.string_0 = value;
                this.bool_1 = true;
                this.bool_0 = false;
            }
        }

        public double WC
        {
            get { return this.double_5; }
        }

        public double WD
        {
            get { return this.double_3; }
        }

        public double X
        {
            set { this.double_0 = value; }
        }

        public double XOffset
        {
            get { return this.double_6; }
            set { this.double_6 = value; }
        }

        public double Y
        {
            set { this.double_1 = value; }
        }
    }
}