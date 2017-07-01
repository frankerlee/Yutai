using System;

namespace Yutai.ArcGIS.Common.DXF
{
    public class DXFPolyBezier : DXFPolyline
    {
        public DXFPolyBezier()
        {
        }

        public DXFPolyBezier(DXFData aData, int aIndex) : base(aData, aIndex)
        {
        }

        public override void ExportAsDXF(DXFExport ADXFExport)
        {
            int i;
            if ((this.data.points.Count - 4)%3 != 0)
            {
                base.ExportAsDXF(ADXFExport);
                return;
            }
            int count = this.data.points.Count - 1 + (int) Math.Floor((double) (this.data.points.Count/3));
            float single = 1f/(float) count;
            ADXFExport.AddName("SPLINE", "AcDbSpline");
            ADXFExport.AddColor(this.data);
            ADXFExport.AddThickness(this.data);
            ADXFExport.Add3DPoint(210, new DXFPoint(0f, 0f, 1f));
            ADXFExport.AddInt(70, 8);
            ADXFExport.AddInt(71, 3);
            ADXFExport.AddInt(72, count + DXFTables.cnstAmount);
            ADXFExport.AddInt(73, count);
            ADXFExport.AddInt(74, 0);
            ADXFExport.AddFloat(42, 1E-07f);
            ADXFExport.AddFloat(43, 1E-07f);
            int j = 0;
            float single1 = 0f;
            while (j < count)
            {
                for (i = 0; i < DXFTables.cnstAmount; i++)
                {
                    ADXFExport.AddFloat(40, single1);
                }
                single1 = single1 + single;
                j = j + DXFTables.cnstAmount;
            }
            single1 = 1f;
            for (i = 0; i < DXFTables.cnstAmount; i++)
            {
                ADXFExport.AddFloat(40, single1);
            }
            for (j = 0; j < this.data.points.Count; j++)
            {
                ADXFExport.Add3DPoint(10, (DXFPoint) this.data.points[j]);
                if (j%3 == 0 && j != 0 && j != this.data.points.Count - 1)
                {
                    ADXFExport.Add3DPoint(10, (DXFPoint) this.data.points[j]);
                }
            }
        }
    }
}