using System.Collections.Generic;
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
        IFeatureWorkspace Workspace { get; set; }
        List<IObjectTemplate> Templates { get; set; }
        bool AddTemplate(IObjectTemplate template );
        bool DeleteTemplate(string templateName);
        bool Connect(string connectionString);
    }

    public interface IObjectDataset
    {
        string Name { get; set; }
        string AliasName { get; set; }
        string BaseName { get; set; }

    }
}