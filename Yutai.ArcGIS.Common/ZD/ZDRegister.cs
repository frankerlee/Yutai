using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Geodatabase;

namespace Yutai.ArcGIS.Common.ZD
{
    public class ZDRegister
    {
        public static bool Register(IFeatureClass ifeatureClass_0, string string_0)
        {
            bool result;
            if (ZDRegister.IsZDFeatureClass(ifeatureClass_0))
            {
                result = false;
            }
            else
            {
                string[] array = (ifeatureClass_0 as IDataset).Name.Split(new char[]
                {
                    '.'
                });
                ZDRegisterTable zDRegisterTable = new ZDRegisterTable();
                ITable table = AppConfigInfo.OpenTable("ZDInfos");
                IFeatureWorkspace featureWorkspace = (ifeatureClass_0 as IDataset).Workspace as IFeatureWorkspace;
                string finalName = WorkspaceOperator.GetFinalName2(featureWorkspace as IWorkspace,
                    esriDatasetType.esriDTFeatureClass, "", array[array.Length - 1], "_His");
                IRow row = table.CreateRow();
                RowOperator.SetFieldValue(row, zDRegisterTable.FeatureClassNameField, array[array.Length - 1]);
                RowOperator.SetFieldValue(row, zDRegisterTable.ZDBHFieldName, string_0);
                RowOperator.SetFieldValue(row, zDRegisterTable.RegisterDateFieldName, System.DateTime.Now);
                RowOperator.SetFieldValue(row, zDRegisterTable.HistoryFeatureClassName, finalName);
                string workspaceConnectInfo =
                    WorkspaceOperator.GetWorkspaceConnectInfo((ifeatureClass_0 as IDataset).Workspace);
                RowOperator.SetFieldValue(row, zDRegisterTable.GDBConnectInfoName, workspaceConnectInfo);
                RowOperator.SetFieldValue(row, zDRegisterTable.GuidName, System.Guid.NewGuid().ToString());
                row.Store();
                (ifeatureClass_0 as IDataset).Name.Split(new char[]
                {
                    '.'
                });
                IFields fields = (ifeatureClass_0.Fields as IClone).Clone() as IFields;
                IField field = new ESRI.ArcGIS.Geodatabase.Field();
                (field as IFieldEdit).Name_2 = "OriginOID_";
                (field as IFieldEdit).AliasName_2 = "原始宗地编号";
                (field as IFieldEdit).Type_2 = esriFieldType.esriFieldTypeInteger;
                (fields as IFieldsEdit).AddField(field);
                IFeatureClass featureClass = featureWorkspace.CreateFeatureClass(finalName, fields, null, null,
                    esriFeatureType.esriFTSimple, "shape", "");
                if ((featureWorkspace as IWorkspace).Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                {
                    IVersionedObject3 versionedObject = featureClass as IVersionedObject3;
                    if (versionedObject != null)
                    {
                        versionedObject.RegisterAsVersioned3(false);
                    }
                }
                result = true;
            }
            return result;
        }

        public static bool UnRegister(IFeatureClass ifeatureClass_0)
        {
            new ZDRegisterTable();
            ITable table = AppConfigInfo.OpenTable("ZDInfos");
            bool result;
            if (table == null)
            {
                result = false;
            }
            else
            {
                IQueryFilter queryFilter = new QueryFilter();
                string[] array = (ifeatureClass_0 as IDataset).Name.Split(new char[]
                {
                    '.'
                });
                string workspaceConnectInfo =
                    WorkspaceOperator.GetWorkspaceConnectInfo((ifeatureClass_0 as IDataset).Workspace);
                queryFilter.WhereClause = string.Format("FeatureClassName='{0}' and GDBConnectInfo='{1}'",
                    array[array.Length - 1], workspaceConnectInfo);
                string arg = "";
                string name = "";
                using (ComReleaser comReleaser = new ComReleaser())
                {
                    ICursor cursor = table.Search(queryFilter, false);
                    comReleaser.ManageLifetime(cursor);
                    IRow row = cursor.NextRow();
                    if (row != null)
                    {
                        int num = table.FindField("HistoryFeatureClass");
                        if (num != -1)
                        {
                            name = row.get_Value(num).ToString();
                        }
                        num = table.FindField("Guid");
                        if (num != -1)
                        {
                            arg = row.get_Value(num).ToString();
                        }
                        row.Delete();
                    }
                }
                table = AppConfigInfo.OpenTable("ZDHistory");
                queryFilter = new QueryFilter();
                queryFilter.WhereClause = string.Format("ZDRegisterGuid='{0}'", arg);
                IWorkspaceEdit workspaceEdit = AppConfigInfo.GetWorkspace() as IWorkspaceEdit;
                bool flag = false;
                if (!workspaceEdit.IsBeingEdited())
                {
                    flag = true;
                    workspaceEdit.StartEditing(true);
                }
                workspaceEdit.StartEditOperation();
                table.DeleteSearchedRows(queryFilter);
                workspaceEdit.StopEditOperation();
                workspaceEdit.StopEditing(true);
                if (!flag)
                {
                    workspaceEdit.StartEditing(true);
                }
                try
                {
                    IFeatureWorkspace featureWorkspace = (ifeatureClass_0 as IDataset).Workspace as IFeatureWorkspace;
                    IFeatureClass featureClass = featureWorkspace.OpenFeatureClass(name);
                    (featureClass as IDataset).Delete();
                }
                catch (System.Exception)
                {
                }
                result = true;
            }
            return result;
        }

        public static bool IsZDFeatureClass(IFeatureClass ifeatureClass_0)
        {
            bool result;
            if (ifeatureClass_0 == null)
            {
                result = false;
            }
            else
            {
                ITable table = null;
                try
                {
                    table = AppConfigInfo.OpenTable("ZDInfos");
                }
                catch
                {
                }
                if (table == null)
                {
                    try
                    {
                        ZDHistoryManage zDHistoryManage = new ZDHistoryManage();
                        zDHistoryManage.CreateTable(AppConfigInfo.GetWorkspace() as IFeatureWorkspace);
                        table = AppConfigInfo.OpenTable("ZDInfos");
                    }
                    catch (System.Exception)
                    {
                    }
                }
                if (table == null)
                {
                    result = false;
                }
                else
                {
                    string workspaceConnectInfo =
                        WorkspaceOperator.GetWorkspaceConnectInfo((ifeatureClass_0 as IDataset).Workspace);
                    IQueryFilter queryFilter = new QueryFilter();
                    string[] array = (ifeatureClass_0 as IDataset).Name.Split(new char[]
                    {
                        '.'
                    });
                    queryFilter.WhereClause = string.Format("FeatureClassName='{0}' and GDBConnectInfo='{1}'",
                        array[array.Length - 1], workspaceConnectInfo);
                    try
                    {
                        int num = table.RowCount(queryFilter);
                        result = (num == 1);
                        return result;
                    }
                    catch (System.Exception)
                    {
                    }
                    result = false;
                }
            }
            return result;
        }

        public static string GetRegisterZDGuid(IFeatureClass ifeatureClass_0)
        {
            string result;
            if (ifeatureClass_0 == null)
            {
                result = "";
            }
            else
            {
                ITable table = null;
                try
                {
                    table = AppConfigInfo.OpenTable("ZDInfos");
                }
                catch
                {
                }
                if (table == null)
                {
                    try
                    {
                        ZDHistoryManage zDHistoryManage = new ZDHistoryManage();
                        zDHistoryManage.CreateTable(AppConfigInfo.GetWorkspace() as IFeatureWorkspace);
                        table = AppConfigInfo.OpenTable("ZDInfos");
                    }
                    catch (System.Exception)
                    {
                    }
                }
                if (table == null)
                {
                    result = "";
                }
                else
                {
                    string workspaceConnectInfo =
                        WorkspaceOperator.GetWorkspaceConnectInfo((ifeatureClass_0 as IDataset).Workspace);
                    IQueryFilter queryFilter = new QueryFilter();
                    string[] array = (ifeatureClass_0 as IDataset).Name.Split(new char[]
                    {
                        '.'
                    });
                    queryFilter.WhereClause = string.Format("FeatureClassName='{0}' and GDBConnectInfo='{1}'",
                        array[array.Length - 1], workspaceConnectInfo);
                    using (ComReleaser comReleaser = new ComReleaser())
                    {
                        ICursor cursor = table.Search(queryFilter, false);
                        comReleaser.ManageLifetime(cursor);
                        IRow row = cursor.NextRow();
                        if (row != null)
                        {
                            int num = table.FindField("Guid");
                            string text = "";
                            if (num != -1)
                            {
                                text = row.get_Value(num).ToString();
                            }
                            result = text;
                            return result;
                        }
                    }
                    result = "";
                }
            }
            return result;
        }

        public static string GetHistoryZDFeatureName(IFeatureClass ifeatureClass_0)
        {
            string result;
            if (ifeatureClass_0 == null)
            {
                result = "";
            }
            else
            {
                ITable table = null;
                try
                {
                    table = AppConfigInfo.OpenTable("ZDInfos");
                }
                catch
                {
                }
                if (table == null)
                {
                    try
                    {
                        ZDHistoryManage zDHistoryManage = new ZDHistoryManage();
                        zDHistoryManage.CreateTable(AppConfigInfo.GetWorkspace() as IFeatureWorkspace);
                        table = AppConfigInfo.OpenTable("ZDInfos");
                    }
                    catch (System.Exception)
                    {
                    }
                }
                if (table == null)
                {
                    result = "";
                }
                else
                {
                    string workspaceConnectInfo =
                        WorkspaceOperator.GetWorkspaceConnectInfo((ifeatureClass_0 as IDataset).Workspace);
                    IQueryFilter queryFilter = new QueryFilter();
                    string[] array = (ifeatureClass_0 as IDataset).Name.Split(new char[]
                    {
                        '.'
                    });
                    queryFilter.WhereClause = string.Format("FeatureClassName='{0}' and GDBConnectInfo='{1}'",
                        array[array.Length - 1], workspaceConnectInfo);
                    using (ComReleaser comReleaser = new ComReleaser())
                    {
                        ICursor cursor = table.Search(queryFilter, false);
                        comReleaser.ManageLifetime(cursor);
                        IRow row = cursor.NextRow();
                        if (row != null)
                        {
                            int num = table.FindField("HistoryFeatureClass");
                            string text = "";
                            if (num != -1)
                            {
                                text = row.get_Value(num).ToString();
                            }
                            result = text;
                            return result;
                        }
                    }
                    result = "";
                }
            }
            return result;
        }

        public static IFeatureClass GetHistoryFeatureClass(IFeatureClass ifeatureClass_0)
        {
            string text = ZDRegister.GetHistoryZDFeatureName(ifeatureClass_0);
            if (string.IsNullOrEmpty(text))
            {
                string[] array = (ifeatureClass_0 as IDataset).Name.Split(new char[]
                {
                    '.'
                });
                text = array[array.Length - 1] + "_His";
            }
            IFeatureWorkspace featureWorkspace = (ifeatureClass_0 as IDataset).Workspace as IFeatureWorkspace;
            return featureWorkspace.OpenFeatureClass(text);
        }
    }
}