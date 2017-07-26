using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Plugins.Services;
using Yutai.Plugins.Template.Interfaces;

namespace Yutai.Plugins.Template.Concretes
{
    public class TemplateDatabase:ITemplateDatabase
    {
        private string _connectionString;
       
        private IFeatureWorkspace _workspace;
        private List<IObjectTemplate> _templates;
        private List<IObjectDataset> _datasets;
        private List<IYTDomain> _domains;

        public string DatabaseName
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public bool IsConnect
        {
            get { return _workspace == null ? false : true; }
        }

        public bool Connect()
        {
            _workspace = Yutai.ArcGIS.Common.Helpers.WorkspaceHelper.GetWorkspace(_connectionString);
            if (_workspace == null) return false;
            return true;
        }

        public void LoadDomains()
        {
            if (_workspace == null)
            {
                Connect();
            }
            if (_domains != null)
                _domains.Clear();
            else
                _domains = new List<IYTDomain>();
            ITable pDomainTable = _workspace.OpenTable("YT_TEMPLATE_DOMAIN");
            
            ITableSort tableSort = new TableSort();
            tableSort.Table = pDomainTable;
            tableSort.Fields = "DomainName";
            tableSort.Sort(null);

            ICursor pCursor = tableSort.Rows;
            int[] fieldIndexes = new int[3];
            IRow pRow = pCursor.NextRow();
          
            IYTDomain domain = null;
            while (pRow != null)
            {
                domain = new YTDomain(pRow);
              _domains.Add(domain);
                pRow = pCursor.NextRow();
            }
            ComReleaser.ReleaseCOMObject(pCursor);
            ComReleaser.ReleaseCOMObject(pDomainTable);
           
        }

        //public void LoadTemplates()
        //{
        //    if (_workspace == null)
        //    {
        //        Connect();
        //    }
        //    if (_templates!=null)
        //        _templates.Clear();
        //   else
        //        _templates=new List<IObjectTemplate>();
        //    ITable pTemplateTable = _workspace.OpenTable("YT_TEMPLATE_FEATURECLASS");
        //    ITable pTemplateFields= _workspace.OpenTable("YT_TEMPLATE_FIELD");

        //    ITableSort tableSort = new TableSort();
        //    tableSort.Table = pTemplateTable;
        //    tableSort.Fields = "TemplateName";
        //    tableSort.Sort(null);

        //    ICursor pCursor = tableSort.Rows;
        //    int[] fieldIndexes = new int[6];
        //    IRow pRow = pCursor.NextRow();
        //    IQueryFilter pQueryFilter = new QueryFilter();

        //    IObjectTemplate oneTemplate = null;
        //    while (pRow != null)
        //    {
        //        oneTemplate = new ObjectTemplate(pRow);
        //        pQueryFilter.WhereClause = "TemplateName='" + oneTemplate.Name+"'";
        //        ICursor pCursor1 = pTemplateFields.Search(pQueryFilter, false);
        //        IRow fieldRow = pCursor1.NextRow();
        //        while (fieldRow != null)
        //        {
        //            YTField ytField=new YTField(fieldRow);
        //            oneTemplate.Fields.Add(ytField);
        //            fieldRow = pCursor1.NextRow();
        //        }
        //        ComReleaser.ReleaseCOMObject(pCursor1);
        //        oneTemplate.Database = this;
        //        _templates.Add(oneTemplate);
        //        pRow = pCursor.NextRow();
        //    }
        //    ComReleaser.ReleaseCOMObject(pCursor);
        //    ComReleaser.ReleaseCOMObject(pTemplateTable);
        //    ComReleaser.ReleaseCOMObject(pTemplateFields);
        //}


