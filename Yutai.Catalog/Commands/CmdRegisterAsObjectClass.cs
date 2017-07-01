using System;
using System.IO;
using ESRI.ArcGIS.DataManagementTools;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geoprocessor;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdRegisterAsObjectClass : YutaiCommand
    {
        public CmdRegisterAsObjectClass(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            //this.m_bitmap = Properties.Resources.icon_catalog_delete;
            this.m_caption = "注册为Geodatebase数据库表";
            this.m_category = "Catalog";
            this.m_message = "注册为Geodatebase数据库表";
            this.m_name = "Catalog_RegisterAsObjectClass";
            this._key = "Catalog_RegisterAsObjectClass";
            this.m_toolTip = "注册为Geodatebase数据库表";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Text;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get
            {
                bool result;
                if (_context == null)
                {
                    result = false;
                }
                else if (_context.GxSelection == null)
                {
                    result = false;
                }
                else if (((IGxSelection) _context.GxSelection).Count > 1)
                {
                    result = false;
                }
                else
                {
                    if (((IGxSelection) _context.GxSelection).FirstObject is IGxDataset)
                    {
                        IObjectClass objectClass =
                            (((IGxSelection) _context.GxSelection).FirstObject as IGxDataset).Dataset as IObjectClass;
                        if (objectClass != null && objectClass.ObjectClassID == -1)
                        {
                            IWorkspace workspace = (objectClass as IDataset).Workspace;
                            if (workspace.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                            {
                                result = WorkspaceOperator.IsConnectedToGeodatabase(workspace);
                                return result;
                            }
                        }
                    }
                    result = false;
                }
                return result;
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            try
            {
                ITable table = (((IGxSelection) _context.GxSelection).FirstObject as IGxDataset).Dataset as ITable;
                if (table is IFeatureClass)
                {
                    Geoprocessor geoprocessor_ = new Geoprocessor();
                    string text = (table as IDataset).Workspace.PathName;
                    bool flag = false;
                    if (string.IsNullOrEmpty(text))
                    {
                        string startupPath = System.Windows.Forms.Application.StartupPath;
                        string text2 = Guid.NewGuid().ToString() + ".sde";
                        (table as IDataset).Workspace.WorkspaceFactory.Create(startupPath, text2,
                            (table as IDataset).Workspace.ConnectionProperties, 0);
                        text = System.IO.Path.Combine(startupPath, text2);
                        flag = true;
                    }
                    string text3 = text;
                    if ((table as IFeatureClass).FeatureDataset != null)
                    {
                        text3 = System.IO.Path.Combine(text3, (table as IFeatureClass).FeatureDataset.Name);
                    }
                    text3 = System.IO.Path.Combine(text3, (table as IDataset).Name);
                    if ((table as IFeatureClass).ShapeType != esriGeometryType.esriGeometryAny)
                    {
                        CommonHelper.RunTool(geoprocessor_, new RegisterWithGeodatabase
                        {
                            in_dataset = text3
                        }, null);
                        if (flag)
                        {
                            File.Delete(text);
                        }
                    }
                }
                else
                {
                    ISchemaLock schemaLock = (ISchemaLock) table;
                    schemaLock.ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock);
                    (table as IClassSchemaEdit).RegisterAsObjectClass("ObjectID", "");
                    schemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}