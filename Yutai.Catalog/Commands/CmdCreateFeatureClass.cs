using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.Geodatabase.UI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdCreateFeatureClass : YutaiCommand
    {
        public CmdCreateFeatureClass(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_featureclass;
            this.m_caption = "新建要素类";
            this.m_category = "Catalog";
            this.m_message = "新建要素类";
            this.m_name = "Catalog_NewFeatureClass";
            this._key = "Catalog_NewFeatureClass";
            this.m_toolTip = "新建要素类";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get { return true; }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            frmNewObjectClass frmNewObjectClass = new frmNewObjectClass();
            IObjectClass objectClass = null;
            IGxSelection gxSelection = _context.GxSelection as IGxSelection;
            if (gxSelection.FirstObject is IGxDatabase)
            {
                GxCatalogCommon.ConnectGDB(gxSelection.FirstObject as IGxDatabase);
                if ((gxSelection.FirstObject as IGxDatabase).Workspace == null)
                {
                    return;
                }
                frmNewObjectClass.Workspace = (gxSelection.FirstObject as IGxDatabase).Workspace;
                if (frmNewObjectClass.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    objectClass = frmNewObjectClass.ObjectClass;
                }
            }
            else if (gxSelection.FirstObject is IGxDataset &&
                     (gxSelection.FirstObject as IGxDataset).DatasetName.Type == esriDatasetType.esriDTFeatureDataset)
            {
                try
                {
                    frmNewObjectClass.Workspace = (gxSelection.FirstObject as IGxDataset).Dataset;
                    if (frmNewObjectClass.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        objectClass = frmNewObjectClass.ObjectClass;
                    }
                }
                catch
                {
                    MessageService.Current.Warn("该要素集有问题，不能新建要素类!");
                }
            }
            if (objectClass != null)
            {
                gxSelection.FirstObject.Refresh();
            }
        }
    }
}