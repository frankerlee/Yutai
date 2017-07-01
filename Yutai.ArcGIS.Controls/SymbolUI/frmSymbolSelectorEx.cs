using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.AELicenseProvider;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.Shared;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    //  [LicenseProvider(typeof(AELicenseProviderEx))]
    public partial class frmSymbolSelectorEx : Form
    {
        private License _license = null;
        private bool m_CanDo = false;
        private string m_Category = "";
        private string m_ClassName = "";
        private string m_CurrentStyleFileName = "";
        private object m_pPreviewSelSymbol = null;
        private IStyleGallery m_pSG = null;
        private string m_styleFileName = "";
        private string m_styleName = "";
        private enumSymbolType m_SymbolType = enumSymbolType.enumSTPoint;

        public frmSymbolSelectorEx()
        {
            this.InitializeComponent();
            //  this._license = LicenseManager.Validate(typeof(frmSymbolSelector), this);
        }

        private void AddSymbol(string FileName)
        {
            IEnumStyleGalleryItem item = null;
            switch (this.m_SymbolType)
            {
                case enumSymbolType.enumSTPoint:
                    item = this.m_pSG.get_Items("Marker Symbols", FileName, this.m_Category);
                    break;

                case enumSymbolType.enumSTLine:
                    item = this.m_pSG.get_Items("Line Symbols", FileName, this.m_Category);
                    break;

                case enumSymbolType.enumSTFill:
                    item = this.m_pSG.get_Items("Fill Symbols", FileName, this.m_Category);
                    break;

                case enumSymbolType.enumSTText:
                    item = this.m_pSG.get_Items("Text Symbols", FileName, this.m_Category);
                    break;

                case enumSymbolType.enumSTColorRamp:
                    item = this.m_pSG.get_Items("Color Ramps", FileName, this.m_Category);
                    break;

                case enumSymbolType.enumSTColor:
                    item = this.m_pSG.get_Items("Colors", FileName, this.m_Category);
                    break;

                case enumSymbolType.enumSTNorthArrow:
                    item = this.m_pSG.get_Items("North Arrows", FileName, this.m_Category);
                    break;

                case enumSymbolType.enumSTScaleBar:
                    item = this.m_pSG.get_Items("Scale Bars", FileName, this.m_Category);
                    break;

                case enumSymbolType.enumSTScaleText:
                    item = this.m_pSG.get_Items("Scale Texts", FileName, this.m_Category);
                    break;

                case enumSymbolType.enumSTBorder:
                    item = this.m_pSG.get_Items("Borders", FileName, this.m_Category);
                    break;

                case enumSymbolType.enumSTShadow:
                    item = this.m_pSG.get_Items("Shadows", FileName, this.m_Category);
                    break;

                case enumSymbolType.enumSTBackground:
                    item = this.m_pSG.get_Items("Backgrounds", FileName, this.m_Category);
                    break;

                case enumSymbolType.enumSTLinePatch:
                    item = this.m_pSG.get_Items("Line Patches", FileName, this.m_Category);
                    break;

                case enumSymbolType.enumSTLegendItem:
                    item = this.m_pSG.get_Items("Legend Items", FileName, this.m_Category);
                    break;

                case enumSymbolType.enumSTLabel:
                    item = this.m_pSG.get_Items("Labels", FileName, this.m_Category);
                    break;

                case enumSymbolType.enumSTMapGrid:
                    item = this.m_pSG.get_Items("Reference Systems", FileName, this.m_Category);
                    break;
            }
            if (item != null)
            {
                item.Reset();
                for (IStyleGalleryItem item2 = item.Next(); item2 != null; item2 = item.Next())
                {
                    this.symbolListView1.Add(item2);
                }
                item.Reset();
                item = null;
                GC.Collect();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.m_pSG = null;
            base.Close();
        }

        private void btnMoreSymbol_Click(object sender, EventArgs e)
        {
            if (this.m_pSG != null)
            {
                try
                {
                    this.popupMenu1.ClearLinks();
                    this.barManager1.Items.Clear();
                    this.barManager1.Items.Add(this.menuItemAddStyleSet);
                    IStyleGalleryStorage pSG = (IStyleGalleryStorage) this.m_pSG;
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
                        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
                        BarButtonItem item = this.barManager1.Items[fileNameWithoutExtension] as BarButtonItem;
                        if (item == null)
                        {
                            item = new BarButtonItem
                            {
                                Name = fileNameWithoutExtension,
                                Caption = path,
                                Id = this.barManager1.GetNewItemId()
                            };
                            this.barManager1.Items.Add(item);
                            item.ItemClick += new ItemClickEventHandler(this.item_ItemClick);
                        }
                        item.ButtonStyle = BarButtonStyle.Check;
                        this.popupMenu1.AddItem(item);
                    }
                    pSG = null;
                    if (this.popupMenu1.ItemLinks.Count == 0)
                    {
                        this.popupMenu1.AddItem(this.menuItemAddStyleSet);
                    }
                    else
                    {
                        this.popupMenu1.AddItem(this.menuItemAddStyleSet).BeginGroup = true;
                    }
                    Point p = new Point(this.btnMoreSymbol.Location.X,
                        this.btnMoreSymbol.Location.Y + this.btnMoreSymbol.Height);
                    p = base.PointToScreen(p);
                    this.popupMenu1.ShowPopup(p);
                }
                catch (Exception exception)
                {
                    Logger.Current.Error("", exception, "");
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.m_pSG = null;
            base.Close();
        }

        private void btnProperty_Click(object sender, EventArgs e)
        {
            if (this.m_pPreviewSelSymbol is IMarkerSymbol)
            {
                frmPointSymbolEdit edit = new frmPointSymbolEdit();
                edit.SetSymbol(this.m_pPreviewSelSymbol as ISymbol);
                if (edit.ShowDialog() == DialogResult.OK)
                {
                    this.m_pPreviewSelSymbol = edit.GetSymbol();
                    this.m_CanDo = false;
                    this.InitControl(this.m_pPreviewSelSymbol);
                    this.m_CanDo = true;
                    this.symbolItem1.Symbol = this.m_pPreviewSelSymbol;
                    this.symbolItem1.Invalidate();
                }
            }
            else if (this.m_pPreviewSelSymbol is ILineSymbol)
            {
                frmLineSymbolEdit edit2 = new frmLineSymbolEdit
                {
                    m_pSG = this.m_pSG
                };
                edit2.SetSymbol(this.m_pPreviewSelSymbol as ISymbol);
                if (edit2.ShowDialog() == DialogResult.OK)
                {
                    this.m_pPreviewSelSymbol = edit2.GetSymbol();
                    this.m_CanDo = false;
                    this.InitControl(this.m_pPreviewSelSymbol);
                    this.m_CanDo = true;
                    this.symbolItem1.Symbol = this.m_pPreviewSelSymbol;
                    this.symbolItem1.Invalidate();
                }
            }
            else if (this.m_pPreviewSelSymbol is IFillSymbol)
            {
                frmFillSymbolEdit edit3 = new frmFillSymbolEdit
                {
                    m_pSG = this.m_pSG
                };
                edit3.SetSymbol(this.m_pPreviewSelSymbol as ISymbol);
                if (edit3.ShowDialog() == DialogResult.OK)
                {
                    this.m_pPreviewSelSymbol = edit3.GetSymbol();
                    this.m_CanDo = false;
                    this.InitControl(this.m_pPreviewSelSymbol);
                    this.m_CanDo = true;
                    this.symbolItem1.Symbol = this.m_pPreviewSelSymbol;
                    this.symbolItem1.Invalidate();
                }
            }
            else if (this.m_pPreviewSelSymbol is ITextSymbol)
            {
                frmTextSymbolEdit edit4 = new frmTextSymbolEdit
                {
                    m_pSG = this.m_pSG
                };
                edit4.SetSymbol(this.m_pPreviewSelSymbol as ISymbol);
                if (edit4.ShowDialog() == DialogResult.OK)
                {
                    this.m_pPreviewSelSymbol = edit4.GetSymbol();
                    this.m_CanDo = false;
                    this.InitControl(this.m_pPreviewSelSymbol);
                    this.m_CanDo = true;
                    this.symbolItem1.Symbol = this.m_pPreviewSelSymbol;
                    this.symbolItem1.Invalidate();
                }
            }
            else
            {
                frmElementProperty property;
                if (this.m_pPreviewSelSymbol is ISymbolBorder)
                {
                    property = new frmElementProperty();
                    BorderSymbolPropertyPage page = new BorderSymbolPropertyPage();
                    property.Text = "边界";
                    property.AddPage(page);
                    if (property.EditProperties(this.m_pPreviewSelSymbol))
                    {
                        this.symbolItem1.Invalidate();
                    }
                }
                else if (this.m_pPreviewSelSymbol is ISymbolBackground)
                {
                    property = new frmElementProperty();
                    BackgroundSymbolPropertyPage page2 = new BackgroundSymbolPropertyPage();
                    property.Text = "背景";
                    property.AddPage(page2);
                    if (property.EditProperties(this.m_pPreviewSelSymbol))
                    {
                        this.symbolItem1.Invalidate();
                    }
                }
                else if (this.m_pPreviewSelSymbol is ISymbolShadow)
                {
                    property = new frmElementProperty();
                    ShadowSymbolPropertyPage page3 = new ShadowSymbolPropertyPage();
                    property.Text = "阴影";
                    property.AddPage(page3);
                    if (property.EditProperties(this.m_pPreviewSelSymbol))
                    {
                        this.symbolItem1.Invalidate();
                    }
                }
            }
        }

        private void colorEditFill_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor pColor = ((IFillSymbol) this.m_pPreviewSelSymbol).Color;
                this.UpdateColorFromColorEdit(this.colorEditFill, pColor);
                ((IFillSymbol) this.m_pPreviewSelSymbol).Color = pColor;
                this.symbolItem1.Invalidate();
            }
        }

        private void colorEditLine_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor pColor = ((ILineSymbol) this.m_pPreviewSelSymbol).Color;
                this.UpdateColorFromColorEdit(this.colorEditLine, pColor);
                ((ILineSymbol) this.m_pPreviewSelSymbol).Color = pColor;
                this.symbolItem1.Invalidate();
            }
        }

        private void colorEditMarker_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor pColor = ((IMarkerSymbol) this.m_pPreviewSelSymbol).Color;
                this.UpdateColorFromColorEdit(this.colorEditMarker, pColor);
                ((IMarkerSymbol) this.m_pPreviewSelSymbol).Color = pColor;
                this.symbolItem1.Invalidate();
            }
        }

        private void colorEditOutline_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo && (((IFillSymbol) this.m_pPreviewSelSymbol).Outline != null))
            {
                ILineSymbol outline = ((IFillSymbol) this.m_pPreviewSelSymbol).Outline;
                IColor pColor = outline.Color;
                this.UpdateColorFromColorEdit(this.colorEditOutline, pColor);
                outline.Color = pColor;
                ((IFillSymbol) this.m_pPreviewSelSymbol).Outline = outline;
                this.symbolItem1.Invalidate();
            }
        }

        private void comboBoxEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxEdit.SelectedIndex == 0)
            {
                this.m_Category = "";
            }
            else
            {
                this.m_Category = this.comboBoxEdit.Text;
            }
            if (this.m_CanDo)
            {
                this.ReadSymbol();
            }
        }

        private void frmSymbolSelector_Load(object sender, EventArgs e)
        {
            this.symbolItem1.HasDrawLine = false;
            this.m_CanDo = false;
            this.m_ClassName = "Marker Symbols";
            this.comboBoxEdit.Properties.Items.Add("全部");
            this.comboBoxEdit.SelectedIndex = 0;
            this.m_Category = "";
            switch (this.m_SymbolType)
            {
                case enumSymbolType.enumSTPoint:
                    this.m_ClassName = "Marker Symbols";
                    this.ReadCategories("Marker Symbols");
                    this.symbolListView1.SetItemSize(20, 20, 50, 50);
                    break;

                case enumSymbolType.enumSTLine:
                    this.m_ClassName = "Line Symbols";
                    this.ReadCategories("Line Symbols");
                    this.symbolListView1.SetItemSize(20, 20, 50, 50);
                    break;

                case enumSymbolType.enumSTFill:
                    this.m_ClassName = "Fill Symbols";
                    this.ReadCategories("Fill Symbols");
                    this.symbolListView1.SetItemSize(20, 20, 50, 50);
                    break;

                case enumSymbolType.enumSTText:
                    this.m_ClassName = "Text Symbols";
                    this.ReadCategories("Text Symbols");
                    this.symbolListView1.SetItemSize(20, 20, 150, 50);
                    break;

                case enumSymbolType.enumSTNorthArrow:
                    this.m_ClassName = "North Arrows";
                    this.ReadCategories("North Arrows");
                    this.symbolListView1.SetItemSize(20, 20, 50, 50);
                    break;

                case enumSymbolType.enumSTScaleBar:
                    this.m_ClassName = "Scale Bars";
                    this.ReadCategories("Scale Bars");
                    this.symbolListView1.SetItemSize(20, 20, 150, 50);
                    break;

                case enumSymbolType.enumSTScaleText:
                    this.m_ClassName = "Scale Texts";
                    this.ReadCategories("Scale Texts");
                    this.symbolListView1.SetItemSize(20, 20, 150, 50);
                    break;

                case enumSymbolType.enumSTBorder:
                    this.m_ClassName = "Borders";
                    this.ReadCategories("Borders");
                    this.symbolListView1.SetItemSize(20, 20, 150, 50);
                    break;

                case enumSymbolType.enumSTShadow:
                    this.m_ClassName = "Shadows";
                    this.ReadCategories("Shadows");
                    this.symbolListView1.SetItemSize(20, 20, 50, 80);
                    break;

                case enumSymbolType.enumSTBackground:
                    this.m_ClassName = "Backgrounds";
                    this.ReadCategories("Backgrounds");
                    this.symbolListView1.SetItemSize(20, 20, 50, 80);
                    break;

                case enumSymbolType.enumSTLinePatch:
                    this.m_ClassName = "Line Patches";
                    this.ReadCategories("Line Patches");
                    this.symbolListView1.SetItemSize(20, 20, 50, 80);
                    break;

                case enumSymbolType.enumSTAreaPatch:
                    this.m_ClassName = "Area Patches";
                    this.ReadCategories("Area Patches");
                    this.symbolListView1.SetItemSize(20, 20, 50, 80);
                    break;

                case enumSymbolType.enumSTLabel:
                    this.m_ClassName = "Labels";
                    this.ReadCategories("Labels");
                    this.symbolListView1.SetItemSize(20, 20, 50, 80);
                    break;
            }
            if (this.m_pSG != null)
            {
                this.ReadSymbol();
                if ((this.m_pPreviewSelSymbol == null) && (this.symbolListView1.Items.Count > 0))
                {
                    this.symbolListView1.SelectedIndices.Add(0);
                    this.m_pPreviewSelSymbol = this.symbolListView1.GetSelectStyleGalleryItem().Item;
                }
            }
            this.InitControl(this.m_pPreviewSelSymbol);
            this.m_CanDo = true;
        }

        private void GetRGB(uint rgb, out int r, out int g, out int b)
        {
            uint num = rgb & 16711680;
            b = (int) (num >> 16);
            num = rgb & 65280;
            g = (int) (num >> 8);
            num = rgb & 255;
            r = (int) num;
        }

        public IStyleGalleryItem GetSelectStyleGalleryItem()
        {
            return this.symbolListView1.GetSelectStyleGalleryItem();
        }

        public object GetSymbol()
        {
            return this.m_pPreviewSelSymbol;
        }

        private void InitControl(object pSymbol)
        {
            try
            {
                if (pSymbol != null)
                {
                    if (pSymbol is IMarkerSymbol)
                    {
                        this.groupBoxMarker.Visible = true;
                        this.spinEditSize.Value = (decimal) ((IMarkerSymbol) pSymbol).Size;
                        this.SetColorEdit(this.colorEditMarker, ((IMarkerSymbol) pSymbol).Color);
                    }
                    else if (pSymbol is ILineSymbol)
                    {
                        this.groupBoxLine.Visible = true;
                        this.spinEditSize.Value = (decimal) ((ILineSymbol) pSymbol).Width;
                        this.SetColorEdit(this.colorEditLine, ((ILineSymbol) pSymbol).Color);
                    }
                    else if (pSymbol is IColorSymbol)
                    {
                        this.groupBoxFill.Visible = true;
                        this.SetColorEdit(this.colorEditFill, ((IColorSymbol) pSymbol).Color);
                        this.colorEditOutline.Enabled = false;
                    }
                    else if (pSymbol is IFillSymbol)
                    {
                        this.groupBoxFill.Visible = true;
                        this.SetColorEdit(this.colorEditFill, ((IFillSymbol) pSymbol).Color);
                        if (((IFillSymbol) pSymbol).Outline != null)
                        {
                            this.SetColorEdit(this.colorEditOutline, ((IFillSymbol) pSymbol).Outline.Color);
                        }
                        else
                        {
                            this.colorEditOutline.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
        }

        public bool IsNmuber(string str)
        {
            if (str.Length > 0)
            {
                int num2;
                int num = 0;
                if ((str[0] < '0') || (str[0] > '9'))
                {
                    if (str[0] != '.')
                    {
                        if (str[0] != '-')
                        {
                            if (str[0] != '+')
                            {
                                return false;
                            }
                            for (num2 = 1; num2 < str.Length; num2++)
                            {
                                if ((str[num2] < '0') || (str[num2] > '9'))
                                {
                                    if (str[num2] != '.')
                                    {
                                        return false;
                                    }
                                    if (num == 1)
                                    {
                                        return false;
                                    }
                                    num++;
                                }
                            }
                        }
                        else
                        {
                            for (num2 = 1; num2 < str.Length; num2++)
                            {
                                if ((str[num2] < '0') || (str[num2] > '9'))
                                {
                                    if (str[num2] != '.')
                                    {
                                        return false;
                                    }
                                    if (num == 1)
                                    {
                                        return false;
                                    }
                                    num++;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (num2 = 1; num2 < str.Length; num2++)
                        {
                            if ((str[num2] < '0') || (str[num2] > '9'))
                            {
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    for (num2 = 1; num2 < str.Length; num2++)
                    {
                        if ((str[num2] < '0') || (str[num2] > '9'))
                        {
                            if (str[num2] != '.')
                            {
                                return false;
                            }
                            if (num == 1)
                            {
                                return false;
                            }
                            num++;
                        }
                    }
                }
            }
            return true;
        }

        private void item_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private void item1_Click(object sender, EventArgs e)
        {
        }

        private void menuItemAddStyleSet_ItemClick(object sender, ItemClickEventArgs e)
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
                    this.AddSymbol(dialog.FileName);
                }
            }
        }

        private void menuItemAddStyleSet1_Click(object sender, EventArgs e)
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
                    this.AddSymbol(dialog.FileName);
                }
            }
        }

        private void ReadCategories(string className)
        {
            try
            {
                IEnumBSTR mbstr = this.m_pSG.get_Categories(className);
                mbstr.Reset();
                for (string str = mbstr.Next(); str != null; str = mbstr.Next())
                {
                    this.comboBoxEdit.Properties.Items.Add(str);
                }
            }
            catch
            {
            }
        }

        private void ReadSymbol()
        {
            try
            {
                this.symbolListView1.RemoveAll();
                IEnumStyleGalleryItem item = null;
                if (this.m_ClassName != null)
                {
                    IStyleGalleryItem styleGalleryItem = null;
                    for (int i = 0; i < (this.m_pSG as IStyleGalleryStorage).FileCount; i++)
                    {
                        string path = (this.m_pSG as IStyleGalleryStorage).get_File(i);
                        string str2 = Path.GetExtension(path).ToLower();
                        if ((!(this.m_pSG is ESRI.ArcGIS.esriSystem.IPersistStream) || (str2 == ".serverstyle")) &&
                            (!(this.m_pSG is MyStyleGallery) || !(str2 != ".style")))
                        {
                            item = this.m_pSG.get_Items(this.m_ClassName, path, this.m_Category);
                            if (item != null)
                            {
                                try
                                {
                                    item.Reset();
                                    for (styleGalleryItem = item.Next();
                                        styleGalleryItem != null;
                                        styleGalleryItem = item.Next())
                                    {
                                        this.symbolListView1.Add(styleGalleryItem);
                                        if (styleGalleryItem.Name.ToLower() == this.m_styleName)
                                        {
                                            this.symbolListView1.SelectedIndices.Clear();
                                            this.symbolListView1.SelectedIndices.Add(this.symbolListView1.Items.Count -
                                                                                     1);
                                            if (this.m_pPreviewSelSymbol == null)
                                            {
                                                this.m_pPreviewSelSymbol = (styleGalleryItem.Item as IClone).Clone();
                                                this.symbolItem1.Symbol = (ISymbol) styleGalleryItem.Item;
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                }
                                item = null;
                                GC.Collect();
                            }
                        }
                    }
                    if (this.m_pPreviewSelSymbol == null)
                    {
                        styleGalleryItem = this.symbolListView1.GetStyleGalleryItem(0);
                        if (styleGalleryItem != null)
                        {
                            this.m_pPreviewSelSymbol = (styleGalleryItem.Item as IClone).Clone();
                            this.symbolItem1.Symbol = (ISymbol) styleGalleryItem.Item;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private int RGB(int r, int g, int b)
        {
            uint num = 0;
            num |= (uint) b;
            num = num << 8;
            num |= (uint) g;
            num = num << 8;
            num |= (uint) r;
            return (int) num;
        }

        private void SetColorEdit(ColorEdit colorEdit, IColor pColor)
        {
            if (pColor.NullColor)
            {
                colorEdit.Color = Color.Empty;
            }
            else
            {
                int num;
                int num2;
                int num3;
                int rGB = pColor.RGB;
                this.GetRGB((uint) rGB, out num, out num2, out num3);
                colorEdit.Color = Color.FromArgb(pColor.Transparency, num, num2, num3);
            }
        }

        public void SetSelectSymbol(string styleFileName, string styleName)
        {
            this.m_styleFileName = styleFileName.ToLower();
            this.m_styleName = styleName.ToLower();
        }

        public void SetStyleGallery(IStyleGallery pSG)
        {
            this.m_pSG = pSG;
        }

        public void SetSymbol(object pSym)
        {
            if (pSym != null)
            {
                if (pSym == null)
                {
                    this.m_SymbolType = enumSymbolType.enumSTPoint;
                }
                else if (pSym is IMarkerSymbol)
                {
                    this.m_SymbolType = enumSymbolType.enumSTPoint;
                }
                else if (pSym is ILineSymbol)
                {
                    this.m_SymbolType = enumSymbolType.enumSTLine;
                }
                else if (pSym is IFillSymbol)
                {
                    this.m_SymbolType = enumSymbolType.enumSTFill;
                }
                else if (pSym is INorthArrow)
                {
                    this.m_SymbolType = enumSymbolType.enumSTNorthArrow;
                }
                else if (pSym is IScaleBar)
                {
                    this.m_SymbolType = enumSymbolType.enumSTScaleBar;
                }
                else if (pSym is IScaleText)
                {
                    this.m_SymbolType = enumSymbolType.enumSTScaleText;
                }
                else if (pSym is IShadow)
                {
                    this.m_SymbolType = enumSymbolType.enumSTShadow;
                }
                else if (pSym is IBorder)
                {
                    this.m_SymbolType = enumSymbolType.enumSTBorder;
                }
                else if (pSym is IBackground)
                {
                    this.m_SymbolType = enumSymbolType.enumSTBackground;
                }
                else if (pSym is ITextSymbol)
                {
                    this.m_SymbolType = enumSymbolType.enumSTText;
                }
                else if (pSym is IMaplexLabelStyle)
                {
                    this.m_SymbolType = enumSymbolType.enumSTMaplexLabel;
                }
                else if (pSym is ILabelStyle)
                {
                    this.m_SymbolType = enumSymbolType.enumSTLabel;
                }
                else if (pSym is IMapGrid)
                {
                    this.m_SymbolType = enumSymbolType.enumSTMapGrid;
                }
                this.m_pPreviewSelSymbol = (pSym as IClone).Clone();
                this.symbolItem1.Symbol = this.m_pPreviewSelSymbol;
            }
        }

        public void SetSymbolType(enumSymbolType type)
        {
            this.m_SymbolType = type;
        }

        private void spinEditSize_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.spinEditSize.Value < 0M)
                {
                    this.spinEditSize.ForeColor = Color.Red;
                }
                else if ((this.spinEditSize.Value == 0M) && !this.IsNmuber(this.spinEditWidth.Text))
                {
                    this.spinEditSize.ForeColor = Color.Red;
                }
                else
                {
                    this.spinEditSize.ForeColor = SystemColors.WindowText;
                    ((IMarkerSymbol) this.m_pPreviewSelSymbol).Size = (double) this.spinEditSize.Value;
                    this.symbolItem1.Invalidate();
                }
            }
        }

        private void spinEditWidth_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.spinEditWidth.Value < 0M)
                {
                    this.spinEditWidth.ForeColor = Color.Red;
                }
                else if ((this.spinEditWidth.Value == 0M) && !this.IsNmuber(this.spinEditWidth.Text))
                {
                    this.spinEditWidth.ForeColor = Color.Red;
                }
                else
                {
                    this.spinEditWidth.ForeColor = SystemColors.WindowText;
                    ((ILineSymbol) this.m_pPreviewSelSymbol).Width = (double) this.spinEditWidth.Value;
                    this.symbolItem1.Invalidate();
                }
            }
        }

        private void symbolListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IStyleGalleryItem selectStyleGalleryItem = this.symbolListView1.GetSelectStyleGalleryItem();
                if (selectStyleGalleryItem != null)
                {
                    this.m_pPreviewSelSymbol = (selectStyleGalleryItem.Item as IClone).Clone();
                    this.symbolItem1.Symbol = this.m_pPreviewSelSymbol;
                    this.m_CanDo = false;
                    this.InitControl(this.m_pPreviewSelSymbol);
                    this.m_CanDo = true;
                    this.symbolItem1.Invalidate();
                }
            }
        }

        private void UpdateColorFromColorEdit(ColorEdit colorEdit, IColor pColor)
        {
            pColor.NullColor = colorEdit.Color.IsEmpty;
            if (!colorEdit.Color.IsEmpty)
            {
                pColor.Transparency = colorEdit.Color.A;
                pColor.RGB = this.RGB(colorEdit.Color.R, colorEdit.Color.G, colorEdit.Color.B);
            }
        }

        public string CnrrentStyleFileName
        {
            get { return this.m_CurrentStyleFileName; }
            set { this.m_CurrentStyleFileName = value; }
        }

        public IStyleGalleryItem SelectedStyleGalleryItem
        {
            get { return this.symbolListView1.GetSelectStyleGalleryItem(); }
        }
    }
}