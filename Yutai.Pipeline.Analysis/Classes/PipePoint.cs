using System;

namespace Yutai.Pipeline.Analysis.Classes
{
    public class PipePoint
    {
        public enum SectionPointType
        {
            sptNoType,
            sptPipe,
            sptDrawPoint,
            sptRoadBorder,
            sptMidGreen,
            sptMidRoadLine
        }

        public double x;

        public double y;

        public double z;

        public double m;

        public bool bIsFromNode;

        public int nID;

        public int nAtPipeSegID;

        public int nCode;

        public string bstrDatasetName = "";

        public string bstrPointKind = "";

        public int Red;

        public int Green;

        public int Blue;

        public string strMaterial = "";

        public string strPipeWidthHeight = "";

        private PipePoint.SectionPointType sectionPointType_0 = PipePoint.SectionPointType.sptPipe;

        public PipePoint.SectionPointType PointType
        {
            get
            {
                return this.sectionPointType_0;
            }
            set
            {
                this.sectionPointType_0 = value;
            }
        }

        public double DistanceToPipePoint(PipePoint ppDst)
        {
            double num = ppDst.x - this.x;
            double num2 = ppDst.y - this.y;
            return Math.Sqrt(num * num + num2 * num2);
        }

        public string ToStringSpati()
        {
            return string.Concat(new string[]
            {
                "{X = ",
                this.x.ToString("f3"),
                "  y = ",
                this.y.ToString("f3"),
                "  Z = ",
                this.z.ToString("f3"),
                "  M = ",
                this.m.ToString(),
                "}"
            });
        }

        public PipePoint GetDeepCopy()
        {
            return new PipePoint
            {
                x = this.x,
                y = this.y,
                z = this.z,
                m = this.m,
                bIsFromNode = this.bIsFromNode,
                bstrDatasetName = this.bstrDatasetName,
                bstrPointKind = this.bstrPointKind,
                nAtPipeSegID = this.nAtPipeSegID,
                nID = this.nID,
                nCode = this.nCode,
                Red = this.Red,
                Green = this.Green,
                Blue = this.Blue,
                PointType = this.PointType,
                strMaterial = this.strMaterial,
                strPipeWidthHeight = this.strPipeWidthHeight
            };
        }
    }
}