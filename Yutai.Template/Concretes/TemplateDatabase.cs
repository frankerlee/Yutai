using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Plugins.Services;
using Yutai.Plugins.Template.Interfaces;

namespace Yutai.Plugins.Template.Concretes
{
    public class TemplateDatabase:ITemplateDatabase
    {
        private string _connectionString;
       
        private IFeatureWorkspace _workspace;
        private List<IObjectTemplate> _templates;

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

        public void LoadTemplates()
        {
           if(_templates!=null)
                _templates.Clear();
           else
                _templates=new List<IObjectTemplate>();
            ITable pTemplateTable = _workspace.OpenTable("YT_TEMPLATE_FEATURECLASS");
            ITable pTemplateFields= _workspace.OpenTable("YT_TEMPLATE_FIELD");

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
                pQueryFilter.WhereClause = "TemplateName='" + oneTemplate.Name+"'";
                ICursor pCursor1 = pTemplateFields.Search(pQueryFilter, false);
                IRow fieldRow = pCursor1.NextRow();
                while (fieldRow != null)
                {
                    IYTField ytField=new YTField(fieldRow);
                    oneTemplate.Fields.Add(ytField);
                    fieldRow = pCursor1.NextRow();
                }
                ComReleaser.ReleaseCOMObject(pCursor1);
                _templates.Add(oneTemplate);
                pRow = pCursor.NextRow();
            }
            ComReleaser.ReleaseCOMObject(pCursor);
            ComReleaser.ReleaseCOMObject(pTemplateTable);
            ComReleaser.ReleaseCOMObject(pTemplateFields);
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
                
                pTable = _workspace.OpenTable("YT_TEMPLATE_FIELD");
                foreach (IYTField ytField in template.Fields)
                {
                    pRow = pTable.CreateRow();
                    ytField.UpdateRow(pRow);
                }
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
                

                pTable = _workspace.OpenTable("YT_TEMPLATE_FIELD");
                foreach (IYTField ytField in objectTemplate.Fields)
                {
                    if (ytField.ID > 0)
                    {
                        pRow = pTable.GetRow(ytField.ID);
                        if(pRow != null) pRow.Delete();
                    }
                }
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
    }
}
