using System;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.Geodatabase
{
	public class GeodatabaseTools
	{
		public GeodatabaseTools()
		{
		}

		public static bool GetGeoDatasetPrecision(IGeodatabaseRelease igeodatabaseRelease_0)
		{
			bool flag;
			bool flag1 = true;
			if (igeodatabaseRelease_0 == null)
			{
				flag1 = false;
			}
			else
			{
				try
				{
					if (igeodatabaseRelease_0.MajorVersion >= 3)
					{
						flag = false;
					}
					else
					{
						flag = (igeodatabaseRelease_0.MajorVersion < 2 ? true : igeodatabaseRelease_0.MinorVersion < 2);
					}
					flag1 = (flag ? false : true);
				}
				catch
				{
				}
			}
			return flag1;
		}

		public static IWorkspace OpenWorkspace(IPropertySet ipropertySet_0)
		{
			object obj;
			object obj1;
			IWorkspaceFactory sdeWorkspaceFactoryClass;
			IWorkspace workspace = null;
			if (ipropertySet_0.Count > 2)
			{
				sdeWorkspaceFactoryClass = new SdeWorkspaceFactory();
				try
				{
					workspace = sdeWorkspaceFactoryClass.Open(ipropertySet_0, 0);
				}
				catch (Exception exception)
				{
				}
			}
			else
			{
				string str = "";
				string lower = "";
				ipropertySet_0.GetAllProperties(out obj, out obj1);
				if (((string[])obj)[0] == "DATABASE")
				{
					str = ((object[])obj1)[0].ToString();
					lower = Path.GetExtension(str).ToLower();
				}
				if (lower == ".mdb")
				{
					sdeWorkspaceFactoryClass = new AccessWorkspaceFactory();
					try
					{
						workspace = sdeWorkspaceFactoryClass.Open(ipropertySet_0, 0);
					}
					catch (Exception exception1)
					{
						MessageBox.Show(exception1.Message);
					}
				}
				else if (lower == ".gdb")
				{
					sdeWorkspaceFactoryClass = new FileGDBWorkspaceFactory();
					try
					{
						workspace = sdeWorkspaceFactoryClass.Open(ipropertySet_0, 0);
					}
					catch (Exception exception2)
					{
						MessageBox.Show(exception2.Message);
					}
				}
			}
			return workspace;
		}
	}
}