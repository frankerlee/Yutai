using System;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdUnregisterVersion : YutaiCommand
    {
        public CmdUnregisterVersion(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            //this.m_bitmap = Properties.Resources.icon_catalog_delete;
            this.m_caption = "取消注册";
            this.m_category = "Catalog";
            this.m_message = "取消注册";
            this.m_name = "Catalog_UnregisterVersion";
            this._key = "Catalog_UnregisterVersion";
            this.m_toolTip = "取消注册";
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
                else if (((IGxSelection) _context.GxSelection).FirstObject == null)
                {
                    result = false;
                }
                else if (((IGxSelection) _context.GxSelection).FirstObject is IGxDataset)
                {
                    if ((((IGxSelection) _context.GxSelection).FirstObject as IGxDataset).DatasetName is IFeatureClassName)
                    {
                        if (((((IGxSelection) _context.GxSelection).FirstObject as IGxDataset).DatasetName as IFeatureClassName).FeatureDatasetName != null)
                        {
                            result = false;
                            return result;
                        }
                    }
                    try
                    {
                        IVersionedObject3 versionedObject = (((IGxSelection) _context.GxSelection).FirstObject as IGxDataset).Dataset as IVersionedObject3;
                        if (versionedObject == null)
                        {
                            result = false;
                            return result;
                        }
                        bool flag;
                        bool flag2;
                        versionedObject.GetVersionRegistrationInfo(out flag, out flag2);
                        if (!versionedObject.IsRegisteredAsVersioned)
                        {
                            if ((((IGxSelection) _context.GxSelection).FirstObject as IGxDataset).Dataset is IFeatureDataset)
                            {
                                IEnumDataset subsets = ((((IGxSelection) _context.GxSelection).FirstObject as IGxDataset).Dataset as IFeatureDataset).Subsets;
                                subsets.Reset();
                                for (IDataset dataset = subsets.Next(); dataset != null; dataset = subsets.Next())
                                {
                                    if (dataset.Type == esriDatasetType.esriDTFeatureClass)
                                    {
                                        IVersionedObject3 versionedObject2 = dataset as IVersionedObject3;
                                        if (versionedObject2.IsRegisteredAsVersioned)
                                        {
                                            result = true;
                                            return result;
                                        }
                                    }
                                }
                            }
                            result = false;
                            return result;
                        }
                    }
                    catch (Exception)
                    {
                        result = false;
                        return result;
                    }
                    result = true;
                }
                else
                {
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

            if (this.Enabled)
            {
                try
                {
                    IVersionedObject3 versionedObject = (((IGxSelection) _context.GxSelection).FirstObject as IGxDataset).Dataset as IVersionedObject3;
                    bool flag;
                    bool flag2;
                    versionedObject.GetVersionRegistrationInfo(out flag, out flag2);
                    versionedObject.RegisterAsVersioned(false);
                    if (flag)
                    {
                        versionedObject.UnRegisterAsVersioned3(false);
                    }
                }
                catch (Exception ex2)
                {
                    COMException ex = ex2 as COMException;
                    if (ex != null && ex.ErrorCode == -2147467259)
                    {
                        System.Windows.Forms.MessageBox.Show("表[" + (((IGxSelection) _context.GxSelection).FirstObject as IGxDataset).DatasetName + "]正在使用");
                    }
                    // CErrorLog.writeErrorLog(this, ex2, "");
                }
            }
        }
    }
}