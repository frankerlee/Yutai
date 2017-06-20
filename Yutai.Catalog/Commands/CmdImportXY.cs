using System;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.Geodatabase.UI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdImportXY : YutaiCommand
    {
        public CmdImportXY(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            //this.m_bitmap = Properties.Resources.icon_catalog_delete;
            this.m_caption = "导入XY坐标";
            this.m_category = "Catalog";
            this.m_message = "导入XY坐标";
            this.m_name = "Catalog_ImportXY";
            this._key = "Catalog_ImportXY";
            this.m_toolTip = "导入XY坐标";
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
                    if (((IGxSelection) _context.GxSelection).Count == 1)
                    {
                        if (((IGxSelection) _context.GxSelection).FirstObject is IGxDataset)
                        {
                            if ((((IGxSelection) _context.GxSelection).FirstObject as IGxDataset).Type == esriDatasetType.esriDTFeatureDataset)
                            {
                                result = true;
                                return result;
                            }
                            if ((((IGxSelection) _context.GxSelection).FirstObject as IGxDataset).Type == esriDatasetType.esriDTFeatureClass)
                            {
                                IFeatureClassName featureClassName = (((IGxSelection) _context.GxSelection).FirstObject as IGxDataset).DatasetName as IFeatureClassName;
                                if (featureClassName.ShapeType == esriGeometryType.esriGeometryPoint)
                                {
                                    result = true;
                                    return result;
                                }
                            }
                        }
                        else if (((IGxSelection) _context.GxSelection).FirstObject is IGxDatabase)
                        {
                            result = true;
                            return result;
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
            IGxObject firstObject = ((IGxSelection) _context.GxSelection).FirstObject;
            IDataset idataset_ = null;
            if (firstObject is IGxDataset)
            {
                idataset_ = (firstObject as IGxDataset).Dataset;
            }
            else if (firstObject is IGxDatabase)
            {
                idataset_ = ((firstObject as IGxDatabase).Workspace as IDataset);
            }
            frmImportxy frmImportxy = new frmImportxy(idataset_);
            frmImportxy.ShowDialog();
        }
    }
}