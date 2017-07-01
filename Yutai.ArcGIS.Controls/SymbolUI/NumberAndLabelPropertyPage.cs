using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public partial class NumberAndLabelPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private IStyleGallery m_pSG; //= ApplicationBase.StyleGallery;
        private string m_Title = "数字和标注";

        public event OnValueChangeEventHandler OnValueChange;

        public NumberAndLabelPropertyPage(IAppContext context)
        {
            this.InitializeComponent();
            _context = context;
            this.m_pSG = _context.StyleGallery;
            ScaleBarEventsClass.ValueChange +=
                new ScaleBarEventsClass.ValueChangeHandler(this.ScaleBarEventsClass_ValueChange);
        }

        public NumberAndLabelPropertyPage()
        {
            this.InitializeComponent();
            ScaleBarEventsClass.ValueChange +=
                new ScaleBarEventsClass.ValueChangeHandler(this.ScaleBarEventsClass_ValueChange);
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                (ScaleBarFormatPropertyPage.m_pOldScaleBar as IClone).Assign(
                    ScaleBarFormatPropertyPage.m_pScaleBar as IClone);
                this.m_IsPageDirty = false;
            }
        }

        private void btnDivisionMarkSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                IScaleMarks pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar as IScaleMarks;
                ILineSymbol divisionMarkSymbol = pScaleBar.DivisionMarkSymbol;
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol(divisionMarkSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        divisionMarkSymbol = selector.GetSymbol() as ILineSymbol;
                        pScaleBar.DivisionMarkSymbol = divisionMarkSymbol;
                        this.ValueChanged();
                    }
                }
            }
            catch
            {
            }
        }

        private void btnLabelSymbolSelector_Click(object sender, EventArgs e)
        {
            try
            {
                ITextSymbol labelSymbol = ScaleBarFormatPropertyPage.m_pScaleBar.LabelSymbol;
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol(labelSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        labelSymbol = selector.GetSymbol() as ITextSymbol;
                        ScaleBarFormatPropertyPage.m_pScaleBar.LabelSymbol = labelSymbol;
                        this.ValueChanged();
                    }
                }
            }
            catch
            {
            }
        }

        private void btnSubdivisionMarkSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                IScaleMarks pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar as IScaleMarks;
                ILineSymbol subdivisionMarkSymbol = pScaleBar.SubdivisionMarkSymbol;
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol(subdivisionMarkSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        subdivisionMarkSymbol = selector.GetSymbol() as ILineSymbol;
                        pScaleBar.SubdivisionMarkSymbol = subdivisionMarkSymbol;
                        this.ValueChanged();
                    }
                }
            }
            catch
            {
            }
        }

        public void Cancel()
        {
        }

        private void cboLabelFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ScaleBarFormatPropertyPage.m_pScaleBar.LabelFrequency =
                    (esriScaleBarFrequency) this.cboLabelFrequency.SelectedIndex;
                this.ValueChanged();
            }
        }

        private void cboLabelPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ScaleBarFormatPropertyPage.m_pScaleBar.LabelPosition =
                    (esriVertPosEnum) this.cboLabelPosition.SelectedIndex;
                this.ValueChanged();
            }
        }

        private void cboMarkFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IScaleMarks pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar as IScaleMarks;
                pScaleBar.MarkFrequency = (esriScaleBarFrequency) this.cboMarkFrequency.SelectedIndex;
                this.ValueChanged();
            }
        }

        private void cboMarkPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IScaleMarks pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar as IScaleMarks;
                pScaleBar.MarkPosition = (esriVertPosEnum) this.cboMarkPosition.SelectedIndex;
                this.ValueChanged();
            }
        }

        public void Hide()
        {
        }

        private void Init()
        {
            IScaleBar pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar;
            IScaleMarks marks = pScaleBar as IScaleMarks;
            this.cboMarkFrequency.SelectedIndex = (int) marks.MarkFrequency;
            this.cboMarkPosition.SelectedIndex = (int) marks.MarkPosition;
            this.txtLabelGap.Text = pScaleBar.LabelGap.ToString("#.##");
            this.cboLabelFrequency.SelectedIndex = (int) pScaleBar.LabelFrequency;
            this.cboLabelPosition.SelectedIndex = (int) pScaleBar.LabelPosition;
            this.txtDivisionMarkHeight.Text = marks.DivisionMarkHeight.ToString("#.##");
            this.txtSubdivisionMarkHeight.Text = marks.SubdivisionMarkHeight.ToString("#.##");
            if (pScaleBar is IScaleLine)
            {
                this.lblMarkHeight.Enabled = true;
                this.lblPosition.Enabled = true;
                this.lblsubMarkHeight.Enabled = true;
                this.cboLabelPosition.Enabled = true;
                this.txtDivisionMarkHeight.Enabled = true;
                this.txtSubdivisionMarkHeight.Enabled = true;
            }
            else
            {
                this.lblMarkHeight.Enabled = false;
                this.lblPosition.Enabled = false;
                this.lblsubMarkHeight.Enabled = false;
                this.cboLabelPosition.Enabled = false;
                this.txtDivisionMarkHeight.Enabled = false;
                this.txtSubdivisionMarkHeight.Enabled = false;
            }
        }

        private void NumberAndLabelPropertyPage_Load(object sender, EventArgs e)
        {
            this.Init();
            this.m_CanDo = true;
        }

        private void ScaleBarEventsClass_ValueChange(object sender)
        {
            this.m_CanDo = false;
            this.Init();
            this.m_CanDo = true;
        }

        public void SetObjects(object @object)
        {
            if (ScaleBarFormatPropertyPage.m_pScaleBar == null)
            {
                ScaleBarFormatPropertyPage.m_pOldScaleBar = @object as IScaleBar;
                ScaleBarFormatPropertyPage.m_pScaleBar = (@object as IClone).Clone() as IScaleBar;
            }
        }

        private void txtDivisionMarkHeight_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IScaleMarks pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar as IScaleMarks;
                try
                {
                    pScaleBar.DivisionMarkHeight = double.Parse(this.txtDivisionMarkHeight.Text);
                    this.ValueChanged();
                }
                catch
                {
                }
            }
        }

        private void txtLabelGap_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IScaleBar pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    pScaleBar.LabelGap = double.Parse(this.txtLabelGap.Text);
                    this.ValueChanged();
                }
                catch
                {
                }
            }
        }

        private void txtSubdivisionMarkHeight_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IScaleMarks pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar as IScaleMarks;
                try
                {
                    pScaleBar.SubdivisionMarkHeight = double.Parse(this.txtSubdivisionMarkHeight.Text);
                    this.ValueChanged();
                }
                catch
                {
                }
            }
        }

        private void ValueChanged()
        {
            this.m_IsPageDirty = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        public bool IsPageDirty
        {
            get { return this.m_IsPageDirty; }
        }

        int IPropertyPage.Height
        {
            get { return base.Height; }
        }

        int IPropertyPage.Width
        {
            get { return base.Width; }
        }

        public string Title
        {
            get { return this.m_Title; }
            set { this.m_Title = value; }
        }
    }
}