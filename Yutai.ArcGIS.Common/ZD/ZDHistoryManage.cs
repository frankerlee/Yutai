using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.ZD
{
	public class ZDHistoryManage
	{
		public void CreateTable(IFeatureWorkspace ifeatureWorkspace_0)
		{
			ZDRegisterTableCreate zDRegisterTableCreate = new ZDRegisterTableCreate();
			zDRegisterTableCreate.CreateTable(ifeatureWorkspace_0);
			ZDHistoryTableCreate zDHistoryTableCreate = new ZDHistoryTableCreate();
			ITable table = zDHistoryTableCreate.CreateTable(ifeatureWorkspace_0);
			try
			{
				if ((ifeatureWorkspace_0 as IWorkspace).Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
				{
					IVersionedObject3 versionedObject = table as IVersionedObject3;
					if (versionedObject != null)
					{
						versionedObject.RegisterAsVersioned3(false);
					}
				}
			}
			catch
			{
			}
		}

		public void CreateHis(IFeatureClass ifeatureClass_0)
		{
			IFeatureWorkspace featureWorkspace = (ifeatureClass_0 as IDataset).Workspace as IFeatureWorkspace;
			string[] array = (ifeatureClass_0 as IDataset).Name.Split(new char[]
			{
				'.'
			});
			featureWorkspace.CreateFeatureClass(array[array.Length - 1] + "_His", ifeatureClass_0.Fields, null, null, esriFeatureType.esriFTSimple, "shape", "");
		}
	}
}