        public void LoadTemplates()
        {
            if (_workspace == null)
            {
                Connect();
            }
            if (_templates != null)
                _templates.Clear();
            else
                _templates = new List<IObjectTemplate>();
            ITable pTemplateTable = _workspace.OpenTable("YT_TEMPLATE_FEATURECLASS");
        

            ITableSort tableSort = new TableSort();
            tableSort.Table = pTemplateTable;
            tableSort.Fields = "TemplateName";
            tableSort.Sort(null);

            ICursor pCursor = tableSort.Rows;
            int[] fieldIndexes = new int[6];
            IRow pRow = pCursor.NextRow();
            IQueryFilter pQueryFilter = new QueryFilter();

            IObjectTemplate oneTemplate = null;
            while (pRow != null)
            {
                oneTemplate = new ObjectTemplate(pRow);
                oneTemplate.Database = this;
                _templates.Add(oneTemplate);
                pRow = pCursor.NextRow();
            }
            ComReleaser.ReleaseCOMObject(pCursor);
            ComReleaser.ReleaseCOMObject(pTemplateTable);
          
        }

        public void LoadDatasets()
        {
            if (_workspace == null)
            {
                Connect();
            }
            if (_datasets != null)
                _datasets.Clear();
            else
                _datasets = new List<IObjectDataset>();
            ITable pTemplateTable = _workspace.OpenTable("YT_TEMPLATE_DATASET");
            ITableSort tableSort = new TableSort();
            tableSort.Table = pTemplateTable;
            tableSort.Fields = "Dataset";
            tableSort.Sort(null);

            ICursor pCursor = tableSort.Rows;
           
            IRow pRow = pCursor.NextRow();
            IQueryFilter pQueryFilter = new QueryFilter();

            IObjectDataset oneDataset = null;
            while (pRow != null)
            {
                oneDataset = new ObjectDataset(pRow);
              
                _datasets.Add(oneDataset);
                pRow = pCursor.NextRow();
            }
            ComReleaser.ReleaseCOMObject(pCursor);
            ComReleaser.ReleaseCOMObject(pTemplateTable);
           
        }

        public List<IObjectTemplate> GetTemplatesByDataset(string datasetName)
        {
           List<IObjectTemplate> templates= new List<IObjectTemplate>();
            ITable pTemplateTable = _workspace.OpenTable("YT_TEMPLATE_FEATURECLASS");
          

           IQueryFilter pFilter1=new QueryFilter();
            pFilter1.WhereClause = "Dataset='" + datasetName + "'";

            ICursor pCursor = pTemplateTable.Search(pFilter1,false);
            int[] fieldIndexes = new int[6];
            IRow pRow = pCursor.NextRow();
            IQueryFilter pQueryFilter = new QueryFilter();

            IObjectTemplate oneTemplate = null;
            while (pRow != null)
            {
                oneTemplate = new ObjectTemplate(pRow);
                templates.Add(oneTemplate);
                pRow = pCursor.NextRow();
            }
            ComReleaser.ReleaseCOMObject(pFilter1);
            ComReleaser.ReleaseCOMObject(pCursor);
            ComReleaser.ReleaseCOMObject(pTemplateTable);
            return templates;
        }


        public bool DisConnect()
        {
            if (_workspace == null) return true;
            _workspace = null;
            return true;
        }

        public IFeatureWorkspace Workspace
        {
            get { return _workspace; }
            set { _workspace = value; }
        }

        public List<IObjectTemplate> Templates
        {
            get
            {
                return _templates;
            }
            set { _templates = value; }
        }

        public List<IObjectDataset> Datasets
        {
            get { return _datasets; }
            set { _datasets = value; }
        }

        public List<IYTDomain> Domains
        {
            get { return _domains; }
            set { _domains = value; }
        }

