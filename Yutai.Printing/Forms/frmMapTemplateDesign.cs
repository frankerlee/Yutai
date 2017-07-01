using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using CartoTemplateApp;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Carto.MapCartoTemplateLib;
using Yutai.ArcGIS.Carto.UI;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Carto;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Controls.ApplicationStyle;
using Yutai.ArcGIS.Controls.Controls;
using Yutai.ArcGIS.Controls.Controls.TOCDisplay;
using frmElementProperty = Yutai.ArcGIS.Controls.Controls.frmElementProperty;
using NorthArrowPropertyPage = Yutai.ArcGIS.Carto.MapCartoTemplateLib.NorthArrowPropertyPage;
using NumberAndLabelPropertyPage = Yutai.ArcGIS.Carto.MapCartoTemplateLib.NumberAndLabelPropertyPage;
using ScaleAndUnitsPropertyPage = Yutai.ArcGIS.Carto.MapCartoTemplateLib.ScaleAndUnitsPropertyPage;
using ScaleBarFormatPropertyPage = Yutai.ArcGIS.Carto.MapCartoTemplateLib.ScaleBarFormatPropertyPage;
using ScaleTextFormatPropertyPage = Yutai.ArcGIS.Carto.MapCartoTemplateLib.ScaleTextFormatPropertyPage;
using ScaleTextTextPropertyPage = Yutai.ArcGIS.Carto.MapCartoTemplateLib.ScaleTextTextPropertyPage;

namespace Yutai.Plugins.Printing.Forms
{
	public class FrmMapTemplateDesign : Form
	{
		private IContainer components = null;

		private BarManager barManager11;

		private Bar bar3;

		private BarDockControl barDockControlTop;

		private BarDockControl barDockControlBottom;

		private BarDockControl barDockControlLeft;

		private BarDockControl barDockControlRight;

		private DockManager dockManager1;

		private DockPanel dockPanel1;

		private ControlContainer dockPanel1_Container;

		private TreeView treeView1;

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

		private AxPageLayoutControl axPageLayoutControl1;

		private BarDockControl barDockControl3;

		private BarDockControl barDockControl4;

		private BarDockControl barDockControl2;

		private BarDockControl barDockControl1;

		private BarManager barManager1;

		private Bar bar4;

		private BarStaticItem barStaticItem1;

		private BarStaticItem barStaticItem2;

		private IStyleGallery m_pSG = new MyStyleGallery();

		//private Framework m_pFrame = new Framework(null);

		private MapTemplateGallery m_MapTemplateGallery = new MapTemplateGallery();

		private bool m_TreeviewSelectChange = false;

		private bool m_CanSelectChange = false;

		private PopupMenu popupMenu1;

		private TOCTreeViewWrapEx m_wrap;

		private bool m_CanDo = true;

		private IActiveViewEvents_Event m_iPageLayout;

		private TreeNode m_pLastSelect = null;

		private MapTemplate m_pLastMapTemplate = null;

