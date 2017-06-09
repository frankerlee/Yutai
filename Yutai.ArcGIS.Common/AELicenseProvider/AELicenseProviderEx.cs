using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IO;
using System.Web;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.AELicenseProvider
{
	public class AELicenseProviderEx : LicenseProvider
	{
		private static Hashtable m_hash;

		static AELicenseProviderEx()
		{
			AELicenseProviderEx.old_acctor_mc();
		}

		public AELicenseProviderEx()
		{
			if (AELicenseProviderEx.m_hash.Count == 0)
			{
				AELicenseProviderEx.m_hash.Add("Yutai.Editor.EditorLicenseProviderCheck", "编辑模块");
				AELicenseProviderEx.m_hash.Add("Yutai.Catalog.CatalogLicenseProviderCheck", "数据管理库");
			}
		}

		public override License GetLicense(LicenseContext licenseContext_0, Type type_0, object object_0, bool bool_0)
		{
			License aELicense;
			if (licenseContext_0 == null)
			{
				throw new LicenseException(type_0, object_0, "License context is null.");
			}
			string savedLicenseKey = licenseContext_0.GetSavedLicenseKey(type_0, null);
			if ((savedLicenseKey == null ? true : !this.IsKeyValid(type_0, savedLicenseKey)))
			{
				switch (licenseContext_0.UsageMode)
				{
					case LicenseUsageMode.Runtime:
					{
						savedLicenseKey = this.GetLicenseFromFile(string.Concat(Application.StartupPath, "\\licences.lic"), type_0.FullName);
						if (savedLicenseKey == null || !this.IsKeyValid(type_0, savedLicenseKey))
						{
							aELicense = null;
							break;
						}
						else
						{
							licenseContext_0.SetSavedLicenseKey(type_0, savedLicenseKey);
							aELicense = new AELicense("Runtime");
							break;
						}
					}
					case LicenseUsageMode.Designtime:
					{
						ITypeResolutionService service = (ITypeResolutionService)licenseContext_0.GetService(typeof(ITypeResolutionService));
						if (service != null)
						{
							if (service.GetPathOfAssembly(type_0.Assembly.GetName()) == null)
							{
								string fullyQualifiedName = type_0.Module.FullyQualifiedName;
							}
							savedLicenseKey = "受限许可";
						}
						aELicense = new AELicense(savedLicenseKey);
						break;
					}
					default:
					{
						aELicense = null;
						break;
					}
				}
			}
			else
			{
				aELicense = new AELicense(savedLicenseKey);
			}
			return aELicense;
		}

		protected virtual string GetLicenseFromFile(string string_0, string string_1)
		{
			string str;
			try
			{
				FileStream fileStream = new FileStream(string_0, FileMode.Open, FileAccess.Read);
				try
				{
					string str1 = "";
					StreamReader streamReader = new StreamReader(fileStream);
					try
					{
						int num = -1;
						string_1 = string_1.ToLower();
						while (true)
						{
							string str2 = streamReader.ReadLine();
							string str3 = str2;
							if (str2 != null)
							{
								num = str3.IndexOf(",");
								if (num != -1 && str3.Substring(0, num).Trim().ToLower() == string_1)
								{
									str1 = str3.Substring(num + 1).Trim();
									break;
								}
							}
							else
							{
								break;
							}
						}
					}
					finally
					{
						if (streamReader != null)
						{
							((IDisposable)streamReader).Dispose();
						}
					}
					str = str1;
					return str;
				}
				catch
				{
				}
				fileStream.Close();
			}
			catch
			{
			}
			str = "";
			return str;
		}

		protected virtual string GetLicenseFromWeb(string string_0)
		{
			string str;
			FileStream fileStream = new FileStream(string.Concat(HttpRuntime.AppDomainAppPath, "\\", string_0), FileMode.Open, FileAccess.Read);
			try
			{
				StreamReader streamReader = new StreamReader(fileStream);
				try
				{
					str = streamReader.ReadLine();
				}
				finally
				{
					streamReader.Close();
				}
			}
			finally
			{
				fileStream.Close();
			}
			return str;
		}

		protected virtual bool IsKeyValid(Type type_0, string string_0)
		{
			bool flag;
			string[] str = string_0.Split(new char[] { ',' });
			object item = AELicenseProviderEx.m_hash[type_0.FullName];
			if (item == null)
			{
				item = type_0.FullName;
			}
			string str1 = "";
			if ((int)str.Length <= 1)
			{
				str1 = string.Concat(type_0.FullName, ",");
			}
			else
			{
				if (str[0].Trim().Length > 0 && str[0].Trim().ToLower() != "none")
				{
					try
					{
						DateTime dateTime = DateTime.Parse(str[0]);
						if (DateTime.Now <= dateTime)
						{
							str[0] = dateTime.ToString("yyyyddMM");
						}
						else
						{
							MessageBox.Show(string.Concat(item.ToString(), "的使用许可过期，请更新使用许可文件!"), "许可", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							flag = false;
							return flag;
						}
					}
					catch
					{
						MessageBox.Show(string.Concat("无法验证", item.ToString(), "的使用许可!"), "许可", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						flag = false;
						return flag;
					}
				}
				str1 = string.Concat(type_0.FullName, ",", str[0].Trim().ToLower(), ",");
			}
			if ((Regedit.IsValid(str1, str[3], str[1]) ? false : !Regedit.IsValidEx(str1, str[1], str[3])))
			{
				MessageBox.Show(string.Concat("无法验证", item.ToString(), "的使用许可!"), "许可", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				flag = false;
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		private static void old_acctor_mc()
		{
			AELicenseProviderEx.m_hash = new Hashtable();
		}
	}
}