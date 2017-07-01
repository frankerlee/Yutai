using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class AnnoClassSetCtrl : UserControl
    {
        public AnnoClassSetCtrl()
        {
            this.InitializeComponent();
            this.cboFontName.Items.Clear();
            InstalledFontCollection fonts = new InstalledFontCollection();
            for (int i = 0; i < fonts.Families.Length; i++)
            {
                this.cboFontName.Items.Add(fonts.Families[i].Name);
                this.cboFontName_rel.Items.Add(fonts.Families[i].Name);
            }
        }

        private void AnnoClassSetCtrl_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.listBoxControl1.SelectedIndex != -1)
            {
                int selectedIndex = this.listBoxControl1.SelectedIndex;
                SymbolIdentifierWrap selectedItem = this.listBoxControl1.SelectedItem as SymbolIdentifierWrap;
                NewObjectClassHelper.m_pObjectClassHelper.m_pSymbolColl.Remove(selectedItem.SymbolIdentifier.ID);
                NewObjectClassHelper.m_pObjectClassHelper.m_pAnnoPropertiesColn.Remove(selectedItem.SymbolIdentifier.ID);
                if (selectedIndex == 0)
                {
                    this.listBoxControl1.SelectedIndex = selectedIndex;
                }
                else
                {
                    this.listBoxControl1.SelectedIndex = selectedIndex - 1;
                }
            }
        }

        private void btnLabelExpress_Click(object sender, EventArgs e)
        {
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmInput input = new frmInput("名称:", "")
            {
                Text = "输入"
            };
            if (input.ShowDialog() == DialogResult.OK)
            {
                if (input.InputValue.Trim().Length == 0)
                {
                    MessageBox.Show("非法类名!");
                }
                else
                {
                    ISymbolIdentifier2 identifier;
                    for (int i = 0; i < this.listBoxControl1.Items.Count; i++)
                    {
                        SymbolIdentifierWrap wrap = this.listBoxControl1.Items[i] as SymbolIdentifierWrap;
                        if (wrap.AnnotateLayerProperties.Class == input.InputValue)
                        {
                            MessageBox.Show("类名必须唯一!");
                            return;
                        }
                    }
                    ITextSymbol symbol = new TextSymbolClass();
                    int symbolID = this.method_4(NewObjectClassHelper.m_pObjectClassHelper.m_pSymbolColl);
                    NewObjectClassHelper.m_pObjectClassHelper.m_pSymbolColl.set_Symbol(symbolID, symbol as ISymbol);
                    IAnnotateLayerProperties item = new LabelEngineLayerPropertiesClass
                    {
                        Class = input.InputValue,
                        FeatureLinked = NewObjectClassHelper.m_pObjectClassHelper.IsRelatedFeature,
                        AddUnplacedToGraphicsContainer = false,
                        CreateUnplacedElements = true,
                        DisplayAnnotation = true,
                        UseOutput = true
                    };
                    ILabelEngineLayerProperties properties2 = item as ILabelEngineLayerProperties;
                    if (NewObjectClassHelper.m_pObjectClassHelper.IsRelatedFeature)
                    {
                        new AnnotationVBScriptEngineClass();
                        IFeatureClass relatedFeatureClass =
                            NewObjectClassHelper.m_pObjectClassHelper.RelatedFeatureClass;
                        properties2.Expression = "[" + relatedFeatureClass.ObjectClassID + "]";
                        properties2.IsExpressionSimple = true;
                    }
                    properties2.Offset = 0.0;
                    properties2.SymbolID = symbolID;
                    properties2.Symbol = symbol;
                    NewObjectClassHelper.m_pObjectClassHelper.m_pAnnoPropertiesColn.Add(item);
                    NewObjectClassHelper.m_pObjectClassHelper.m_pSymbolColl.GetSymbolIdentifier(symbolID, out identifier);
                    this.listBoxControl1.Items.Add(new SymbolIdentifierWrap(item, identifier));
                }
            }
        }

        private void btnReName_Click(object sender, EventArgs e)
        {
            if (this.listBoxControl1.SelectedIndex != -1)
            {
                SymbolIdentifierWrap selectedItem = this.listBoxControl1.SelectedItem as SymbolIdentifierWrap;
                frmInput input = new frmInput("名称:", selectedItem.AnnotateLayerProperties.Class)
                {
                    Text = "输入新类名"
                };
                if (input.ShowDialog() == DialogResult.OK)
                {
                    if (input.InputValue.Trim().Length == 0)
                    {
                        MessageBox.Show("非法类名!");
                    }
                    else
                    {
                        for (int i = 0; i < this.listBoxControl1.Items.Count; i++)
                        {
                            SymbolIdentifierWrap wrap2 = this.listBoxControl1.Items[i] as SymbolIdentifierWrap;
                            if (wrap2.AnnotateLayerProperties.Class == input.InputValue)
                            {
                                MessageBox.Show("类名必须唯一!");
                                return;
                            }
                        }
                        selectedItem.AnnotateLayerProperties.Class = input.InputValue;
                        this.method_5(selectedItem);
                    }
                }
            }
        }

        private void cboFontName_rel_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cboFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxControl1.SelectedIndex != -1)
            {
                SymbolIdentifierWrap selectedItem = this.listBoxControl1.SelectedItem as SymbolIdentifierWrap;
                stdole.IFontDisp font = (selectedItem.SymbolIdentifier.Symbol as ITextSymbol).Font;
                font.Name = this.cboFontName.Text;
                (selectedItem.SymbolIdentifier.Symbol as ITextSymbol).Font = font;
                this.method_5(selectedItem);
            }
        }

        private void cboFontSize_rel_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cboFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxControl1.SelectedIndex != -1)
            {
                SymbolIdentifierWrap selectedItem = this.listBoxControl1.SelectedItem as SymbolIdentifierWrap;
                (selectedItem.SymbolIdentifier.Symbol as ITextSymbol).Size = double.Parse(this.cboFontSize.Text);
                this.method_5(selectedItem);
            }
        }

        private void cboLabelField_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void chkBold_CheckedChanged(object sender, EventArgs e)
        {
            if (this.listBoxControl1.SelectedIndex != -1)
            {
                SymbolIdentifierWrap selectedItem = this.listBoxControl1.SelectedItem as SymbolIdentifierWrap;
                stdole.IFontDisp font = (selectedItem.SymbolIdentifier.Symbol as ITextSymbol).Font;
                font.Bold = this.chkBold.Checked;
                (selectedItem.SymbolIdentifier.Symbol as ITextSymbol).Font = font;
                this.method_5(selectedItem);
            }
        }

        private void chkBold_rel_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chkIta_rel_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chkItalic_CheckedChanged(object sender, EventArgs e)
        {
            if (this.listBoxControl1.SelectedIndex != -1)
            {
                SymbolIdentifierWrap selectedItem = this.listBoxControl1.SelectedItem as SymbolIdentifierWrap;
                stdole.IFontDisp font = (selectedItem.SymbolIdentifier.Symbol as ITextSymbol).Font;
                font.Italic = this.chkItalic.Checked;
                (selectedItem.SymbolIdentifier.Symbol as ITextSymbol).Font = font;
                this.method_5(selectedItem);
            }
        }

        private void chkUnderline_CheckedChanged(object sender, EventArgs e)
        {
            if (this.listBoxControl1.SelectedIndex != -1)
            {
                SymbolIdentifierWrap selectedItem = this.listBoxControl1.SelectedItem as SymbolIdentifierWrap;
                stdole.IFontDisp font = (selectedItem.SymbolIdentifier.Symbol as ITextSymbol).Font;
                font.Underline = this.chkUnderline.Checked;
                (selectedItem.SymbolIdentifier.Symbol as ITextSymbol).Font = font;
                this.method_5(selectedItem);
            }
        }

        private void chkUnderLine_rel_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.listBoxControl1.SelectedIndex != -1)
            {
                SymbolIdentifierWrap selectedItem = this.listBoxControl1.SelectedItem as SymbolIdentifierWrap;
                IColor color = (selectedItem.SymbolIdentifier.Symbol as ITextSymbol).Color;
                this.method_3(this.colorEdit1, color);
                (selectedItem.SymbolIdentifier.Symbol as ITextSymbol).Color = color;
                this.method_5(selectedItem);
            }
        }

        private void colorEdit2_EditValueChanged(object sender, EventArgs e)
        {
        }

        public void Init()
        {
            if (NewObjectClassHelper.m_pObjectClassHelper.IsRelatedFeature)
            {
                this.panel1.Visible = false;
                this.panel2.Visible = true;
            }
            else
            {
                this.panel2.Visible = false;
                this.panel1.Visible = true;
            }
            if (NewObjectClassHelper.m_pObjectClassHelper.m_pSymbolColl.Count == 0)
            {
                ITextSymbol symbol = new TextSymbolClass();
                NewObjectClassHelper.m_pObjectClassHelper.m_pSymbolColl.set_Symbol(0, symbol as ISymbol);
                IAnnotateLayerProperties item = new LabelEngineLayerPropertiesClass
                {
                    Class = "要素类 1",
                    FeatureLinked = NewObjectClassHelper.m_pObjectClassHelper.IsRelatedFeature,
                    AddUnplacedToGraphicsContainer = false,
                    CreateUnplacedElements = true,
                    DisplayAnnotation = true,
                    UseOutput = true
                };
                ILabelEngineLayerProperties properties2 = item as ILabelEngineLayerProperties;
                if (NewObjectClassHelper.m_pObjectClassHelper.IsRelatedFeature)
                {
                    new AnnotationVBScriptEngineClass();
                    IFeatureClass relatedFeatureClass = NewObjectClassHelper.m_pObjectClassHelper.RelatedFeatureClass;
                    properties2.Expression = "[" + relatedFeatureClass.ObjectClassID + "]";
                    properties2.IsExpressionSimple = true;
                }
                properties2.Offset = 0.0;
                properties2.SymbolID = 0;
                properties2.Symbol = symbol;
                NewObjectClassHelper.m_pObjectClassHelper.m_pAnnoPropertiesColn.Add(item);
            }
            this.listBoxControl1.Items.Clear();
            for (int i = 0; i < NewObjectClassHelper.m_pObjectClassHelper.m_pAnnoPropertiesColn.Count; i++)
            {
                IAnnotateLayerProperties properties3;
                int num2;
                ISymbolIdentifier2 identifier;
                NewObjectClassHelper.m_pObjectClassHelper.m_pAnnoPropertiesColn.QueryItem(i, out properties3, out num2);
                NewObjectClassHelper.m_pObjectClassHelper.m_pSymbolColl.GetSymbolIdentifier(num2, out identifier);
                this.listBoxControl1.Items.Add(new SymbolIdentifierWrap(properties3, identifier));
            }
            this.listBoxControl1.SelectedIndex = 0;
        }

        private void listBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.listBoxControl1.ItemCount > 1) && (this.listBoxControl1.SelectedIndex != -1))
            {
                this.btnDelete.Enabled = true;
            }
            else
            {
                this.btnDelete.Enabled = false;
            }
            if (this.listBoxControl1.SelectedIndex == -1)
            {
                this.groupBox1.Enabled = false;
            }
            else
            {
                int num;
                this.groupBox1.Enabled = true;
                SymbolIdentifierWrap selectedItem = this.listBoxControl1.SelectedItem as SymbolIdentifierWrap;
                ITextSymbol symbol = selectedItem.SymbolIdentifier.Symbol as ITextSymbol;
                stdole.IFontDisp font = (selectedItem.SymbolIdentifier.Symbol as ITextSymbol).Font;
                if (!NewObjectClassHelper.m_pObjectClassHelper.IsRelatedFeature)
                {
                    if ((selectedItem.AnnotateLayerProperties.AnnotationMaximumScale != 0.0) ||
                        (selectedItem.AnnotateLayerProperties.AnnotationMinimumScale != 0.0))
                    {
                        this.rdoDisplayScale.SelectedIndex = 1;
                        this.panelScaleSet.Enabled = true;
                        this.txtMaxScale.Text = selectedItem.AnnotateLayerProperties.AnnotationMaximumScale.ToString();
                        this.txtMinScale.Text = selectedItem.AnnotateLayerProperties.AnnotationMinimumScale.ToString();
                    }
                    else
                    {
                        this.rdoDisplayScale.SelectedIndex = 0;
                        this.panelScaleSet.Enabled = false;
                    }
                    this.method_0(this.colorEdit1, symbol.Color);
                    num = 0;
                    while (num < this.cboFontName.Items.Count)
                    {
                        if (symbol.Font.Name == this.cboFontName.Items[num].ToString())
                        {
                            this.cboFontName.SelectedIndex = num;
                            break;
                        }
                        num++;
                    }
                }
                else
                {
                    this.method_0(this.colorEdit2, symbol.Color);
                    for (num = 0; num < this.cboFontName_rel.Items.Count; num++)
                    {
                        if (symbol.Font.Name == this.cboFontName_rel.Items[num].ToString())
                        {
                            this.cboFontName_rel.SelectedIndex = num;
                            break;
                        }
                    }
                    this.cboFontSize_rel.Text = symbol.Size.ToString();
                    this.chkBold_rel.Checked = font.Bold;
                    this.chkIta_rel.Checked = font.Italic;
                    this.chkUnderLine_rel.Checked = font.Underline;
                    return;
                }
                this.cboFontSize.Text = symbol.Size.ToString();
                this.chkBold.Checked = font.Bold;
                this.chkItalic.Checked = font.Italic;
                this.chkUnderline.Checked = font.Underline;
            }
        }

        private void method_0(ColorEdit colorEdit_0, IColor icolor_0)
        {
            if (icolor_0.NullColor)
            {
                colorEdit_0.Color = Color.Empty;
            }
            else
            {
                int num2;
                int num3;
                int num4;
                int rGB = icolor_0.RGB;
                this.method_1((uint) rGB, out num2, out num3, out num4);
                colorEdit_0.Color = Color.FromArgb(icolor_0.Transparency, num2, num3, num4);
            }
        }

        private void method_1(uint uint_0, out int int_0, out int int_1, out int int_2)
        {
            uint num = uint_0 & 16711680;
            int_2 = (int) (num >> 16);
            num = uint_0 & 65280;
            int_1 = (int) (num >> 8);
            num = uint_0 & 255;
            int_0 = (int) num;
        }

        private int method_2(int int_0, int int_1, int int_2)
        {
            uint num = 0;
            num = (uint) (0 | int_2);
            num = num << 8;
            num |= (uint) int_1;
            num = num << 8;
            num |= (uint) int_0;
            return (int) num;
        }

        private void method_3(ColorEdit colorEdit_0, IColor icolor_0)
        {
            icolor_0.NullColor = colorEdit_0.Color.IsEmpty;
            if (!colorEdit_0.Color.IsEmpty)
            {
                icolor_0.Transparency = colorEdit_0.Color.A;
                icolor_0.RGB = this.method_2(colorEdit_0.Color.R, colorEdit_0.Color.G, colorEdit_0.Color.B);
            }
        }

        private int method_4(ISymbolCollection2 isymbolCollection2_0)
        {
            isymbolCollection2_0.Reset();
            ISymbolIdentifier2 identifier = isymbolCollection2_0.Next() as ISymbolIdentifier2;
            IList list = new ArrayList();
            while (identifier != null)
            {
                list.Add(identifier.ID);
                identifier = isymbolCollection2_0.Next() as ISymbolIdentifier2;
            }
            int num = 0;
            if (list.Count != 0)
            {
                while (list.IndexOf(num) != -1)
                {
                    num++;
                }
                return num;
            }
            return num;
        }

        private void method_5(SymbolIdentifierWrap symbolIdentifierWrap_0)
        {
            NewObjectClassHelper.m_pObjectClassHelper.m_pSymbolColl.Replace(symbolIdentifierWrap_0.SymbolIdentifier.ID,
                symbolIdentifierWrap_0.SymbolIdentifier.Symbol);
            NewObjectClassHelper.m_pObjectClassHelper.m_pAnnoPropertiesColn.Replace(this.listBoxControl1.SelectedIndex,
                symbolIdentifierWrap_0.AnnotateLayerProperties);
        }

        private void rdoDisplayScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            SymbolIdentifierWrap selectedItem;
            if (this.rdoDisplayScale.SelectedIndex == 0)
            {
                this.panelScaleSet.Enabled = false;
                if (this.listBoxControl1.SelectedIndex != -1)
                {
                    selectedItem = this.listBoxControl1.SelectedItem as SymbolIdentifierWrap;
                    if ((selectedItem.AnnotateLayerProperties.AnnotationMaximumScale != 0.0) ||
                        (selectedItem.AnnotateLayerProperties.AnnotationMinimumScale != 0.0))
                    {
                        selectedItem.AnnotateLayerProperties.AnnotationMaximumScale = 0.0;
                        selectedItem.AnnotateLayerProperties.AnnotationMinimumScale = 0.0;
                        this.method_5(selectedItem);
                    }
                }
            }
            else
            {
                this.panelScaleSet.Enabled = true;
                if (this.listBoxControl1.SelectedIndex != -1)
                {
                    selectedItem = this.listBoxControl1.SelectedItem as SymbolIdentifierWrap;
                    double num = 0.0;
                    double num2 = 0.0;
                    try
                    {
                        num = double.Parse(this.txtMinScale.Text);
                        num2 = double.Parse(this.txtMaxScale.Text);
                    }
                    catch
                    {
                    }
                    if ((num != 0.0) || (num2 != 0.0))
                    {
                        selectedItem.AnnotateLayerProperties.AnnotationMaximumScale = num2;
                        selectedItem.AnnotateLayerProperties.AnnotationMinimumScale = num;
                        this.method_5(selectedItem);
                    }
                }
            }
        }

        private void txtMaxScale_EditValueChanged(object sender, EventArgs e)
        {
            if (this.listBoxControl1.SelectedIndex != -1)
            {
                SymbolIdentifierWrap selectedItem = this.listBoxControl1.SelectedItem as SymbolIdentifierWrap;
                double num = 0.0;
                try
                {
                    num = double.Parse(this.txtMaxScale.Text);
                }
                catch
                {
                    MessageBox.Show("请输入数字!");
                }
                if (num != 0.0)
                {
                    selectedItem.AnnotateLayerProperties.AnnotationMaximumScale = num;
                    this.method_5(selectedItem);
                }
            }
        }

        private void txtMinScale_EditValueChanged(object sender, EventArgs e)
        {
            if (this.listBoxControl1.SelectedIndex != -1)
            {
                SymbolIdentifierWrap selectedItem = this.listBoxControl1.SelectedItem as SymbolIdentifierWrap;
                double num = 0.0;
                try
                {
                    num = double.Parse(this.txtMinScale.Text);
                }
                catch
                {
                    MessageBox.Show("请输入数字!");
                }
                if (num != 0.0)
                {
                    selectedItem.AnnotateLayerProperties.AnnotationMinimumScale = num;
                    this.method_5(selectedItem);
                }
            }
        }

        internal partial class SymbolIdentifierWrap
        {
            private IAnnotateLayerProperties iannotateLayerProperties_0 = null;
            private ISymbolIdentifier2 isymbolIdentifier2_0 = null;

            public SymbolIdentifierWrap(IAnnotateLayerProperties iannotateLayerProperties_1,
                ISymbolIdentifier2 isymbolIdentifier2_1)
            {
                this.iannotateLayerProperties_0 = iannotateLayerProperties_1;
                this.isymbolIdentifier2_0 = isymbolIdentifier2_1;
            }

            public override string ToString()
            {
                return this.iannotateLayerProperties_0.Class;
            }

            public IAnnotateLayerProperties AnnotateLayerProperties
            {
                get { return this.iannotateLayerProperties_0; }
            }

            public ISymbolIdentifier2 SymbolIdentifier
            {
                get { return this.isymbolIdentifier2_0; }
            }
        }
    }
}