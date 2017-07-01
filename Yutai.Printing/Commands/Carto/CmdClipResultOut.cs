using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using System;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Carto.UI;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Printing.Forms;
using ICommandSubType = ESRI.ArcGIS.SystemUI.ICommandSubType;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdClipResultOut : YutaiCommand, ICommandSubType
    {
        private int _subType = 0;


        public override bool Enabled
        {
            get
            {
                bool result;
                if (this._context.FocusMap == null)
                {
                    result = false;
                }
                else if (this._context.FocusMap.SelectionCount >= 1)
                {
                    result = true;
                }
                else
                {
                    IGraphicsContainer graphicsContainer = this._context.FocusMap as IGraphicsContainer;
                    graphicsContainer.Reset();
                    result = (graphicsContainer.Next() != null ||
                              (this._context.Hook is IApplication &&
                               (this._context.Hook as IApplication).BufferGeometry != null));
                }
                return result;
            }
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "裁剪输出";
            this.m_category = "工具";
            this.m_message = "裁剪输出";
            this.m_toolTip = "裁剪输出";
            base.m_bitmap = Properties.Resources.icon_delete;
            base.m_name = "Printing_ClipOut";
            _key = "Printing_ClipOut";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public int SubType
        {
            get { return _subType; }
            set
            {
                switch (value)
                {
                    case 0:
                        m_name = "Printing_ClipOutToPrint";
                        _key = "Printing_ClipOutToPrint";
                        m_caption = "打印";
                        m_message = "打印输出";
                        m_toolTip = "打印输出";
                        m_bitmap = Properties.Resources.icon_clip_print;
                        break;
                    case 1:
                        m_name = "Printing_ClipOutToShapefile";
                        _key = "Printing_ClipOutToShapefile";
                        m_caption = "数据";
                        m_message = "数据输出";
                        m_toolTip = "数据输出";
                        m_bitmap = Properties.Resources.icon_clip_cut1;
                        break;
                }
                _subType = value;
            }
        }

        public CmdClipResultOut(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            try
            {
                IGeometry geometry = null;
                if (this._context.FocusMap.SelectionCount >= 1)
                {
                    IEnumFeature enumFeature = this._context.FocusMap.FeatureSelection as IEnumFeature;
                    enumFeature.Reset();
                    IFeature feature = enumFeature.Next();
                    ITopologicalOperator topologicalOperator = null;
                    while (feature != null)
                    {
                        if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
                        {
                            if (topologicalOperator == null)
                            {
                                topologicalOperator = (feature.ShapeCopy as ITopologicalOperator);
                            }
                            else
                            {
                                topologicalOperator = (topologicalOperator.Union(feature.Shape) as ITopologicalOperator);
                                topologicalOperator.Simplify();
                            }
                        }
                        feature = enumFeature.Next();
                    }
                    geometry = (topologicalOperator as IGeometry);
                }
                else
                {
                    ITopologicalOperator topologicalOperator = null;
                    IGraphicsContainer graphicsContainer = this._context.FocusMap as IGraphicsContainer;
                    graphicsContainer.Reset();
                    IElement element = graphicsContainer.Next();
                    if (element != null)
                    {
                        if (element is IPolygonElement)
                        {
                            if (topologicalOperator == null)
                            {
                                topologicalOperator = (element.Geometry as ITopologicalOperator);
                            }
                            else
                            {
                                topologicalOperator =
                                    (topologicalOperator.Union(element.Geometry) as ITopologicalOperator);
                                topologicalOperator.Simplify();
                            }
                        }
                        element = graphicsContainer.Next();
                    }
                    geometry = (topologicalOperator as IGeometry);
                }
                if (geometry == null && this._context.Hook is IApplication)
                {
                    geometry = (this._context.Hook as IApplication).BufferGeometry;
                }
                if (geometry != null && !geometry.IsEmpty)
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
                        IWorkspaceName iworkspaceName_ = workspaceFactory.Create(System.IO.Path.GetDirectoryName(path),
                            System.IO.Path.GetFileNameWithoutExtension(path), null, 0);
                        IMap imap_ = new Map();
                        Clip.ClipMapByRegion(iworkspaceName_, this._context.FocusMap, geometry, imap_);
                        new FormPrinterSetup();
                        CMapPrinter cMapPrinter;
                        if (this._context.Hook is IApplication)
                        {
                            cMapPrinter = new CMapPrinter(imap_, ApplicationBase.StyleGallery);
                        }
                        else
                        {
                            cMapPrinter = new CMapPrinter(imap_);
                        }
                        cMapPrinter.showPrintUI("打印地图");
                    }
                    else
                    {
                        new frmDir
                        {
                            FocusMap = this._context.FocusMap,
                            ClipGeometry = geometry
                        }.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        public void SetSubType(int int_1)
        {
            this._subType = int_1;
        }

        public int GetCount()
        {
            return 2;
        }
    }
}