using System;
using System.Collections.Generic;
using System.IO;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.Helpers
{
    public class WorkspaceHelper
    {
        public static string GISConnectionString;

        public WorkspaceHelper()
        {
        }

        public static IWorkspace GetAccessWorkspace(string sFilePath)
        {
            IWorkspace workspace;
            if (File.Exists(sFilePath))
            {
                try
                {
                    workspace = (new AccessWorkspaceFactory()).OpenFromFile(sFilePath, 0);
                }
                catch
                {
                    workspace = null;
                }
            }
            else
            {
                workspace = null;
            }
            return workspace;
        }

        public static List<IConfigurationKeyword> GetConfigurationKeywordList(IWorkspace pWS)
        {
            List<IConfigurationKeyword> configurationKeywords = new List<IConfigurationKeyword>();
            IEnumConfigurationKeyword enumConfigurationKeyword = (pWS as IWorkspaceConfiguration).ConfigurationKeywords;
            for (IConfigurationKeyword i = enumConfigurationKeyword.Next();
                i != null;
                i = enumConfigurationKeyword.Next())
            {
                configurationKeywords.Add(i);
            }
            return configurationKeywords;
        }

        public static List<IConfigurationParameter> GetConfigurationParameterList(IConfigurationKeyword pConfig)
        {
            List<IConfigurationParameter> configurationParameters = new List<IConfigurationParameter>();
            IEnumConfigurationParameter enumConfigurationParameter = pConfig.ConfigurationParameters;
            for (IConfigurationParameter i = enumConfigurationParameter.Next();
                i != null;
                i = enumConfigurationParameter.Next())
            {
                configurationParameters.Add(i);
            }
            return configurationParameters;
        }

        public static IWorkspace GetFGDBWorkspace(string sFilePath)
        {
            IWorkspace workspace;
            if (Directory.Exists(sFilePath))
            {
                try
                {
                    workspace = (new FileGDBWorkspaceFactory()).OpenFromFile(sFilePath, 0);
                }
                catch
                {
                    workspace = null;
                }
            }
            else
            {
                workspace = null;
            }
            return workspace;
        }

        public static IWorkspace GetSDEWorkspace(string sServerName, string sInstancePort, string sUserName,
            string sPassword, string sVersionName)
        {
            IWorkspace workspace;
            IPropertySet propertySetClass = new PropertySet();
            propertySetClass.SetProperty("Server", sServerName);
            propertySetClass.SetProperty("Instance", sInstancePort);
            propertySetClass.SetProperty("User", sUserName);
            propertySetClass.SetProperty("password", sPassword);
            propertySetClass.SetProperty("version", sVersionName);
            SdeWorkspaceFactory sdeWorkspaceFactoryClass = new SdeWorkspaceFactory();
            try
            {
                workspace = sdeWorkspaceFactoryClass.Open(propertySetClass, 0);
            }
            catch (Exception exception)
            {
                workspace = null;
            }
            return workspace;
        }

        public static IFeatureClass GetFeatureClass(string wksName,string classFullName)
        {
            
            int mdbIndex = wksName.IndexOf(".mdb");
            string workspacePath;
            string fcPath;
            string className = "";
            IFeatureWorkspace pWorkspace = null;
            if (mdbIndex > 0)
            {
                pWorkspace = GetAccessWorkspace(wksName) as IFeatureWorkspace;
            }
            else if (wksName.IndexOf(".gdb") > 0)
            {
                    pWorkspace = GetFGDBWorkspace(wksName) as IFeatureWorkspace;
            }
            else if (wksName.IndexOf(".sde") > 0)
            {
                pWorkspace = GetSDEWorkspace(wksName) as IFeatureWorkspace;
            }

            className = LayerHelper.GetClassShortName(classFullName);
            if (pWorkspace == null) return null;

            try
            {
                IFeatureClass pClass = pWorkspace.OpenFeatureClass(className);
                return pClass;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public static IWorkspace GetSDEWorkspace(string connectFile)
        {
            IWorkspaceFactory factory=new SdeWorkspaceFactory();
            return factory.OpenFromFile(connectFile, 0);
        }
        public static IWorkspace GetShapefileWorkspace(string sFilePath)
        {
            IWorkspace workspace;
            if (File.Exists(sFilePath))
            {
                try
                {
                    IWorkspaceFactory shapefileWorkspaceFactoryClass = new ShapefileWorkspaceFactory();
                    sFilePath = Path.GetDirectoryName(sFilePath);
                    workspace = shapefileWorkspaceFactoryClass.OpenFromFile(sFilePath, 0);
                }
                catch
                {
                    workspace = null;
                }
            }
            else
            {
                workspace = null;
            }
            return workspace;
        }

        public static bool HighPrecision(IWorkspace pWorkspace)
        {
            bool flag;
            IGeodatabaseRelease geodatabaseRelease = pWorkspace as IGeodatabaseRelease;
            if (geodatabaseRelease != null)
            {
                flag = ((geodatabaseRelease.MajorVersion != 2 ? true : geodatabaseRelease.MinorVersion != 2)
                    ? false
                    : true);
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public static string PGDBDataConnectionString(string sPath)
        {
            return string.Concat("Provider=ESRI.GeoDB.OLEDB.1;Data Source=", sPath,
                ";Extended Properties=workspacetype=esriDataSourcesGDB.AccessWorkspaceFactory.1;Geometry=WKB");
        }

        public static List<string> QueryFeatureClassName(IWorkspace pWorkspace)
        {
            return WorkspaceHelper.QueryFeatureClassName(pWorkspace, false, false);
        }

        public static List<string> QueryFeatureClassName(IWorkspace pWorkspace, bool pUpperCase)
        {
            return WorkspaceHelper.QueryFeatureClassName(pWorkspace, pUpperCase, false);
        }

        public static List<string> QueryFeatureClassName(IWorkspace pWorkspace, bool pUpperCase, bool pEscapeMetaTable)
        {
            IDatasetName i;
            string upper;
            string classShortName;
            List<string> strs;
            try
            {
                string str = "";
                if (pWorkspace.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                {
                    str = pWorkspace.ConnectionProperties.GetProperty("user").ToString();
                    str = str.ToUpper();
                }
                List<string> strs1 = new List<string>();
                IEnumDatasetName datasetNames = pWorkspace.DatasetNames[esriDatasetType.esriDTFeatureDataset];
                for (i = datasetNames.Next(); i != null; i = datasetNames.Next())
                {
                    upper = i.Name.ToUpper();
                    if (str.Equals(LayerHelper.GetClassOwnerName(upper)))
                    {
                        IEnumDatasetName subsetNames = i.SubsetNames;
                        for (i = subsetNames.Next(); i != null; i = subsetNames.Next())
                        {
                            upper = i.Name.ToUpper();
                            if (!(i is ITopologyName))
                            {
                                classShortName = LayerHelper.GetClassShortName(upper);
                                if (pUpperCase)
                                {
                                    classShortName = classShortName.ToUpper();
                                }
                                if (!pEscapeMetaTable)
                                {
                                    strs1.Add(classShortName);
                                }
                            }
                        }
                    }
                }
                datasetNames = pWorkspace.DatasetNames[esriDatasetType.esriDTFeatureClass];
                for (i = datasetNames.Next(); i != null; i = datasetNames.Next())
                {
                    upper = i.Name.ToUpper();
                    if (str.Equals(LayerHelper.GetClassOwnerName(upper)))
                    {
                        classShortName = LayerHelper.GetClassShortName(upper);
                        if (pUpperCase)
                        {
                            classShortName = classShortName.ToUpper();
                        }
                        if (!pEscapeMetaTable)
                        {
                            strs1.Add(classShortName);
                        }
                    }
                }
                strs = strs1;
            }
            catch (Exception exception)
            {
                strs = null;
            }
            return strs;
        }

        public static string SDEDataConnectionString(string sServerName, string sDataSource, string sUserName,
            string sPW)
        {
            string[] strArrays = new string[]
            {
                "Provider=ESRI.GeoDB.OLEDB.1;Location=", sServerName, ";Data Source=", sDataSource, "; User Id=",
                sUserName, ";Password=", sPW,
                "; Extended Properties=WorkspaceType= esriDataSourcesGDB.SDEWorkspaceFactory.1;Geometry=WKB|OBJECT;Instance=5151;Version=SDE.DEFAULT"
            };
            return string.Concat(strArrays);
        }

        public static string ShapefileDataConnectionString(string sPath)
        {
            sPath = Path.GetDirectoryName(sPath);
            return string.Concat("Provider=ESRI.GeoDB.OLEDB.1;Data Source=", sPath,
                ";Extended Properties=WorkspaceType=esriDataSourcesFile.ShapefileWorkspaceFactory.1;Geometry=WKB|OBJECT");
        }

        public static IWorkspace SwitchVersionWorkspace(IWorkspace pWS, string sVersion, string sPws)
        {
            string str = pWS.ConnectionProperties.GetProperty("Server").ToString();
            string str1 = pWS.ConnectionProperties.GetProperty("Instance").ToString();
            string str2 = pWS.ConnectionProperties.GetProperty("User").ToString();
            return WorkspaceHelper.GetSDEWorkspace(str, str1, str2, sPws, sVersion);
        }

        public static string GetSpecialCharacter(IDataset pDS, esriSQLSpecialCharacters specChar)
        {
            IWorkspace pWorkspace = pDS.Workspace;
            //if (pWorkspace is ShapefileWorkspaceFactory)
            //{
            //    if (specChar == esriSQLSpecialCharacters.esriSQL_WildcardManyMatch) return "%";
            //    if (specChar == esriSQLSpecialCharacters.esriSQL_WildcardSingleMatch) return "%";
            //    if (specChar == esriSQLSpecialCharacters.esriSQL_WildcardManyMatch) return "%";
            //    if (specChar == esriSQLSpecialCharacters.esriSQL_WildcardManyMatch) return "%";
            //    if (specChar == esriSQLSpecialCharacters.esriSQL_WildcardManyMatch) return "%";
            //}
            ISQLSyntax sqlSyntax = pWorkspace as ISQLSyntax;
            if (sqlSyntax == null) return "";
            return sqlSyntax.GetSpecialCharacter(specChar);
        }

        public static IFeatureWorkspace GetWorkspace(string fullName)
        {

            int mdbIndex = fullName.IndexOf(".mdb");
            string workspacePath;
            string fcPath;
            string className = "";
            IFeatureWorkspace pWorkspace = null;
            if (mdbIndex > 0)
            {
                pWorkspace = GetAccessWorkspace(fullName) as IFeatureWorkspace;
            }
            else if (fullName.IndexOf(".gdb") > 0)
            {
                pWorkspace = GetFGDBWorkspace(fullName) as IFeatureWorkspace;
            }
            else if (fullName.IndexOf(".sde") > 0)
            {
                pWorkspace = GetSDEWorkspace(fullName) as IFeatureWorkspace;
            }

            return pWorkspace;
        }
    }
}