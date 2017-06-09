using System.Windows.Forms;
using ESRI.ArcGIS;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Common
{
    public class AoLicenseManager
    {
        private static IAoInitialize m_pAoInitialize;

        static AoLicenseManager()
        {
            old_acctor_mc();
        }

        private static esriLicenseStatus CheckOutLicenses(esriLicenseProductCode esriLicenseProductCode_0, esriLicenseExtensionCode esriLicenseExtensionCode_0)
        {
            if (m_pAoInitialize == null)
            {
               // m_pAoInitialize = new AoInitializeClass();
            }
            esriLicenseStatus status = m_pAoInitialize.IsProductCodeAvailable(esriLicenseProductCode_0);
            if (status == esriLicenseStatus.esriLicenseAvailable)
            {
                status = m_pAoInitialize.IsExtensionCodeAvailable(esriLicenseProductCode_0, esriLicenseExtensionCode_0);
                if (status == esriLicenseStatus.esriLicenseAvailable)
                {
                    status = m_pAoInitialize.Initialize(esriLicenseProductCode_0);
                    if (status == esriLicenseStatus.esriLicenseCheckedOut)
                    {
                        status = m_pAoInitialize.CheckOutExtension(esriLicenseExtensionCode_0);
                    }
                }
            }
            return status;
        }

        public static bool Initialize()
        {
            if (!RuntimeManager.Bind(ProductCode.Engine) && !RuntimeManager.Bind(ProductCode.Desktop))
            {
                return false;
            }
            return InitLicense();
        }

        private static bool InitLicense()
        {
            //m_pAoInitialize = new AoInitializeClass();
            //if (m_pAoInitialize == null)
            //{
            //    return false;
            //}
            esriLicenseProductCode[] codeArray2 = new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB, esriLicenseProductCode.esriLicenseProductCodeEngine, esriLicenseProductCode.esriLicenseProductCodeArcServer, esriLicenseProductCode.esriLicenseProductCodeAdvanced };
            esriLicenseStatus esriLicenseNotLicensed = esriLicenseStatus.esriLicenseNotLicensed;
            for (int i = 0; i < codeArray2.Length; i++)
            {
                esriLicenseNotLicensed = m_pAoInitialize.IsProductCodeAvailable(codeArray2[i]);
                if (esriLicenseNotLicensed == esriLicenseStatus.esriLicenseAvailable)
                {
                    esriLicenseNotLicensed = m_pAoInitialize.Initialize(codeArray2[i]);
                    break;
                }
            }
            switch (esriLicenseNotLicensed)
            {
                case esriLicenseStatus.esriLicenseNotLicensed:
                    MessageBox.Show("无ArcGIS Engine使用权限!");
                    return false;
            }
            esriLicenseNotLicensed = m_pAoInitialize.CheckOutExtension(esriLicenseExtensionCode.esriLicenseExtensionCode3DAnalyst);
            esriLicenseNotLicensed = m_pAoInitialize.CheckOutExtension(esriLicenseExtensionCode.esriLicenseExtensionCodeNetwork);
            esriLicenseNotLicensed = m_pAoInitialize.CheckOutExtension(esriLicenseExtensionCode.esriLicenseExtensionCodeSpatialAnalyst);
            esriLicenseNotLicensed = m_pAoInitialize.CheckOutExtension(esriLicenseExtensionCode.esriLicenseExtensionCodeDataInteroperability);
            return true;
        }

        private static void old_acctor_mc()
        {
            m_pAoInitialize = null;
        }
    }
}

