using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.Shared;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public partial class frmUpdateAnnoAttribute : Form
    {
        private bool m_CanDo = false;
        private IList m_pAnnoFeatureList = null;
        private ITextElement m_pTextElement = null;

        public frmUpdateAnnoAttribute()
        {
            this.InitializeComponent();
            this.cboFontName.Items.Clear();
            InstalledFontCollection fonts = new InstalledFontCollection();
            for (int i = 0; i < fonts.Families.Length; i++)
            {
                this.cboFontName.Items.Add(fonts.Families[i].Name);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            IWorkspaceEdit workspace = null;
            for (int i = 0; i < this.m_pAnnoFeatureList.Count; i++)
            {
                IAnnotationFeature feature = this.m_pAnnoFeatureList[i] as IAnnotationFeature;
                try
                {
                    if (workspace == null)
                    {
                        IDataset dataset = (feature as IObject).Class as IDataset;
                        workspace = dataset.Workspace as IWorkspaceEdit;
                        workspace.StartEditOperation();
                    }
                    ITextElement annotation = feature.Annotation as ITextElement;
                    annotation.Symbol = this.m_pTextElement.Symbol;
                    feature.Annotation = annotation as IElement;
                    (feature as IFeature).Store();
                }
                catch (Exception exception)
                {
                    Logger.Current.Error("", exception, "");
                }
            }
            if (workspace != null)
            {
                workspace.StopEditOperation();
            }
        }

        private void cboFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                (this.m_pTextElement as ISymbolCollectionElement).FontName = this.cboFontName.Text;
            }
        }

        private void cboFontSize_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                try
                {
                    (this.m_pTextElement as ISymbolCollectionElement).Size = double.Parse(this.cboFontSize.Text);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void chkBold_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                (this.m_pTextElement as ISymbolCollectionElement).Bold = this.chkBold.Checked;
            }
        }

        private void chkItalic_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                (this.m_pTextElement as ISymbolCollectionElement).Italic = this.chkItalic.Checked;
            }
        }

        private void chkUnderline_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                (this.m_pTextElement as ISymbolCollectionElement).Underline = this.chkUnderline.Checked;
            }
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IColor pColor = (this.m_pTextElement as ISymbolCollectionElement).Color;
                this.UpdateColorFromColorEdit(this.colorEdit1, pColor);
                (this.m_pTextElement as ISymbolCollectionElement).Color = pColor;
            }
        }

        private void frmUpdateAnnoAttribute_Load(object sender, EventArgs e)
        {
            if (this.m_pAnnoFeatureList.Count > 0)
            {
                this.m_pTextElement = (this.m_pAnnoFeatureList[0] as IAnnotationFeature).Annotation as ITextElement;
                this.Init(this.m_pTextElement);
            }
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

        private void Init(ITextElement pTextElement)
        {
            this.SetColorEdit(this.colorEdit1, (pTextElement as ISymbolCollectionElement).Color);
            this.chkBold.Checked = (pTextElement as ISymbolCollectionElement).Bold;
            this.chkItalic.Checked = (pTextElement as ISymbolCollectionElement).Italic;
            this.chkUnderline.Checked = (pTextElement as ISymbolCollectionElement).Underline;
            switch ((pTextElement as ISymbolCollectionElement).HorizontalAlignment)
            {
                case esriTextHorizontalAlignment.esriTHALeft:
                    this.rdoTHALeft.Checked = true;
                    break;

                case esriTextHorizontalAlignment.esriTHACenter:
                    this.rdoTHACenter.Checked = true;
                    break;

                case esriTextHorizontalAlignment.esriTHARight:
                    this.rdoTHARight.Checked = true;
                    break;

                case esriTextHorizontalAlignment.esriTHAFull:
                    this.rdoTHAFul.Checked = true;
                    break;
            }
            this.cboFontName.Text = (pTextElement as ISymbolCollectionElement).FontName;
            this.cboFontSize.Text = (pTextElement as ISymbolCollectionElement).Size.ToString();
            this.symbolItem1.Symbol = this.m_pTextElement.Symbol;
        }

        private void rdoTHACenter_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo && this.rdoTHACenter.Checked)
            {
                (this.m_pTextElement as ISymbolCollectionElement).HorizontalAlignment =
                    esriTextHorizontalAlignment.esriTHACenter;
            }
        }

        private void rdoTHAFul_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo && this.rdoTHAFul.Checked)
            {
                (this.m_pTextElement as ISymbolCollectionElement).HorizontalAlignment =
                    esriTextHorizontalAlignment.esriTHAFull;
            }
        }

        private void rdoTHALeft_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo && this.rdoTHALeft.Checked)
            {
                (this.m_pTextElement as ISymbolCollectionElement).HorizontalAlignment =
                    esriTextHorizontalAlignment.esriTHALeft;
            }
        }

        private void rdoTHARight_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo && this.rdoTHARight.Checked)
            {
                (this.m_pTextElement as ISymbolCollectionElement).HorizontalAlignment =
                    esriTextHorizontalAlignment.esriTHARight;
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

        private void UpdateColorFromColorEdit(ColorEdit colorEdit, IColor pColor)
        {
            pColor.NullColor = colorEdit.Color.IsEmpty;
            if (!colorEdit.Color.IsEmpty)
            {
                pColor.Transparency = colorEdit.Color.A;
                pColor.RGB = this.RGB(colorEdit.Color.R, colorEdit.Color.G, colorEdit.Color.B);
            }
        }

        public IList AnnoFeatureList
        {
            set { this.m_pAnnoFeatureList = value; }
        }
    }
}