using System.Text;

namespace Yutai.ArcGIS.Common.Geodatabase
{
    public class Parts : EnumImpl
    {
        public readonly Feature Feature;

        public Part this[int int_1]
        {
            get
            {
                Part part;
                if (int_1 < base.Count)
                {
                    part = this.CreatePart(int_1);
                }
                else
                {
                    part = null;
                }
                return part;
            }
        }

        protected internal Parts(Feature feature_0) : base(MiApi.mitab_c_get_parts(feature_0.Handle))
        {
            this.Feature = feature_0;
        }

        protected internal virtual Part CreatePart(int int_1)
        {
            return new Part(this.Feature, int_1);
        }

        public override object GetObj(int int_1)
        {
            return this[int_1];
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(string.Concat("Part Count: ", base.Count, "\n"));
            foreach (Part part in this)
            {
                stringBuilder.Append(string.Concat(part.ToString(), "\n"));
            }
            return stringBuilder.ToString();
        }
    }
}