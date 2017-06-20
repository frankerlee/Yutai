using System;
using System.Runtime.InteropServices;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Editor;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Cursor = ESRI.ArcGIS.Geodatabase.Cursor;

namespace Yutai.Plugins.Catalog.Commands
{
    class CmdDeleteItem : YutaiCommand
    {
        public CmdDeleteItem(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_catalog_delete;
            this.m_caption = "删除";
            this.m_category = "Catalog";
            this.m_message = "删除";
            this.m_name = "Catalog_Delete";
            this._key = "Catalog_Delete";
            this.m_toolTip = "删除";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
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
                else if (((IGxSelection) _context.GxSelection).Count == 0)
                {
                    result = false;
                }
                else
                {
                    IEnumGxObject selectedObjects = ((IGxSelection) _context.GxSelection).SelectedObjects;
                    selectedObjects.Reset();
                    for (IGxObject gxObject = selectedObjects.Next();
                        gxObject != null;
                        gxObject = selectedObjects.Next())
                    {
                        IGxObjectEdit gxObjectEdit = gxObject as IGxObjectEdit;
                        if (gxObjectEdit == null)
                        {
                            result = false;
                            return result;
                        }
                        if (!gxObjectEdit.CanDelete())
                        {
                            result = false;
                            return result;
                        }
                    }
                    result = true;
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
            if (
                System.Windows.Forms.MessageBox.Show("是否删除选中对象!", "删除", System.Windows.Forms.MessageBoxButtons.YesNo,
                    System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                IEnumGxObject enumGxObject = ((IGxSelection) _context.GxSelection).SelectedObjects;
                enumGxObject.Reset();
                IGxObject gxObject = enumGxObject.Next();
                IGxObjectArray gxObjectArray = new GxObjectArray();
                while (gxObject != null)
                {
                    gxObjectArray.Insert(-1, gxObject);
                    gxObject = enumGxObject.Next();
                }
                (enumGxObject as IGxObjectArray).Empty();
                enumGxObject = (gxObjectArray as IEnumGxObject);
                enumGxObject.Reset();
                gxObject = enumGxObject.Next();
                bool flag = false;
                IGxObject gxObject2 = null;
                while (gxObject != null)
                {
                    if (gxObject2 == null)
                    {
                        gxObject2 = gxObject.Parent;
                    }
                    IGxObjectEdit gxObjectEdit = gxObject as IGxObjectEdit;
                    try
                    {
                        if(gxObjectEdit.CanDelete())
                        gxObjectEdit.Delete();
                        else
                        {
                            flag = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        flag = true;
                    }
                    gxObject = enumGxObject.Next();
                }
                if (gxObject2 != null)
                {
                    try
                    {
                        gxObject2.Refresh();
                    }
                    catch (Exception ex)
                    {
                       
                    }
                   
                }
                if (flag)
                {
                    System.Windows.Forms.MessageBox.Show("一个或多个对象不能删除!", "删除");
                }
            }
        }
    }


    //class CmdValidateTopology : YutaiCommand
    //{
    //    public CmdValidateTopology(IAppContext context)
    //    {
    //        OnCreate(context);
    //    }

    //    public override void OnCreate(object hook)
    //    {
    //        //this.m_bitmap = Properties.Resources.icon_catalog_delete;
    //        this.m_caption = "完整拓扑校验";
    //        this.m_category = "Catalog";
    //        this.m_message = "完整拓扑校验";
    //        this.m_name = "Catalog_ValidateTopology";
    //        this._key = "Catalog_ValidateTopology";
    //        this.m_toolTip = "完整拓扑校验";
    //        _context = hook as IAppContext;
    //        DisplayStyleYT = DisplayStyleYT.Text;
    //        base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
    //        base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
    //        _itemType = RibbonItemType.Button;
    //    }

    //    public override bool Enabled
    //    {
    //        get
    //        {
    //            bool flag;
    //            if (this.m_HookHelper.FocusMap == null)
    //            {
    //                flag = false;
    //            }
    //            else if (this.m_HookHelper.FocusMap.LayerCount == 0)
    //            {
    //                SelectTopologyCommand.m_TopologyGraph = null;
    //                flag = false;
    //            }
    //            else if (!(this.m_HookHelper.Hook is IApplication) || (this.m_HookHelper.Hook as IApplication).CanEdited)
    //            {
    //                if (Editor.EditWorkspace == null)
    //                {
    //                    SelectTopologyCommand.m_TopologyGraph = null;
    //                }
    //                else
    //                {
    //                    if (SelectTopologyCommand.m_TopologyGraph == null)
    //                    {
    //                        goto Label1;
    //                    }
    //                    flag = true;
    //                    return flag;
    //                }
    //                Label1:
    //                flag = false;
    //            }
    //            else
    //            {
    //                flag = false;
    //            }
    //            return flag;
    //        }
    //    }
    //    public override void OnClick(object sender, EventArgs args)
    //    {
    //        OnClick();
    //    }

    //    public override void OnClick()
    //    {
    //        Editor.EditWorkspace.StartEditOperation();
    //        try
    //        {
    //            ITopology mTopology = SelectTopologyCommand.m_Topology;
    //            ISegmentCollection polygonClass = new PolygonClass();
    //            polygonClass.SetRectangle((mTopology as IGeoDataset).Extent);
    //            IPolygon dirtyArea = mTopology[polygonClass as IPolygon];
    //            mTopology.ValidateTopology(dirtyArea.Envelope);
    //            this.m_HookHelper.ActiveView.Refresh();
    //        }
    //        catch (COMException cOMException)
    //        {
    //            CErrorLog.writeErrorLog(this, cOMException, "");
    //        }
    //        catch (Exception exception)
    //        {
    //            CErrorLog.writeErrorLog(this, exception, "");
    //        }
    //        Editor.EditWorkspace.StopEditOperation();
    //    }
    //}
}