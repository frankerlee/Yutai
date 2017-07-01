using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Editor
{
    public class SnappingResult : ISnappingResult
    {
        public string Description
        {
            get { return "Text"; }
        }

        public IPoint Location
        {
            get
            {
                IPoint pointClass = new Point()
                {
                    X = this.X,
                    Y = this.Y
                };
                return pointClass;
            }
        }

        public esriSnappingType Type
        {
            get { return esriSnappingType.esriSnappingTypeMidpoint; }
        }

        public double X { get; set; }

        public double Y { get; set; }

        public SnappingResult()
        {
        }
    }
}