        public bool SaveTemplate(IObjectTemplate template)
        {
            try
            {
                if (_workspace == null)
                {
                    Connect();
                }

                ITable pTable = _workspace.OpenTable("YT_TEMPLATE_FEATURECLASS");
                IWorkspaceEdit pWksEdit = _workspace as IWorkspaceEdit;
                pWksEdit.StartEditing(true);
                pWksEdit.StartEditOperation();

                IRow pRow;
                int oldID = template.ID;
                if(oldID > 0)
                    pRow=pTable.GetRow(template.ID);
                else
                    pRow= pTable.CreateRow();
                template.UpdateRow(pRow);

              

                //pTable = _workspace.OpenTable("YT_TEMPLATE_FIELD");
                //if (oldID > 0)
                //{
                //    //删除数据库中原有的字段定义
                //    IQueryFilter filter=new QueryFilterClass();
                //    filter.WhereClause = "TemplateName='" + template.Name + "'";
                //    ICursor pCursor = pTable.Search(filter, false);
                //    pRow = pCursor.NextRow();
                //    while (pRow != null)
                //    {
                //        pRow.Delete();
                //        pRow = pCursor.NextRow();
                //    }
                //    ComReleaser.ReleaseCOMObject(pCursor);

                //}
                //int tmpIndex = pTable.FindField("TemplateName");
                //foreach (IYTField ytField in template.Fields)
                //{
                //    pRow = pTable.CreateRow();
                //    pRow.set_Value(tmpIndex, template.Name);
                //    ytField.UpdateRow(pRow);
                //}
                pWksEdit.StopEditOperation();
                pWksEdit.StopEditing(true);
                if (oldID <= 0)
                    _templates.Add(template);
                ComReleaser.ReleaseCOMObject(pTable);
                DisConnect();

                return true;
            }
            catch (Exception ex)
            {
                MessageService.Current.Warn("系统发生错误:" + ex.Message);
                return false;

                throw;
            }


        }
        public bool AddTemplate(IObjectTemplate template)
        {
            try
            {
                IObjectTemplate objectTemplate = _templates.FirstOrDefault(c => c.Name == template.Name);
                if (objectTemplate == null)
                {
                    MessageService.Current.Warn("该模板已经存在，请更换名字!");
                    return false;
                }

                if (_workspace == null)
                {
                    Connect();
                }

                ITable pTable = _workspace.OpenTable("YT_TEMPLATE_FEATURECLASS");
                IWorkspaceEdit pWksEdit=_workspace as IWorkspaceEdit;
                pWksEdit.StartEditing(true);
                pWksEdit.StartEditOperation();
                IRow pRow = pTable.CreateRow();
                template.UpdateRow(pRow);
                
                //pTable = _workspace.OpenTable("YT_TEMPLATE_FIELD");
                //int tmpIndex = pTable.FindField("TemplateName");
                //foreach (IYTField ytField in template.Fields)
                //{
                //    pRow = pTable.CreateRow();
                //    pRow.set_Value(tmpIndex,template.Name);
                //    ytField.UpdateRow(pRow);
                //}
                pWksEdit.StopEditOperation();
                pWksEdit.StopEditing(true);
                _templates.Add(template);
                ComReleaser.ReleaseCOMObject(pTable);
                DisConnect();
               
                return true;
            }
            catch (Exception ex)
            {
                MessageService.Current.Warn("系统发生错误:"+ex.Message);
                return false;
                
                throw;
            }


        }

        public bool DeleteTemplate(string templateName)
        {
            try
            {
                IObjectTemplate objectTemplate = _templates.FirstOrDefault(c => c.Name == templateName);
                if (objectTemplate == null || objectTemplate.ID<=0)
                {
                    return true;
                }

                if (_workspace == null)
                {
                    Connect();
                }

                ITable pTable = _workspace.OpenTable("YT_TEMPLATE_FEATURECLASS");
                IWorkspaceEdit pWksEdit = _workspace as IWorkspaceEdit;
                pWksEdit.StartEditing(true);
                pWksEdit.StartEditOperation();

                IRow pRow = pTable.GetRow(objectTemplate.ID);
                if (pRow != null)
                {
                    pRow.Delete();
                }
                

                //pTable = _workspace.OpenTable("YT_TEMPLATE_FIELD");
                //foreach (IYTField ytField in objectTemplate.Fields)
                //{
                //    if (ytField.ID > 0)
                //    {
                //        pRow = pTable.GetRow(ytField.ID);
                //        if(pRow != null) pRow.Delete();
                //    }
                //}
                pWksEdit.StopEditOperation();
                pWksEdit.StopEditing(true);

                DisConnect();
                return true;
            }
            catch (Exception ex)
            {
                MessageService.Current.Warn("系统发生错误:" + ex.Message);
                return false;

                throw;
            }
        }

