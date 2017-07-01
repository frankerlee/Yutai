using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.Geodatabase.UI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdCreateRelationClass : YutaiCommand
    {
        public CmdCreateRelationClass(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            //this.m_bitmap = Properties.Resources.icon_catalog_delete;
            this.m_caption = "关系类";
            this.m_category = "Catalog";
            this.m_message = "新建关系类";
            this.m_name = "Catalog_NewRelationClass";
            this._key = "Catalog_NewRelationClass";
            this.m_toolTip = "新建关系类";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Text;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get { return _context.GxSelection != null && ((IGxSelection) _context.GxSelection).FirstObject != null; }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            frmNewRelationClass _frmNewRelationClass;
            try
            {
                if (((IGxSelection) _context.GxSelection).FirstObject is IGxDatabase)
                {
                    GxCatalogCommon.ConnectGDB(((IGxSelection) _context.GxSelection).FirstObject as IGxDatabase);
                    if ((((IGxSelection) _context.GxSelection).FirstObject as IGxDatabase).Workspace != null)
                    {
                        _frmNewRelationClass = new frmNewRelationClass()
                        {
                            Workspace = (((IGxSelection) _context.GxSelection).FirstObject as IGxDatabase).Workspace
                        };
                        try
                        {
                            if (_frmNewRelationClass.ShowDialog() == DialogResult.OK)
                            {
                                ((IGxSelection) _context.GxSelection).FirstObject.Refresh();
                            }
                        }
                        catch (Exception exception)
                        {
                            // CErrorLog.writeErrorLog(this, exception, "");
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else if (((IGxSelection) _context.GxSelection).FirstObject is IGxDataset)
                {
                    IDataset dataset = (((IGxSelection) _context.GxSelection).FirstObject as IGxDataset).Dataset;
                    if (dataset is IFeatureDataset)
                    {
                        _frmNewRelationClass = new frmNewRelationClass()
                        {
                            FeatureDataset = dataset as IFeatureDataset
                        };
                        try
                        {
                            if (_frmNewRelationClass.ShowDialog() == DialogResult.OK)
                            {
                                ((IGxSelection) _context.GxSelection).FirstObject.Refresh();
                            }
                        }
                        catch (Exception exception1)
                        {
                            //CErrorLog.writeErrorLog(this, exception1, "");
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }
}