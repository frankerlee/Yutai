using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;

namespace Yutai.ArcGIS.Catalog.ISConfig
{
    internal class ISConfig
    {
        private static string password;
        private static IRasterDataset rasterDataset;
        private static string restAdmin;
        private static string serviceName;
        private static IServerObjectAdmin soAdmin;
        private static string sourcePath;
        private static string username;

        static ISConfig()
        {
            old_acctor_mc();
        }

        private static bool ConnectAGS(string string_0)
        {
            try
            {
                IPropertySet set = new PropertySetClass();
                set.SetProperty("url", string_0);
                set.SetProperty("ConnectionMode", esriAGSConnectionMode.esriAGSConnectionModePublisher);
                set.SetProperty("ServerType", esriAGSServerType.esriAGSServerTypeDiscovery);
                set.SetProperty("user", username);
                set.SetProperty("password", password);
                set.SetProperty("ALLOWINSECURETOKENURL", true);
                IAGSServerConnectionName3 name = new AGSServerConnectionNameClass() as IAGSServerConnectionName3;
                name.ConnectionProperties = set;
                IAGSServerConnectionAdmin admin = ((IName) name).Open() as IAGSServerConnectionAdmin;
                soAdmin = admin.ServerObjectAdmin;
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error: Couldn't connect to AGSServer: {0}. Message: {1}", string_0, exception.Message);
                return false;
            }
        }

