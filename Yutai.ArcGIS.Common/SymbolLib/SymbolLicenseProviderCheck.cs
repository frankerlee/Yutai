using System.ComponentModel;
using Yutai.ArcGIS.Common.AELicenseProvider;

namespace Yutai.ArcGIS.Common.SymbolLib
{
	[LicenseProvider(typeof(AELicenseProviderEx))]
	internal class SymbolLicenseProviderCheck
	{
		private static SymbolLicenseProviderCheck _licenceseCheck;

		private static bool m_initOk;

		private License license_0 = null;

		static SymbolLicenseProviderCheck()
		{
			// 注意: 此类型已标记为 'beforefieldinit'.
			SymbolLicenseProviderCheck.old_acctor_mc();
		}

		internal SymbolLicenseProviderCheck()
		{
			this.license_0 = LicenseManager.Validate(typeof(SymbolLicenseProviderCheck), this);
		}

		internal static void Init()
		{
			SymbolLicenseProviderCheck.m_initOk = true;
			try
			{
				SymbolLicenseProviderCheck._licenceseCheck = new SymbolLicenseProviderCheck();
			}
			catch
			{
			}
		}

		internal static bool Check()
		{
			bool result;
			if (!SymbolLicenseProviderCheck.m_initOk)
			{
				SymbolLicenseProviderCheck.Init();
				result = (SymbolLicenseProviderCheck._licenceseCheck != null);
			}
			else
			{
				if (SymbolLicenseProviderCheck._licenceseCheck == null)
				{
					System.Windows.Forms.MessageBox.Show("无法验证符号库模块的的使用许可!", "许可", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk);
				}
				result = (SymbolLicenseProviderCheck._licenceseCheck != null);
			}
			return result;
		}

		private static void old_acctor_mc()
		{
			SymbolLicenseProviderCheck._licenceseCheck = null;
			SymbolLicenseProviderCheck.m_initOk = false;
		}
	}
}
