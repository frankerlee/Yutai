namespace Yutai.ArcGIS.Common.DXF
{
    public class DXFSolid : DXFFigure
    {
        public DXFSolid()
        {
        }

        public DXFSolid(DXFData aData) : base(aData)
        {
        }

        public override void ExportAsDXF(DXFExport ADXFExport)
        {
            ADXFExport.AddName("SOLID", "AcDbTrace");
            ADXFExport.AddColor(this.data);
            ADXFExport.Add3DPoint(10, this.data.point);
            ADXFExport.Add3DPoint(11, this.data.point1);
            ADXFExport.Add3DPoint(12, this.data.point2);
            ADXFExport.Add3DPoint(13, this.data.point3);
        }
    }
}