		public FrmMapTemplateDesign()
		{
			this.InitializeComponent();
		}

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

		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
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
						propertySheet.AddPage(new ScaleBarFormatPropertyPage());
						propertySheet.AddPage(new ScaleAndUnitsPropertyPage());
						propertySheet.AddPage(new NumberAndLabelPropertyPage());
						obj = element;
					}
					else if (tag.MapTemplateElementType == MapTemplateElementType.ScaleTextElement)
					{
						propertySheet.AddPage(new ScaleTextTextPropertyPage());
						propertySheet.AddPage(new ScaleTextFormatPropertyPage());
						obj = element;
					}
					else if (tag.MapTemplateElementType == MapTemplateElementType.NorthElement)
					{
						propertySheet.AddPage(new NorthArrowPropertyPage());
						obj = element;
					}
					else if (element is IGroupElement)
					{
					}
					if (propertySheet.EditProperties(tag))
					{
						tag.Update(this.axPageLayoutControl1.PageLayout);
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
							tag.Update(this.axPageLayoutControl1.PageLayout);
							this.axPageLayoutControl1.ActiveView.Refresh();
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
							tag.Update(this.axPageLayoutControl1.PageLayout);
							this.axPageLayoutControl1.ActiveView.Refresh();
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
							tag.Update(this.axPageLayoutControl1.PageLayout);
							this.axPageLayoutControl1.ActiveView.Refresh();
						}
					}
				}
			}
			catch (Exception exception)
			{
			}
		}

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
							(item.Tag as MapTemplateElement).ChangePosition(this.axPageLayoutControl1.PageLayout);
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
				IGraphicsContainerSelect graphicsContainer = this.axPageLayoutControl1.ActiveView.GraphicsContainer as IGraphicsContainerSelect;
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

		private void frmMain_Load(object sender, EventArgs e)
		{
			PaintStyleMenuItem.SetDefaultStyle();
			ElementChangeEvent.OnEditElementProperty += new OnEditElementPropertyHandler(this.ElementChangeEvent_OnEditElementProperty);
			ElementChangeEvent.OnDeleteElement += new OnDeleteElementHandler(this.ElementChangeEvent_OnDeleteElement);
			ElementChangeEvent.OnElementPositionChange += new OnElementPositionChangeHandler(this.ElementChangeEvent_OnElementPositionChange);
			ElementChangeEvent.OnElementSelectChange += new OnElementSelectChangeHandler(this.ElementChangeEvent_OnElementSelectChange);
			this.InitTree();
			this.m_pFrame.Hook = this.axPageLayoutControl1.Object;
			BarManagerImplement barManagerImplement = new BarManagerImplement()
			{
				BarMousePositionItem = this.barStaticItem1,
				BarPagePositionItem = this.barStaticItem2,
				BarManager = this.barManager1,
				PopupMenu = this.popupMenu1
			};
			this.m_pFrame.BarManager = barManagerImplement;
			barManagerImplement.CreateBars(System.IO.Path.Combine(Application.StartupPath, "CartoTempleteDesignTool.xml"));
			this.m_pFrame.Init();
			string str = System.IO.Path.Combine(Application.StartupPath, "Xian 1980 3 Degree GK CM 114E.prj");
			ISpatialReferenceFactory spatialReferenceEnvironmentClass = new SpatialReferenceEnvironmentClass();
			this.axPageLayoutControl1.ActiveView.FocusMap.SpatialReference = spatialReferenceEnvironmentClass.CreateESRISpatialReferenceFromPRJFile(str);
			this.axPageLayoutControl1.ActiveView.FocusMap.MapScale = 5000;
			this.m_iPageLayout = (this.axPageLayoutControl1.Object as IPageLayoutControl2).PageLayout as IActiveViewEvents_Event;
			this.m_iPageLayout.ItemAdded += new IActiveViewEvents_ItemAddedEventHandler(this.m_iPageLayout_ItemAdded);
			this.m_iPageLayout.ItemDeleted += new IActiveViewEvents_ItemDeletedEventHandler(this.m_iPageLayout_ItemDeleted);
			this.m_CanSelectChange = true;
			this.m_TreeviewSelectChange = true;
			this.m_pFrame.ActiveControl = this.axPageLayoutControl1;
			this.barManager1.SetPopupContextMenu(this.axPageLayoutControl1, this.popupMenu1);
			this.InitStyle();
		}

		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMapTemplateDesign));
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar4 = new DevExpress.XtraBars.Bar();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItem2 = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(0, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 0);
            this.barDockControlBottom.Size = new System.Drawing.Size(0, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 0);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Location = new System.Drawing.Point(0, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 0);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.MenuManager = this.barManager1;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "JLK.Bars.BarDockControl",
            "System.Windows.Forms.StatusBar",
            "JLK.Bars.Ribbon.RibbonStatusBar",
            "JLK.Bars.Ribbon.RibbonControl"});
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar4});
            this.barManager1.DockControls.Add(this.barDockControl1);
            this.barManager1.DockControls.Add(this.barDockControl2);
            this.barManager1.DockControls.Add(this.barDockControl3);
            this.barManager1.DockControls.Add(this.barDockControl4);
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barStaticItem1,
            this.barStaticItem2});
            this.barManager1.MaxItemId = 2;
            this.barManager1.StatusBar = this.bar4;
            // 
            // bar4
            // 
            this.bar4.BarName = "Status bar";
            this.bar4.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar4.DockCol = 0;
            this.bar4.DockRow = 0;
            this.bar4.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar4.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem2)});
            this.bar4.OptionsBar.AllowQuickCustomization = false;
            this.bar4.OptionsBar.DrawDragBorder = false;
            this.bar4.OptionsBar.UseWholeRow = true;
            this.bar4.Text = "Status bar";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.AutoSize = DevExpress.XtraBars.BarStaticItemSize.None;
            this.barStaticItem1.Id = 0;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.Size = new System.Drawing.Size(260, 0);
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            this.barStaticItem1.Width = 260;
            // 
            // barStaticItem2
            // 
            this.barStaticItem2.AutoSize = DevExpress.XtraBars.BarStaticItemSize.None;
            this.barStaticItem2.Id = 1;
            this.barStaticItem2.Name = "barStaticItem2";
            this.barStaticItem2.Size = new System.Drawing.Size(260, 0);
            this.barStaticItem2.TextAlignment = System.Drawing.StringAlignment.Near;
            this.barStaticItem2.Width = 260;
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 0);
            this.barDockControl1.Size = new System.Drawing.Size(699, 0);
            // 
            // barDockControl2
            // 
            this.barDockControl2.CausesValidation = false;
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(0, 402);
            this.barDockControl2.Size = new System.Drawing.Size(699, 27);
            // 
            // barDockControl3
            // 
            this.barDockControl3.CausesValidation = false;
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl3.Location = new System.Drawing.Point(0, 0);
            this.barDockControl3.Size = new System.Drawing.Size(0, 402);
            // 
            // barDockControl4
            // 
            this.barDockControl4.CausesValidation = false;
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl4.Location = new System.Drawing.Point(699, 0);
            this.barDockControl4.Size = new System.Drawing.Size(0, 402);
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.FloatVertical = true;
            this.dockPanel1.ID = new System.Guid("84d4b3f4-b12f-4a64-bc48-f0e034083dbd");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(200, 402);
            this.dockPanel1.Size = new System.Drawing.Size(200, 429);
            this.dockPanel1.Text = "制图模板";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.treeView1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(191, 402);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(191, 402);
            this.treeView1.TabIndex = 0;
            this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // axPageLayoutControl1
            // 
            this.axPageLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axPageLayoutControl1.Location = new System.Drawing.Point(200, 0);
            this.axPageLayoutControl1.Name = "axPageLayoutControl1";
            this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
            this.axPageLayoutControl1.Size = new System.Drawing.Size(499, 402);
            this.axPageLayoutControl1.TabIndex = 6;
            // 
            // popupMenu1
            // 
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 429);
            this.Controls.Add(this.axPageLayoutControl1);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.Name = "FrmMapTemplateDesign";
            this.Text = "地图制图模板管理系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

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
				this.m_pSG = new ServerStyleGalleryClass();
				for (int i = (this.m_pSG as IStyleGalleryStorage).FileCount - 1; i >= 0; i--)
				{
					string file = (this.m_pSG as IStyleGalleryStorage)[i];
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
			ApplicationBase.StyleGallery = this.m_pSG;
		}

		private void InitTree()
		{
			TreeNode treeNode = new TreeNode("地图模板");
			this.m_MapTemplateGallery.Init();
			foreach (MapTemplateClass mapTemplateClass in this.m_MapTemplateGallery.MapTemplateClass)
			{
				TreeNode treeNode1 = new TreeNode(mapTemplateClass.Name)
				{
					Tag = mapTemplateClass
				};
				treeNode1.Nodes.Add(new TreeNode(""));
				treeNode.Nodes.Add(treeNode1);
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
									mapTemplateTextElement.ChangePosition(this.axPageLayoutControl1.PageLayout);
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
						this.axPageLayoutControl1.ActiveView.GraphicsContainer.DeleteElement(tag.Element);
					}
					tag.Delete();
				}
				selectedNode.Nodes.Clear();
				(selectedNode.Parent.Tag as MapTemplate).RemoveAllMapTemplateElement();
				this.axPageLayoutControl1.ActiveView.Refresh();
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
					this.axPageLayoutControl1.ActiveView.Refresh();
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
				this.axPageLayoutControl1.ActiveView.Refresh();
			}
		}

		private void MenuItem_DeleteMapTemplateElement(object sender, EventArgs e)
		{
			if (MessageBox.Show("是否确定删除元素?", "地图模板", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
			{
				MapTemplateElement tag = this.treeView1.SelectedNode.Tag as MapTemplateElement;
				if (tag.Element != null)
				{
					IGraphicsContainer graphicsContainer = this.axPageLayoutControl1.ActiveView.GraphicsContainer;
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
				this.axPageLayoutControl1.ActiveView.Refresh();
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
				tag.Update(this.axPageLayoutControl1.PageLayout);
				this.treeView1.SelectedNode.Text = tag.Name;
				this.DeleteAllElements(this.axPageLayoutControl1.ActiveView);
				tag.CreateDesignTK(this.axPageLayoutControl1.ActiveView);
				(this.axPageLayoutControl1.ActiveView as IGraphicsContainerSelect).UnselectAllElements();
				this.axPageLayoutControl1.ActiveView.Refresh();
				this.m_CanDo = true;
			}
		}

		private void MenuItem_NewMapTemplate(object sender, EventArgs e)
		{
			JLKMapTemplateWizard jLKMapTemplateWizard = new JLKMapTemplateWizard();
			MapTemplateClass tag = this.treeView1.SelectedNode.Tag as MapTemplateClass;
			MapTemplate mapTemplate = new MapTemplate(-1, tag);
			jLKMapTemplateWizard.MapTemplate = mapTemplate;
			if (jLKMapTemplateWizard.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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
			JLKElementWizardForm jLKElementWizardForm = new JLKElementWizardForm();
			MapTemplate tag = this.treeView1.SelectedNode.Parent.Tag as MapTemplate;
			jLKElementWizardForm.MapTemplate = tag;
			if (jLKElementWizardForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				jLKElementWizardForm.MapTemplateElement.CreateElement(this.axPageLayoutControl1.PageLayout);
				jLKElementWizardForm.MapTemplateElement.Save();
				tag.AddMapTemplateElement(jLKElementWizardForm.MapTemplateElement);
				TreeNode treeNode = new TreeNode(jLKElementWizardForm.MapTemplateElement.Name)
				{
					Tag = jLKElementWizardForm.MapTemplateElement
				};
				this.treeView1.SelectedNode.Nodes.Add(treeNode);
				IGraphicsContainer graphicsContainer = this.axPageLayoutControl1.ActiveView.GraphicsContainer;
				IElement element = jLKElementWizardForm.MapTemplateElement.GetElement(this.axPageLayoutControl1.PageLayout);
				graphicsContainer.AddElement(element, -1);
				this.axPageLayoutControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
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
				Debug.WriteLine(exception.Message);
			}
			return str;
		}

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			MapTemplate tag;
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
							this.DeleteAllElements(this.axPageLayoutControl1.ActiveView);
							this.axPageLayoutControl1.ActiveView.Refresh();
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
								this.DeleteAllElements(this.axPageLayoutControl1.ActiveView);
							}
							this.m_pLastMapTemplate = e.Node.Tag as MapTemplate;
							this.m_pLastMapTemplate.CreateDesignTK(this.axPageLayoutControl1.ActiveView);
							(this.axPageLayoutControl1.ActiveView as IGraphicsContainerSelect).UnselectAllElements();
							this.axPageLayoutControl1.ActiveView.Refresh();
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
								this.DeleteAllElements(this.axPageLayoutControl1.ActiveView);
							}
							this.m_pLastMapTemplate = tag;
							this.m_pLastMapTemplate.CreateDesignTK(this.axPageLayoutControl1.ActiveView);
							(this.axPageLayoutControl1.ActiveView as IGraphicsContainerSelect).UnselectAllElements();
							this.axPageLayoutControl1.ActiveView.Refresh();
						}
						this.m_pLastSelect = this.treeView1.SelectedNode.Parent.Parent;
						if (this.treeView1.SelectedNode.Tag is MapTemplateElement)
						{
							IGraphicsContainerSelect graphicsContainer = this.axPageLayoutControl1.ActiveView.GraphicsContainer as IGraphicsContainerSelect;
							ISelection elementSelection = (this.axPageLayoutControl1.ActiveView as IViewManager).ElementSelection;
							this.axPageLayoutControl1.ActiveView.Selection = elementSelection;
							graphicsContainer.UnselectAllElements();
							graphicsContainer.SelectElement((this.treeView1.SelectedNode.Tag as MapTemplateElement).Element);
							this.axPageLayoutControl1.ActiveView.Refresh();
						}
						MapTemplate.CurrentMapTemplate = this.m_pLastMapTemplate;
					}
					else if (!(e.Node.Tag is string))
					{
						if (this.m_pLastSelect != null)
						{
							this.DeleteAllElements(this.axPageLayoutControl1.ActiveView);
						}
						this.axPageLayoutControl1.ActiveView.Refresh();
						this.m_pLastSelect = null;
						this.m_pLastMapTemplate = null;
						MapTemplate.CurrentMapTemplate = this.m_pLastMapTemplate;
					}
					else
					{
						if ((e.Node.Text == "模板元素" ? false : !(e.Node.Text == "模板参数")))
						{
							if (this.m_pLastSelect != null)
							{
								this.DeleteAllElements(this.axPageLayoutControl1.ActiveView);
							}
							this.axPageLayoutControl1.ActiveView.Refresh();
							this.m_pLastSelect = null;
						}
						else
						{
							tag = e.Node.Parent.Tag as MapTemplate;
							if (this.m_pLastMapTemplate == tag)
							{
								(this.axPageLayoutControl1.ActiveView as IGraphicsContainerSelect).UnselectAllElements();
								this.axPageLayoutControl1.ActiveView.Refresh();
							}
							else
							{
								if (this.m_pLastSelect != null)
								{
									this.DeleteAllElements(this.axPageLayoutControl1.ActiveView);
								}
								this.m_pLastMapTemplate = tag;
								this.m_pLastMapTemplate.CreateDesignTK(this.axPageLayoutControl1.ActiveView);
								(this.axPageLayoutControl1.ActiveView as IGraphicsContainerSelect).UnselectAllElements();
								this.axPageLayoutControl1.ActiveView.Refresh();
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
	}
}