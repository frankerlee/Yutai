using System;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdCreateVersionView : YutaiCommand
    {
        public CmdCreateVersionView(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            //this.m_bitmap = Properties.Resources.icon_catalog_delete;
            this.m_caption = "创建版本化视图";
            this.m_category = "Catalog";
            this.m_message = "创建版本化视图";
            this.m_name = "Catalog_CreateVersionView";
            this._key = "Catalog_CreateVersionView";
            this.m_toolTip = "创建版本化视图";
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
                        if (type == esriDatasetType.esriDTFeatureClass || type == esriDatasetType.esriDTTable ||
                            type == esriDatasetType.esriDTFeatureDataset)
                        {
                            IVersionedObject versionedObject =
                                (((IGxSelection) _context.GxSelection).FirstObject as IGxDataset).Dataset as
                                    IVersionedObject;
                            if (versionedObject == null)
                            {
                                result = false;
                                return result;
                            }
                            if (versionedObject.IsRegisteredAsVersioned)
                            {
                                result = true;
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
                IVersionedView versionedView =
                    (((IGxSelection) _context.GxSelection).FirstObject as IGxDataset).Dataset as IVersionedView;
                string name = ((IGxSelection) _context.GxSelection).FirstObject.Name + "_vw";
                versionedView.CreateVersionedView(name);
                System.Windows.Forms.MessageBox.Show("创建附件成果");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}