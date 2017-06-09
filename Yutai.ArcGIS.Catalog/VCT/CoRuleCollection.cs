using System;
using System.Collections.Generic;

namespace Yutai.ArcGIS.Catalog.VCT
{
    public class CoRuleCollection : List<CoRuleClass>
    {
        private string string_0;

        public CoRuleCollection()
        {
            this.string_0 = Guid.NewGuid().ToString();
        }

        public CoRuleCollection(string string_1)
        {
            this.string_0 = Guid.NewGuid().ToString();
            this.string_0 = string_1;
        }

        public string ID
        {
            get
            {
                return this.string_0;
            }
        }
    }
}

