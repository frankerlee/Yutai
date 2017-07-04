using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CartoTemplateApp;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Carto.MapCartoTemplateLib;
using Yutai.ArcGIS.Carto.UI;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Carto;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Controls.Controls;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;
using Yutai.UI.Controls;
using frmElementProperty = Yutai.ArcGIS.Carto.UI.frmElementProperty;

namespace Yutai.Plugins.Printing.Views
{
    public partial class MapTemplateView : DockPanelControlBase, IMapTemplateView
    {
        private IPageLayoutControl2 _layoutControl;
        private TreeNode m_pLastSelect;
        private bool m_CanSelectChange;
        private bool m_TreeviewSelectChange;
        private IAppContext _context;
        private IActiveViewEvents_Event m_iPageLayout;
        private IStyleGallery m_pSG;
        private MapTemplateGallery m_MapTemplateGallery = new MapTemplateGallery();
        private bool m_CanDo;
        private MapTemplate m_pLastMapTemplate;
        private PrintingPlugin _plugin;

        public MapTemplateView()
        {
            InitializeComponent();
        }

        public void Initialize(IAppContext context, PrintingPlugin plugin)
        {
            try
            {
                _context = context;
                _plugin = plugin;
                _layoutControl = _context.MainView.PageLayoutControl;
            }
            catch (Exception ex)
            {
                MessageService.Current.Warn(ex.Message);
            }
        }
        public void SetBuddyControl()
        {
            _layoutControl = _context.MainView.PageLayoutControl;
           
        }


        #region 菜单生成
        private void BulidMapTemplateClassNodeMeun()
        {
            this.contextMenuStrip1.Items.Add("新建", null, new EventHandler(this.MenuItem_NewMapTemplate));
            this.contextMenuStrip1.Items.Add("属性", null, new EventHandler(this.MenuItem_MapTemplateClassProperty));
            this.contextMenuStrip1.Items.Add("导入模板", null, new EventHandler(this.MenuItem_ExportMapTemplateProperty));
            this.contextMenuStrip1.Items.Add(new ToolStripSeparator());
            this.contextMenuStrip1.Items.Add("删除", null, new EventHandler(this.MenuItem_DeleteMapTemplateClass));
        }

        private void BulidMapTemplateElementNodeMeun()
        {
            this.contextMenuStrip1.Items.Add("属性", null, new EventHandler(this.MenuItem_MapTemplateElementProperty));
            this.contextMenuStrip1.Items.Add(new ToolStripSeparator());
            this.contextMenuStrip1.Items.Add("删除", null, new EventHandler(this.MenuItem_DeleteMapTemplateElement));
        }

        private void BulidMapTemplateElementsNodeMeun()
        {
            this.contextMenuStrip1.Items.Add("新建", null, new EventHandler(this.MenuItem_NewMapTemplateElement));
            this.contextMenuStrip1.Items.Add(new ToolStripSeparator());
            this.contextMenuStrip1.Items.Add("刷新", null, new EventHandler(this.MenuItem_RefreshTreeView));
        }

        private void BulidMapTemplateNodeMeun()
        {
            this.contextMenuStrip1.Items.Add("属性", null, new EventHandler(this.MenuItem_MapTemplateProperty));
            this.contextMenuStrip1.Items.Add("拷贝", null, new EventHandler(this.MenuItem_CopyMapTemplate));
            this.contextMenuStrip1.Items.Add("保存到文件", null, new EventHandler(this.MenuItem_SaveMapTemplateToFile));
            this.contextMenuStrip1.Items.Add(new ToolStripSeparator());
            this.contextMenuStrip1.Items.Add("删除", null, new EventHandler(this.MenuItem_DeleteMapTemplate));
        }

        private void BulidMapTemplateParamNodeMeun()
        {
            this.contextMenuStrip1.Items.Add("属性", null, new EventHandler(this.MenuItem_MapTemplateParamProperty));
            this.contextMenuStrip1.Items.Add(new ToolStripSeparator());
            this.contextMenuStrip1.Items.Add("删除", null, new EventHandler(this.MenuItem_DeleteMapTemplateParam));
        }

        private void BulidMapTemplateParamsNodeMeun()
        {
            this.contextMenuStrip1.Items.Add("新建", null, new EventHandler(this.MenuItem_NewMapTemplateParam));
            this.contextMenuStrip1.Items.Add("删除", null, new EventHandler(this.MenuItem_DeleteAllMapTemplateParam));
        }

        private void BulidMenu(TreeNode pParentNode)
        {
            this.contextMenuStrip1.Items.Clear();
            if (pParentNode.Parent == null)
            {
                this.BulidRootNodeMeun();
            }
            else if (pParentNode.Tag is MapTemplateClass)
            {
                this.BulidMapTemplateClassNodeMeun();
            }
            else if (pParentNode.Tag is MapTemplate)
            {
                this.BulidMapTemplateNodeMeun();
            }
            else if (pParentNode.Tag is MapTemplateElement)
            {
                this.BulidMapTemplateElementNodeMeun();
            }
            else if (pParentNode.Tag is MapTemplateParam)
            {
                this.BulidMapTemplateParamNodeMeun();
            }
            else if (pParentNode.Tag is string)
            {
                if (pParentNode.Text == "模板元素")
                {
                    this.BulidMapTemplateElementsNodeMeun();
                }
                else if (pParentNode.Text == "模板参数")
                {
                    this.BulidMapTemplateParamsNodeMeun();
                }
            }
        }

        private void BulidRootNodeMeun()
        {
            this.contextMenuStrip1.Items.Add("新建", null, new EventHandler(this.MenuItem_NewMapTemplateClass));
            this.contextMenuStrip1.Items.Add(new ToolStripSeparator());
            this.contextMenuStrip1.Items.Add("刷新", null, new EventHandler(this.MenuItem_RefreshTreeView));
        }





        #endregion