        private static void CreateISConfig()
        {
            try
            {
                if (!ConnectAGS(restAdmin))
                {
                    return;
                }
                esriImageServiceSourceType sourceType = GetSourceType(sourcePath);
                IServerObjectConfiguration5 config = (IServerObjectConfiguration5) soAdmin.CreateConfiguration();
                config.Name = serviceName;
                config.TypeName = "ImageServer";
                config.TargetCluster = "default";
                config.StartupType = esriStartupType.esriSTAutomatic;
                config.IsPooled = true;
                config.IsolationLevel = esriServerIsolationLevel.esriServerIsolationHigh;
                config.MinInstances = 1;
                config.MaxInstances = 2;
                config.RecycleProperties.SetProperty("Interval", "24");
                IPropertySet properties = config.Properties;
                IWorkspace workspace = ((IDataset) ISConfig.rasterDataset).Workspace;
                if (workspace.WorkspaceFactory.WorkspaceType == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                {
                    IWorkspaceName2 fullName = ((IDataset) workspace).FullName as IWorkspaceName2;
                    string connectionString = fullName.ConnectionString;
                    properties.SetProperty("ConnectionString", connectionString);
                    properties.SetProperty("Raster", ((IDataset) ISConfig.rasterDataset).Name);
                }
                else
                {
                    properties.SetProperty("Path", sourcePath);
                }
                properties.SetProperty("EsriImageServiceSourceType", sourceType.ToString());
                properties.SetProperty("SupportedImageReturnTypes", "MIME+URL");
                IEnumServerDirectory serverDirectories = soAdmin.GetServerDirectories();
                serverDirectories.Reset();
                IServerDirectory directory2 = serverDirectories.Next();
                while (directory2 != null)
                {
                    if (((IServerDirectory2) directory2).Type == esriServerDirectoryType.esriSDTypeOutput)
                    {
                        goto Label_0176;
                    }
                    directory2 = serverDirectories.Next();
                }
                goto Label_019A;
                Label_0176:
                properties.SetProperty("OutputDir", directory2.Path);
                properties.SetProperty("VirtualOutputDir", directory2.URL);
                Label_019A:
                properties.SetProperty("CopyRight", "");
                if (sourceType == esriImageServiceSourceType.esriImageServiceSourceTypeMosaicDataset)
                {
                    object obj2;
                    object obj3;
                    IFunctionRasterDataset rasterDataset = (IFunctionRasterDataset) ISConfig.rasterDataset;
                    IPropertySet set3 = rasterDataset.Properties;
                    set3.GetAllProperties(out obj2, out obj3);
                    List<string> list = new List<string>();
                    list.AddRange((string[]) obj2);
                    if (list.Contains("MaxImageHeight"))
                    {
                        properties.SetProperty("MaxImageHeight", set3.GetProperty("MaxImageHeight"));
                    }
                    if (list.Contains("MaxImageWidth"))
                    {
                        properties.SetProperty("MaxImageWidth", set3.GetProperty("MaxImageWidth"));
                    }
                    if (list.Contains("AllowedCompressions"))
                    {
                        properties.SetProperty("AllowedCompressions", set3.GetProperty("AllowedCompressions"));
                    }
                    if (list.Contains("DefaultResamplingMethod"))
                    {
                        properties.SetProperty("DefaultResamplingMethod", set3.GetProperty("DefaultResamplingMethod"));
                    }
                    if (list.Contains("DefaultCompressionQuality"))
                    {
                        properties.SetProperty("DefaultCompressionQuality",
                            set3.GetProperty("DefaultCompressionQuality"));
                    }
                    if (list.Contains("MaxRecordCount"))
                    {
                        properties.SetProperty("MaxRecordCount", set3.GetProperty("MaxRecordCount"));
                    }
                    if (list.Contains("MaxMosaicImageCount"))
                    {
                        properties.SetProperty("MaxMosaicImageCount", set3.GetProperty("MaxMosaicImageCount"));
                    }
                    if (list.Contains("MaxDownloadSizeLimit"))
                    {
                        properties.SetProperty("MaxDownloadSizeLimit", set3.GetProperty("MaxDownloadSizeLimit"));
                    }
                    if (list.Contains("MaxDownloadImageCount"))
                    {
                        properties.SetProperty("MaxDownloadImageCount", set3.GetProperty("MaxDownloadImageCount"));
                    }
                    if (list.Contains("AllowedFields"))
                    {
                        properties.SetProperty("AllowedFields", set3.GetProperty("AllowedFields"));
                    }
                    if (list.Contains("AllowedMosaicMethods"))
                    {
                        properties.SetProperty("AllowedMosaicMethods", set3.GetProperty("AllowedMosaicMethods"));
                    }
                    if (list.Contains("AllowedItemMetadata"))
                    {
                        properties.SetProperty("AllowedItemMetadata", set3.GetProperty("AllowedItemMetadata"));
                    }
                    if (list.Contains("AllowedMensurationCapabilities"))
                    {
                        properties.SetProperty("AllowedMensurationCapabilities",
                            set3.GetProperty("AllowedMensurationCapabilities"));
                    }
                    if (list.Contains("DefaultCompressionTolerance"))
                    {
                        properties.SetProperty("DefaultCompressionTolerance",
                            set3.GetProperty("DefaultCompressionTolerance"));
                    }
                }
                else
                {
                    properties.SetProperty("MaxImageHeight", 4100);
                    properties.SetProperty("MaxImageWidth", 15000);
                    properties.SetProperty("AllowedCompressions", "None,JPEG,LZ77");
                    properties.SetProperty("DefaultResamplingMethod", 0);
                    properties.SetProperty("DefaultCompressionQuality", 75);
                    properties.SetProperty("DefaultCompressionTolerance", 0.01);
                    IMensuration mensuration = new MensurationClass
                    {
                        Raster = ((IRasterDataset2) ISConfig.rasterDataset).CreateFullRaster()
                    };
                    string str2 = "";
                    if (mensuration.CanMeasure)
                    {
                        str2 = "Basic";
                    }
                    if (mensuration.CanMeasureHeightBaseToTop)
                    {
                        str2 = str2 + ",Base-Top Height";
                    }
                    if (mensuration.CanMeasureHeightBaseToTopShadow)
                    {
                        str2 = str2 + ",Base-Top Shadow Height";
                    }
                    if (mensuration.CanMeasureHeightTopToTopShadow)
                    {
                        str2 = str2 + ",Top-Top Shadow Height";
                    }
                    properties.SetProperty("AllowedMensurationCapabilities", str2);
                }
                properties.SetProperty("IsCached", false);
                properties.SetProperty("IgnoreCache", true);
                properties.SetProperty("UseLocalCacheDir", false);
                properties.SetProperty("ClientCachingAllowed", false);
                properties.SetProperty("ColormapToRGB", false);
                properties.SetProperty("ReturnJPGPNGAsJPG", false);
                properties.SetProperty("AllowFunction", true);
                if (sourceType == esriImageServiceSourceType.esriImageServiceSourceTypeMosaicDataset)
                {
                    config.Info.SetProperty("Capabilities", "Image,Catalog,Metadata,Mensuration");
                }
                else
                {
                    config.Info.SetProperty("Capabilities", "Image,Metadata,Mensuration");
                }
                config.set_ExtensionEnabled("WCSServer", true);
                IPropertySet ppExtProperties = new PropertySetClass();
                ppExtProperties.SetProperty("WebEnabled", "true");
                config.set_ExtensionInfo("WCSServer", ppExtProperties);
                IPropertySet set5 = new PropertySetClass();
                set5.SetProperty("CustomGetCapabilities", false);
                set5.SetProperty("PathToCustomGetCapabilitiesFiles", "");
                config.set_ExtensionProperties("WCSServer", set5);
                config.set_ExtensionEnabled("WMSServer", true);
                IPropertySet set6 = new PropertySetClass();
                set6.SetProperty("WebEnabled", "true");
                config.set_ExtensionInfo("WMSServer", set6);
                IPropertySet set7 = new PropertySetClass();
                set7.SetProperty("name", "WMS");
                config.set_ExtensionProperties("WMSServer", set7);
                soAdmin.AddConfiguration(config);
                soAdmin.StartConfiguration(serviceName, "ImageServer");
                if (soAdmin.GetConfigurationStatus(serviceName, "ImageServer").Status ==
                    esriConfigurationStatus.esriCSStarted)
                {
                    Console.WriteLine("{0} on {1} has been configured and started.", serviceName, restAdmin);
                }
                else
                {
                    Console.WriteLine("{0} on {1} was configured but can not be started, please investigate.",
                        serviceName, restAdmin);
                }
                if (ISConfig.rasterDataset != null)
                {
                    Marshal.ReleaseComObject(ISConfig.rasterDataset);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error: {0}", exception.Message);
            }
        }

        public static bool CreateISConfig_RESTAdmin()
        {
            try
            {
                esriImageServiceSourceType sourceType = GetSourceType(sourcePath);
                string str = "ImageServer";
                restAdmin = restAdmin.EndsWith("/") ? restAdmin.Substring(0, restAdmin.Length - 1) : restAdmin;
                string str2 = "";
                if (serviceName.Contains("/"))
                {
                    str2 = serviceName.Substring(0, serviceName.IndexOf("/"));
                    CreateServerFolder_RESTAdmin(str2, "");
                    serviceName = serviceName.Substring(str2.Length + 1, (serviceName.Length - str2.Length) - 1);
                }
                string requestUriString = "";
                if (str2 == "")
                {
                    requestUriString = restAdmin + "/services/createService";
                }
                else
                {
                    requestUriString = restAdmin + "/services/" + str2 + "/createService";
                }
                WebRequest request = WebRequest.Create(requestUriString);
                StringBuilder builder = new StringBuilder();
                builder.Append("{");
                builder.AppendFormat("{0}: {1},", QuoteString("serviceName"), QuoteString(serviceName));
                builder.AppendFormat("{0}: {1},", QuoteString("type"), QuoteString(str));
                builder.AppendFormat("{0}: {1},", QuoteString("description"), QuoteString(""));
                builder.AppendFormat("{0}: {1},", QuoteString("clusterName"), QuoteString("default"));
                builder.AppendFormat("{0}: {1},", QuoteString("minInstancesPerNode"), 1);
                builder.AppendFormat("{0}: {1},", QuoteString("maxInstancesPerNode"), 2);
                builder.AppendFormat("{0}: {1},", QuoteString("maxWaitTime"), 10000);
                builder.AppendFormat("{0}: {1},", QuoteString("maxIdleTime"), 1800);
                builder.AppendFormat("{0}: {1},", QuoteString("maxUsageTime"), 600);
                builder.AppendFormat("{0}: {1},", QuoteString("loadBalancing"), QuoteString("ROUND_ROBIN"));
                builder.AppendFormat("{0}: {1},", QuoteString("isolationLevel"), QuoteString("HIGH"));
                builder.AppendFormat("{0}: {1},", QuoteString("configuredState"), QuoteString("STARTED"));
                string str4 = "";
                if (sourceType == esriImageServiceSourceType.esriImageServiceSourceTypeMosaicDataset)
                {
                    str4 = "Image,Catalog,Metadata,Mensuration";
                }
                else
                {
                    str4 = "Image,Metadata,Mensuration";
                }
                builder.AppendFormat("{0}: {1},", QuoteString("capabilities"), QuoteString(str4));
                builder.AppendFormat("{0}: {1}", QuoteString("properties"), "{");
                builder.AppendFormat("{0}: {1},", QuoteString("supportedImageReturnTypes"), QuoteString("MIME+URL"));
                IWorkspace workspace = ((IDataset) ISConfig.rasterDataset).Workspace;
                if (workspace.WorkspaceFactory.WorkspaceType == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                {
                    IWorkspaceName2 fullName = ((IDataset) workspace).FullName as IWorkspaceName2;
                    string connectionString = fullName.ConnectionString;
                    builder.AppendFormat("{0}: {1},", QuoteString("connectionString"), QuoteString(connectionString));
                    builder.AppendFormat("{0}: {1},", QuoteString("raster"),
                        QuoteString(((IDataset) ISConfig.rasterDataset).Name));
                }
                else
                {
                    builder.AppendFormat("{0}: {1},", QuoteString("path"), QuoteString(sourcePath.Replace(@"\", @"\\")));
                }
                builder.AppendFormat("{0}: {1},", QuoteString("esriImageServiceSourceType"),
                    QuoteString(sourceType.ToString()));
                string str6 = "";
                string str7 = "";
                GetServerDirectory_RESTAdmin("arcgisoutput", out str6, out str7);
                string str8 = "";
                string str9 = "";
                GetServerDirectory_RESTAdmin("arcgisoutput", out str8, out str9);
                if (str6 != "")
                {
                    builder.AppendFormat("{0}: {1},", QuoteString("outputDir"), QuoteString(str6));
                    builder.AppendFormat("{0}: {1},", QuoteString("virtualOutputDir"), QuoteString(str7));
                }
                builder.AppendFormat("{0}: {1},", QuoteString("copyright"), QuoteString(""));
                if (sourceType == esriImageServiceSourceType.esriImageServiceSourceTypeMosaicDataset)
                {
                    object obj2;
                    object obj3;
                    IFunctionRasterDataset rasterDataset = (IFunctionRasterDataset) ISConfig.rasterDataset;
                    IPropertySet properties = rasterDataset.Properties;
                    properties.GetAllProperties(out obj2, out obj3);
                    List<string> list = new List<string>();
                    list.AddRange((string[]) obj2);
                    if (list.Contains("AllowedCompressions"))
                    {
                        builder.AppendFormat("{0}: {1},", QuoteString("allowedCompressions"),
                            QuoteString(properties.GetProperty("AllowedCompressions").ToString()));
                    }
                    if (list.Contains("MaxImageHeight"))
                    {
                        builder.AppendFormat("{0}: {1},", QuoteString("maxImageHeight"),
                            QuoteString(properties.GetProperty("MaxImageHeight").ToString()));
                    }
                    if (list.Contains("MaxImageWidth"))
                    {
                        builder.AppendFormat("{0}: {1},", QuoteString("maxImageWidth"),
                            QuoteString(properties.GetProperty("MaxImageWidth").ToString()));
                    }
                    if (list.Contains("DefaultResamplingMethod"))
                    {
                        builder.AppendFormat("{0}: {1},", QuoteString("defaultResamplingMethod"),
                            QuoteString(properties.GetProperty("DefaultResamplingMethod").ToString()));
                    }
                    if (list.Contains("DefaultCompressionQuality"))
                    {
                        builder.AppendFormat("{0}: {1},", QuoteString("defaultCompressionQuality"),
                            QuoteString(properties.GetProperty("DefaultCompressionQuality").ToString()));
                    }
                    if (list.Contains("MaxRecordCount"))
                    {
                        builder.AppendFormat("{0}: {1},", QuoteString("maxRecordCount"),
                            QuoteString(properties.GetProperty("MaxRecordCount").ToString()));
                    }
                    if (list.Contains("MaxMosaicImageCount"))
                    {
                        builder.AppendFormat("{0}: {1},", QuoteString("maxMosaicImageCount"),
                            QuoteString(properties.GetProperty("MaxMosaicImageCount").ToString()));
                    }
                    if (list.Contains("MaxDownloadImageCount"))
                    {
                        builder.AppendFormat("{0}: {1},", QuoteString("maxDownloadImageCount"),
                            QuoteString(properties.GetProperty("MaxDownloadImageCount").ToString()));
                    }
                    if (list.Contains("MaxDownloadSizeLimit"))
                    {
                        builder.AppendFormat("{0}: {1},", QuoteString("MaxDownloadSizeLimit"),
                            QuoteString(properties.GetProperty("MaxDownloadSizeLimit").ToString()));
                    }
                    if (list.Contains("AllowedFields"))
                    {
                        builder.AppendFormat("{0}: {1},", QuoteString("allowedFields"),
                            QuoteString(properties.GetProperty("AllowedFields").ToString()));
                    }
                    if (list.Contains("AllowedMosaicMethods"))
                    {
                        builder.AppendFormat("{0}: {1},", QuoteString("allowedMosaicMethods"),
                            QuoteString(properties.GetProperty("AllowedMosaicMethods").ToString()));
                    }
                    if (list.Contains("AllowedItemMetadata"))
                    {
                        builder.AppendFormat("{0}: {1},", QuoteString("allowedItemMetadata"),
                            QuoteString(properties.GetProperty("AllowedItemMetadata").ToString()));
                    }
                    if (list.Contains("AllowedMensurationCapabilities"))
                    {
                        builder.AppendFormat("{0}: {1},", QuoteString("AllowedMensurationCapabilities"),
                            QuoteString(properties.GetProperty("AllowedMensurationCapabilities").ToString()));
                    }
                    if (list.Contains("DefaultCompressionTolerance"))
                    {
                        builder.AppendFormat("{0}: {1},", QuoteString("defaultCompressionTolerance"),
                            QuoteString(properties.GetProperty("DefaultCompressionTolerance").ToString()));
                    }
                }
                else if (sourceType != esriImageServiceSourceType.esriImageServiceSourceTypeCatalog)
                {
                    builder.AppendFormat("{0}: {1},", QuoteString("allowedCompressions"), QuoteString("None,JPEG,LZ77"));
                    builder.AppendFormat("{0}: {1},", QuoteString("maxImageHeight"), QuoteString("4100"));
                    builder.AppendFormat("{0}: {1},", QuoteString("maxImageWidth"), QuoteString("15000"));
                    builder.AppendFormat("{0}: {1},", QuoteString("defaultResamplingMethod"), QuoteString("0"));
                    builder.AppendFormat("{0}: {1},", QuoteString("defaultCompressionQuality"), QuoteString("75"));
                    builder.AppendFormat("{0}: {1},", QuoteString("defaultCompressionTolerance"), QuoteString("0.01"));
                    IMensuration mensuration = new MensurationClass
                    {
                        Raster = ((IRasterDataset2) ISConfig.rasterDataset).CreateFullRaster()
                    };
                    string str10 = "";
                    if (mensuration.CanMeasure)
                    {
                        str10 = "Basic";
                    }
                    if (mensuration.CanMeasureHeightBaseToTop)
                    {
                        str10 = str10 + ",Base-Top Height";
                    }
                    if (mensuration.CanMeasureHeightBaseToTopShadow)
                    {
                        str10 = str10 + ",Base-Top Shadow Height";
                    }
                    if (mensuration.CanMeasureHeightTopToTopShadow)
                    {
                        str10 = str10 + ",Top-Top Shadow Height";
                    }
                    builder.AppendFormat("{0}: {1},", QuoteString("AllowedMensurationCapabilities"), QuoteString(str10));
                }
                builder.AppendFormat("{0}: {1},", QuoteString("colormapToRGB"), QuoteString("false"));
                builder.AppendFormat("{0}: {1},", QuoteString("returnJPGPNGAsJPG"), QuoteString("false"));
                builder.AppendFormat("{0}: {1},", QuoteString("allowFunction"), QuoteString("true"));
                string str11 = "";
                builder.AppendFormat("{0}: {1},", QuoteString("rasterFunctions"),
                    QuoteString(str11).Replace(@"\", @"\\"));
                string str12 = "";
                builder.AppendFormat("{0}: {1}", QuoteString("rasterTypes"), QuoteString(str12).Replace(@"\", @"\\"));
                builder.Append("},");
                builder.AppendFormat("{0}: {1}", QuoteString("extensions"),
                    string.Concat(new object[]
                    {
                        "[{\"typeName\":\"WCSServer\",\"enabled\":\"", true,
                        "\",\"capabilities\":null,\"properties\":{}},{\"typeName\":\"WMSServer\",\"enabled\":\"", true,
                        "\",\"capabilities\":null,\"properties\":{\"title\":\"WMS\",\"name\":\"WMS\",\"inheritLayerNames\":\"false\"}}"
                    }));
                builder.Append("],");
                builder.AppendFormat("{0}: {1}", QuoteString("datasets"), "[]");
                builder.Append("}");
                string s = "";
                string str14 = GenerateAGSToken_RESTAdmin();
                s = "service=" + s + "&startAfterCreate=on&f=pjson&token=" + str14;
                byte[] bytes = Encoding.UTF8.GetBytes(s);
                request.ContentLength = bytes.Length;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                string str15 = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
                Console.WriteLine("create service:" + serviceName + " result:" + str15);
                return str15.Contains("success");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }
        }

        private static bool CreateServerFolder_RESTAdmin(string string_0, string string_1)
        {
            try
            {
                string str = GenerateAGSToken_RESTAdmin();
                restAdmin = restAdmin.EndsWith("/") ? restAdmin.Substring(0, restAdmin.Length - 1) : restAdmin;
                StreamReader reader =
                    new StreamReader(
                        WebRequest.Create(restAdmin + "/services/" + string_0 + "?f=json&token=" + str)
                            .GetResponse()
                            .GetResponseStream());
                if (!reader.ReadToEnd().Contains("error"))
                {
                    return true;
                }
                WebRequest request = WebRequest.Create(restAdmin + "/services/createFolder");
                string s = string.Format("folderName={0}&description={1}&f=pjson&token={2}", string_0, string_1, str);
                byte[] bytes = Encoding.UTF8.GetBytes(s);
                request.ContentLength = bytes.Length;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                reader = new StreamReader(request.GetResponse().GetResponseStream());
                return reader.ReadToEnd().Contains("success");
            }
            catch
            {
                return false;
            }
        }

        private static void DeleteService()
        {
            try
            {
                if (ConnectAGS(restAdmin) && ValidateServiceName(soAdmin, ref serviceName, restAdmin))
                {
                    soAdmin.DeleteConfiguration(serviceName, "ImageServer");
                    Console.WriteLine("{0} on {1} was deleted successfully.", serviceName, restAdmin);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error: {0}", exception.Message);
            }
        }

        public static bool DeleteService_RESTAdmin()
        {
            try
            {
                string str = GenerateAGSToken_RESTAdmin();
                restAdmin = restAdmin.EndsWith("/") ? restAdmin.Substring(0, restAdmin.Length - 1) : restAdmin;
                WebRequest request = WebRequest.Create(restAdmin + "/services/" + serviceName + ".ImageServer/delete");
                string s = "f=pjson&token=" + str;
                byte[] bytes = Encoding.UTF8.GetBytes(s);
                request.ContentLength = bytes.Length;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                string str4 = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
                Console.WriteLine("delete service {0}, result: {1}", serviceName, str4);
                return str4.Contains("success");
            }
            catch
            {
                return false;
            }
        }

        private static string DeQuoteString(string string_0)
        {
            if (string_0.StartsWith("\""))
            {
                return string_0.Substring(1, string_0.Length - 2).Trim();
            }
            return string_0;
        }

        public static string GenerateAGSToken_RESTAdmin()
        {
            try
            {
                WebRequest request = WebRequest.Create(restAdmin + "/generateToken");
                request.Method = "POST";
                string s = "username=" + username + "&password=" + password + "&client=requestip&expiration=&f=json";
                byte[] bytes = Encoding.UTF8.GetBytes(s);
                request.ContentLength = bytes.Length;
                request.ContentType = "application/x-www-form-urlencoded";
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                string str3 = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
                int startIndex = str3.IndexOf("token\":\"") + "token\":\"".Length;
                int index = str3.IndexOf("\"", startIndex);
                return str3.Substring(startIndex, index - startIndex);
            }
            catch
            {
                return "";
            }
        }

        private static void GetServerDirectory_RESTAdmin(string string_0, out string string_1, out string string_2)
        {
            string_1 = "";
            string_2 = "";
            try
            {
                string str = GenerateAGSToken_RESTAdmin();
                restAdmin = restAdmin.EndsWith("/") ? restAdmin.Substring(0, restAdmin.Length - 1) : restAdmin;
                string str2 = string_0.ToString().ToLower().Replace("esrisdtype", "arcgis");
                string str4 =
                    new StreamReader(
                        WebRequest.Create(restAdmin + "/system/directories/" + str2 + "?f=json&token=" + str)
                            .GetResponse()
                            .GetResponseStream()).ReadToEnd();
                try
                {
                    int index = str4.IndexOf(string_0);
                    int startIndex = str4.IndexOf("physicalPath\":\"", index) + "physicalPath\":\"".Length;
                    int num3 = str4.IndexOf("\"", startIndex);
                    string_1 = str4.Substring(startIndex, num3 - startIndex);
                    startIndex = str4.IndexOf("virtualPath\":\"", index) + "virtualPath\":\"".Length;
                    num3 = str4.IndexOf("\"", startIndex);
                    string_2 = str4.Substring(startIndex, num3 - startIndex);
                }
                catch
                {
                }
            }
            catch
            {
            }
        }

        private static esriImageServiceSourceType GetSourceType(string string_0)
        {
            if (string_0.ToLower().EndsWith(".lyr"))
            {
                return esriImageServiceSourceType.esriImageServiceSourceTypeLayer;
            }
            FileInfo info = new FileInfo(string_0);
            OpenRasterDataset(info.DirectoryName, info.Name);
            if (rasterDataset is IMosaicDataset)
            {
                return esriImageServiceSourceType.esriImageServiceSourceTypeMosaicDataset;
            }
            return esriImageServiceSourceType.esriImageServiceSourceTypeDataset;
        }

        private static bool InitLicense()
        {
            return true;
        }

        private static void ListServices()
        {
            try
            {
                if (ConnectAGS(restAdmin))
                {
                    IEnumServerObjectConfiguration configurations = soAdmin.GetConfigurations();
                    configurations.Reset();
                    IServerObjectConfiguration configuration2 = configurations.Next();
                    Console.WriteLine("ArcGIS Server {0} has the following image services:", restAdmin);
                    while (configuration2 != null)
                    {
                        if (configuration2.TypeName == "ImageServer")
                        {
                            Console.WriteLine("{0}", configuration2.Name);
                        }
                        configuration2 = configurations.Next();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error: {0}", exception.Message);
            }
        }

        public static void ListServices_RESTAdmin()
        {
            Console.WriteLine("List of image services: ");
            ListServices_RESTAdmin(restAdmin + "/services", "");
        }

        public static void ListServices_RESTAdmin(string string_0, string string_1)
        {
            try
            {
                string str = GenerateAGSToken_RESTAdmin();
                restAdmin = restAdmin.EndsWith("/") ? restAdmin.Substring(0, restAdmin.Length - 1) : restAdmin;
                string requestUriString = string_0 + "/" + string_1;
                WebRequest request = WebRequest.Create(requestUriString);
                string s = "f=json&token=" + str;
                byte[] bytes = Encoding.UTF8.GetBytes(s);
                request.ContentLength = bytes.Length;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                string str4 = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
                int index = str4.IndexOf("folders\":[");
                if (index != -1)
                {
                    index += "folders\":[".Length;
                    int num2 = str4.IndexOf("]", index);
                    foreach (
                        string str6 in str4.Substring(index, num2 - index).Replace("\"", "").Split(new char[] {','}))
                    {
                        ListServices_RESTAdmin(requestUriString, str6);
                    }
                }
                int startIndex = str4.IndexOf("services");
                while (startIndex > 0)
                {
                    try
                    {
                        int num5 = str4.IndexOf("folderName\":\"", startIndex);
                        if (num5 == -1)
                        {
                            return;
                        }
                        num5 += "folderName\":\"".Length;
                        int num6 = str4.IndexOf("\"", num5);
                        string str7 = str4.Substring(num5, num6 - num5);
                        num5 = str4.IndexOf("serviceName\":\"", num6) + "serviceName\":\"".Length;
                        num6 = str4.IndexOf("\"", num5);
                        string str8 = str4.Substring(num5, num6 - num5);
                        num5 = str4.IndexOf("type\":\"", num6) + "type\":\"".Length;
                        num6 = str4.IndexOf("\"", num5);
                        if (str4.Substring(num5, num6 - num5) == "ImageServer")
                        {
                            if (str7 == "/")
                            {
                                Console.WriteLine(str8);
                            }
                            else
                            {
                                Console.WriteLine(str7 + "/" + str8);
                            }
                        }
                        startIndex = num6;
                        continue;
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            catch
            {
            }
        }

        [STAThread]
        public static void Main(string[] string_0)
        {
            try
            {
                if (ValidateParams(string_0) && InitLicense())
                {
                    Retrieve_Params(string_0);
                    string str2 = string_0[1].ToLower();
                    switch (str2)
                    {
                        case null:
                            return;

                        case "publish":
                            goto Label_00A2;

                        case "delete":
                            DeleteService_RESTAdmin();
                            return;

                        case "start":
                            StartService_RESTAdmin();
                            return;

                        case "stop":
                            StopService_RESTAdmin();
                            return;
                    }
                    if (!(str2 == "pause"))
                    {
                        if (str2 == "list")
                        {
                            ListServices_RESTAdmin();
                        }
                    }
                    else
                    {
                        PauseService();
                    }
                }
                return;
                Label_00A2:
                CreateISConfig_RESTAdmin();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error: {0}", exception.Message);
            }
        }

        private static void old_acctor_mc()
        {
            sourcePath = "";
            restAdmin = "";
            serviceName = "";
            rasterDataset = null;
            soAdmin = null;
            username = "";
            password = "";
        }

        private static void OpenRasterDataset(string string_0, string string_1)
        {
            IWorkspaceFactory factory = null;
            IRasterWorkspaceEx ex = null;
            try
            {
                string str = string_0.Substring(string_0.Length - 4, 4).ToLower();
                if (str != null)
                {
                    if (!(str == ".gdb"))
                    {
                        if (!(str == ".sde"))
                        {
                            goto Label_00AB;
                        }
                        factory =
                            Activator.CreateInstance(Type.GetTypeFromProgID("esriDataSourcesGDB.SdeWorkspaceFactory"))
                                as IWorkspaceFactory;
                        ex = (IRasterWorkspaceEx) factory.OpenFromFile(string_0, 1);
                        rasterDataset = ex.OpenRasterDataset(string_1);
                    }
                    else
                    {
                        factory =
                            Activator.CreateInstance(Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory"))
                                as IWorkspaceFactory;
                        rasterDataset =
                            ((IRasterWorkspaceEx) factory.OpenFromFile(string_0, 1)).OpenRasterDataset(string_1);
                    }
                    return;
                }
                Label_00AB:
                factory =
                    Activator.CreateInstance(Type.GetTypeFromProgID("esriDataSourcesRaster.RasterWorkspaceFactory")) as
                        IWorkspaceFactory;
                rasterDataset = ((IRasterWorkspace) factory.OpenFromFile(string_0, 1)).OpenRasterDataset(string_1);
            }
            catch (Exception)
            {
                throw new ArgumentException("Failed to open source data");
            }
        }

        private static void PauseService()
        {
            try
            {
                if (ConnectAGS(restAdmin) && ValidateServiceName(soAdmin, ref serviceName, restAdmin))
                {
                    if (soAdmin.GetConfigurationStatus(serviceName, "ImageServer").Status ==
                        esriConfigurationStatus.esriCSStopped)
                    {
                        Console.WriteLine("{0} on {1} is currently stopped --- not paused.", serviceName, restAdmin);
                    }
                    else
                    {
                        soAdmin.PauseConfiguration(serviceName, "ImageServer");
                        if (soAdmin.GetConfigurationStatus(serviceName, "ImageServer").Status ==
                            esriConfigurationStatus.esriCSPaused)
                        {
                            Console.WriteLine("{0} on {1} was paused successfully.", serviceName, restAdmin);
                        }
                        else
                        {
                            Console.WriteLine("{0} on {1} couldn't be paused, please investigate.", serviceName,
                                restAdmin);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error: {0}", exception.Message);
            }
        }

        private static string QuoteString(string string_0)
        {
            return ("\"" + string_0 + "\"");
        }

        private static void Retrieve_Params(string[] string_0)
        {
            for (int i = 2; i < string_0.Length; i++)
            {
                string str = string_0[i];
                switch (str)
                {
                    case null:
                        break;

                    case "-h":
                        restAdmin = string_0[++i];
                        break;

                    case "-d":
                        sourcePath = string_0[++i];
                        break;

                    case "-n":
                        serviceName = string_0[++i];
                        break;

                    default:
                        if (!(str == "-u"))
                        {
                            if (str == "-p")
                            {
                                password = string_0[++i];
                            }
                        }
                        else
                        {
                            username = string_0[++i];
                        }
                        break;
                }
            }
        }

        private static void ShowUsage()
        {
            Console.WriteLine();
            Console.WriteLine(
                "ArcObject Sample: command line image service publishing and configuration utility (10.1 ArcGIS Server). Data is not copied to server using this tool. source data must be accessible by ArcGIS Server running account. CachingScheme can't be defined through this tool but can be done through Caching geoprocessing tools. REST Metadata/thumbnail/iteminfo resource are not available through this app but can be developed using a similar approach to server admin endpoint.");
            Console.WriteLine();
            Console.WriteLine("Usage. <>: required parameter; |: pick one.");
            Console.WriteLine(
                "isconfig -o publish -h <host_admin_url> -u <adminuser> -p <adminpassword> -d <datapath> -n <serviceName>");
            Console.WriteLine(
                "isconfig -o <delete|start|stop> -h <host_admin_url> -u <adminuser> -p <adminpassword> -n <serviceName>");
            Console.WriteLine("isconfig -o <list> -h <host_admin_url> -u <adminuser> -p <adminpassword>");
            Console.WriteLine("e.g. isconfig -o list -h http://myserver:6080/arcgis/admin -u username -p password");
        }

        private static void StartService()
        {
            try
            {
                if (ConnectAGS(restAdmin) && ValidateServiceName(soAdmin, ref serviceName, restAdmin))
                {
                    soAdmin.StartConfiguration(serviceName, "ImageServer");
                    if (soAdmin.GetConfigurationStatus(serviceName, "ImageServer").Status ==
                        esriConfigurationStatus.esriCSStarted)
                    {
                        Console.WriteLine("{0} on {1} was started successfully.", serviceName, restAdmin);
                    }
                    else
                    {
                        Console.WriteLine("{0} on {1} couldn't be started, please investigate.", serviceName, restAdmin);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error: {0}", exception.Message);
            }
        }

        public static bool StartService_RESTAdmin()
        {
            try
            {
                string str = GenerateAGSToken_RESTAdmin();
                restAdmin = restAdmin.EndsWith("/") ? restAdmin.Substring(0, restAdmin.Length - 1) : restAdmin;
                WebRequest request = WebRequest.Create(restAdmin + "/services/" + serviceName + ".ImageServer/start");
                string s = "f=pjson&token=" + str;
                byte[] bytes = Encoding.UTF8.GetBytes(s);
                request.ContentLength = bytes.Length;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                string str4 = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
                Console.WriteLine("start service {0}, result: {1}", serviceName, str4);
                return str4.Contains("success");
            }
            catch
            {
                return false;
            }
        }

        private static void StopService()
        {
            try
            {
                if (ConnectAGS(restAdmin) && ValidateServiceName(soAdmin, ref serviceName, restAdmin))
                {
                    soAdmin.StopConfiguration(serviceName, "ImageServer");
                    if (soAdmin.GetConfigurationStatus(serviceName, "ImageServer").Status ==
                        esriConfigurationStatus.esriCSStopped)
                    {
                        Console.WriteLine("{0} on {1} was stopped successfully.", serviceName, restAdmin);
                    }
                    else
                    {
                        Console.WriteLine("{0} on {1} couldn't be stopped, please investigate.", serviceName, restAdmin);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error: {0}", exception.Message);
            }
        }

        public static bool StopService_RESTAdmin()
        {
            try
            {
                string str = GenerateAGSToken_RESTAdmin();
                restAdmin = restAdmin.EndsWith("/") ? restAdmin.Substring(0, restAdmin.Length - 1) : restAdmin;
                WebRequest request = WebRequest.Create(restAdmin + "/services/" + serviceName + ".ImageServer/stop");
                string s = "f=pjson&token=" + str;
                byte[] bytes = Encoding.UTF8.GetBytes(s);
                request.ContentLength = bytes.Length;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                string str4 = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
                Console.WriteLine("stop service {0}, result: {1}", serviceName, str4);
                return str4.Contains("success");
            }
            catch
            {
                return false;
            }
        }

        private static bool strInArray(string string_0, string[] string_1)
        {
            for (int i = 0; i < string_1.Length; i++)
            {
                if (string_1[i] == string_0)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool ValidateParams(string[] string_0)
        {
            if (string_0.Length == 0)
            {
                ShowUsage();
                Console.WriteLine("press any key to continue ...");
                Console.ReadKey();
                return false;
            }
            if (string_0.Length < 2)
            {
                ShowUsage();
                return false;
            }
            string[] strArray2 = new string[] {"publish", "delete", "start", "stop", "pause", "list"};
            if (!(string_0[0].StartsWith("-o") && strInArray(string_0[1].ToLower(), strArray2)))
            {
                Console.WriteLine("Incorrect operation");
                ShowUsage();
                return false;
            }
            if ((((string_0[1].ToLower() == "stop") || (string_0[1].ToLower() == "start")) ||
                 (string_0[1].ToLower() == "pause")) || (string_0[1].ToLower() == "delete"))
            {
                if (!strInArray("-h", string_0))
                {
                    Console.WriteLine("Missing host server -h");
                    return false;
                }
                if (!strInArray("-n", string_0))
                {
                    Console.WriteLine("Missing service name switch -n");
                    return false;
                }
                if (!strInArray("-u", string_0))
                {
                    Console.WriteLine("Missing admin/publisher username switch -u");
                    return false;
                }
                if (!strInArray("-p", string_0))
                {
                    Console.WriteLine("Missing admin/publisher name switch -p");
                    return false;
                }
            }
            if (string_0[1].ToLower() == "publish")
            {
                if (!strInArray("-d", string_0))
                {
                    Console.WriteLine("Missing data source switch -d");
                    return false;
                }
                if (!strInArray("-n", string_0))
                {
                    Console.WriteLine("Missing service name switch -n");
                    return false;
                }
                if (!strInArray("-u", string_0))
                {
                    Console.WriteLine("Missing admin/publisher username switch -u");
                    return false;
                }
                if (!strInArray("-p", string_0))
                {
                    Console.WriteLine("Missing admin/publisher name switch -p");
                    return false;
                }
            }
            string[] strArray3 = new string[] {"-h", "-d", "-n", "-u", "-p"};
            int index = 2;
            while (index < string_0.Length)
            {
                string str = string_0[index];
                switch (str)
                {
                    case null:
                        goto Label_0431;

                    case "-h":
                        if (index == (string_0.Length - 1))
                        {
                            Console.WriteLine("Missing host parameter, switch -h");
                            return false;
                        }
                        if (strInArray(string_0[index + 1], strArray3))
                        {
                            Console.WriteLine("Missing host parameter, switch -h");
                            return false;
                        }
                        index++;
                        break;

                    case "-d":
                        if (index == (string_0.Length - 1))
                        {
                            Console.WriteLine("Missing data source parameter, switch -d");
                            return false;
                        }
                        if (strInArray(string_0[index + 1], strArray3))
                        {
                            Console.WriteLine("Missing data source parameter, switch -d");
                            return false;
                        }
                        index++;
                        break;

                    case "-n":
                        if (index == (string_0.Length - 1))
                        {
                            Console.WriteLine("Missing service name parameter, switch -n");
                            return false;
                        }
                        if (strInArray(string_0[index + 1], strArray3))
                        {
                            Console.WriteLine("Missing service name parameter, switch -n");
                            return false;
                        }
                        index++;
                        break;

                    default:
                        if (!(str == "-u"))
                        {
                            if (!(str == "-p"))
                            {
                                goto Label_0431;
                            }
                            if (index == (string_0.Length - 1))
                            {
                                Console.WriteLine("Missing admin/publisher password parameter, switch -p");
                                return false;
                            }
                            if (strInArray(string_0[index + 1], strArray3))
                            {
                                Console.WriteLine("Missing admin/publisher password parameter, switch -p");
                                return false;
                            }
                            index++;
                        }
                        else
                        {
                            if (index == (string_0.Length - 1))
                            {
                                Console.WriteLine("Missing admin/publisher username parameter, switch -u");
                                return false;
                            }
                            if (strInArray(string_0[index + 1], strArray3))
                            {
                                Console.WriteLine("Missing admin/publisher username parameter, switch -u");
                                return false;
                            }
                            index++;
                        }
                        break;
                }
                index++;
            }
            return true;
            Label_0431:
            Console.WriteLine("Incorrect parameter switch: {0} is not a recognized.", string_0[index]);
            return false;
        }

        private static bool ValidateServiceName(IServerObjectAdmin iserverObjectAdmin_0, ref string string_0,
            string string_1)
        {
            IEnumServerObjectConfiguration configurations = iserverObjectAdmin_0.GetConfigurations();
            configurations.Reset();
            for (IServerObjectConfiguration configuration2 = configurations.Next();
                configuration2 != null;
                configuration2 = configurations.Next())
            {
                if (configuration2.Name.ToUpper() == string_0.ToUpper())
                {
                    string_0 = configuration2.Name;
                    return true;
                }
            }
            Console.WriteLine("Configuration {0} on {1} can not be found.", string_0, string_1);
            return false;
        }
    }
}