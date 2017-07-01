using System;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Controls.Controls;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdRegisterAsVersion : YutaiCommand
    {
        public CmdRegisterAsVersion(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            //this.m_bitmap = Properties.Resources.icon_catalog_delete;
            this.m_caption = "注册为版本";
            this.m_category = "Catalog";
            this.m_message = "注册为版本";
            this.m_name = "Catalog_RegisterAsVersion";
            this._key = "Catalog_RegisterAsVersion";
            this.m_toolTip = "注册为版本";
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
                try
                {
                    if (_context.GxSelection == null)
                    {
                        result = false;
                        return result;
                    }
                    if (((IGxSelection) _context.GxSelection).FirstObject == null)
                    {
                        result = false;
                        return result;
                    }
                    if (((IGxSelection) _context.GxSelection).FirstObject is IGxDataset)
                    {
                        try
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
                                result = false;
                                return result;
                            }
                        }
                        catch
                        {
                            result = false;
                            return result;
                        }
                        result = true;
                        return result;
                    }
                }
                catch (Exception)
                {
                }
                result = false;
                return result;
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            frmRegisterAsVersion frmRegisterAsVersion = new frmRegisterAsVersion();
            if (frmRegisterAsVersion.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    IVersionedObject3 versionedObject =
                        (((IGxSelection) _context.GxSelection).FirstObject as IGxDataset).Dataset as IVersionedObject3;
                    versionedObject.RegisterAsVersioned3(frmRegisterAsVersion.EditToBase);
                }
                catch (Exception ex2)
                {
                    COMException ex = ex2 as COMException;
                    if (ex != null)
                    {
                        if (ex.ErrorCode == -2147467259)
                        {
                            System.Windows.Forms.MessageBox.Show("表[" +
                                                                 (((IGxSelection) _context.GxSelection).FirstObject as
                                                                     IGxDataset).DatasetName + "]正在使用");
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show(ex2.Message);
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show(ex2.Message);
                    }
                    // CErrorLog.writeErrorLog(this, ex2, "");
                }
            }
        }
    }
}