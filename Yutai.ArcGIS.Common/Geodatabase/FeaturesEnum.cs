namespace Yutai.ArcGIS.Common.Geodatabase
{
    public class FeaturesEnum : IndexedEnum
    {
        private readonly Layer layer_0;

        internal FeaturesEnum(IObjectProvider iobjectProvider_1, Layer layer_1) : base(iobjectProvider_1)
        {
            this.layer_0 = layer_1;
        }

        public override bool MoveNext()
        {
            int num = MiApi.mitab_c_next_feature_id(this.layer_0.Handle, this.eIdx);
            int num1 = num;
            this.eIdx = num;
            return num1 != -1;
        }
    }
}