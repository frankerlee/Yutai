using System.IO;

namespace Yutai.ArcGIS.Common.Data
{
	public class ClsReadWriteTxt
	{
		public ClsReadWriteTxt()
		{
		}

		public void AppendTextToFile(string string_0, string string_1)
		{
			if (!File.Exists(string_0))
			{
				this.CreateTextFile(string_0);
			}
			StreamWriter streamWriter = File.AppendText(string_0);
			try
			{
				streamWriter.WriteLine(string_1);
				streamWriter.Close();
			}
			catch
			{
				streamWriter.Close();
			}
		}

		public bool CreateTextFile(string string_0)
		{
			bool flag = true;
			if (File.Exists(string_0))
			{
				try
				{
					File.Delete(string_0);
				}
				catch
				{
				}
			}
			try
			{
				File.Create(string_0).Close();
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		public bool DeleteTextFile(string string_0)
		{
			bool flag = true;
			if (File.Exists(string_0))
			{
				try
				{
					File.Delete(string_0);
				}
				catch
				{
					flag = false;
				}
			}
			return flag;
		}
	}
}