using System.Collections;
using System.Text;

namespace Yutai.ArcGIS.Common.Geodatabase
{
    public class Features : EnumImpl
    {
        private readonly Layer layer_0;

        public Feature this[int int_1]
        {
            get
            {
                Feature feature;
                if (int_1 != -1)
                {
                    feature = this.CreateFeature(int_1);
                }
                else
                {
                    feature = null;
                }
                return feature;
            }
        }

        public Layer Layer
        {
            get { return this.layer_0; }
        }

        public Features(Layer layer_1) : base(MiApi.mitab_c_get_feature_count(layer_1.Handle))
        {
            this.layer_0 = layer_1;
        }

        public virtual Feature CreateFeature(int int_1)
        {
            return new Feature(this.layer_0, int_1);
        }

        public override IEnumerator GetEnumerator()
        {
            return new FeaturesEnum(this, this.layer_0);
        }

        public Feature GetFirst()
        {
            Feature item = this[MiApi.mitab_c_next_feature_id(this.layer_0.Handle, -1)];
            return item;
        }

        public override object GetObj(int int_1)
        {
            return this[int_1];
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(string.Concat("Feature Count: ", base.Count, "\n"));
            foreach (Feature feature in this)
            {
                stringBuilder.Append(string.Concat(feature.ToString(), "\n"));
            }
            return stringBuilder.ToString();
        }
    }
}