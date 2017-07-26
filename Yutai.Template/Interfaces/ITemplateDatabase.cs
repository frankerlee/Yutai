using System.Collections.Generic;
using System.Xml;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Plugins.Template.Interfaces
{
    public interface ITemplateDatabase
    {
        string DatabaseName { get; set; }
        bool IsConnect { get; }
        bool Connect();
        bool DisConnect();

        void LoadTemplates();
        void LoadDatasets();
        void LoadDomains();
        List<IObjectTemplate> GetTemplatesByDataset(string datasetName);
        IFeatureWorkspace Workspace { get; set; }
        List<IObjectTemplate> Templates { get; set; }
        List<IObjectDataset> Datasets { get; set; }

        List<IYTDomain> Domains { get; set; }
        bool AddTemplate(IObjectTemplate template );

        bool SaveTemplate(IObjectTemplate template);
        bool DeleteTemplate(string templateName);
        bool Connect(string connectionString);

        int GetObjectID(string objectName, enumTemplateObjectType objectType);

        bool DeleteObject(int objectID, enumTemplateObjectType objectType);
        bool SaveDomain(IYTDomain domain);
        bool SaveDataset(IObjectDataset dataset);

        bool IsTemplateDatabase();

        bool RegisterTemplateDatabase();
    }

    public enum enumTemplateObjectType
    {
        FeatureClass,
        Domain,
        FeatureDataset
    }
    public interface IObjectDataset
    {
        int ID { get; set; }
        string Name { get; set; }
        string AliasName { get; set; }
        string BaseName { get; set; }
        void ReadFromXml(XmlNode xmlNode);
        XmlNode ToXml(XmlDocument doc);

        void UpdateRow(IRow pRow);

    }
}