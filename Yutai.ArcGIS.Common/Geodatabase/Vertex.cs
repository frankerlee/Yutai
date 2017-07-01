namespace Yutai.ArcGIS.Common.Geodatabase
{
    public class Vertex
    {
        public readonly double X;

        public readonly double Y;

        protected internal Vertex(double double_0, double double_1)
        {
            this.X = double_0;
            this.Y = double_1;
        }

        public override string ToString()
        {
            string str = string.Concat(this.X, ", ", this.Y);
            return str;
        }
    }
}