        public bool Connect(string connectionString)
        {
            if (_workspace != null) DisConnect();
            _connectionString = connectionString;
            return Connect();
        }

        public int GetObjectID(string objectName, enumTemplateObjectType objectType)
        {
            if (_workspace == null)
            {
                Connect();
            }
           IQueryFilter filter=new QueryFilterClass();
            ITable pTable;
            switch (objectType)
            {
                case enumTemplateObjectType.FeatureClass:
                    pTable = _workspace.OpenTable("YT_TEMPLATE_FEATURECLASS");
                    filter.WhereClause = "TemplateName='" + objectName + "'";
                    break;
                case enumTemplateObjectType.Domain:
                    pTable = _workspace.OpenTable("YT_TEMPLATE_DOMAIN");
                    filter.WhereClause = "DomainName='" + objectName + "'";
                    break;
                case enumTemplateObjectType.FeatureDataset:
                    pTable = _workspace.OpenTable("YT_TEMPLATE_DATASET");
                    filter.WhereClause = "Dataset='" + objectName + "'";
                    break;
                default:
                    return -1;
            }

            ICursor pCursor=pTable.Search(filter,false);
            IRow pRow = pCursor.NextRow();
            int oid = -1;
            if (pRow != null) oid = pRow.OID;
            ComReleaser.ReleaseCOMObject(filter);
            ComReleaser.ReleaseCOMObject(pCursor);

            return oid;
        }

        public bool DeleteObject(int objectID, enumTemplateObjectType objectType)
        {
            if (_workspace == null)
            {
                Connect();
            }
            IWorkspaceEdit pWksEdit = _workspace as IWorkspaceEdit;
            pWksEdit.StartEditing(true);
            pWksEdit.StartEditOperation();
            ITable pTable;
            IRow pRow;
            switch (objectType)
            {
                case enumTemplateObjectType.FeatureClass:
                    pTable = _workspace.OpenTable("YT_TEMPLATE_FEATURECLASS");
                    pRow = pTable.GetRow(objectID);
                  
                    break;
                case enumTemplateObjectType.Domain:
                    pTable = _workspace.OpenTable("YT_TEMPLATE_DOMAIN");
                    pRow = pTable.GetRow(objectID);
                    break;
                case enumTemplateObjectType.FeatureDataset:
                    pTable = _workspace.OpenTable("YT_TEMPLATE_DATASET");
                    pRow = pTable.GetRow(objectID);
                    break;
                default:
                    return false;
            }

           pRow.Delete();
            pWksEdit.StopEditOperation();
            pWksEdit.StopEditing(true);

            DisConnect();
            return true;
        }

        public bool SaveDomain(IYTDomain domain)
        {
            try
            {
                if (_workspace == null)
                {
                    Connect();
                }
                if (_domains == null)
                {
                    _domains=new List<IYTDomain>();
                }
                ITable pTable = _workspace.OpenTable("YT_TEMPLATE_DOMAIN");
                IWorkspaceEdit pWksEdit = _workspace as IWorkspaceEdit;
                pWksEdit.StartEditing(true);
                pWksEdit.StartEditOperation();

                IRow pRow;
                int oldID = domain.ID;
                if (oldID > 0)
                    pRow = pTable.GetRow(domain.ID);
                else
                    pRow = pTable.CreateRow();
                domain.UpdateRow(pRow);
                
                pWksEdit.StopEditOperation();
                pWksEdit.StopEditing(true);
                if (oldID <= 0)
                    _domains.Add(domain);
                ComReleaser.ReleaseCOMObject(pTable);
                DisConnect();
                return true;
            }
            catch (Exception ex)
            {
                MessageService.Current.Warn("系统发生错误:" + ex.Message);
                return false;

                throw;
            }

        }

