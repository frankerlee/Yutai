using System.ComponentModel;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.AELicenseProvider;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    [LicenseProvider(typeof(AELicenseProviderEx))]
    internal class NetworkLibLicenseProviderCheck
    {
        private static NetworkLibLicenseProviderCheck _licenceseCheck = null;
        private License _license = null;
        private static bool m_initOk = false;

        internal NetworkLibLicenseProviderCheck()
        {
            this._license = LicenseManager.Validate(typeof(NetworkLibLicenseProviderCheck), this);
        }

        internal static bool Check()
        {
            if (!m_initOk)
            {
                Init();
            }
            if (_licenceseCheck == null)
            {
                MessageBox.Show("无法验证网络分析模块的的使用许可!", "许可", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return (_licenceseCheck != null);
        }

        internal static void Init()
        {
            m_initOk = true;
            try
            {
                _licenceseCheck = new NetworkLibLicenseProviderCheck();
            }
            catch
            {
            }
        }
    }
}

