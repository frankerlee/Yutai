using System;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdDeleteAttachments : YutaiCommand
    {

        public CmdDeleteAttachments(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            //this.m_bitmap = Properties.Resources.icon_catalog_delete;
            this.m_caption = "删除附件";
            this.m_category = "Catalog";
            this.m_message = "删除附件";
            this.m_name = "Catalog_DeleteAttachments";
            this._key = "Catalog_DeleteAttachments";
            this.m_toolTip = "删除附件";
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
                if (_context.GxSelection == null)
                {
                    result = false;
                }
                else
                {
                    if (((IGxSelection) _context.GxSelection).FirstObject is IGxDataset)
                    {
                        esriDatasetType type = (((IGxSelection) _context.GxSelection).FirstObject as IGxDataset).Type;
                        if (type == esriDatasetType.esriDTFeatureClass || type == esriDatasetType.esriDTTable)
                        {
                            ITableAttachments tableAttachments = (ITableAttachments)(((IGxSelection) _context.GxSelection).FirstObject as IGxDataset).Dataset;
                            if (tableAttachments != null)
                            {
                                try
                                {
                                    if (tableAttachments.HasAttachments)
                                    {
                                        result = true;
                                        return result;
                                    }
                                }
                                catch
                                {
                                }
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
                ITableAttachments tableAttachments = (ITableAttachments)(((IGxSelection) _context.GxSelection).FirstObject as IGxDataset).Dataset;
                tableAttachments.DeleteAttachments();
                System.Windows.Forms.MessageBox.Show("创建附件成果");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

    }
}