using System.Collections;

namespace Yutai.ArcGIS.Common.DXF
{
    public class DXFFigure
    {
        public DXFData data;

        private string layer;

        public int Color
        {
            get { return this.data.color; }
            set { this.data.color = value; }
        }

        public string Layer
        {
            get { return this.layer; }
            set { this.layer = value; }
        }

        public DXFFigure()
        {
            this.layer = "0";
            this.data = new DXFData();
        }

        public DXFFigure(DXFData dt)
        {
            this.layer = "0";
            this.data = (DXFData) dt.Clone();
        }

        public virtual void ExportAsDXF(DXFExport ADXFExport)
        {
        }

        public virtual void ExportBlockAsDXF(DXFExport ADXFExport)
        {
        }

        public virtual bool IntersecRect(Rect aRect)
        {
            return false;
        }

        public virtual void ParseToLines(ArrayList NewElemes)
        {
        }

        protected DXFPoint ToDXFPoint(float X, float Y)
        {
            return new DXFPoint(X, Y, 0f);
        }
    }
}