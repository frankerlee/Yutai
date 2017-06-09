using System.ComponentModel;
using Yutai.ArcGIS.Common.AELicenseProvider;

namespace Yutai.ArcGIS.Common
{
    [LicenseProvider(typeof(AELicenseProviderEx))]
    internal class UtilityLicenseProviderCheck
    {
        private static UtilityLicenseProviderCheck _licenceseCheck;

        private static bool m_initOk;

        private License license_0 = null;

        static UtilityLicenseProviderCheck()
        {
            // 注意: 此类型已标记为 'beforefieldinit'.
            UtilityLicenseProviderCheck.old_acctor_mc();
        }

        internal UtilityLicenseProviderCheck()
        {
          //  this.license_0 = LicenseManager.Validate(typeof(UtilityLicenseProviderCheck), this);
        }

        internal static void Init()
        {
            UtilityLicenseProviderCheck.m_initOk = true;
            try
            {
                UtilityLicenseProviderCheck._licenceseCheck = new UtilityLicenseProviderCheck();
            }
            catch
            {
            }
        }

        internal static bool Check()
        {
            return true;
            if (!UtilityLicenseProviderCheck.m_initOk)
            {
                UtilityLicenseProviderCheck.Init();
            }
            bool result;
            if (UtilityLicenseProviderCheck._licenceseCheck == null)
            {
                System.Windows.Forms.MessageBox.Show("无法验证基础模块的的使用许可!", "许可", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk);
                result = false;
            }
            else
            {
                result = (UtilityLicenseProviderCheck._licenceseCheck != null);
            }
            return result;
        }

        private static void old_acctor_mc()
        {
            UtilityLicenseProviderCheck._licenceseCheck = null;
            UtilityLicenseProviderCheck.m_initOk = false;
        }
    }
}