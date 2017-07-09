using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using System;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Carto.UI;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Printing.Forms;
using ICommandSubType = ESRI.ArcGIS.SystemUI.ICommandSubType;

namespace Yutai.Plugins.Printing.Commands
{
    public class ToolRealClipOut : YutaiTool, ICommandSubType
    {
        private int _subType = 0;

        private bool bool_0 = false;

        private IPoint ipoint_0 = null;

        private IDisplayFeedback idisplayFeedback_0 = null;


        public int SubType
        {
            get { return _subType; }
            set
            {
                _subType = value;
                SetSubType(value);
            }
        }

        public override bool Enabled
        {
            get { return this._context.FocusMap != null && this._context.FocusMap.LayerCount != 0; }
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "裁剪";
            this.m_category = "工具";
            this.m_message = "裁剪输出";
            this.m_toolTip = "裁剪输出";
            base.m_bitmap = Properties.Resources.icon_clip_cut3;
            base.m_name = "Printing_ClipOutTool";
            _key = "Printing_ClipOutTool";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public ToolRealClipOut(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            _context.SetCurrentTool(this);
        }

        public override void OnMouseDown(int int_1, int int_2, int int_3, int int_4)
        {
            if (this._context.ActiveView is IPageLayout)
            {
                IPoint location = this._context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_3, int_4);
                IMap map = this._context.ActiveView.HitTestMap(location);
                if (map == null)
                {
                    return;
                }
                this._context.ActiveView.FocusMap = map;
                this._context.ActiveView.Refresh();
            }
            IActiveView activeView = this._context.FocusMap as IActiveView;
            this.ipoint_0 = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_3, int_4);
            this.bool_0 = true;
            if (this.idisplayFeedback_0 == null)
            {
                this.idisplayFeedback_0 = new NewPolygonFeedback();
                this.idisplayFeedback_0.Display = activeView.ScreenDisplay;
                (this.idisplayFeedback_0 as INewPolygonFeedback).Start(this.ipoint_0);
            }
            else
            {
                (this.idisplayFeedback_0 as INewPolygonFeedback).AddPoint(this.ipoint_0);
            }
        }

        public override void OnMouseMove(int int_1, int int_2, int int_3, int int_4)
        {
            if (this.bool_0)
            {
                IActiveView activeView = this._context.FocusMap as IActiveView;
                this.idisplayFeedback_0.MoveTo(activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_3, int_4));
            }
        }

        public override void OnDblClick()
        {
            if (!this.bool_0)
            {
                this.idisplayFeedback_0 = null;
            }
            else
            {
                IGeometry geometry = (this.idisplayFeedback_0 as INewPolygonFeedback).Stop();
                this.bool_0 = false;
                this.idisplayFeedback_0 = null;
                if (!geometry.IsEmpty)
                {
                    (geometry as IPolygon).SimplifyPreserveFromTo();
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                    try
                    {
                        geometry.SpatialReference = this._context.FocusMap.SpatialReference;
                        int num = this._subType;
                        if (num != 0)
                        {
                            string str = System.IO.Path.GetTempPath() + "TempPersonGDB";
                            int num2 = 1;
                            string path = str + ".mdb";
                            while (File.Exists(path))
                            {
                                try
                                {
                                    File.Delete(path);
                                    break;
                                }
                                catch
                                {
                                }
                                num2++;
                                path = str + " (" + num2.ToString() + ").mdb";
                            }
                            IWorkspaceFactory workspaceFactory = new AccessWorkspaceFactory();
                            IWorkspaceName workspaceName = workspaceFactory.Create(
                                System.IO.Path.GetDirectoryName(path), System.IO.Path.GetFileNameWithoutExtension(path),
                                null, 0);
                            IMap imap_ = new Map();
                            Clip.ExtractSpecifyHRegFeatures(workspaceName, this._context.FocusMap, geometry, imap_);
                            new FormPrinterSetup();
                            CMapPrinter cMapPrinter;
                           
                            cMapPrinter = new CMapPrinter(imap_);
                            
                            cMapPrinter.showPrintUI("打印地图");
                        }
                        else
                        {
                            frmOpenFile frmOpenFile = new frmOpenFile();
                            frmOpenFile.Text = "保存位置";
                            frmOpenFile.RemoveAllFilters();
                            frmOpenFile.AddFilter(new MyGxFilterWorkspaces(), true);
                            if (frmOpenFile.DoModalSave() == System.Windows.Forms.DialogResult.OK)
                            {
                                IArray items = frmOpenFile.Items;
                                if (items.Count == 0)
                                {
                                    return;
                                }
                                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                                try
                                {
                                    IWorkspaceName workspaceName = null;
                                    IGxObject gxObject = items.get_Element(0) as IGxObject;
                                    if (gxObject is IGxDatabase)
                                    {
                                        workspaceName = (gxObject.InternalObjectName as IWorkspaceName);
                                    }
                                    else if (gxObject is IGxFolder)
                                    {
                                        workspaceName = new WorkspaceName() as IWorkspaceName;
                                        workspaceName.WorkspaceFactoryProgID =
                                            "esriDataSourcesFile.ShapefileWorkspaceFactory";
                                        workspaceName.PathName = (gxObject.InternalObjectName as IFileName).Path;
                                    }
                                    if (workspaceName != null)
                                    {
                                        Clip.ExtractSpecifyHRegFeatures(workspaceName, this._context.FocusMap, geometry);
                                    }
                                }
                                catch
                                {
                                }
                                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.ToString());
                    }
                    if (this._context.Hook is IApplication)
                    {
                        (this._context.Hook as IApplication).CurrentTool = null;
                    }
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                }
            }
        }

        public void SetSubType(int int_1)
        {
            this._subType = int_1;
            switch (_subType)
            {
                case 0:
                    m_name = "Printing_RealClipOutToPrint";
                    _key = "Printing_RealClipOutToPrint";
                    m_caption = "裁剪并打印";
                    m_message = "打印裁剪";
                    m_toolTip = "打印裁剪";
                    m_bitmap = Properties.Resources.icon_clip_print;
                    _itemType = RibbonItemType.Tool;
                    break;
                case 1:
                    m_name = "Printing_RealClipOutToShapefile";
                    _key = "Printing_RealClipOutToShapefile";
                    m_caption = "数据裁剪";
                    m_message = "数据裁剪";
                    m_toolTip = "数据裁剪";
                    m_bitmap = Properties.Resources.icon_clip_cut3;
                    _itemType = RibbonItemType.Tool;
                    break;
            }
        }

        public int GetCount()
        {
            return 2;
        }
    }
}