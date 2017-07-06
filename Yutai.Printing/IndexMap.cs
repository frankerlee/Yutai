using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;
using Yutai.Shared;
using WorkspaceHelper = Yutai.ArcGIS.Common.Helpers.WorkspaceHelper;

namespace Yutai.Plugins.Printing
{
    public class IndexMap : IIndexMap
    {
        private string _name;
        private string _indexLayerName;
        private string _templateName;
        private string _searchFields;
        private string _nameField;
        private string _workspaceName;

        private IFeatureClass _featureClass;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string WorkspaceName
        {
            get { return _workspaceName; }
            set { _workspaceName = value; }
        }

        public string IndexLayerName
        {
            get { return _indexLayerName; }
            set { _indexLayerName = value; }
        }

        public string TemplateName
        {
            get { return _templateName; }
            set { _templateName = value; }
        }

        public string SearchFields
        {
            get { return _searchFields; }
            set { _searchFields = value; }
        }

        public string NameField
        {
            get { return _nameField; }
            set { _nameField = value; }
        }

        public IndexMap()
        {
        }

        public IndexMap(XmlNode xmlNode)
        {
            ReadFromXml(xmlNode);
        }

        public void ReadFromXml(XmlNode xml)
        {
            if (xml == null)
                return;
            if (xml.Attributes != null)
            {
                _name = xml.Attributes["Name"] == null ? "" : xml.Attributes["Name"].Value;
                _templateName = xml.Attributes["Template"] == null ? "" : xml.Attributes["Template"].Value;
                _searchFields = xml.Attributes["SearchFields"] == null ? "" : xml.Attributes["SearchFields"].Value;
                _nameField = xml.Attributes["NameField"] == null ? "" : xml.Attributes["NameField"].Value;
                _indexLayerName = xml.Attributes["IndexLayer"] == null ? "" : xml.Attributes["IndexLayer"].Value;
                _workspaceName = xml.Attributes["Workspace"] == null ? "" : xml.Attributes["Workspace"].Value;
            }
        
        }

        public XmlNode SaveToXml(XmlDocument doc)
        {
            XmlNode layerNode = doc.CreateElement("IndexMap");
            XmlAttribute nameAttribute = doc.CreateAttribute("Name");
            nameAttribute.Value = _name;
            XmlAttribute codeAttribute = doc.CreateAttribute("Template");
            codeAttribute.Value = _templateName;
            XmlAttribute classcodeAttribute = doc.CreateAttribute("SearchFields");
            classcodeAttribute.Value = _searchFields;
            XmlAttribute autoNamesAttribute = doc.CreateAttribute("NameField");
            autoNamesAttribute.Value = _nameField;
            XmlAttribute wksAttribute = doc.CreateAttribute("Workspace");
            wksAttribute.Value = _workspaceName;
            XmlAttribute layerAttribute = doc.CreateAttribute("IndexLayer");
            layerAttribute.Value = _indexLayerName;
            layerNode.Attributes.Append(nameAttribute);
            layerNode.Attributes.Append(codeAttribute);
            layerNode.Attributes.Append(classcodeAttribute);
            layerNode.Attributes.Append(autoNamesAttribute);
            layerNode.Attributes.Append(wksAttribute);
            layerNode.Attributes.Append(layerAttribute);
            return layerNode;
        }

        public IFeatureCursor Search(string searchKey)
        {
            if (_featureClass == null)
            {
                bool back= TryOpenFeatureClass();
                if (!back)
                {
                    MessageService.Current.Warn("索引图层不能正确连接，请检查索引图层设置!");
                    return null;
                }
               
            }
            return TrySearch(searchKey);
        }

       
        private IFeatureCursor TrySearch(string searchKey)
        {
            string likeStr = Yutai.ArcGIS.Common.Helpers.WorkspaceHelper.GetSpecialCharacter(_featureClass as IDataset,
                esriSQLSpecialCharacters.esriSQL_WildcardManyMatch);
            IQueryFilter queryFilter = new QueryFilter();
            if (!string.IsNullOrEmpty(searchKey))
            {
                queryFilter.WhereClause = BuildWhereClause(_searchFields, searchKey, likeStr);
            }
            IFeatureCursor cursor = _featureClass.Search(queryFilter, false);
            return cursor;
        }
        private string BuildWhereClause(string locatorSearchFields, string searchKey, string likeStr)
        {
            string[] fields = locatorSearchFields.Split(',');
            string whereClause = "";
            for (int i = 0; i < fields.Length; i++)
            {
                if (i == 0)
                    whereClause = string.Format("{0} Like '{1}{2}{1}' ", fields[i], likeStr, searchKey);
                else
                    whereClause += string.Format(" OR {0} Like '{1}{2}{1}' ", fields[i], likeStr, searchKey);
            }
            return whereClause;
        }
        private bool TryOpenFeatureClass()
        {
            if (string.IsNullOrEmpty(_workspaceName)) return false;
            if (string.IsNullOrEmpty(_indexLayerName)) return false;

            IFeatureClass fClass = Yutai.ArcGIS.Common.Helpers.WorkspaceHelper.GetFeatureClass(_workspaceName,_indexLayerName);
            if (fClass == null) return false;
            _featureClass = fClass;
            return true;
        }

        public List<IPrintPageInfo> QueryPageInfo(IGeometry searchGeometry)
        {
            return QueryPageInfo(searchGeometry, "");
        }

        public List<IPrintPageInfo> QueryPageInfo(IGeometry searchGeometry,string searchKeys)
        {
            if (_featureClass == null)
            {
                TryOpenFeatureClass();
            }
            if (_featureClass == null)
            {
                MessageService.Current.Warn("索引图不可用，请检查索引图配置!");
                return null;
            }
            IQueryFilter queryFlter = null;
            string likeStr = WorkspaceHelper.GetSpecialCharacter(_featureClass as IDataset,
              esriSQLSpecialCharacters.esriSQL_WildcardManyMatch);
           
            if (searchGeometry != null && searchGeometry.IsEmpty == false)
            {
                ISpatialFilter filter = new SpatialFilter();
                filter.Geometry = searchGeometry;
                filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                queryFlter = filter;
            }
            if (queryFlter == null)
            {
                queryFlter=new QueryFilter();
            }
            if (!string.IsNullOrEmpty(searchKeys))
            {
                queryFlter.WhereClause= BuildWhereClause(_searchFields, searchKeys, likeStr);
            }
            IFeatureCursor pCursor = _featureClass.Search((IQueryFilter)queryFlter, false);
            List<IPrintPageInfo> pages = new List<IPrintPageInfo>();
            IFeature pFeature = pCursor.NextFeature();
            int i = 0;
            while (pFeature != null)
            {
                IPrintPageInfo page = new PrintPageInfo();
                page.Load(pFeature, _nameField);
                page.PageID = i + 1;
                pages.Add(page);
                i++;
                pFeature = pCursor.NextFeature();
            }

            foreach (IPrintPageInfo page in pages)
            {
                page.TotalCount = i;
            }
            
            ComReleaser.ReleaseCOMObject(pCursor);
           
            return pages;
        }

     
        public List<IPrintPageInfo> QueryPageInfo(string searchKeys)
        {
            return QueryPageInfo(null, searchKeys);
         
        }
    }
}
