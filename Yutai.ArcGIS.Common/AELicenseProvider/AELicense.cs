using System.ComponentModel;

namespace Yutai.ArcGIS.Common.AELicenseProvider
{
    public class AELicense : License
    {
        private string string_0 = null;

        public override string LicenseKey
        {
            get { return this.string_0; }
        }

        public AELicense(string string_1)
        {
            this.string_0 = string_1;
        }

        public override void Dispose()
        {
        }
    }
}