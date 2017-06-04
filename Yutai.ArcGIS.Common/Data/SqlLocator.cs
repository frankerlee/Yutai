using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Yutai.ArcGIS.Common.Data
{
	public class SqlLocator
	{
		private const short SQL_HANDLE_ENV = 1;

		private const short SQL_HANDLE_DBC = 2;

		private const int SQL_ATTR_ODBC_VERSION = 200;

		private const int SQL_OV_ODBC3 = 3;

		private const short SQL_SUCCESS = 0;

		private const short SQL_NEED_DATA = 99;

		private const short DEFAULT_RESULT_SIZE = 1024;

		private const string SQL_DRIVER_STR = "DRIVER=SQL SERVER";

		private SqlLocator()
		{
		}

		public static string[] GetServers()
		{
			string empty = string.Empty;
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr = IntPtr.Zero;
			StringBuilder stringBuilder = new StringBuilder("DRIVER=SQL SERVER");
			StringBuilder stringBuilder1 = new StringBuilder(1024);
			short length = (short)stringBuilder.Length;
			short num = 0;
			try
			{
				try
				{
					if (0 == SqlLocator.SQLAllocHandle(1, zero, out zero) && 0 == SqlLocator.SQLSetEnvAttr(zero, 200, (IntPtr)3, 0) && 0 == SqlLocator.SQLAllocHandle(2, zero, out intPtr) && 99 == SqlLocator.SQLBrowseConnect(intPtr, stringBuilder, length, stringBuilder1, 1024, out num))
					{
						if (1024 < num)
						{
							stringBuilder1.Capacity = num;
							if (99 != SqlLocator.SQLBrowseConnect(intPtr, stringBuilder, length, stringBuilder1, num, out num))
							{
								throw new ApplicationException("Unabled to aquire SQL Servers from ODBC driver.");
							}
						}
						empty = stringBuilder1.ToString();
						int num1 = empty.IndexOf("{") + 1;
						int num2 = empty.IndexOf("}") - num1;
						empty = ((num1 <= 0 ? true : num2 <= 0) ? string.Empty : empty.Substring(num1, num2));
					}
				}
				catch
				{
					empty = string.Empty;
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					SqlLocator.SQLFreeHandle(2, intPtr);
				}
				if (zero != IntPtr.Zero)
				{
					SqlLocator.SQLFreeHandle(1, intPtr);
				}
			}
			string[] strArrays = null;
			if (empty.Length > 0)
			{
				strArrays = empty.Split(new char[] { ',' });
			}
			return strArrays;
		}

		[DllImport("odbc32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		private static extern short SQLAllocHandle(short short_0, IntPtr intptr_0, out IntPtr intptr_1);

		[DllImport("odbc32.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		private static extern short SQLBrowseConnect(IntPtr intptr_0, StringBuilder stringBuilder_0, short short_0, StringBuilder stringBuilder_1, short short_1, out short short_2);

		[DllImport("odbc32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		private static extern short SQLFreeHandle(short short_0, IntPtr intptr_0);

		[DllImport("odbc32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		private static extern short SQLSetEnvAttr(IntPtr intptr_0, int int_0, IntPtr intptr_1, int int_1);
	}
}