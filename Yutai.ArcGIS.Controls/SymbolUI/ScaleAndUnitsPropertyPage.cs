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
    public partial class ScaleAndUnitsPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private IStyleGallery m_pSG ;//= ApplicationBase.StyleGallery;
        private string m_Title = "比例和单位";

        public event OnValueChangeEventHandler OnValueChange;

        public ScaleAndUnitsPropertyPage(IAppContext context)
        {
            this.InitializeComponent();
            _context = context;
            this.m_pSG = _context.StyleGallery;
            ScaleBarEventsClass.ValueChange += new ScaleBarEventsClass.ValueChangeHandler(this.ScaleBarEventsClass_ValueChange);
        }
        public ScaleAndUnitsPropertyPage()
        {
            this.InitializeComponent();
            ScaleBarEventsClass.ValueChange += new ScaleBarEventsClass.ValueChangeHandler(this.ScaleBarEventsClass_ValueChange);
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                (ScaleBarFormatPropertyPage.m_pOldScaleBar as IClone).Assign(ScaleBarFormatPropertyPage.m_pScaleBar as IClone);
                this.m_IsPageDirty = false;
            }
        }

        private void btnSymbolSelector_Click(object sender, EventArgs e)
        {
            try
            {
                ITextSymbol unitLabelSymbol = ScaleBarFormatPropertyPage.m_pScaleBar.UnitLabelSymbol;
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol(unitLabelSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        unitLabelSymbol = selector.GetSymbol() as ITextSymbol;
                        ScaleBarFormatPropertyPage.m_pScaleBar.UnitLabelSymbol = unitLabelSymbol;
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

        private void cboLabelPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IScaleBar pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    pScaleBar.UnitLabelPosition = (esriScaleBarPos) this.cboLabelPosition.SelectedIndex;
                    this.ValueChanged();
                }
                catch
                {
                }
            }
        }

        private void cboResizeHint_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IScaleBar pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    pScaleBar.ResizeHint = (esriScaleBarResizeHint) this.cboResizeHint.SelectedIndex;
                    this.ValueChanged();
                }
                catch
                {
                }
            }
        }

        private void cboUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IScaleBar pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    pScaleBar.Units = (esriUnits) this.cboUnits.SelectedIndex;
                    this.ValueChanged();
                }
                catch
                {
                }
            }
        }

        private void chkDivisionsBeforeZero_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IScaleBar pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    if (this.chkDivisionsBeforeZero.Checked)
                    {
                        pScaleBar.DivisionsBeforeZero = 1;
                    }
                    else
                    {
                        pScaleBar.DivisionsBeforeZero = 0;
                    }
                    this.ValueChanged();
                }
                catch
                {
                }
            }
        }

        private void chkDivisionsBeforeZero_Click(object sender, EventArgs e)
        {
        }

 public void Hide()
        {
            base.Hide();
        }

        private void Init()
        {
            IScaleBar pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar;
            this.txtDivisions.Text = pScaleBar.Divisions.ToString();
            this.txtsubDivisions.Text = pScaleBar.Subdivisions.ToString();
            this.chkDivisionsBeforeZero.Checked = pScaleBar.DivisionsBeforeZero == 1;
            this.cboResizeHint.SelectedIndex = (int) pScaleBar.ResizeHint;
            this.cboUnits.SelectedIndex = (int) pScaleBar.Units;
            this.cboLabelPosition.SelectedIndex = (int) pScaleBar.UnitLabelPosition;
            this.txtLabel.Text = pScaleBar.UnitLabel;
            this.txtGap.Text = pScaleBar.UnitLabelGap.ToString("#.##");
        }

 private void ScaleAndUnitsPropertyPage_Load(object sender, EventArgs e)
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

        private void txtDivisions_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IScaleBar pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    pScaleBar.Divisions = short.Parse(this.txtDivisions.Text);
                    this.ValueChanged();
                }
                catch
                {
                }
            }
        }

        private void txtGap_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IScaleBar pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    pScaleBar.UnitLabelGap = double.Parse(this.txtGap.Text);
                    this.ValueChanged();
                }
                catch
                {
                }
            }
        }

        private void txtLabel_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IScaleBar pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    pScaleBar.UnitLabel = this.txtLabel.Text;
                    this.ValueChanged();
                }
                catch
                {
                }
            }
        }

        private void txtsubDivisions_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                IScaleBar pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    pScaleBar.Subdivisions = short.Parse(this.txtsubDivisions.Text);
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
            get
            {
                return this.m_IsPageDirty;
            }
        }

        int IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public string Title
        {
            get
            {
                return this.m_Title;
            }
            set
            {
                this.m_Title = value;
            }
        }
    }
}

