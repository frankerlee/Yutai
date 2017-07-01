using System;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdEnableArchiving : YutaiCommand
    {
        private IVersionedObject3 iversionedObject3_0 = null;

        public CmdEnableArchiving(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            //this.m_bitmap = Properties.Resources.icon_catalog_delete;
            this.m_caption = "启用存档";
            this.m_category = "Catalog";
            this.m_message = "启用存档";
            this.m_name = "Catalog_EnableArchiving";
            this._key = "Catalog_EnableArchiving";
            this.m_toolTip = "启用存档";
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
                else
                {
                    IGxDataset gxDataset = ((IGxSelection) _context.GxSelection).FirstObject as IGxDataset;
                    if (gxDataset != null)
                    {
                        try
                        {
                            IDataset dataset = gxDataset.Dataset;
                            if (dataset == null)
                            {
                                result = false;
                                return result;
                            }
                            if (dataset.Workspace.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace &&
                                dataset is IVersionedObject3)
                            {
                                this.iversionedObject3_0 = (IVersionedObject3) dataset;
                                bool flag;
                                bool flag2;
                                this.iversionedObject3_0.GetVersionRegistrationInfo(out flag, out flag2);
                                if (flag && !flag2)
                                {
                                    IArchivableObject archivableObject = (IArchivableObject) this.iversionedObject3_0;
                                    if (!archivableObject.IsArchiving)
                                    {
                                        result = true;
                                        return result;
                                    }
                                }
                            }
                        }
                        catch
                        {
                            result = false;
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
            try
            {
                IArchivableObject archivableObject = (IArchivableObject) this.iversionedObject3_0;
                archivableObject.EnableArchiving(null, null, true);
            }
            catch (Exception ex)
            {
                MessageService.Current.Warn(ex.Message);
            }
        }

        public void EnableArchivingOnClass(ref IFeatureClass ifeatureClass_0)
        {
            bool flag = false;
            bool flag2 = false;
            IVersionedObject3 versionedObject = (IVersionedObject3) ifeatureClass_0;
            versionedObject.GetVersionRegistrationInfo(out flag, out flag2);
            if (!flag)
            {
                versionedObject.RegisterAsVersioned3(false);
            }
            else if (flag2)
            {
                versionedObject.UnRegisterAsVersioned3(false);
                versionedObject.RegisterAsVersioned3(false);
            }
            IArchivableObject archivableObject = (IArchivableObject) versionedObject;
            if (archivableObject.IsArchiving)
            {
                archivableObject.DisableArchiving(true, true);
                archivableObject.EnableArchiving(null, null, true);
            }
            else
            {
                archivableObject.EnableArchiving(null, null, true);
            }
        }
    }
}