        public bool SaveDataset(IObjectDataset dataset)
        {
            try
            {
                if (_workspace == null)
                {
                    Connect();
                }
                if (_datasets == null)
                {
                    _datasets = new List<IObjectDataset>();
                }
                ITable pTable = _workspace.OpenTable("YT_TEMPLATE_DATASET");
                IWorkspaceEdit pWksEdit = _workspace as IWorkspaceEdit;
                pWksEdit.StartEditing(true);
                pWksEdit.StartEditOperation();

                IRow pRow;
                int oldID = dataset.ID;
                if (oldID > 0)
                    pRow = pTable.GetRow(dataset.ID);
                else
                    pRow = pTable.CreateRow();
                dataset.UpdateRow(pRow);

                pWksEdit.StopEditOperation();
                pWksEdit.StopEditing(true);
                if (oldID <= 0)
                    _datasets.Add(dataset);
                ComReleaser.ReleaseCOMObject(pTable);
                DisConnect();
                return true;
            }
            catch (Exception ex)
            {
                MessageService.Current.Warn("系统发生错误:" + ex.Message);
                return false;

                throw;
            }

        }

        public bool IsTemplateDatabase()
        {
            if (_workspace == null)
            {
                Connect();
            }

            bool isExists1 = ((IWorkspace2)_workspace).get_NameExists(esriDatasetType.esriDTTable, "YT_TEMPLATE_FEATURECLASS");
            // bool isExists2 = ((IWorkspace2)pWorkspace).get_NameExists(esriDatasetType.esriDTTable, "YT_TEMPLATE_FIELD");
            bool isExists3 = ((IWorkspace2)_workspace).get_NameExists(esriDatasetType.esriDTTable, "YT_TEMPLATE_DOMAIN");
            bool isExists4 = ((IWorkspace2)_workspace).get_NameExists(esriDatasetType.esriDTTable, "YT_TEMPLATE_DATASET");
            DisConnect();
            return isExists1 && isExists3 && isExists4;
        }

        public bool RegisterTemplateDatabase()
        {
            if (_workspace == null)
            {
                Connect();
            }

            bool isExists1 = ((IWorkspace2)_workspace).get_NameExists(esriDatasetType.esriDTTable, "YT_TEMPLATE_FEATURECLASS");
            if (!isExists1)
            {
                CreateFeatureClassTable();
            }
            // bool isExists2 = ((IWorkspace2)pWorkspace).get_NameExists(esriDatasetType.esriDTTable, "YT_TEMPLATE_FIELD");
            bool isExists3 = ((IWorkspace2)_workspace).get_NameExists(esriDatasetType.esriDTTable, "YT_TEMPLATE_DOMAIN");
            if (!isExists3)
            {
                CreateDomainTable();
            }
            bool isExists4 = ((IWorkspace2)_workspace).get_NameExists(esriDatasetType.esriDTTable, "YT_TEMPLATE_DATASET");
            if (!isExists4)
            {
                CreateDatasetTable();
            }
            DisConnect();
            return true;
        }

        private void CreateFeatureClassTable()
        {
            if (_workspace == null)
            {
                Connect();
            }
            
            IFieldsEdit  fieldsEdit=new FieldsClass();
            IField pField = FieldHelper.CreateOIDField();
            fieldsEdit.AddField(pField);

            IFieldEdit pFieldEdit=new FieldClass();
            pFieldEdit.Name_2 = "TemplateName";
            pFieldEdit.Length_2 = 255;
            pFieldEdit.AliasName_2 = "模板名称";
            pFieldEdit.IsNullable_2 = false;
            pFieldEdit.Type_2= esriFieldType.esriFieldTypeString;
            fieldsEdit.AddField(pFieldEdit);

            pFieldEdit = new FieldClass();
            pFieldEdit.Name_2 = "BaseName";
            pFieldEdit.Length_2 = 255;
            pFieldEdit.AliasName_2 = "基本名称";
            pFieldEdit.IsNullable_2 = false;
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            fieldsEdit.AddField(pFieldEdit);

            pFieldEdit = new FieldClass();
            pFieldEdit.Name_2 = "AliasName";
            pFieldEdit.Length_2 = 255;
            pFieldEdit.AliasName_2 = "别名";
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            fieldsEdit.AddField(pFieldEdit);

            pFieldEdit = new FieldClass();
            pFieldEdit.Name_2 = "Dataset";
            pFieldEdit.Length_2 = 255;
            pFieldEdit.AliasName_2 = "数据集";
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            fieldsEdit.AddField(pFieldEdit);

            pFieldEdit = new FieldClass();
            pFieldEdit.Name_2 = "FeatureType";
            pFieldEdit.Length_2 = 30;
            pFieldEdit.AliasName_2 = "要素类型";
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            fieldsEdit.AddField(pFieldEdit);

            pFieldEdit = new FieldClass();
            pFieldEdit.Name_2 = "GeometryType";
            pFieldEdit.Length_2 = 30;
            pFieldEdit.AliasName_2 = "图形类型";
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            fieldsEdit.AddField(pFieldEdit);

            pFieldEdit = new FieldClass();
            pFieldEdit.Name_2 = "FieldDefs";
            pFieldEdit.Length_2 = 2147483647;
            pFieldEdit.AliasName_2 = "字段定义";
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            fieldsEdit.AddField(pFieldEdit);

            IObjectClassDescription ocDescription = new ObjectClassDescriptionClass();
            _workspace.CreateTable("YT_TEMPLATE_FEATURECLASS", (IFields) fieldsEdit, ocDescription.InstanceCLSID, null,
                "");

            return;
        }

