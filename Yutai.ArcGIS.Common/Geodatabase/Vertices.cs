using System.Text;

namespace Yutai.ArcGIS.Common.Geodatabase
{
    public class Vertices : EnumImpl
    {
        public readonly Part Part;

        public virtual Vertex this[int int_1]
        {
            get
            {
                Vertex vertex;
                if (int_1 < base.Count)
                {
                    vertex =
                        this.CreateVertex(MiApi.mitab_c_get_vertex_x(this.Part.Feature.Handle, this.Part.Index, int_1),
                            MiApi.mitab_c_get_vertex_y(this.Part.Feature.Handle, this.Part.Index, int_1));
                }
                else
                {
                    vertex = null;
                }
                return vertex;
            }
        }

        protected internal Vertices(Part part_0)
            : base(MiApi.mitab_c_get_vertex_count(part_0.Feature.Handle, part_0.Index))
        {
            this.Part = part_0;
        }

        protected internal virtual Vertex CreateVertex(double double_0, double double_1)
        {
            return new Vertex(double_0, double_1);
        }

        public override object GetObj(int int_1)
        {
            return this[int_1];
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Vertex vertex in this)
            {
                stringBuilder.Append(string.Concat(vertex, "\t"));
            }
            return stringBuilder.ToString();
        }
    }
}