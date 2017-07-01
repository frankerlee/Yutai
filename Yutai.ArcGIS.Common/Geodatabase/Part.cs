namespace Yutai.ArcGIS.Common.Geodatabase
{
    public class Part
    {
        public readonly Feature Feature;

        public readonly Vertices Vertices;

        public readonly int Index;

        protected internal Part(Feature feature_0, int int_0)
        {
            this.Index = int_0;
            this.Feature = feature_0;
            this.Vertices = this.CreateVertices(this);
        }

        protected internal virtual Vertices CreateVertices(Part part_0)
        {
            return new Vertices(this);
        }

        public override string ToString()
        {
            object[] index = new object[] {"Part: ", this.Index, "\nVertices:\n", this.Vertices.ToString()};
            return string.Concat(index);
        }
    }
}