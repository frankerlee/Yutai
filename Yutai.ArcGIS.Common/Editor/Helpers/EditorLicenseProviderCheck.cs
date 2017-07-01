using System.ComponentModel;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.AELicenseProvider;

namespace Yutai.ArcGIS.Common.Editor.Helpers
{
    [LicenseProvider(typeof(AELicenseProviderEx))]
    public class EditorLicenseProviderCheck
    {
        private static EditorLicenseProviderCheck _licenceseCheck;

        private static bool m_initOk;

        private License license_0 = null;

        static EditorLicenseProviderCheck()
        {
            EditorLicenseProviderCheck.old_acctor_mc();
        }

        internal EditorLicenseProviderCheck()
        {
            //	this.license_0 = LicenseManager.Validate(typeof(EditorLicenseProviderCheck), this);
        }

        public static bool Check()
        {
            return true;
            if (!EditorLicenseProviderCheck.m_initOk)
            {
                EditorLicenseProviderCheck.Init();
            }
            if (EditorLicenseProviderCheck._licenceseCheck == null)
            {
                MessageBox.Show("无法验证编辑模块的的使用许可!", "许可", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return EditorLicenseProviderCheck._licenceseCheck != null;
        }

        internal static void Init()
        {
            EditorLicenseProviderCheck.m_initOk = true;
            try
            {
                EditorLicenseProviderCheck._licenceseCheck = new EditorLicenseProviderCheck();
            }
            catch
            {
            }
        }

        private static void old_acctor_mc()
        {
            EditorLicenseProviderCheck._licenceseCheck = null;
            EditorLicenseProviderCheck.m_initOk = false;
        }
    }
}