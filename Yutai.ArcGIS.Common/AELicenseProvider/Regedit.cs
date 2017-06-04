using System;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using Microsoft.Win32;
using Yutai.Shared;

namespace Yutai.ArcGIS.Common.AELicenseProvider
{
	internal class Regedit
	{
		public Regedit()
		{
		}

		private static string DecryptNet(string string_0, string string_1)
		{
			string str = "";
			byte[] bytes = new byte[0];
			byte[] numArray = new byte[] { 18, 52, 86, 120, 144, 171, 205, 239 };
			byte[] numArray1 = numArray;
			byte[] numArray2 = new byte[string_0.Length + 1];
			try
			{
				bytes = Encoding.UTF8.GetBytes(string_1.Substring(0, 8));
				DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
				numArray2 = Convert.FromBase64String(string_0);
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(bytes, numArray1), CryptoStreamMode.Write);
				cryptoStream.Write(numArray2, 0, (int)numArray2.Length);
				cryptoStream.FlushFinalBlock();
				str = Encoding.UTF8.GetString(memoryStream.ToArray());
			}
			catch
			{
			}
			return str;
		}

		public static string DecryptTextNet(string string_0)
		{
			return Regedit.DecryptNet(string_0, "&%#@?,:*");
		}

		public static string Encrypt(string string_0)
		{
			return "";
		}

		private static string EncryptNet(string string_0, string string_1)
		{
			string base64String = "";
			byte[] bytes = new byte[0];
			byte[] numArray = new byte[] { 18, 52, 86, 120, 144, 171, 205, 239 };
			byte[] numArray1 = numArray;
			try
			{
				bytes = Encoding.UTF8.GetBytes(string_1.Substring(0, 8));
				DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
				byte[] bytes1 = Encoding.UTF8.GetBytes(string_0);
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(bytes, numArray1), CryptoStreamMode.Write);
				cryptoStream.Write(bytes1, 0, (int)bytes1.Length);
				cryptoStream.FlushFinalBlock();
				base64String = Convert.ToBase64String(memoryStream.ToArray());
			}
			catch
			{
			}
			return base64String;
		}

		public static string EncryptPassword(string string_0, string string_1)
		{
			string str;
			if (string_1.ToLower() != "sha1")
			{
				str = (string_1.ToLower() != "md5" ? "" : FormsAuthentication.HashPasswordForStoringInConfigFile(string_0, "md5"));
			}
			else
			{
				str = FormsAuthentication.HashPasswordForStoringInConfigFile(string_0, "sha1");
			}
			return str;
		}

		public static string EncryptTextNet(string string_0)
		{
			return Regedit.EncryptNet(string_0, "&%#@?,:*");
		}

		public static string GetSysManagementSerialNumber()
		{
			string str = "";
			foreach (ManagementObject instance in (new ManagementClass("Win32_NetworkAdapterConfiguration")).GetInstances())
			{
				if (!(bool)instance["IPEnabled"])
				{
					continue;
				}
				str = instance["MacAddress"].ToString().Trim();
			}
			return str;
		}

		public static string GetSysManagementSerialNumber(string string_0)
		{
			string str;
			RegistryKey localMachine = Registry.LocalMachine;
			localMachine = localMachine.OpenSubKey("SOFTWARE");
			localMachine = localMachine.OpenSubKey(string_0);
			if (localMachine != null)
			{
				object value = localMachine.GetValue("AELicenseInfo");
				if (value == null)
				{
					str = "ii";
					return str;
				}
				str = value.ToString();
				return str;
			}
			str = "ii";
			return str;
		}

		public static string GetSysManagementSerialNumber64(string string_0)
		{
			string str = "HKEY_LOCAL_MACHINE";
			string str1 = string.Concat("SOFTWARE\\", string_0);
			string str2 = "AELicenseInfo";
			string empty = string.Empty;
			return RegistryTools.Get64BitRegistryKey(str, str1, str2);
		}

		public static string Is64or32System()
		{
			string str = "";
			if (RegistryTools.GetPlatform() == RegistryTools.Platform.X64)
			{
				str = "64";
			}
			else if (RegistryTools.GetPlatform() == RegistryTools.Platform.X86)
			{
				str = "32";
			}
			return str;
		}

		public static bool IsValid(string string_0, string string_1, string string_2)
		{
			bool flag;
			string str = "";
			string str1 = Regedit.Is64or32System();
			if (str1 == "64")
			{
				str = string.Concat(string_2, ",", Regedit.GetSysManagementSerialNumber64(string_2));
			}
			else if (str1 == "32")
			{
				str = string.Concat(string_2, ",", Regedit.GetSysManagementSerialNumber(string_2));
			}
			flag = (!string_1.Equals(Regedit.EncryptPassword(string.Concat(string_0, Regedit.EncryptTextNet(str)), "md5")) ? false : true);
			return flag;
		}

		public static bool IsValidEx(string string_0, string string_1, string string_2)
		{
			bool flag;
			ManagementObjectCollection instances = (new ManagementClass("Win32_NetworkAdapterConfiguration")).GetInstances();
			bool flag1 = false;
			foreach (ManagementObject instance in instances)
			{
				if (!(bool)instance["IPEnabled"] || !string_2.Equals(Regedit.EncryptPassword(string.Concat(string_0, instance["MacAddress"].ToString().Trim()), "md5")))
				{
					continue;
				}
				flag = true;
				return flag;
			}
			flag = (string_1 == "http://www.linjon.cn" ? flag1 : false);
			return flag;
		}
	}
}