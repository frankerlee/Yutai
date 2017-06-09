using System.Collections.Generic;
using Yutai.ArcGIS.Catalog.VCT.VCT;

namespace Yutai.ArcGIS.Catalog.VCT
{
    public class VctFactory
    {
        private int int_0;
        private static string m_path;
        private VctClass vctClass_0;

        public List<ICoConvert> Create(string string_0)
        {
            if (this.vctClass_0 == null)
            {
                m_path = string.Empty;
            }
            List<ICoConvert> list = new List<ICoConvert>();
            if (m_path != string_0)
            {
                this.vctClass_0 = new VctClass(string_0);
                this.vctClass_0.CreateMap();
                m_path = string_0;
            }
            foreach (ICoLayer layer in this.vctClass_0.Layers)
            {
                list.Add(new VctLayerClass(this.vctClass_0, layer));
            }
            return list;
        }

        public ICoConvert Create(string string_0, string string_1)
        {
            if (this.vctClass_0 == null)
            {
                m_path = string.Empty;
            }
            if (m_path != string_1)
            {
                this.vctClass_0 = new VctClass(string_1);
                this.vctClass_0.CreateMap();
                m_path = string_1;
            }
            ICoLayer layer = null;
            foreach (ICoLayer layer2 in this.vctClass_0.Layers)
            {
                if (layer2.Name.ToUpper() == string_0.ToUpper())
                {
                    layer = layer2;
                    break;
                }
            }
            if (layer != null)
            {
                return new VctLayerClass(this.vctClass_0, layer);
            }
            return null;
        }
    }
}

