using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.CodeDomainEx;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class DomainWrap1
    {
        private CodeDomainEx codeDomainEx_0;
        private IDomain idomain_0;

        public DomainWrap1()
        {
            this.idomain_0 = null;
            this.codeDomainEx_0 = null;
        }

        public DomainWrap1(IDomain idomain_1)
        {
            this.idomain_0 = null;
            this.codeDomainEx_0 = null;
            this.idomain_0 = idomain_1;
        }

        public DomainWrap1(CodeDomainEx codeDomainEx_1)
        {
            this.idomain_0 = null;
            this.codeDomainEx_0 = null;
            this.codeDomainEx_0 = codeDomainEx_1;
        }

        public override string ToString()
        {
            if (this.idomain_0 != null)
            {
                return this.idomain_0.Name;
            }
            if (this.codeDomainEx_0 != null)
            {
                return this.codeDomainEx_0.Name;
            }
            return "";
        }

        public IDomain Domain
        {
            get
            {
                return this.idomain_0;
            }
        }

        public CodeDomainEx DomainEx
        {
            get
            {
                return this.codeDomainEx_0;
            }
        }
    }
}

