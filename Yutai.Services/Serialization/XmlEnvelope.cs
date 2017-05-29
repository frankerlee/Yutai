using System.Runtime.Serialization;

namespace Yutai.Services.Serialization
{
    [DataContract]
    public class XmlEnvelope
    {
        public XmlEnvelope()
        {
        }

        public XmlEnvelope(ESRI.ArcGIS.Geometry.IEnvelope e)
        {
            XMin = e.XMin;
            XMax = e.XMax;
            YMin = e.YMin;
            YMax = e.YMax;
        }

        [DataMember]
        public double XMin { get; set; }

        [DataMember]
        public double XMax { get; set; }

        [DataMember]
        public double YMin { get; set; }

        [DataMember]
        public double YMax { get; set; }
    }
}