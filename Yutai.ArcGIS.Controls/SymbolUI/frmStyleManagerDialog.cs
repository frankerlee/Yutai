using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    [Guid("D80B76D7-E320-4d49-A984-60AFEBE6E374")]
    public class frmStyleManagerDialog : Form
    {
        private License _license = null;
        private BarAndDockingController barAndDockingController1;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarDockControl barDockControlTop;
        private BarManager barManager1;
        private SimpleButton btnClose;
        private SimpleButton btnStyleSet;
        private ComboBoxEdit comboBoxEdit;
        private IContainer components;
        private ContextMenu contextMenu1;
        private ImageList imageList1;
        private Label label7;
        private IArray m_boards = new ArrayClass();
        private string m_Category = "";
        private string m_ClassName = "";
        private ListViewItem m_HitItem = null;
        private IStyleGalleryItem m_HitStyleItem = null;
        private IStyleGallery m_pSG = null;
        private string m_StyleSet = "";
        private MenuItem menuItem4;
        private MenuItem menuItem5;
        private MenuItem menuItem9;
        private MenuItem menuItemAddStyleSet;
        private MenuItem menuItemCopy;
        private MenuItem menuItemCut;
        private MenuItem menuItemDelete;
        private MenuItem menuItemNew;
        private MenuItem menuItemNewFillRepRule;
        private MenuItem menuItemNewLineRepRule;
        private MenuItem menuItemNewPointRepRule;
        private MenuItem menuItemNewStyleSet;
        private MenuItem menuItemPaste;
        private MenuItem menuItemProperty;
        private PopupMenu popupMenu1;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private ContextMenu StyleSetManagerMenu;
        private TreeView SymbolLibTreeView;
        private SymbolListViewEx symbolListView1;

        public frmStyleManagerDialog()
        {
            if (!SymbolUILicenseProviderCheck.Check())
            {
                base.Close();
            }
            else
            {
                this.InitializeComponent();
                this.symbolListView1.FullRowSelect = true;
                this.symbolListView1.CanEditLabel = true;
                this.symbolListView1.OnValueChanged += new SymbolListViewEx.OnValueChangedHandler(this.symbolListView1_OnValueChanged);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnStyleSet_Click(object sender, EventArgs e)
        {
            if (this.m_pSG != null)
            {
                System.Drawing.Point pos = new System.Drawing.Point(this.btnStyleSet.Location.X, this.btnStyleSet.Location.Y + this.btnStyleSet.Height);
                IStyleGalleryStorage pSG = (IStyleGalleryStorage) this.m_pSG;
                for (int i = 0; i < pSG.FileCount; i++)
                {
                    string text = pSG.get_File(i);
                    int length = text.LastIndexOf(@"\");
                    if (length != -1)
                    {
                        text = text.Substring(length + 1);
                    }
                    length = text.LastIndexOf(".");
                    if (length != -1)
                    {
                        text = text.Substring(0, length);
                    }
                    MenuItem item = new MenuItem(text) {
                        Checked = true
                    };
                    item.Click += new EventHandler(this.item_Click);
                    this.StyleSetManagerMenu.MenuItems.Add(0, item);
                }
                pSG = null;
                this.StyleSetManagerMenu.Show(this, pos);
                if (this.StyleSetManagerMenu.MenuItems.Count != 2)
                {
                    MenuItem item2 = this.StyleSetManagerMenu.MenuItems[this.StyleSetManagerMenu.MenuItems.Count - 2];
                    MenuItem item3 = this.StyleSetManagerMenu.MenuItems[this.StyleSetManagerMenu.MenuItems.Count - 1];
                    this.StyleSetManagerMenu.MenuItems.Clear();
                    this.StyleSetManagerMenu.MenuItems.Add(0, item2);
                    this.StyleSetManagerMenu.MenuItems.Add(1, item3);
                }
            }
        }

        private void comboBoxEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_ClassName != "")
            {
                if (this.comboBoxEdit.SelectedIndex == 0)
                {
                    this.m_Category = "";
                }
                else
                {
                    this.m_Category = this.comboBoxEdit.Text;
                }
                this.ReadSymbol(this.m_ClassName, this.m_StyleSet);
            }
        }

        private void CreateSymbol()
        {
            IStyleGalleryItem si = new ServerStyleGalleryItemClass();
            object mapGrid = null;
            if (this.SymbolLibTreeView.SelectedNode != null)
            {
                switch (this.SymbolLibTreeView.SelectedNode.Text)
                {
                    case "Marker Symbols":
                        mapGrid = new MultiLayerMarkerSymbolClass();
                        ((IMultiLayerMarkerSymbol) mapGrid).AddLayer(new SimpleMarkerSymbolClass());
                        goto Label_0210;

                    case "Line Symbols":
                        mapGrid = new MultiLayerLineSymbolClass();
                        ((IMultiLayerLineSymbol) mapGrid).AddLayer(new SimpleLineSymbolClass());
                        goto Label_0210;

                    case "Fill Symbols":
                        mapGrid = new MultiLayerFillSymbolClass();
                        ((IMultiLayerFillSymbol) mapGrid).AddLayer(new SimpleFillSymbolClass());
                        goto Label_0210;

                    case "Text Symbols":
                        mapGrid = new TextSymbolClass();
                        goto Label_0210;

                    case "Borders":
                        mapGrid = new SymbolBorderClass();
                        goto Label_0210;

                    case "Backgrounds":
                        mapGrid = new SymbolBackgroundClass();
                        goto Label_0210;

                    case "Shadows":
                        mapGrid = new SymbolShadowClass();
                        goto Label_0210;

                    case "North Arrows":
                        mapGrid = new MarkerNorthArrowClass();
                        goto Label_0210;

                    case "Scale Bars":
                        mapGrid = new ScaleLineClass();
                        goto Label_0210;

                    case "Scale Texts":
                        mapGrid = new ScaleTextClass();
                        goto Label_0210;

                    case "Legend Items":
                        mapGrid = new HorizontalLegendItemClass();
                        goto Label_0210;

                    case "Labels":
                        mapGrid = new LabelStyleClass();
                        goto Label_0210;

                    case "Reference Systems":
                    {
                        frmMapGridType type = new frmMapGridType();
                        if (type.ShowDialog() == DialogResult.OK)
                        {
                            mapGrid = type.MapGrid;
                            goto Label_0210;
                        }
                        break;
                    }
                }
            }
            return;
        Label_0210:
            si.Item = mapGrid;
            si.Name = "新符号";
            si.Category = "Default";
            if (this.EditProperty1(si))
            {
                IStyleGalleryStorage pSG = (IStyleGalleryStorage) this.m_pSG;
                pSG.TargetFile = this.SymbolLibTreeView.SelectedNode.Parent.Text;
                this.m_pSG.AddItem(si);
                this.ReadSymbol(this.SymbolLibTreeView.SelectedNode.Text, this.SymbolLibTreeView.SelectedNode.Parent.Text);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.components != null)
                {
                    this.components.Dispose();
                }
                if (this._license != null)
                {
                    this._license.Dispose();
                }
                this._license = null;
            }
            base.Dispose(disposing);
        }

        private void EditProperty(IStyleGalleryItem si)
        {
            DialogResult none = DialogResult.None;
            if (si.Item is IRepresentationRuleItem)
            {
                frmRepresationRule rule = new frmRepresationRule();
                frmRepresationRule.m_pSG = this.m_pSG;
                rule.RepresentationRuleItem = si.Item as IRepresentationRuleItem;
                if (rule.ShowDialog() == DialogResult.OK)
                {
                    si.Item = rule.RepresentationRuleItem;
                    this.m_pSG.UpdateItem(si);
                    this.symbolListView1.UpdateItem(this.m_HitItem);
                }
            }
            else if (si.Item is ISymbol)
            {
                ISymbol item = (ISymbol) si.Item;
                if (item is IMarkerSymbol)
                {
                    frmPointSymbolEdit edit = new frmPointSymbolEdit {
                        m_pSG = this.m_pSG
                    };
                    edit.SetSymbol(item);
                    none = edit.ShowDialog();
                    item = edit.GetSymbol();
                }
                else if (item is ILineSymbol)
                {
                    frmLineSymbolEdit edit2 = new frmLineSymbolEdit {
                        m_pSG = this.m_pSG
                    };
                    edit2.SetSymbol(item);
                    none = edit2.ShowDialog();
                    item = edit2.GetSymbol();
                }
                else if (item is IFillSymbol)
                {
                    frmFillSymbolEdit edit3 = new frmFillSymbolEdit {
                        m_pSG = this.m_pSG
                    };
                    edit3.SetSymbol(item);
                    none = edit3.ShowDialog();
                    item = edit3.GetSymbol();
                }
                else if (item is ITextSymbol)
                {
                    frmTextSymbolEdit edit4 = new frmTextSymbolEdit {
                        m_pSG = this.m_pSG
                    };
                    edit4.SetSymbol(item);
                    none = edit4.ShowDialog();
                    item = edit4.GetSymbol();
                }
                if (none == DialogResult.OK)
                {
                    si.Item = item;
                    this.m_pSG.UpdateItem(si);
                    this.symbolListView1.UpdateItem(this.m_HitItem);
                }
            }
            else
            {
                frmElementProperty property;
                object obj2 = si.Item;
                if (obj2 is ISymbolBorder)
                {
                    property = new frmElementProperty();
                    BorderSymbolPropertyPage page = new BorderSymbolPropertyPage();
                    property.Text = "边界";
                    property.AddPage(page);
                    if (property.EditProperties(obj2))
                    {
                        si.Item = obj2;
                        this.m_pSG.UpdateItem(si);
                        this.symbolListView1.UpdateItem(this.m_HitItem);
                    }
                }
                else if (obj2 is ISymbolBackground)
                {
                    property = new frmElementProperty();
                    BackgroundSymbolPropertyPage page2 = new BackgroundSymbolPropertyPage();
                    property.Text = "背景";
                    property.AddPage(page2);
                    if (property.EditProperties(obj2))
                    {
                        si.Item = obj2;
                        this.m_pSG.UpdateItem(si);
                        this.symbolListView1.UpdateItem(this.m_HitItem);
                    }
                }
                else if (obj2 is ISymbolShadow)
                {
                    property = new frmElementProperty();
                    ShadowSymbolPropertyPage page3 = new ShadowSymbolPropertyPage();
                    property.Text = "阴影";
                    property.AddPage(page3);
                    if (property.EditProperties(obj2))
                    {
                        si.Item = obj2;
                        this.m_pSG.UpdateItem(si);
                        this.symbolListView1.UpdateItem(this.m_HitItem);
                    }
                }
                else if (obj2 is INorthArrow)
                {
                    property = new frmElementProperty();
                    NorthArrowPropertyPage page4 = new NorthArrowPropertyPage();
                    property.Text = "指北针";
                    property.AddPage(page4);
                    if (property.EditProperties(obj2))
                    {
                        si.Item = obj2;
                        this.m_pSG.UpdateItem(si);
                        this.symbolListView1.UpdateItem(this.m_HitItem);
                    }
                }
                else if (obj2 is ILabelStyle)
                {
                    property = new frmElementProperty {
                        Text = "标注属性"
                    };
                    LabelStylePropertyPage page5 = new LabelStylePropertyPage();
                    property.AddPage(page5);
                    if (property.EditProperties(obj2))
                    {
                        si.Item = obj2;
                        this.m_pSG.UpdateItem(si);
                        this.symbolListView1.UpdateItem(this.m_HitItem);
                    }
                }
                else
                {
                    IPropertyPage page6;
                    if (obj2 is IScaleBar)
                    {
                        property = new frmElementProperty {
                            Text = "比例尺"
                        };
                        page6 = new ScaleBarFormatPropertyPage();
                        property.AddPage(page6);
                        page6 = new ScaleAndUnitsPropertyPage();
                        property.AddPage(page6);
                        page6 = new NumberAndLabelPropertyPage();
                        property.AddPage(page6);
                        if (property.EditProperties(obj2))
                        {
                            si.Item = obj2;
                            this.m_pSG.UpdateItem(si);
                            this.symbolListView1.UpdateItem(this.m_HitItem);
                        }
                    }
                    else if (obj2 is IScaleText)
                    {
                        property = new frmElementProperty {
                            Text = "比例尺文本"
                        };
                        page6 = new ScaleTextTextPropertyPage();
                        property.AddPage(page6);
                        page6 = new ScaleTextFormatPropertyPage();
                        property.AddPage(page6);
                        if (property.EditProperties(obj2))
                        {
                            si.Item = obj2;
                            this.m_pSG.UpdateItem(si);
                            this.symbolListView1.UpdateItem(this.m_HitItem);
                        }
                    }
                    else if (obj2 is ILegendItem)
                    {
                        property = new frmElementProperty {
                            Text = "图例项"
                        };
                        page6 = new LegendItemArrangementPropertyPage();
                        property.AddPage(page6);
                        page6 = new LegendItemGeneralPropertyPage();
                        property.AddPage(page6);
                        if (property.EditProperties(obj2))
                        {
                            si.Item = obj2;
                            this.m_pSG.UpdateItem(si);
                            this.symbolListView1.UpdateItem(this.m_HitItem);
                        }
                    }
                    else if (obj2 is IMapGrid)
                    {
                        GridAxisPropertyPage.m_pSG = this.m_pSG;
                        property = new frmElementProperty {
                            Text = "参考系"
                        };
                        page6 = null;
                        if (obj2 is IProjectedGrid)
                        {
                            page6 = new GridAxisPropertyPage();
                            property.AddPage(page6);
                            page6 = new LabelFormatPropertyPage();
                            property.AddPage(page6);
                            page6 = new TickSymbolPropertyPage();
                            property.AddPage(page6);
                            page6 = new MeasureCoordinatePropertyPage();
                            property.AddPage(page6);
                            page6 = new MeasuredGridPropertyPage();
                            property.AddPage(page6);
                        }
                        else if (obj2 is IMgrsGrid)
                        {
                            page6 = new MGRSPropertyPage();
                            property.AddPage(page6);
                            page6 = new GridAxisPropertyPage();
                            property.AddPage(page6);
                            page6 = new LabelFormatPropertyPage();
                            property.AddPage(page6);
                            page6 = new TickSymbolPropertyPage();
                            property.AddPage(page6);
                        }
                        else if (obj2 is IGraticule)
                        {
                            page6 = new GridAxisPropertyPage();
                            property.AddPage(page6);
                            page6 = new GridInteriorLabelsPropertyPage();
                            property.AddPage(page6);
                            page6 = new LabelFormatPropertyPage();
                            property.AddPage(page6);
                            page6 = new TickSymbolPropertyPage();
                            property.AddPage(page6);
                            page6 = new GridHatchPropertyPage();
                            property.AddPage(page6);
                            page6 = new MeasuredGridPropertyPage();
                            property.AddPage(page6);
                        }
                        else if (obj2 is ICustomOverlayGrid)
                        {
                            page6 = new CustomOverlayGridPropertyPage();
                            property.AddPage(page6);
                            page6 = new GridAxisPropertyPage();
                            property.AddPage(page6);
                            page6 = new LabelFormatPropertyPage();
                            property.AddPage(page6);
                            page6 = new TickSymbolPropertyPage();
                            property.AddPage(page6);
                        }
                        else if (obj2 is IIndexGrid)
                        {
                            page6 = new GridAxisPropertyPage();
                            property.AddPage(page6);
                            page6 = new IndexGridProperyPage();
                            property.AddPage(page6);
                            page6 = new LabelFormatPropertyPage();
                            property.AddPage(page6);
                            page6 = new TickSymbolPropertyPage();
                            property.AddPage(page6);
                        }
                        if (property.EditProperties(obj2))
                        {
                            si.Item = obj2;
                            this.m_pSG.UpdateItem(si);
                            this.symbolListView1.UpdateItem(this.m_HitItem);
                        }
                    }
                }
            }
            this.symbolListView1.Invalidate();
        }

        private bool EditProperty1(IStyleGalleryItem si)
        {
            DialogResult none = DialogResult.None;
            if (si.Item is ISymbol)
            {
                ISymbol item = (ISymbol) si.Item;
                if (item is IMarkerSymbol)
                {
                    frmPointSymbolEdit edit = new frmPointSymbolEdit {
                        m_pSG = this.m_pSG
                    };
                    edit.SetSymbol(item);
                    none = edit.ShowDialog();
                    item = edit.GetSymbol();
                }
                else if (item is ILineSymbol)
                {
                    frmLineSymbolEdit edit2 = new frmLineSymbolEdit {
                        m_pSG = this.m_pSG
                    };
                    edit2.SetSymbol(item);
                    none = edit2.ShowDialog();
                    item = edit2.GetSymbol();
                }
                else if (item is IFillSymbol)
                {
                    frmFillSymbolEdit edit3 = new frmFillSymbolEdit {
                        m_pSG = this.m_pSG
                    };
                    edit3.SetSymbol(item);
                    none = edit3.ShowDialog();
                    item = edit3.GetSymbol();
                }
                else if (item is ITextSymbol)
                {
                    frmTextSymbolEdit edit4 = new frmTextSymbolEdit {
                        m_pSG = this.m_pSG
                    };
                    edit4.SetSymbol(item);
                    none = edit4.ShowDialog();
                    item = edit4.GetSymbol();
                }
                if (none == DialogResult.OK)
                {
                    try
                    {
                        si.Item = item;
                    }
                    catch
                    {
                    }
                    return true;
                }
            }
            else
            {
                frmElementProperty property;
                object obj2 = si.Item;
                if (obj2 is ISymbolBorder)
                {
                    property = new frmElementProperty();
                    BorderSymbolPropertyPage page = new BorderSymbolPropertyPage();
                    property.Text = "边界";
                    property.AddPage(page);
                    if (property.EditProperties(obj2))
                    {
                        si.Item = obj2;
                        return true;
                    }
                }
                else if (obj2 is ISymbolBackground)
                {
                    property = new frmElementProperty();
                    BackgroundSymbolPropertyPage page2 = new BackgroundSymbolPropertyPage();
                    property.Text = "背景";
                    property.AddPage(page2);
                    if (property.EditProperties(obj2))
                    {
                        si.Item = obj2;
                        return true;
                    }
                }
                else if (obj2 is ISymbolShadow)
                {
                    property = new frmElementProperty();
                    ShadowSymbolPropertyPage page3 = new ShadowSymbolPropertyPage();
                    property.Text = "阴影";
                    property.AddPage(page3);
                    if (property.EditProperties(obj2))
                    {
                        si.Item = obj2;
                        return true;
                    }
                }
                else if (obj2 is INorthArrow)
                {
                    property = new frmElementProperty();
                    NorthArrowPropertyPage page4 = new NorthArrowPropertyPage();
                    property.Text = "指北针";
                    property.AddPage(page4);
                    if (property.EditProperties(obj2))
                    {
                        si.Item = obj2;
                        return true;
                    }
                }
                else
                {
                    IPropertyPage page5;
                    if (obj2 is IScaleBar)
                    {
                        property = new frmElementProperty {
                            Text = "比例尺"
                        };
                        page5 = new ScaleBarFormatPropertyPage();
                        property.AddPage(page5);
                        page5 = new ScaleAndUnitsPropertyPage();
                        property.AddPage(page5);
                        page5 = new NumberAndLabelPropertyPage();
                        property.AddPage(page5);
                        if (property.EditProperties(obj2))
                        {
                            si.Item = obj2;
                            return true;
                        }
                    }
                    else if (obj2 is ILabelStyle)
                    {
                        property = new frmElementProperty {
                            Text = "标注属性"
                        };
                        LabelStylePropertyPage page6 = new LabelStylePropertyPage();
                        property.AddPage(page6);
                        if (property.EditProperties(obj2))
                        {
                            si.Item = obj2;
                            this.m_pSG.UpdateItem(si);
                        }
                    }
                    else if (obj2 is IScaleText)
                    {
                        property = new frmElementProperty {
                            Text = "比例尺文本"
                        };
                        page5 = new ScaleTextTextPropertyPage();
                        property.AddPage(page5);
                        page5 = new ScaleTextFormatPropertyPage();
                        property.AddPage(page5);
                        if (property.EditProperties(obj2))
                        {
                            si.Item = obj2;
                            return true;
                        }
                    }
                    else if (obj2 is ILegendItem)
                    {
                        property = new frmElementProperty {
                            Text = "图例项"
                        };
                        page5 = new LegendItemArrangementPropertyPage();
                        property.AddPage(page5);
                        page5 = new LegendItemGeneralPropertyPage();
                        property.AddPage(page5);
                        if (property.EditProperties(obj2))
                        {
                            si.Item = obj2;
                            return true;
                        }
                    }
                    else if (obj2 is IMapGrid)
                    {
                        GridAxisPropertyPage.m_pSG = this.m_pSG;
                        property = new frmElementProperty {
                            Text = "参考系"
                        };
                        page5 = null;
                        if (obj2 is IProjectedGrid)
                        {
                            page5 = new GridAxisPropertyPage();
                            property.AddPage(page5);
                            page5 = new LabelFormatPropertyPage();
                            property.AddPage(page5);
                            page5 = new TickSymbolPropertyPage();
                            property.AddPage(page5);
                            page5 = new MeasureCoordinatePropertyPage();
                            property.AddPage(page5);
                            page5 = new MeasuredGridPropertyPage();
                            property.AddPage(page5);
                        }
                        else if (obj2 is IMgrsGrid)
                        {
                            page5 = new MGRSPropertyPage();
                            property.AddPage(page5);
                            page5 = new GridAxisPropertyPage();
                            property.AddPage(page5);
                            page5 = new LabelFormatPropertyPage();
                            property.AddPage(page5);
                            page5 = new TickSymbolPropertyPage();
                            property.AddPage(page5);
                        }
                        else if (obj2 is IGraticule)
                        {
                            page5 = new GridAxisPropertyPage();
                            property.AddPage(page5);
                            page5 = new GridInteriorLabelsPropertyPage();
                            property.AddPage(page5);
                            page5 = new LabelFormatPropertyPage();
                            property.AddPage(page5);
                            page5 = new TickSymbolPropertyPage();
                            property.AddPage(page5);
                            page5 = new GridHatchPropertyPage();
                            property.AddPage(page5);
                            page5 = new MeasuredGridPropertyPage();
                            property.AddPage(page5);
                        }
                        else if (obj2 is ICustomOverlayGrid)
                        {
                            page5 = new CustomOverlayGridPropertyPage();
                            property.AddPage(page5);
                            page5 = new GridAxisPropertyPage();
                            property.AddPage(page5);
                            page5 = new LabelFormatPropertyPage();
                            property.AddPage(page5);
                            page5 = new TickSymbolPropertyPage();
                            property.AddPage(page5);
                        }
                        else if (obj2 is IIndexGrid)
                        {
                            page5 = new GridAxisPropertyPage();
                            property.AddPage(page5);
                            page5 = new IndexGridProperyPage();
                            property.AddPage(page5);
                            page5 = new LabelFormatPropertyPage();
                            property.AddPage(page5);
                            page5 = new TickSymbolPropertyPage();
                            property.AddPage(page5);
                        }
                        if (property.EditProperties(obj2))
                        {
                            si.Item = obj2;
                            this.m_pSG.UpdateItem(si);
                        }
                    }
                }
            }
            return false;
        }

        private void frmStyleManagerDialog_Load(object sender, EventArgs e)
        {
            if (this.m_pSG != null)
            {
                this.comboBoxEdit.Properties.Items.Add("全部");
                this.symbolListView1.MultiSelect = true;
                IStyleGalleryStorage pSG = (IStyleGalleryStorage) this.m_pSG;
                for (int i = 0; i < pSG.FileCount; i++)
                {
                    TreeNode pParentNode = new TreeNode(pSG.get_File(i)) {
                        Tag = 0
                    };
                    this.InsertStyleClassToNode(pParentNode, this.m_pSG);
                    if (i == 0)
                    {
                        pParentNode.Expand();
                    }
                    this.SymbolLibTreeView.Nodes.Add(pParentNode);
                }
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStyleManagerDialog));
            this.SymbolLibTreeView = new TreeView();
            this.imageList1 = new ImageList(this.components);
            this.btnClose = new SimpleButton();
            this.contextMenu1 = new ContextMenu();
            this.menuItemNew = new MenuItem();
            this.menuItem4 = new MenuItem();
            this.menuItemCut = new MenuItem();
            this.menuItemCopy = new MenuItem();
            this.menuItemPaste = new MenuItem();
            this.menuItem5 = new MenuItem();
            this.menuItemDelete = new MenuItem();
            this.menuItem9 = new MenuItem();
            this.menuItemProperty = new MenuItem();
            this.menuItemNewPointRepRule = new MenuItem();
            this.menuItemNewLineRepRule = new MenuItem();
            this.menuItemNewFillRepRule = new MenuItem();
            this.btnStyleSet = new SimpleButton();
            this.StyleSetManagerMenu = new ContextMenu();
            this.menuItemNewStyleSet = new MenuItem();
            this.menuItemAddStyleSet = new MenuItem();
            this.symbolListView1 = new SymbolListViewEx();
            this.radioButton1 = new RadioButton();
            this.radioButton2 = new RadioButton();
            this.radioButton3 = new RadioButton();
            this.barManager1 = new BarManager(this.components);
            this.barAndDockingController1 = new BarAndDockingController(this.components);
            this.barDockControlTop = new BarDockControl();
            this.barDockControlBottom = new BarDockControl();
            this.barDockControlLeft = new BarDockControl();
            this.barDockControlRight = new BarDockControl();
            this.popupMenu1 = new PopupMenu(this.components);
            this.label7 = new Label();
            this.comboBoxEdit = new ComboBoxEdit();
            this.barManager1.BeginInit();
            this.barAndDockingController1.BeginInit();
            this.popupMenu1.BeginInit();
            this.comboBoxEdit.Properties.BeginInit();
            base.SuspendLayout();
            this.SymbolLibTreeView.HideSelection = false;
            this.SymbolLibTreeView.ImageIndex = 0;
            this.SymbolLibTreeView.ImageList = this.imageList1;
            this.SymbolLibTreeView.Location = new System.Drawing.Point(1, 8);
            this.SymbolLibTreeView.Name = "SymbolLibTreeView";
            this.SymbolLibTreeView.SelectedImageIndex = 1;
            this.SymbolLibTreeView.Size = new Size(0xc0, 0x170);
            this.SymbolLibTreeView.TabIndex = 0;
            this.SymbolLibTreeView.AfterSelect += new TreeViewEventHandler(this.SymbolLibTreeView_AfterSelect);
            this.imageList1.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.btnClose.Location = new System.Drawing.Point(0x1a8, 0x160);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x30, 0x18);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.contextMenu1.MenuItems.AddRange(new MenuItem[] { this.menuItemNew, this.menuItem4, this.menuItemCut, this.menuItemCopy, this.menuItemPaste, this.menuItem5, this.menuItemDelete, this.menuItem9, this.menuItemProperty });
            this.menuItemNew.Index = 0;
            this.menuItemNew.Text = "新建";
            this.menuItemNew.Click += new EventHandler(this.menuItemNew_Click);
            this.menuItem4.Index = 1;
            this.menuItem4.Text = "-";
            this.menuItemCut.Index = 2;
            this.menuItemCut.Text = "剪切";
            this.menuItemCut.Click += new EventHandler(this.menuItemCut_Click);
            this.menuItemCopy.Index = 3;
            this.menuItemCopy.Text = "拷贝";
            this.menuItemCopy.Click += new EventHandler(this.menuItemCopy_Click);
            this.menuItemPaste.Enabled = false;
            this.menuItemPaste.Index = 4;
            this.menuItemPaste.Text = "粘贴";
            this.menuItemPaste.Click += new EventHandler(this.menuItemPaste_Click);
            this.menuItem5.Index = 5;
            this.menuItem5.Text = "-";
            this.menuItemDelete.Index = 6;
            this.menuItemDelete.Text = "删除";
            this.menuItemDelete.Click += new EventHandler(this.menuItemDelete_Click);
            this.menuItem9.Index = 7;
            this.menuItem9.Text = "-";
            this.menuItemProperty.Index = 8;
            this.menuItemProperty.Text = "属性";
            this.menuItemProperty.Click += new EventHandler(this.menuItemProperty_Click);
            this.menuItemNewPointRepRule.Index = -1;
            this.menuItemNewPointRepRule.Text = "点表现规则";
            this.menuItemNewPointRepRule.Click += new EventHandler(this.menuItemNewPointRepRule_Click);
            this.menuItemNewLineRepRule.Index = -1;
            this.menuItemNewLineRepRule.Text = "线表现规则";
            this.menuItemNewLineRepRule.Click += new EventHandler(this.menuItemNewLineRepRule_Click);
            this.menuItemNewFillRepRule.Index = -1;
            this.menuItemNewFillRepRule.Text = "面表现规则";
            this.menuItemNewFillRepRule.Click += new EventHandler(this.menuItemNewFillRepRule_Click);
            this.btnStyleSet.Location = new System.Drawing.Point(0x170, 0x160);
            this.btnStyleSet.Name = "btnStyleSet";
            this.btnStyleSet.Size = new Size(0x30, 0x18);
            this.btnStyleSet.TabIndex = 3;
            this.btnStyleSet.Text = "样式";
            this.btnStyleSet.Click += new EventHandler(this.btnStyleSet_Click);
            this.StyleSetManagerMenu.MenuItems.AddRange(new MenuItem[] { this.menuItemNewStyleSet, this.menuItemAddStyleSet });
            this.menuItemNewStyleSet.Index = 0;
            this.menuItemNewStyleSet.Text = "新建";
            this.menuItemNewStyleSet.Click += new EventHandler(this.menuItemNewStyleSet_Click);
            this.menuItemAddStyleSet.Index = 1;
            this.menuItemAddStyleSet.Text = "添加";
            this.menuItemAddStyleSet.Click += new EventHandler(this.menuItemAddStyleSet_Click);
            this.symbolListView1.BackColor = SystemColors.Window;
            this.symbolListView1.CanEditLabel = true;
            this.symbolListView1.Location = new System.Drawing.Point(200, 0x38);
            this.symbolListView1.Name = "symbolListView1";
            this.symbolListView1.Size = new Size(300, 0x120);
            this.symbolListView1.TabIndex = 4;
            this.symbolListView1.UseCompatibleStateImageBehavior = false;
            this.symbolListView1.DoubleClick += new EventHandler(this.symbolListView1_DoubleClick);
            this.symbolListView1.MouseUp += new MouseEventHandler(this.symbolListView1_MouseUp);
            this.radioButton1.Appearance = Appearance.Button;
            this.radioButton1.Checked = true;
            this.radioButton1.FlatStyle = FlatStyle.Popup;
            this.radioButton1.ImageIndex = 2;
            this.radioButton1.ImageList = this.imageList1;
            this.radioButton1.Location = new System.Drawing.Point(0xe0, 0x160);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new Size(0x18, 0x18);
            this.radioButton1.TabIndex = 5;
            this.radioButton1.TabStop = true;
            this.radioButton1.Click += new EventHandler(this.radioButton1_Click);
            this.radioButton2.Appearance = Appearance.Button;
            this.radioButton2.FlatStyle = FlatStyle.Popup;
            this.radioButton2.ImageIndex = 3;
            this.radioButton2.ImageList = this.imageList1;
            this.radioButton2.Location = new System.Drawing.Point(0xf8, 0x160);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new Size(0x18, 0x18);
            this.radioButton2.TabIndex = 6;
            this.radioButton2.Click += new EventHandler(this.radioButton2_Click);
            this.radioButton3.Appearance = Appearance.Button;
            this.radioButton3.FlatStyle = FlatStyle.Popup;
            this.radioButton3.ImageIndex = 4;
            this.radioButton3.ImageList = this.imageList1;
            this.radioButton3.Location = new System.Drawing.Point(0x110, 0x160);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new Size(0x18, 0x18);
            this.radioButton3.TabIndex = 7;
            this.radioButton3.Click += new EventHandler(this.radioButton3_Click);
            this.barManager1.Controller = this.barAndDockingController1;
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.MaxItemId = 0;
            this.barAndDockingController1.PaintStyleName = "Office2003";
            this.barAndDockingController1.PropertiesBar.AllowLinkLighting = false;
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(0xca, 0x10);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x23, 12);
            this.label7.TabIndex = 20;
            this.label7.Text = "类别:";
            this.comboBoxEdit.EditValue = "";
            this.comboBoxEdit.Location = new System.Drawing.Point(250, 14);
            this.comboBoxEdit.Name = "comboBoxEdit";
            this.comboBoxEdit.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.comboBoxEdit.Size = new Size(230, 0x15);
            this.comboBoxEdit.TabIndex = 0x13;
            this.comboBoxEdit.SelectedIndexChanged += new EventHandler(this.comboBoxEdit_SelectedIndexChanged);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x1f9, 0x187);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.radioButton1);
            base.Controls.Add(this.comboBoxEdit);
            base.Controls.Add(this.radioButton3);
            base.Controls.Add(this.radioButton2);
            base.Controls.Add(this.symbolListView1);
            base.Controls.Add(this.btnStyleSet);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.SymbolLibTreeView);
            base.Controls.Add(this.barDockControlLeft);
            base.Controls.Add(this.barDockControlRight);
            base.Controls.Add(this.barDockControlBottom);
            base.Controls.Add(this.barDockControlTop);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) resources.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmStyleManagerDialog";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "符号库管理器";
            base.Load += new EventHandler(this.frmStyleManagerDialog_Load);
            this.barManager1.EndInit();
            this.barAndDockingController1.EndInit();
            this.popupMenu1.EndInit();
            this.comboBoxEdit.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InsertStyleClassToNode(TreeNode pParentNode, IStyleGallery pSG)
        {
            for (int i = 0; i < pSG.ClassCount; i++)
            {
                TreeNode node = new TreeNode(pSG.get_Class(i).Name) {
                    Tag = 1
                };
                pParentNode.Nodes.Add(node);
            }
        }

        private void item_Click(object sender, EventArgs e)
        {
            if (sender is MenuItem)
            {
                IStyleGalleryStorage pSG = (IStyleGalleryStorage) this.m_pSG;
                if (pSG.FileCount != 1)
                {
                    for (int i = 0; i < pSG.FileCount; i++)
                    {
                        string path = pSG.get_File(i);
                        int length = path.LastIndexOf(@"\");
                        if (length != -1)
                        {
                            path = path.Substring(length + 1);
                        }
                        length = path.LastIndexOf(".");
                        if (length != -1)
                        {
                            path = path.Substring(0, length);
                        }
                        if (path == ((MenuItem) sender).Text)
                        {
                            path = pSG.get_File(i);
                            for (int j = 0; j < this.SymbolLibTreeView.Nodes.Count; j++)
                            {
                                if (this.SymbolLibTreeView.Nodes[j].Text == path)
                                {
                                    this.SymbolLibTreeView.Nodes.RemoveAt(j);
                                    break;
                                }
                            }
                            pSG.RemoveFile(path);
                            pSG = null;
                            break;
                        }
                    }
                }
            }
        }

        private void menuItemAddStyleSet_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (this.m_pSG is ESRI.ArcGIS.esriSystem.IPersistStream)
            {
                dialog.Filter = "符号库 (*.ServerStyle)|*.ServerStyle";
            }
            else
            {
                dialog.Filter = "符号库 (*.Style)|*.Style";
            }
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                IStyleGalleryStorage pSG = (IStyleGalleryStorage) this.m_pSG;
                int fileCount = pSG.FileCount;
                pSG.AddFile(dialog.FileName);
                if (fileCount == pSG.FileCount)
                {
                    pSG = null;
                }
                else
                {
                    pSG = null;
                    TreeNode pParentNode = new TreeNode(dialog.FileName) {
                        Tag = 0
                    };
                    this.InsertStyleClassToNode(pParentNode, this.m_pSG);
                    this.SymbolLibTreeView.Nodes.Add(pParentNode);
                }
            }
        }

        private void menuItemCopy_Click(object sender, EventArgs e)
        {
            this.m_boards.RemoveAll();
            IList selectedItems = this.symbolListView1.SelectedItems;
            for (int i = 0; i < selectedItems.Count; i++)
            {
                this.m_boards.Add(((IClone) (selectedItems[i] as ListViewItem).Tag).Clone());
            }
        }

        private void menuItemCut_Click(object sender, EventArgs e)
        {
            int num;
            this.m_boards.RemoveAll();
            IList selectedItems = this.symbolListView1.SelectedItems;
            for (num = 0; num < selectedItems.Count; num++)
            {
                this.m_boards.Add(((IClone) (selectedItems[num] as ListViewItem).Tag).Clone());
            }
            for (num = 0; num < selectedItems.Count; num++)
            {
                this.m_pSG.RemoveItem((IStyleGalleryItem) selectedItems[num]);
            }
            this.symbolListView1.DeleteSelectItem();
            this.symbolListView1.Invalidate();
        }

        private void menuItemDelete_Click(object sender, EventArgs e)
        {
            IList selectedItems = this.symbolListView1.SelectedItems;
            for (int i = 0; i < selectedItems.Count; i++)
            {
                this.m_pSG.RemoveItem((IStyleGalleryItem) (selectedItems[i] as ListViewItem).Tag);
            }
            this.symbolListView1.DeleteSelectItem();
            this.symbolListView1.Invalidate();
        }

        private void menuItemNew_Click(object sender, EventArgs e)
        {
            this.CreateSymbol();
        }

        private void menuItemNewFillRepRule_Click(object sender, EventArgs e)
        {
            IRepresentationRuleItem item = new RepresentationRuleItemClass {
                GeometryType = esriGeometryType.esriGeometryPolygon
            };
            IRepresentationRule rule = new RepresentationRuleClass();
            IBasicFillSymbol symbol = new BasicFillSymbolClass();
            rule.InsertLayer(0, symbol as IBasicSymbol);
            item.RepresentationRule = rule;
            frmRepresationRule rule2 = new frmRepresationRule();
            frmRepresationRule.m_pSG = this.m_pSG;
            rule2.RepresentationRuleItem = item;
            if (rule2.ShowDialog() == DialogResult.OK)
            {
                IStyleGalleryItem item2 = new ServerStyleGalleryItemClass {
                    Item = rule2.RepresentationRuleItem,
                    Name = "新面制图表现",
                    Category = "Default"
                };
                IStyleGalleryStorage pSG = (IStyleGalleryStorage) this.m_pSG;
                pSG.TargetFile = this.SymbolLibTreeView.SelectedNode.Parent.Text;
                this.m_pSG.AddItem(item2);
                this.ReadSymbol(this.SymbolLibTreeView.SelectedNode.Text, this.SymbolLibTreeView.SelectedNode.Parent.Text);
            }
        }

        private void menuItemNewLineRepRule_Click(object sender, EventArgs e)
        {
            IRepresentationRuleItem item = new RepresentationRuleItemClass {
                GeometryType = esriGeometryType.esriGeometryPolyline
            };
            IRepresentationRule rule = new RepresentationRuleClass();
            IBasicLineSymbol symbol = new BasicLineSymbolClass();
            rule.InsertLayer(0, symbol as IBasicSymbol);
            item.RepresentationRule = rule;
            frmRepresationRule rule2 = new frmRepresationRule();
            frmRepresationRule.m_pSG = this.m_pSG;
            rule2.RepresentationRuleItem = item;
            if (rule2.ShowDialog() == DialogResult.OK)
            {
                IStyleGalleryItem item2 = new ServerStyleGalleryItemClass {
                    Item = rule2.RepresentationRuleItem,
                    Name = "新线制图表现",
                    Category = "Default"
                };
                IStyleGalleryStorage pSG = (IStyleGalleryStorage) this.m_pSG;
                pSG.TargetFile = this.SymbolLibTreeView.SelectedNode.Parent.Text;
                this.m_pSG.AddItem(item2);
                this.ReadSymbol(this.SymbolLibTreeView.SelectedNode.Text, this.SymbolLibTreeView.SelectedNode.Parent.Text);
            }
        }

        private void menuItemNewPointRepRule_Click(object sender, EventArgs e)
        {
            IRepresentationRuleItem item = new RepresentationRuleItemClass {
                GeometryType = esriGeometryType.esriGeometryPoint
            };
            IRepresentationRule rule = new RepresentationRuleClass();
            IBasicMarkerSymbol symbol = new BasicMarkerSymbolClass();
            rule.InsertLayer(0, symbol as IBasicSymbol);
            item.RepresentationRule = rule;
            frmRepresationRule rule2 = new frmRepresationRule();
            frmRepresationRule.m_pSG = this.m_pSG;
            rule2.RepresentationRuleItem = item;
            if (rule2.ShowDialog() == DialogResult.OK)
            {
                IStyleGalleryItem item2 = new ServerStyleGalleryItemClass {
                    Item = rule2.RepresentationRuleItem,
                    Name = "新点制图表现",
                    Category = "Default"
                };
                IStyleGalleryStorage pSG = (IStyleGalleryStorage) this.m_pSG;
                pSG.TargetFile = this.SymbolLibTreeView.SelectedNode.Parent.Text;
                this.m_pSG.AddItem(item2);
                this.ReadSymbol(this.SymbolLibTreeView.SelectedNode.Text, this.SymbolLibTreeView.SelectedNode.Parent.Text);
            }
        }

        private void menuItemNewStyleSet_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (this.m_pSG is ESRI.ArcGIS.esriSystem.IPersistStream)
            {
                dialog.Filter = "符号库 (*.ServerStyle)|*.ServerStyle";
            }
            else
            {
                dialog.Filter = "符号库 (*.Style)|*.Style";
            }
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Stream stream = dialog.OpenFile();
                if (stream != null)
                {
                    Stream manifestResourceStream;
                    int num;
                    if (this.m_pSG is ESRI.ArcGIS.esriSystem.IPersistStream)
                    {
                        manifestResourceStream = base.GetType().Assembly.GetManifestResourceStream("JLK.Controls.SymbolUI.template.ServerStyle");
                    }
                    else
                    {
                        manifestResourceStream = base.GetType().Assembly.GetManifestResourceStream("JLK.Controls.SymbolUI.template.style");
                    }
                    byte[] buffer = new byte[0x1000];
                    while ((num = manifestResourceStream.Read(buffer, 0, 0x1000)) > 0)
                    {
                        stream.Write(buffer, 0, num);
                    }
                    stream.Close();
                }
                ((IStyleGalleryStorage) this.m_pSG).AddFile(dialog.FileName);
                TreeNode pParentNode = new TreeNode(dialog.FileName) {
                    Tag = 0
                };
                this.InsertStyleClassToNode(pParentNode, this.m_pSG);
                this.SymbolLibTreeView.Nodes.Add(pParentNode);
            }
        }

        private void menuItemPaste_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.m_boards.Count; i++)
            {
                IStyleGalleryItem item = (IStyleGalleryItem) this.m_boards.get_Element(i);
                IStyleGalleryItem item2 = new ServerStyleGalleryItemClass {
                    Item = ((IClone) item.Item).Clone(),
                    Category = item.Category,
                    Name = "拷贝" + item.Name
                };
                this.m_pSG.AddItem(item2);
                this.symbolListView1.Add(item2);
            }
            this.symbolListView1.Invalidate();
        }

        private void menuItemProperty_Click(object sender, EventArgs e)
        {
            this.EditProperty(this.m_HitStyleItem);
        }

        private void menuItemRename_Click(object sender, EventArgs e)
        {
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (this.radioButton1.Checked)
            {
                this.symbolListView1.View = View.LargeIcon;
            }
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            if (this.radioButton2.Checked)
            {
                this.symbolListView1.View = View.List;
            }
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            if (this.radioButton3.Checked)
            {
                this.symbolListView1.View = View.Details;
            }
        }

        private void ReadCategories(string className)
        {
            IEnumBSTR mbstr = this.m_pSG.get_Categories(className);
            mbstr.Reset();
            for (string str = mbstr.Next(); str != null; str = mbstr.Next())
            {
                this.comboBoxEdit.Properties.Items.Add(str);
            }
        }

        private void ReadSymbol(string ClassName, string styleSet)
        {
            try
            {
                this.symbolListView1.RemoveAll();
                IEnumStyleGalleryItem item = null;
                switch (ClassName)
                {
                    case "Marker Symbols":
                        this.symbolListView1.SetItemSize(0x10, 0x10, 50, 50);
                        break;

                    case "Line Symbols":
                        this.symbolListView1.SetItemSize(20, 0x10, 50, 50);
                        break;

                    case "Fill Symbols":
                        this.symbolListView1.SetItemSize(20, 20, 50, 50);
                        break;

                    case "Color Ramps":
                        this.symbolListView1.SetItemSize(40, 20, 160, 0x23);
                        break;

                    case "Colors":
                        this.symbolListView1.SetItemSize(20, 20, 50, 50);
                        break;

                    case "Text Symbols":
                        this.symbolListView1.SetItemSize(50, 20, 230, 50);
                        break;

                    case "Line Patches":
                        this.symbolListView1.SetItemSize(20, 20, 70, 50);
                        break;

                    case "Area Patches":
                        this.symbolListView1.SetItemSize(20, 20, 70, 70);
                        break;

                    case "North Arrows":
                        this.symbolListView1.SetItemSize(20, 20, 50, 50);
                        break;

                    case "Scale Bars":
                        this.symbolListView1.SetItemSize(50, 20, 180, 40);
                        break;

                    case "Scale Texts":
                        this.symbolListView1.SetItemSize(50, 20, 180, 50);
                        break;

                    case "Borders":
                        this.symbolListView1.SetItemSize(50, 20, 230, 50);
                        break;

                    case "Backgrounds":
                        this.symbolListView1.SetItemSize(20, 20, 70, 70);
                        break;

                    case "Shadows":
                        this.symbolListView1.SetItemSize(20, 20, 70, 70);
                        break;

                    case "Legend Items":
                        this.symbolListView1.SetItemSize(20, 20, 180, 50);
                        break;

                    case "Reference Systems":
                        this.symbolListView1.SetItemSize(20, 20, 180, 50);
                        break;

                    case "Labels":
                        this.symbolListView1.SetItemSize(50, 20, 180, 50);
                        break;

                    case "Representation Markers":
                        this.symbolListView1.SetItemSize(20, 20, 50, 50);
                        break;

                    case "Representation Rules":
                        this.symbolListView1.SetItemSize(0x10, 0x10, 50, 50);
                        break;

                    default:
                        return;
                }
                ((IStyleGalleryStorage) this.m_pSG).TargetFile = styleSet;
                item = this.m_pSG.get_Items(ClassName, styleSet, this.m_Category);
                if (item != null)
                {
                    item.Reset();
                    for (IStyleGalleryItem item2 = item.Next(); item2 != null; item2 = item.Next())
                    {
                        this.symbolListView1.Add(item2);
                    }
                    item = null;
                    GC.Collect();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void SetStyleGallery(IStyleGallery pSG)
        {
            this.m_pSG = pSG;
        }

        private void SymbolLibTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.comboBoxEdit.Properties.Items.Clear();
            this.comboBoxEdit.Properties.Items.Add("全部");
            this.comboBoxEdit.SelectedIndex = 0;
            this.m_Category = "";
            if (((int) e.Node.Tag) == 1)
            {
                IStyleGalleryStorage pSG = (IStyleGalleryStorage) this.m_pSG;
                pSG.TargetFile = e.Node.Parent.Text;
                pSG = null;
                this.ReadSymbol(e.Node.Text, e.Node.Parent.Text);
                this.ReadCategories(e.Node.Text);
                this.m_ClassName = e.Node.Text;
                this.m_StyleSet = e.Node.Parent.Text;
                this.symbolListView1.Invalidate();
                if (e.Node.Text == "Representation Rules")
                {
                    this.menuItemNew.MenuItems.Add(this.menuItemNewPointRepRule);
                    this.menuItemNew.MenuItems.Add(this.menuItemNewLineRepRule);
                    this.menuItemNew.MenuItems.Add(this.menuItemNewFillRepRule);
                }
                else
                {
                    this.menuItemNew.MenuItems.Clear();
                }
            }
            else
            {
                this.symbolListView1.RemoveAll();
                this.symbolListView1.Invalidate();
                this.m_ClassName = "";
            }
        }

        private void symbolListView1_DoubleClick(object sender, EventArgs e)
        {
        }

        private void symbolListView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.menuItemPaste.Enabled = false;
                if (this.m_boards.Count > 0)
                {
                    object item = ((IStyleGalleryItem) this.m_boards.get_Element(0)).Item;
                    switch (this.SymbolLibTreeView.SelectedNode.Text)
                    {
                        case "Marker Symbols":
                            if (item is IMarkerSymbol)
                            {
                                this.menuItemPaste.Enabled = true;
                            }
                            break;

                        case "Line Symbols":
                            if (item is ILineSymbol)
                            {
                                this.menuItemPaste.Enabled = true;
                            }
                            break;

                        case "Fill Symbols":
                            if (item is IFillSymbol)
                            {
                                this.menuItemPaste.Enabled = true;
                            }
                            break;

                        case "Text Symbols":
                            if (item is ITextSymbol)
                            {
                                this.menuItemPaste.Enabled = true;
                            }
                            break;

                        case "Reference Systems":
                            if (item is IMapGrid)
                            {
                                this.menuItemPaste.Enabled = true;
                            }
                            break;

                        case "Colors":
                            if (item is IColor)
                            {
                                this.menuItemPaste.Enabled = true;
                            }
                            break;

                        case "Color Ramps":
                            if (item is IColorRamp)
                            {
                                this.menuItemPaste.Enabled = true;
                            }
                            break;

                        case "North Arrows":
                            if (item is INorthArrow)
                            {
                                this.menuItemPaste.Enabled = true;
                            }
                            break;

                        case "Scale Bars":
                            if (item is IScaleBar)
                            {
                                this.menuItemPaste.Enabled = true;
                            }
                            break;

                        case "Scale Texts":
                            if (item is IScaleText)
                            {
                                this.menuItemPaste.Enabled = true;
                            }
                            break;

                        case "Line Patches":
                            if (item is ILinePatch)
                            {
                                this.menuItemPaste.Enabled = true;
                            }
                            break;

                        case "Area Patches":
                            if (item is IAreaPatch)
                            {
                                this.menuItemPaste.Enabled = true;
                            }
                            break;

                        case "Borders":
                            if (item is ISymbolBorder)
                            {
                                this.menuItemPaste.Enabled = true;
                            }
                            break;

                        case "Labels":
                            if (item is ILabelStyle)
                            {
                                this.menuItemPaste.Enabled = true;
                            }
                            break;

                        case "Backgrounds":
                            if (item is ISymbolBackground)
                            {
                                this.menuItemPaste.Enabled = true;
                            }
                            break;

                        case "Shadows":
                            if (item is ISymbolShadow)
                            {
                                this.menuItemPaste.Enabled = true;
                            }
                            break;

                        case "Legend Items":
                            if (item is ILegendItem)
                            {
                                this.menuItemPaste.Enabled = true;
                            }
                            break;
                    }
                }
                ListViewItem itemAt = this.symbolListView1.GetItemAt(e.X, e.Y);
                this.m_HitItem = null;
                this.m_HitStyleItem = null;
                if (itemAt != null)
                {
                    this.m_HitItem = itemAt;
                    this.m_HitStyleItem = itemAt.Tag as IStyleGalleryItem;
                }
                else if (this.symbolListView1.SelectedItems.Count > 0)
                {
                    this.m_HitItem = this.symbolListView1.SelectedItems[0];
                    this.m_HitStyleItem = this.symbolListView1.SelectedItems[0].Tag as IStyleGalleryItem;
                }
                if (this.m_HitStyleItem == null)
                {
                    this.menuItemCopy.Enabled = false;
                    this.menuItemCut.Enabled = false;
                    this.menuItemDelete.Enabled = false;
                    this.menuItemProperty.Enabled = false;
                }
                else
                {
                    this.menuItemCopy.Enabled = true;
                    this.menuItemCut.Enabled = true;
                    this.menuItemDelete.Enabled = true;
                    this.menuItemProperty.Enabled = true;
                }
                System.Drawing.Point pos = new System.Drawing.Point(e.X, e.Y);
                this.contextMenu1.Show(this.symbolListView1, pos);
            }
        }

        private void symbolListView1_OnValueChanged(object newValue)
        {
            this.m_pSG.UpdateItem(newValue as IStyleGalleryItem);
        }
    }
}

