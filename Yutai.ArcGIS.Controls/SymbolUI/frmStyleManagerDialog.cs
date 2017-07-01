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
    public partial class frmStyleManagerDialog : Form
    {
        private License _license = null;
        private IArray m_boards = new ArrayClass();
        private string m_Category = "";
        private string m_ClassName = "";
        private ListViewItem m_HitItem = null;
        private IStyleGalleryItem m_HitStyleItem = null;
        private IStyleGallery m_pSG = null;
        private string m_StyleSet = "";

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
                this.symbolListView1.OnValueChanged +=
                    new SymbolListViewEx.OnValueChangedHandler(this.symbolListView1_OnValueChanged);
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
                System.Drawing.Point pos = new System.Drawing.Point(this.btnStyleSet.Location.X,
                    this.btnStyleSet.Location.Y + this.btnStyleSet.Height);
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
                    MenuItem item = new MenuItem(text)
                    {
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
                this.ReadSymbol(this.SymbolLibTreeView.SelectedNode.Text,
                    this.SymbolLibTreeView.SelectedNode.Parent.Text);
            }
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
                    frmPointSymbolEdit edit = new frmPointSymbolEdit
                    {
                        m_pSG = this.m_pSG
                    };
                    edit.SetSymbol(item);
                    none = edit.ShowDialog();
                    item = edit.GetSymbol();
                }
                else if (item is ILineSymbol)
                {
                    frmLineSymbolEdit edit2 = new frmLineSymbolEdit
                    {
                        m_pSG = this.m_pSG
                    };
                    edit2.SetSymbol(item);
                    none = edit2.ShowDialog();
                    item = edit2.GetSymbol();
                }
                else if (item is IFillSymbol)
                {
                    frmFillSymbolEdit edit3 = new frmFillSymbolEdit
                    {
                        m_pSG = this.m_pSG
                    };
                    edit3.SetSymbol(item);
                    none = edit3.ShowDialog();
                    item = edit3.GetSymbol();
                }
                else if (item is ITextSymbol)
                {
                    frmTextSymbolEdit edit4 = new frmTextSymbolEdit
                    {
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
                    property = new frmElementProperty
                    {
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
                        property = new frmElementProperty
                        {
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
                        property = new frmElementProperty
                        {
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
                        property = new frmElementProperty
                        {
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
                        property = new frmElementProperty
                        {
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
                    frmPointSymbolEdit edit = new frmPointSymbolEdit
                    {
                        m_pSG = this.m_pSG
                    };
                    edit.SetSymbol(item);
                    none = edit.ShowDialog();
                    item = edit.GetSymbol();
                }
                else if (item is ILineSymbol)
                {
                    frmLineSymbolEdit edit2 = new frmLineSymbolEdit
                    {
                        m_pSG = this.m_pSG
                    };
                    edit2.SetSymbol(item);
                    none = edit2.ShowDialog();
                    item = edit2.GetSymbol();
                }
                else if (item is IFillSymbol)
                {
                    frmFillSymbolEdit edit3 = new frmFillSymbolEdit
                    {
                        m_pSG = this.m_pSG
                    };
                    edit3.SetSymbol(item);
                    none = edit3.ShowDialog();
                    item = edit3.GetSymbol();
                }
                else if (item is ITextSymbol)
                {
                    frmTextSymbolEdit edit4 = new frmTextSymbolEdit
                    {
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
                        property = new frmElementProperty
                        {
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
                        property = new frmElementProperty
                        {
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
                        property = new frmElementProperty
                        {
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
                        property = new frmElementProperty
                        {
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
                        property = new frmElementProperty
                        {
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
                    TreeNode pParentNode = new TreeNode(pSG.get_File(i))
                    {
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

        private void InsertStyleClassToNode(TreeNode pParentNode, IStyleGallery pSG)
        {
            for (int i = 0; i < pSG.ClassCount; i++)
            {
                TreeNode node = new TreeNode(pSG.get_Class(i).Name)
                {
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
                    TreeNode pParentNode = new TreeNode(dialog.FileName)
                    {
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
            IRepresentationRuleItem item = new RepresentationRuleItemClass
            {
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
                IStyleGalleryItem item2 = new ServerStyleGalleryItemClass
                {
                    Item = rule2.RepresentationRuleItem,
                    Name = "新面制图表现",
                    Category = "Default"
                };
                IStyleGalleryStorage pSG = (IStyleGalleryStorage) this.m_pSG;
                pSG.TargetFile = this.SymbolLibTreeView.SelectedNode.Parent.Text;
                this.m_pSG.AddItem(item2);
                this.ReadSymbol(this.SymbolLibTreeView.SelectedNode.Text,
                    this.SymbolLibTreeView.SelectedNode.Parent.Text);
            }
        }

        private void menuItemNewLineRepRule_Click(object sender, EventArgs e)
        {
            IRepresentationRuleItem item = new RepresentationRuleItemClass
            {
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
                IStyleGalleryItem item2 = new ServerStyleGalleryItemClass
                {
                    Item = rule2.RepresentationRuleItem,
                    Name = "新线制图表现",
                    Category = "Default"
                };
                IStyleGalleryStorage pSG = (IStyleGalleryStorage) this.m_pSG;
                pSG.TargetFile = this.SymbolLibTreeView.SelectedNode.Parent.Text;
                this.m_pSG.AddItem(item2);
                this.ReadSymbol(this.SymbolLibTreeView.SelectedNode.Text,
                    this.SymbolLibTreeView.SelectedNode.Parent.Text);
            }
        }

        private void menuItemNewPointRepRule_Click(object sender, EventArgs e)
        {
            IRepresentationRuleItem item = new RepresentationRuleItemClass
            {
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
                IStyleGalleryItem item2 = new ServerStyleGalleryItemClass
                {
                    Item = rule2.RepresentationRuleItem,
                    Name = "新点制图表现",
                    Category = "Default"
                };
                IStyleGalleryStorage pSG = (IStyleGalleryStorage) this.m_pSG;
                pSG.TargetFile = this.SymbolLibTreeView.SelectedNode.Parent.Text;
                this.m_pSG.AddItem(item2);
                this.ReadSymbol(this.SymbolLibTreeView.SelectedNode.Text,
                    this.SymbolLibTreeView.SelectedNode.Parent.Text);
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
                        manifestResourceStream =
                            base.GetType()
                                .Assembly.GetManifestResourceStream(
                                    "Yutai.ArcGIS.Controls.Controls.SymbolUI.template.ServerStyle");
                    }
                    else
                    {
                        manifestResourceStream =
                            base.GetType()
                                .Assembly.GetManifestResourceStream(
                                    "Yutai.ArcGIS.Controls.Controls.SymbolUI.template.style");
                    }
                    byte[] buffer = new byte[4096];
                    while ((num = manifestResourceStream.Read(buffer, 0, 4096)) > 0)
                    {
                        stream.Write(buffer, 0, num);
                    }
                    stream.Close();
                }
                ((IStyleGalleryStorage) this.m_pSG).AddFile(dialog.FileName);
                TreeNode pParentNode = new TreeNode(dialog.FileName)
                {
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
                IStyleGalleryItem item2 = new ServerStyleGalleryItemClass
                {
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
                        this.symbolListView1.SetItemSize(16, 16, 50, 50);
                        break;

                    case "Line Symbols":
                        this.symbolListView1.SetItemSize(20, 16, 50, 50);
                        break;

                    case "Fill Symbols":
                        this.symbolListView1.SetItemSize(20, 20, 50, 50);
                        break;

                    case "Color Ramps":
                        this.symbolListView1.SetItemSize(40, 20, 160, 35);
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
                        this.symbolListView1.SetItemSize(16, 16, 50, 50);
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