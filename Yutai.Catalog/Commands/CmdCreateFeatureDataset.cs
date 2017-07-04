using System;
using System.Windows.Forms;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.Geodatabase.UI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdCreateFeatureDataset : YutaiCommand
    {
        public CmdCreateFeatureDataset(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_dataset;
            this.m_caption = "创建要素集";
            this.m_category = "Catalog";
            this.m_message = "创建要素集";
            this.m_name = "Catalog_NewFeatureDataset";
            this._key = "Catalog_NewFeatureDataset";
            this.m_toolTip = "创建要素集";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get { return _context.GxSelection != null; }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            if (((IGxSelection) _context.GxSelection).FirstObject is IGxDatabase)
            {
                GxCatalogCommon.ConnectGDB(((IGxSelection) _context.GxSelection).FirstObject as IGxDatabase);
                if ((((IGxSelection) _context.GxSelection).FirstObject as IGxDatabase).Workspace != null)
                {
                    frmNewFeatureDataset _frmNewFeatureDataset = new frmNewFeatureDataset()
                    {
                        Workspace = (((IGxSelection) _context.GxSelection).FirstObject as IGxDatabase).Workspace
                    };
                    try
                    {
                        if (_frmNewFeatureDataset.ShowDialog() == DialogResult.OK)
                        {
                            ((IGxSelection) _context.GxSelection).FirstObject.Refresh();
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}