        private void CreateDatasetTable()
        {
            if (_workspace == null)
            {
                Connect();
            }
          
            IFieldsEdit fieldsEdit = new FieldsClass();
            IField pField = FieldHelper.CreateOIDField();
            fieldsEdit.AddField(pField);

            IFieldEdit pFieldEdit = new FieldClass();
            pFieldEdit.Name_2 = "Dataset";
            pFieldEdit.Length_2 = 255;
            pFieldEdit.AliasName_2 = "数据集名称";
            pFieldEdit.IsNullable_2 = false;
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            fieldsEdit.AddField(pFieldEdit);

            pFieldEdit = new FieldClass();
            pFieldEdit.Name_2 = "BaseName";
            pFieldEdit.Length_2 = 255;
            pFieldEdit.AliasName_2 = "基本名称";
            pFieldEdit.IsNullable_2 = false;
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            fieldsEdit.AddField(pFieldEdit);

            pFieldEdit = new FieldClass();
            pFieldEdit.Name_2 = "AliasName";
            pFieldEdit.Length_2 = 255;
            pFieldEdit.AliasName_2 = "别名";
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            fieldsEdit.AddField(pFieldEdit);

          

            IObjectClassDescription ocDescription = new ObjectClassDescriptionClass();
            _workspace.CreateTable("YT_TEMPLATE_DATASET", (IFields)fieldsEdit, ocDescription.InstanceCLSID, null,
                "");

            return;
        }

        private void CreateDomainTable()
        {
            if (_workspace == null)
            {
                Connect();
            }
         
            IFieldsEdit fieldsEdit = new FieldsClass();
            IField pField = FieldHelper.CreateOIDField();
            fieldsEdit.AddField(pField);

            IFieldEdit pFieldEdit = new FieldClass();
            pFieldEdit.Name_2 = "DomainName";
            pFieldEdit.Length_2 = 255;
            pFieldEdit.AliasName_2 = "字典名称";
            pFieldEdit.IsNullable_2 = false;
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            fieldsEdit.AddField(pFieldEdit);

            pFieldEdit = new FieldClass();
            pFieldEdit.Name_2 = "DomainDescription";
            pFieldEdit.Length_2 = 255;
            pFieldEdit.AliasName_2 = "字典说明";
            pFieldEdit.IsNullable_2 = false;
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            fieldsEdit.AddField(pFieldEdit);

            pFieldEdit = new FieldClass();
            pFieldEdit.Name_2 = "DomainValues";
            pFieldEdit.Length_2 = 2147483647;
            pFieldEdit.AliasName_2 = "别名";
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            fieldsEdit.AddField(pFieldEdit);



            IObjectClassDescription ocDescription = new ObjectClassDescriptionClass();
            _workspace.CreateTable("YT_TEMPLATE_DOMAIN", (IFields)fieldsEdit, ocDescription.InstanceCLSID, null,
                "");

            return;
        }
    }
}