        #region Element操作
        private void DeleteAllElements(IActiveView pAV)
        {
            IGraphicsContainer graphicsContainer = pAV.GraphicsContainer;
            IMapFrame mapFrame = graphicsContainer.FindFrame(pAV.FocusMap) as IMapFrame;
            (mapFrame as IMapGrids).ClearMapGrids();
            graphicsContainer.Reset();
            IElement element = graphicsContainer.Next();
            List<IElement> elements = new List<IElement>();
            try
            {
                graphicsContainer.DeleteAllElements();
                graphicsContainer.Reset();
                element = graphicsContainer.Next();
                if (element != null)
                {
                    graphicsContainer.DeleteElement(element);
                }
                graphicsContainer.AddElement(mapFrame as IElement, -1);
                pAV.FocusMap = mapFrame.Map;
            }
            catch (Exception exception)
            {
            }
        }

        private void EditElementProperty()
        {
            IPropertySheet _frmElementProperty;
            IPropertyPage propertyPage;
            IEnvelope envelope;
            try
            {
                MapTemplateElement tag = this.treeView1.SelectedNode.Tag as MapTemplateElement;
                IElement element = tag.Element;
                object obj = null;
                PropertySheet propertySheet = new PropertySheet()
                {
                    Text = "元素属性"
                };
                if (tag.MapTemplateElementType != MapTemplateElementType.GraphicElement)
                {
                    propertySheet.AddPage(new ElementPosition());
                    if (element is ITextElement)
                    {
                        propertySheet.AddPage(new ElementSymbolSetPage());
                        propertySheet.AddPage(new TextElementValueSetPage());
                    }
                    else if (tag.MapTemplateElementType == MapTemplateElementType.CustomLegendElement)
                    {
                        propertySheet.AddPage(new CustomLegendConfigPage());
                    }
                    else if (tag.MapTemplateElementType == MapTemplateElementType.MyGruopElement)
                    {
                        propertySheet.AddPage(new GroupElementExPropertyPage());
                    }
                    else if (tag.MapTemplateElementType == MapTemplateElementType.TableElement)
                    {
                        propertySheet.AddPage(new TableGeneralPage()
                        {
                            IsEdit = true
                        });
                        propertySheet.AddPage(new TableCellSetPage());
                    }
                    else if (tag.MapTemplateElementType == MapTemplateElementType.ScaleBarElement)
                    {
                        propertySheet.AddPage(new ArcGIS.Carto.MapCartoTemplateLib.ScaleBarFormatPropertyPage());
                        propertySheet.AddPage(new ArcGIS.Carto.MapCartoTemplateLib.ScaleAndUnitsPropertyPage());
                        propertySheet.AddPage(new ArcGIS.Carto.MapCartoTemplateLib.NumberAndLabelPropertyPage());
                        obj = element;
                    }
                    else if (tag.MapTemplateElementType == MapTemplateElementType.ScaleTextElement)
                    {
                        propertySheet.AddPage(new ArcGIS.Carto.MapCartoTemplateLib.ScaleTextTextPropertyPage());
                        propertySheet.AddPage(new ArcGIS.Carto.MapCartoTemplateLib.ScaleTextFormatPropertyPage());
                        obj = element;
                    }
                    else if (tag.MapTemplateElementType == MapTemplateElementType.NorthElement)
                    {
                        propertySheet.AddPage(new ArcGIS.Carto.MapCartoTemplateLib.NorthArrowPropertyPage());
                        obj = element;
                    }
                    else if (element is IGroupElement)
                    {
                    }
                    if (propertySheet.EditProperties(tag))
                    {
                        tag.Update(this._layoutControl.PageLayout);
                    }
                }
                else
                {
                    IElement element1 = element;
                    if (element1 is IFillShapeElement)
                    {
                        _frmElementProperty = new frmElementProperty()
                        {
                            Title = "属性"
                        };
                        propertyPage = null;
                        _frmElementProperty.AddPage(new FillSymbolPropertyPage());
                        _frmElementProperty.AddPage(new ElementGeometryInfoPropertyPage());
                        envelope = element1.Geometry.Envelope;
                        if (_frmElementProperty.EditProperties(element1))
                        {
                            tag.Update(this._layoutControl.PageLayout);
                            this._layoutControl.ActiveView.Refresh();
                        }
                    }
                    else if (element1 is ILineElement)
                    {
                        _frmElementProperty = new frmElementProperty()
                        {
                            Title = "属性"
                        };
                        propertyPage = null;
                        _frmElementProperty.AddPage(new LineSymbolPropertyPage());
                        _frmElementProperty.AddPage(new ElementSizeAndPositionCtrl());
                        envelope = element1.Geometry.Envelope;
                        if (_frmElementProperty.EditProperties(element1))
                        {
                            tag.Update(this._layoutControl.PageLayout);
                            this._layoutControl.ActiveView.Refresh();
                        }
                    }
                    else if (element1 is IMarkerElement)
                    {
                        _frmElementProperty = new frmElementProperty()
                        {
                            Title = "属性"
                        };
                        propertyPage = null;
                        _frmElementProperty.AddPage(new MarkerElementPropertyPage());
                        envelope = element1.Geometry.Envelope;
                        if (_frmElementProperty.EditProperties(element1))
                        {
                            tag.Update(this._layoutControl.PageLayout);
                            this._layoutControl.ActiveView.Refresh();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }
        #endregion

        #region Element事件
        private void ElementChangeEvent_OnDeleteElement(IElement pElement)
        {
            for (int i = 0; i < this.m_pLastSelect.Nodes[1].Nodes.Count; i++)
            {
                TreeNode item = this.m_pLastSelect.Nodes[1].Nodes[i];
                if (item.Tag is MapTemplateElement)
                {
                    if (pElement == (item.Tag as MapTemplateElement).Element)
                    {
                        (item.Tag as MapTemplateElement).Delete();
                        ((this.treeView1.SelectedNode.Parent != null ? this.treeView1.SelectedNode.Nodes : this.treeView1.Nodes)).Remove(item);
                    }
                }
            }
        }
        private void ElementChangeEvent_OnEditElementProperty(IElement pElement)
        {
            this.EditElementProperty();
        }

        private void ElementChangeEvent_OnElementPositionChange(IElement pElement)
        {
            try
            {
                for (int i = 0; i < this.m_pLastSelect.Nodes[1].Nodes.Count; i++)
                {
                    TreeNode item = this.m_pLastSelect.Nodes[1].Nodes[i];
                    if (item.Tag is MapTemplateElement)
                    {
                        if (pElement == (item.Tag as MapTemplateElement).Element)
                        {
                            (item.Tag as MapTemplateElement).ChangePosition(this._layoutControl.PageLayout);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }

        private void ElementChangeEvent_OnElementSelectChange()
        {
            if (this.m_CanSelectChange)
            {
                IGraphicsContainerSelect graphicsContainer = this._layoutControl.ActiveView.GraphicsContainer as IGraphicsContainerSelect;
                if (graphicsContainer.ElementSelectionCount != 0)
                {
                    IElement element = graphicsContainer.SelectedElement(0);
                    if (this.m_pLastSelect.Nodes.Count == 1)
                    {
                        this.m_pLastSelect.Nodes.Clear();
                        this.InitMapTemplate(this.m_pLastSelect);
                    }
                    this.treeView1.SelectedNode = null;
                    for (int i = 0; i < this.m_pLastSelect.Nodes[1].Nodes.Count; i++)
                    {
                        TreeNode item = this.m_pLastSelect.Nodes[1].Nodes[i];
                        if (item.Tag is MapTemplateElement)
                        {
                            if (element == (item.Tag as MapTemplateElement).Element)
                            {
                                this.m_TreeviewSelectChange = false;
                                this.treeView1.SelectedNode = item;
                                this.BulidMenu(item);
                                this.m_TreeviewSelectChange = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    this.treeView1.SelectedNode = null;
                    this.contextMenuStrip1.Items.Clear();
                }
            }
        }
        #endregion

        #region Template操作
      

        private void InitMapTemplate(TreeNode pParentNode)
        {
            MapTemplate tag = pParentNode.Tag as MapTemplate;
            TreeNode treeNode = new TreeNode("模板参数")
            {
                Tag = "模板参数"
            };
            pParentNode.Nodes.Add(treeNode);
            TreeNode treeNode1 = new TreeNode("模板元素")
            {
                Tag = "模板元素"
            };
            pParentNode.Nodes.Add(treeNode1);
            foreach (MapTemplateParam mapTemplateParam in tag.MapTemplateParam)
            {
                TreeNodeCollection nodes = treeNode.Nodes;
                TreeNode treeNode2 = new TreeNode(mapTemplateParam.Name)
                {
                    Tag = mapTemplateParam
                };
                nodes.Add(treeNode2);
            }
            foreach (MapTemplateElement mapTemplateElement in tag.MapTemplateElement)
            {
                TreeNodeCollection treeNodeCollections = treeNode1.Nodes;
                TreeNode treeNode3 = new TreeNode(mapTemplateElement.Name)
                {
                    Tag = mapTemplateElement
                };
                treeNodeCollections.Add(treeNode3);
            }
        }

        private void InitStyle()
        {
            if (AppConfigInfo.StyleFileType != 0)
            {
                this.m_pSG = new ServerStyleGallery();
                for (int i = (this.m_pSG as IStyleGalleryStorage).FileCount - 1; i >= 0; i--)
                {
                    string file = (this.m_pSG as IStyleGalleryStorage).File[i];
                    (this.m_pSG as IStyleGalleryStorage).RemoveFile(file);
                }
            }
            else
            {
                this.m_pSG = new MyStyleGallery();
            }
            string str = string.Concat(Application.StartupPath, "\\Styles\\");
            if (Directory.Exists(str))
            {
                string str1 = "*.serverstyle";
                if (AppConfigInfo.StyleFileType == 0)
                {
                    str1 = "*.style";
                }
                string[] files = Directory.GetFiles(str, str1, SearchOption.AllDirectories);
                for (int j = 0; j < (int)files.Length; j++)
                {
                    string str2 = files[j];
                    (this.m_pSG as IStyleGalleryStorage).AddFile(str2);
                }
            }
            _context.StyleGallery = this.m_pSG;
        }

        private string BuildConnectionString(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            string ext = fileInfo.Extension.Substring(1);

            return string.Format("dbclient={0};gdbname={1}", ext, fileName);
        }
        private void InitTree(string fileName)
        {
            this.treeView1.Nodes.Clear();
            TreeNode treeNode = new TreeNode("地图模板");
            if (!string.IsNullOrEmpty(fileName))
            {
                if(!fileName.Contains("dbclient="))
                this.m_MapTemplateGallery.SetWorkspace(BuildConnectionString(fileName));
                else
                    this.m_MapTemplateGallery.SetWorkspace(fileName);
                foreach (MapTemplateClass mapTemplateClass in this.m_MapTemplateGallery.MapTemplateClass)
                {
                    TreeNode treeNode1 = new TreeNode(mapTemplateClass.Name)
                    {
                        Tag = mapTemplateClass
                    };
                    treeNode1.Nodes.Add(new TreeNode(""));
                    treeNode.Nodes.Add(treeNode1);
                }
            }
            this.treeView1.Nodes.Add(treeNode);
        }

        private void LoadMapTemplate(TreeNode pParentNode)
        {
            MapTemplateClass tag = pParentNode.Tag as MapTemplateClass;
            tag.Load();
            foreach (MapTemplate mapTemplate in tag.MapTemplate)
            {
                TreeNode treeNode = new TreeNode(mapTemplate.Name)
                {
                    Tag = mapTemplate
                };
                treeNode.Nodes.Add(new TreeNode(""));
                pParentNode.Nodes.Add(treeNode);
            }
        }
        #endregion


        #region PageLayout事件
        private void m_iPageLayout_ItemAdded(object Item)
        {
            TreeNode treeNode;
            if (this.m_CanDo)
            {
                MapTemplate tag = null;
                TreeNode item = null;
                if (this.treeView1.SelectedNode != null)
                {
                    if (this.treeView1.SelectedNode.Tag is MapTemplate)
                    {
                        tag = this.treeView1.SelectedNode.Tag as MapTemplate;
                        if (this.treeView1.SelectedNode.Nodes.Count == 1)
                        {
                            if (this.treeView1.SelectedNode.Nodes[0].Tag == null)
                            {
                                this.treeView1.SelectedNode.Nodes.Clear();
                                this.InitMapTemplate(this.treeView1.SelectedNode);
                            }
                        }
                        item = this.treeView1.SelectedNode.Nodes[1];
                    }
                    else if (!(this.treeView1.SelectedNode.Tag is MapTemplateElement ? false : !(this.treeView1.SelectedNode.Tag is MapTemplateParam)))
                    {
                        tag = this.treeView1.SelectedNode.Parent.Parent.Tag as MapTemplate;
                        item = this.treeView1.SelectedNode.Parent.Parent.Nodes[1];
                    }
                    else if (this.treeView1.SelectedNode.Tag is string)
                    {
                        if ((this.treeView1.SelectedNode.Text == "模板元素" ? true : this.treeView1.SelectedNode.Text == "模板参数"))
                        {
                            tag = this.treeView1.SelectedNode.Parent.Tag as MapTemplate;
                            item = this.treeView1.SelectedNode.Parent.Nodes[1];
                        }
                    }
                    if (item != null)
                    {
                        if (Item is IElement)
                        {
                            bool flag = false;
                            string str = "";
                            if (Item is ILineElement)
                            {
                                str = "线元素";
                                flag = true;
                            }
                            else if (Item is IPolygonElement)
                            {
                                str = "面元素";
                                flag = true;
                            }
                            else if (Item is IRectangleElement)
                            {
                                str = "矩形元素";
                                flag = true;
                            }
                            else if (Item is IEllipseElement)
                            {
                                str = "椭圆元素";
                                flag = true;
                            }
                            else if (Item is ITextElement)
                            {
                                str = "文本元素";
                                flag = true;
                            }
                            else if (Item is ICircleElement)
                            {
                                str = "圆元素";
                                flag = true;
                            }
                            else if (Item is IMarkerElement)
                            {
                                str = "点元素";
                                flag = true;
                            }
                            if (flag)
                            {
                                (Item as IElementProperties2).Name = str;
                                if (!(Item is ITextElement))
                                {
                                    MapTemplateGraphicsElement mapTemplateGraphicsElement = new MapTemplateGraphicsElement(tag)
                                    {
                                        Element = Item as IElement
                                    };
                                    mapTemplateGraphicsElement.Save();
                                    treeNode = new TreeNode(mapTemplateGraphicsElement.Name)
                                    {
                                        Tag = mapTemplateGraphicsElement
                                    };
                                    item.Nodes.Add(treeNode);
                                    tag.AddMapTemplateElement(mapTemplateGraphicsElement);
                                    this.treeView1.SelectedNode = treeNode;
                                }
                                else
                                {
                                    MapTemplateTextElement mapTemplateTextElement = new MapTemplateTextElement(tag)
                                    {
                                        Element = Item as IElement
                                    };
                                    mapTemplateTextElement.Element = Item as IElement;
                                    mapTemplateTextElement.Text = (Item as ITextElement).Text;
                                    mapTemplateTextElement.Style = (Item as ITextElement).Symbol;
                                    mapTemplateTextElement.Save();
                                    mapTemplateTextElement.ChangePosition(this._layoutControl.PageLayout);
                                    treeNode = new TreeNode(mapTemplateTextElement.Name)
                                    {
                                        Tag = mapTemplateTextElement
                                    };
                                    item.Nodes.Add(treeNode);
                                    tag.AddMapTemplateElement(mapTemplateTextElement);
                                    this.treeView1.SelectedNode = treeNode;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void m_iPageLayout_ItemDeleted(object Item)
        {
            MapTemplateElement tag;
            if (this.m_CanDo)
            {
                if (this.treeView1.SelectedNode == null)
                {
                    if (this.m_pLastSelect != null)
                    {
                    }
                }
                else if (this.treeView1.SelectedNode.Tag is MapTemplateElement)
                {
                    if ((this.treeView1.SelectedNode.Tag as MapTemplateElement).Element == Item)
                    {
                        tag = this.treeView1.SelectedNode.Tag as MapTemplateElement;
                        tag.Delete();
                        (this.treeView1.SelectedNode.Parent.Parent.Tag as MapTemplate).RemoveMapTemplateElement(tag);
                        this.treeView1.SelectedNode.Remove();
                    }
                }
                else if (this.treeView1.SelectedNode.Tag is string)
                {
                    if (this.treeView1.SelectedNode.Tag.ToString() == "模板元素")
                    {
                        foreach (TreeNode node in this.treeView1.SelectedNode.Nodes)
                        {
                            if ((node.Tag as MapTemplateElement).Element == Item)
                            {
                                tag = node.Tag as MapTemplateElement;
                                tag.Delete();
                                (this.treeView1.SelectedNode.Parent.Tag as MapTemplate).RemoveMapTemplateElement(tag);
                                node.Remove();
                                break;
                            }
                        }
                    }
                }
            }
        }

        #endregion


        #region 菜单事件
        private void MenuItem_CopyMapTemplate(object sender, EventArgs e)
        {
            try
            {
                MapTemplate mapTemplate = (this.treeView1.SelectedNode.Tag as MapTemplate).Clone();
                TreeNode treeNode = new TreeNode(mapTemplate.Name)
                {
                    Tag = mapTemplate
                };
                treeNode.Nodes.Add(new TreeNode(""));
                this.treeView1.SelectedNode.Parent.Nodes.Add(treeNode);
            }
            catch (Exception exception)
            {
            }
        }

        private void MenuItem_DeleteAllMapTemplateElement(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确定删除所有元素?", "地图模板", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                TreeNode selectedNode = this.treeView1.SelectedNode;
                foreach (TreeNode node in selectedNode.Nodes)
                {
                    MapTemplateElement tag = node.Tag as MapTemplateElement;
                    if (tag.Element != null)
                    {
                        this._layoutControl.ActiveView.GraphicsContainer.DeleteElement(tag.Element);
                    }
                    tag.Delete();
                }
                selectedNode.Nodes.Clear();
                (selectedNode.Parent.Tag as MapTemplate).RemoveAllMapTemplateElement();
                this._layoutControl.ActiveView.Refresh();
            }
        }

        private void MenuItem_DeleteAllMapTemplateParam(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确定删除所有参数?", "地图模板", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                TreeNode selectedNode = this.treeView1.SelectedNode;
                foreach (TreeNode node in selectedNode.Nodes)
                {
                    (node.Tag as MapTemplateParam).Delete();
                }
                selectedNode.Nodes.Clear();
            }
        }

        private void MenuItem_DeleteMapTemplate(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确定删除地图模板?", "地图模板", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    (this.treeView1.SelectedNode.Tag as MapTemplate).Delete();
                    this.treeView1.SelectedNode.Remove();
                    this._layoutControl.ActiveView.Refresh();
                }
                catch (Exception exception)
                {
                }
            }
        }

        private void MenuItem_DeleteMapTemplateClass(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确定删除地图模板类别?", "地图模板", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                (this.treeView1.SelectedNode.Tag as MapTemplateClass).Delete();
                this.treeView1.SelectedNode.Remove();
                this._layoutControl.ActiveView.Refresh();
            }
        }

        private void MenuItem_DeleteMapTemplateElement(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确定删除元素?", "地图模板", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                MapTemplateElement tag = this.treeView1.SelectedNode.Tag as MapTemplateElement;
                if (tag.Element != null)
                {
                    IGraphicsContainer graphicsContainer = this._layoutControl.ActiveView.GraphicsContainer;
                    this.m_CanDo = false;
                    try
                    {
                        graphicsContainer.DeleteElement(tag.Element);
                    }
                    catch
                    {
                    }
                    this.m_CanDo = true;
                }
                tag.Delete();
                tag.MapTemplate.RemoveMapTemplateElement(tag);
                TreeNode parent = this.treeView1.SelectedNode.Parent;
                this.treeView1.SelectedNode.Remove();
                if (parent != null)
                {
                    this.treeView1.SelectedNode = parent;
                }
                this._layoutControl.ActiveView.Refresh();
            }
        }

        private void MenuItem_DeleteMapTemplateParam(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确定删除参数?", "地图模板", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                MapTemplateParam tag = this.treeView1.SelectedNode.Tag as MapTemplateParam;
                tag.Delete();
                tag.MapTemplate.RemoveMapTemplateParam(tag);
                this.treeView1.SelectedNode.Remove();
            }
        }

        private void MenuItem_ExportMapTemplateProperty(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "地图模板|*.tmp"
            };
            MapTemplateClass tag = this.treeView1.SelectedNode.Tag as MapTemplateClass;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                MapTemplate mapTemplate = new MapTemplate(-1, tag);
                try
                {
                    mapTemplate.LoadFromFile(fileName);
                }
                catch (Exception exception)
                {
                }
                mapTemplate.Save();
                TreeNode treeNode = new TreeNode(mapTemplate.Name)
                {
                    Tag = mapTemplate
                };
                TreeNodeCollection nodes = treeNode.Nodes;
                TreeNode treeNode1 = new TreeNode("模板参数")
                {
                    Tag = "模板参数"
                };
                nodes.Add(treeNode1);
                TreeNodeCollection treeNodeCollections = treeNode.Nodes;
                TreeNode treeNode2 = new TreeNode("模板元素")
                {
                    Tag = "模板元素"
                };
                treeNodeCollections.Add(treeNode2);
                this.treeView1.SelectedNode.Nodes.Add(treeNode);
            }
        }

        private void MenuItem_MapTemplateClassProperty(object sender, EventArgs e)
        {
            frmNewMapTemplateClass _frmNewMapTemplateClass = new frmNewMapTemplateClass();
            MapTemplateClass tag = this.treeView1.SelectedNode.Tag as MapTemplateClass;
            _frmNewMapTemplateClass.MapTemplateClassName = tag.Name;
            _frmNewMapTemplateClass.Description = tag.Description;
            if (_frmNewMapTemplateClass.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tag.Name = _frmNewMapTemplateClass.MapTemplateClassName;
                tag.Description = _frmNewMapTemplateClass.Description;
                tag.Save();
                this.treeView1.SelectedNode.Text = tag.Name;
            }
        }

        private void MenuItem_MapTemplateElementProperty(object sender, EventArgs e)
        {
            this.EditElementProperty();
        }

        private void MenuItem_MapTemplateParamProperty(object sender, EventArgs e)
        {
            frmAddParams frmAddParam = new frmAddParams();
            MapTemplateParam tag = this.treeView1.SelectedNode.Tag as MapTemplateParam;
            frmAddParam.MapTemplateParam = tag;
            if (frmAddParam.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tag.Save();
                this.treeView1.SelectedNode.Text = tag.Name;
            }
        }

        private void MenuItem_MapTemplateProperty(object sender, EventArgs e)
        {
            MapTemplate tag = this.treeView1.SelectedNode.Tag as MapTemplate;
            PropertySheet propertySheet = new PropertySheet();
            propertySheet.AddPage(new MapTemplateGeneralPage());
            if (tag.MapGrid == null)
            {
                propertySheet.AddPage(new OtherGridPropertyPage());
            }
            else
            {
                propertySheet.AddPage(new GridAxisPropertyPage());
                propertySheet.AddPage(new LabelFormatPropertyPage());
                propertySheet.AddPage(new TickSymbolPropertyPage());
            }
            if (propertySheet.EditProperties(tag))
            {
                this.m_CanDo = false;
                tag.Update(this._layoutControl.PageLayout);
                this.treeView1.SelectedNode.Text = tag.Name;
                this.DeleteAllElements(this._layoutControl.ActiveView);
                tag.CreateDesignTK(this._layoutControl.ActiveView);
                (this._layoutControl.ActiveView as IGraphicsContainerSelect).UnselectAllElements();
                this._layoutControl.ActiveView.Refresh();
                this.m_CanDo = true;
            }
        }

        private void MenuItem_NewMapTemplate(object sender, EventArgs e)
        {
            YTMapTemplateWizard ytMapTemplateWizard = new YTMapTemplateWizard();
            MapTemplateClass tag = this.treeView1.SelectedNode.Tag as MapTemplateClass;
            MapTemplate mapTemplate = new MapTemplate(-1, tag);
            ytMapTemplateWizard.MapTemplate = mapTemplate;
            if (ytMapTemplateWizard.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mapTemplate.Save();
                TreeNode treeNode = new TreeNode(mapTemplate.Name)
                {
                    Tag = mapTemplate
                };
                TreeNodeCollection nodes = treeNode.Nodes;
                TreeNode treeNode1 = new TreeNode("模板参数")
                {
                    Tag = "模板参数"
                };
                nodes.Add(treeNode1);
                TreeNodeCollection treeNodeCollections = treeNode.Nodes;
                TreeNode treeNode2 = new TreeNode("模板元素")
                {
                    Tag = "模板元素"
                };
                treeNodeCollections.Add(treeNode2);
                this.treeView1.SelectedNode.Nodes.Add(treeNode);
            }
        }

        private void MenuItem_NewMapTemplateClass(object sender, EventArgs e)
        {
            frmNewMapTemplateClass _frmNewMapTemplateClass = new frmNewMapTemplateClass()
            {
                MapTemplateGallery = this.m_MapTemplateGallery
            };
            if (_frmNewMapTemplateClass.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MapTemplateClass mapTemplateClass = new MapTemplateClass(-1, this.m_MapTemplateGallery)
                {
                    Name = _frmNewMapTemplateClass.MapTemplateClassName,
                    Description = _frmNewMapTemplateClass.Description
                };
                MapTemplateClass mapTemplateClass1 = mapTemplateClass;
                mapTemplateClass1.Save();
                TreeNode treeNode = new TreeNode(mapTemplateClass1.Name)
                {
                    Tag = mapTemplateClass1
                };
                this.treeView1.Nodes[0].Nodes.Add(treeNode);
            }
        }

        private void MenuItem_NewMapTemplateElement(object sender, EventArgs e)
        {
            this.m_CanDo = false;
            YTElementWizardForm ytElementWizardForm = new YTElementWizardForm();
            MapTemplate tag = this.treeView1.SelectedNode.Parent.Tag as MapTemplate;
            ytElementWizardForm.MapTemplate = tag;
            if (ytElementWizardForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ytElementWizardForm.MapTemplateElement.CreateElement(this._layoutControl.PageLayout);
                ytElementWizardForm.MapTemplateElement.Save();
                tag.AddMapTemplateElement(ytElementWizardForm.MapTemplateElement);
                TreeNode treeNode = new TreeNode(ytElementWizardForm.MapTemplateElement.Name)
                {
                    Tag = ytElementWizardForm.MapTemplateElement
                };
                this.treeView1.SelectedNode.Nodes.Add(treeNode);
                IGraphicsContainer graphicsContainer = this._layoutControl.ActiveView.GraphicsContainer;
                IElement element = ytElementWizardForm.MapTemplateElement.GetElement(this._layoutControl.PageLayout);
                graphicsContainer.AddElement(element, -1);
                this._layoutControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
            }
            this.m_CanDo = true;
        }

        private void MenuItem_NewMapTemplateParam(object sender, EventArgs e)
        {
            frmAddParams frmAddParam = new frmAddParams();
            MapTemplate tag = this.treeView1.SelectedNode.Parent.Tag as MapTemplate;
            MapTemplateParam mapTemplateParam = new MapTemplateParam(-1, tag);
            frmAddParam.MapTemplateParam = mapTemplateParam;
            if (frmAddParam.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mapTemplateParam.Save();
                tag.AddMapTemplateParam(mapTemplateParam);
                TreeNode treeNode = new TreeNode(mapTemplateParam.Name)
                {
                    Tag = mapTemplateParam
                };
                this.treeView1.SelectedNode.Nodes.Add(treeNode);
            }
        }

        private void MenuItem_RefreshTreeView(object sender, EventArgs e)
        {
            TreeNode item = this.treeView1.Nodes[0];
            this.treeView1.Nodes[0].Nodes.Clear();
            foreach (MapTemplateClass mapTemplateClass in this.m_MapTemplateGallery.MapTemplateClass)
            {
                TreeNode treeNode = new TreeNode(mapTemplateClass.Name)
                {
                    Tag = mapTemplateClass
                };
                treeNode.Nodes.Add(new TreeNode(""));
                item.Nodes.Add(treeNode);
            }
        }

        private void MenuItem_SaveMapTemplateToFile(object sender, EventArgs e)
        {
            try
            {
                MapTemplate tag = this.treeView1.SelectedNode.Tag as MapTemplate;
                SaveFileDialog saveFileDialog = new SaveFileDialog()
                {
                    Filter = "地图模板|*.tmp",
                    OverwritePrompt = true,
                    FileName = string.Concat(tag.Name, ".tmp")
                };
                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileName = saveFileDialog.FileName;
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                    tag.SaveToFile(fileName);
                }
            }
            catch (Exception exception)
            {
            }
        }
        #endregion

        #region TreeView事件
      

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            MapTemplate tag;
            if (_layoutControl == null)
            {
                _layoutControl = _context.MainView.PageLayoutControl;
            }
            if (this.m_TreeviewSelectChange)
            {
                this.m_CanSelectChange = false;
                this.BulidMenu(e.Node);
                this.m_CanDo = false;
                try
                {
                    if (e.Node.Tag is MapTemplateClass)
                    {
                        if (this.m_pLastSelect != null)
                        {
                            this.DeleteAllElements(this._layoutControl.ActiveView);
                            ((IActiveView)this._layoutControl.ActiveView.FocusMap).Refresh();
                            this._layoutControl.ActiveView.Refresh();
                        }
                        if (e.Node.Nodes.Count == 1)
                        {
                            if (e.Node.Nodes[0].Tag == null)
                            {
                                e.Node.Nodes.Clear();
                                this.LoadMapTemplate(e.Node);
                            }
                        }
                        this.m_pLastSelect = null;
                        this.m_pLastMapTemplate = null;
                        MapTemplate.CurrentMapTemplate = null;
                    }
                    else if (e.Node.Tag is MapTemplate)
                    {
                        if (this.m_pLastMapTemplate != e.Node.Tag)
                        {
                            if (this.m_pLastSelect != null)
                            {
                                this.DeleteAllElements(this._layoutControl.ActiveView);
                            }
                            this.m_pLastMapTemplate = e.Node.Tag as MapTemplate;
                            this.m_pLastMapTemplate.CreateDesignTK(this._layoutControl.ActiveView);
                            (this._layoutControl.ActiveView as IGraphicsContainerSelect).UnselectAllElements();
                            ((IActiveView)this._layoutControl.ActiveView.FocusMap).Refresh();
                            this._layoutControl.ActiveView.Refresh();
                            this.m_pLastSelect = this.treeView1.SelectedNode;
                        }
                        MapTemplate.CurrentMapTemplate = this.m_pLastMapTemplate;
                    }
                    else if (!(e.Node.Tag is MapTemplateElement ? false : !(e.Node.Tag is MapTemplateParam)))
                    {
                        tag = e.Node.Parent.Parent.Tag as MapTemplate;
                        if (this.m_pLastMapTemplate != tag)
                        {
                            if (this.m_pLastSelect != null)
                            {
                                this.DeleteAllElements(this._layoutControl.ActiveView);
                            }
                            this.m_pLastMapTemplate = tag;
                            this.m_pLastMapTemplate.CreateDesignTK(this._layoutControl.ActiveView);
                            (this._layoutControl.ActiveView as IGraphicsContainerSelect).UnselectAllElements();
                            ((IActiveView)this._layoutControl.ActiveView.FocusMap).Refresh();
                            this._layoutControl.ActiveView.Refresh();
                        }
                        this.m_pLastSelect = this.treeView1.SelectedNode.Parent.Parent;
                        if (this.treeView1.SelectedNode.Tag is MapTemplateElement)
                        {
                            IGraphicsContainerSelect graphicsContainer = this._layoutControl.ActiveView.GraphicsContainer as IGraphicsContainerSelect;
                            ISelection elementSelection = (this._layoutControl.ActiveView as IViewManager).ElementSelection;
                            this._layoutControl.ActiveView.Selection = elementSelection;
                            graphicsContainer.UnselectAllElements();
                            graphicsContainer.SelectElement((this.treeView1.SelectedNode.Tag as MapTemplateElement).Element);
                            ((IActiveView)this._layoutControl.ActiveView.FocusMap).Refresh();
                            this._layoutControl.ActiveView.Refresh();
                        }
                        MapTemplate.CurrentMapTemplate = this.m_pLastMapTemplate;
                    }
                    else if (!(e.Node.Tag is string))
                    {
                        if (this.m_pLastSelect != null)
                        {
                            this.DeleteAllElements(this._layoutControl.ActiveView);
                        }
                        ((IActiveView)this._layoutControl.ActiveView.FocusMap).Refresh();
                        this._layoutControl.ActiveView.Refresh();
                        this.m_pLastSelect = null;
                        this._layoutControl = null;
                        MapTemplate.CurrentMapTemplate = this.m_pLastMapTemplate;
                    }
                    else
                    {
                        if ((e.Node.Text == "模板元素" ? false : !(e.Node.Text == "模板参数")))
                        {
                            if (this.m_pLastSelect != null)
                            {
                                this.DeleteAllElements(this._layoutControl.ActiveView);
                            }
                            this._layoutControl.ActiveView.Refresh();
                            this.m_pLastSelect = null;
                        }
                        else
                        {
                            tag = e.Node.Parent.Tag as MapTemplate;
                            if (this.m_pLastMapTemplate == tag)
                            {
                                (this._layoutControl.ActiveView as IGraphicsContainerSelect).UnselectAllElements();
                                ((IActiveView)this._layoutControl.ActiveView.FocusMap).Refresh();
                                this._layoutControl.ActiveView.Refresh();
                            }
                            else
                            {
                                if (this.m_pLastSelect != null)
                                {
                                    this.DeleteAllElements(this._layoutControl.ActiveView);
                                }
                                this.m_pLastMapTemplate = tag;
                                this.m_pLastMapTemplate.CreateDesignTK(this._layoutControl.ActiveView);
                                (this._layoutControl.ActiveView as IGraphicsContainerSelect).UnselectAllElements();
                                ((IActiveView)this._layoutControl.ActiveView.FocusMap).Refresh();
                                this._layoutControl.ActiveView.Refresh();
                                this.m_pLastSelect = this.treeView1.SelectedNode.Parent;
                            }
                        }
                        MapTemplate.CurrentMapTemplate = this.m_pLastMapTemplate;
                    }
                }
                catch (Exception exception)
                {
                }
                this.m_CanSelectChange = true;
                this.m_CanDo = true;
            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Tag is MapTemplateClass)
            {
                if (e.Node.Nodes.Count == 1)
                {
                    if (e.Node.Nodes[0].Tag == null)
                    {
                        e.Node.Nodes.Clear();
                        this.LoadMapTemplate(e.Node);
                    }
                }
            }
            else if (e.Node.Tag is MapTemplate)
            {
                if (e.Node.Nodes.Count == 1)
                {
                    if (e.Node.Nodes[0].Tag == null)
                    {
                        e.Node.Nodes.Clear();
                        this.InitMapTemplate(e.Node);
                    }
                }
            }
        }

        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
            TreeNode nodeAt = this.treeView1.GetNodeAt(e.Location);
            if (nodeAt != null)
            {
                if (this.treeView1.SelectedNode != nodeAt)
                {
                    this.treeView1.SelectedNode = nodeAt;
                }
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    this.contextMenuStrip1.Show(this.treeView1, e.Location);
                }
            }
        }

        #endregion
        #region 其他
        public string OldTFHToNew(string oldTFH)
        {
            string str = "";
            string str1 = "";
            string str2 = "";
            int num = 0;
            int num1 = 0;
            string str3 = "";
            string str4 = "";
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            char[] chrArray = new char[1];
            string[] strArrays = null;
            string str5 = "";
            try
            {
                chrArray[0] = '-';
                strArrays = oldTFH.Split(chrArray);
                num5 = int.Parse(strArrays.GetValue(2).ToString());
                num6 = (num5 % 12 == 0 ? num5 / 12 : num5 / 12 + 1);
                num7 = (num5 % 12 != 0 ? num5 % 12 : 12);
                str5 = strArrays.GetValue(3).ToString();
                str5 = str5.Replace('(', ' ');
                num2 = int.Parse(str5.Replace(')', ' '));
                num3 = (num2 % 8 == 0 ? num2 / 8 : num2 / 8 + 1);
                num4 = (num2 % 8 != 0 ? num2 % 8 : 8);
                if ((oldTFH.Contains("a") || oldTFH.Contains("b") || oldTFH.Contains("c") ? false : !oldTFH.Contains("d")))
                {
                    str2 = "G";
                    num = (num6 - 1) * 8 + num3;
                    num1 = (num7 - 1) * 8 + num4;
                }
                else
                {
                    str2 = "H";
                    str5 = strArrays.GetValue((int)strArrays.Length - 1).ToString();
                    string str6 = str5;
                    if (str6 != null)
                    {
                        if (str6 == "a")
                        {
                            num8 = 1;
                            num9 = 1;
                        }
                        else if (str6 == "b")
                        {
                            num8 = 1;
                            num9 = 2;
                        }
                        else if (str6 == "c")
                        {
                            num8 = 2;
                            num9 = 1;
                        }
                        else if (str6 == "d")
                        {
                            num8 = 2;
                            num9 = 2;
                        }
                    }
                    num = ((num6 - 1) * 8 + (num3 - 1)) * 2 + num8;
                    num1 = ((num7 - 1) * 8 + (num4 - 1)) * 2 + num9;
                }
                str1 = string.Concat(str2, strArrays.GetValue(0), strArrays.GetValue(1));
                str3 = num.ToString();
                str4 = num1.ToString();
                if (str3.Length == 1)
                {
                    str3 = string.Concat("0", str3);
                }
                if (str4.Length == 1)
                {
                    str4 = string.Concat("0", str4);
                }
                str = string.Concat(str1, str3, str4);
            }
            catch (Exception exception)
            {
                //Debug.WriteLine(exception.Message);
            }
            return str;
        }
        #endregion


        #region Override DockPanelControlBase

        public override Bitmap Image
        {
            get { return Properties.Resources.icon_layout; }
        }

        public override string Caption
        {
            get { return "地图模板"; }
            set { Caption = value; }
        }

        public override DockPanelState DefaultDock
        {
            get { return DockPanelState.Right; }
        }

        public override string DockName
        {
            get { return DefaultDockName; }
        }

        public override string DefaultNestDockName
        {
            get { return ""; }
        }

        public const string DefaultDockName = "MapTemplate_Viewer";
        public IEnumerable<ToolStripItemCollection> ToolStrips
        {
            get { yield break; }
        }

        public IEnumerable<Control> Buttons
        {
            get { yield break; }
        }



        #endregion

        private void mnuConnectTemplateDB_Click(object sender, EventArgs e)
        {
            frmOpenFile openFile = new frmOpenFile();
            openFile.AddFilter(new MyGxFilterPersonalGeodatabases(), true);
            openFile.AddFilter(new MyGxFilterFileGeodatabases(), false);
            openFile.AllowMultiSelect = false;
            if (openFile.ShowDialog() != DialogResult.OK) return;
            IGxObject obj2 = openFile.Items.get_Element(0) as IGxObject;
            string fileName = obj2.FullName;
            InitTree(fileName);
        }

        private void mnuDisconnectTemplateDB_Click(object sender, EventArgs e)
        {
            if (this.m_MapTemplateGallery == null || this.m_MapTemplateGallery.IsValid() == false)
            {
                return;
            }
            m_MapTemplateGallery.Workspace = null;
            mnuDisconnectTemplateDB.Enabled = false;
        }

        private void MapTemplateView_Load(object sender, EventArgs e)
        {
            ElementChangeEvent.OnEditElementProperty += new OnEditElementPropertyHandler(this.ElementChangeEvent_OnEditElementProperty);
            ElementChangeEvent.OnDeleteElement += new OnDeleteElementHandler(this.ElementChangeEvent_OnDeleteElement);
            ElementChangeEvent.OnElementPositionChange += new OnElementPositionChangeHandler(this.ElementChangeEvent_OnElementPositionChange);
            ElementChangeEvent.OnElementSelectChange += new OnElementSelectChangeHandler(this.ElementChangeEvent_OnElementSelectChange);
            this.InitTree(_plugin.PrintingConfig.TemplateConnectionString);
            this.m_iPageLayout = _layoutControl.ActiveView as IActiveViewEvents_Event;
            this.m_iPageLayout.ItemAdded += new IActiveViewEvents_ItemAddedEventHandler(m_iPageLayout_ItemAdded);
            this.m_iPageLayout.ItemDeleted += new IActiveViewEvents_ItemDeletedEventHandler(m_iPageLayout_ItemDeleted);
            this.m_CanSelectChange = true;
            this.m_TreeviewSelectChange = true;
        }

     
    }
}
