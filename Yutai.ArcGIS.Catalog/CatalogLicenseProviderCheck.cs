using System.ComponentModel;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.AELicenseProvider;

namespace Yutai.ArcGIS.Catalog
{
    [LicenseProvider(typeof(AELicenseProviderEx))]
    internal class CatalogLicenseProviderCheck
    {
        private static CatalogLicenseProviderCheck _licenceseCheck;
        private License license_0 = null;
        private static bool m_initOk;

        static CatalogLicenseProviderCheck()
        {
            old_acctor_mc();
        }

        internal CatalogLicenseProviderCheck()
        {
            this.license_0 = LicenseManager.Validate(typeof(CatalogLicenseProviderCheck), this);
        }

        internal static bool Check()
        {
            if (!m_initOk)
            {
                Init();
                return (_licenceseCheck != null);
            }
            if (_licenceseCheck == null)
            {
                MessageBox.Show("无法验证数据管理模块的的使用许可!", "许可", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return (_licenceseCheck != null);
        }

        internal static void Init()
        {
            m_initOk = true;
            try
            {
                _licenceseCheck = new CatalogLicenseProviderCheck();
            }
            catch
            {
            }
        }

        private static void old_acctor_mc()
        {
            _licenceseCheck = null;
            m_initOk = false;
        }
    }
}

