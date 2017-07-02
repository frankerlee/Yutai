using System.Collections.Generic;

namespace Yutai.Plugins.Interfaces
{
    public interface IPrintingConfig
    {
        void LoadFromXml(string fileName);
        void SaveToXml(string fileName);
        void Save();
        string TemplateConnectionString { get; set; }

        List<IIndexMap> IndexMaps { get; set; }
    }
}