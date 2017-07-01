using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    internal partial class AnnoEditControl : UserControl
    {
        private bool m_CanDo = false;
        private IActiveView m_pActiveView = null;
        private IAnnotationFeature m_pAnnoFeat = null;
        private ITextElement m_pTextElement = null;

        public AnnoEditControl()
        {
            this.InitializeComponent();
            this.cboFontName.Items.Clear();
            InstalledFontCollection fonts = new InstalledFontCollection();
            for (int i = 0; i < fonts.Families.Length; i++)
            {
                this.cboFontName.Items.Add(fonts.Families[i].Name);
            }
        }

        private void AnnoEditControl_Load(object sender, EventArgs e)
        {
            this.Init();
            this.m_CanDo = true;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (this.m_pAnnoFeat != null)
            {
                try
                {
                    IDataset dataset = (this.m_pAnnoFeat as IObject).Class as IDataset;
                    IWorkspaceEdit workspace = dataset.Workspace as IWorkspaceEdit;
                    workspace.StartEditOperation();
                    this.m_pAnnoFeat.Annotation = this.m_pTextElement as IElement;
                    (this.m_pAnnoFeat as IFeature).Store();
                    workspace.StopEditOperation();
                    this.m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
                catch (Exception exception)
                {
                    Logger.Current.Error("", exception, "");
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.m_pTextElement = this.m_pAnnoFeat.Annotation as ITextElement;
            this.Init();
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

        private void cboFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        private void GetRGB(uint rgb, out int r, out int g, out int b)
        {
            uint num = rgb & 16711680;
            b = (int) (num >> 16);
            num = rgb & 65280;
            g = (int) (num >> 8);
            num = rgb & 255;
            r = (int) num;
        }

        private void Init()
        {
            this.memoEdit1.Text = (this.m_pTextElement as ISymbolCollectionElement).Text;
            this.SetColorEdit(this.colorEdit1, (this.m_pTextElement as ISymbolCollectionElement).Color);
            this.chkBold.Checked = (this.m_pTextElement as ISymbolCollectionElement).Bold;
            this.chkItalic.Checked = (this.m_pTextElement as ISymbolCollectionElement).Italic;
            this.chkUnderline.Checked = (this.m_pTextElement as ISymbolCollectionElement).Underline;
            switch ((this.m_pTextElement as ISymbolCollectionElement).HorizontalAlignment)
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
            this.cboFontName.Text = (this.m_pTextElement as ISymbolCollectionElement).FontName;
            this.cboFontSize.Text = (this.m_pTextElement as ISymbolCollectionElement).Size.ToString();
        }

        private void memoEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_pTextElement.Text = this.memoEdit1.Text;
            }
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

        public IActiveView ActiveView
        {
            set { this.m_pActiveView = value; }
        }

        public IAnnotationFeature AnnotationFeature
        {
            set
            {
                this.m_pAnnoFeat = value;
                if (this.m_pAnnoFeat != null)
                {
                    this.m_pTextElement = this.m_pAnnoFeat.Annotation as ITextElement;
                    if (this.m_CanDo)
                    {
                        this.m_CanDo = false;
                        this.Init();
                        this.m_CanDo = true;
                    }
                }
            }
        }
    }
}