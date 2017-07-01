using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.ZD
{
    public class HitFeatureInfo
    {
        public IFeature Feature { get; set; }

        public int PartIndex { get; set; }

        public int VertIndex { get; set; }

        public int SegmentIndex { get; set; }

        public HitFeatureInfo()
        {
            this.PartIndex = -1;
            this.VertIndex = -1;
            this.SegmentIndex = -1;
        }

        public ISegment GetSegment()
        {
            ISegment result;
            if (this.VertIndex == -1)
            {
                result = null;
            }
            else
            {
                IGeometryCollection geometryCollection = this.Feature.Shape as IGeometryCollection;
                ISegmentCollection segmentCollection =
                    geometryCollection.get_Geometry(this.PartIndex) as ISegmentCollection;
                result = segmentCollection.get_Segment(this.VertIndex);
            }
            return result;
        }

        public ISegment GetSegment2()
        {
            ISegment result;
            if (this.VertIndex == -1)
            {
                result = null;
            }
            else if (this.VertIndex == 0)
            {
                IGeometryCollection geometryCollection = this.Feature.Shape as IGeometryCollection;
                ISegmentCollection segmentCollection =
                    geometryCollection.get_Geometry(this.PartIndex) as ISegmentCollection;
                result = segmentCollection.get_Segment(segmentCollection.SegmentCount - 1);
            }
            else
            {
                IGeometryCollection geometryCollection = this.Feature.Shape as IGeometryCollection;
                ISegmentCollection segmentCollection =
                    geometryCollection.get_Geometry(this.PartIndex) as ISegmentCollection;
                result = segmentCollection.get_Segment(this.VertIndex - 1);
            }
            return result;
        }
    }
}