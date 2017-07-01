using System.IO;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.Geodatabase
{
    internal class MyNameMapping : INameMapping
    {
        public object m_pSource = null;

        private string string_0 = "";

        private bool bool_0 = false;

        public IEnumNameMapping Children
        {
            get { return null; }
        }

        public string ConfigKeyword
        {
            set { }
        }

        public bool NameConflicts
        {
            get { return this.bool_0; }
        }

        public object SourceObject
        {
            get { return this.m_pSource; }
        }

        public string TargetName
        {
            get { return this.string_0; }
            set { this.string_0 = value; }
        }

        public MyNameMapping()
        {
        }

        public string GetSuggestedName(IName iname_0)
        {
            string fileNameWithoutExtension = "";
            string name = "";
            if (iname_0 is IWorkspaceName)
            {
                string pathName = (iname_0 as IWorkspaceName).PathName;
                if (this.m_pSource is IDataset)
                {
                    name = (this.m_pSource as IDataset).Name;
                }
                else if (this.m_pSource is IDatasetName)
                {
                    name = (this.m_pSource as IDatasetName).Name;
                }
                if (name.Length > 0)
                {
                    fileNameWithoutExtension = Path.GetFileNameWithoutExtension(name);
                    if (this.bool_0)
                    {
                        name = this.method_0(iname_0, fileNameWithoutExtension);
                    }
                }
            }
            return fileNameWithoutExtension;
        }

        private string method_0(IName iname_0, string string_1)
        {
            string str;
            IFieldChecker fieldCheckerClass = new FieldChecker()
            {
                ValidateWorkspace = iname_0.Open() as IWorkspace
            };
            fieldCheckerClass.ValidateTableName(string_1, out str);
            fieldCheckerClass = null;
            return str;
        }

        public void ValidateTargetName(IName iname_0)
        {
            string name = "";
            if (iname_0 is IWorkspaceName)
            {
                IWorkspace2 workspace2 = iname_0.Open() as IWorkspace2;
                if (this.m_pSource is IDataset)
                {
                    name = (this.m_pSource as IDataset).Name;
                }
                else if (this.m_pSource is IDatasetName)
                {
                    name = (this.m_pSource as IDatasetName).Name;
                }
                if (name.Length > 0)
                {
                    name = Path.GetFileNameWithoutExtension(name);
                    this.string_0 = name;
                    this.bool_0 = workspace2.NameExists[esriDatasetType.esriDTFeatureClass, name];
                }
            }
        }
    }
}