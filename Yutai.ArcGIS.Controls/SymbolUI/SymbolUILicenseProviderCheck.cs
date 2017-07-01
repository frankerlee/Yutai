using System;
using System.ComponentModel;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.AELicenseProvider;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    //  [LicenseProvider(typeof(AELicenseProviderEx))]
    internal class SymbolUILicenseProviderCheck
    {
        private static SymbolUILicenseProviderCheck _licenceseCheck = null;
        private License _license = null;
        private static bool m_initOk = false;

        internal SymbolUILicenseProviderCheck()
        {
            //  this._license = LicenseManager.Validate(typeof(SymbolUILicenseProviderCheck), this);
        }

        internal static bool Check()
        {
            return true;
            if (!m_initOk)
            {
                Init();
                return (_licenceseCheck != null);
            }
            if (_licenceseCheck == null)
            {
                MessageBox.Show("无法验证授权许可!");
            }
            return (_licenceseCheck != null);
        }

        internal static void Init()
        {
            m_initOk = true;
            try
            {
                _licenceseCheck = new SymbolUILicenseProviderCheck();
            }
            catch (Exception)
            {
            }
        }
    }
}