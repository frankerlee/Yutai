using System.ComponentModel;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.AELicenseProvider;

namespace Yutai.ArcGIS.Common.Carto
{
    [LicenseProvider(typeof(AELicenseProviderEx))]
    public class CartoLicenseProviderCheck
    {
        private static CartoLicenseProviderCheck _licenceseCheck;

        private static bool m_initOk;

        private License license_0 = null;

        static CartoLicenseProviderCheck()
        {
            CartoLicenseProviderCheck.old_acctor_mc();
        }

        internal CartoLicenseProviderCheck()
        {
            //this.license_0 = LicenseManager.Validate(typeof(CartoLicenseProviderCheck), this);
        }

        public static bool Check()
        {
            return true;
            if (!CartoLicenseProviderCheck.m_initOk)
            {
                CartoLicenseProviderCheck.Init();
            }
            if (CartoLicenseProviderCheck._licenceseCheck == null)
            {
                MessageBox.Show("无法验证制图模块的的使用许可!", "许可", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return CartoLicenseProviderCheck._licenceseCheck != null;
        }

        internal static void Init()
        {
            CartoLicenseProviderCheck.m_initOk = true;
            try
            {
                CartoLicenseProviderCheck._licenceseCheck = new CartoLicenseProviderCheck();
            }
            catch
            {
            }
        }

        private static void old_acctor_mc()
        {
            CartoLicenseProviderCheck._licenceseCheck = null;
            CartoLicenseProviderCheck.m_initOk = false;
        }
    }
}