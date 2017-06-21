using ESRI.ArcGIS.Geometry;

namespace Yutai.Pipeline.Analysis.Classes
{
    public class DstLineItem
    {
        public IPolyline m_pPolyline;

        public int m_nDstLineFromID;

        public int m_nDstLineToID;

        public double m_dPipeWidth;

        public double m_dPipeHeight;

        public string m_strPipeWidthAndHeight;

        public int m_nPipeHeightKind;

        public string m_strLayerName;

        public int m_nFID;

        public double m_nTolernDist;

        public float m_dResultDistH;

        public float m_dResultDistV;

        public float m_dTolDistH = -1f;

        public float m_dTolDistV = -1f;

        public DstLineItem()
        {
        }
    }
}